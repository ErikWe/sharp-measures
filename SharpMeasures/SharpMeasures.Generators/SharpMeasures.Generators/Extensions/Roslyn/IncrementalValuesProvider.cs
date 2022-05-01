namespace Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Utility;

internal static partial class Extensions
{
    public static IncrementalValuesProvider<T> WhereNotNull<T>(this IncrementalValuesProvider<T?> provider) where T : struct
        => provider.Where(static (x) => x is not null).Select(static (x, _) => x!.Value);

    public static IncrementalValuesProvider<T> WhereNotNull<T>(this IncrementalValuesProvider<T?> provider) where T : class
        => provider.Where(static (x) => x is not null)!;

    public static IncrementalValuesProvider<T> ExtractResult<T>(this IncrementalValuesProvider<ResultWithDiagnostics<T>> provider)
        => provider.Select(static (result, token) => result.Result);
}
