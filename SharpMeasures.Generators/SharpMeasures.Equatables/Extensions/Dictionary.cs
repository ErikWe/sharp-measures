namespace SharpMeasures.Equatables;

using System.Collections.Generic;

public static partial class EquatableExtensions
{
    public static EquatableDictionary<TKey, TValue> AsEquatable<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        if (dictionary is EquatableDictionary<TKey, TValue> equatableDictionary)
        {
            return equatableDictionary;
        }

        return new(dictionary);
    }

    public static ReadOnlyEquatableDictionary<TKey, TValue> AsReadOnlyEquatable<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary)
    {
        if (dictionary is ReadOnlyEquatableDictionary<TKey, TValue> equatableDictionary)
        {
            return equatableDictionary;
        }

        return new(dictionary);
    }
}
