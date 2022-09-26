﻿namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

internal sealed class EmptyGroupTypeProcessingDiagnostics : IGroupTypeProcessingDiagnostics
{
    public static EmptyGroupTypeProcessingDiagnostics Instance { get; } = new();

    private EmptyGroupTypeProcessingDiagnostics() { }

    Diagnostic? IGroupTypeProcessingDiagnostics.ContradictoryUnitInstanceInclusionAndExclusion(IVectorGroup group) => null;
}
