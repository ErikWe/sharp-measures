namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

internal class SharpMeasuresUnitResolutionDiagnostics : ISharpMeasuresUnitResolutionDiagnostics
{
    public static SharpMeasuresUnitResolutionDiagnostics Instance { get; } = new();

    private SharpMeasuresUnitResolutionDiagnostics() { }

    public Diagnostic TypeAlreadyUnit(ISharpMeasuresUnitResolutionContext context, UnresolvedSharpMeasuresUnitDefinition definition)
    {
        return DiagnosticConstruction.UnitTypeAlreadyDefinedAsUnit(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic QuantityNotScalar(ISharpMeasuresUnitResolutionContext context, UnresolvedSharpMeasuresUnitDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Quantity?.AsRoslynLocation(), definition.Quantity.Name);
    }

    public Diagnostic QuantityBiased(ISharpMeasuresUnitResolutionContext context, UnresolvedSharpMeasuresUnitDefinition definition)
    {
        return DiagnosticConstruction.TypeNotUnbiasedScalar(definition.Locations.Quantity?.AsRoslynLocation(), definition.Quantity.Name);
    }
}
