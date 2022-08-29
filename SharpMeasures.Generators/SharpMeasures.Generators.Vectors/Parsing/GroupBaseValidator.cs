namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
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

        var availableUnits = new HashSet<string>(input.UnitPopulation.Units[input.UnvalidatedVector.Definition.Unit].UnitsByName.Keys);

        var unitInclusions = ValidateIncludeUnits(input.UnvalidatedVector, input.UnitPopulation, availableUnits);
        var unitExclusions = ValidateExcludeUnits(input.UnvalidatedVector, input.UnitPopulation, availableUnits);

        GroupBaseType product = new(input.UnvalidatedVector.Type, input.UnvalidatedVector.TypeLocation, vector.Result, derivations.Result, conversions.Result, unitInclusions.Result, unitExclusions.Result);

        var allDiagnostics = vector.Concat(derivations).Concat(conversions).Concat(unitInclusions).Concat(unitExclusions);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<SharpMeasuresVectorGroupDefinition> ValidateVector(GroupBaseType vectorType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulationWithData vectorPopulation)
    {
        var validationContext = new SharpMeasuresVectorGroupValidationContext(vectorType.Type, unitPopulation, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(SharpMeasuresVectorGroupValidator).Filter(validationContext, vectorType.Definition);
    }

    private static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ValidateDerivations(GroupBaseType vectorType, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new DerivedQuantityValidationContext(vectorType.Type, scalarPopulation, vectorPopulation);

        return ValidityFilter.Create(DerivedQuantityValidator).Filter(validationContext, vectorType.Derivations);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ValidateConversions(GroupBaseType vectorType, IVectorPopulation vectorPopulation)
    {
        var filteringContext = new ConvertibleVectorFilteringContext(vectorType.Type, VectorType.Group, vectorPopulation, new HashSet<NamedType>());

        return ProcessingFilter.Create(ConvertibleVectorFilterer).Filter(filteringContext, vectorType.Conversions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<IncludeUnitsDefinition>> ValidateIncludeUnits(GroupBaseType vectorType, IUnitPopulation unitPopulation, HashSet<string> availableUnits)
    {
        var filteringContext = new IncludeUnitsFilteringContext(vectorType.Type, unitPopulation.Units[vectorType.Definition.Unit], availableUnits);

        return ProcessingFilter.Create(IncludeUnitsFilterer).Filter(filteringContext, vectorType.UnitInclusions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ValidateExcludeUnits(GroupBaseType vectorType, IUnitPopulation unitPopulation, HashSet<string> availableUnits)
    {
        var filteringContext = new ExcludeUnitsFilteringContext(vectorType.Type, unitPopulation.Units[vectorType.Definition.Unit], availableUnits);

        return ProcessingFilter.Create(ExcludeUnitsFilterer).Filter(filteringContext, vectorType.UnitExclusions);
    }

    private static SharpMeasuresVectorGroupValidator SharpMeasuresVectorGroupValidator { get; } = new(SharpMeasuresVectorGroupValidationDiagnostics.Instance);

    private static DerivedQuantityValidator DerivedQuantityValidator { get; } = new(DerivedQuantityValidationDiagnostics.Instance);
    private static ConvertibleVectorFilterer ConvertibleVectorFilterer { get; } = new(ConvertibleVectorFilteringDiagnostics.Instance);

    private static IncludeUnitsFilterer IncludeUnitsFilterer { get; } = new(IncludeUnitsFilteringDiagnostics.Instance);
    private static ExcludeUnitsFilterer ExcludeUnitsFilterer { get; } = new(ExcludeUnitsFilteringDiagnostics.Instance);
}
