namespace SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;

internal class EmptyBiasedUnitInstanceProcessingDiagnostics : EmptyModifiedUnitInstanceProcessingDiagnostics<RawBiasedUnitInstanceDefinition, BiasedUnitInstanceLocations>, IBiasedUnitInstanceProcessingDiagnostics
{
    new public static EmptyBiasedUnitInstanceProcessingDiagnostics Instance { get; } = new();

    Diagnostic? IBiasedUnitInstanceProcessingDiagnostics.EmptyExpression(IUnitInstanceProcessingContext context, RawBiasedUnitInstanceDefinition definition) => null;
    Diagnostic? IBiasedUnitInstanceProcessingDiagnostics.NullExpression(IUnitInstanceProcessingContext context, RawBiasedUnitInstanceDefinition definition) => null;
}
