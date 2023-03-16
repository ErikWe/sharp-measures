namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;

public static partial class RoslynUtilityExtensions
{
    public static NamedType AsNamedType(this INamedTypeSymbol typeSymbol) => NamedType.FromSymbol(typeSymbol);
    public static DefinedType AsDefinedType(this INamedTypeSymbol typeSymbol) => DefinedType.FromSymbol(typeSymbol);

    public static IReadOnlyList<NamedType?> AsNamedTypes<TCollection>(this TCollection typeSymbols) where TCollection : IReadOnlyCollection<INamedTypeSymbol?>
    {
        return typeSymbols.AsTransformed(transform);

        static NamedType? transform(INamedTypeSymbol? typeSymbol)
        {
            if (typeSymbol is null)
            {
                return null;
            }

            return typeSymbol.AsNamedType();
        }
    }

    public static IReadOnlyList<DefinedType?> AsDefinedTypes<TCollection>(this TCollection typeSymbols) where TCollection : IReadOnlyCollection<INamedTypeSymbol?>
    {
        return typeSymbols.AsTransformed(transform);

        static DefinedType? transform(INamedTypeSymbol? typeSymbol)
        {
            if (typeSymbol is null)
            {
                return null;
            }

            return typeSymbol.AsDefinedType();
        }
    }

    public static bool HasAttributeOfType<TAttribute>(this INamedTypeSymbol typeSymbol) => typeSymbol.HasAttributeOfType(typeof(TAttribute));
    public static bool HasAttributeOfType(this INamedTypeSymbol typeSymbol, Type attributeType) => typeSymbol.HasAttributeOfName(attributeType.FullName);
    public static bool HasAttributeOfName(this INamedTypeSymbol typeSymbol, string attributeName)
    {
        foreach (var attributeData in typeSymbol.GetAttributes())
        {
            if (attributeData.AttributeClass?.ToDisplayString() == attributeName)
            {
                return true;
            }
        }

        return false;
    }

    public static IEnumerable<AttributeData> GetAttributesOfType<TAttribute>(this INamedTypeSymbol typeSymbol) => typeSymbol.GetAttributesOfType(typeof(TAttribute));
    public static IEnumerable<AttributeData> GetAttributesOfType(this INamedTypeSymbol typeSymbol, Type attributeType) => typeSymbol.GetAttributesOfName(attributeType.FullName);
    public static IEnumerable<AttributeData> GetAttributesOfName(this INamedTypeSymbol typeSymbol, string attributeName)
    {
        foreach (var attributeData in typeSymbol.GetAttributes())
        {
            if (attributeData.AttributeClass?.ToDisplayString() == attributeName)
            {
                yield return attributeData;
            }
        }
    }

    public static AttributeData? GetAttributeOfType<TAttribute>(this INamedTypeSymbol typeSymbol) => typeSymbol.GetAttributeOfType(typeof(TAttribute));
    public static AttributeData? GetAttributeOfType(this INamedTypeSymbol typeSymbol, Type attributeType) => typeSymbol.GetAttributeOfName(attributeType.FullName);
    public static AttributeData? GetAttributeOfName(this INamedTypeSymbol typeSymbol, string attributeName)
    {
        foreach (var attributeData in typeSymbol.GetAttributesOfName(attributeName))
        {
            return attributeData;
        }

        return null;
    }

    private static IReadOnlyList<TResult?> AsTransformed<TResult, TCollection>(this TCollection typeSymbols, Func<INamedTypeSymbol?, TResult?> transform) where TCollection : IReadOnlyCollection<INamedTypeSymbol?>
    {
        var result = new TResult?[typeSymbols.Count];

        var index = 0;

        foreach (var typeSymbol in typeSymbols)
        {
            result[index++] = transform(typeSymbol);
        }

        return result;
    }
}
