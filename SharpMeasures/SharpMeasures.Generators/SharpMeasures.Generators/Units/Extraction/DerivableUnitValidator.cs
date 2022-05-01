namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Diagnostics.DerivableUnits;

internal class DerivableUnitValidator : IValidator<DerivableUnitParameters>
{
    public static DerivableUnitValidator Validator { get; } = new();

    private DerivableUnitValidator() { }

    public ExtractionValidity Check(AttributeData attributeData, DerivableUnitParameters parameters)
    {
        if (CheckExpressionValidity(attributeData, parameters) is ExtractionValidity { IsInvalid: true } invalidExpression)
        {
            return invalidExpression;
        }

        if (CheckSignatureValidity(attributeData, parameters) is ExtractionValidity { IsInvalid: true } invalidSignature)
        {
            return invalidSignature;
        }

        return ExtractionValidity.Valid;
    }

    private static ExtractionValidity CheckExpressionValidity(AttributeData attributeData, DerivableUnitParameters parameters)
    {
        if (string.IsNullOrEmpty(parameters.Expression))
        {
            return ExtractionValidity.Invalid(CreateInvalidExpressionDiagnostics(attributeData));
        }

        return ExtractionValidity.Valid;
    }

    private static ExtractionValidity CheckSignatureValidity(AttributeData attributeData, DerivableUnitParameters parameters)
    {
        if (parameters.ParsingData.EmptySignature)
        {
            return ExtractionValidity.Invalid(CreateEmptySignatureDiagnostics(attributeData));
        }

        if (parameters.ParsingData.InvalidIndex != -1)
        {
            return ExtractionValidity.Invalid(CreateTypeNotUnitDiagnostics(attributeData, parameters));
        }

        return ExtractionValidity.Valid;
    }

    private static Diagnostic? CreateInvalidExpressionDiagnostics(AttributeData attributeData)
    {
        int expressionArgumentIndex = DerivableUnitParser.ExpressionIndex(attributeData);

        if (attributeData.GetArgumentSyntax(expressionArgumentIndex)?.GetFirstChildOfKind<LiteralExpressionSyntax>(SyntaxKind.StringLiteralExpression)
            is LiteralExpressionSyntax expressionSyntax)
        {
            return InvalidUnitDerivationExpressionDiagnostics.Create(expressionSyntax);
        }

        return null;
    }

    private static Diagnostic? CreateEmptySignatureDiagnostics(AttributeData attributeData)
    {
        int signatureArgumentIndex = DerivableUnitParser.SignatureIndex(attributeData);

        if (attributeData.GetArgumentSyntax(signatureArgumentIndex)?.GetFirstChildOfKind<InitializerExpressionSyntax>(SyntaxKind.ArrayInitializerExpression)
            is InitializerExpressionSyntax initializerSyntax)
        {
            return EmptyUnitDerivationSignatureDiagnostics.Create(initializerSyntax);
        }

        return null;
    }

    private static Diagnostic? CreateTypeNotUnitDiagnostics(AttributeData attributeData, DerivableUnitParameters parameters)
    {
        if (parameters.ParsingData.InvalidIndex < 0 || parameters.ParsingData.InvalidIndex >= parameters.Signature.Count)
        {
            return null;
        }

        INamedTypeSymbol invalidSymbol = parameters.Signature[parameters.ParsingData.InvalidIndex];

        if (invalidSymbol.GetAttributeOfType<GeneratedUnitAttribute>() is null)
        {
            int signatureArgumentIndex = DerivableUnitParser.SignatureIndex(attributeData);

            if (attributeData.GetArgumentSyntax(signatureArgumentIndex)?.GetFirstChildOfKind<InitializerExpressionSyntax>(SyntaxKind.ArrayInitializerExpression)
                is InitializerExpressionSyntax arraySyntax)
            {
                if (arraySyntax.Expressions[parameters.ParsingData.InvalidIndex] is TypeOfExpressionSyntax arrayTypeofSyntax)
                {
                    return TypeNotUnitDiagnostics.Create(arrayTypeofSyntax);
                }

                return null;
            }

            if (attributeData.GetArgumentSyntax(signatureArgumentIndex + parameters.ParsingData.InvalidIndex)?.GetFirstChildOfType<TypeOfExpressionSyntax>()
                is TypeOfExpressionSyntax paramsTypeofSyntax)
            {
                return TypeNotUnitDiagnostics.Create(paramsTypeofSyntax);
            }
        }

        return null;
    }
}
