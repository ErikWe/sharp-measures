namespace SharpMeasures.Generators.Tests.Utility;

using Microsoft.CodeAnalysis.Text;

using System;
using System.Collections.Generic;

internal static class ExpectedDiagnosticsLocation
{
    public static IEnumerable<TextSpan> TextSpan(string source, string target, string prefix = "", string postfix = "")
    {
        return TextSpan(source, new[] { (target, prefix, postfix) });
    }

    public static IEnumerable<TextSpan> TextSpan(string source, IEnumerable<(string Target, string Prefix, string Postfix)> texts)
    {
        foreach (var (target, prefix, postfix) in texts)
        {
            yield return ComputeExpectedTextSpan(source, target, prefix, postfix);
        }
    }

    public static IEnumerable<TextSpan> TypeofArgumentTextSpan(string source, string target, string prefix = "", string postfix = "")
    {
        return TypeofArgumentTextSpan(source, new[] { (target, prefix, postfix) });
    }

    public static IEnumerable<TextSpan> TypeofArgumentTextSpan(string source, IEnumerable<(string Target, string Prefix, string Postfix)> texts)
    {
        foreach (var (target, prefix, postfix) in texts)
        {
            if (target.StartsWith("typeof(", StringComparison.InvariantCulture) && target.EndsWith(")", StringComparison.InvariantCulture))
            {
                yield return ComputeExpectedTypeofArgumentTextSpan(source, target, prefix, postfix);
            }
            else
            {
                yield return ComputeExpectedTextSpan(source, target, prefix, postfix);
            }
        }
    }

    private static TextSpan ComputeExpectedTypeofArgumentTextSpan(string source, string target, string prefix, string postfix)
    {
        return ComputeExpectedTextSpan(source, target[7..^1], $"{prefix}typeof(", $"){postfix}");
    }

    private static TextSpan ComputeExpectedTextSpan(string source, string target, string prefix, string postfix)
    {
        int startIndex = source.IndexOf($"{prefix}{target}{postfix}", StringComparison.InvariantCulture) + prefix.Length;

        return new(startIndex, target.Length);
    }
}
