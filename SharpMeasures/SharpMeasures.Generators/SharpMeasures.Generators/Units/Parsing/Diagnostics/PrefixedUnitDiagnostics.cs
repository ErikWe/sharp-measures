namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal class PrefixedUnitDiagnostics : ADependantUnitDiagnostics<RawPrefixedUnitDefinition>, IPrefixedUnitDiagnostics
{
    public static PrefixedUnitDiagnostics Instance { get; } = new();

    private PrefixedUnitDiagnostics() { }

    public Diagnostic UnrecognizedMetricPrefix(IDependantUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedPrefix(definition.Locations.MetricPrefixName?.AsRoslynLocation(), definition.MetricPrefixName);
    }

    public Diagnostic UnrecognizedBinaryPrefix(IDependantUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedPrefix(definition.Locations.BinaryPrefixName?.AsRoslynLocation(), definition.BinaryPrefixName);
    }
}
