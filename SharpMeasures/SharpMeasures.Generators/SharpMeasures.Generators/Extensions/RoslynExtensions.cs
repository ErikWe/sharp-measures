namespace Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;

internal static class RoslynExtensions
{
    public static INamedTypeSymbol? GetTypeByMetadataName<T>(this Compilation compilation)
    {
        Type type = typeof(T);
        return compilation.GetTypeByMetadataName($"{type.Namespace}.{type.Name}");
    }

    public static INamedTypeSymbol? GetTypeByMetadataName<T>(this Compilation compilation, int arity)
    {
        Type type = typeof(T);
        return compilation.GetTypeByMetadataName($"{type.Namespace}.{type.Name}`{arity}");
    }

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

    public static IncrementalValuesProvider<T> WhereNotNull<T>(this IncrementalValuesProvider<T?> provider) where T : struct
        => provider.Where(static (x) => x is not null).Select(static (x, _) => x!.Value);

    public static IncrementalValuesProvider<T> WhereNotNull<T>(this IncrementalValuesProvider<T?> provider) where T : class
        => provider.Where(static (x) => x is not null)!;
}
