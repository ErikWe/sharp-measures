﻿namespace SharpMeasures.Equatables;

using System.Collections.Generic;

public static partial class EquatableExtensions
{
    public static EquatableCollection<T> AsEquatable<T>(this ICollection<T> collection)
    {
        if (collection is EquatableCollection<T> equatableCollection)
        {
            return equatableCollection;
        }

        return new(collection);
    }

    public static ReadOnlyEquatableCollection<T> AsReadOnlyEquatable<T>(this IReadOnlyCollection<T> collection)
    {
        if (collection is ReadOnlyEquatableCollection<T> equatableCollection)
        {
            return equatableCollection;
        }

        return new(collection);
    }
}
