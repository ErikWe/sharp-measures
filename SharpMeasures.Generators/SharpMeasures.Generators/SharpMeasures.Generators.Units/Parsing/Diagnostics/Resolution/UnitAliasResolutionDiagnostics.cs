namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Resolution;

using SharpMeasures.Generators.Units.Parsing.UnitAlias;

internal class UnitAliasResolutionDiagnostics : ADependantUnitResolutionDiagnostics<UnresolvedUnitAliasDefinition, UnitAliasLocations>
{
    public static UnitAliasResolutionDiagnostics Instance { get; } = new();

    private UnitAliasResolutionDiagnostics() { }
}
