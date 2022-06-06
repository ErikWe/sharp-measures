namespace SharpMeasures.Equatables;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EquatableCollection<T> : IReadOnlyCollection<T>, IEquatable<EquatableCollection<T>>
{
    public int Count => Items.Count;

    private IReadOnlyCollection<T> Items { get; }

    public EquatableCollection(IReadOnlyCollection<T> items)
    {
        Items = items;
    }

    public virtual bool Equals(EquatableCollection<T>? other)
    {
        if (other is null)
        {
            return false;
        }

        return Items.SequenceEqual(other.Items);
    }

    public override bool Equals(object obj)
    {
        if (obj is EquatableCollection<T> other)
        {
            return Equals(other);
        }

        return false;
    }

    public static bool operator ==(EquatableCollection<T>? lhs, EquatableCollection<T>? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(EquatableCollection<T>? lhs, EquatableCollection<T>? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => Items.GetSequenceHashCode();

    public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
