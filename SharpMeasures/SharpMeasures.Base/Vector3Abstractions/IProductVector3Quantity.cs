namespace SharpMeasures.Vector3Abstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a three-dimensional vector quantity as { <typeparamref name="TFactor1"/> ∙ <typeparamref name="TFactor2"/> }.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TFactor1">The three-dimensional vector quantity that represents the first factor of { <typeparamref name="TFactor1"/> ∙
/// <typeparamref name="TFactor2"/> }.</typeparam>
/// <typeparam name="TFactor2">The scalar quantity that represents the second factor of { <typeparamref name="TFactor1"/> ∙ <typeparamref name="TFactor2"/> }.</typeparam>
public interface IProductVector3Quantity<out TSelf, in TFactor1, in TFactor2> :
    IVector3Quantity
    where TSelf : IProductVector3Quantity<TSelf, TFactor1, TFactor2>
    where TFactor1 : IVector3Quantity
    where TFactor2 : IScalarQuantity
{
    /// <summary>Computes { <paramref name="a"/> ∙ <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }, a three-dimensional vector quantity.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }, a scalar quantity.</param>
    [SuppressMessage("Design", "CA1000")]
    public static abstract TSelf From(TFactor1 a, TFactor2 b);

    /// <summary>Computes { <paramref name="b"/> ∙ <paramref name="a"/> }.</summary>
    /// <param name="b">The first factor of { <paramref name="b"/> ∙ <paramref name="a"/> }, a scalar quantity.</param>
    /// <param name="a">The second factor of { <paramref name="b"/> ∙ <paramref name="a"/> }, a three-dimensional vector quantity.</param>
    [SuppressMessage("Design", "CA1000")]
    public static abstract TSelf From(TFactor2 b, TFactor1 a);
}
