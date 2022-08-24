namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

internal class VectorConstantValidationDiagnostics : QuantityConstantValidationDiagnostics<VectorConstantDefinition, VectorConstantLocations>, IVectorConstantValidationDiagnostics
{
    new public static VectorConstantValidationDiagnostics Instance { get; } = new();

    private VectorConstantValidationDiagnostics() { }

    public Diagnostic InvalidConstantDimensionality(IVectorConstantValidationContext context, VectorConstantDefinition definition)
    {
        if (definition.Locations.ExplicitlySetValue)
        {
            return DiagnosticConstruction.VectorConstantInvalidDimension(definition.Locations.ValueCollection?.AsRoslynLocation(), context.Dimension, definition.Value.Count, context.Type.Name);
        }

        return DiagnosticConstruction.VectorConstantInvalidDimension(definition.Locations.AttributeName.AsRoslynLocation(), context.Dimension, definition.Value.Count, context.Type.Name);
    }
}
