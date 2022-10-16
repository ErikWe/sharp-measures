namespace SharpMeasures;

using System;
using System.Collections.Generic;

public static partial class UtilityExtensions
{
    public static int GetOrderIndependentHashCode<TKey, TValue>(this Dictionary<TKey, TValue> dictionary) => ((IDictionary<TKey, TValue>)dictionary).GetOrderIndependentHashCode();
    public static bool OrderIndependentSequenceEquals<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, IReadOnlyDictionary<TKey, TValue> other) => ((IDictionary<TKey, TValue>) dictionary).OrderIndependentSequenceEquals(other);

    public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value) => ((IDictionary<TKey, TValue>)dictionary).TryAdd(key, value);

    public static IDictionary<TKey, TOut> Transform<TKey, TIn, TOut>(this Dictionary<TKey, TIn> dictionary, Func<TIn, TOut> transform) => ((IDictionary<TKey, TIn>)dictionary).Transform(transform);
}
