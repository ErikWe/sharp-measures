namespace Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;

internal static class INamedTypeSymbolExtensions
{
    public static IEnumerable<AttributeData> GetAttributesOfType<TAttribute>(this INamedTypeSymbol typeSymbol)
        => typeSymbol.GetAttributesOfType(typeof(TAttribute));

    public static IEnumerable<AttributeData> GetAttributesOfType(this INamedTypeSymbol typeSymbol, Type attributeType)
        => typeSymbol.GetAttributesOfName(attributeType.FullName);

    public static IEnumerable<AttributeData> GetAttributesOfName(this INamedTypeSymbol typeSymbol, string attributeName)
    {
        foreach (AttributeData attributeData in typeSymbol.GetAttributes())
        {
            if (attributeData.AttributeClass?.ToDisplayString() == attributeName)
            {
                yield return attributeData;
            }
        }
    }

    public static AttributeData? GetAttributeOfType<TAttribute>(this INamedTypeSymbol typeSymbol)
        => typeSymbol.GetAttributeOfType(typeof(TAttribute));

    public static AttributeData? GetAttributeOfType(this INamedTypeSymbol typeSymbol, Type attributeType)
        => typeSymbol.GetAttributeOfName(attributeType.FullName);

    public static AttributeData? GetAttributeOfName(this INamedTypeSymbol typeSymbol, string attributeName)
    {
        foreach (AttributeData attributeData in typeSymbol.GetAttributesOfName(attributeName))
        {
            return attributeData;
        }

        return null;
    }
}
