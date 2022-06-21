namespace SharpMeasures.Equatables;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

public class EquatableList<T> : List<T>, IReadOnlyList<T>, IEquatable<EquatableList<T>>
{
    [SuppressMessage("Design", "CA1000", Justification = "Property")]
    public static EquatableList<T> Empty => new();

    public EquatableList() : base() { }
    public EquatableList(int capacity) : base(capacity) { }
    public EquatableList(IEnumerable<T> collection) : base(collection) { }

    new public ReadOnlyEquatableList<T> AsReadOnly() => new(this);

    public virtual bool Equals(EquatableList<T>? other) => other is not null && this.SequenceEqual(other);
    public override bool Equals(object? obj) => obj is EquatableList<T> other && Equals(other);

    public static bool operator ==(EquatableList<T>? lhs, EquatableList<T>? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(EquatableList<T>? lhs, EquatableList<T>? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => this.GetSequenceHashCode();
}
