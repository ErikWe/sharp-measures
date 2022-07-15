namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Resolution;

using SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

internal class PrefixedUnitResolutionDiagnostics : ADependantUnitResolutionDiagnostics<UnresolvedPrefixedUnitDefinition, PrefixedUnitLocations>
{
    public static PrefixedUnitResolutionDiagnostics Instance { get; } = new();

    private PrefixedUnitResolutionDiagnostics() { }
}
