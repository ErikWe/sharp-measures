namespace SharpMeasures.Equatables;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EquatableList<T> : IReadOnlyList<T>, IEquatable<EquatableList<T>>
{
    public int Count => Items.Count;

    public T this[int index] => Items[index];

    private IReadOnlyList<T> Items { get; }

    public EquatableList(IReadOnlyList<T> items)
    {
        Items = items;
    }

    public virtual bool Equals(EquatableList<T>? other)
    {
        if (other is null)
        {
            return false;
        }

        return Items.SequenceEqual(other.Items);
    }

    public override bool Equals(object obj)
    {
        if (obj is EquatableList<T> other)
        {
            return Equals(other);
        }

        return false;
    }

    public static bool operator ==(EquatableList<T>? lhs, EquatableList<T>? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(EquatableList<T>? lhs, EquatableList<T>? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => Items.GetSequenceHashCode();

    public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
