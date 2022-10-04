namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;

public sealed class EmptyQuantityOperationValidationDiagnostics : IQuantityOperationValidationDiagnostics
{
    public static EmptyQuantityOperationValidationDiagnostics Instance { get; } = new();

    private EmptyQuantityOperationValidationDiagnostics() { }

    public Diagnostic? ResultNotQuantity(IQuantityOperationValidationContext context, QuantityOperationDefinition definition) => null;
    public Diagnostic? OtherNotQuantity(IQuantityOperationValidationContext context, QuantityOperationDefinition definition) => null;
    public Diagnostic? InvalidOperation(IQuantityOperationValidationContext context, QuantityOperationDefinition definition) => null;
    public Diagnostic? NonOverlappingVectorDimensions(IQuantityOperationValidationContext context, QuantityOperationDefinition definition) => null;
    public Diagnostic? MirrorNotSupported(IQuantityOperationValidationContext context, QuantityOperationDefinition definition) => null;
}
