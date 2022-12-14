namespace SharpMeasures;

using System.Collections;
using System.Collections.Generic;

public static partial class UtilityExtensions
{
    public static int GetSequenceHashCode(this IEnumerable enumerable)
    {
        unchecked
        {
            var hashCode = (int)2166136261;

            foreach (var item in enumerable)
            {
                var itemHashCode = item?.GetHashCode() ?? 0;

                for (var i = 0; i < 4; i++)
                {
                    hashCode = (hashCode * 16777619) ^ (itemHashCode >> (8 * i));
                }
            }

            return hashCode;
        }
    }

    public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
    {
        Dictionary<TKey, TValue> dictionary = new();

        foreach (var keyValuePair in keyValuePairs)
        {
            dictionary.Add(keyValuePair.Key, keyValuePair.Value);
        }

        return dictionary;
    }
}
