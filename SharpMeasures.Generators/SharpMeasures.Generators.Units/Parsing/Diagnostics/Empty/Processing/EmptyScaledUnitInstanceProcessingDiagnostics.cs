namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;

internal sealed class EmptyScaledUnitInstanceProcessingDiagnostics : AEmptyModifiedUnitInstanceProcessingDiagnostics<RawScaledUnitInstanceDefinition, ScaledUnitInstanceLocations>, IScaledUnitInstanceProcessingDiagnostics
{
    public static EmptyScaledUnitInstanceProcessingDiagnostics Instance { get; } = new();

    private EmptyScaledUnitInstanceProcessingDiagnostics() { }

    Diagnostic? IScaledUnitInstanceProcessingDiagnostics.EmptyExpression(IUnitInstanceProcessingContext context, RawScaledUnitInstanceDefinition definition) => null;
    Diagnostic? IScaledUnitInstanceProcessingDiagnostics.NullExpression(IUnitInstanceProcessingContext context, RawScaledUnitInstanceDefinition definition) => null;
}
