namespace SharpMeasures.Equatables;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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
        if (other is null)
        {
            return false;
        }

        return Items.SequenceEqual(other.Items);
    }

    public override bool Equals(object obj)
    {
        if (obj is ReadOnlyEquatableList<T> other)
        {
            return Equals(other);
        }

        return false;
    }

    public static bool operator ==(ReadOnlyEquatableList<T>? lhs, ReadOnlyEquatableList<T>? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(ReadOnlyEquatableList<T>? lhs, ReadOnlyEquatableList<T>? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => Items.GetSequenceHashCode();
}
