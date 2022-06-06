namespace SharpMeasures.Equatables;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EquatableEnumerable<T> : IEnumerable<T>, IEquatable<EquatableEnumerable<T>>
{
    private IEnumerable<T> Items { get; }

    public EquatableEnumerable(IEnumerable<T> items)
    {
        Items = items;
    }

    public virtual bool Equals(EquatableEnumerable<T>? other)
    {
        if (other is null)
        {
            return false;
        }

        return Items.SequenceEqual(other.Items);
    }

    public override bool Equals(object obj)
    {
        if (obj is EquatableEnumerable<T> other)
        {
            return Equals(other);
        }

        return false;
    }

    public static bool operator ==(EquatableEnumerable<T>? lhs, EquatableEnumerable<T>? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(EquatableEnumerable<T>? lhs, EquatableEnumerable<T>? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => Items.GetSequenceHashCode();

    public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
