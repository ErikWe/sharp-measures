namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.BiasedUnit;

internal class BiasedUnitResolutionDiagnostics : ADependantUnitResolutionDiagnostics<RawBiasedUnitDefinition, BiasedUnitLocations>, IBiasedUnitResolutionDiagnostics
{
    public static BiasedUnitResolutionDiagnostics Instance { get; } = new();

    private BiasedUnitResolutionDiagnostics() { }

    public Diagnostic UnitNotIncludingBiasTerm(IBiasedUnitResolutionContext context, RawBiasedUnitDefinition definition)
    {
        return DiagnosticConstruction.BiasedUnitDefinedButUnitNotBiased(definition.Locations.AttributeName.AsRoslynLocation(), definition.Name, context.Type.Name);
    }
}
