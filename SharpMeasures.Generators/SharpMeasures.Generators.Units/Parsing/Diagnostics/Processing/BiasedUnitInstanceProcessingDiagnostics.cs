namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;

internal sealed class BiasedUnitInstanceProcessingDiagnostics : AModifiedUnitInstanceProcessingDiagnostics<RawBiasedUnitInstanceDefinition, BiasedUnitInstanceLocations>, IBiasedUnitInstanceProcessingDiagnostics
{
    public static BiasedUnitInstanceProcessingDiagnostics Instance { get; } = new();

    private BiasedUnitInstanceProcessingDiagnostics() { }

    public Diagnostic NullExpression(IUnitInstanceProcessingContext context, RawBiasedUnitInstanceDefinition definition)
    {
        return DiagnosticConstruction.NullBiasedUnitExpression(definition.Locations.Expression?.AsRoslynLocation());
    }

    public Diagnostic EmptyExpression(IUnitInstanceProcessingContext context, RawBiasedUnitInstanceDefinition definition)
    {
        return DiagnosticConstruction.EmptyBiasedUnitExpression(definition.Locations.Expression?.AsRoslynLocation());
    }
}
