namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

public sealed class EmptyDerivedQuantityProcessingDiagnostics : IDerivedQuantityProcessingDiagnostics
{
    public static EmptyDerivedQuantityProcessingDiagnostics Instance { get; } = new();

    private EmptyDerivedQuantityProcessingDiagnostics() { }

    public Diagnostic? EmptyExpression(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition) => null;
    public Diagnostic? EmptySignature(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition) => null;
    public Diagnostic? ExpressionContainsConstant(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition) => null;
    public Diagnostic? ExpressionDoesNotIncludeQuantity(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition, int index) => null;
    public Diagnostic? ExpressionNotCompatibleWithOperators(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition) => null;
    public Diagnostic? MalformedExpression(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition) => null;
    public Diagnostic? NullExpression(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition) => null;
    public Diagnostic? NullSignature(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition) => null;
    public Diagnostic? NullSignatureElement(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition, int index) => null;
    public Diagnostic? UnmatchedExpressionQuantity(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition, int requestedIndex) => null;
    public Diagnostic? UnrecognizedOperatorImplementation(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition) => null;
}
