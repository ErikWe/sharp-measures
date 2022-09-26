namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;

using SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;

internal sealed class PrefixedUnitInstanceValidationDiagnostics : AModifiedUnitInstanceValidationDiagnostics<PrefixedUnitInstanceDefinition>, IPrefixedUnitInstanceValidationDiagnostics
{
    public static PrefixedUnitInstanceValidationDiagnostics Instance { get; } = new();

    private PrefixedUnitInstanceValidationDiagnostics() { }
}
