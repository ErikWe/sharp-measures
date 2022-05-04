namespace Microsoft.CodeAnalysis.CSharp.Syntax;

using System;

public static partial class Extensions
{
    public static bool HasModifierOfKind(this MemberDeclarationSyntax declarationSyntax, SyntaxKind token)
    {
        if (declarationSyntax is null)
        {
            throw new ArgumentNullException(nameof(declarationSyntax));
        }

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
        => declarationSyntax.HasAttributeOfType(semanticModel, typeof(TAttribute));

    public static bool HasAttributeOfType(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, Type attributeType)
    {
        if (attributeType is null)
        {
            throw new ArgumentNullException(nameof(attributeType));
        }

        return declarationSyntax.HasAttributeWithName(semanticModel, attributeType.FullName);
    }

    public static bool HasAttributeWithName(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, string attributeName)
    {
        if (declarationSyntax is null)
        {
            throw new ArgumentNullException(nameof(declarationSyntax));
        }

        if (semanticModel is null)
        {
            throw new ArgumentNullException(nameof(semanticModel));
        }

        if (attributeName is null)
        {
            return false;
        }

        foreach (AttributeListSyntax attributeList in declarationSyntax.AttributeLists)
        {
            foreach (AttributeSyntax attribute in attributeList.Attributes)
            {
                if (isCorrectAttribute(attribute) || isAliasOfCorrectAttribute(attribute))
                {
                    return true;
                }
            }
        }

        return false;

        bool isCorrectAttribute(AttributeSyntax syntax)
            => (attributeName.EndsWith(syntax.Name.ToString(), StringComparison.Ordinal)
                    || attributeName.EndsWith(syntax.Name.ToString() + "Attribute", StringComparison.Ordinal))
                && semanticModel.GetSymbolInfo(syntax.Name).Symbol is IMethodSymbol attributeConstructorSymbol
                && isSymbolCorrectAttribute(attributeConstructorSymbol.ContainingType);

        bool isAliasOfCorrectAttribute(AttributeSyntax syntax)
            => semanticModel.GetAliasInfo(syntax.Name) is IAliasSymbol { Target: INamedTypeSymbol aliasSymbol }
                && isSymbolCorrectAttribute(aliasSymbol);

        bool isSymbolCorrectAttribute(INamedTypeSymbol attributeSymbol) => attributeSymbol.ToDisplayString() == attributeName;
    }
}
