namespace SharpMeasures.Equatables;

using System;
using System.Collections;
using System.Collections.Generic;

public class ReadOnlyEquatableDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>, IEquatable<ReadOnlyEquatableDictionary<TKey, TValue>>
{
    public static ReadOnlyEquatableDictionary<TKey, TValue> Empty => new(new Dictionary<TKey, TValue>());

    public int Count => Items.Count;

    public TValue this[TKey key] => Items[key];

    public IEnumerable<TKey> Keys => Items.Keys;
    public IEnumerable<TValue> Values => Items.Values;

    private IReadOnlyDictionary<TKey, TValue> Items { get; }

    public ReadOnlyEquatableDictionary(IReadOnlyDictionary<TKey, TValue> items)
    {
        Items = items;
    }

    public ReadOnlyEquatableDictionary(IDictionary<TKey, TValue> items)
    {
        Items = new ReadOnlyWrappedDictionary<TKey, TValue>(items);
    }

    public ReadOnlyEquatableDictionary(Dictionary<TKey, TValue> items) : this((IReadOnlyDictionary<TKey, TValue>)items) { }

    public bool TryGetValue(TKey key, out TValue value) => Items.TryGetValue(key, out value);
    public bool ContainsKey(TKey key) => Items.ContainsKey(key);

    public virtual bool Equals(ReadOnlyEquatableDictionary<TKey, TValue>? other) => other is not null && Items.OrderIndependentSequenceEquals(other.Items);
    public override bool Equals(object? obj) => obj is ReadOnlyEquatableDictionary<TKey, TValue> other && Equals(other);

    public static bool operator ==(ReadOnlyEquatableDictionary<TKey, TValue>? lhs, ReadOnlyEquatableDictionary<TKey, TValue>? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(ReadOnlyEquatableDictionary<TKey, TValue>? lhs, ReadOnlyEquatableDictionary<TKey, TValue>? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => Items.GetOrderIndependentHashCode();

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => Items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
