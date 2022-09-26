namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.Parsing.Abstraction;

internal sealed class EmptyScalarTypeProcessingDiagnostics : IScalarTypeProcessingDiagnostics
{
    public static EmptyScalarTypeProcessingDiagnostics Instance { get; } = new();

    private EmptyScalarTypeProcessingDiagnostics() { }

    Diagnostic? IScalarTypeProcessingDiagnostics.ContradictoryUnitInstanceInclusionAndExclusion(IScalar scalar) => null;

    Diagnostic? IScalarTypeProcessingDiagnostics.ContradictoryUnitBaseInstanceInclusionAndExclusion(IScalar scalar) => null;
}
