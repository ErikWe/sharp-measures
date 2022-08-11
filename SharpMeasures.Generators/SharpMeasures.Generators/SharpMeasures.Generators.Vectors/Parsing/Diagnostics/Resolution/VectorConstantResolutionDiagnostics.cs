namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Resolution;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

internal class VectorConstantResolutionDiagnostics : QuantityConstantResolutionDiagnostics<UnresolvedVectorConstantDefinition, VectorConstantLocations>, IVectorConstantResolutionDiagnostics
{
    new public static VectorConstantResolutionDiagnostics Instance { get; } = new();

    private VectorConstantResolutionDiagnostics() { }

    public Diagnostic InvalidConstantDimensionality(IVectorConstantResolutionContext context, UnresolvedVectorConstantDefinition definition)
    {
        if (definition.Locations.ExplicitlySetValue)
        {
            return DiagnosticConstruction.VectorConstantInvalidDimension(definition.Locations.ValueCollection?.AsRoslynLocation(), context.Dimension, definition.Value.Count, context.Type.Name);
        }

        return DiagnosticConstruction.VectorConstantInvalidDimension(definition.Locations.AttributeName.AsRoslynLocation(), context.Dimension, definition.Value.Count, context.Type.Name);
    }
}
