namespace SharpMeasures.Equatables;

using System;
using System.Collections.Generic;

public class EquatableList<T> : List<T>, IReadOnlyList<T>, IEquatable<EquatableList<T>>
{
    public static EquatableList<T> Empty => new();

    public EquatableList() : base() { }
    public EquatableList(int capacity) : base(capacity) { }
    public EquatableList(IEnumerable<T> collection) : base(collection) { }

    new public ReadOnlyEquatableList<T> AsReadOnly() => new(this);

    public virtual bool Equals(EquatableList<T>? other)
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

    public override bool Equals(object? obj) => obj is EquatableList<T> other && Equals(other);

    public static bool operator ==(EquatableList<T>? lhs, EquatableList<T>? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(EquatableList<T>? lhs, EquatableList<T>? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => this.GetSequenceHashCode();
}
