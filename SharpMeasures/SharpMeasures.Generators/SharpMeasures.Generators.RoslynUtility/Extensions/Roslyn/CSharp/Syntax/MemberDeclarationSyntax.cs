namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
        return declarationSyntax.GetAttributeOfType(semanticModel, typeof(TAttribute));
    }

    public static AttributeSyntax? GetAttributeOfType(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, Type attributeType)
    {
        if (attributeType is null)
        {
            throw new ArgumentNullException(nameof(attributeType));
        }

        return declarationSyntax.GetAttributeWithName(semanticModel, attributeType.FullName);
    }

    public static AttributeSyntax? GetAttributeWithName(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, string attributeName)
    {
        if (declarationSyntax is null)
        {
            throw new ArgumentNullException(nameof(declarationSyntax));
        }

        foreach (AttributeListSyntax attributeList in declarationSyntax.AttributeLists)
        {
            if (attributeList.GetAttributeWithName(semanticModel, attributeName) is AttributeSyntax attributeSyntax)
            {
                return attributeSyntax;
            }
        }

        return null;
    }

    public static AttributeSyntax? GetFirstAttributeOfType<TAttribute1, TAttribute2>(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel)
    {
        return declarationSyntax.GetFirstAttributeOfType(semanticModel, typeof(TAttribute1), typeof(TAttribute2));
    }

    public static AttributeSyntax? GetFirstAttributeOfType<TAttribute1, TAttribute2, TAttribute3>(this MemberDeclarationSyntax declarationSyntax,
        SemanticModel semanticModel)
    {
        return declarationSyntax.GetFirstAttributeOfType(semanticModel, typeof(TAttribute1), typeof(TAttribute2), typeof(TAttribute3));
    }

    public static AttributeSyntax? GetFirstAttributeOfType(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel,
        IEnumerable<Type> candidateTypes)
    {
        if (candidateTypes is null)
        {
            throw new ArgumentNullException(nameof(candidateTypes));
        }

        return declarationSyntax.GetFirstAttributeWithName(semanticModel, candidateNames());

        IEnumerable<string> candidateNames()
        {
            foreach (Type candidateType in candidateTypes)
            {
                yield return candidateType.FullName;
            }
        }
    }

    public static AttributeSyntax? GetFirstAttributeOfType(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, params Type[] candidateTypes)
    {
        return declarationSyntax.GetFirstAttributeOfType(semanticModel, candidateTypes as IEnumerable<Type>);
    }

    public static AttributeSyntax? GetFirstAttributeWithName(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel,
        IEnumerable<string> candidateNames)
    {
        if (declarationSyntax is null)
        {
            throw new ArgumentNullException(nameof(declarationSyntax));
        }

        foreach (AttributeListSyntax attributeList in declarationSyntax.AttributeLists)
        {
            if (attributeList.GetFirstAttributeWithName(semanticModel, candidateNames) is AttributeSyntax attributeSyntax)
            {
                return attributeSyntax;
            }
        }

        return null;
    }

    public static AttributeSyntax? GetFirstAttributeWithName(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel,
        params string[] candidateNames)
    {
        return declarationSyntax.GetFirstAttributeWithName(semanticModel, candidateNames as IEnumerable<string>);
    }
}
