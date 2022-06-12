namespace SharpMeasures.Equatables;

using System.Collections.Generic;

public static partial class EquatableExtensions
{
    public static EquatableList<T> AsEquatable<T>(this IList<T> list) => new(list);
    public static ReadOnlyEquatableList<T> AsReadOnlyEquatable<T>(this IReadOnlyList<T> list) => new(list);
}
