namespace SharpMeasures.Equatables;

using System;
using System.Collections;
using System.Collections.Generic;

public class EquatableDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>, IEquatable<EquatableDictionary<TKey, TValue>>
{
    public int Count => Items.Count;

    public TValue this[TKey key] => Items[key];

    public IEnumerable<TKey> Keys => Items.Keys;
    public IEnumerable<TValue> Values => Items.Values;

    private IReadOnlyDictionary<TKey, TValue> Items { get; }

    public EquatableDictionary(IReadOnlyDictionary<TKey, TValue> items)
    {
        Items = items;
    }

    public bool TryGetValue(TKey key, out TValue value) => Items.TryGetValue(key, out value);

    public bool ContainsKey(TKey key) => Items.ContainsKey(key);

    public virtual bool Equals(EquatableDictionary<TKey, TValue>? other)
    {
        if (other is null)
        {
            return false;
        }

        return Items.OrderIndependentSequenceEquals(other.Items);
    }

    public override bool Equals(object obj)
    {
        if (obj is EquatableDictionary<TKey, TValue> other)
        {
            return Equals(other);
        }

        return false;
    }

    public static bool operator ==(EquatableDictionary<TKey, TValue>? lhs, EquatableDictionary<TKey, TValue>? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(EquatableDictionary<TKey, TValue>? lhs, EquatableDictionary<TKey, TValue>? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => Items.GetOrderIndependentHashCode();

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => Items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
