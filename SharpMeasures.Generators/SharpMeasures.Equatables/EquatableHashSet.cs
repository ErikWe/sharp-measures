namespace SharpMeasures.Equatables;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

public class EquatableHashSet<T> : HashSet<T>, IReadOnlyHashSet<T>, IEquatable<EquatableHashSet<T>>
{
    [SuppressMessage("Design", "CA1000", Justification = "Property")]
    public static EquatableHashSet<T> Empty => new();

    public EquatableHashSet() : base() { }
    public EquatableHashSet(IEnumerable<T> collection) : base(collection) { }
    public EquatableHashSet(IEqualityComparer<T> comparer) : base(comparer) { }
    public EquatableHashSet(SerializationInfo info, StreamingContext context) : base(info, context) { }
    public EquatableHashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer) : base(collection, comparer) { }

    public ReadOnlyEquatableHashSet<T> AsReadOnly() => new(this);

    public virtual bool Equals(EquatableHashSet<T>? other)
    {
        if (other is null || Count != other.Count)
        {
            return false;
        }

        if (Count is 0)
        {
            return true;
        }

        foreach (var item in this)
        {
            if (other.Contains(item) is false)
            {
                return false;
            }
        }

        return true;
    }

    public override bool Equals(object? obj) => obj is EquatableHashSet<T> other && Equals(other);

    public static bool operator ==(EquatableHashSet<T>? lhs, EquatableHashSet<T>? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(EquatableHashSet<T>? lhs, EquatableHashSet<T>? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => this.GetOrderIndependentHashCode();
}
