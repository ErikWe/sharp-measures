namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class UnitAliasDiagnostics : ADependantUnitDiagnostics<RawUnitAlias>
{
    public static UnitAliasDiagnostics Instance { get; } = new();

    private UnitAliasDiagnostics() { }
}
