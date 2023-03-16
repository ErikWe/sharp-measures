namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;

internal sealed class EmptyBiasedUnitInstanceValidationDiagnostics : AEmptyModifiedUnitInstanceValidationDiagnostics<BiasedUnitInstanceDefinition>, IBiasedUnitInstanceValidationDiagnostics
{
    public static EmptyBiasedUnitInstanceValidationDiagnostics Instance { get; } = new();

    private EmptyBiasedUnitInstanceValidationDiagnostics() { }

    public Diagnostic? UnitNotIncludingBiasTerm(IBiasedUnitInstanceValidationContext context, BiasedUnitInstanceDefinition definition) => null;
}
