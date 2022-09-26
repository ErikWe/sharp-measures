namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public static class VectorValidator
{
    private static GroupBaseValidator GroupBaseValidator { get; } = new(VectorValidationDiagnosticsStrategies.Default);
    private static GroupSpecializationValidator GroupSpecializationValidator { get; } = new(VectorValidationDiagnosticsStrategies.Default);
    private static GroupMemberValidator GroupMemberValidator { get; } = new(VectorValidationDiagnosticsStrategies.Default);

    private static VectorBaseValidator VectorBaseValidator { get; } = new(VectorValidationDiagnosticsStrategies.Default);
    private static VectorSpecializationValidator VectorSpecializationValidator { get; } = new(VectorValidationDiagnosticsStrategies.Default);

    public static (VectorValidationResult ValidationResult, IncrementalValueProvider<IVectorPopulation> Population) Validate(IncrementalGeneratorInitializationContext context, VectorProcessingResult processingResult, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider)
    {
        var validatedGroupBases = processingResult.GroupBaseProvider.Combine(processingResult.ProcessingDataProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(GroupBaseValidator.Validate).ReportDiagnostics(context);
        var validatedGroupSpecializations = processingResult.GroupSpecializationProvider.Combine(processingResult.ProcessingDataProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(GroupSpecializationValidator.Validate).ReportDiagnostics(context);
        var validatedGroupMembers = processingResult.GroupMemberProvider.Combine(processingResult.ProcessingDataProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(GroupMemberValidator.Validate).ReportDiagnostics(context);

        var validatedVectorBases = processingResult.VectorBaseProvider.Combine(processingResult.ProcessingDataProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(VectorBaseValidator.Validate).ReportDiagnostics(context);
        var validatedVectorSpecializations = processingResult.VectorSpecializationProvider.Combine(processingResult.ProcessingDataProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(VectorSpecializationValidator.Validate).ReportDiagnostics(context);

        var groupBaseInterfaces = validatedGroupBases.Select(ExtractInterface).CollectResults();
        var groupSpecializationInterfaces = validatedGroupSpecializations.Select(ExtractInterface).CollectResults();
        var groupMemberInterfaces = validatedGroupMembers.Select(ExtractInterface).CollectResults();

        var vectorBaseInterfaces = validatedVectorBases.Select(ExtractInterface).CollectResults();
        var vectorSpecializationInterfaces = validatedVectorSpecializations.Select(ExtractInterface).CollectResults();

        var result = new VectorValidationResult(validatedGroupBases, validatedGroupSpecializations, validatedGroupMembers, validatedVectorBases, validatedVectorSpecializations);
        var population = groupBaseInterfaces.Combine(groupSpecializationInterfaces, groupMemberInterfaces, vectorBaseInterfaces, vectorSpecializationInterfaces).Select(CreatePopulation);

        return (result, population);
    }

    private static Optional<IVectorGroupBaseType> ExtractInterface(Optional<GroupBaseType> groupType, CancellationToken _) => groupType.HasValue ? groupType.Value : new Optional<IVectorGroupBaseType>();
    private static Optional<IVectorGroupSpecializationType> ExtractInterface(Optional<GroupSpecializationType> groupType, CancellationToken _) => groupType.HasValue ? groupType.Value : new Optional<IVectorGroupSpecializationType>();
    private static Optional<IVectorGroupMemberType> ExtractInterface(Optional<GroupMemberType> groupMemberType, CancellationToken _) => groupMemberType.HasValue ? groupMemberType.Value : new Optional<IVectorGroupMemberType>();

    private static Optional<IVectorBaseType> ExtractInterface(Optional<VectorBaseType> vectorType, CancellationToken _) => vectorType.HasValue ? vectorType.Value : new Optional<IVectorBaseType>();
    private static Optional<IVectorSpecializationType> ExtractInterface(Optional<VectorSpecializationType> vectorType, CancellationToken _) => vectorType.HasValue ? vectorType.Value : new Optional<IVectorSpecializationType>();

    private static IVectorPopulation CreatePopulation((ImmutableArray<IVectorGroupBaseType> GroupBases, ImmutableArray<IVectorGroupSpecializationType> GroupSpecializations, ImmutableArray<IVectorGroupMemberType> GroupMembers, ImmutableArray<IVectorBaseType> VectorBases, ImmutableArray<IVectorSpecializationType> VectorSpecializations) vectors, CancellationToken _)
    {
        return VectorPopulation.BuildWithoutProcessingData(vectors.VectorBases, vectors.VectorSpecializations, vectors.GroupBases, vectors.GroupSpecializations, vectors.GroupMembers);
    }
}
