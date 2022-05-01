namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics.UnitDefinitions;

internal abstract class UnitDefinitionValidator<TParameters> : IValidator<TParameters> where TParameters : IUnitDefinitionParameters
{
    public virtual ExtractionValidity Check(AttributeData attributeData, TParameters parameters)
    {
        if (string.IsNullOrEmpty(parameters.Name))
        {
            return ExtractionValidity.Invalid(CreateInvalidUnitNameDiagnostics(attributeData));
        }

        if (string.IsNullOrEmpty(parameters.Plural))
        {
            return ExtractionValidity.Invalid(CreateInvalidUnitPluralFormDiagnostics(attributeData));
        }

        return ExtractionValidity.Valid;
    }

    private Diagnostic? CreateInvalidUnitNameDiagnostics(AttributeData attributeData)
    {
        if (NameArgumentSyntax(attributeData)?.GetFirstChildOfKind<LiteralExpressionSyntax>(SyntaxKind.StringLiteralExpression)
            is LiteralExpressionSyntax expressionSyntax)
        {
            return InvalidUnitNameDiagnostics.Create(expressionSyntax);
        }

        return null;
    }

    private Diagnostic? CreateInvalidUnitPluralFormDiagnostics(AttributeData attributeData)
    {
        if (PluralArgumentSyntax(attributeData)?.GetFirstChildOfKind<LiteralExpressionSyntax>(SyntaxKind.StringLiteralExpression)
            is LiteralExpressionSyntax expressionSyntax)
        {
            return InvalidUnitPluralFormDiagnostics.Create(expressionSyntax);
        }

        return null;
    }

    protected AttributeArgumentSyntax? NameArgumentSyntax(AttributeData attributeData)
        => attributeData.GetArgumentSyntax(NameArgumentIndex(attributeData));

    protected AttributeArgumentSyntax? PluralArgumentSyntax(AttributeData attributeData)
        => attributeData.GetArgumentSyntax(PluralArgumentIndex(attributeData));

    protected abstract int NameArgumentIndex(AttributeData attributeData);
    protected abstract int PluralArgumentIndex(AttributeData attributeData);
}
