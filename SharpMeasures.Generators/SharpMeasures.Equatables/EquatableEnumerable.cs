﻿namespace SharpMeasures.Equatables;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

public class EquatableEnumerable<T> : IEnumerable<T>, IEquatable<EquatableEnumerable<T>>
{
    [SuppressMessage("Design", "CA1000", Justification = "Property")]
    public static EquatableEnumerable<T> Empty => new(Array.Empty<T>());

    private IEnumerable<T> Items { get; }

    public EquatableEnumerable(IEnumerable<T> items)
    {
        Items = items;
    }

    public virtual bool Equals(EquatableEnumerable<T>? other) => other is not null && Items.SequenceEqual(other.Items);
    public override bool Equals(object? obj) => obj is EquatableEnumerable<T> other && Equals(other);

    public static bool operator ==(EquatableEnumerable<T>? lhs, EquatableEnumerable<T>? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(EquatableEnumerable<T>? lhs, EquatableEnumerable<T>? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => Items.GetSequenceHashCode();

    public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
