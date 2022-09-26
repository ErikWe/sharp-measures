namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

internal sealed class EmptyVectorTypeProcessingDiagnostics : IVectorTypeProcessingDiagnostics
{
    public static EmptyVectorTypeProcessingDiagnostics Instance { get; } = new();

    private EmptyVectorTypeProcessingDiagnostics() { }

    Diagnostic? IVectorTypeProcessingDiagnostics.ContradictoryUnitInstanceInclusionAndExclusion(IVector vector) => null;
}
