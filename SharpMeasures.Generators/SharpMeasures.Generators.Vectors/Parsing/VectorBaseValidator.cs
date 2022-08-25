namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
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

        var unitInclusions = ValidateUnitList(input.UnvalidatedVector, input.UnitPopulation, input.UnvalidatedVector.UnitInclusions, UnitInclusionFilter);
        var unitExclusions = ValidateUnitList(input.UnvalidatedVector, input.UnitPopulation, input.UnvalidatedVector.UnitExclusions, UnitExclusionFilter);

        VectorBaseType product = new(input.UnvalidatedVector.Type, input.UnvalidatedVector.TypeLocation, vector.Result, input.UnvalidatedVector.Derivations,
            input.UnvalidatedVector.Constants, input.UnvalidatedVector.Conversions, unitInclusions.Result, unitExclusions.Result);

        var allDiagnostics = vector.Concat(derivations).Concat(constants).Concat(conversions).Concat(unitInclusions).Concat(unitExclusions);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<SharpMeasuresVectorDefinition> ValidateVector(VectorBaseType vectorType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulationWithData vectorPopulation)
    {
        var validationContext = new SharpMeasuresVectorValidationContext(vectorType.Type, unitPopulation, scalarPopulation, vectorPopulation);

        return SharpMeasuresVectorValidator.Process(validationContext, vectorType.Definition);
    }

    private static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ValidateDerivations(VectorBaseType vectorType, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new DerivedQuantityValidationContext(vectorType.Type, scalarPopulation, vectorPopulation);

        return ValidityFilter.Create(DerivedQuantityValidator).Filter(validationContext, vectorType.Derivations);
    }

    private static IResultWithDiagnostics<IReadOnlyList<VectorConstantDefinition>> ValidateConstants(VectorBaseType vectorType, IUnitPopulation unitPopulation)
    {
        var unit = unitPopulation.Units[vectorType.Definition.Unit];

        var includedUnits = GetUnitInclusions(unit, vectorType.UnitInclusions, () => vectorType.UnitExclusions);

        HashSet<string> incluedUnitPlurals = new(includedUnits.Select((unitInstance) => unit.UnitsByName[unitInstance].Plural));

        var validationContext = new VectorConstantValidationContext(vectorType.Type, vectorType.Definition.Dimension, unit, new HashSet<string>(), new HashSet<string>(), incluedUnitPlurals);

        return ValidityFilter.Create(VectorConstantValidator).Filter(validationContext, vectorType.Constants);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ValidateConversions(VectorBaseType vectorType, IVectorPopulation vectorPopulation)
    {
        var filteringContext = new ConvertibleVectorFilteringContext(vectorType.Type, VectorType.Vector, vectorPopulation, new HashSet<NamedType>());

        return ProcessingFilter.Create(ConvertibleVectorFilterer).Filter(filteringContext, vectorType.Conversions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<UnitListDefinition>> ValidateUnitList(VectorBaseType vectorType, IUnitPopulation unitPopulation, IEnumerable<UnitListDefinition> unitList, IProcesser<IUnitListFilteringContext, UnitListDefinition, UnitListDefinition> filter)
    {
        var filteringContext = new UnitListFilteringContext(vectorType.Type, unitPopulation.Units[vectorType.Definition.Unit], new HashSet<string>());

        return ProcessingFilter.Create(filter).Filter(filteringContext, unitList);
    }

    private static IReadOnlyList<string> GetUnitInclusions(IUnitType unit, IEnumerable<IUnitList> inclusions, Func<IEnumerable<IUnitList>> exclusionsDelegate)
    {
        if (inclusions.Any())
        {
            return inclusions.SelectMany(static (unitList) => unitList.Units).ToList();
        }

        HashSet<string> includedUnits = new(unit.UnitsByName.Keys);

        includedUnits.ExceptWith(exclusionsDelegate().SelectMany(static (unitList) => unitList.Units));

        return includedUnits.ToList();
    }

    private static SharpMeasuresVectorValidator SharpMeasuresVectorValidator { get; } = new(SharpMeasuresVectorValidationDiagnostics.Instance);

    private static DerivedQuantityValidator DerivedQuantityValidator { get; } = new(DerivedQuantityValidationDiagnostics.Instance);
    private static VectorConstantValidator VectorConstantValidator { get; } = new(VectorConstantValidationDiagnostics.Instance);
    private static ConvertibleVectorFilterer ConvertibleVectorFilterer { get; } = new(ConvertibleVectorFilteringDiagnostics.Instance);

    private static UnitListFilterer UnitInclusionFilter { get; } = new(UnitInclusionFilteringDiagnostics.Instance);
    private static UnitListFilterer UnitExclusionFilter { get; } = new(UnitExclusionFilteringDiagnostics.Instance);
}
