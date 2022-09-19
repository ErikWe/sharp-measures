namespace SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;

internal sealed class EmptyBiasedUnitInstanceValidationDiagnostics : AEmptyModifiedUnitInstanceValidationDiagnostics<BiasedUnitInstanceDefinition>, IBiasedUnitInstanceValidationDiagnostics
{
    public static EmptyBiasedUnitInstanceValidationDiagnostics Instance { get; } = new();

    private EmptyBiasedUnitInstanceValidationDiagnostics() { }

    Diagnostic? IBiasedUnitInstanceValidationDiagnostics.UnitNotIncludingBiasTerm(IBiasedUnitInstanceValidationContext context, BiasedUnitInstanceDefinition definition) => null;
}
