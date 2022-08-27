namespace SharpMeasures.Maths;

using System.Numerics;

/// <summary>Describes mathematical operations that result in a four-dimensional vector quantity <typeparamref name="TResult"/>.</summary>
/// <typeparam name="TResult">The four-dimensional vector quantity that is the result of the described mathematical operations.</typeparam>
public interface IVector4ResultingMaths<TResult> where TResult : IVector4Quantity<TResult>
{
    /// <summary>Computes the normalized <paramref name="vector"/>.</summary>
    /// <param name="vector">This four-dimensional vector quantity is normalized.</param>
    public abstract TResult Normalize(TResult vector);
}
