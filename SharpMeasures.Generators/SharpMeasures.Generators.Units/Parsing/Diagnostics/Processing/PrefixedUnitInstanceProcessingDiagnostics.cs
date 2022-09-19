namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;

internal sealed class PrefixedUnitInstanceProcessingDiagnostics : AModifiedUnitInstanceProcessingDiagnostics<RawPrefixedUnitInstanceDefinition, PrefixedUnitInstanceLocations>, IPrefixedUnitInstanceProcessingDiagnostics
{
    public static PrefixedUnitInstanceProcessingDiagnostics Instance { get; } = new();

    private PrefixedUnitInstanceProcessingDiagnostics() { }

    public Diagnostic UnrecognizedMetricPrefix(IUnitInstanceProcessingContext context, RawPrefixedUnitInstanceDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedEnumValue(definition.Locations.MetricPrefix?.AsRoslynLocation(), definition.MetricPrefix!.Value);
    }

    public Diagnostic UnrecognizedBinaryPrefix(IUnitInstanceProcessingContext context, RawPrefixedUnitInstanceDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedEnumValue(definition.Locations.BinaryPrefix?.AsRoslynLocation(), definition.BinaryPrefix!.Value);
    }
}
