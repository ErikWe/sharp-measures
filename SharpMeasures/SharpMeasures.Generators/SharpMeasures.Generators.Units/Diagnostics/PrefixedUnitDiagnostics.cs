namespace SharpMeasures.Generators.Units.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

internal class PrefixedUnitDiagnostics : ADependantUnitDiagnostics<RawPrefixedUnitDefinition>, IPrefixedUnitProcessingDiagnostics
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
