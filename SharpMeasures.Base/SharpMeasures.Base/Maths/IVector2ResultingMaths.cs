namespace SharpMeasures.Maths;

/// <summary>Describes mathematical operations that result in a two-dimensional vector quantity <typeparamref name="TResult"/>.</summary>
/// <typeparam name="TResult">The two-dimensional vector quantity that is the result of the described mathematical operations.</typeparam>
public interface IVector2ResultingMaths<TResult> where TResult : IVector2Quantity<TResult>
{
    /// <summary>Computes the normalized <paramref name="vector"/>.</summary>
    /// <param name="vector">This two-dimensional vector quantity is normalized.</param>
    public abstract TResult Normalize(TResult vector);
}
