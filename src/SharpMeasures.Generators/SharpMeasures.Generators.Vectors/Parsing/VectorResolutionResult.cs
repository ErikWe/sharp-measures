namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

public sealed record class VectorResolutionResult
{
    internal IncrementalValuesProvider<Optional<ResolvedGroupType>> GroupBaseProvider { get; }
    internal IncrementalValuesProvider<Optional<ResolvedGroupType>> GroupSpecializationProvider { get; }
    internal IncrementalValuesProvider<Optional<ResolvedVectorType>> GroupMemberProvider { get; }

    internal IncrementalValuesProvider<Optional<ResolvedVectorType>> VectorBaseProvider { get; }
    internal IncrementalValuesProvider<Optional<ResolvedVectorType>> VectorSpecializationProvider { get; }

    internal VectorResolutionResult(IncrementalValuesProvider<Optional<ResolvedGroupType>> groupBaseProvider, IncrementalValuesProvider<Optional<ResolvedGroupType>> groupSpecializationProvider, IncrementalValuesProvider<Optional<ResolvedVectorType>> groupMemberProvider,
        IncrementalValuesProvider<Optional<ResolvedVectorType>> vectorBaseProvider, IncrementalValuesProvider<Optional<ResolvedVectorType>> vectorSpecializationProvider)
    {
        GroupBaseProvider = groupBaseProvider;
        GroupSpecializationProvider = groupSpecializationProvider;
        GroupMemberProvider = groupMemberProvider;

        VectorBaseProvider = vectorBaseProvider;
        VectorSpecializationProvider = vectorSpecializationProvider;
    }
}
