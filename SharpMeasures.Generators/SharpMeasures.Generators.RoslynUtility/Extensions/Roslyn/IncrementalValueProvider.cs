namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System.Collections.Immutable;
using System.Threading;

public static partial class RoslynUtilityExtensions
{
    public static IncrementalValueProvider<T> ExtractResult<T>(this IncrementalValueProvider<IResultWithDiagnostics<T>> provider)
        => provider.Select(static (result, _) => result.Result);

    public static IncrementalValueProvider<T> ReportDiagnostics<T>(this IncrementalValueProvider<IResultWithDiagnostics<T>> provider,
        IncrementalGeneratorInitializationContext context)
    {
        context.ReportDiagnostics(provider);

        return provider.ExtractResult();
    }

    public static IncrementalValueProvider<ImmutableArray<T>> Concat<T>(this IncrementalValueProvider<ImmutableArray<T>> provider1, IncrementalValueProvider<ImmutableArray<T>> provider2)
    {
        return provider1.Combine(provider2).Select(concatArrays);

        static ImmutableArray<T> concatArrays((ImmutableArray<T> Left, ImmutableArray<T> Right) arrays, CancellationToken _)
        {
            return arrays.Left.AddRange(arrays.Right);
        }
    }

    public static IncrementalValueProvider<(T1, T2, T3)> Combine<T1, T2, T3>(this IncrementalValueProvider<T1> provider1, IncrementalValueProvider<T2> provider2, IncrementalValueProvider<T3> provider3)
    {
        return provider1.Combine(provider2).Combine(provider3).Flatten();
    }

    public static IncrementalValueProvider<(T1, T2, T3, T4)> Combine<T1, T2, T3, T4>(this IncrementalValueProvider<T1> provider1, IncrementalValueProvider<T2> provider2, IncrementalValueProvider<T3> provider3, IncrementalValueProvider<T4> provider4)
    {
        return provider1.Combine(provider2).Combine(provider3).Combine(provider4).Flatten();
    }

    public static IncrementalValueProvider<(T1, T2, T3, T4, T5)> Combine<T1, T2, T3, T4, T5>(this IncrementalValueProvider<T1> provider1, IncrementalValueProvider<T2> provider2, IncrementalValueProvider<T3> provider3, IncrementalValueProvider<T4> provider4, IncrementalValueProvider<T5> provider5)
    {
        return provider1.Combine(provider2).Combine(provider3).Combine(provider4).Combine(provider5).Flatten();
    }

    public static IncrementalValueProvider<(T1, T2, T3, T4, T5, T6)> Combine<T1, T2, T3, T4, T5, T6>(this IncrementalValueProvider<T1> provider1, IncrementalValueProvider<T2> provider2, IncrementalValueProvider<T3> provider3, IncrementalValueProvider<T4> provider4, IncrementalValueProvider<T5> provider5, IncrementalValueProvider<T6> provider6)
    {
        return provider1.Combine(provider2).Combine(provider3).Combine(provider4).Combine(provider5).Combine(provider6).Flatten();
    }

    public static IncrementalValueProvider<(T1, T2, T3)> Flatten<T1, T2, T3>(this IncrementalValueProvider<((T1, T2), T3)> provider)
    {
        return provider.Select(flatten);

        static (T1, T2, T3) flatten(((T1, T2), T3) tupleHierarchy, CancellationToken _)
        {
            return (tupleHierarchy.Item1.Item1, tupleHierarchy.Item1.Item2, tupleHierarchy.Item2);
        }
    }

    public static IncrementalValueProvider<(T1, T2, T3, T4)> Flatten<T1, T2, T3, T4>(this IncrementalValueProvider<(((T1, T2), T3), T4)> provider)
    {
        return provider.Select(flatten);

        static (T1, T2, T3, T4) flatten((((T1, T2), T3), T4) tupleHierarchy, CancellationToken _)
        {
            return (tupleHierarchy.Item1.Item1.Item1, tupleHierarchy.Item1.Item1.Item2, tupleHierarchy.Item1.Item2, tupleHierarchy.Item2);
        }
    }

    public static IncrementalValueProvider<(T1, T2, T3, T4, T5)> Flatten<T1, T2, T3, T4, T5>(this IncrementalValueProvider<((((T1, T2), T3), T4), T5)> provider)
    {
        return provider.Select(flatten);

        static (T1, T2, T3, T4, T5) flatten(((((T1, T2), T3), T4), T5) tupleHierarchy, CancellationToken _)
        {
            return (tupleHierarchy.Item1.Item1.Item1.Item1, tupleHierarchy.Item1.Item1.Item1.Item2, tupleHierarchy.Item1.Item1.Item2,
                tupleHierarchy.Item1.Item2, tupleHierarchy.Item2);
        }
    }

    public static IncrementalValueProvider<(T1, T2, T3, T4, T5, T6)> Flatten<T1, T2, T3, T4, T5, T6>(this IncrementalValueProvider<(((((T1, T2), T3), T4), T5), T6)> provider)
    {
        return provider.Select(flatten);

        static (T1, T2, T3, T4, T5, T6) flatten((((((T1, T2), T3), T4), T5), T6) tupleHierarchy, CancellationToken _)
        {
            return (tupleHierarchy.Item1.Item1.Item1.Item1.Item1, tupleHierarchy.Item1.Item1.Item1.Item1.Item2, tupleHierarchy.Item1.Item1.Item1.Item2,
                tupleHierarchy.Item1.Item1.Item2, tupleHierarchy.Item1.Item2, tupleHierarchy.Item2);
        }
    }
}
