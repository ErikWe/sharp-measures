namespace SharpMeasures.Generators.Units.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Refinement.SharpMeasuresUnit;

internal class SharpMeasuresUnitDiagnostics : ISharpMeasuresUnitProcessingDiagnostics, ISharpMeasuresUnitRefinementDiagnostics
{
    public static SharpMeasuresUnitDiagnostics Instance { get; } = new();

    private SharpMeasuresUnitDiagnostics() { }

    public Diagnostic NullQuantity(IProcessingContext context, RawSharpMeasuresUnitDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Quantity?.AsRoslynLocation());
    }

    public Diagnostic QuantityNotScalar(ISharpMeasuresUnitRefinementContext context, SharpMeasuresUnitDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Quantity?.AsRoslynLocation(), definition.Quantity.Name);
    }

    public Diagnostic QuantityBiased(ISharpMeasuresUnitRefinementContext context, SharpMeasuresUnitDefinition definition)
    {
        return DiagnosticConstruction.ScalarBiased(definition.Locations.Quantity?.AsRoslynLocation(), definition.Quantity.Name);
    }
}
