namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class UnitAliasValidator : ADependantUnitValidator<IDependantUnitValidationContext, UnitAliasDefinition, UnitAliasLocations>
{
    public UnitAliasValidator(IDependantUnitValidationDiagnostics<UnitAliasDefinition, UnitAliasLocations> diagnostics) : base(diagnostics) { }
}
