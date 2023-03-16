namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;

public static partial class Extensions
{
    public static AttributeSyntax? GetAttributeOfType<TAttribute>(this AttributeListSyntax attributeList, SemanticModel semanticModel) => attributeList.GetAttributeOfType<TAttribute>(semanticModel, checkForAlias: true);
    public static AttributeSyntax? GetAttributeOfType<TAttribute>(this AttributeListSyntax attributeList, SemanticModel semanticModel, bool checkForAlias) => attributeList.GetAttributeOfType(semanticModel, typeof(TAttribute), checkForAlias);

    public static AttributeSyntax? GetAttributeOfType(this AttributeListSyntax attributeList, SemanticModel semanticModel, Type attributeType) => attributeList.GetAttributeOfType(semanticModel, attributeType, checkForAlias: true);
    public static AttributeSyntax? GetAttributeOfType(this AttributeListSyntax attributeList, SemanticModel semanticModel, Type attributeType, bool checkForAlias) => attributeList.GetAttributeWithName(semanticModel, attributeType.FullName, checkForAlias);

    public static AttributeSyntax? GetAttributeWithName(this AttributeListSyntax attributeList, SemanticModel semanticModel, string attributeName) => attributeList.GetAttributeWithName(semanticModel, attributeName, checkForAlias: true);
    public static AttributeSyntax? GetAttributeWithName(this AttributeListSyntax attributeList, SemanticModel semanticModel, string attributeName, bool checkForAlias)
    {
        foreach (var attributeSyntax in attributeList.Attributes)
        {
            if (attributeSyntax.RepresentsAttributeWithName(semanticModel, attributeName, checkForAlias: false))
            {
                return attributeSyntax;
            }
        }

        if (checkForAlias)
        {
            return attributeList.GetAliasedAttributeWithName(semanticModel, attributeName);
        }

        return null;
    }

    public static (AttributeSyntax Syntax, Type Target)? GetAttributeOfAnyType<TAttribute1, TAttribute2>(this AttributeListSyntax attributeList, SemanticModel semanticModel) => attributeList.GetAttributeOfAnyType<TAttribute1, TAttribute2>(semanticModel, checkForAlias: true);
    public static (AttributeSyntax Syntax, Type Target)? GetAttributeOfAnyType<TAttribute1, TAttribute2>(this AttributeListSyntax attributeList, SemanticModel semanticModel, bool checkForAlias) => attributeList.GetAttributeOfAnyType(semanticModel, checkForAlias, typeof(TAttribute1), typeof(TAttribute2));

    public static (AttributeSyntax Syntax, Type Target)? GetAttributeOfAnyType<TAttribute1, TAttribute2, TAttribute3>(this AttributeListSyntax attributeList, SemanticModel semanticModel) => attributeList.GetAttributeOfAnyType<TAttribute1, TAttribute2, TAttribute3>(semanticModel, checkForAlias: true);
    public static (AttributeSyntax Syntax, Type Target)? GetAttributeOfAnyType<TAttribute1, TAttribute2, TAttribute3>(this AttributeListSyntax attributeList, SemanticModel semanticModel, bool checkForAlias) => attributeList.GetAttributeOfAnyType(semanticModel, checkForAlias, typeof(TAttribute1), typeof(TAttribute2), typeof(TAttribute3));

    public static (AttributeSyntax Syntax, Type Target)? GetAttributeOfAnyType(this AttributeListSyntax attributeList, SemanticModel semanticModel, params Type[] candidateTypes) => attributeList.GetAttributeOfAnyType(semanticModel, checkForAlias: true, candidateTypes);
    public static (AttributeSyntax Syntax, Type Target)? GetAttributeOfAnyType(this AttributeListSyntax attributeList, SemanticModel semanticModel, bool checkForAlias, params Type[] candidateTypes) => attributeList.GetAttributeOfAnyType(semanticModel, candidateTypes, checkForAlias);

    public static (AttributeSyntax Syntax, Type Target)? GetAttributeOfAnyType(this AttributeListSyntax attributeList, SemanticModel semanticModel, IEnumerable<Type> candidateTypes) => attributeList.GetAttributeOfAnyType(semanticModel, candidateTypes, checkForAlias: true);
    public static (AttributeSyntax Syntax, Type Target)? GetAttributeOfAnyType(this AttributeListSyntax attributeList, SemanticModel semanticModel, IEnumerable<Type> candidateTypes, bool checkForAlias)
    {
        foreach (var attributeSyntax in attributeList.Attributes)
        {
            if (attributeSyntax.RepresentsAttributeOfAnyType(semanticModel, candidateTypes, checkForAlias: false) is Type target)
            {
                return (attributeSyntax, target);
            }
        }

        if (checkForAlias)
        {
            return attributeList.GetAliasedAttributeOfAnyType(semanticModel, candidateTypes);
        }

        return null;
    }

