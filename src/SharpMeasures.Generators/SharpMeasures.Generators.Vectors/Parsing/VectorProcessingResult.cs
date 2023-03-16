namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

public sealed record class VectorProcessingResult
{
    internal IncrementalValuesProvider<Optional<GroupBaseType>> GroupBaseProvider { get; }
    internal IncrementalValuesProvider<Optional<GroupSpecializationType>> GroupSpecializationProvider { get; }
    internal IncrementalValuesProvider<Optional<GroupMemberType>> GroupMemberProvider { get; }

    internal IncrementalValuesProvider<Optional<VectorBaseType>> VectorBaseProvider { get; }
    internal IncrementalValuesProvider<Optional<VectorSpecializationType>> VectorSpecializationProvider { get; }

    internal IncrementalValueProvider<VectorProcessingData> ProcessingDataProvider { get; }

    internal VectorProcessingResult(IncrementalValuesProvider<Optional<GroupBaseType>> groupBaseProvider, IncrementalValuesProvider<Optional<GroupSpecializationType>> groupSpecializationProvider, IncrementalValuesProvider<Optional<GroupMemberType>> groupMemberProvider,
        IncrementalValuesProvider<Optional<VectorBaseType>> vectorBaseProvider, IncrementalValuesProvider<Optional<VectorSpecializationType>> vectorSpecializationProvider, IncrementalValueProvider<VectorProcessingData> processingDataProvider)
    {
        GroupBaseProvider = groupBaseProvider;
        GroupSpecializationProvider = groupSpecializationProvider;
        GroupMemberProvider = groupMemberProvider;

        VectorBaseProvider = vectorBaseProvider;
        VectorSpecializationProvider = vectorSpecializationProvider;

        ProcessingDataProvider = processingDataProvider;
    }
}
