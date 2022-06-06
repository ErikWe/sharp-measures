namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class OffsetUnitDiagnostics : ADependantUnitDiagnostics<RawOffsetUnit>
{
    public static OffsetUnitDiagnostics Instance { get; } = new();

    private OffsetUnitDiagnostics() { }
}
