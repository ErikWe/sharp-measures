namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

internal sealed class VectorConstantValidationDiagnostics : IVectorConstantValidationDiagnostics
{
    public static VectorConstantValidationDiagnostics Instance { get; } = new();

    private static QuantityConstantValidationDiagnostics<VectorConstantDefinition, VectorConstantLocations> QuantityInstance { get; } = QuantityConstantValidationDiagnostics<VectorConstantDefinition, VectorConstantLocations>.Instance;

    private VectorConstantValidationDiagnostics() { }

    public Diagnostic? UnrecognizedUnitInstanceName(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => QuantityInstance.UnrecognizedUnitInstanceName(context, definition);
    public Diagnostic? DuplicateName(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => QuantityInstance.DuplicateName(context, definition);
    public Diagnostic? NameReservedByMultiples(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => QuantityInstance.NameReservedByMultiples(context, definition);
    public Diagnostic? DuplicateMultiples(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => QuantityInstance.DuplicateMultiples(context, definition);
    public Diagnostic? MultiplesReservedByName(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => QuantityInstance.MultiplesReservedByName(context, definition);
    public Diagnostic? NameReservedByUnitInstancePluralForm(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => QuantityInstance.NameReservedByUnitInstancePluralForm(context, definition);
    public Diagnostic? MultiplesReservedByUnitInstancePluralForm(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => QuantityInstance.MultiplesReservedByUnitInstancePluralForm(context, definition);

    public Diagnostic InvalidValueDimensionality(IVectorConstantValidationContext context, VectorConstantDefinition definition)
    {
        if (definition.Locations.ExplicitlySetValue)
        {
            return DiagnosticConstruction.VectorConstantInvalidDimension(definition.Locations.ValueCollection?.AsRoslynLocation(), context.Dimension, definition.Value?.Count ?? 0, context.Type.Name);
        }

        return DiagnosticConstruction.VectorConstantInvalidDimension(definition.Locations.AttributeName.AsRoslynLocation(), context.Dimension, 0, context.Type.Name);
    }

    public Diagnostic InvalidExpressionsDimensionality(IVectorConstantValidationContext context, VectorConstantDefinition definition)
    {
        if (definition.Locations.ExplicitlySetExpressions)
        {
            return DiagnosticConstruction.VectorConstantInvalidDimension(definition.Locations.ExpressionCollection?.AsRoslynLocation(), context.Dimension, definition.Expressions?.Count ?? 0, context.Type.Name);
        }

        return DiagnosticConstruction.VectorConstantInvalidDimension(definition.Locations.AttributeName.AsRoslynLocation(), context.Dimension, 0, context.Type.Name);
    }
}
