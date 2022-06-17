namespace SharpMeasures.Maths;

using System.Numerics;

/// <summary>Describes mathematical operations that result in a three-dimensional vector quantity <typeparamref name="TResult"/>.</summary>
/// <typeparam name="TResult">The three-dimensional vector quantity that is the result of the described mathematical operations.</typeparam>
public interface IVector3ResultingMaths<TResult> where TResult : IVector3Quantity<TResult>
{
    /// <summary>Computes { <paramref name="a"/> ⨯ <paramref name="b"/> }.</summary>
    /// <typeparam name="TFactor1">The three-dimensional vector quantity that represents the first factor of { <paramref name="a"/> ⨯ <paramref name="b"/> }.</typeparam>
    /// <typeparam name="TFactor2">The three-dimensional vector quantity that represents the second factor of { <paramref name="a"/> ⨯ <paramref name="b"/> }.</typeparam>
    /// <param name="a">The first factor of { <paramref name="a"/> ⨯ <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ⨯ <paramref name="b"/> }.</param>
    public abstract TResult Cross<TFactor1, TFactor2>(TFactor1 a, TFactor2 b)
        where TFactor1 : IVector3Quantity
        where TFactor2 : IVector3Quantity;

    /// <summary>Computes the normalized <paramref name="vector"/>.</summary>
    /// <param name="vector">This three-dimensional vector quantity is normalized.</param>
    public abstract TResult Normalize(TResult vector);

    /// <summary>Transforms <paramref name="factor"/> according to <paramref name="transform"/>.</summary>
    /// <param name="factor">The quantity that is transformed according to <paramref name="transform"/>.</param>
    /// <param name="transform"><paramref name="factor"/> is transformed according to this transform.</param>
    public abstract TResult Transform(TResult factor, Matrix4x4 transform);
}
