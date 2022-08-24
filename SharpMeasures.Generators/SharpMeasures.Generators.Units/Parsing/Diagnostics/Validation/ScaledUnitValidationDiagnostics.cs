namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;

using SharpMeasures.Generators.Units.Parsing.ScaledUnit;

internal class ScaledUnitValidationDiagnostics : ADependantUnitValidationDiagnostics<ScaledUnitDefinition, ScaledUnitLocations>
{
    public static ScaledUnitValidationDiagnostics Instance { get; } = new();

    private ScaledUnitValidationDiagnostics() { }
}
