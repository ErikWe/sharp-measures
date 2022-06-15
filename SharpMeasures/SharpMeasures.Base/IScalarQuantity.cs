namespace SharpMeasures;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity, having only magnitude.</summary>
public interface IScalarQuantity
{
    /// <summary>The magnitude of <see langword="this"/>.</summary>
    public abstract Scalar Magnitude { get; }

    /// <summary>Computes { <see langword="this"/> ∙ <paramref name="factor"/> }.</summary>
    /// <typeparam name="TFactor">The scalar quantity that represents the second factor of { <see langword="this"/> ∙ <paramref name="factor"/> }.</typeparam>
    /// <param name="factor">The second factor of { <see langword="this"/> ∙ <paramref name="factor"/> }.</param>
    public abstract Unhandled Multiply<TFactor>(TFactor factor) where TFactor : IScalarQuantity;

    /// <summary>Computes { <see langword="this"/> / <paramref name="divisor"/> }.</summary>
    /// <typeparam name="TDivisor">The scalar quantity that represents the divisor of { <see langword="this"/> / <paramref name="divisor"/> }.</typeparam>
    /// <param name="divisor">The divisor of { <see langword="this"/> / <paramref name="divisor"/> }.</param>
    public abstract Unhandled Divide<TDivisor>(TDivisor divisor) where TDivisor : IScalarQuantity;

    /// <summary>Computes { <paramref name="dividend"/> / <see langword="this"/> }.</summary>
    /// <typeparam name="TDividend">The scalar quantity that represents the dividend of { <paramref name="dividend"/> / <see langword="this"/> }.</typeparam>
    /// <param name="dividend">The dividend of { <paramref name="dividend"/> / <see langword="this"/> }.</param>
    public abstract Unhandled DivideInto<TDividend>(TDividend dividend) where TDividend : IScalarQuantity;

    /// <inheritdoc cref="Multiply{TFactor}(TFactor)"/>
    /// <typeparam name="TProduct">The scalar quantity that represents the result of { <see langword="this"/> ∙ <paramref name="factor"/> }.</typeparam>
    /// <typeparam name="TFactor">The scalar quantity that represents the second factor of { <see langword="this"/> ∙ <paramref name="factor"/> }.</typeparam>
    /// <param name="factor">The second factor of { <see langword="this"/> ∙ <paramref name="factor"/> }.</param>
    public abstract TProduct Multiply<TProduct, TFactor>(TFactor factor) where TProduct : IScalarQuantity<TProduct> where TFactor : IScalarQuantity;

    /// <inheritdoc cref="Divide{TDivisor}(TDivisor)"/>
    /// <typeparam name="TQuotient">The scalar quantity that represents the result of { <see langword="this"/> / <paramref name="divisor"/> }.</typeparam>
    /// <typeparam name="TDivisor">The scalar quantity that represents the divisor of { <see langword="this"/> / <paramref name="divisor"/> }.</typeparam>
    /// <param name="divisor">The divisor of { <see langword="this"/> / <paramref name="divisor"/> }.</param>
    public abstract TQuotient Divide<TQuotient, TDivisor>(TDivisor divisor) where TQuotient : IScalarQuantity<TQuotient> where TDivisor : IScalarQuantity;

    /// <inheritdoc cref="DivideInto{TDividend}(TDividend)"/>
    /// <typeparam name="TQuotient">The scalar quantity that represents the result of { <paramref name="dividend"/> / <see langword="this"/> }.</typeparam>
    /// <typeparam name="TDividend">The scalar quantity that represents the dividend of { <paramref name="dividend"/> / <see langword="this"/> }.</typeparam>
    /// <param name="dividend">The dividend of { <paramref name="dividend"/> / <see langword="this"/> }.</param>
    public abstract TQuotient DivideInto<TQuotient, TDividend>(TDividend dividend) where TQuotient : IScalarQuantity<TQuotient> where TDividend : IScalarQuantity;
}

/// <inheritdoc cref="IScalarQuantity" path="/summary"/>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
public interface IScalarQuantity<TSelf>
    : IScalarQuantity
    where TSelf : IScalarQuantity<TSelf>
{
    /// <summary>Constructs a new <typeparamref name="TSelf"/> representing { <paramref name="magnitude"/> }.</summary>
    /// <param name="magnitude">The magnitude represented by the constructed <typeparamref name="TSelf"/>.</param>
    [SuppressMessage("Design", "CA1000", Justification = "Implementing type is not necessarily generic")]
    public static abstract TSelf WithMagnitude(Scalar magnitude);

    /// <summary>Computes { +<see langword="this"/> }.</summary>
    public abstract TSelf Plus();
    /// <summary>Computes { -<see langword="this"/> }.</summary>
    public abstract TSelf Negate();

    /// <inheritdoc cref="IScalarQuantity.Multiply{TFactor}(TFactor)"/>
    public abstract TSelf Multiply(Scalar factor);

    /// <inheritdoc cref="IScalarQuantity.Divide{TDivisor}(TDivisor)"/>
    public abstract TSelf Divide(Scalar divisor);

    /// <summary>Computes { +<paramref name="x"/> }.</summary>
    /// <param name="x">The factor of { +<paramref name="x"/> }.</param>
    public static abstract TSelf operator +(TSelf x);
    /// <summary>Computes { -<paramref name="x"/> }.</summary>
    /// <param name="x">The factor of { -<paramref name="x"/> }.</param>
    public static abstract TSelf operator -(TSelf x);

    /// <summary>Computes { <paramref name="x"/> ∙ <paramref name="y"/> }.</summary>
    /// <param name="x">The first factor of { <paramref name="x"/> ∙ <paramref name="y"/> }.</param>
    /// <param name="y">The second factor of { <paramref name="x"/> ∙ <paramref name="y"/> }.</param>
    public static abstract TSelf operator *(TSelf x, Scalar y);
    /// <inheritdoc cref="operator *(TSelf, Scalar)"/>
    public static abstract TSelf operator *(Scalar x, TSelf y);

    /// <summary>Computes { <paramref name="x"/> / <paramref name="y"/> }.</summary>
    /// <param name="x">The dividend of { <paramref name="x"/> / <paramref name="y"/> }.</param>
    /// <param name="y">The divisor of { <paramref name="x"/> / <paramref name="y"/> }.</param>
    public static abstract TSelf operator /(TSelf x, Scalar y);
}
