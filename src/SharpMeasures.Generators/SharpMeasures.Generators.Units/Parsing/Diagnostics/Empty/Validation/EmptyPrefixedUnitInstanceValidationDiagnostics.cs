namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Validation;

using SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;

internal sealed class EmptyPrefixedUnitInstanceValidationDiagnostics : AEmptyModifiedUnitInstanceValidationDiagnostics<PrefixedUnitInstanceDefinition>, IPrefixedUnitInstanceValidationDiagnostics
{
    public static EmptyPrefixedUnitInstanceValidationDiagnostics Instance { get; } = new();

    private EmptyPrefixedUnitInstanceValidationDiagnostics() { }
}
