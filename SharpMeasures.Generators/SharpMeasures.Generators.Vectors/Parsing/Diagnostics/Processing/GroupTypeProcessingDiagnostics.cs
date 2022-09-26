namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

internal sealed class GroupTypeProcessingDiagnostics : IGroupTypeProcessingDiagnostics
{
    public static GroupTypeProcessingDiagnostics Instance { get; } = new();

    private GroupTypeProcessingDiagnostics() { }

    public Diagnostic ContradictoryUnitInstanceInclusionAndExclusion(IVectorGroup group)
    {
        return DiagnosticConstruction.ContradictoryAttributes<IncludeUnitsAttribute, ExcludeUnitsAttribute>(group.Locations.AttributeName.AsRoslynLocation());
    }
}
