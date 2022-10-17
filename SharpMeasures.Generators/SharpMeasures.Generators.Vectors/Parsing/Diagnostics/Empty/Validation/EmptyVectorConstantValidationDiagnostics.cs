namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

internal sealed class EmptyVectorConstantValidationDiagnostics : IVectorConstantValidationDiagnostics
{
    public static EmptyVectorConstantValidationDiagnostics Instance { get; } = new();

    private EmptyVectorConstantValidationDiagnostics() { }

    public Diagnostic? InvalidValueDimensionality(IVectorConstantValidationContext context, VectorConstantDefinition definition) => null;
    public Diagnostic? InvalidExpressionsDimensionality(IVectorConstantValidationContext context, VectorConstantDefinition definition) => null;
    public Diagnostic? UnrecognizedUnitInstanceName(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => null;
    public Diagnostic? DuplicateName(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => null;
    public Diagnostic? NameReservedByMultiples(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => null;
    public Diagnostic? DuplicateMultiples(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => null;
    public Diagnostic? MultiplesReservedByName(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => null;
    public Diagnostic? NameReservedByUnitInstancePluralForm(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => null;
    public Diagnostic? MultiplesReservedByUnitInstancePluralForm(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => null;
}
