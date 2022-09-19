namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

internal sealed class EmptyDerivedQuantityProcessingDiagnostics : IDerivedQuantityProcessingDiagnostics
{
    public static EmptyDerivedQuantityProcessingDiagnostics Instance { get; } = new();

    private EmptyDerivedQuantityProcessingDiagnostics() { }

    Diagnostic? IDerivedQuantityProcessingDiagnostics.EmptyExpression(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition) => null;
    Diagnostic? IDerivedQuantityProcessingDiagnostics.EmptySignature(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition) => null;
    Diagnostic? IDerivedQuantityProcessingDiagnostics.ExpressionContainsConstant(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition) => null;
    Diagnostic? IDerivedQuantityProcessingDiagnostics.ExpressionDoesNotIncludeQuantity(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition, int index) => null;
    Diagnostic? IDerivedQuantityProcessingDiagnostics.ExpressionNotCompatibleWithOperators(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition) => null;
    Diagnostic? IDerivedQuantityProcessingDiagnostics.MalformedExpression(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition) => null;
    Diagnostic? IDerivedQuantityProcessingDiagnostics.NullExpression(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition) => null;
    Diagnostic? IDerivedQuantityProcessingDiagnostics.NullSignature(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition) => null;
    Diagnostic? IDerivedQuantityProcessingDiagnostics.NullSignatureElement(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition, int index) => null;
    Diagnostic? IDerivedQuantityProcessingDiagnostics.UnmatchedExpressionQuantity(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition, int requestedIndex) => null;
    Diagnostic? IDerivedQuantityProcessingDiagnostics.UnrecognizedOperatorImplementation(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition) => null;
}
