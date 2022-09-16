namespace SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;

internal class EmptyPrefixedUnitInstanceProcessingDiagnostics : EmptyModifiedUnitInstanceProcessingDiagnostics<RawPrefixedUnitInstanceDefinition, PrefixedUnitInstanceLocations>, IPrefixedUnitInstanceProcessingDiagnostics
{
    new public static EmptyPrefixedUnitInstanceProcessingDiagnostics Instance { get; } = new();

    Diagnostic? IPrefixedUnitInstanceProcessingDiagnostics.UnrecognizedBinaryPrefix(IUnitInstanceProcessingContext context, RawPrefixedUnitInstanceDefinition definition) => null;
    Diagnostic? IPrefixedUnitInstanceProcessingDiagnostics.UnrecognizedMetricPrefix(IUnitInstanceProcessingContext context, RawPrefixedUnitInstanceDefinition definition) => null;
}
