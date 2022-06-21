namespace SharpMeasures.Equatables;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public class ReadOnlyEquatableHashSet<T> : ReadOnlyEquatableCollection<T>, IReadOnlyHashSet<T>, IEquatable<ReadOnlyEquatableHashSet<T>>
{
    [SuppressMessage("Design", "CA1000", Justification = "Property")]
    new public static ReadOnlyEquatableHashSet<T> Empty => new(new HashSet<T>());

    private HashSet<T> Items { get; }

    public ReadOnlyEquatableHashSet(HashSet<T> items) : base(items)
    {
        Items = items;
    }

    public bool Contains(T item) => Items.Contains(item);

    public virtual bool Equals(ReadOnlyEquatableHashSet<T>? other) => other is not null && Items.SetEquals(other.Items);
    public override bool Equals(object? obj) => obj is ReadOnlyEquatableHashSet<T> other && Equals(other);

    public static bool operator ==(ReadOnlyEquatableHashSet<T>? lhs, ReadOnlyEquatableHashSet<T>? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(ReadOnlyEquatableHashSet<T>? lhs, ReadOnlyEquatableHashSet<T>? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => Items.GetOrderIndependentHashCode();
}
