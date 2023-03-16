namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

internal sealed class EmptyVectorOperationValidationDiagnostics : IVectorOperationValidationDiagnostics
{
    public static EmptyVectorOperationValidationDiagnostics Instance { get; } = new();

    private EmptyVectorOperationValidationDiagnostics() { }

    public Diagnostic? ResultNotQuantity(IVectorOperationValidationContext context, VectorOperationDefinition definition) => null;
    public Diagnostic? OtherNotVector(IVectorOperationValidationContext context, VectorOperationDefinition definition) => null;
    public Diagnostic? InvalidOperation(IVectorOperationValidationContext context, VectorOperationDefinition definition) => null;
    public Diagnostic? NonOverlappingVectorDimensions(IVectorOperationValidationContext context, VectorOperationDefinition definition) => null;
    public Diagnostic? ResultDoesNotSupportCrossProduct(IVectorOperationValidationContext context, VectorOperationDefinition definition) => null;
    public Diagnostic? OtherDoesNotSupportCrossProduct(IVectorOperationValidationContext context, VectorOperationDefinition definition) => null;
    public Diagnostic? ThisDoesNotSupportCrossProduct(IVectorOperationValidationContext context, VectorOperationDefinition definition) => null;
}
