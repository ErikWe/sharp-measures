﻿namespace SharpMeasures.Generators.SourceBuilding;

using System;
using System.Collections.Generic;
using System.Text;

internal static class UsingsBuilding
{
    public static void AppendUsings(StringBuilder source, string fromNamespace, IEnumerable<string> usingsNames)
    {
        AppendUsings(source, implicitlyUsed, usingsNames);

        bool implicitlyUsed(string name) => fromNamespace.StartsWith(name, StringComparison.Ordinal);
    }

    public static void AppendUsings(StringBuilder source, string fromNamespace, params string[] usingsNames)
    {
        AppendUsings(source, fromNamespace, usingsNames as IEnumerable<string>);
    }

    public static void AppendUsings(StringBuilder source, IEnumerable<string> usingsNames)
    {
        AppendUsings(source, ignoreUsing, usingsNames);

        static bool ignoreUsing(string name) => false;
    }

    public static void InsertUsings(StringBuilder source, string fromNamespace, int startIndex, IEnumerable<string> usingsNames)
    {
        StringBuilder usings = new();

        AppendUsings(usings, fromNamespace, usingsNames);

        source.Insert(startIndex, usings);
    }

    public static void InsertUsings(StringBuilder source, string fromNamespace, int startIndex, params string[] usingsNames)
    {
        InsertUsings(source, fromNamespace, startIndex, usingsNames as IEnumerable<string>);
    }

    public static void InsertUsings(StringBuilder source, int startIndex, IEnumerable<string> usingsNames)
    {
        StringBuilder usings = new();

        AppendUsings(usings, usingsNames);

        source.Insert(startIndex, usings);
    }

    public static void InsertUsings(StringBuilder source, int startIndex, params string[] usingsNames)
    {
        InsertUsings(source, startIndex, usingsNames as IEnumerable<string>);
    }

    private static void AppendUsings(StringBuilder source, Func<string, bool> ignoreDelegate, IEnumerable<string> usingsNames)
    {
        HashSet<string> definedUsings = new();

        IterativeBuilding.AppendEnumerable(source, usings(), Environment.NewLine, Environment.NewLine);

        IEnumerable<string> usings()
        {
            foreach (string usingsName in usingsNames)
            {
                if (definedUsings.Contains(usingsName) || ignoreDelegate(usingsName))
                {
                    continue;
                }

                definedUsings.Add(usingsName);
                yield return $"using {usingsName};";
            }
        }
    }
}
