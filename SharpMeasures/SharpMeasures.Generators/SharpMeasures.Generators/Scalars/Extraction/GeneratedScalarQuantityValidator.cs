namespace SharpMeasures.Generators.Scalars.Extraction;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal class GeneratedScalarQuantityValidator : IValidator<GeneratedScalarQuantityParameters>
{
    public static GeneratedScalarQuantityValidator Validator { get; } = new();

    private GeneratedScalarQuantityValidator() { }

    public ExtractionValidity Check(AttributeData attributeData, GeneratedScalarQuantityParameters parameters)
    {
        if (CheckUnitValidity(attributeData, parameters) is ExtractionValidity { IsInvalid: true } invalidUnit)
        {
            return invalidUnit;
        }

        return ExtractionValidity.Valid;
    }

    private static ExtractionValidity CheckUnitValidity(AttributeData attributeData, GeneratedScalarQuantityParameters parameters)
    {
        if (parameters.Unit is null
            || GeneratedUnitParser.Parser.ParseFirst(parameters.Unit) is not GeneratedUnitParameters unitParameters)
        {
            return ExtractionValidity.Invalid(CreateTypeIsNotScalarQuantityDiagnostics(attributeData));
        }

        if (unitParameters.Biased)
        {
            return ExtractionValidity.Invalid(CreateTypeIsNotUnbiasedDiagnostics(attributeData));
        }

        return ExtractionValidity.Valid;
    }

    private static Diagnostic? CreateTypeIsNotScalarQuantityDiagnostics(AttributeData attributeData)
    {
        int quantityArgumentIndex = GeneratedUnitParser.QuantityIndex(attributeData);

        if (attributeData.GetArgumentSyntax(quantityArgumentIndex)?.GetFirstChildOfType<TypeOfExpressionSyntax>()
            is TypeOfExpressionSyntax typeofSyntax)
        {
            return TypeNotScalarQuantityDiagnostics.Create(typeofSyntax);
        }

        return null;
    }

    private static Diagnostic? CreateTypeIsNotUnbiasedDiagnostics(AttributeData attributeData)
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
