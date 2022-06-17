namespace SharpMeasures.Equatables;

using System.Collections.Generic;

public static partial class EquatableExtensions
{
    public static EquatableDictionary<TKey, TValue> AsEquatable<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) => new(dictionary);
    public static ReadOnlyEquatableDictionary<TKey, TValue> AsReadOnlyEquatable<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> set) => new(set);
}
