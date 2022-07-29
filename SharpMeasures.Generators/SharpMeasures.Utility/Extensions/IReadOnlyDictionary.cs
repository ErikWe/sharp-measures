namespace SharpMeasures;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static partial class UtilityExtensions
{
    public static int GetOrderIndependentHashCode<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary)
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

    public static bool OrderIndependentSequenceEquals<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, IReadOnlyDictionary<TKey, TValue> other)
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
}
