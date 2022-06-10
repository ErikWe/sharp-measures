namespace SharpMeasures.Generators.Units.Diagnostics;

using SharpMeasures.Generators.Units.Parsing.FixedUnit;

internal class FixedUnitDiagnostics : AUnitDiagnostics<RawFixedUnitDefinition>
{
    public static FixedUnitDiagnostics Instance { get; } = new();

    private FixedUnitDiagnostics() { }
}
