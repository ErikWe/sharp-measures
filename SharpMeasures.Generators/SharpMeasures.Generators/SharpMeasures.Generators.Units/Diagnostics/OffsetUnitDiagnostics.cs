namespace SharpMeasures.Generators.Units.Diagnostics;

using SharpMeasures.Generators.Units.Parsing.OffsetUnit;

internal class OffsetUnitDiagnostics : ADependantUnitDiagnostics<RawOffsetUnitDefinition>
{
    public static OffsetUnitDiagnostics Instance { get; } = new();

    private OffsetUnitDiagnostics() { }
}
