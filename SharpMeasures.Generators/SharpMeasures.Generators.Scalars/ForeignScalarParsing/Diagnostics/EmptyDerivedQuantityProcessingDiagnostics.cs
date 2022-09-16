namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

internal class EmptyDerivedQuantityProcessingDiagnostics : IDerivedQuantityProcessingDiagnostics
{
    public static EmptyDerivedQuantityProcessingDiagnostics Instance { get; } = new();

    Diagnostic? IDerivedQuantityProcessingDiagnostics.EmptyExpression(IProcessingContext context, RawDerivedQuantityDefinition definition) => null;
    Diagnostic? IDerivedQuantityProcessingDiagnostics.EmptySignature(IProcessingContext context, RawDerivedQuantityDefinition definition) => null;
    Diagnostic? IDerivedQuantityProcessingDiagnostics.ExpressionDoesNotIncludeQuantity(IProcessingContext context, RawDerivedQuantityDefinition definition, int index) => null;
    Diagnostic? IDerivedQuantityProcessingDiagnostics.ExpressionNotCompatibleWithOperators(IProcessingContext context, RawDerivedQuantityDefinition definition) => null;
    Diagnostic? IDerivedQuantityProcessingDiagnostics.NullExpression(IProcessingContext context, RawDerivedQuantityDefinition definition) => null;
    Diagnostic? IDerivedQuantityProcessingDiagnostics.NullSignature(IProcessingContext context, RawDerivedQuantityDefinition definition) => null;
    Diagnostic? IDerivedQuantityProcessingDiagnostics.NullSignatureElement(IProcessingContext context, RawDerivedQuantityDefinition definition, int index) => null;
    Diagnostic? IDerivedQuantityProcessingDiagnostics.UnmatchedExpressionQuantity(IProcessingContext context, RawDerivedQuantityDefinition definition, int requestedIndex) => null;
    Diagnostic? IDerivedQuantityProcessingDiagnostics.UnrecognizedOperatorImplementation(IProcessingContext context, RawDerivedQuantityDefinition definition) => null;
}
