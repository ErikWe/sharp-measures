﻿namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal class GeneratedUnitValidator : IValidator<GeneratedUnitDefinition>
{
    public static GeneratedUnitValidator Validator { get; } = new();

    private GeneratedUnitValidator() { }

    public ExtractionValidity Check(AttributeData attributeData, GeneratedUnitDefinition parameters)
    {
        if (CheckQuantityValidity(attributeData, parameters) is ExtractionValidity { IsInvalid: true } invalidQuantity)
        {
            return invalidQuantity;
        }

        return ExtractionValidity.Valid;
    }

    private static ExtractionValidity CheckQuantityValidity(AttributeData attributeData, GeneratedUnitDefinition parameters)
    {
        if (parameters.Quantity is null
            || GeneratedScalarQuantityParser.Parser.ParseFirst(parameters.Quantity) is not GeneratedScalarQuantityParameters quantityParameters)
        {
            return ExtractionValidity.Invalid(CreateTypeNotScalarQuantityDiagnostics(attributeData));
        }

        if (quantityParameters.Biased)
        {
            return ExtractionValidity.Invalid(CreateTypeNotUnbiasedDiagnostics(attributeData));
        }

        return ExtractionValidity.Valid;
    }

    private static Diagnostic? CreateTypeNotScalarQuantityDiagnostics(AttributeData attributeData)
    {
        int quantityArgumentIndex = GeneratedUnitParser.QuantityIndex(attributeData);

        if (attributeData.GetArgumentSyntax(quantityArgumentIndex) is not AttributeArgumentSyntax attributeArgumentSyntax)
        {
            return null;
        }

        if (attributeArgumentSyntax.GetFirstChildOfType<TypeOfExpressionSyntax>()
            is TypeOfExpressionSyntax typeofSyntax)
        {
            return TypeNotScalarQuantityDiagnostics.Create(typeofSyntax);
        }
        else if (attributeArgumentSyntax.GetFirstChildOfKind<LiteralExpressionSyntax>(SyntaxKind.NullLiteralExpression)
            is LiteralExpressionSyntax nullSyntax)
        {
            return TypeNotScalarQuantityDiagnostics.Create(nullSyntax);
        }

        return null;
    }

    private static Diagnostic? CreateTypeNotUnbiasedDiagnostics(AttributeData attributeData)
    {
        int quantityArgumentIndex = GeneratedUnitParser.QuantityIndex(attributeData);

        if (attributeData.GetArgumentSyntax(quantityArgumentIndex)?.GetFirstChildOfType<TypeOfExpressionSyntax>()
            is TypeOfExpressionSyntax typeofSyntax)
        {
            return TypeNotUnbiasedScalarQuantityDiagnostics.Create(typeofSyntax);
        }

        return null;
    }
}
