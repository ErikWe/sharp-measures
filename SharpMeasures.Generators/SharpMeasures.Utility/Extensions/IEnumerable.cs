namespace SharpMeasures;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static partial class UtilityExtensions
{
    public static int GetSequenceHashCode(this IEnumerable enumerable)
    {
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

    public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs) => keyValuePairs.ToDictionary(static (x) => x.Key, static (x) => x.Value);
}
