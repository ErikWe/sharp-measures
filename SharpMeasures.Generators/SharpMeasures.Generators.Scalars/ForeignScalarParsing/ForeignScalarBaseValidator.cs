namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.ForeignScalarParsing.Diagnostics.Validation;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System;
using System.Collections.Generic;
using System.Linq;

internal static class ForeignScalarBaseValidator
{
    public static Optional<ScalarBaseType> Validate(ScalarBaseType scalarType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var scalar = ValidateScalar(scalarType, unitPopulation, scalarPopulation, vectorPopulation);

        if (scalar.HasValue is false)
        {
            return new Optional<ScalarBaseType>();
        }

        var derivations = ValidateDerivations(scalarType, scalarPopulation, vectorPopulation);
        var constants = ValidateConstants(scalarType, unitPopulation);
        var conversions = ValidateConversions(scalarType, scalarPopulation);

        var includedUnitsInstanceNames = new HashSet<string>(unitPopulation.Units[scalarType.Definition.Unit].UnitInstancesByName.Keys);

        var unitBaseInclusions = ValidateIncludeUnitBaseInstances(scalarType, unitPopulation, includedUnitsInstanceNames);
        var unitBaseExclusions = ValidateExcludeUnitBaseInstances(scalarType, unitPopulation, includedUnitsInstanceNames);
        var unitInstanceInclusions = ValidateIncludeUnitInstances(scalarType, unitPopulation, includedUnitsInstanceNames);
        var unitInstanceExclusions = ValidateExcludeUnitInstances(scalarType, unitPopulation, includedUnitsInstanceNames);

        ScalarBaseType product = new(scalarType.Type, MinimalLocation.None, scalar.Value, derivations, constants, conversions, unitBaseInclusions, unitBaseExclusions, unitInstanceInclusions, unitInstanceExclusions);

        return product;
    }

