namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class FixedUnitDiagnostics : AUnitDiagnostics<FixedUnitDefinition>
{
    public static FixedUnitDiagnostics Instance { get; } = new();

    private FixedUnitDiagnostics() { }
}
