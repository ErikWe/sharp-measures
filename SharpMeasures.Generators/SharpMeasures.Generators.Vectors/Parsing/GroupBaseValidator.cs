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
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class GroupBaseValidator
{
    public static IncrementalValuesProvider<Optional<GroupBaseType>> Validate(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<GroupBaseType>> vectorProvider, IncrementalValueProvider<VectorProcessingData> processingDataProvider,
        IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider)
    {
        return vectorProvider.Combine(processingDataProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(Validate).ReportDiagnostics(context);
    }

    private static IOptionalWithDiagnostics<GroupBaseType> Validate((Optional<GroupBaseType> UnvalidatedGroup, VectorProcessingData ProcessingData, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.UnvalidatedGroup.HasValue is false)
        {
            return OptionalWithDiagnostics.Empty<GroupBaseType>();
        }

        return Validate(input.UnvalidatedGroup.Value, input.ProcessingData, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    private static IOptionalWithDiagnostics<GroupBaseType> Validate(GroupBaseType groupType, VectorProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var group = ValidateGroup(groupType, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        if (group.LacksResult)
        {
            return group.AsEmptyOptional<GroupBaseType>();
        }

        var derivations = ValidateDerivations(groupType, scalarPopulation, vectorPopulation);
        var conversions = ValidateConversions(groupType, vectorPopulation);

        var includedUnitInstanceNames = new HashSet<string>(unitPopulation.Units[groupType.Definition.Unit].UnitInstancesByName.Keys);

        var unitInstanceInclusions = ValidateIncludeUnitInstances(groupType, unitPopulation, includedUnitInstanceNames);
        var unitInstanceExclusions = ValidateExcludeUnitInstances(groupType, unitPopulation, includedUnitInstanceNames);

        GroupBaseType product = new(groupType.Type, groupType.TypeLocation, group.Result, derivations.Result, conversions.Result, unitInstanceInclusions.Result, unitInstanceExclusions.Result);
        var allDiagnostics = group.Concat(derivations).Concat(conversions).Concat(unitInstanceInclusions).Concat(unitInstanceExclusions);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<SharpMeasuresVectorGroupDefinition> ValidateGroup(GroupBaseType groupType, VectorProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new SharpMeasuresVectorGroupValidationContext(groupType.Type, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(SharpMeasuresVectorGroupValidator).Filter(validationContext, groupType.Definition);
    }

    private static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ValidateDerivations(GroupBaseType groupType, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new DerivedQuantityValidationContext(groupType.Type, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(DerivedQuantityValidator).Filter(validationContext, groupType.Derivations);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ValidateConversions(GroupBaseType groupType, IVectorPopulation vectorPopulation)
    {
        var filteringContext = new ConvertibleVectorFilteringContext(groupType.Type, VectorType.Group, vectorPopulation);

        return ProcessingFilter.Create(ConvertibleVectorFilterer).Filter(filteringContext, groupType.Conversions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<IncludeUnitsDefinition>> ValidateIncludeUnitInstances(GroupBaseType groupType, IUnitPopulation unitPopulation, HashSet<string> includedUnitInstanceNames)
    {
        var filteringContext = new IncludeUnitsFilteringContext(groupType.Type, unitPopulation.Units[groupType.Definition.Unit], includedUnitInstanceNames);

        return ProcessingFilter.Create(IncludeUnitsFilterer).Filter(filteringContext, groupType.UnitInstanceInclusions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ValidateExcludeUnitInstances(GroupBaseType groupType, IUnitPopulation unitPopulation, HashSet<string> includedUnitInstanceNames)
    {
        var filteringContext = new ExcludeUnitsFilteringContext(groupType.Type, unitPopulation.Units[groupType.Definition.Unit], includedUnitInstanceNames);

        return ProcessingFilter.Create(ExcludeUnitsFilterer).Filter(filteringContext, groupType.UnitInstanceExclusions);
    }

    private static SharpMeasuresVectorGroupValidator SharpMeasuresVectorGroupValidator { get; } = new(SharpMeasuresVectorGroupValidationDiagnostics.Instance);

    private static DerivedQuantityValidator DerivedQuantityValidator { get; } = new(DerivedQuantityValidationDiagnostics.Instance);
    private static ConvertibleVectorFilterer ConvertibleVectorFilterer { get; } = new(ConvertibleVectorFilteringDiagnostics.Instance);

    private static IncludeUnitsFilterer IncludeUnitsFilterer { get; } = new(IncludeUnitsFilteringDiagnostics.Instance);
    private static ExcludeUnitsFilterer ExcludeUnitsFilterer { get; } = new(ExcludeUnitsFilteringDiagnostics.Instance);
}
