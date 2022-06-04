namespace SharpMeasures.Vector3Abstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity that supports the operation { <see langword="this"/> ∙ <typeparamref name="TFactor"/> },
/// resulting in a quantity of type <typeparamref name="TProduct"/>.</summary>
/// <typeparam name="TProduct">The three-dimensional vector quantity that represents the result of { <see langword="this"/> ∙ <typeparamref name="TFactor"/> }.</typeparam>
/// <typeparam name="TFactor">The three-dimensional vector quantity that represents the second factor of { <see langword="this"/> ∙
/// <typeparamref name="TFactor"/> }.</typeparam>
public interface IFactor3ScalarQuantity<out TProduct, in TFactor>
    : IScalarQuantity
    where TProduct : IVector3Quantity
    where TFactor : IVector3Quantity
{
    /// <summary>Computes { <see langword="this"/> ∙ <paramref name="factor"/> }.</summary>
    /// <param name="factor">The second factor of { <see langword="this"/> ∙ <paramref name="factor"/> }.</param>
    public abstract TProduct Multiply(TFactor factor);
}

/// <summary>Describes a scalar quantity that supports the operation { <typeparamref name="TSelf"/> ∙ <typeparamref name="TFactor"/> },
/// resulting in a quantity of type <typeparamref name="TProduct"/>.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TProduct">The three-dimensional vector quantity that represents the result of { <typeparamref name="TSelf"/> ∙
/// <typeparamref name="TFactor"/> }.</typeparam>
/// <typeparam name="TFactor">The three-dimensional vector quantity that represents the second factor of { <typeparamref name="TSelf"/> ∙
/// <typeparamref name="TFactor"/> }.</typeparam>
public interface IFactor3ScalarQuantity<in TSelf, out TProduct, in TFactor>
    : IFactor3ScalarQuantity<TProduct, TFactor>
    where TSelf : IFactor3ScalarQuantity<TSelf, TProduct, TFactor>
    where TProduct : IVector3Quantity
    where TFactor : IVector3Quantity
{
    /// <summary>Computes { <paramref name="a"/> ∙ <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }, a scalar quantity.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }, a three-dimensional vector quantity.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "False positive, available in base interface")]
    public static abstract TProduct operator *(TSelf a, TFactor b);

    /// <summary>Computes { <paramref name="a"/> ∙ <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }, a three-dimensional vector quantity.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }, a scalar quantity.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "False positive, available in base interface")]
    public static abstract TProduct operator *(TFactor a, TSelf b);
}
