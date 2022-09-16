namespace SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;

internal class EmptyScaledUnitInstanceProcessingDiagnostics : EmptyModifiedUnitInstanceProcessingDiagnostics<RawScaledUnitInstanceDefinition, ScaledUnitInstanceLocations>, IScaledUnitInstanceProcessingDiagnostics
{
    new public static EmptyScaledUnitInstanceProcessingDiagnostics Instance { get; } = new();

    Diagnostic? IScaledUnitInstanceProcessingDiagnostics.EmptyExpression(IUnitInstanceProcessingContext context, RawScaledUnitInstanceDefinition definition) => null;
    Diagnostic? IScaledUnitInstanceProcessingDiagnostics.NullExpression(IUnitInstanceProcessingContext context, RawScaledUnitInstanceDefinition definition) => null;
}
