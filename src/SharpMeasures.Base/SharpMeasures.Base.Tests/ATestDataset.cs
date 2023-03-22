namespace SharpMeasures.Tests;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class ATestDataset
{
    protected static HashSet<Type> ValueTupleTypes { get; } = new()
    {
        typeof(ValueTuple<>),
        typeof(ValueTuple<,>),
        typeof(ValueTuple<,,>),
        typeof(ValueTuple<,,,>),
        typeof(ValueTuple<,,,,>),
        typeof(ValueTuple<,,,,,>),
        typeof(ValueTuple<,,,,,,>),
        typeof(ValueTuple<,,,,,,,>)
    };
}

public abstract class ATestDataset<T> : ATestDataset, IEnumerable<object?[]>
{
    protected abstract IEnumerable<T> GetSamples();

    protected static IEnumerable<(T1, T2, T3)> Flatten<T1, T2, T3>(IEnumerable<((T1, T2), T3)> items)
    {
        foreach (var item in items)
        {
            yield return (item.Item1.Item1, item.Item1.Item2, item.Item2);
        }
    }

    protected static IEnumerable<(T1, T2, T3)> Flatten<T1, T2, T3>(IEnumerable<(T1, (T2, T3))> items)
    {
        foreach (var item in items)
        {
            yield return (item.Item1, item.Item2.Item1, item.Item2.Item2);
        }
    }

    protected static IEnumerable<(T1, T2, T3, T4)> Flatten<T1, T2, T3, T4>(IEnumerable<(T1, (T2, T3, T4))> items)
    {
        foreach (var item in items)
        {
            yield return (item.Item1, item.Item2.Item1, item.Item2.Item2, item.Item2.Item3);
        }
    }

    protected static IEnumerable<(T1, T1)> Permutate<T1>(IEnumerable<T1> items)
    {
        var firstEnumerator = items.GetEnumerator();

        while (firstEnumerator.MoveNext())
        {
            var secondEnumerator = items.GetEnumerator();

            while (secondEnumerator.MoveNext())
            {
                yield return (firstEnumerator.Current, secondEnumerator.Current);
            }
        }
    }

    protected static IEnumerable<(T1, T2)> Permutate<T1, T2>(IEnumerable<T1> firstItems, IEnumerable<T2> secondItems)
    {
        var firstEnumerator = firstItems.GetEnumerator();

        while (firstEnumerator.MoveNext())
        {
            var secondEnumerator = secondItems.GetEnumerator();

            while (secondEnumerator.MoveNext())
            {
                yield return (firstEnumerator.Current, secondEnumerator.Current);
            }
        }
    }

    protected static IEnumerable<(T1, T2, T3)> Permutate<T1, T2, T3>(IEnumerable<T1> firstItems, IEnumerable<T2> secondItems, IEnumerable<T3> thirdItems)
    {
        var firstEnumerator = firstItems.GetEnumerator();

        while (firstEnumerator.MoveNext())
        {
            var secondEnumerator = secondItems.GetEnumerator();

            while (secondEnumerator.MoveNext())
            {
                var thirdEnumerator = thirdItems.GetEnumerator();

                while (thirdEnumerator.MoveNext())
                {
                    yield return (firstEnumerator.Current, secondEnumerator.Current, thirdEnumerator.Current);
                }
            }
        }
    }

    private static IEnumerable<object?[]> Wrap(IEnumerable<T> items)
    {
        if (typeof(T).IsGenericType is false || ValueTupleTypes.Contains(typeof(T).GetGenericTypeDefinition()) is false)
        {
            return wrap();
        }

        return separateAndWrap();

        IEnumerable<object?[]> wrap()
        {
            foreach (var item in items)
            {
                yield return new object?[] { item };
            }
        }

        IEnumerable<object?[]> separateAndWrap()
        {
            var fields = typeof(T).GetFields();

            foreach (var item in items)
            {
                yield return fields.Select((field) => field.GetValue(item)).ToArray();
            }
        }
    }

    public IEnumerator<object?[]> GetEnumerator() => Wrap(GetSamples()).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
