﻿namespace SharpMeasures.Generators;

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

    public static IncrementalValuesProvider<(T1, T2, T3)> Combine<T1, T2, T3>(this IncrementalValuesProvider<T1> provider1, IncrementalValueProvider<T2> provider2,
        IncrementalValueProvider<T3> provider3)
    {
        return provider1.Combine(provider2).Combine(provider3).Flatten();
    }

    public static IncrementalValuesProvider<(T1, T2, T3, T4)> Combine<T1, T2, T3, T4>(this IncrementalValuesProvider<T1> provider1, IncrementalValueProvider<T2> provider2,
        IncrementalValueProvider<T3> provider3, IncrementalValueProvider<T4> provider4)
    {
        return provider1.Combine(provider2).Combine(provider3).Combine(provider4).Flatten();
    }

    public static IncrementalValuesProvider<(T1, T2, T3, T4, T5)> Combine<T1, T2, T3, T4, T5>(this IncrementalValuesProvider<T1> provider1,
        IncrementalValueProvider<T2> provider2, IncrementalValueProvider<T3> provider3, IncrementalValueProvider<T4> provider4, IncrementalValueProvider<T5> provider5)
    {
        return provider1.Combine(provider2).Combine(provider3).Combine(provider4).Combine(provider5).Flatten();
    }

    public static IncrementalValuesProvider<(T1, T2, T3)> Flatten<T1, T2, T3>(this IncrementalValuesProvider<((T1, T2), T3)> provider)
    {
        return provider.Select(flatten);

        static (T1, T2, T3) flatten(((T1, T2), T3) tupleHierarchy, CancellationToken _)
        {
            return (tupleHierarchy.Item1.Item1, tupleHierarchy.Item1.Item2, tupleHierarchy.Item2);
        }
    }

    public static IncrementalValuesProvider<(T1, T2, T3, T4)> Flatten<T1, T2, T3, T4>(this IncrementalValuesProvider<(((T1, T2), T3), T4)> provider)
    {
        return provider.Select(flatten);

        static (T1, T2, T3, T4) flatten((((T1, T2), T3), T4) tupleHierarchy, CancellationToken _)
        {
            return (tupleHierarchy.Item1.Item1.Item1, tupleHierarchy.Item1.Item1.Item2, tupleHierarchy.Item1.Item2, tupleHierarchy.Item2);
        }
    }

    public static IncrementalValuesProvider<(T1, T2, T3, T4, T5)> Flatten<T1, T2, T3, T4, T5>(this IncrementalValuesProvider<((((T1, T2), T3), T4), T5)> provider)
    {
        return provider.Select(flatten);

        static (T1, T2, T3, T4, T5) flatten(((((T1, T2), T3), T4), T5) tupleHierarchy, CancellationToken _)
        {
            return (tupleHierarchy.Item1.Item1.Item1.Item1, tupleHierarchy.Item1.Item1.Item1.Item2, tupleHierarchy.Item1.Item1.Item2,
                tupleHierarchy.Item1.Item2, tupleHierarchy.Item2);
        }
    }

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