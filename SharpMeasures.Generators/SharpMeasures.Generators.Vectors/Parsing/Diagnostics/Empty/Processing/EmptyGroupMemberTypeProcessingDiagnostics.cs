namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

internal sealed class EmptyGroupMemberTypeProcessingDiagnostics : IGroupMemberTypeProcessingDiagnostics
{
    public static EmptyGroupMemberTypeProcessingDiagnostics Instance { get; } = new();

    private EmptyGroupMemberTypeProcessingDiagnostics() { }

    Diagnostic? IGroupMemberTypeProcessingDiagnostics.ContradictoryUnitInstanceInclusionAndExclusion(IVectorGroupMember member) => null;
}
