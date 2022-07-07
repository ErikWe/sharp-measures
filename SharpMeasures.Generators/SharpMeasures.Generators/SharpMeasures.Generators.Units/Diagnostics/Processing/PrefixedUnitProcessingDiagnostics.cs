namespace SharpMeasures.Generators.Units.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

internal class PrefixedUnitProcessingDiagnostics : ADependantUnitProcessingDiagnostics<RawPrefixedUnitDefinition, PrefixedUnitLocations>, IPrefixedUnitProcessingDiagnostics
{
    public static PrefixedUnitProcessingDiagnostics Instance { get; } = new();

    private PrefixedUnitProcessingDiagnostics() { }

    public Diagnostic UnrecognizedMetricPrefix(IUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedPrefix(definition.Locations.MetricPrefixName?.AsRoslynLocation(), definition.MetricPrefixName);
    }

    public Diagnostic UnrecognizedBinaryPrefix(IUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedPrefix(definition.Locations.BinaryPrefixName?.AsRoslynLocation(), definition.BinaryPrefixName);
    }
}
