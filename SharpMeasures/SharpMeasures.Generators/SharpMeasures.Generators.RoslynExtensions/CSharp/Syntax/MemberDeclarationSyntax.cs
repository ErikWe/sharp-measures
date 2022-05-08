namespace Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;

public static partial class Extensions
{
    public static bool HasModifierOfKind(this MemberDeclarationSyntax declarationSyntax, SyntaxKind token)
    {
        if (declarationSyntax is null)
        {
            throw new ArgumentNullException(nameof(declarationSyntax));
        }

        return declarationSyntax.Modifiers.ContainsTokenOfKind(token);
    }

    public static AttributeSyntax? GetAttributeOfType<TAttribute>(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel)
    {
        return declarationSyntax.GetAttributeOfType(typeof(TAttribute), semanticModel);
    }

    public static AttributeSyntax? GetAttributeOfType(this MemberDeclarationSyntax declarationSyntax, Type attributeType, SemanticModel semanticModel)
    {
        if (attributeType is null)
        {
            throw new ArgumentNullException(nameof(attributeType));
        }

        return declarationSyntax.GetAttributeWithName(attributeType.FullName, semanticModel);
    }

    public static AttributeSyntax? GetAttributeWithName(this MemberDeclarationSyntax declarationSyntax, string attributeName, SemanticModel semanticModel)
    {
        if (declarationSyntax is null)
        {
            throw new ArgumentNullException(nameof(declarationSyntax));
        }

        foreach (AttributeListSyntax attributeList in declarationSyntax.AttributeLists)
        {
            if (attributeList.GetAttributeWithName(attributeName, semanticModel) is AttributeSyntax attributeSyntax)
            {
                return attributeSyntax;
            }
        }

        return null;
    }

    public static AttributeSyntax? GetFirstAttributeOfTypeIn(this MemberDeclarationSyntax declarationSyntax, IReadOnlyCollection<Type> attributeTypes,
        SemanticModel semanticModel)
    {
        if (attributeTypes is null)
        {
            throw new ArgumentNullException(nameof(attributeTypes));
        }

        string[] attributeNames = new string[attributeTypes.Count];

        int index = 0;
        foreach (Type attributeType in attributeTypes)
        {
            attributeNames[index++] = attributeType.FullName;
        }

        return GetFirstAttributeWithNameIn(declarationSyntax, attributeNames, semanticModel);
    }

    public static AttributeSyntax? GetFirstAttributeOfTypeIn(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, params Type[] attributeTypes)
    {
        return GetFirstAttributeOfTypeIn(declarationSyntax, attributeTypes, semanticModel);
    }

    public static AttributeSyntax? GetFirstAttributeWithNameIn(this MemberDeclarationSyntax declarationSyntax, IReadOnlyCollection<string> attributeNames,
        SemanticModel semanticModel)
    {
        if (declarationSyntax is null)
        {
            throw new ArgumentNullException(nameof(declarationSyntax));
        }

        foreach (AttributeListSyntax attributeList in declarationSyntax.AttributeLists)
        {
            if (attributeList.GetFirstAttributeWithNameIn(attributeNames, semanticModel) is AttributeSyntax attributeSyntax)
            {
                return attributeSyntax;
            }
        }

        return null;
    }

    public static AttributeSyntax? GetFirstAttributeWithNameIn(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel,
        params string[] attributeNames)
    {
        return GetFirstAttributeWithNameIn(declarationSyntax, attributeNames, semanticModel);
    }
}
