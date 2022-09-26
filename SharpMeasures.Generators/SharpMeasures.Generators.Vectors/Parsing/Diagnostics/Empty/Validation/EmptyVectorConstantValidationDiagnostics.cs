namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

internal sealed class EmptyVectorConstantValidationDiagnostics : IVectorConstantValidationDiagnostics
{
    public static EmptyVectorConstantValidationDiagnostics Instance { get; } = new();

    private EmptyVectorConstantValidationDiagnostics() { }

    Diagnostic? IVectorConstantValidationDiagnostics.InvalidConstantDimensionality(IVectorConstantValidationContext context, VectorConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantValidationDiagnostics<VectorConstantDefinition, VectorConstantLocations>.UnrecognizedUnitInstanceName(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantValidationDiagnostics<VectorConstantDefinition, VectorConstantLocations>.DuplicateName(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantValidationDiagnostics<VectorConstantDefinition, VectorConstantLocations>.NameReservedByMultiples(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantValidationDiagnostics<VectorConstantDefinition, VectorConstantLocations>.DuplicateMultiples(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantValidationDiagnostics<VectorConstantDefinition, VectorConstantLocations>.MultiplesReservedByName(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantValidationDiagnostics<VectorConstantDefinition, VectorConstantLocations>.NameReservedByUnitInstancePluralForm(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantValidationDiagnostics<VectorConstantDefinition, VectorConstantLocations>.MultiplesReservedByUnitInstancePluralForm(IQuantityConstantValidationContext context, VectorConstantDefinition definition) => null;
}
