namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Validation;

using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

internal sealed class EmptyUnitInstanceAliasValidationDiagnostics : AEmptyModifiedUnitInstanceValidationDiagnostics<UnitInstanceAliasDefinition>, IUnitInstanceAliasValidationDiagnostics
{
    public static EmptyUnitInstanceAliasValidationDiagnostics Instance { get; } = new();

    private EmptyUnitInstanceAliasValidationDiagnostics() { }
}
