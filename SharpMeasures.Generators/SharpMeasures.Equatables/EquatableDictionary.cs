namespace SharpMeasures.Equatables;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

public class EquatableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IEquatable<EquatableDictionary<TKey, TValue>>
{
    [SuppressMessage("Design", "CA1000", Justification = "Property")]
    public static EquatableDictionary<TKey, TValue> Empty => new();

    public EquatableDictionary() : base() { }
    public EquatableDictionary(int capacity) : base(capacity) { }
    public EquatableDictionary(IEqualityComparer<TKey> comparer) : base(comparer) { }
    public EquatableDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary) { }
    public EquatableDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer) { }
    public EquatableDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }
    public EquatableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer) { }

    public ReadOnlyEquatableDictionary<TKey, TValue> AsReadOnly() => new(this);

    public virtual bool Equals(EquatableDictionary<TKey, TValue>? other) => other is not null && this.OrderIndependentSequenceEquals(other);
    public override bool Equals(object? obj) => obj is EquatableDictionary<TKey, TValue> other && Equals(other);

    public static bool operator ==(EquatableDictionary<TKey, TValue>? lhs, EquatableDictionary<TKey, TValue>? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(EquatableDictionary<TKey, TValue>? lhs, EquatableDictionary<TKey, TValue>? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => this.GetOrderIndependentHashCode();
}
