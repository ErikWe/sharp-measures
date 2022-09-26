namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;

internal sealed class EmptyFixedUnitInstanceProcessingDiagnostics : AEmptyUnitInstanceProcessingDiagnostics<RawFixedUnitInstanceDefinition, FixedUnitInstanceLocations>, IFixedUnitInstanceProcessingDiagnostics
{
    public static EmptyFixedUnitInstanceProcessingDiagnostics Instance { get; } = new();

    private EmptyFixedUnitInstanceProcessingDiagnostics() { }

    Diagnostic? IFixedUnitInstanceProcessingDiagnostics.UnitIsDerivable(IFixedUnitInstanceProcessingContext context, RawFixedUnitInstanceDefinition definition) => null;
}
