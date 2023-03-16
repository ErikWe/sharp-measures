namespace SharpMeasures.Equatables;

using System.Collections.Generic;

public static partial class EquatableExtensions
{
    public static EquatableList<T> AsEquatable<T>(this IList<T> list)
    {
        if (list is EquatableList<T> equatableList)
        {
            return equatableList;
        }

        return new(list);
    }

    public static ReadOnlyEquatableList<T> AsReadOnlyEquatable<T>(this IReadOnlyList<T> list)
    {
        if (list is ReadOnlyEquatableList<T> equatableList)
        {
            return equatableList;
        }

        return new(list);
    }
}
