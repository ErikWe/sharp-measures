namespace SharpMeasures.Equatables;

using System;
using System.Collections.Generic;

public class ReadOnlyEquatableCollection<T> : EquatableEnumerable<T>, IReadOnlyCollection<T>, IEquatable<ReadOnlyEquatableCollection<T>>
{
    new public static ReadOnlyEquatableCollection<T> Empty => new(Array.Empty<T>());

    public int Count => Items.Count;

    private IReadOnlyCollection<T> Items { get; }

    public ReadOnlyEquatableCollection(IReadOnlyCollection<T> items) : base(items)
    {
        Items = items;
    }

    public virtual bool Equals(ReadOnlyEquatableCollection<T>? other)
    {
        if (other is null || Count != other.Count)
        {
            return false;
        }

        if (Count is 0)
        {
            return true;
        }

        var thisEnumerator = GetEnumerator();
        var otherEnumerator = other.GetEnumerator();

        while (thisEnumerator.MoveNext() && otherEnumerator.MoveNext())
        {
            if ((thisEnumerator.Current is null && otherEnumerator.Current is not null) || (thisEnumerator.Current is not null && otherEnumerator.Current is null))
            {
                return false;
            }

            if (thisEnumerator.Current!.Equals(otherEnumerator.Current) is false)
            {
                return false;
            }
        }

        return true;
    }

    public override bool Equals(object? obj) => obj is ReadOnlyEquatableCollection<T> other && Equals(other);

    public static bool operator ==(ReadOnlyEquatableCollection<T>? lhs, ReadOnlyEquatableCollection<T>? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(ReadOnlyEquatableCollection<T>? lhs, ReadOnlyEquatableCollection<T>? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => Items.GetSequenceHashCode();
}
