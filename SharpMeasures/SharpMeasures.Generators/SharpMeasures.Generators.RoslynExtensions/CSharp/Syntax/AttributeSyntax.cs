namespace Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;

public static partial class Extensions
{
    public static bool IsDescribedAttributeType<TAttribute>(this AttributeSyntax attributeSyntax, SemanticModel semanticModel)
    {
        return IsDescribedAttributeType(attributeSyntax, typeof(TAttribute), semanticModel);
    }

    public static bool IsDescribedAttributeType(this AttributeSyntax attributeSyntax, Type attributeType, SemanticModel semanticModel)
    {
        if (attributeType is null)
        {
            throw new ArgumentNullException(nameof(attributeType));
        }

        return IsDescribedAttributeName(attributeSyntax, attributeType.FullName, semanticModel);
    }

    public static bool IsDescribedAttributeName(this AttributeSyntax attributeSyntax, string attributeName, SemanticModel semanticModel)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        if (attributeName is null)
        {
            return false;
        }

        if (attributeName.EndsWith(attributeSyntax.Name.ToString(), StringComparison.Ordinal)
            || attributeName.EndsWith($"{attributeSyntax.Name}Attribute", StringComparison.Ordinal))
        {
            if (semanticModel.GetSymbolInfo(attributeSyntax.Name).Symbol is IMethodSymbol attributeConstructor
                && attributeConstructor.ContainingType.ToDisplayString() == attributeName)
            {
                return true;
            }
        }
        else
        {
            return semanticModel.GetAliasInfo(attributeSyntax.Name) is IAliasSymbol { Target: INamedTypeSymbol aliasSymbol }
                && aliasSymbol.ToDisplayString() == attributeName;
        }

        return false;
    }

    public static bool IsAnyDescribedAttributeType(this AttributeSyntax attributeSyntax, IReadOnlyCollection<Type> attributeTypes, SemanticModel semanticModel)
    {
        if (attributeTypes is null)
        {
            throw new ArgumentNullException(nameof(attributeTypes));
        }

        string[] attributeNames = new string[attributeTypes.Count];

        int index = 0;
        foreach (Type attributeType in attributeTypes)
        {
            attributeNames[index++] = attributeType.Name;
        }

        return IsAnyDescribedAttributeName(attributeSyntax, attributeNames, semanticModel);
    }

    public static bool IsAnyDescribedAttributeType(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, params Type[] attributeTypes)
    {
        return IsAnyDescribedAttributeType(attributeSyntax, attributeTypes, semanticModel);
    }

    public static bool IsAnyDescribedAttributeName(this AttributeSyntax attributeSyntax, IReadOnlyCollection<string> attributeNames, SemanticModel semanticModel)
    {
        if (attributeNames is null)
        {
            throw new ArgumentNullException(nameof(attributeNames));
        }

        foreach (string attributeName in attributeNames)
        {
            if (IsDescribedAttributeName(attributeSyntax, attributeName, semanticModel))
            {
                return true;
            }
        }

        return false;
    }

    public static bool AnyMatchingName(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, params string[] attributeNames)
    {
        return IsAnyDescribedAttributeName(attributeSyntax, attributeNames, semanticModel);
    }
}