    private static Optional<SharpMeasuresScalarDefinition> ValidateScalar(ScalarBaseType scalarType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        ScalarProcessingData processingData = new(new Dictionary<NamedType, IScalarBaseType>(), new Dictionary<NamedType, IScalarSpecializationType>(), new Dictionary<NamedType, IScalarSpecializationType>());

        var validationContext = new SharpMeasuresScalarValidationContext(scalarType.Type, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        var scalar = ProcessingFilter.Create(SharpMeasuresScalarValidator).Filter(validationContext, scalarType.Definition);

        if (scalar.LacksResult)
        {
            return new Optional<SharpMeasuresScalarDefinition>();
        }

        return scalar.Result;
    }

    private static IReadOnlyList<DerivedQuantityDefinition> ValidateDerivations(ScalarBaseType scalarType, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new DerivedQuantityValidationContext(scalarType.Type, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(DerivedQuantityValidator).Filter(validationContext, scalarType.Derivations).Result;
    }

    private static IReadOnlyList<ScalarConstantDefinition> ValidateConstants(ScalarBaseType scalarType, IUnitPopulation unitPopulation)
    {
        var unit = unitPopulation.Units[scalarType.Definition.Unit];

        var includedUnitInstanceBases = GetUnitInstanceInclusions(unit, scalarType.UnitBaseInstanceInclusions, () => scalarType.UnitBaseInstanceExclusions);
        var includedInstanceUnits = GetUnitInstanceInclusions(unit, scalarType.UnitInstanceInclusions, () => scalarType.UnitInstanceExclusions);

        HashSet<string> includedUnitInstanceNames = new(includedUnitInstanceBases);
        HashSet<string> incluedUnitInstancePluralForms = new(includedInstanceUnits.Select((unitInstance) => unit.UnitInstancesByName[unitInstance].PluralForm));

        var validationContext = new ScalarConstantValidationContext(scalarType.Type, unit, new HashSet<string>(), new HashSet<string>(), includedUnitInstanceNames, incluedUnitInstancePluralForms);

        return ProcessingFilter.Create(ScalarConstantValidator).Filter(validationContext, scalarType.Constants).Result;
    }

    private static IReadOnlyList<ConvertibleScalarDefinition> ValidateConversions(ScalarBaseType scalarType, IScalarPopulation scalarPopulation)
    {
        var filteringContext = new ConvertibleScalarFilteringContext(scalarType.Type, scalarType.Definition.UseUnitBias, scalarPopulation, new HashSet<NamedType>());

        return ProcessingFilter.Create(ConvertibleScalarFilterer).Filter(filteringContext, scalarType.Conversions).Result;
    }

    private static IReadOnlyList<IncludeUnitBasesDefinition> ValidateIncludeUnitBaseInstances(ScalarBaseType scalarType, IUnitPopulation unitPopulation, HashSet<string> includedUnitInstanceNames)
    {
        var filteringContext = new IncludeBasesFilteringContext(scalarType.Type, unitPopulation.Units[scalarType.Definition.Unit], includedUnitInstanceNames);

        return ProcessingFilter.Create(IncludeBasesFilterer).Filter(filteringContext, scalarType.UnitBaseInstanceInclusions).Result;
    }

    private static IReadOnlyList<ExcludeUnitBasesDefinition> ValidateExcludeUnitBaseInstances(ScalarBaseType scalarType, IUnitPopulation unitPopulation, HashSet<string> includedUnitInstanceNames)
    {
        var filteringContext = new ExcludeBasesFilteringContext(scalarType.Type, unitPopulation.Units[scalarType.Definition.Unit], includedUnitInstanceNames);

        return ProcessingFilter.Create(ExcludeBasesFilterer).Filter(filteringContext, scalarType.UnitBaseInstanceExclusions).Result;
    }

    private static IReadOnlyList<IncludeUnitsDefinition> ValidateIncludeUnitInstances(ScalarBaseType scalarType, IUnitPopulation unitPopulation, HashSet<string> includedUnitInstanceNames)
    {
        var filteringContext = new IncludeUnitsFilteringContext(scalarType.Type, unitPopulation.Units[scalarType.Definition.Unit], includedUnitInstanceNames);

        return ProcessingFilter.Create(IncludeUnitsFilterer).Filter(filteringContext, scalarType.UnitInstanceInclusions).Result;
    }

    private static IReadOnlyList<ExcludeUnitsDefinition> ValidateExcludeUnitInstances(ScalarBaseType scalarType, IUnitPopulation unitPopulation, HashSet<string> includedUnitInstanceNames)
    {
        var filteringContext = new ExcludeUnitsFilteringContext(scalarType.Type, unitPopulation.Units[scalarType.Definition.Unit], includedUnitInstanceNames);

        return ProcessingFilter.Create(ExcludeUnitsFilterer).Filter(filteringContext, scalarType.UnitInstanceExclusions).Result;
    }

    private static IReadOnlyList<string> GetUnitInstanceInclusions(IUnitType unit, IEnumerable<IUnitInstanceList> inclusions, Func<IEnumerable<IUnitInstanceList>> exclusionsDelegate)
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

    private static SharpMeasuresScalarValidator SharpMeasuresScalarValidator { get; } = new(EmptySharpMeasuresScalarValidationDiagnostics.Instance);

    private static DerivedQuantityValidator DerivedQuantityValidator { get; } = new(EmptyDerivedQuantityValidationDiagnostics.Instance);
    private static ScalarConstantValidator ScalarConstantValidator { get; } = new(EmptyScalarConstantValidationDiagnostics.Instance);
    private static ConvertibleScalarFilterer ConvertibleScalarFilterer { get; } = new(EmptyConvertibleScalarFilteringDiagnostics.Instance);

    private static IncludeUnitBasesFilterer IncludeBasesFilterer { get; } = new(EmptyIncludeUnitBasesFilteringDiagnostics.Instance);
    private static ExcludeUnitBasesFilterer ExcludeBasesFilterer { get; } = new(EmptyExcludeUnitBasesFilteringDiagnostics.Instance);
    private static IncludeUnitsFilterer IncludeUnitsFilterer { get; } = new(EmptyIncludeUnitsFilteringDiagnostics.Instance);
    private static ExcludeUnitsFilterer ExcludeUnitsFilterer { get; } = new(EmptyExcludeUnitsFilteringDiagnostics.Instance);
}
