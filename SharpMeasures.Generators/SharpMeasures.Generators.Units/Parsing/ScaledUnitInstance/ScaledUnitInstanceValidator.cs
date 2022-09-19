namespace SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal sealed class ScaledUnitInstanceValidator : AModifiedUnitInstanceValidator<IModifiedUnitInstanceValidationContext, ScaledUnitInstanceDefinition>
{
    public ScaledUnitInstanceValidator(IModifiedUnitInstanceValidationDiagnostics<ScaledUnitInstanceDefinition> diagnostics) : base(diagnostics) { }
}
