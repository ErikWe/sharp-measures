namespace SharpMeasures.Vector3Abstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a three-dimensional vector quantity as { <typeparamref name="TFactor1"/> ⨯ <typeparamref name="TFactor2"/> }.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TFactor1">The three-dimensional vector quantity that represents the first factor of { <typeparamref name="TFactor1"/> ⨯
/// <typeparamref name="TFactor2"/> }.</typeparam>
/// <typeparam name="TFactor2">The three-dimensional vector quantity that represents the second factor of { <typeparamref name="TFactor1"/> ∙
/// <typeparamref name="TFactor2"/> }.</typeparam>
public interface ICrossProductVector3Quantity<out TSelf, in TFactor1, in TFactor2> :
    IVector3Quantity
    where TSelf : ICrossProductVector3Quantity<TSelf, TFactor1, TFactor2>
    where TFactor1 : IVector3Quantity
    where TFactor2 : IVector3Quantity
{
    /// <summary>Computes { <paramref name="a"/> ⨯ <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> ⨯ <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ⨯ <paramref name="b"/> }.</param>
    [SuppressMessage("Design", "CA1000")]
    public static abstract TSelf From(TFactor1 a, TFactor2 b);

    /// <summary>Computes { <paramref name="a"/> ⨯ <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> ⨯ <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ⨯ <paramref name="b"/> }.</param>
    [SuppressMessage("Design", "CA1000")]
    public static abstract TSelf From(TFactor2 a, TFactor1 b);
}
