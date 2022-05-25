namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public class OffsetUnitValidator : ADependantUnitValidator<IDependantUnitValidatorContext, OffsetUnitDefinition>
{
    public OffsetUnitValidator(IDependantUnitDiagnostics<OffsetUnitDefinition> diagnostics) : base(diagnostics) { }
}