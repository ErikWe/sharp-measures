namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;

public static partial class Extensions
{
    public static AttributeSyntax? GetAttributeOfType<TAttribute>(this AttributeListSyntax attributeList, SemanticModel semanticModel)
    {
        return attributeList.GetAttributeOfType(semanticModel, typeof(TAttribute));
    }

    public static AttributeSyntax? GetAttributeOfType(this AttributeListSyntax attributeList, SemanticModel semanticModel, Type attributeType)
    {
        if (attributeType is null)
        {
            throw new ArgumentNullException(nameof(attributeType));
        }

        return attributeList.GetAttributeWithName(semanticModel, attributeType.FullName);
    }

    public static AttributeSyntax? GetAttributeWithName(this AttributeListSyntax attributeList, SemanticModel semanticModel, string attributeName)
    {
        if (attributeList is null)
        {
            throw new ArgumentNullException(nameof(attributeList));
        }

        foreach (AttributeSyntax attributeSyntax in attributeList.Attributes)
        {
            if (attributeSyntax.RepresentsAttributeWithName(semanticModel, attributeName))
            {
                return attributeSyntax;
            }
        }

        return null;
    }

    public static AttributeSyntax? GetFirstAttributeOfType<TAttribute1, TAttribute2>(this AttributeListSyntax attributeList, SemanticModel semanticModel)
    {
        return attributeList.GetFirstAttributeOfType(semanticModel, typeof(TAttribute1), typeof(TAttribute2));
    }

    public static AttributeSyntax? GetFirstAttributeOfType<TAttribute1, TAttribute2, TAttribute3>(this AttributeListSyntax attributeList, SemanticModel semanticModel)
    {
        return attributeList.GetFirstAttributeOfType(semanticModel, typeof(TAttribute1), typeof(TAttribute2), typeof(TAttribute3));
    }

    public static AttributeSyntax? GetFirstAttributeOfType(this AttributeListSyntax attributeList, SemanticModel semanticModel, IEnumerable<Type> candidateTypes)
    {
        if (candidateTypes is null)
        {
            throw new ArgumentNullException(nameof(candidateTypes));
        }

        return attributeList.GetFirstAttributeWithName(semanticModel, candidateNames());

        IEnumerable<string> candidateNames()
        {
            foreach (Type candidateType in candidateTypes)
            {
                yield return candidateType.FullName;
            }
        }
    }

    public static AttributeSyntax? GetFirstAttributeOfType(this AttributeListSyntax attributeList, SemanticModel semanticModel, params Type[] candidateTypes)
    {
        return attributeList.GetFirstAttributeOfType(semanticModel, candidateTypes as IEnumerable<Type>);
    }

    public static AttributeSyntax? GetFirstAttributeWithName(this AttributeListSyntax attributeList, SemanticModel semanticModel, IEnumerable<string> candidateNames)
    {
        if (candidateNames is null)
        {
            throw new ArgumentNullException(nameof(candidateNames));
        }

        foreach (string attributeName in candidateNames)
        {
            if (attributeList.GetAttributeWithName(semanticModel, attributeName) is AttributeSyntax attributeSyntax)
            {
                return attributeSyntax;
            }
        }

        return null;
    }

    public static AttributeSyntax? GetFirstAttributeWithName(this AttributeListSyntax attributeList, SemanticModel semanticModel, params string[] candidateNames)
    {
        return attributeList.GetFirstAttributeWithName(semanticModel, candidateNames as IEnumerable<string>);
    }
}
