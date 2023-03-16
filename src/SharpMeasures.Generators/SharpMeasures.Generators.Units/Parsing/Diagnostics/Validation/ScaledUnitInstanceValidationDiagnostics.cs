namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;

using SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;

internal sealed class ScaledUnitInstanceValidationDiagnostics : AModifiedUnitInstanceValidationDiagnostics<ScaledUnitInstanceDefinition>, IScaledUnitInstanceValidationDiagnostics
{
    public static ScaledUnitInstanceValidationDiagnostics Instance { get; } = new();

    private ScaledUnitInstanceValidationDiagnostics() { }
}
