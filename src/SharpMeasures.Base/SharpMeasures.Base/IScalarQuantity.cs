namespace SharpMeasures;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity, having only magnitude.</summary>
public interface IScalarQuantity
{
    /// <summary>The magnitude of the scalar quantity, expressed in some arbitrary unit.</summary>
    public abstract Scalar Magnitude { get; }
}

/// <inheritdoc cref="IScalarQuantity" path="/summary"/>
/// <typeparam name="TSelf">The self-type.</typeparam>
public interface IScalarQuantity<TSelf> : IScalarQuantity where TSelf : IScalarQuantity<TSelf>
{
    /// <summary>Constructs the <typeparamref name="TSelf"/> representing the provided <see cref="Scalar"/> magnitude, expressed in some arbitrary unit.</summary>
    /// <param name="magnitude">The magnitude of the constructed <typeparamref name="TSelf"/>, expressed in some arbitrary unit.</param>
    /// <remarks>In most cases, this should be avoided - instead, construction of <typeparamref name="TSelf"/> should be done by also specifying the intended unit.</remarks>
    /// <returns>The constructed <typeparamref name="TSelf"/>, representing the provided <see cref="Scalar"/> magnitude.</returns>
    [SuppressMessage("Design", "CA1000: Do not declare static members on generic types", Justification = "Member is not accessed a generic fashion.")]
    public static abstract TSelf WithMagnitude(Scalar magnitude);

    /// <summary>Applies the unary plus, resulting in the same <typeparamref name="TSelf"/>.</summary>
    /// <returns>The same <typeparamref name="TSelf"/>, { <see langword="this"/> }.</returns>
    public abstract TSelf Plus();

    /// <summary>Negates the <typeparamref name="TSelf"/>.</summary>
    /// <returns>The negated <typeparamref name="TSelf"/>, { -<see langword="this"/> }.</returns>
    public abstract TSelf Negate();

    /// <summary>Scales the <typeparamref name="TSelf"/> by the provided <see cref="Scalar"/> factor.</summary>
    /// <param name="factor">The <see cref="Scalar"/> factor, by which the <typeparamref name="TSelf"/> is scaled.</param>
    /// <returns>The scalad <typeparamref name="TSelf"/>, { <paramref name="factor"/> ∙ <see langword="this"/> }.</returns>
    public abstract TSelf Multiply(Scalar factor);

    /// <summary>Scales the <typeparamref name="TSelf"/> through division by the provided <see cref="Scalar"/> value.</summary>
    /// <param name="divisor">The <see cref="Scalar"/> value, scaling the <typeparamref name="TSelf"/> through division.</param>
    /// <returns>The scaled <typeparamref name="TSelf"/>, { <see langword="this"/> / <paramref name="divisor"/> }.</returns>
    public abstract TSelf Divide(Scalar divisor);

    /// <summary>Applies the unary plus to the provided <typeparamref name="TSelf"/>, resulting in the same <typeparamref name="TSelf"/>.</summary>
    /// <param name="x">The <typeparamref name="TSelf"/>, to which the unary plus is applied.</param>
    /// <returns>The same <typeparamref name="TSelf"/>, { <paramref name="x"/> }.</returns>
    public static abstract TSelf operator +(TSelf x);

    /// <summary>Negates the provided <typeparamref name="TSelf"/>.</summary>
    /// <param name="x">The <typeparamref name="TSelf"/> that is negated.</param>
    /// <returns>The negated <typeparamref name="TSelf"/>, { -<paramref name="x"/> }.</returns>
    public static abstract TSelf operator -(TSelf x);

    /// <summary>Scales the provided <typeparamref name="TSelf"/> by the provided <see cref="Scalar"/> factor.</summary>
    /// <param name="x">The <typeparamref name="TSelf"/> that is scaled.</param>
    /// <param name="y">The <see cref="Scalar"/> factor, by which the <typeparamref name="TSelf"/> is scaled.</param>
    /// <returns>The scaled <typeparamref name="TSelf"/>, { <paramref name="x"/> ∙ <paramref name="y"/> }.</returns>
    public static abstract TSelf operator *(TSelf x, Scalar y);

    /// <summary>Scales the provided <typeparamref name="TSelf"/> by the provided <see cref="Scalar"/> factor.</summary>
    /// <param name="x">The <see cref="Scalar"/> factor, by which the <typeparamref name="TSelf"/> is scaled.</param>
    /// <param name="y">The <typeparamref name="TSelf"/> that is scaled.</param>
    /// <returns>The scaled <typeparamref name="TSelf"/>, { <paramref name="x"/> ∙ <paramref name="y"/> }.</returns>
    public static abstract TSelf operator *(Scalar x, TSelf y);

    /// <summary>Scales the provided <typeparamref name="TSelf"/> through division by the provided <see cref="Scalar"/> value.</summary>
    /// <param name="x">The <typeparamref name="TSelf"/> that is scaled.</param>
    /// <param name="y">The <see cref="Scalar"/> value, scaling the <typeparamref name="TSelf"/> through division.</param>
    /// <returns>The scaled <typeparamref name="TSelf"/>, { <paramref name="x"/> / <paramref name="y"/> }.</returns>
    public static abstract TSelf operator /(TSelf x, Scalar y);
}
