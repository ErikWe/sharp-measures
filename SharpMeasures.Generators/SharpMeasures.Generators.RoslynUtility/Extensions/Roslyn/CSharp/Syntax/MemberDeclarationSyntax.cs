namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;

public static partial class Extensions
{
    public static bool HasModifierOfKind(this MemberDeclarationSyntax declarationSyntax, SyntaxKind token) => declarationSyntax.Modifiers.ContainsTokenOfKind(token);

    public static AttributeSyntax? GetAttributeOfType<TAttribute>(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel) => declarationSyntax.GetAttributeOfType<TAttribute>(semanticModel, checkForAlias: true);
    public static AttributeSyntax? GetAttributeOfType<TAttribute>(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, bool checkForAlias) => declarationSyntax.GetAttributeOfType(semanticModel, typeof(TAttribute), checkForAlias);

    public static AttributeSyntax? GetAttributeOfType(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, Type attributeType) => declarationSyntax.GetAttributeOfType(semanticModel, attributeType, checkForAlias: true);
    public static AttributeSyntax? GetAttributeOfType(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, Type attributeType, bool checkForAlias) => declarationSyntax.GetAttributeWithName(semanticModel, attributeType.FullName, checkForAlias);

    public static AttributeSyntax? GetAttributeWithName(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, string attributeName) => declarationSyntax.GetAttributeWithName(semanticModel, attributeName, checkForAlias: true);
    public static AttributeSyntax? GetAttributeWithName(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, string attributeName, bool checkForAlias)
    {
        foreach (var attributeList in declarationSyntax.AttributeLists)
        {
            if (attributeList.GetAttributeWithName(semanticModel, attributeName, checkForAlias: false) is AttributeSyntax attributeSyntax)
            {
                return attributeSyntax;
            }
        }

        if (checkForAlias)
        {
            foreach (var attributeList in declarationSyntax.AttributeLists)
            {
                if (attributeList.GetAliasedAttributeWithName(semanticModel, attributeName) is AttributeSyntax attributeSyntax)
                {
                    return attributeSyntax;
                }
            }
        }

        return null;
    }

    public static (AttributeSyntax Syntax, Type Target)? GetAttributeOfAnyType<TAttribute1, TAttribute2>(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel) => declarationSyntax.GetAttributeOfAnyType<TAttribute1, TAttribute2>(semanticModel, checkForAlias: true);
    public static (AttributeSyntax Syntax, Type Target)? GetAttributeOfAnyType<TAttribute1, TAttribute2>(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, bool checkForAlias) => declarationSyntax.GetAttributeOfAnyType(semanticModel, checkForAlias, typeof(TAttribute1), typeof(TAttribute2));

    public static (AttributeSyntax Syntax, Type Target)? GetAttributeOfAnyType<TAttribute1, TAttribute2, TAttribute3>(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel) => declarationSyntax.GetAttributeOfAnyType<TAttribute1, TAttribute2, TAttribute3>(semanticModel, checkForAlias: true);
    public static (AttributeSyntax Syntax, Type Target)? GetAttributeOfAnyType<TAttribute1, TAttribute2, TAttribute3>(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, bool checkForAlias) => declarationSyntax.GetAttributeOfAnyType(semanticModel, checkForAlias, typeof(TAttribute1), typeof(TAttribute2), typeof(TAttribute3));

    public static (AttributeSyntax Syntax, Type Target)? GetAttributeOfAnyType(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, params Type[] candidateTypes) => declarationSyntax.GetAttributeOfAnyType(semanticModel, checkForAlias: true, candidateTypes);
    public static (AttributeSyntax Syntax, Type Target)? GetAttributeOfAnyType(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, bool checkForAlias, params Type[] candidateTypes) => declarationSyntax.GetAttributeOfAnyType(semanticModel, candidateTypes as IEnumerable<Type>, checkForAlias);

    public static (AttributeSyntax Syntax, Type Target)? GetAttributeOfAnyType(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, IEnumerable<Type> candidateTypes) => declarationSyntax.GetAttributeOfAnyType(semanticModel, candidateTypes, checkForAlias: true);
    public static (AttributeSyntax Syntax, Type Target)? GetAttributeOfAnyType(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, IEnumerable<Type> candidateTypes, bool checkForAlias)
    {
        foreach (var attributeList in declarationSyntax.AttributeLists)
        {
            if (attributeList.GetAttributeOfAnyType(semanticModel, candidateTypes, checkForAlias: false) is (AttributeSyntax, Type) result)
            {
                return result;
            }
        }

        if (checkForAlias)
        {
            foreach (var attributeList in declarationSyntax.AttributeLists)
            {
                if (attributeList.GetAliasedAttributeOfAnyType(semanticModel, candidateTypes) is (AttributeSyntax, Type) result)
                {
                    return result;
                }
            }
        }

        return null;
    }

    public static (AttributeSyntax Syntax, string Target)? GetAttributeWithAnyName(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, params string[] candidateNames) => declarationSyntax.GetAttributeWithAnyName(semanticModel, checkForAlias: true, candidateNames);
    public static (AttributeSyntax Syntax, string Target)? GetAttributeWithAnyName(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, bool checkForAlias, params string[] candidateNames) => declarationSyntax.GetAttributeWithAnyName(semanticModel, candidateNames as IEnumerable<string>, checkForAlias);

    public static (AttributeSyntax Syntax, string Target)? GetAttributeWithAnyName(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, IEnumerable<string> candidateNames) => declarationSyntax.GetAttributeWithAnyName(semanticModel, candidateNames, checkForAlias: true);
    public static (AttributeSyntax Syntax, string Target)? GetAttributeWithAnyName(this MemberDeclarationSyntax declarationSyntax, SemanticModel semanticModel, IEnumerable<string> candidateNames, bool checkForAlias)
    {
        foreach (var attributeList in declarationSyntax.AttributeLists)
        {
            if (attributeList.GetAttributeWithAnyName(semanticModel, candidateNames, checkForAlias: false) is (AttributeSyntax, string) result)
            {
                return result;
            }
        }

        if (checkForAlias)
        {
            foreach (var attributeList in declarationSyntax.AttributeLists)
            {
                if (attributeList.GetAliasedAttributeWithAnyName(semanticModel, candidateNames) is (AttributeSyntax, string) result)
                {
                    return result;
                }
            }
        }

        return null;
    }
}
