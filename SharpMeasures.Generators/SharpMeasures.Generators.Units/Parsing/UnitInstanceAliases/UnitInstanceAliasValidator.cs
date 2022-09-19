namespace SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal sealed class UnitInstanceAliasValidator : AModifiedUnitInstanceValidator<IModifiedUnitInstanceValidationContext, UnitInstanceAliasDefinition>
{
    public UnitInstanceAliasValidator(IModifiedUnitInstanceValidationDiagnostics<UnitInstanceAliasDefinition> diagnostics) : base(diagnostics) { }
}
