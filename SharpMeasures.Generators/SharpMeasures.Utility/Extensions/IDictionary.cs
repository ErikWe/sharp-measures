namespace SharpMeasures;

using System.Collections.Generic;

public static partial class UtilityExtensions
{
    public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (dictionary.ContainsKey(key) is false)
        {
            dictionary.Add(key, value);

            return true;
        }

        return false;
    }
}
