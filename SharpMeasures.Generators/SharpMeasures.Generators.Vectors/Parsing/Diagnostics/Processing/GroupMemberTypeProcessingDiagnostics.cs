namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

internal sealed class GroupMemberTypeProcessingDiagnostics : IGroupMemberTypeProcessingDiagnostics
{
    public static GroupMemberTypeProcessingDiagnostics Instance { get; } = new();

    private GroupMemberTypeProcessingDiagnostics() { }

    public Diagnostic ContradictoryUnitInstanceInclusionAndExclusion(IVectorGroupMember member)
    {
        return DiagnosticConstruction.ContradictoryAttributes<IncludeUnitsAttribute, ExcludeUnitsAttribute>(member.Locations.AttributeName.AsRoslynLocation());
    }
}
