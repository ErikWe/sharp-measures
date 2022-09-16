namespace SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;

internal class EmptyFixedUnitInstanceProcessingDiagnostics : EmptyUnitInstanceProcessingDiagnostics<RawFixedUnitInstanceDefinition, FixedUnitInstanceLocations>, IFixedUnitInstanceProcessingDiagnostics
{
    new public static EmptyFixedUnitInstanceProcessingDiagnostics Instance { get; } = new();

    Diagnostic? IFixedUnitInstanceProcessingDiagnostics.UnitIsDerivable(IFixedUnitInstanceProcessingContext context, RawFixedUnitInstanceDefinition definition) => null;
}
