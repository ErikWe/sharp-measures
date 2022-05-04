namespace Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Utility;

internal static partial class Extensions
{
    public static IncrementalValueProvider<T> ExtractResult<T>(this IncrementalValueProvider<ResultWithDiagnostics<T>> provider)
        => provider.Select(static (result, token) => result.Result);
}
