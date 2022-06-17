namespace SharpMeasures.Generators.Units.Diagnostics;

using SharpMeasures.Generators.Units.Parsing.ScaledUnit;

internal class ScaledUnitDiagnostics : ADependantUnitDiagnostics<RawScaledUnitDefinition>
{
    public static ScaledUnitDiagnostics Instance { get; } = new();

    private ScaledUnitDiagnostics() { }
}
