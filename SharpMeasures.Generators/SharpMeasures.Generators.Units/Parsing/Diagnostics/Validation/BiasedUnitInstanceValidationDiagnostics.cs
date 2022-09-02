namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;

internal class BiasedUnitInstanceValidationDiagnostics : AModifiedUnitInstanceValidationDiagnostics<BiasedUnitInstanceDefinition>, IBiasedUnitInstanceValidationDiagnostics
{
    public static BiasedUnitInstanceValidationDiagnostics Instance { get; } = new();

    private BiasedUnitInstanceValidationDiagnostics() { }

    public Diagnostic UnitNotIncludingBiasTerm(IBiasedUnitInstanceValidationContext context, BiasedUnitInstanceDefinition definition)
    {
        return DiagnosticConstruction.BiasedUnitDefinedButUnitNotBiased(definition.Locations.AttributeName.AsRoslynLocation(), definition.Name, context.Type.Name);
    }
}
