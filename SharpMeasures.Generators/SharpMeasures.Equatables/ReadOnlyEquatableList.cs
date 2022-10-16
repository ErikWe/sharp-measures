namespace SharpMeasures.Equatables;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public class ReadOnlyEquatableList<T> : ReadOnlyEquatableCollection<T>, IReadOnlyList<T>, IEquatable<ReadOnlyEquatableList<T>>
{
    [SuppressMessage("Design", "CA1000", Justification = "Property")]
    new public static ReadOnlyEquatableList<T> Empty => new(Array.Empty<T>());

    public T this[int index] => Items[index];

    private IReadOnlyList<T> Items { get; }

    public ReadOnlyEquatableList(IReadOnlyList<T> items) : base(items)
    {
        Items = items;
    }

    public virtual bool Equals(ReadOnlyEquatableList<T>? other)
    {
        if (other is null || Count != other.Count)
        {
            return false;
        }

        if (Count is 0)
        {
            return true;
        }

        IEnumerator<T> thisEnumerator = GetEnumerator();
        IEnumerator<T> otherEnumerator = other.GetEnumerator();

        while (thisEnumerator.MoveNext() && otherEnumerator.MoveNext())
        {
            if (thisEnumerator.Current is null && otherEnumerator.Current is not null || thisEnumerator.Current is not null && otherEnumerator.Current is null)
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

    public override bool Equals(object? obj) => obj is ReadOnlyEquatableList<T> other && Equals(other);

    public static bool operator ==(ReadOnlyEquatableList<T>? lhs, ReadOnlyEquatableList<T>? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(ReadOnlyEquatableList<T>? lhs, ReadOnlyEquatableList<T>? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => Items.GetSequenceHashCode();
}
