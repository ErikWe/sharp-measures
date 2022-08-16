namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;

using SharpMeasures.Generators.Units.Parsing.FixedUnit;

internal class FixedUnitProcessingDiagnostics : AUnitProcessingDiagnostics<UnprocessedFixedUnitDefinition, FixedUnitLocations>
{
    public static FixedUnitProcessingDiagnostics Instance { get; } = new();

    private FixedUnitProcessingDiagnostics() { }
}
