namespace SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics.Validation;

using SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;

internal sealed class EmptyPrefixedUnitInstanceValidationDiagnostics : AEmptyModifiedUnitInstanceValidationDiagnostics<PrefixedUnitInstanceDefinition>
{
    public static EmptyPrefixedUnitInstanceValidationDiagnostics Instance { get; } = new();

    private EmptyPrefixedUnitInstanceValidationDiagnostics() { }
}
