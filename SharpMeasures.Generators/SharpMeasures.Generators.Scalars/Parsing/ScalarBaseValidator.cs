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
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeBases;
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

        var availableUnitsAndBases = new HashSet<string>(input.UnitPopulation.Units[input.UnvalidatedScalar.Definition.Unit].UnitsByName.Keys);

        var baseInclusions = ValidateIncludeBases(input.UnvalidatedScalar, input.UnitPopulation, availableUnitsAndBases);
        var baseExclusions = ValidateExcludeBases(input.UnvalidatedScalar, input.UnitPopulation, availableUnitsAndBases);
        var unitInclusions = ValidateIncludeUnits(input.UnvalidatedScalar, input.UnitPopulation, availableUnitsAndBases);
        var unitExclusions = ValidateExcludeUnits(input.UnvalidatedScalar, input.UnitPopulation, availableUnitsAndBases);

        ScalarBaseType product = new(input.UnvalidatedScalar.Type, input.UnvalidatedScalar.TypeLocation, scalar.Result, derivations.Result, constants.Result, conversions.Result, baseInclusions.Result,
            baseExclusions.Result, unitInclusions.Result, unitExclusions.Result);

        var allDiagnostics = scalar.Concat(derivations).Concat(constants).Concat(conversions).Concat(baseInclusions).Concat(baseExclusions).Concat(unitInclusions).Concat(unitExclusions);

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

        var includedBases = GetUnitInclusions(unit, scalarType.BaseInclusions, () => scalarType.BaseExclusions);
        var includedUnits = GetUnitInclusions(unit, scalarType.UnitInclusions, () => scalarType.UnitExclusions);

        HashSet<string> includedUnitNames = new(includedBases);
        HashSet<string> incluedUnitPlurals = new(includedUnits.Select((unitInstance) => unit.UnitsByName[unitInstance].Plural));

        var validationContext = new ScalarConstantValidationContext(scalarType.Type, unit, new HashSet<string>(), new HashSet<string>(), includedUnitNames, incluedUnitPlurals);

        return ProcessingFilter.Create(ScalarConstantValidator).Filter(validationContext, scalarType.Constants);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ConvertibleScalarDefinition>> ValidateConversions(ScalarBaseType scalarType, IScalarPopulation scalarPopulation)
    {
        var filteringContext = new ConvertibleScalarFilteringContext(scalarType.Type, scalarType.Definition.UseUnitBias, scalarPopulation, new HashSet<NamedType>());

        return ProcessingFilter.Create(ConvertibleScalarFilterer).Filter(filteringContext, scalarType.Conversions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<IncludeBasesDefinition>> ValidateIncludeBases(ScalarBaseType scalarType, IUnitPopulation unitPopulation, HashSet<string> availableBases)
    {
        var filteringContext = new IncludeBasesFilteringContext(scalarType.Type, unitPopulation.Units[scalarType.Definition.Unit], availableBases);

        return ProcessingFilter.Create(IncludeBasesFilterer).Filter(filteringContext, scalarType.BaseInclusions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeBasesDefinition>> ValidateExcludeBases(ScalarBaseType scalarType, IUnitPopulation unitPopulation, HashSet<string> availableBases)
    {
        var filteringContext = new ExcludeBasesFilteringContext(scalarType.Type, unitPopulation.Units[scalarType.Definition.Unit], availableBases);

        return ProcessingFilter.Create(ExcludeBasesFilterer).Filter(filteringContext, scalarType.BaseExclusions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<IncludeUnitsDefinition>> ValidateIncludeUnits(ScalarBaseType scalarType, IUnitPopulation unitPopulation, HashSet<string> availableUnits)
    {
        var filteringContext = new IncludeUnitsFilteringContext(scalarType.Type, unitPopulation.Units[scalarType.Definition.Unit], availableUnits);

        return ProcessingFilter.Create(IncludeUnitsFilterer).Filter(filteringContext, scalarType.UnitInclusions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ValidateExcludeUnits(ScalarBaseType scalarType, IUnitPopulation unitPopulation, HashSet<string> availableUnits)
    {
        var filteringContext = new ExcludeUnitsFilteringContext(scalarType.Type, unitPopulation.Units[scalarType.Definition.Unit], availableUnits);

        return ProcessingFilter.Create(ExcludeUnitsFilterer).Filter(filteringContext, scalarType.UnitExclusions);
    }

    private static IReadOnlyList<string> GetUnitInclusions(IUnitType unit, IEnumerable<IUnitList> inclusions, Func<IEnumerable<IUnitList>> exclusionsDelegate)
    {
        HashSet<string> includedUnits = new(unit.UnitsByName.Keys);

        if (inclusions.Any())
        {
            includedUnits.IntersectWith(inclusions.SelectMany(static (unitList) => unitList.Units));

            return includedUnits.ToList();
        }

        includedUnits.ExceptWith(exclusionsDelegate().SelectMany(static (unitList) => unitList.Units));

        return includedUnits.ToList();
    }

    private static SharpMeasuresScalarValidator SharpMeasuresScalarValidator { get; } = new(SharpMeasuresScalarValidationDiagnostics.Instance);

    private static DerivedQuantityValidator DerivedQuantityValidator { get; } = new(DerivedQuantityValidationDiagnostics.Instance);
    private static ScalarConstantValidator ScalarConstantValidator { get; } = new(ScalarConstantValidationDiagnostics.Instance);
    private static ConvertibleScalarFilterer ConvertibleScalarFilterer { get; } = new(ConvertibleScalarFilteringDiagnostics.Instance);

    private static IncludeBasesFilterer IncludeBasesFilterer { get; } = new(IncludeBasesFilteringDiagnostics.Instance);
    private static ExcludeBasesFilterer ExcludeBasesFilterer { get; } = new(ExcludeBasesFilteringDiagnostics.Instance);
    private static IncludeUnitsFilterer IncludeUnitsFilterer { get; } = new(IncludeUnitsFilteringDiagnostics.Instance);
    private static ExcludeUnitsFilterer ExcludeUnitsFilterer { get; } = new(ExcludeUnitsFilteringDiagnostics.Instance);
}
