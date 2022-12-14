namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;

public static partial class Extensions
{
    public static bool RepresentsAttributeOfType<TAttribute>(this AttributeSyntax attributeSyntax, SemanticModel semanticModel) => attributeSyntax.RepresentsAttributeOfType<TAttribute>(semanticModel, checkForAlias: true);
    public static bool RepresentsAttributeOfType<TAttribute>(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, bool checkForAlias) => attributeSyntax.RepresentsAttributeOfType(semanticModel, typeof(TAttribute), checkForAlias);

    public static bool RepresentsAttributeOfType(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, Type attributeType) => attributeSyntax.RepresentsAttributeOfType(semanticModel, attributeType, checkForAlias: true);
    public static bool RepresentsAttributeOfType(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, Type attributeType, bool checkForAlias) => attributeSyntax.RepresentsAttributeWithName(semanticModel, attributeType.FullName, checkForAlias);

    public static bool RepresentsAttributeWithName(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, string attributeName) => attributeSyntax.RepresentsAttributeWithName(semanticModel, attributeName, checkForAlias: true);
    public static bool RepresentsAttributeWithName(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, string attributeName, bool checkForAlias)
    {
        var simpleAttributeName = getSimpleAttributeName();
        var actualName = getActualName();

        if (simpleAttributeName == actualName || simpleAttributeName == $"{actualName}Attribute")
        {
            if (semanticModel.GetSymbolInfo(attributeSyntax.Name).Symbol is IMethodSymbol attributeConstructor && attributeConstructor.ContainingType.ToDisplayString() == attributeName)
            {
                return true;
            }

            return false;
        }

        if (checkForAlias)
        {
            return attributeSyntax.RepresentsAliasedAttributeWithName(semanticModel, attributeName);
        }

        return false;

        string getSimpleAttributeName()
        {
            var components = attributeName.Split('.');

            return components[components.Length - 1];
        }

        string getActualName()
        {
            if (attributeSyntax.Name is QualifiedNameSyntax qualifiedName)
            {
                return qualifiedName.Right.ToString();
            }

            return attributeSyntax.Name.ToString();
        }
    }

    public static Type? RepresentsAttributeOfAnyType<TAttribute1, TAttribute2>(this AttributeSyntax attributeSyntax, SemanticModel semanticModel) => attributeSyntax.RepresentsAttributeOfAnyType<TAttribute1, TAttribute2>(semanticModel, checkForAlias: true);
    public static Type? RepresentsAttributeOfAnyType<TAttribute1, TAttribute2>(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, bool checkForAlias) => attributeSyntax.RepresentsAttributeOfAnyType(semanticModel, checkForAlias, typeof(TAttribute1), typeof(TAttribute2));

    public static Type? RepresentsAttributeOfAnyType<TAttribute1, TAttribute2, TAttribute3>(this AttributeSyntax attributeSyntax, SemanticModel semanticModel) => attributeSyntax.RepresentsAttributeOfAnyType<TAttribute1, TAttribute2, TAttribute3>(semanticModel, checkForAlias: true);
    public static Type? RepresentsAttributeOfAnyType<TAttribute1, TAttribute2, TAttribute3>(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, bool checkForAlias) => attributeSyntax.RepresentsAttributeOfAnyType(semanticModel, checkForAlias, typeof(TAttribute1), typeof(TAttribute2), typeof(TAttribute3));

    public static Type? RepresentsAttributeOfAnyType(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, params Type[] candidateTypes) => attributeSyntax.RepresentsAttributeOfAnyType(semanticModel, checkForAlias: true, candidateTypes);
    public static Type? RepresentsAttributeOfAnyType(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, bool checkForAlias, params Type[] candidateTypes) => attributeSyntax.RepresentsAttributeOfAnyType(semanticModel, candidateTypes, checkForAlias);

    public static Type? RepresentsAttributeOfAnyType(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, IEnumerable<Type> candidateTypes) => attributeSyntax.RepresentsAttributeOfAnyType(semanticModel, candidateTypes, checkForAlias: true);
    public static Type? RepresentsAttributeOfAnyType(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, IEnumerable<Type> candidateTypes, bool checkForAlias)
    {
        foreach (var candidateType in candidateTypes)
        {
            if (attributeSyntax.RepresentsAttributeOfType(semanticModel, candidateType, checkForAlias: false))
            {
                return candidateType;
            }
        }

        if (checkForAlias)
        {
            return attributeSyntax.RepresentsAliasedAttributeOfAnyType(semanticModel, candidateTypes);
        }

        return null;
    }

