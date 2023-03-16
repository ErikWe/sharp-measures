namespace SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IUnitInstanceAliasValidationDiagnostics : IModifiedUnitInstanceValidationDiagnostics<UnitInstanceAliasDefinition> { }

internal sealed class UnitInstanceAliasValidator : AModifiedUnitInstanceValidator<IModifiedUnitInstanceValidationContext, UnitInstanceAliasDefinition>
{
    public UnitInstanceAliasValidator(IUnitInstanceAliasValidationDiagnostics diagnostics) : base(diagnostics) { }
}
