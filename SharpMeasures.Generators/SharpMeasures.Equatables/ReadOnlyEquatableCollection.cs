namespace SharpMeasures.Equatables;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

public class ReadOnlyEquatableCollection<T> : EquatableEnumerable<T>, IReadOnlyCollection<T>, IEquatable<ReadOnlyEquatableCollection<T>>
{
    [SuppressMessage("Design", "CA1000", Justification = "Property")]
    new public static ReadOnlyEquatableCollection<T> Empty => new(Array.Empty<T>());

    public int Count => Items.Count;

    private IReadOnlyCollection<T> Items { get; }

    public ReadOnlyEquatableCollection(IReadOnlyCollection<T> items) : base(items)
    {
        Items = items;
    }

    public virtual bool Equals(ReadOnlyEquatableCollection<T>? other) => other is not null && Items.SequenceEqual(other.Items);
    public override bool Equals(object? obj) => obj is ReadOnlyEquatableCollection<T> other && Equals(other);

    public static bool operator ==(ReadOnlyEquatableCollection<T>? lhs, ReadOnlyEquatableCollection<T>? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(ReadOnlyEquatableCollection<T>? lhs, ReadOnlyEquatableCollection<T>? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => Items.GetSequenceHashCode();
}
