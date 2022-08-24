namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.BiasedUnit;

internal class BiasedUnitValidationDiagnostics : ADependantUnitValidationDiagnostics<BiasedUnitDefinition, BiasedUnitLocations>, IBiasedUnitValidationDiagnostics
{
    public static BiasedUnitValidationDiagnostics Instance { get; } = new();

    private BiasedUnitValidationDiagnostics() { }

    public Diagnostic UnitNotIncludingBiasTerm(IBiasedUnitValidationContext context, BiasedUnitDefinition definition)
    {
        return DiagnosticConstruction.BiasedUnitDefinedButUnitNotBiased(definition.Locations.AttributeName.AsRoslynLocation(), definition.Name, context.Type.Name);
    }
}