    public static string? RepresentsAttributeWithAnyName(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, params string[] candidateNames) => attributeSyntax.RepresentsAttributeWithAnyName(semanticModel, checkForAlias: true, candidateNames);
    public static string? RepresentsAttributeWithAnyName(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, bool checkForAlias, params string[] candidateNames) => attributeSyntax.RepresentsAttributeWithAnyName(semanticModel, candidateNames, checkForAlias);

    public static string? RepresentsAttributeWithAnyName(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, IEnumerable<string> candidateNames) => attributeSyntax.RepresentsAttributeWithAnyName(semanticModel, candidateNames, checkForAlias: true);
    public static string? RepresentsAttributeWithAnyName(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, IEnumerable<string> candidateNames, bool checkForAlias)
    {
        foreach (var candidateName in candidateNames)
        {
            if (attributeSyntax.RepresentsAttributeWithName(semanticModel, candidateName, checkForAlias: false))
            {
                return candidateName;
            }
        }

        if (checkForAlias)
        {
            return attributeSyntax.RepresentsAliasedAttributeWithAnyName(semanticModel, candidateNames);
        }

        return null;
    }

    public static bool RepresentsAliasedAttributeOfType<TAttribute>(this AttributeSyntax attributeSyntax, SemanticModel semanticModel) => attributeSyntax.RepresentsAliasedAttributeOfType(semanticModel, typeof(TAttribute));
    public static bool RepresentsAliasedAttributeOfType(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, Type attributeType) => attributeSyntax.RepresentsAliasedAttributeWithName(semanticModel, attributeType.FullName);
    public static bool RepresentsAliasedAttributeWithName(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, string attributeName)
    {
        return semanticModel.GetAliasInfo(attributeSyntax.Name) is IAliasSymbol { Target: INamedTypeSymbol aliasSymbol } && aliasSymbol.ToDisplayString() == attributeName;
    }

    public static Type? RepresentsAliasedAttributeOfAnyType<TAttribute1, TAttribute2>(this AttributeSyntax attributeSyntax, SemanticModel semanticModel) => attributeSyntax.RepresentsAliasedAttributeOfAnyType(semanticModel, typeof(TAttribute1), typeof(TAttribute2));
    public static Type? RepresentsAliasedAttributeOfAnyType<TAttribute1, TAttribute2, TAttribute3>(this AttributeSyntax attributeSyntax, SemanticModel semanticModel) => attributeSyntax.RepresentsAliasedAttributeOfAnyType(semanticModel, typeof(TAttribute1), typeof(TAttribute2), typeof(TAttribute3));

    public static Type? RepresentsAliasedAttributeOfAnyType(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, params Type[] candidateTypes) => attributeSyntax.RepresentsAliasedAttributeOfAnyType(semanticModel, candidateTypes as IEnumerable<Type>);
    public static Type? RepresentsAliasedAttributeOfAnyType(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, IEnumerable<Type> candidateTypes)
    {
        if (semanticModel.GetAliasInfo(attributeSyntax.Name) is not IAliasSymbol { Target: INamedTypeSymbol aliasSymbol })
        {
            return null;
        }

        foreach (var candidateType in candidateTypes)
        {
            if (aliasSymbol.ToDisplayString() == candidateType.FullName)
            {
                return candidateType;
            }
        }

        return null;
    }

    public static string? RepresentsAliasedAttributeWithAnyName(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, params string[] candidateNames) => attributeSyntax.RepresentsAliasedAttributeWithAnyName(semanticModel, candidateNames as IEnumerable<string>);
    public static string? RepresentsAliasedAttributeWithAnyName(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, IEnumerable<string> candidateNames)
    {
        if (semanticModel.GetAliasInfo(attributeSyntax.Name) is not IAliasSymbol { Target: INamedTypeSymbol aliasSymbol })
        {
            return null;
        }

        foreach (var candidateName in candidateNames)
        {
            if (aliasSymbol.ToDisplayString() == candidateName)
            {
                return candidateName;
            }
        }

        return null;
    }
}
