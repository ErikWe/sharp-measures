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
    public static IncrementalValuesProvider<Optional<GroupBaseType>> Validate(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<GroupBaseType>> vectorProvider,
       IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider,
       IncrementalValueProvider<IVectorPopulationWithData> vectorPopulationProvider)
    {
        return vectorProvider.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(Validate).ReportDiagnostics(context);
    }

    private static IOptionalWithDiagnostics<GroupBaseType> Validate((Optional<GroupBaseType> UnvalidatedVector, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulationWithData VectorPopulation) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.UnvalidatedVector.HasValue is false)
        {
            return OptionalWithDiagnostics.Empty<GroupBaseType>();
        }

        return Validate(input.UnvalidatedVector.Value, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    private static IOptionalWithDiagnostics<GroupBaseType> Validate(GroupBaseType vectorType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulationWithData vectorPopulation)
    {
        var vector = ValidateVector(vectorType, unitPopulation, scalarPopulation, vectorPopulation);

        if (vector.LacksResult)
        {
            return vector.AsEmptyOptional<GroupBaseType>();
        }

        var derivations = ValidateDerivations(vectorType, scalarPopulation, vectorPopulation);
        var conversions = ValidateConversions(vectorType, vectorPopulation);

        var includedUnitInstanceNames = new HashSet<string>(unitPopulation.Units[vectorType.Definition.Unit].UnitInstancesByName.Keys);

        var unitInstanceInclusions = ValidateIncludeUnitInstances(vectorType, unitPopulation, includedUnitInstanceNames);
        var unitInstanceExclusions = ValidateExcludeUnitInstances(vectorType, unitPopulation, includedUnitInstanceNames);

        GroupBaseType product = new(vectorType.Type, vectorType.TypeLocation, vector.Result, derivations.Result, conversions.Result, unitInstanceInclusions.Result, unitInstanceExclusions.Result);

        var allDiagnostics = vector.Concat(derivations).Concat(conversions).Concat(unitInstanceInclusions).Concat(unitInstanceExclusions);

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

    private static IResultWithDiagnostics<IReadOnlyList<IncludeUnitsDefinition>> ValidateIncludeUnitInstances(GroupBaseType vectorType, IUnitPopulation unitPopulation, HashSet<string> includedUnitInstanceNames)
    {
        var filteringContext = new IncludeUnitsFilteringContext(vectorType.Type, unitPopulation.Units[vectorType.Definition.Unit], includedUnitInstanceNames);

        return ProcessingFilter.Create(IncludeUnitsFilterer).Filter(filteringContext, vectorType.UnitInclusions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ValidateExcludeUnitInstances(GroupBaseType vectorType, IUnitPopulation unitPopulation, HashSet<string> includedUnitInstanceNames)
    {
        var filteringContext = new ExcludeUnitsFilteringContext(vectorType.Type, unitPopulation.Units[vectorType.Definition.Unit], includedUnitInstanceNames);

        return ProcessingFilter.Create(ExcludeUnitsFilterer).Filter(filteringContext, vectorType.UnitExclusions);
    }

    private static SharpMeasuresVectorGroupValidator SharpMeasuresVectorGroupValidator { get; } = new(SharpMeasuresVectorGroupValidationDiagnostics.Instance);

    private static DerivedQuantityValidator DerivedQuantityValidator { get; } = new(DerivedQuantityValidationDiagnostics.Instance);
    private static ConvertibleVectorFilterer ConvertibleVectorFilterer { get; } = new(ConvertibleVectorFilteringDiagnostics.Instance);

    private static IncludeUnitsFilterer IncludeUnitsFilterer { get; } = new(IncludeUnitsFilteringDiagnostics.Instance);
    private static ExcludeUnitsFilterer ExcludeUnitsFilterer { get; } = new(ExcludeUnitsFilteringDiagnostics.Instance);
}
