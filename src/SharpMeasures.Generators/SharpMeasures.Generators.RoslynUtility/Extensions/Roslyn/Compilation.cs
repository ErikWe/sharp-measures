namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using System;

public static partial class RoslynUtilityExtensions
{
    public static INamedTypeSymbol? GetTypeByMetadataName<T>(this Compilation compilation) => compilation.GetTypeByMetadataName(typeof(T));

    public static INamedTypeSymbol? GetTypeByMetadataName(this Compilation compilation, Type type)
    {
        if (type.GenericTypeArguments.Length > 0)
        {
            return compilation.GetTypeByMetadataName(type, type.GenericTypeArguments.Length);
        }

        return compilation.GetTypeByMetadataName($"{type.Namespace}.{type.Name}");
    }

    public static INamedTypeSymbol? GetTypeByMetadataName<T>(this Compilation compilation, int arity) => compilation.GetTypeByMetadataName(typeof(T), arity);
    public static INamedTypeSymbol? GetTypeByMetadataName(this Compilation compilation, Type type, int arity) => compilation.GetTypeByMetadataName($"{type.Namespace}.{type.Name}`{arity}");
}
