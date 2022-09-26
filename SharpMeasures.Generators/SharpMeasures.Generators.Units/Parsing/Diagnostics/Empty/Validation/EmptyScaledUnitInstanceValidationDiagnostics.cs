namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Validation;

using SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;

internal sealed class EmptyScaledUnitInstanceValidationDiagnostics : AEmptyModifiedUnitInstanceValidationDiagnostics<ScaledUnitInstanceDefinition>, IScaledUnitInstanceValidationDiagnostics
{
    public static EmptyScaledUnitInstanceValidationDiagnostics Instance { get; } = new();

    private EmptyScaledUnitInstanceValidationDiagnostics() { }
}
