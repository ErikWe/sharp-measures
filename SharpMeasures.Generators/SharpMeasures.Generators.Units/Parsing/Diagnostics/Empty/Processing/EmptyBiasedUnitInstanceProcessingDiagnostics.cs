namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;

internal sealed class EmptyBiasedUnitInstanceProcessingDiagnostics : AEmptyModifiedUnitInstanceProcessingDiagnostics<RawBiasedUnitInstanceDefinition, BiasedUnitInstanceLocations>, IBiasedUnitInstanceProcessingDiagnostics
{
    public static EmptyBiasedUnitInstanceProcessingDiagnostics Instance { get; } = new();

    private EmptyBiasedUnitInstanceProcessingDiagnostics() { }

    Diagnostic? IBiasedUnitInstanceProcessingDiagnostics.EmptyExpression(IUnitInstanceProcessingContext context, RawBiasedUnitInstanceDefinition definition) => null;
    Diagnostic? IBiasedUnitInstanceProcessingDiagnostics.NullExpression(IUnitInstanceProcessingContext context, RawBiasedUnitInstanceDefinition definition) => null;
}
