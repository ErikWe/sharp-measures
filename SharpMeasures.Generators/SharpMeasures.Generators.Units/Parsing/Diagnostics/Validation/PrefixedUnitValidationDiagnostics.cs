namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;

using SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

internal class PrefixedUnitValidationDiagnostics : ADependantUnitValidationDiagnostics<PrefixedUnitDefinition, PrefixedUnitLocations>
{
    public static PrefixedUnitValidationDiagnostics Instance { get; } = new();

    private PrefixedUnitValidationDiagnostics() { }
}
