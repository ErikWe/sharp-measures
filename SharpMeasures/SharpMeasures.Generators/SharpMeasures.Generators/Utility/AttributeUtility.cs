namespace SharpMeasures.Generators.Utility;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class AttributeUtility
{
    public static AttributeData? ExtractAttributeData(INamedTypeSymbol typeSymbol, AttributeSyntax attributeSyntax)
    {
        SyntaxReference reference = attributeSyntax.GetReference();
        foreach (AttributeData attributeData in typeSymbol.GetAttributes())
        {
            if (reference.SyntaxTree == attributeData.ApplicationSyntaxReference?.SyntaxTree
                && reference.Span == attributeData.ApplicationSyntaxReference?.Span)
            {
                return attributeData;
            }
        }

        return null;
    }
}
