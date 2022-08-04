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

    private static TextSpan ComputeExpectedTextSpan(string source, string target, string prefix, string postfix)
    {
        int startIndex = source.IndexOf($"{prefix}{target}{postfix}", StringComparison.InvariantCulture) + prefix.Length;

        return new(startIndex, target.Length);
    }
}
