namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

internal class SharpMeasuresUnitValidationDiagnostics : ISharpMeasuresUnitValidationDiagnostics
{
    public static SharpMeasuresUnitValidationDiagnostics Instance { get; } = new();

    private SharpMeasuresUnitValidationDiagnostics() { }

    public Diagnostic TypeAlreadyUnit(ISharpMeasuresUnitValidationContext context, SharpMeasuresUnitDefinition definition)
    {
        return DiagnosticConstruction.UnitTypeAlreadyDefinedAsUnit(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic QuantityNotScalar(ISharpMeasuresUnitValidationContext context, SharpMeasuresUnitDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Quantity?.AsRoslynLocation(), definition.Quantity.Name);
    }

    public Diagnostic QuantityBiased(ISharpMeasuresUnitValidationContext context, SharpMeasuresUnitDefinition definition)
    {
        return DiagnosticConstruction.TypeNotUnbiasedScalar(definition.Locations.Quantity?.AsRoslynLocation(), definition.Quantity.Name);
    }
}
