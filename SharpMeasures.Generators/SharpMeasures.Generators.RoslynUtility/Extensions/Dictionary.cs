namespace SharpMeasures.Generators;

using System;
using System.Collections.Generic;
using System.Linq;

public static class DictionaryExtensions
{
    public static Dictionary<TKey, TOut> Transform<TKey, TIn, TOut>(this Dictionary<TKey, TIn> dictionary, Func<TIn, TOut> transform)
    {
        return dictionary.ToDictionary(static (x) => x.Key, (x) => transform(x.Value));
    }

    public static Dictionary<TKey, TOut> Transform<TKey, TIn, TOut>(this IReadOnlyDictionary<TKey, TIn> dictionary, Func<TIn, TOut> transform)
    {
        return dictionary.ToDictionary(static (x) => x.Key, (x) => transform(x.Value));
    }
}
