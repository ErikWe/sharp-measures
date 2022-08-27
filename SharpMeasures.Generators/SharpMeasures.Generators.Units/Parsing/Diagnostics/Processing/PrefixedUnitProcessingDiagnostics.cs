namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;

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
        return DiagnosticConstruction.UnrecognizedEnumValue(definition.Locations.MetricPrefix?.AsRoslynLocation(), definition.MetricPrefix);
    }

    public Diagnostic UnrecognizedBinaryPrefix(IUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedEnumValue(definition.Locations.BinaryPrefix?.AsRoslynLocation(), definition.BinaryPrefix);
    }
}
