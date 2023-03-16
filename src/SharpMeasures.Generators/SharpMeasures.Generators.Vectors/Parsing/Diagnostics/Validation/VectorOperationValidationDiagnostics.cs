namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

internal sealed class VectorOperationValidationDiagnostics : IVectorOperationValidationDiagnostics
{
    public static VectorOperationValidationDiagnostics Instance { get; } = new();

    private VectorOperationValidationDiagnostics() { }

    public Diagnostic ResultNotQuantity(IVectorOperationValidationContext context, VectorOperationDefinition definition)
    {
        return DiagnosticConstruction.TypeNotQuantity(definition.Locations.Result?.AsRoslynLocation(), definition.Result.Name);
    }

    public Diagnostic OtherNotVector(IVectorOperationValidationContext context, VectorOperationDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.Other?.AsRoslynLocation(), definition.Other.Name);
    }

    public Diagnostic InvalidOperation(IVectorOperationValidationContext context, VectorOperationDefinition definition)
    {
        return DiagnosticConstruction.InvalidQuantityOperation(definition.Locations.AttributeName.AsRoslynLocation());
    }

    public Diagnostic NonOverlappingVectorDimensions(IVectorOperationValidationContext context, VectorOperationDefinition definition)
    {
        return DiagnosticConstruction.NonOverlappingVectorDimensions(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name, definition.Result.Name);
    }

    public Diagnostic ResultDoesNotSupportCrossProduct(IVectorOperationValidationContext context, VectorOperationDefinition definition)
    {
        return DiagnosticConstruction.VectorNotSupportingCrossMultiplication(definition.Locations.Result?.AsRoslynLocation(), definition.Result.Name);
    }

    public Diagnostic OtherDoesNotSupportCrossProduct(IVectorOperationValidationContext context, VectorOperationDefinition definition)
    {
        return DiagnosticConstruction.VectorNotSupportingCrossMultiplication(definition.Locations.Other?.AsRoslynLocation(), definition.Other.Name);
    }

    public Diagnostic ThisDoesNotSupportCrossProduct(IVectorOperationValidationContext context, VectorOperationDefinition definition)
    {
        return DiagnosticConstruction.VectorNotSupportingCrossMultiplication(definition.Locations.OperatorType?.AsRoslynLocation(), context.Type.Name);
    }
}
