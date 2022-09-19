namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

public class DerivedQuantityProcessingDiagnostics : IDerivedQuantityProcessingDiagnostics
{
    public static DerivedQuantityProcessingDiagnostics Instance { get; } = new();

    private DerivedQuantityProcessingDiagnostics() { }

    public Diagnostic NullExpression(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return DiagnosticConstruction.NullDerivationExpression(definition.Locations.Expression?.AsRoslynLocation());
    }

    public Diagnostic EmptyExpression(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition) => NullExpression(context, definition);

    public Diagnostic UnmatchedExpressionQuantity(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition, int requestedIndex)
    {
        return DiagnosticConstruction.UnmatchedDerivationExpressionQuantity(definition.Locations.Expression?.AsRoslynLocation(), requestedIndex);
    }

    public Diagnostic ExpressionDoesNotIncludeQuantity(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.ExpressionDoesNotIncludeQuantity(definition.Locations.Expression?.AsRoslynLocation(), index);
    }

    public Diagnostic MalformedExpression(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return DiagnosticConstruction.MalformedDerivationExpression(definition.Locations.Expression?.AsRoslynLocation());
    }

    public Diagnostic ExpressionContainsConstant(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return DiagnosticConstruction.DerivationExpressionContainsConstant(definition.Locations.Expression?.AsRoslynLocation());
    }

    public Diagnostic NullSignature(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return DiagnosticConstruction.NullDerivationSignature(definition.Locations.SignatureCollection?.AsRoslynLocation());
    }

    public Diagnostic EmptySignature(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return DiagnosticConstruction.EmptyQuantityDerivationSignature(definition.Locations.SignatureCollection?.AsRoslynLocation());
    }

    public Diagnostic NullSignatureElement(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition, int index)
    {
        return DiagnosticConstruction.NullTypeNotQuantity(definition.Locations.SignatureElements[index].AsRoslynLocation());
    }

    public Diagnostic UnrecognizedOperatorImplementation(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedEnumValue(definition.Locations.OperatorImplementation?.AsRoslynLocation(), definition.OperatorImplementation);
    }

    public Diagnostic ExpressionNotCompatibleWithOperators(IDerivedQuantityProcessingContext context, RawDerivedQuantityDefinition definition)
    {
        return DiagnosticConstruction.DerivationOperatorsIncompatibleExpression(definition.Locations.OperatorImplementation?.AsRoslynLocation());
    }
}
