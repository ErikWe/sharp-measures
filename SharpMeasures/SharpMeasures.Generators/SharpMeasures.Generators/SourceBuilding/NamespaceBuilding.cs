namespace SharpMeasures.Generators.SourceBuilding;

using Microsoft.CodeAnalysis;

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
}
