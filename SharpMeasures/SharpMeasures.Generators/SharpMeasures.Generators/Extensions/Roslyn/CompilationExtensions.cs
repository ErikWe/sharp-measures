﻿namespace Microsoft.CodeAnalysis;

using System;

internal static class CompilationExtensions
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
}
