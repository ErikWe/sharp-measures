namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal class GeneratedUnitValidator : IValidator<GeneratedUnitParameters>
{
    public static GeneratedUnitValidator Validator { get; } = new();

    private GeneratedUnitValidator() { }

    public ExtractionValidity Check(AttributeData attributeData, GeneratedUnitParameters parameters)
    {
        if (CheckQuantityValidity(attributeData, parameters) is ExtractionValidity { IsInvalid: true } invalidQuantity)
        {
            return invalidQuantity;
        }

        return ExtractionValidity.Valid;
    }

    private static ExtractionValidity CheckQuantityValidity(AttributeData attributeData, GeneratedUnitParameters parameters)
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

        if (attributeData.GetArgumentSyntax(quantityArgumentIndex)?.GetFirstChildOfType<TypeOfExpressionSyntax>()
            is TypeOfExpressionSyntax typeofSyntax)
        {
            return TypeNotScalarQuantityDiagnostics.Create(typeofSyntax);
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
