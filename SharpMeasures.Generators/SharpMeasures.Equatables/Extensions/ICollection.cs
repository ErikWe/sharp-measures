namespace SharpMeasures.Equatables;

using System.Collections.Generic;

public static partial class EquatableExtensions
{
    public static EquatableCollection<T> AsEquatable<T>(this ICollection<T> collection) => new(collection);
    public static ReadOnlyEquatableCollection<T> AsReadOnlyEquatable<T>(this IReadOnlyCollection<T> collection) => new(collection);
}
