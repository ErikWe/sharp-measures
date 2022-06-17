namespace SharpMeasures.Generators.Units.Diagnostics;

using SharpMeasures.Generators.Units.Parsing.UnitAlias;

internal class UnitAliasDiagnostics : ADependantUnitDiagnostics<RawUnitAliasDefinition>
{
    public static UnitAliasDiagnostics Instance { get; } = new();

    private UnitAliasDiagnostics() { }
}
