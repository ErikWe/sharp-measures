namespace Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Utility;

internal static partial class Extensions
{
    public static IncrementalValuesProvider<T> ExtractResult<T>(this IncrementalValuesProvider<ResultWithDiagnostics<T>> provider)
        => provider.Select(static (result, token) => result.Result);
}
