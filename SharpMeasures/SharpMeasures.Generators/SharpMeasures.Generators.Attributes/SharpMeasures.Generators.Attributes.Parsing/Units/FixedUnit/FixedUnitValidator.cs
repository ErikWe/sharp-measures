namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public class FixedUnitValidator : AUnitValidator<IUnitValidatorContext, FixedUnitDefinition>
{
    public FixedUnitValidator(IUnitDiagnostics<FixedUnitDefinition> diagnostics) : base(diagnostics) { }
}