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

    public static NamedTypePopulation<T> AsNamedTypePopulation<T>(this Dictionary<NamedType, T> dictionary)
    {
        return new((IReadOnlyDictionary<NamedType, T>)dictionary);
    }

    public static NamedTypePopulation<T> AsNamedTypePopulation<T>(this IReadOnlyDictionary<NamedType, T> dictionary)
    {
        return new(dictionary);
    }

    public static NamedTypePopulation<TOut> AsNamedTypePopulation<TIn, TOut>(this Dictionary<NamedType, TIn> dictionary, Func<TIn, TOut> transform)
    {
        return new(dictionary.Transform(transform));
    }

    public static NamedTypePopulation<TOut> AsNamedTypePopulation<TIn, TOut>(this IReadOnlyDictionary<NamedType, TIn> dictionary, Func<TIn, TOut> transform)
    {
        return new(dictionary.Transform(transform));
    }
}
