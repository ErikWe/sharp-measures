namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public interface IVectorProcesser
{
    public abstract (IncrementalValueProvider<IVectorPopulation> Population, IVectorValidator Validator) Process(IncrementalGeneratorInitializationContext context);
}

public class VectorProcesser : IVectorProcesser
{
    private IncrementalValuesProvider<Optional<RawGroupBaseType>> GroupBaseProvider { get; }
    private IncrementalValuesProvider<Optional<RawGroupSpecializationType>> GroupSpecializationProvider { get; }
    private IncrementalValuesProvider<Optional<RawGroupMemberType>> GroupMemberProvider { get; }

    private IncrementalValuesProvider<Optional<RawVectorBaseType>> VectorBaseProvider { get; }
    private IncrementalValuesProvider<Optional<RawVectorSpecializationType>> VectorSpecializationProvider { get; }

    internal VectorProcesser(IncrementalValuesProvider<Optional<RawGroupBaseType>> groupBaseProvider, IncrementalValuesProvider<Optional<RawGroupSpecializationType>> groupSpecializationProvider,
        IncrementalValuesProvider<Optional<RawGroupMemberType>> groupMemberProvider, IncrementalValuesProvider<Optional<RawVectorBaseType>> vectorBaseProvider, IncrementalValuesProvider<Optional<RawVectorSpecializationType>> vectorSpecializationProvider)
    {
        GroupBaseProvider = groupBaseProvider;
        GroupSpecializationProvider = groupSpecializationProvider;
        GroupMemberProvider = groupMemberProvider;

        VectorBaseProvider = vectorBaseProvider;
        VectorSpecializationProvider = vectorSpecializationProvider;
    }

    public (IncrementalValueProvider<IVectorPopulation>, IVectorValidator) Process(IncrementalGeneratorInitializationContext context)
    {
        GroupBaseProcesser groupBaseProcesser = new();
        GroupSpecializationProcesser groupSpecializationProcesser = new();

        VectorBaseProcesser vectorBaseProcesser = new();
        VectorSpecializationProcesser vectorSpecializationProcesser = new();

        var groupBases = GroupBaseProvider.Select(groupBaseProcesser.Process).ReportDiagnostics(context);
        var groupSpecializations = GroupSpecializationProvider.Select(groupSpecializationProcesser.Process).ReportDiagnostics(context);
        var groupMembers = GroupMemberProvider.Select(GroupMemberProcesser.Process).ReportDiagnostics(context);

        var vectorBases = VectorBaseProvider.Select(vectorBaseProcesser.Process).ReportDiagnostics(context);
        var vectorSpecializations = VectorSpecializationProvider.Select(vectorSpecializationProcesser.Process).ReportDiagnostics(context);

        var groupBaseInterfaces = groupBases.Select(ExtractInterface).CollectResults();
        var groupSpecializationInterfaces = groupSpecializations.Select(ExtractInterface).CollectResults();
        var groupMemberInterfaces = groupMembers.Select(ExtractInterface).CollectResults();

        var vectorBaseInterfaces = vectorBases.Select(ExtractInterface).CollectResults();
        var vectorSpecializationInterfaces = vectorSpecializations.Select(ExtractInterface).CollectResults();

        var populationWithData = groupBaseInterfaces.Combine(groupSpecializationInterfaces, groupMemberInterfaces, vectorBaseInterfaces, vectorSpecializationInterfaces).Select(CreatePopulation);

        return (populationWithData.Select(ReducePopulation), new VectorValidator(populationWithData, groupBases, groupSpecializations, groupMembers, vectorBases, vectorSpecializations));
    }

    private static Optional<IVectorGroupBaseType> ExtractInterface(Optional<GroupBaseType> groupType, CancellationToken _) => groupType.HasValue ? groupType.Value : new Optional<IVectorGroupBaseType>();
    private static Optional<IVectorGroupSpecializationType> ExtractInterface(Optional<GroupSpecializationType> groupType, CancellationToken _) => groupType.HasValue ? groupType.Value : new Optional<IVectorGroupSpecializationType>();
    private static Optional<IVectorGroupMemberType> ExtractInterface(Optional<GroupMemberType> groupMemberType, CancellationToken _) => groupMemberType.HasValue ? groupMemberType.Value : new Optional<IVectorGroupMemberType>();

    private static Optional<IVectorBaseType> ExtractInterface(Optional<VectorBaseType> vectorType, CancellationToken _) => vectorType.HasValue ? vectorType.Value : new Optional<IVectorBaseType>();
    private static Optional<IVectorSpecializationType> ExtractInterface(Optional<VectorSpecializationType> vectorType, CancellationToken _) => vectorType.HasValue ? vectorType.Value : new Optional<IVectorSpecializationType>();

    private static IVectorPopulation ReducePopulation(IVectorPopulationWithData vectorPopulation, CancellationToken _) => vectorPopulation;

    private static IVectorPopulationWithData CreatePopulation((ImmutableArray<IVectorGroupBaseType> GroupBases, ImmutableArray<IVectorGroupSpecializationType> GroupSpecializations,
        ImmutableArray<IVectorGroupMemberType> GroupMembers, ImmutableArray<IVectorBaseType> VectorBases, ImmutableArray<IVectorSpecializationType> VectorSpecializations) vectors, CancellationToken _)
    {
        return VectorPopulation.Build(vectors.VectorBases, vectors.VectorSpecializations, vectors.GroupBases, vectors.GroupSpecializations, vectors.GroupMembers);
    }
}
