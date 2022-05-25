namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public class UnitAliasValidator : ADependantUnitValidator<IDependantUnitValidatorContext, UnitAliasDefinition>
{
    public UnitAliasValidator(IDependantUnitDiagnostics<UnitAliasDefinition> diagnostics) : base(diagnostics) { }
}