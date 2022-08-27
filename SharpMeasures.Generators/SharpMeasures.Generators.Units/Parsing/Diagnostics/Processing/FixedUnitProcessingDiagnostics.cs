namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;

using SharpMeasures.Generators.Units.Parsing.FixedUnit;

internal class FixedUnitProcessingDiagnostics : AUnitProcessingDiagnostics<RawFixedUnitDefinition, FixedUnitLocations>
{
    public static FixedUnitProcessingDiagnostics Instance { get; } = new();

    private FixedUnitProcessingDiagnostics() { }
}
