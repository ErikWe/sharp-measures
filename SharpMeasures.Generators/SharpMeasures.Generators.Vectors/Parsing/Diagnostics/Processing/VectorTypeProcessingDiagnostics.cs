namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

internal sealed class VectorTypeProcessingDiagnostics : IVectorTypeProcessingDiagnostics
{
    public static VectorTypeProcessingDiagnostics Instance { get; } = new();

    private VectorTypeProcessingDiagnostics() { }

    public Diagnostic ContradictoryUnitInstanceInclusionAndExclusion(IVector vector)
    {
        return DiagnosticConstruction.ContradictoryAttributes<IncludeUnitsAttribute, ExcludeUnitsAttribute>(vector.Locations.AttributeName.AsRoslynLocation());
    }
}
