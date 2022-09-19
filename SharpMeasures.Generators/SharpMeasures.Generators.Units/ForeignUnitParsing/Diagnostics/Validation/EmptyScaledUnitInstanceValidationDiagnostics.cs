namespace SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics.Validation;

using SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;

internal sealed class EmptyScaledUnitInstanceValidationDiagnostics : AEmptyModifiedUnitInstanceValidationDiagnostics<ScaledUnitInstanceDefinition>
{
    public static EmptyScaledUnitInstanceValidationDiagnostics Instance { get; } = new();

    private EmptyScaledUnitInstanceValidationDiagnostics() { }
}
