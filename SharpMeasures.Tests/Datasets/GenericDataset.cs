namespace ErikWe.SharpMeasures.Tests.Datasets;

using System.Collections;
using System.Collections.Generic;

public static class GenericDataset
{
    public static IEnumerable<object?[]> Enumerate(IEnumerator<object?[]>[] iterators)
    {
        List<object?[]>[] elements = new List<object?[]>[iterators.Length];
        int longestLength = 0;

        for (int i = 0; i < iterators.Length; i++)
        {
            elements[i] = new List<object?[]>();

            while (iterators[i].MoveNext())
            {
                elements[i].Add(iterators[i].Current);
            }

            if (elements[i].Count > longestLength)
            {
                longestLength = elements[i].Count;
            }
        }

        for (int i = 0; i < longestLength; i++)
        {
            yield return Compose(elements, i, 1);
            yield return Compose(elements, i, 2);
            yield return Compose(elements, i, 3);
            yield return Compose(elements, i, 5);
        }
    }

    private static object?[] Compose(List<object?[]>[] elements, int iteration, int bias)
    {
        List<object?> list = new();

        for (int j = 0; j < elements.Length; j++)
        {
            list.AddRange(elements[j][(iteration + j) * bias % elements[j].Count]);
        }

        return list.ToArray();
    }
}

public class GenericDataset<T1, T2> : IEnumerable<object?[]>
    where T1 : IEnumerable<object?[]>, new()
    where T2 : IEnumerable<object?[]>, new()
{
    public IEnumerator<object?[]> GetEnumerator()
    {
        IEnumerator<object?[]>[] iterators = new IEnumerator<object?[]>[]
        {
            new T1().GetEnumerator(),
            new T2().GetEnumerator()
        };

        return GenericDataset.Enumerate(iterators).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class GenericDataset<T1, T2, T3> : IEnumerable<object?[]>
    where T1 : IEnumerable<object?[]>, new()
    where T2 : IEnumerable<object?[]>, new()
    where T3 : IEnumerable<object?[]>, new()
{
    public IEnumerator<object?[]> GetEnumerator()
    {
        IEnumerator<object?[]>[] iterators = new IEnumerator<object?[]>[]
        {
            new T1().GetEnumerator(),
            new T2().GetEnumerator(),
            new T3().GetEnumerator()
        };

        return GenericDataset.Enumerate(iterators).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class GenericDataset<T1, T2, T3, T4> : IEnumerable<object?[]>
    where T1 : IEnumerable<object?[]>, new()
    where T2 : IEnumerable<object?[]>, new()
    where T3 : IEnumerable<object?[]>, new()
    where T4 : IEnumerable<object?[]>, new()
{
    public IEnumerator<object?[]> GetEnumerator()
    {
        IEnumerator<object?[]>[] iterators = new IEnumerator<object?[]>[]
        {
            new T1().GetEnumerator(),
            new T2().GetEnumerator(),
            new T3().GetEnumerator(),
            new T4().GetEnumerator()
        };

        return GenericDataset.Enumerate(iterators).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class GenericDataset<T1, T2, T3, T4, T5> : IEnumerable<object?[]>
    where T1 : IEnumerable<object?[]>, new()
    where T2 : IEnumerable<object?[]>, new()
    where T3 : IEnumerable<object?[]>, new()
    where T4 : IEnumerable<object?[]>, new()
    where T5 : IEnumerable<object?[]>, new()
{
    public IEnumerator<object?[]> GetEnumerator()
    {
        IEnumerator<object?[]>[] iterators = new IEnumerator<object?[]>[]
        {
            new T1().GetEnumerator(),
            new T2().GetEnumerator(),
            new T3().GetEnumerator(),
            new T4().GetEnumerator(),
            new T5().GetEnumerator()
        };

        return GenericDataset.Enumerate(iterators).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}