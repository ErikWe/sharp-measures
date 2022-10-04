namespace SharpMeasures.Equatables;

using System.Collections.Generic;

public static partial class EquatableExtensions
{
    public static EquatableEnumerable<T> AsEquatable<T>(this IEnumerable<T> enumerable)
    {
        if (enumerable is EquatableEnumerable<T> equatableEnumerable)
        {
            return equatableEnumerable;
        }

        return new(enumerable);
    }
}
