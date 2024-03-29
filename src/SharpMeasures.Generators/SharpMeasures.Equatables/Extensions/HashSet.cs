﻿namespace SharpMeasures.Equatables;

using System.Collections.Generic;

public static partial class EquatableExtensions
{
    public static EquatableHashSet<T> AsEquatable<T>(this HashSet<T> set) => new(set);
    public static ReadOnlyEquatableHashSet<T> AsReadOnlyEquatable<T>(this HashSet<T> set) => new(set);

    public static ReadOnlyEquatableHashSet<T> AsReadOnlyEquatable<T>(this IReadOnlyHashSet<T> set)
    {
        if (set is ReadOnlyEquatableHashSet<T> equatableSet)
        {
            return equatableSet;
        }

        return new(new HashSet<T>(set));
    }
}
