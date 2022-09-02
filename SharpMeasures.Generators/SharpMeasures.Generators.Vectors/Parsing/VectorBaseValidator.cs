namespace SharpMeasures.Generators.Vectors.Parsing;

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
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class VectorBaseValidator
{
    public static IncrementalValuesProvider<VectorBaseType> Validate(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<VectorBaseType> vectorProvider,
       IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider,
       IncrementalValueProvider<IVectorPopulationWithData> vectorPopulationProvider)
    {
        return vectorProvider.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(Validate).ReportDiagnostics(context);
    }

    private static IOptionalWithDiagnostics<VectorBaseType> Validate((VectorBaseType UnvalidatedVector, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulationWithData VectorPopulation) input, CancellationToken _)
    {
        var vector = ValidateVector(input.UnvalidatedVector, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);

        if (vector.LacksResult)
        {
            return vector.AsEmptyOptional<VectorBaseType>();
        }

        var derivations = ValidateDerivations(input.UnvalidatedVector, input.ScalarPopulation, input.VectorPopulation);
        var constants = ValidateConstants(input.UnvalidatedVector, input.UnitPopulation);
        var conversions = ValidateConversions(input.UnvalidatedVector, input.VectorPopulation);

        var availableUnitInstanceNames = new HashSet<string>(input.UnitPopulation.Units[input.UnvalidatedVector.Definition.Unit].UnitInstancesByName.Keys);

        var unitInstanceInclusions = ValidateIncludeUnits(input.UnvalidatedVector, input.UnitPopulation, availableUnitInstanceNames);
        var unitInstanceExclusions = ValidateExcludeUnits(input.UnvalidatedVector, input.UnitPopulation, availableUnitInstanceNames);

        VectorBaseType product = new(input.UnvalidatedVector.Type, input.UnvalidatedVector.TypeLocation, vector.Result, derivations.Result, constants.Result, conversions.Result, unitInstanceInclusions.Result, unitInstanceExclusions.Result);

        var allDiagnostics = vector.Concat(derivations).Concat(constants).Concat(conversions).Concat(unitInstanceInclusions).Concat(unitInstanceExclusions);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<SharpMeasuresVectorDefinition> ValidateVector(VectorBaseType vectorType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulationWithData vectorPopulation)
    {
        var validationContext = new SharpMeasuresVectorValidationContext(vectorType.Type, unitPopulation, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(SharpMeasuresVectorValidator).Filter(validationContext, vectorType.Definition);
    }

    private static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ValidateDerivations(VectorBaseType vectorType, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new DerivedQuantityValidationContext(vectorType.Type, scalarPopulation, vectorPopulation);

        return ValidityFilter.Create(DerivedQuantityValidator).Filter(validationContext, vectorType.Derivations);
    }

    private static IResultWithDiagnostics<IReadOnlyList<VectorConstantDefinition>> ValidateConstants(VectorBaseType vectorType, IUnitPopulation unitPopulation)
    {
        var unit = unitPopulation.Units[vectorType.Definition.Unit];

        var includedUnits = GetUnitInclusions(unit, vectorType.UnitInstanceInclusions, () => vectorType.UnitInstanceExclusions);

        HashSet<string> incluedUnitPlurals = new(includedUnits.Select((unitInstance) => unit.UnitInstancesByName[unitInstance].PluralForm));

        var validationContext = new VectorConstantValidationContext(vectorType.Type, vectorType.Definition.Dimension, unit, new HashSet<string>(), new HashSet<string>(), incluedUnitPlurals);

        return ProcessingFilter.Create(VectorConstantValidator).Filter(validationContext, vectorType.Constants);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ValidateConversions(VectorBaseType vectorType, IVectorPopulation vectorPopulation)
    {
        var filteringContext = new ConvertibleVectorFilteringContext(vectorType.Type, vectorType.Definition.Dimension, VectorType.Vector, vectorPopulation, new HashSet<NamedType>());

        return ProcessingFilter.Create(ConvertibleVectorFilterer).Filter(filteringContext, vectorType.Conversions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<IncludeUnitsDefinition>> ValidateIncludeUnits(VectorBaseType vectorType, IUnitPopulation unitPopulation, HashSet<string> availableUnits)
    {
        var filteringContext = new IncludeUnitsFilteringContext(vectorType.Type, unitPopulation.Units[vectorType.Definition.Unit], availableUnits);

        return ProcessingFilter.Create(IncludeUnitsFilterer).Filter(filteringContext, vectorType.UnitInstanceInclusions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ValidateExcludeUnits(VectorBaseType vectorType, IUnitPopulation unitPopulation, HashSet<string> availableUnits)
    {
        var filteringContext = new ExcludeUnitsFilteringContext(vectorType.Type, unitPopulation.Units[vectorType.Definition.Unit], availableUnits);

        return ProcessingFilter.Create(ExcludeUnitsFilterer).Filter(filteringContext, vectorType.UnitInstanceExclusions);
    }

    private static IReadOnlyList<string> GetUnitInclusions(IUnitType unit, IEnumerable<IUnitInstanceList> inclusions, Func<IEnumerable<IUnitInstanceList>> exclusionsDelegate)
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

    private static SharpMeasuresVectorValidator SharpMeasuresVectorValidator { get; } = new(SharpMeasuresVectorValidationDiagnostics.Instance);

    private static DerivedQuantityValidator DerivedQuantityValidator { get; } = new(DerivedQuantityValidationDiagnostics.Instance);
    private static VectorConstantValidator VectorConstantValidator { get; } = new(VectorConstantValidationDiagnostics.Instance);
    private static ConvertibleVectorFilterer ConvertibleVectorFilterer { get; } = new(ConvertibleVectorFilteringDiagnostics.Instance);

    private static IncludeUnitsFilterer IncludeUnitsFilterer { get; } = new(IncludeUnitsFilteringDiagnostics.Instance);
    private static ExcludeUnitsFilterer ExcludeUnitsFilterer { get; } = new(ExcludeUnitsFilteringDiagnostics.Instance);
}
