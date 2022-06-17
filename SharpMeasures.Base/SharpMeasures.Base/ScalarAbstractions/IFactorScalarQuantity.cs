namespace SharpMeasures.ScalarAbstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity that supports the operation { <see langword="this"/> ∙ <typeparamref name="TFactor"/> }, resulting in a quantity of type
/// <typeparamref name="TProduct"/>.</summary>
/// <typeparam name="TProduct">The scalar quantity that represents the result of { <see langword="this"/> ∙ <typeparamref name="TFactor"/> }.</typeparam>
/// <typeparam name="TFactor">The scalar quantity that represents the second factor of { <see langword="this"/> ∙ <typeparamref name="TFactor"/> }.</typeparam>
public interface IFactorScalarQuantity<out TProduct, in TFactor>
    : IScalarQuantity
    where TProduct : IScalarQuantity
    where TFactor : IScalarQuantity
{
    /// <summary>Computes { <see langword="this"/> ∙ <paramref name="factor"/> }.</summary>
    /// <param name="factor">The second factor of { <see langword="this"/> ∙ <paramref name="factor"/> }.</param>
    public abstract TProduct Multiply(TFactor factor);
}

/// <summary>Describes a scalar quantity that supports the operation { <typeparamref name="TSelf"/> ∙ <typeparamref name="TFactor"/> }, resulting in a quantity of type
/// <typeparamref name="TProduct"/>.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TProduct">The scalar quantity that represents the result of { <typeparamref name="TSelf"/> ∙ <typeparamref name="TFactor"/> }.</typeparam>
/// <typeparam name="TFactor">The scalar quantity that represents the second factor of { <typeparamref name="TSelf"/> ∙ <typeparamref name="TFactor"/> }.</typeparam>
public interface IFactorScalarQuantity<in TSelf, out TProduct, in TFactor>
    : IFactorScalarQuantity<TProduct, TFactor>
    where TSelf : IFactorScalarQuantity<TSelf, TProduct, TFactor>
    where TProduct : IScalarQuantity
    where TFactor : IScalarQuantity
{
    /// <summary>Computes { <paramref name="x"/> ∙ <paramref name="y"/> }.</summary>
    /// <param name="x">The first factor of { <paramref name="x"/> ∙ <paramref name="y"/> }.</param>
    /// <param name="y">The second factor of { <paramref name="x"/> ∙ <paramref name="y"/> }.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "False positive, available in base interface")]
    public static abstract TProduct operator *(TSelf x, TFactor y);

    /// <summary>Computes { <paramref name="x"/> ∙ <paramref name="y"/> }.</summary>
    /// <param name="x">The first factor of { <paramref name="x"/> ∙ <paramref name="y"/> }.</param>
    /// <param name="y">The second factor of { <paramref name="x"/> ∙ <paramref name="y"/> }.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "False positive, available in base interface")]
    public static abstract TProduct operator *(TFactor x, TSelf y);
}
