namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using System.Collections.Immutable;
using System.Threading;

public static class VectorProcesser
{
    private static GroupBaseProcesser GroupBaseProcesser { get; } = new(VectorProcessingDiagnosticsStrategies.Default);
    private static GroupSpecializationProcesser GroupSpecializationProcesser { get; } = new(VectorProcessingDiagnosticsStrategies.Default);
    private static GroupMemberProcesser GroupMemberProcesser { get; } = new(VectorProcessingDiagnosticsStrategies.Default);

    private static VectorBaseProcesser VectorBaseProcesser { get; } = new(VectorProcessingDiagnosticsStrategies.Default);
    private static VectorSpecializationProcesser VectorSpecializationProcesser { get; } = new(VectorProcessingDiagnosticsStrategies.Default);

    public static (VectorProcessingResult ProcessingResult, IncrementalValueProvider<IVectorPopulation> Population) Process(IncrementalGeneratorInitializationContext context, VectorParsingResult parsingResult)
    {
        var groupBases = parsingResult.GroupBaseProvider.Select(GroupBaseProcesser.Process).ReportDiagnostics(context);
        var groupSpecializations = parsingResult.GroupSpecializationProvider.Select(GroupSpecializationProcesser.Process).ReportDiagnostics(context);
        var groupMembers = parsingResult.GroupMemberProvider.Select(GroupMemberProcesser.Process).ReportDiagnostics(context);

        var vectorBases = parsingResult.VectorBaseProvider.Select(VectorBaseProcesser.Process).ReportDiagnostics(context);
        var vectorSpecializations = parsingResult.VectorSpecializationProvider.Select(VectorSpecializationProcesser.Process).ReportDiagnostics(context);

        var groupBaseInterfaces = groupBases.Select(ExtractInterface).CollectResults();
        var groupSpecializationInterfaces = groupSpecializations.Select(ExtractInterface).CollectResults();
        var groupMemberInterfaces = groupMembers.Select(ExtractInterface).CollectResults();

        var vectorBaseInterfaces = vectorBases.Select(ExtractInterface).CollectResults();
        var vectorSpecializationInterfaces = vectorSpecializations.Select(ExtractInterface).CollectResults();

        var populationAndProcessingData = groupBaseInterfaces.Combine(groupSpecializationInterfaces, groupMemberInterfaces, vectorBaseInterfaces, vectorSpecializationInterfaces).Select(CreatePopulation);

        var population = populationAndProcessingData.Select(ExtractPopulation);
        var processingData = populationAndProcessingData.Select(ExtractProcessingData);

        return (new VectorProcessingResult(groupBases, groupSpecializations, groupMembers, vectorBases, vectorSpecializations, processingData), population);
    }

    private static Optional<IVectorGroupBaseType> ExtractInterface(Optional<GroupBaseType> groupType, CancellationToken _) => groupType.HasValue ? groupType.Value : new Optional<IVectorGroupBaseType>();
    private static Optional<IVectorGroupSpecializationType> ExtractInterface(Optional<GroupSpecializationType> groupType, CancellationToken _) => groupType.HasValue ? groupType.Value : new Optional<IVectorGroupSpecializationType>();
    private static Optional<IVectorGroupMemberType> ExtractInterface(Optional<GroupMemberType> groupMemberType, CancellationToken _) => groupMemberType.HasValue ? groupMemberType.Value : new Optional<IVectorGroupMemberType>();

    private static Optional<IVectorBaseType> ExtractInterface(Optional<VectorBaseType> vectorType, CancellationToken _) => vectorType.HasValue ? vectorType.Value : new Optional<IVectorBaseType>();
    private static Optional<IVectorSpecializationType> ExtractInterface(Optional<VectorSpecializationType> vectorType, CancellationToken _) => vectorType.HasValue ? vectorType.Value : new Optional<IVectorSpecializationType>();

    private static IVectorPopulation ExtractPopulation<T>((IVectorPopulation Population, T) input, CancellationToken _) => input.Population;
    private static VectorProcessingData ExtractProcessingData<T>((T, VectorProcessingData ProcessingData) input, CancellationToken _) => input.ProcessingData;

    private static (IVectorPopulation Population, VectorProcessingData ProcessingData) CreatePopulation((ImmutableArray<IVectorGroupBaseType> GroupBases, ImmutableArray<IVectorGroupSpecializationType> GroupSpecializations,
        ImmutableArray<IVectorGroupMemberType> GroupMembers, ImmutableArray<IVectorBaseType> VectorBases, ImmutableArray<IVectorSpecializationType> VectorSpecializations) vectors, CancellationToken _)
    {
        return VectorPopulation.Build(vectors.VectorBases, vectors.VectorSpecializations, vectors.GroupBases, vectors.GroupSpecializations, vectors.GroupMembers);
    }
}
