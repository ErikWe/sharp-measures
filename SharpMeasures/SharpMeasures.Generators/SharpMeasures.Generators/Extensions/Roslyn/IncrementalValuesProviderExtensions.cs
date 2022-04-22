namespace Microsoft.CodeAnalysis;

internal static class IncrementalValuesProviderExtensions
{
    public static IncrementalValuesProvider<T> WhereNotNull<T>(this IncrementalValuesProvider<T?> provider) where T : struct
        => provider.Where(static (x) => x is not null).Select(static (x, _) => x!.Value);

    public static IncrementalValuesProvider<T> WhereNotNull<T>(this IncrementalValuesProvider<T?> provider) where T : class
        => provider.Where(static (x) => x is not null)!;
}
