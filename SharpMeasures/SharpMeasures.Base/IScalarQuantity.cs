namespace SharpMeasures;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity, having only magnitude.</summary>
public interface IScalarQuantity
{
    /// <summary>The magnitude of <see langword="this"/>.</summary>
    public abstract Scalar Magnitude { get; }
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

    /// <summary>Computes { <see langword="this"/> ∙ <paramref name="factor"/> }.</summary>
    /// <param name="factor">The second factor of { <see langword="this"/> ∙ <paramref name="factor"/> }.</param>
    public abstract TSelf Multiply(Scalar factor);

    /// <summary>Computes { <see langword="this"/> / <paramref name="divisor"/> }.</summary>
    /// <param name="divisor">The divisor of { <see langword="this"/> / <paramref name="divisor"/> }.</param>
    public abstract TSelf Divide(Scalar divisor);

    /// <summary>Computes { <see langword="this"/> % <paramref name="divisor"/> }.</summary>
    /// <param name="divisor">The divisor of { <see langword="this"/> % <paramref name="divisor"/> }.</param>
    public abstract TSelf Remainder(Scalar divisor);

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

    /// <summary>Computes { <paramref name="x"/> % <paramref name="y"/> }.</summary>
    /// <param name="x">The dividend of { <paramref name="x"/> % <paramref name="y"/> }.</param>
    /// <param name="y">The divisor of { <paramref name="x"/> % <paramref name="y"/> }.</param>
    public static abstract TSelf operator %(TSelf x, Scalar y);
}
