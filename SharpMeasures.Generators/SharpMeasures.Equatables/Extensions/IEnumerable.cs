namespace SharpMeasures.Equatables;

using System.Collections.Generic;

public static partial class EquatableExtensions
{
    public static EquatableEnumerable<T> AsEquatable<T>(this IEnumerable<T> enumerable) => new(enumerable);
}
