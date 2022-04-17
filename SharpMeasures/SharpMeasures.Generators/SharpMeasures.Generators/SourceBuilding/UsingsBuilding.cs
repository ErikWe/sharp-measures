namespace SharpMeasures.Generators.SourceBuilding;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.Text;

internal static class UsingsBuilding
{
    public static void AppendUsings(StringBuilder source, ISymbol symbol, IEnumerable<string> usingsNames)
        => AppendUsings(source, symbol.ContainingNamespace?.ToDisplayString() ?? string.Empty, usingsNames);

    public static void AppendUsings(StringBuilder source, string namespaceName, IEnumerable<string> usingsNames)
    {
        IterativeBuilding.AppendEnumerable(source, toLines(), string.Empty, Environment.NewLine);

        IEnumerable<string> toLines()
        {
            foreach (string usingName in usingsNames)
            {
                if (!namespaceName.StartsWith(usingName, StringComparison.Ordinal))
                {
                    yield return $"using {usingName};{Environment.NewLine}";
                }
            }
        }
    }

    public static void AppendUsings(StringBuilder source, IEnumerable<string> usingsNames)
    {
        bool anyUsings = false;

        foreach (string usingName in usingsNames)
        {
            anyUsings = true;
            source.Append($"using {usingName};{Environment.NewLine}");
        }

        if (anyUsings)
        {
            source.Append(Environment.NewLine);
        }
    }
}
