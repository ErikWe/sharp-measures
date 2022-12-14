namespace SharpMeasures.Equatables;

using System;
using System.Collections;
using System.Collections.Generic;

public class EquatableCollection<T> : ICollection<T>, IReadOnlyCollection<T>, IEquatable<EquatableCollection<T>>
{
    public static EquatableCollection<T> Empty => new(new List<T>());

    private ICollection<T> Items { get; }

    public int Count => Items.Count;

    bool ICollection<T>.IsReadOnly => Items.IsReadOnly;

    public EquatableCollection(ICollection<T> items)
    {
        Items = items;
    }

    public ReadOnlyEquatableCollection<T> AsReadOnly() => new(this);

    public void Add(T item) => Items.Add(item);
    public bool Remove(T item) => Items.Remove(item);
    public void Clear() => Items.Clear();

    public bool Contains(T item) => Items.Contains(item);

    void ICollection<T>.CopyTo(T[] array, int arrayIndex) => Items.CopyTo(array, arrayIndex);

    public bool Equals(EquatableCollection<T>? other)
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

    public override bool Equals(object? obj) => obj is EquatableCollection<T> other && Equals(other);

    public static bool operator ==(EquatableCollection<T>? lhs, EquatableCollection<T>? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(EquatableCollection<T>? lhs, EquatableCollection<T>? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => Items.GetSequenceHashCode();

    public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();
}
