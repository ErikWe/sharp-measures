namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;

internal sealed class EmptyPrefixedUnitInstanceProcessingDiagnostics : AEmptyModifiedUnitInstanceProcessingDiagnostics<RawPrefixedUnitInstanceDefinition, PrefixedUnitInstanceLocations>, IPrefixedUnitInstanceProcessingDiagnostics
{
    public static EmptyPrefixedUnitInstanceProcessingDiagnostics Instance { get; } = new();

    private EmptyPrefixedUnitInstanceProcessingDiagnostics() { }

    public Diagnostic? UnrecognizedBinaryPrefix(IUnitInstanceProcessingContext context, RawPrefixedUnitInstanceDefinition definition) => null;
    public Diagnostic? UnrecognizedMetricPrefix(IUnitInstanceProcessingContext context, RawPrefixedUnitInstanceDefinition definition) => null;
}
