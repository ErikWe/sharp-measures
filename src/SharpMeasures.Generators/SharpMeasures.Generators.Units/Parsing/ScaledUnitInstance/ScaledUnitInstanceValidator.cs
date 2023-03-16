namespace SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IScaledUnitInstanceValidationDiagnostics : IModifiedUnitInstanceValidationDiagnostics<ScaledUnitInstanceDefinition> { }

internal sealed class ScaledUnitInstanceValidator : AModifiedUnitInstanceValidator<IModifiedUnitInstanceValidationContext, ScaledUnitInstanceDefinition>
{
    public ScaledUnitInstanceValidator(IScaledUnitInstanceValidationDiagnostics diagnostics) : base(diagnostics) { }
}
