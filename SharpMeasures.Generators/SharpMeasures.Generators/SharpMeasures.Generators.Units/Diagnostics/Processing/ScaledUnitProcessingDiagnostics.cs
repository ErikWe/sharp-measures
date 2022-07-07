namespace SharpMeasures.Generators.Units.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.ScaledUnit;

internal class ScaledUnitProcessingDiagnostics : ADependantUnitProcessingDiagnostics<RawScaledUnitDefinition, ScaledUnitLocations>, IScaledUnitProcessingDiagnostics
{
    public static ScaledUnitProcessingDiagnostics Instance { get; } = new();

    private ScaledUnitProcessingDiagnostics() { }

    public Diagnostic NullExpression(IUnitProcessingContext context, RawScaledUnitDefinition definition)
    {
        return DiagnosticConstruction.NullScaledUnitExpression(definition.Locations.Expression?.AsRoslynLocation());
    }

    public Diagnostic EmptyExpression(IUnitProcessingContext context, RawScaledUnitDefinition definition) => NullExpression(context, definition);
}
