namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public interface IVectorResolver
{
    public abstract (IncrementalValueProvider<IResolvedVectorPopulation>, IVectorGenerator) Resolve(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider);
}

internal class VectorResolver : IVectorResolver
{
    private IncrementalValueProvider<IVectorPopulationWithData> VectorPopulationProvider { get; }

    private IncrementalValuesProvider<GroupBaseType> GroupBaseProvider { get; }
    private IncrementalValuesProvider<GroupSpecializationType> GroupSpecializationProvider { get; }
    private IncrementalValuesProvider<GroupMemberType> GroupMemberProvider { get; }

    private IncrementalValuesProvider<VectorBaseType> VectorBaseProvider { get; }
    private IncrementalValuesProvider<VectorSpecializationType> VectorSpecializationProvider { get; }

    internal VectorResolver(IncrementalValueProvider<IVectorPopulationWithData> vectorPopulationProvider, IncrementalValuesProvider<GroupBaseType> groupBaseProvider,
        IncrementalValuesProvider<GroupSpecializationType> groupSpecializationProvider, IncrementalValuesProvider<GroupMemberType> groupMemberProvider,
        IncrementalValuesProvider<VectorBaseType> vectorBaseProvider, IncrementalValuesProvider<VectorSpecializationType> vectorSpecializationProvider)
    {
        VectorPopulationProvider = vectorPopulationProvider;

        GroupBaseProvider = groupBaseProvider;
        GroupSpecializationProvider = groupSpecializationProvider;
        GroupMemberProvider = groupMemberProvider;

        VectorBaseProvider = vectorBaseProvider;
        VectorSpecializationProvider = vectorSpecializationProvider;
    }

    public (IncrementalValueProvider<IResolvedVectorPopulation>, IVectorGenerator) Resolve(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider)
    {
        var resolvedGroupBases = GroupBaseResolver.Resolve(GroupBaseProvider, unitPopulationProvider, VectorPopulationProvider);
        var resolvedGroupSpecializations = GroupSpecializationResolver.Resolve(GroupSpecializationProvider, unitPopulationProvider, VectorPopulationProvider);
        var resolvedGroupMembers = GroupMemberResolver.Resolve(GroupMemberProvider, unitPopulationProvider, VectorPopulationProvider);

        var resolvedVectorBases = VectorBaseResolver.Resolve(VectorBaseProvider, unitPopulationProvider);
        var resolvedVectorSpecializations = VectorSpecializationResolver.Resolve(VectorSpecializationProvider, unitPopulationProvider, VectorPopulationProvider);

        var groupBaseInterfaces = resolvedGroupBases.Select(ExtractInterface).Collect();
        var groupSpecializationInterfaces = resolvedGroupSpecializations.Select(ExtractInterface).Collect();
        var groupMemberInterfaces = resolvedGroupMembers.Select(ExtractInterface).Collect();

        var vectorBaseInterfaces = resolvedVectorBases.Select(ExtractInterface).Collect();
        var vectorSpecializationInterfaces = resolvedVectorSpecializations.Select(ExtractInterface).Collect();

        var populationWithData = groupBaseInterfaces.Combine(groupSpecializationInterfaces, groupMemberInterfaces, vectorBaseInterfaces, vectorSpecializationInterfaces).Select(CreatePopulation);

        return (populationWithData, new VectorGenerator(populationWithData, resolvedGroupBases, resolvedGroupSpecializations, resolvedGroupMembers,
            resolvedVectorBases, resolvedVectorSpecializations));
    }

    private static IResolvedVectorGroupType ExtractInterface(ResolvedGroupType groupType, CancellationToken _) => groupType;
    private static IResolvedVectorType ExtractInterface(ResolvedVectorType vectorType, CancellationToken _) => vectorType;

    private static IResolvedVectorPopulation CreatePopulation((ImmutableArray<IResolvedVectorGroupType> GroupBases, ImmutableArray<IResolvedVectorGroupType> GroupSpecializations,
        ImmutableArray<IResolvedVectorType> GroupMembers, ImmutableArray<IResolvedVectorType> VectorBases, ImmutableArray<IResolvedVectorType> VectorSpecializations) vectors, CancellationToken _)
    {
        return ResolvedVectorPopulation.Build(vectors.VectorBases, vectors.VectorSpecializations, vectors.GroupBases, vectors.GroupSpecializations, vectors.GroupMembers);
    }
}
