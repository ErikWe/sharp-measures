namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis.Diagnostics;

public static partial class RoslynUtilityExtensions
{
    public static string? GetValue(this AnalyzerConfigOptions options, string key)
    {
        _ = options.TryGetValue(key, out var value);

        return value;
    }
}
