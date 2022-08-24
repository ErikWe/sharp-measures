namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class ScaledUnitValidator : ADependantUnitValidator<IDependantUnitValidationContext, ScaledUnitDefinition, ScaledUnitLocations>
{
    public ScaledUnitValidator(IDependantUnitValidationDiagnostics<ScaledUnitDefinition, ScaledUnitLocations> diagnostics) : base(diagnostics) { }
}
