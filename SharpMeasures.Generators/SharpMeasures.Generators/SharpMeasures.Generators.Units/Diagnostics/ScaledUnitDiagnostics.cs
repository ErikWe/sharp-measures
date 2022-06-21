namespace SharpMeasures.Generators.Units.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.ScaledUnit;

internal class ScaledUnitDiagnostics : ADependantUnitDiagnostics<RawScaledUnitDefinition>, IScaledUnitProcessingDiagnostics
{
    public static ScaledUnitDiagnostics Instance { get; } = new();

    private ScaledUnitDiagnostics() { }

    public Diagnostic NullExpression(IDependantUnitProcessingContext context, RawScaledUnitDefinition definition)
    {
        return DiagnosticConstruction.NullScaledUnitExpression(definition.Locations.Expression?.AsRoslynLocation());
    }

    public Diagnostic EmptyExpression(IDependantUnitProcessingContext context, RawScaledUnitDefinition definition) => NullExpression(context, definition);
}
