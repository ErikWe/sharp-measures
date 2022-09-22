namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics.Validation;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System;
using System.Collections.Generic;
using System.Linq;

internal static class ForeignVectorBaseValidator
{
    public static Optional<VectorBaseType> Validate(VectorBaseType vectorType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var vector = ValidateVector(vectorType, unitPopulation, scalarPopulation, vectorPopulation);

        if (vector.HasValue is false)
        {
            return new Optional<VectorBaseType>();
        }

        var derivations = ValidateDerivations(vectorType, scalarPopulation, vectorPopulation);
        var constants = ValidateConstants(vectorType, unitPopulation);
        var conversions = ValidateConversions(vectorType, vectorPopulation);

        var availableUnitInstanceNames = new HashSet<string>(unitPopulation.Units[vectorType.Definition.Unit].UnitInstancesByName.Keys);

        var unitInstanceInclusions = ValidateIncludeUnits(vectorType, unitPopulation, availableUnitInstanceNames);
        var unitInstanceExclusions = ValidateExcludeUnits(vectorType, unitPopulation, availableUnitInstanceNames);

        return new VectorBaseType(vectorType.Type, vectorType.TypeLocation, vector.Value, derivations, constants, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    private static Optional<SharpMeasuresVectorDefinition> ValidateVector(VectorBaseType vectorType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        VectorProcessingData processingData = new(new Dictionary<NamedType, IVectorBaseType>(), new Dictionary<NamedType, IVectorSpecializationType>(), new Dictionary<NamedType, IVectorSpecializationType>(), new Dictionary<NamedType, IVectorGroupBaseType>(),
            new Dictionary<NamedType, IVectorGroupSpecializationType>(), new Dictionary<NamedType, IVectorGroupSpecializationType>(), new Dictionary<NamedType, IVectorGroupMemberType>());

        var validationContext = new SharpMeasuresVectorValidationContext(vectorType.Type, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        var vector = ProcessingFilter.Create(SharpMeasuresVectorValidator).Filter(validationContext, vectorType.Definition);

        if (vector.LacksResult)
        {
            return new Optional<SharpMeasuresVectorDefinition>();
        }

        return vector.Result;
    }

    private static IReadOnlyList<DerivedQuantityDefinition> ValidateDerivations(VectorBaseType vectorType, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new DerivedQuantityValidationContext(vectorType.Type, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(DerivedQuantityValidator).Filter(validationContext, vectorType.Derivations).Result;
    }

    private static IReadOnlyList<VectorConstantDefinition> ValidateConstants(VectorBaseType vectorType, IUnitPopulation unitPopulation)
    {
        var unit = unitPopulation.Units[vectorType.Definition.Unit];

        var includedUnitInstances = GetUnitInstanceInclusions(unit, vectorType.UnitInstanceInclusions, () => vectorType.UnitInstanceExclusions);

        HashSet<string> incluedUnitPlurals = new(includedUnitInstances.Select((unitInstance) => unit.UnitInstancesByName[unitInstance].PluralForm));

        var validationContext = new VectorConstantValidationContext(vectorType.Type, vectorType.Definition.Dimension, unit, new HashSet<string>(), new HashSet<string>(), incluedUnitPlurals);

        return ProcessingFilter.Create(VectorConstantValidator).Filter(validationContext, vectorType.Constants).Result;
    }

    private static IReadOnlyList<ConvertibleVectorDefinition> ValidateConversions(VectorBaseType vectorType, IVectorPopulation vectorPopulation)
    {
        var filteringContext = new ConvertibleVectorFilteringContext(vectorType.Type, vectorType.Definition.Dimension, VectorType.Vector, vectorPopulation);

        return ProcessingFilter.Create(ConvertibleVectorFilterer).Filter(filteringContext, vectorType.Conversions).Result;
    }

    private static IReadOnlyList<IncludeUnitsDefinition> ValidateIncludeUnits(VectorBaseType vectorType, IUnitPopulation unitPopulation, HashSet<string> availableUnits)
    {
        var filteringContext = new IncludeUnitsFilteringContext(vectorType.Type, unitPopulation.Units[vectorType.Definition.Unit], availableUnits);

        return ProcessingFilter.Create(IncludeUnitsFilterer).Filter(filteringContext, vectorType.UnitInstanceInclusions).Result;
    }

    private static IReadOnlyList<ExcludeUnitsDefinition> ValidateExcludeUnits(VectorBaseType vectorType, IUnitPopulation unitPopulation, HashSet<string> availableUnits)
    {
        var filteringContext = new ExcludeUnitsFilteringContext(vectorType.Type, unitPopulation.Units[vectorType.Definition.Unit], availableUnits);

        return ProcessingFilter.Create(ExcludeUnitsFilterer).Filter(filteringContext, vectorType.UnitInstanceExclusions).Result;
    }

    private static IReadOnlyList<string> GetUnitInstanceInclusions(IUnitType unit, IEnumerable<IUnitInstanceList> inclusions, Func<IEnumerable<IUnitInstanceList>> exclusionsDelegate)
    {
        HashSet<string> includedUnits = new(unit.UnitInstancesByName.Keys);

        if (inclusions.Any())
        {
            includedUnits.IntersectWith(inclusions.SelectMany(static (unitList) => unitList.UnitInstances).ToList());

            return includedUnits.ToList();
        }

        includedUnits.ExceptWith(exclusionsDelegate().SelectMany(static (unitList) => unitList.UnitInstances));

        return includedUnits.ToList();
    }

    private static SharpMeasuresVectorValidator SharpMeasuresVectorValidator { get; } = new(EmptySharpMeasuresVectorValidationDiagnostics.Instance);

    private static DerivedQuantityValidator DerivedQuantityValidator { get; } = new(EmptyDerivedQuantityValidationDiagnostics.Instance);
    private static VectorConstantValidator VectorConstantValidator { get; } = new(EmptyVectorConstantValidationDiagnostics.Instance);
    private static ConvertibleVectorFilterer ConvertibleVectorFilterer { get; } = new(EmptyConvertibleVectorFilteringDiagnostics.Instance);

    private static IncludeUnitsFilterer IncludeUnitsFilterer { get; } = new(EmptyIncludeUnitsFilteringDiagnostics.Instance);
    private static ExcludeUnitsFilterer ExcludeUnitsFilterer { get; } = new(EmptyExcludeUnitsFilteringDiagnostics.Instance);
}
