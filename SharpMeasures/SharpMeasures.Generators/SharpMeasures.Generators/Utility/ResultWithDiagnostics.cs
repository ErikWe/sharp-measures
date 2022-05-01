namespace SharpMeasures.Generators.Utility;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

internal readonly record struct ResultWithDiagnostics<T>(T Result, IEnumerable<Diagnostic> Diagnostics)
{
    public static IncrementalValuesProvider<T> ExtractResult(IncrementalValuesProvider<ResultWithDiagnostics<T>> provider)
        => provider.Select(static (result, token) => result.Result);

    public static IncrementalValueProvider<T> ExtractResult(IncrementalValueProvider<ResultWithDiagnostics<T>> provider)
        => provider.Select(static (result, token) => result.Result);
}