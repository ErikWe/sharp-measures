namespace SharpMeasures.Generators.DriverUtility;

using Microsoft.CodeAnalysis.Diagnostics;

using System.Collections.Immutable;

public sealed class CustomAnalyzerConfigOptions : AnalyzerConfigOptions
{
    public static CustomAnalyzerConfigOptions Empty { get; } = new CustomAnalyzerConfigOptions(ImmutableDictionary<string, string>.Empty);

    public ImmutableDictionary<string, string> Options { get; }

    public CustomAnalyzerConfigOptions(ImmutableDictionary<string, string> options)
    {
        Options = options;
    }

    public override bool TryGetValue(string key, out string value)
    {
        if (Options.TryGetValue(key, out var result))
        {
            value = result;
            return true;
        }

        value = string.Empty;
        return false;
    }
}
