namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.Parsing.Abstraction;

internal sealed class EmptyScalarTypeProcessingDiagnostics : IScalarTypeProcessingDiagnostics
{
    public static EmptyScalarTypeProcessingDiagnostics Instance { get; } = new();

    private EmptyScalarTypeProcessingDiagnostics() { }

    public Diagnostic? ContradictoryUnitInstanceInclusionAndExclusion(IScalar scalar) => null;

    public Diagnostic? ContradictoryUnitBaseInstanceInclusionAndExclusion(IScalar scalar) => null;
}
