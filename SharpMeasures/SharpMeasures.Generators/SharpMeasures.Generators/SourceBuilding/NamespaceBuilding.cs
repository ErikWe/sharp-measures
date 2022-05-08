﻿namespace SharpMeasures.Generators.SourceBuilding;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Utility;

using System;
using System.Text;

internal static class NamespaceBuilding
{
    public static void AppendNamespace(StringBuilder source, ISymbol symbol)
    {
        if (symbol.ContainingNamespace?.ToDisplayString() is string namespaceName)
        {
            AppendNamespace(source, namespaceName);
        }
    }

    public static void AppendNamespace(StringBuilder source, string namespaceName)
    {
        if (!string.IsNullOrEmpty(namespaceName))
        {
            source.Append($"namespace {namespaceName};{Environment.NewLine}{Environment.NewLine}");
        }
    }

    public static void AppendNamespace(StringBuilder source, NamedType type) => AppendNamespace(source, type.Namespace);
    public static void AppendNamespace(StringBuilder source, DefinedType type) => AppendNamespace(source, type.Namespace);
}
