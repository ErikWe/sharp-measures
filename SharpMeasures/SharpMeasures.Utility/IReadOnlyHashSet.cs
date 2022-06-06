namespace SharpMeasures;

public interface IReadOnlyHashSet<T>
{
    public abstract int Count { get; }
    public abstract bool Contains(T item);
}
