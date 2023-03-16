namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;

using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

internal sealed class UnitInstanceAliasValidationDiagnostics : AModifiedUnitInstanceValidationDiagnostics<UnitInstanceAliasDefinition>, IUnitInstanceAliasValidationDiagnostics
{
    public static UnitInstanceAliasValidationDiagnostics Instance { get; } = new();

    private UnitInstanceAliasValidationDiagnostics() { }
}
