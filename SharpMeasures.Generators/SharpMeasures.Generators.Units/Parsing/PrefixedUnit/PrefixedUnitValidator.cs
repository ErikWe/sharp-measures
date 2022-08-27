namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class PrefixedUnitValidator : ADependantUnitValidator<IDependantUnitValidationContext, PrefixedUnitDefinition, PrefixedUnitLocations>
{
    public PrefixedUnitValidator(IDependantUnitValidationDiagnostics<PrefixedUnitDefinition, PrefixedUnitLocations> diagnostics) : base(diagnostics) { }
}
