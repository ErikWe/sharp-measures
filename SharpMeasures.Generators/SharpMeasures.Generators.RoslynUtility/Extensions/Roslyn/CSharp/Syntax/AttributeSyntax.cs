namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;

public static partial class Extensions
{
    public static bool RepresentsAttributeOfType<TAttribute>(this AttributeSyntax attributeSyntax, SemanticModel semanticModel)
    {
        return attributeSyntax.RepresentsAttributeOfType(semanticModel, typeof(TAttribute));
    }

    public static bool RepresentsAttributeOfType(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, Type attributeType)
    {
        return attributeSyntax.RepresentsAttributeWithName(semanticModel, attributeType.FullName);
    }

    public static bool RepresentsAttributeWithName(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, string attributeName)
    {
        if (attributeName.EndsWith(attributeSyntax.Name.ToString(), StringComparison.Ordinal) || attributeName.EndsWith($"{attributeSyntax.Name}Attribute", StringComparison.Ordinal))
        {
            if (semanticModel.GetSymbolInfo(attributeSyntax.Name).Symbol is IMethodSymbol attributeConstructor && attributeConstructor.ContainingType.ToDisplayString() == attributeName)
            {
                return true;
            }
        }
        else
        {
            return semanticModel.GetAliasInfo(attributeSyntax.Name) is IAliasSymbol { Target: INamedTypeSymbol aliasSymbol } && aliasSymbol.ToDisplayString() == attributeName;
        }

        return false;
    }

    public static bool RepresentsAttributeOfType<TAttribute1, TAttribute2>(this AttributeSyntax attributeSyntax, SemanticModel semanticModel)
    {
        return attributeSyntax.RepresentsAttributeOfType(semanticModel, typeof(TAttribute1), typeof(TAttribute2));
    }

    public static bool RepresentsAttributeOfType<TAttribute1, TAttribute2, TAttribute3>(this AttributeSyntax attributeSyntax, SemanticModel semanticModel)
    {
        return attributeSyntax.RepresentsAttributeOfType(semanticModel, typeof(TAttribute1), typeof(TAttribute2), typeof(TAttribute3));
    }

    public static bool RepresentsAttributeOfType(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, IEnumerable<Type> candidateTypes)
    {
        return attributeSyntax.RepresentsAttributeWithName(semanticModel, candidateNames());

        IEnumerable<string> candidateNames()
        {
            foreach (Type candidateType in candidateTypes)
            {
                yield return candidateType.FullName;
            }
        }
    }

    public static bool RepresentsAttributeOfType(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, params Type[] candidateTypes)
    {
        return attributeSyntax.RepresentsAttributeOfType(semanticModel, candidateTypes as IEnumerable<Type>);
    }

    public static bool RepresentsAttributeWithName(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, IEnumerable<string> candidateNames)
    {
        foreach (string attributeName in candidateNames)
        {
            if (attributeSyntax.RepresentsAttributeWithName(semanticModel, attributeName))
            {
                return true;
            }
        }

        return false;
    }

    public static bool RepresentsAttributeWithName(this AttributeSyntax attributeSyntax, SemanticModel semanticModel, params string[] candidateNames)
    {
        return attributeSyntax.RepresentsAttributeWithName(semanticModel, candidateNames as IEnumerable<string>);
    }
}
