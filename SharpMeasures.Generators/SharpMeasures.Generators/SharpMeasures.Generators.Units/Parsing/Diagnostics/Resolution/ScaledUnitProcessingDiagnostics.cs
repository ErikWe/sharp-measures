namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Resolution;

using SharpMeasures.Generators.Units.Parsing.ScaledUnit;

internal class ScaledUnitResolutionDiagnostics : ADependantUnitResolutionDiagnostics<UnresolvedScaledUnitDefinition, ScaledUnitLocations>
{
    public static ScaledUnitResolutionDiagnostics Instance { get; } = new();

    private ScaledUnitResolutionDiagnostics() { }
}
