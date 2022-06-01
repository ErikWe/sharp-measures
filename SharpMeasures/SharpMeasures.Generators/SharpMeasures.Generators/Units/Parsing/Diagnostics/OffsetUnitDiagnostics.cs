namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class OffsetUnitDiagnostics : ADependantUnitDiagnostics<RawOffsetUnitDefinition>
{
    public static OffsetUnitDiagnostics Instance { get; } = new();

    private OffsetUnitDiagnostics() { }
}
