namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal class PrefixedUnitDiagnostics : ADependantUnitDiagnostics<PrefixedUnitDefinition>, IPrefixedUnitDiagnostics
{
    public static PrefixedUnitDiagnostics Instance { get; } = new();

    private PrefixedUnitDiagnostics() { }

    public Diagnostic UnrecognizedMetricPrefix(IDependantUnitValidatorContext context, PrefixedUnitDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedPrefix(definition.ParsingData.Locations.MetricPrefixName.AsRoslynLocation(), definition.MetricPrefixName);
    }

    public Diagnostic UnrecognizedBinaryPrefix(IDependantUnitValidatorContext context, PrefixedUnitDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedPrefix(definition.ParsingData.Locations.BinaryPrefixName.AsRoslynLocation(), definition.BinaryPrefixName);
    }
}
