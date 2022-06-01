namespace SharpMeasures;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static partial class UtilityExtensions
{
    public static int GetSequenceHashCode(this IEnumerable enumerable)
    {
        if (enumerable is null)
        {
            throw new ArgumentNullException(nameof(enumerable));
        }

        unchecked
        {
            int hashCode = (int)2166136261;

            foreach (object? item in enumerable)
            {
                int itemHashCode = item?.GetHashCode() ?? 0;

                for (int i = 0; i < 4; i++)
                {
                    hashCode = (hashCode * 16777619) ^ (itemHashCode >> 8 * i);
                }
            }

            return hashCode;
        }
    }

    public static int GetOrderIndependentHashCode<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        => ((IReadOnlyDictionary<TKey, TValue>)dictionary).GetOrderIndependentHashCode();

    public static int GetOrderIndependentHashCode<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary)
    {
        if (dictionary is null)
        {
            throw new ArgumentNullException(nameof(dictionary));
        }

        return dictionary.OrderBy(static (x) => x).GetSequenceHashCode();
    }

    public static int GetOrderIndependentHashCode<TKey>(this HashSet<TKey> hashSet)
    {
        if (hashSet is null)
        {
            throw new ArgumentNullException(nameof(hashSet));
        }

        return hashSet.OrderBy(static (x) => x).GetSequenceHashCode();
    }

    public static bool OrderIndependentSequenceEquals<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Dictionary<TKey, TValue> other)
        => ((IReadOnlyDictionary<TKey, TValue>)dictionary).OrderIndependentSequenceEquals(other);

    public static bool OrderIndependentSequenceEquals<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, IReadOnlyDictionary<TKey, TValue> other)
    {
        if (dictionary is null)
        {
            return other is null;
        }

        if (other is null)
        {
            return false;
        }

        if (dictionary.Count != other.Count)
        {
            return false;
        }

        foreach (var keyValuePair in dictionary)
        {
            if ((other.TryGetValue(keyValuePair.Key, out var value) && keyValuePair.Equals(value)) is false)
            {
                return false;
            }
        }

        return true;
    }

    public static bool OrderIndependentSequenceEquals<TKey>(this HashSet<TKey> hashSet, HashSet<TKey> other)
    {
        if (hashSet is null)
        {
            return other is null;
        }

        if (other is null)
        {
            return false;
        }

        if (hashSet.Count != other.Count)
        {
            return false;
        }

        foreach (var entry in hashSet)
        {
            if (other.Contains(entry) is false)
            {
                return false;
            }
        }

        return true;
    }
}
