namespace SharpMeasures.Generators.Units.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.BiasedUnit;

internal class BiasedUnitDiagnostics : ADependantUnitDiagnostics<RawBiasedUnitDefinition>, IBiasedUnitProcessingDiagnostics
{
    public static BiasedUnitDiagnostics Instance { get; } = new();

    private BiasedUnitDiagnostics() { }

    public Diagnostic UnitNotIncludingBiasTerm(IBiasedUnitProcessingContext context, RawBiasedUnitDefinition definition)
    {
        return DiagnosticConstruction.BiasedUnitDefinedButUnitNotBiased(definition.Locations.Attribute.AsRoslynLocation(), definition.Name!, context.Type.Name);
    }

    public Diagnostic NullExpression(IBiasedUnitProcessingContext context, RawBiasedUnitDefinition definition)
    {
        return DiagnosticConstruction.NullBiasedUnitExpression(definition.Locations.Expression?.AsRoslynLocation());
    }

    public Diagnostic EmptyExpression(IBiasedUnitProcessingContext context, RawBiasedUnitDefinition definition) => NullExpression(context, definition);
}
