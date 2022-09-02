namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;

internal class ScaledUnitInstanceProcessingDiagnostics : AModifiedUnitInstanceProcessingDiagnostics<RawScaledUnitInstanceDefinition, ScaledUnitInstanceLocations>, IScaledUnitInstanceProcessingDiagnostics
{
    public static ScaledUnitInstanceProcessingDiagnostics Instance { get; } = new();

    private ScaledUnitInstanceProcessingDiagnostics() { }

    public Diagnostic NullExpression(IUnitInstanceProcessingContext context, RawScaledUnitInstanceDefinition definition)
    {
        return DiagnosticConstruction.NullScaledUnitExpression(definition.Locations.Expression?.AsRoslynLocation());
    }

    public Diagnostic EmptyExpression(IUnitInstanceProcessingContext context, RawScaledUnitInstanceDefinition definition)
    {
        return DiagnosticConstruction.EmptyScaledUnitExpression(definition.Locations.Expression?.AsRoslynLocation());
    }
}
