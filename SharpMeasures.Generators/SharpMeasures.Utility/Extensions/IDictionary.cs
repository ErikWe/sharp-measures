namespace SharpMeasures;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static partial class UtilityExtensions
{
    public static int GetOrderIndependentHashCode<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        return orderAndFlattenDictionary().GetSequenceHashCode();

        IEnumerable orderAndFlattenDictionary()
        {
            foreach (KeyValuePair<TKey, TValue> pair in dictionary.OrderBy(static (x) => x.Key))
            {
                yield return pair.Key;
                yield return pair.Value;
            }
        }
    }

    public static bool OrderIndependentSequenceEquals<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IReadOnlyDictionary<TKey, TValue> other)
    {
        if (dictionary.Count != other.Count)
        {
            return false;
        }

        foreach (var keyValuePair in dictionary)
        {
            if ((other.TryGetValue(keyValuePair.Key, out var value) && (keyValuePair.Value?.Equals(value) ?? value is null)) is false)
            {
                return false;
            }
        }

        return true;
    }

    public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (dictionary.ContainsKey(key) is false)
        {
            dictionary.Add(key, value);

            return true;
        }

        return false;
    }

    public static IDictionary<TKey, TOut> Transform<TKey, TIn, TOut>(this IDictionary<TKey, TIn> dictionary, Func<TIn, TOut> transform)
    {
        Dictionary<TKey, TOut> transformedDictionary = new(dictionary.Count);

        foreach (var entry in dictionary)
        {
            transformedDictionary.Add(entry.Key, transform(entry.Value));
        }

        return transformedDictionary;
    }
}
