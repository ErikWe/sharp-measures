namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public class ScaledUnitValidator : ADependantUnitValidator<IDependantUnitValidatorContext, ScaledUnitDefinition>
{
    public ScaledUnitValidator(IDependantUnitDiagnostics<ScaledUnitDefinition> diagnostics) : base(diagnostics) { }
}