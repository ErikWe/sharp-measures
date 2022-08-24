namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;

using SharpMeasures.Generators.Units.Parsing.UnitAlias;

internal class UnitAliasValidationDiagnostics : ADependantUnitValidationDiagnostics<UnitAliasDefinition, UnitAliasLocations>
{
    public static UnitAliasValidationDiagnostics Instance { get; } = new();

    private UnitAliasValidationDiagnostics() { }
}
