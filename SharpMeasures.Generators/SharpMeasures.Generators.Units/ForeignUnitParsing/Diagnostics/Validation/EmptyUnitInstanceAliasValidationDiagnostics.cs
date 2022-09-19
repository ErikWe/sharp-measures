namespace SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics.Validation;

using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

internal sealed class EmptyUnitInstanceAliasValidationDiagnostics : AEmptyModifiedUnitInstanceValidationDiagnostics<UnitInstanceAliasDefinition>
{
    public static EmptyUnitInstanceAliasValidationDiagnostics Instance { get; } = new();

    private EmptyUnitInstanceAliasValidationDiagnostics() { }
}
