namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
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
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class GroupBaseValidator
{
    public static IncrementalValuesProvider<GroupBaseType> Validate(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<GroupBaseType> vectorProvider,
       IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider,
       IncrementalValueProvider<IVectorPopulationWithData> vectorPopulationProvider)
    {
        return vectorProvider.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(Validate).ReportDiagnostics(context);
    }

    private static IOptionalWithDiagnostics<GroupBaseType> Validate((GroupBaseType UnvalidatedVector, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulationWithData VectorPopulation) input, CancellationToken _)
    {
        var vector = ValidateVector(input.UnvalidatedVector, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);

        if (vector.LacksResult)
        {
            return vector.AsEmptyOptional<GroupBaseType>();
        }

        var derivations = ValidateDerivations(input.UnvalidatedVector, input.ScalarPopulation, input.VectorPopulation);
        var conversions = ValidateConversions(input.UnvalidatedVector, input.VectorPopulation);

        var unitInclusions = ValidateUnitList(input.UnvalidatedVector, input.UnitPopulation, input.UnvalidatedVector.UnitInclusions, UnitInclusionFilter);
        var unitExclusions = ValidateUnitList(input.UnvalidatedVector, input.UnitPopulation, input.UnvalidatedVector.UnitExclusions, UnitExclusionFilter);

        GroupBaseType product = new(input.UnvalidatedVector.Type, input.UnvalidatedVector.TypeLocation, vector.Result, derivations.Result, conversions.Result, unitInclusions.Result, unitExclusions.Result);

        var allDiagnostics = vector.Concat(derivations).Concat(conversions).Concat(unitInclusions).Concat(unitExclusions);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<SharpMeasuresVectorGroupDefinition> ValidateVector(GroupBaseType vectorType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulationWithData vectorPopulation)
    {
        var validationContext = new SharpMeasuresVectorGroupValidationContext(vectorType.Type, unitPopulation, scalarPopulation, vectorPopulation);

        return SharpMeasuresVectorGroupValidator.Process(validationContext, vectorType.Definition);
    }

    private static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ValidateDerivations(GroupBaseType vectorType, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new DerivedQuantityValidationContext(vectorType.Type, scalarPopulation, vectorPopulation);

        return ValidityFilter.Create(DerivedQuantityValidator).Filter(validationContext, vectorType.Derivations);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ValidateConversions(GroupBaseType vectorType, IVectorPopulation vectorPopulation)
    {
        var filteringContext = new ConvertibleVectorFilteringContext(vectorType.Type, VectorType.Vector, vectorPopulation, new HashSet<NamedType>());

        return ProcessingFilter.Create(ConvertibleVectorFilterer).Filter(filteringContext, vectorType.Conversions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<UnitListDefinition>> ValidateUnitList(GroupBaseType vectorType, IUnitPopulation unitPopulation, IEnumerable<UnitListDefinition> unitList, IProcesser<IUnitListFilteringContext, UnitListDefinition, UnitListDefinition> filter)
    {
        var filteringContext = new UnitListFilteringContext(vectorType.Type, unitPopulation.Units[vectorType.Definition.Unit], new HashSet<string>());

        return ProcessingFilter.Create(filter).Filter(filteringContext, unitList);
    }

    private static SharpMeasuresVectorGroupValidator SharpMeasuresVectorGroupValidator { get; } = new(SharpMeasuresVectorGroupValidationDiagnostics.Instance);

    private static DerivedQuantityValidator DerivedQuantityValidator { get; } = new(DerivedQuantityValidationDiagnostics.Instance);
    private static ConvertibleVectorFilterer ConvertibleVectorFilterer { get; } = new(ConvertibleVectorFilteringDiagnostics.Instance);

    private static UnitListFilterer UnitInclusionFilter { get; } = new(UnitInclusionFilteringDiagnostics.Instance);
    private static UnitListFilterer UnitExclusionFilter { get; } = new(UnitExclusionFilteringDiagnostics.Instance);
}
