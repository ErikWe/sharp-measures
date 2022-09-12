namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

internal class SharpMeasuresUnitValidationDiagnostics : ISharpMeasuresUnitValidationDiagnostics
{
    public static SharpMeasuresUnitValidationDiagnostics Instance { get; } = new();

    private SharpMeasuresUnitValidationDiagnostics() { }

    public Diagnostic QuantityNotScalar(ISharpMeasuresUnitValidationContext context, SharpMeasuresUnitDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Quantity?.AsRoslynLocation(), definition.Quantity.Name);
    }

    public Diagnostic QuantityBiased(ISharpMeasuresUnitValidationContext context, SharpMeasuresUnitDefinition definition)
    {
        return DiagnosticConstruction.TypeNotUnbiasedScalar(definition.Locations.Quantity?.AsRoslynLocation(), definition.Quantity.Name);
    }
}
