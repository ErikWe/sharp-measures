namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public static class VectorResolver
{
    public static (VectorResolutionResult ResolutionResult, IncrementalValueProvider<IResolvedVectorPopulation> Population) Resolve(VectorValidationResult validationResult, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider)
    {
        var resolvedGroupBases = GroupBaseResolver.Resolve(validationResult.GroupBaseProvider, unitPopulationProvider, vectorPopulationProvider);
        var resolvedGroupSpecializations = GroupSpecializationResolver.Resolve(validationResult.GroupSpecializationProvider, unitPopulationProvider, vectorPopulationProvider);
        var resolvedGroupMembers = GroupMemberResolver.Resolve(validationResult.GroupMemberProvider, unitPopulationProvider, vectorPopulationProvider);

        var resolvedVectorBases = VectorBaseResolver.Resolve(validationResult.VectorBaseProvider, unitPopulationProvider);
        var resolvedVectorSpecializations = VectorSpecializationResolver.Resolve(validationResult.VectorSpecializationProvider, unitPopulationProvider, vectorPopulationProvider);

        var groupBaseInterfaces = resolvedGroupBases.Select(ExtractInterface).CollectResults();
        var groupSpecializationInterfaces = resolvedGroupSpecializations.Select(ExtractInterface).CollectResults();
        var groupMemberInterfaces = resolvedGroupMembers.Select(ExtractInterface).CollectResults();

        var vectorBaseInterfaces = resolvedVectorBases.Select(ExtractInterface).CollectResults();
        var vectorSpecializationInterfaces = resolvedVectorSpecializations.Select(ExtractInterface).CollectResults();

        var population = groupBaseInterfaces.Combine(groupSpecializationInterfaces, groupMemberInterfaces, vectorBaseInterfaces, vectorSpecializationInterfaces).Select(CreatePopulation);

        return (new VectorResolutionResult(resolvedGroupBases, resolvedGroupSpecializations, resolvedGroupMembers, resolvedVectorBases, resolvedVectorSpecializations), population);
    }

    private static Optional<IResolvedVectorGroupType> ExtractInterface(Optional<ResolvedGroupType> groupType, CancellationToken _) => groupType.HasValue ? groupType.Value : new Optional<IResolvedVectorGroupType>();
    private static Optional<IResolvedVectorType> ExtractInterface(Optional<ResolvedVectorType> vectorType, CancellationToken _) => vectorType.HasValue ? vectorType.Value : new Optional<IResolvedVectorType>();

    private static IResolvedVectorPopulation CreatePopulation((ImmutableArray<IResolvedVectorGroupType> GroupBases, ImmutableArray<IResolvedVectorGroupType> GroupSpecializations, ImmutableArray<IResolvedVectorType> GroupMembers, ImmutableArray<IResolvedVectorType> VectorBases, ImmutableArray<IResolvedVectorType> VectorSpecializations) vectors, CancellationToken _)
    {
        return ResolvedVectorPopulation.Build(vectors.VectorBases, vectors.VectorSpecializations, vectors.GroupBases, vectors.GroupSpecializations, vectors.GroupMembers);
    }
}
