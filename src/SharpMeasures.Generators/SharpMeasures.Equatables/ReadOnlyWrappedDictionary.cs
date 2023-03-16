namespace SharpMeasures.Equatables;

using System.Collections;
using System.Collections.Generic;

internal readonly record struct ReadOnlyWrappedDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
{
    private IDictionary<TKey, TValue> WrappedDictionary { get; }

    public int Count => WrappedDictionary.Count;

    public TValue this[TKey key] => WrappedDictionary[key];

    public IEnumerable<TKey> Keys => WrappedDictionary.Keys;
    public IEnumerable<TValue> Values => WrappedDictionary.Values;

    public ReadOnlyWrappedDictionary(IDictionary<TKey, TValue> wrappedDictionary)
    {
        WrappedDictionary = wrappedDictionary;
    }

    public bool TryGetValue(TKey key, out TValue value) => WrappedDictionary.TryGetValue(key, out value);
    public bool ContainsKey(TKey key) => WrappedDictionary.ContainsKey(key);

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => WrappedDictionary.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
