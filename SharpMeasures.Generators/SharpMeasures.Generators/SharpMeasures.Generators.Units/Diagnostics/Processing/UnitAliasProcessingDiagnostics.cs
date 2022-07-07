namespace SharpMeasures.Generators.Units.Diagnostics.Processing;

using SharpMeasures.Generators.Units.Parsing.UnitAlias;

internal class UnitAliasProcessingDiagnostics : ADependantUnitProcessingDiagnostics<RawUnitAliasDefinition, UnitAliasLocations>
{
    public static UnitAliasProcessingDiagnostics Instance { get; } = new();

    private UnitAliasProcessingDiagnostics() { }
}
