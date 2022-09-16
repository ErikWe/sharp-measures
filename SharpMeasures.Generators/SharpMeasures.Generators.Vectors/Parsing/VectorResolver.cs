namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public interface IVectorResolver
{
    public abstract (IncrementalValueProvider<IResolvedVectorPopulation>, IVectorGenerator) Resolve(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider);
}

internal class VectorResolver : IVectorResolver
{
    private IncrementalValuesProvider<Optional<GroupBaseType>> GroupBaseProvider { get; }
    private IncrementalValuesProvider<Optional<GroupSpecializationType>> GroupSpecializationProvider { get; }
    private IncrementalValuesProvider<Optional<GroupMemberType>> GroupMemberProvider { get; }

    private IncrementalValuesProvider<Optional<VectorBaseType>> VectorBaseProvider { get; }
    private IncrementalValuesProvider<Optional<VectorSpecializationType>> VectorSpecializationProvider { get; }

    internal VectorResolver(IncrementalValuesProvider<Optional<GroupBaseType>> groupBaseProvider, IncrementalValuesProvider<Optional<GroupSpecializationType>> groupSpecializationProvider,
        IncrementalValuesProvider<Optional<GroupMemberType>> groupMemberProvider, IncrementalValuesProvider<Optional<VectorBaseType>> vectorBaseProvider, IncrementalValuesProvider<Optional<VectorSpecializationType>> vectorSpecializationProvider)
    {
        GroupBaseProvider = groupBaseProvider;
        GroupSpecializationProvider = groupSpecializationProvider;
        GroupMemberProvider = groupMemberProvider;

        VectorBaseProvider = vectorBaseProvider;
        VectorSpecializationProvider = vectorSpecializationProvider;
    }

    public (IncrementalValueProvider<IResolvedVectorPopulation>, IVectorGenerator) Resolve(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider)
    {
        var resolvedGroupBases = GroupBaseResolver.Resolve(GroupBaseProvider, unitPopulationProvider, vectorPopulationProvider);
        var resolvedGroupSpecializations = GroupSpecializationResolver.Resolve(GroupSpecializationProvider, unitPopulationProvider, vectorPopulationProvider);
        var resolvedGroupMembers = GroupMemberResolver.Resolve(GroupMemberProvider, unitPopulationProvider, vectorPopulationProvider);

        var resolvedVectorBases = VectorBaseResolver.Resolve(VectorBaseProvider, unitPopulationProvider);
        var resolvedVectorSpecializations = VectorSpecializationResolver.Resolve(VectorSpecializationProvider, unitPopulationProvider, vectorPopulationProvider);

        var groupBaseInterfaces = resolvedGroupBases.Select(ExtractInterface).CollectResults();
        var groupSpecializationInterfaces = resolvedGroupSpecializations.Select(ExtractInterface).CollectResults();
        var groupMemberInterfaces = resolvedGroupMembers.Select(ExtractInterface).CollectResults();

        var vectorBaseInterfaces = resolvedVectorBases.Select(ExtractInterface).CollectResults();
        var vectorSpecializationInterfaces = resolvedVectorSpecializations.Select(ExtractInterface).CollectResults();

        var population = groupBaseInterfaces.Combine(groupSpecializationInterfaces, groupMemberInterfaces, vectorBaseInterfaces, vectorSpecializationInterfaces).Select(CreatePopulation);

        return (population, new VectorGenerator(resolvedGroupBases, resolvedGroupSpecializations, resolvedGroupMembers, resolvedVectorBases, resolvedVectorSpecializations));
    }

    private static Optional<IResolvedVectorGroupType> ExtractInterface(Optional<ResolvedGroupType> groupType, CancellationToken _) => groupType.HasValue ? groupType.Value : new Optional<IResolvedVectorGroupType>();
    private static Optional<IResolvedVectorType> ExtractInterface(Optional<ResolvedVectorType> vectorType, CancellationToken _) => vectorType.HasValue ? vectorType.Value : new Optional<IResolvedVectorType>();

    private static IResolvedVectorPopulation CreatePopulation((ImmutableArray<IResolvedVectorGroupType> GroupBases, ImmutableArray<IResolvedVectorGroupType> GroupSpecializations,
        ImmutableArray<IResolvedVectorType> GroupMembers, ImmutableArray<IResolvedVectorType> VectorBases, ImmutableArray<IResolvedVectorType> VectorSpecializations) vectors, CancellationToken _)
    {
        return ResolvedVectorPopulation.Build(vectors.VectorBases, vectors.VectorSpecializations, vectors.GroupBases, vectors.GroupSpecializations, vectors.GroupMembers);
    }
}
