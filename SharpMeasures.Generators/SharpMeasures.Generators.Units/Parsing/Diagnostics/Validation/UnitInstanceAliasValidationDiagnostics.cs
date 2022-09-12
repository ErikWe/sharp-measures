namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;

using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

internal class UnitInstanceAliasValidationDiagnostics : AModifiedUnitInstanceValidationDiagnostics<UnitInstanceAliasDefinition>
{
    public static UnitInstanceAliasValidationDiagnostics Instance { get; } = new();

    private UnitInstanceAliasValidationDiagnostics() { }
}
