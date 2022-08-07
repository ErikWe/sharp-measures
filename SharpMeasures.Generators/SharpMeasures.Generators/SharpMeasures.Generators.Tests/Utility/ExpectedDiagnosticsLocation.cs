namespace SharpMeasures.Generators.Tests.Utility;

using Microsoft.CodeAnalysis.Text;

using System;
using System.Collections.Generic;

internal static class ExpectedDiagnosticsLocation
{
    public static TextSpan TextSpan(string source, SourceSearchContext context)
    {
        int contextualStartIndex = source.IndexOf(context.ToString(), StringComparison.InvariantCulture);

        if (contextualStartIndex < 0)
        {
            throw new NotSupportedException($"Target string ({context}) could not be found in source text");
        }

        return new(contextualStartIndex + context.Prefix.Length, context.Target.Length);
    }

    public static TextSpan TextSpan(string source, string target, string prefix = "", string postfix = "")
    {
        SourceSearchContext context = new(target, prefix, postfix);

        return TextSpan(source, context);
    }

    public static TextSpan TextSpan(string source, SourceSubtext subtext, string prefix = "", string postfix = "")
    {
        SourceSearchContext context = new(subtext, prefix, postfix);

        return TextSpan(source, context);
    }

    public static IEnumerable<TextSpan> TextSpan(string source, IEnumerable<SourceSearchContext> contexts)
    {
        foreach (var context in contexts)
        {
            yield return TextSpan(source, context);
        }
    }

    public static TextSpan TypeofArgumentTextSpan(string source, string target, string prefix = "", string postfix = "")
    {
        if (target.StartsWith("typeof(", StringComparison.InvariantCulture))
        {
            return TextSpan(source, SourceSubtext.Typeof(target[7..^1]), prefix, postfix);
        }

        return TextSpan(source, target, prefix, postfix);
    }
}