    public static (AttributeSyntax Syntax, string Target)? GetAttributeWithAnyName(this AttributeListSyntax attributeList, SemanticModel semanticModel, params string[] candidateNames) => attributeList.GetAttributeWithAnyName(semanticModel, checkForAlias: true, candidateNames);
    public static (AttributeSyntax Syntax, string Target)? GetAttributeWithAnyName(this AttributeListSyntax attributeList, SemanticModel semanticModel, bool checkForAlias, params string[] candidateNames) => attributeList.GetAttributeWithAnyName(semanticModel, candidateNames, checkForAlias);

    public static (AttributeSyntax Syntax, string Target)? GetAttributeWithAnyName(this AttributeListSyntax attributeList, SemanticModel semanticModel, IEnumerable<string> candidateNames) => attributeList.GetAttributeWithAnyName(semanticModel, candidateNames, checkForAlias: true);
    public static (AttributeSyntax Syntax, string Target)? GetAttributeWithAnyName(this AttributeListSyntax attributeList, SemanticModel semanticModel, IEnumerable<string> candidateNames, bool checkForAlias)
    {
        foreach (var attributeSyntax in attributeList.Attributes)
        {
            if (attributeSyntax.RepresentsAttributeWithAnyName(semanticModel, candidateNames, checkForAlias: false) is string target)
            {
                return (attributeSyntax, target);
            }
        }

        if (checkForAlias)
        {
            return attributeList.GetAliasedAttributeWithAnyName(semanticModel, candidateNames);
        }

        return null;
    }

    public static AttributeSyntax? GetAliasedAttributeOfType<TAttribute>(this AttributeListSyntax attributeList, SemanticModel semanticModel) => attributeList.GetAliasedAttributeOfType(semanticModel, typeof(TAttribute));
    public static AttributeSyntax? GetAliasedAttributeOfType(this AttributeListSyntax attributeList, SemanticModel semanticModel, Type attributeType) => attributeList.GetAliasedAttributeWithName(semanticModel, attributeType.FullName);
    public static AttributeSyntax? GetAliasedAttributeWithName(this AttributeListSyntax attributeList, SemanticModel semanticModel, string attributeName)
    {
        foreach (var attributeSyntax in attributeList.Attributes)
        {
            if (attributeSyntax.RepresentsAliasedAttributeWithName(semanticModel, attributeName))
            {
                return attributeSyntax;
            }
        }

        return null;
    }

    public static (AttributeSyntax Syntax, Type Target)? GetAliasedAttributeOfAnyType<TAttribute1, TAttribute2>(this AttributeListSyntax attributeList, SemanticModel semanticModel) => attributeList.GetAliasedAttributeOfAnyType(semanticModel, typeof(TAttribute1), typeof(TAttribute2));
    public static (AttributeSyntax Syntax, Type Target)? GetAliasedAttributeOfAnyType<TAttribute1, TAttribute2, TAttribute3>(this AttributeListSyntax attributeList, SemanticModel semanticModel) => attributeList.GetAliasedAttributeOfAnyType(semanticModel, typeof(TAttribute1), typeof(TAttribute2), typeof(TAttribute3));
    public static (AttributeSyntax Syntax, Type Target)? GetAliasedAttributeOfAnyType(this AttributeListSyntax attributeList, SemanticModel semanticModel, params Type[] candidateTypes) => attributeList.GetAliasedAttributeOfAnyType(semanticModel, candidateTypes as IEnumerable<Type>);
    public static (AttributeSyntax Syntax, Type Target)? GetAliasedAttributeOfAnyType(this AttributeListSyntax attributeList, SemanticModel semanticModel, IEnumerable<Type> candidateTypes)
    {
        foreach (var attributeSyntax in attributeList.Attributes)
        {
            if (attributeSyntax.RepresentsAliasedAttributeOfAnyType(semanticModel, candidateTypes) is Type target)
            {
                return (attributeSyntax, target);
            }
        }

        return null;
    }

    public static (AttributeSyntax Syntax, string Target)? GetAliasedAttributeWithAnyName(this AttributeListSyntax attributeList, SemanticModel semanticModel, params string[] candidateNames) => attributeList.GetAliasedAttributeWithAnyName(semanticModel, candidateNames as IEnumerable<string>);
    public static (AttributeSyntax Syntax, string Target)? GetAliasedAttributeWithAnyName(this AttributeListSyntax attributeList, SemanticModel semanticModel, IEnumerable<string> candidateNames)
    {
        foreach (var attributeSyntax in attributeList.Attributes)
        {
            if (attributeSyntax.RepresentsAliasedAttributeWithAnyName(semanticModel, candidateNames) is string target)
            {
                return (attributeSyntax, target);
            }
        }

        return null;
    }
}
