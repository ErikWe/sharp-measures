namespace SharpMeasures.Equatables;

using System;
using System.Collections;
using System.Collections.Generic;

public class EquatableHashSet<T> : IReadOnlyCollection<T>, IReadOnlyHashSet<T>, IEquatable<EquatableHashSet<T>>
{
    public int Count => Items.Count;

    private HashSet<T> Items { get; }

    public EquatableHashSet(HashSet<T> items)
    {
        Items = items;
    }

    public bool Contains(T item) => Items.Contains(item);

    public virtual bool Equals(EquatableHashSet<T>? other)
    {
        if (other is null)
        {
            return false;
        }

        return Items.SetEquals(other.Items);
    }

    public override bool Equals(object obj)
    {
        if (obj is EquatableHashSet<T> other)
        {
            return Equals(other);
        }

        return false;
    }

    public static bool operator ==(EquatableHashSet<T>? lhs, EquatableHashSet<T>? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(EquatableHashSet<T>? lhs, EquatableHashSet<T>? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => Items.GetOrderIndependentHashCode();

    public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
