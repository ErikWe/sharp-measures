namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class ScaledUnitDiagnostics : ADependantUnitDiagnostics<RawScaledUnitDefinition>
{
    public static ScaledUnitDiagnostics Instance { get; } = new();

    private ScaledUnitDiagnostics() { }
}
