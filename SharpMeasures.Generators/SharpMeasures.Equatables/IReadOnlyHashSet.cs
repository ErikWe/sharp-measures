namespace SharpMeasures;

using System.Collections.Generic;

public interface IReadOnlyHashSet<T> : IReadOnlyCollection<T>
{
    public abstract bool Contains(T item);
}
