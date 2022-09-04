namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class ScalarBaseValidator
{
    public static IncrementalValuesProvider<ScalarBaseType> Validate(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<ScalarBaseType> scalarProvider,
        IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulationWithData> scalarPopulationProvider,
        IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider)
    {
        return scalarProvider.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(Validate).ReportDiagnostics(context);
    }

    private static IOptionalWithDiagnostics<ScalarBaseType> Validate((ScalarBaseType UnvalidatedScalar, IUnitPopulation UnitPopulation, IScalarPopulationWithData ScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken _)
    {
        var scalar = ValidateScalar(input.UnvalidatedScalar, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);

        if (scalar.LacksResult)
        {
            return scalar.AsEmptyOptional<ScalarBaseType>();
        }

        var derivations = ValidateDerivations(input.UnvalidatedScalar, input.ScalarPopulation, input.VectorPopulation);
        var constants = ValidateConstants(input.UnvalidatedScalar, input.UnitPopulation);
        var conversions = ValidateConversions(input.UnvalidatedScalar, input.ScalarPopulation);

        var includedUnitsInstanceNames = new HashSet<string>(input.UnitPopulation.Units[input.UnvalidatedScalar.Definition.Unit].UnitInstancesByName.Keys);

        var unitBaseInclusions = ValidateIncludeUnitBaseInstances(input.UnvalidatedScalar, input.UnitPopulation, includedUnitsInstanceNames);
        var unitBaseExclusions = ValidateExcludeUnitBaseInstances(input.UnvalidatedScalar, input.UnitPopulation, includedUnitsInstanceNames);
        var unitInclusions = ValidateIncludeUnitInstances(input.UnvalidatedScalar, input.UnitPopulation, includedUnitsInstanceNames);
        var unitExclusions = ValidateExcludeUnitInstances(input.UnvalidatedScalar, input.UnitPopulation, includedUnitsInstanceNames);

        ScalarBaseType product = new(input.UnvalidatedScalar.Type, input.UnvalidatedScalar.TypeLocation, scalar.Result, derivations.Result, constants.Result, conversions.Result, unitBaseInclusions.Result,
            unitBaseExclusions.Result, unitInclusions.Result, unitExclusions.Result);

        var allDiagnostics = scalar.Concat(derivations).Concat(constants).Concat(conversions).Concat(unitBaseInclusions).Concat(unitBaseExclusions).Concat(unitInclusions).Concat(unitExclusions);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<SharpMeasuresScalarDefinition> ValidateScalar(ScalarBaseType scalarType, IUnitPopulation unitPopulation, IScalarPopulationWithData scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new SharpMeasuresScalarValidationContext(scalarType.Type, unitPopulation, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(SharpMeasuresScalarValidator).Filter(validationContext, scalarType.Definition);
    }

    private static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ValidateDerivations(ScalarBaseType scalarType, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new DerivedQuantityValidationContext(scalarType.Type, scalarPopulation, vectorPopulation);

        return ValidityFilter.Create(DerivedQuantityValidator).Filter(validationContext, scalarType.Derivations);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ScalarConstantDefinition>> ValidateConstants(ScalarBaseType scalarType, IUnitPopulation unitPopulation)
    {
        var unit = unitPopulation.Units[scalarType.Definition.Unit];

        var includedUnitBases = GetUnitInclusions(unit, scalarType.UnitBaseInstanceInclusions, () => scalarType.UnitBaseInstanceExclusions);
        var includedUnits = GetUnitInclusions(unit, scalarType.UnitInstanceInclusions, () => scalarType.UnitInstanceExclusions);

        HashSet<string> includedUnitInstanceNames = new(includedUnitBases);
        HashSet<string> incluedUnitInstancePluralForms = new(includedUnits.Select((unitInstance) => unit.UnitInstancesByName[unitInstance].PluralForm));

        var validationContext = new ScalarConstantValidationContext(scalarType.Type, unit, new HashSet<string>(), new HashSet<string>(), includedUnitInstanceNames, incluedUnitInstancePluralForms);

        return ProcessingFilter.Create(ScalarConstantValidator).Filter(validationContext, scalarType.Constants);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ConvertibleScalarDefinition>> ValidateConversions(ScalarBaseType scalarType, IScalarPopulation scalarPopulation)
    {
        var filteringContext = new ConvertibleScalarFilteringContext(scalarType.Type, scalarType.Definition.UseUnitBias, scalarPopulation, new HashSet<NamedType>());

        return ProcessingFilter.Create(ConvertibleScalarFilterer).Filter(filteringContext, scalarType.Conversions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<IncludeUnitBasesDefinition>> ValidateIncludeUnitBaseInstances(ScalarBaseType scalarType, IUnitPopulation unitPopulation, HashSet<string> includedUnitInstanceNames)
    {
        var filteringContext = new IncludeBasesFilteringContext(scalarType.Type, unitPopulation.Units[scalarType.Definition.Unit], includedUnitInstanceNames);

        return ProcessingFilter.Create(IncludeBasesFilterer).Filter(filteringContext, scalarType.UnitBaseInstanceInclusions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitBasesDefinition>> ValidateExcludeUnitBaseInstances(ScalarBaseType scalarType, IUnitPopulation unitPopulation, HashSet<string> includedUnitInstanceNames)
    {
        var filteringContext = new ExcludeBasesFilteringContext(scalarType.Type, unitPopulation.Units[scalarType.Definition.Unit], includedUnitInstanceNames);

        return ProcessingFilter.Create(ExcludeBasesFilterer).Filter(filteringContext, scalarType.UnitBaseInstanceExclusions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<IncludeUnitsDefinition>> ValidateIncludeUnitInstances(ScalarBaseType scalarType, IUnitPopulation unitPopulation, HashSet<string> includedUnitInstanceNames)
    {
        var filteringContext = new IncludeUnitsFilteringContext(scalarType.Type, unitPopulation.Units[scalarType.Definition.Unit], includedUnitInstanceNames);

        return ProcessingFilter.Create(IncludeUnitsFilterer).Filter(filteringContext, scalarType.UnitInstanceInclusions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ValidateExcludeUnitInstances(ScalarBaseType scalarType, IUnitPopulation unitPopulation, HashSet<string> includedUnitInstanceNames)
    {
        var filteringContext = new ExcludeUnitsFilteringContext(scalarType.Type, unitPopulation.Units[scalarType.Definition.Unit], includedUnitInstanceNames);

        return ProcessingFilter.Create(ExcludeUnitsFilterer).Filter(filteringContext, scalarType.UnitInstanceExclusions);
    }

    private static IReadOnlyList<string> GetUnitInclusions(IUnitType unit, IEnumerable<IUnitInstanceList> inclusions, Func<IEnumerable<IUnitInstanceList>> exclusionsDelegate)
    {
        HashSet<string> includedUnitInstances = new(unit.UnitInstancesByName.Keys);

        if (inclusions.Any())
        {
            includedUnitInstances.IntersectWith(inclusions.SelectMany(static (unitList) => unitList.UnitInstances));

            return includedUnitInstances.ToList();
        }

        includedUnitInstances.ExceptWith(exclusionsDelegate().SelectMany(static (unitList) => unitList.UnitInstances));

        return includedUnitInstances.ToList();
    }

    private static SharpMeasuresScalarValidator SharpMeasuresScalarValidator { get; } = new(SharpMeasuresScalarValidationDiagnostics.Instance);

    private static DerivedQuantityValidator DerivedQuantityValidator { get; } = new(DerivedQuantityValidationDiagnostics.Instance);
    private static ScalarConstantValidator ScalarConstantValidator { get; } = new(ScalarConstantValidationDiagnostics.Instance);
    private static ConvertibleScalarFilterer ConvertibleScalarFilterer { get; } = new(ConvertibleScalarFilteringDiagnostics.Instance);

    private static IncludeUnitBasesFilterer IncludeBasesFilterer { get; } = new(IncludeBasesFilteringDiagnostics.Instance);
    private static ExcludeUnitBasesFilterer ExcludeBasesFilterer { get; } = new(ExcludeBasesFilteringDiagnostics.Instance);
    private static IncludeUnitsFilterer IncludeUnitsFilterer { get; } = new(IncludeUnitsFilteringDiagnostics.Instance);
    private static ExcludeUnitsFilterer ExcludeUnitsFilterer { get; } = new(ExcludeUnitsFilteringDiagnostics.Instance);
}
