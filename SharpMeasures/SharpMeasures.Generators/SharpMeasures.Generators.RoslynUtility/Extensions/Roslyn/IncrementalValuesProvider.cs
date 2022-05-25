namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Threading;

public static partial class RoslynUtilityExtensions
{
    public static IncrementalValuesProvider<T> WhereNotNull<T>(this IncrementalValuesProvider<T?> provider) where T : struct
        => provider.Where(static (x) => x is not null).Select(static (x, _) => x!.Value);

    public static IncrementalValuesProvider<T> WhereNotNull<T>(this IncrementalValuesProvider<T?> provider) where T : class
        => provider.Where(static (x) => x is not null)!;

    public static IncrementalValuesProvider<IResultWithDiagnostics<T>> WhereResult<T>(this IncrementalValuesProvider<IOptionalWithDiagnostics<T>> provider)
        => provider.Where(static (result) => result.HasResult).Select(static (result, _) => ResultWithDiagnostics.Construct(result.Result, result.Diagnostics));

    public static IncrementalValuesProvider<T> FilterAndReport<T>(this IncrementalValuesProvider<T> provider, IncrementalGeneratorInitializationContext context,
        Func<T, CancellationToken, IValidityWithDiagnostics> validityDelegate)
    {
        return provider.Select(filter).ReportDiagnostics(context);

        IOptionalWithDiagnostics<T> filter(T value, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return OptionalWithDiagnostics.Empty<T>();
            }

            var validity = validityDelegate(value, token);

            if (validity.IsInvalid)
            {
                return OptionalWithDiagnostics.Empty<T>(validity.Diagnostics);
            }

            return OptionalWithDiagnostics.Result(value, validity.Diagnostics);
        }
    }

    public static IncrementalValuesProvider<T> ExtractResults<T>(this IncrementalValuesProvider<IOptionalWithDiagnostics<T>> provider)
        => provider.WhereResult().Select(static (result, _) => result.Result);

    public static IncrementalValuesProvider<T> ExtractResults<T>(this IncrementalValuesProvider<IResultWithDiagnostics<T>> provider)
        => provider.Select(static (result, _) => result.Result);

    public static IncrementalValuesProvider<T> ReportDiagnostics<T>(this IncrementalValuesProvider<IOptionalWithDiagnostics<T>> provider,
        IncrementalGeneratorInitializationContext context)
    {
        context.ReportDiagnostics(provider);

        return provider.ExtractResults();
    }

    public static IncrementalValuesProvider<T> ReportDiagnostics<T>(this IncrementalValuesProvider<IResultWithDiagnostics<T>> provider,
        IncrementalGeneratorInitializationContext context)
    {
        context.ReportDiagnostics(provider);

        return provider.ExtractResults();
    }
}
