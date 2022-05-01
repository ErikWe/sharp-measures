namespace Microsoft.CodeAnalysis.CSharp.Syntax;

internal static partial class Extensions
{
    public static bool HasModifierOfKind(this MemberDeclarationSyntax declarationSyntax, SyntaxKind token)
    {
        foreach (SyntaxToken candidateToken in declarationSyntax.Modifiers)
        {
            if (candidateToken.IsKind(token))
            {
                return true;
            }
        }

        return false;
    }

    public static bool HasAttributeOfType<TAttribute>(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel)
        => declarationSyntax.HasAttributeWithName(semanticModel, typeof(TAttribute).FullName);

    public static bool HasAttributeWithName(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, string attributeName)
    {
        foreach (AttributeListSyntax attributeList in declarationSyntax.AttributeLists)
        {
            foreach (AttributeSyntax attribute in attributeList.Attributes)
            {
                if (getAttributeSymbol(attribute) is IMethodSymbol attributeConstructorSymbol
                    && isCorrectAttribute(attributeConstructorSymbol.ContainingType))
                {
                    return true;
                }
            }
        }

        return false;

        ISymbol? getAttributeSymbol(AttributeSyntax syntax) => semanticModel.GetSymbolInfo(syntax).Symbol;
        bool isCorrectAttribute(INamedTypeSymbol attributeSymbol) => attributeSymbol.ToDisplayString() == attributeName;
    }
}
