namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

public sealed record class VectorParsingResult
{
    internal IncrementalValuesProvider<Optional<RawGroupBaseType>> GroupBaseProvider { get; }
    internal IncrementalValuesProvider<Optional<RawGroupSpecializationType>> GroupSpecializationProvider { get; }
    internal IncrementalValuesProvider<Optional<RawGroupMemberType>> GroupMemberProvider { get; }

    internal IncrementalValuesProvider<Optional<RawVectorBaseType>> VectorBaseProvider { get; }
    internal IncrementalValuesProvider<Optional<RawVectorSpecializationType>> VectorSpecializationProvider { get; }

    internal VectorParsingResult(IncrementalValuesProvider<Optional<RawGroupBaseType>> groupBaseProvider, IncrementalValuesProvider<Optional<RawGroupSpecializationType>> groupSpecializationProvider, IncrementalValuesProvider<Optional<RawGroupMemberType>> groupMemberProvider,
        IncrementalValuesProvider<Optional<RawVectorBaseType>> vectorBaseProvider, IncrementalValuesProvider<Optional<RawVectorSpecializationType>> vectorSpecializationProvider)
    {
        GroupBaseProvider = groupBaseProvider;
        GroupSpecializationProvider = groupSpecializationProvider;
        GroupMemberProvider = groupMemberProvider;

        VectorBaseProvider = vectorBaseProvider;
        VectorSpecializationProvider = vectorSpecializationProvider;
    }
}
