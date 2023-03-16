namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IPrefixedUnitInstanceValidationDiagnostics : IModifiedUnitInstanceValidationDiagnostics<PrefixedUnitInstanceDefinition> { }

internal sealed class PrefixedUnitInstanceValidator : AModifiedUnitInstanceValidator<IModifiedUnitInstanceValidationContext, PrefixedUnitInstanceDefinition>
{
    public PrefixedUnitInstanceValidator(IPrefixedUnitInstanceValidationDiagnostics diagnostics) : base(diagnostics) { }
}
