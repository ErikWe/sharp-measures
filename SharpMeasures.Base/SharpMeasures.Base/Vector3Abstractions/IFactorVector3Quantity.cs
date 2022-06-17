namespace SharpMeasures.Vector3Abstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a three-dimensional vector quantity that supports the operation { <see langword="this"/> ∙ <typeparamref name="TFactor"/> },
/// resulting in a quantity of type <typeparamref name="TProduct"/>.</summary>
/// <typeparam name="TProduct">The three-dimensional vector quantity that represents the result of { <see langword="this"/> ∙ <typeparamref name="TFactor"/> }.</typeparam>
/// <typeparam name="TFactor">The scalar quantity that represents the second factor of { <see langword="this"/> ∙ <typeparamref name="TFactor"/> }.</typeparam>
public interface IFactorVector3Quantity<out TProduct, in TFactor>
    : IVector3Quantity
    where TProduct : IVector3Quantity
    where TFactor : IScalarQuantity
{
    /// <summary>Computes { <see langword="this"/> ∙ <paramref name="factor"/> }.</summary>
    /// <param name="factor">The second factor of { <see langword="this"/> ∙ <paramref name="factor"/> }.</param>
    public abstract TProduct Multiply(TFactor factor);
}

/// <summary>Describes a three-dimensional vector quantity that supports the operation { <typeparamref name="TSelf"/> ∙ <typeparamref name="TFactor"/> },
/// resulting in a quantity of type <typeparamref name="TProduct"/>.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TProduct">The three-dimensional vector quantity that represents the result of { <typeparamref name="TSelf"/> ∙
/// <typeparamref name="TFactor"/> }.</typeparam>
/// <typeparam name="TFactor">The scalar quantity that represents the second factor of { <typeparamref name="TSelf"/> ∙ <typeparamref name="TFactor"/> }.</typeparam>
public interface IFactorVector3Quantity<in TSelf, out TProduct, in TFactor>
    : IFactorVector3Quantity<TProduct, TFactor>
    where TSelf : IFactorVector3Quantity<TSelf, TProduct, TFactor>
    where TProduct : IVector3Quantity
    where TFactor : IScalarQuantity
{
    /// <summary>Computes { <paramref name="a"/> ∙ <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }, a three-dimensional vector quantity.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }, a scalar quantity.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "False positive, available in base interface")]
    public static abstract TProduct operator *(TSelf a, TFactor b);

    /// <summary>Computes { <paramref name="a"/> ∙ <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }, a scalar quantity.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }, a three-dimensional vector quantity.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "False positive, available in base interface")]
    public static abstract TProduct operator *(TFactor a, TSelf b);
}
