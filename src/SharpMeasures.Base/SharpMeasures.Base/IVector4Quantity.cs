namespace SharpMeasures;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a four-dimensional vector quantity, consisting of X, Y, Z, and W components.</summary>
public interface IVector4Quantity : IVectorQuantity
{
    /// <summary>The magnitude of the X-component of the quantity, expressed in some arbitrary unit.</summary>
    public abstract Scalar X { get; }

    /// <summary>The magnitude of the Y-component of the quantity, expressed in some arbitrary unit.</summary>
    public abstract Scalar Y { get; }

    /// <summary>The magnitude of the Z-component of the quantity, expressed in some arbitrary unit.</summary>
    public abstract Scalar Z { get; }

    /// <summary>The magnitude of the W-component of the quantity, expressed in some arbitrary unit.</summary>
    public abstract Scalar W { get; }

    /// <summary>The magnitudes of the X, Y, Z, and W components of the quantity, expressed in some arbitrary unit.</summary>
    public abstract Vector4 Components { get; }
}

/// <inheritdoc cref="IVector4Quantity" path="/summary"/>
/// <typeparam name="TSelf">The self-type.</typeparam>
public interface IVector4Quantity<TSelf> : IVector4Quantity where TSelf : IVector4Quantity<TSelf>
{
    /// <summary>Constructs the <typeparamref name="TSelf"/> representing the provided <see cref="Scalar"/> components, expressed in some arbitrary unit.</summary>
    /// <param name="x">The magnitude of the X-component of the constructed <typeparamref name="TSelf"/>, expressed in some arbitrary unit.</param>
    /// <param name="y">The magnitude of the Y-component of the constructed <typeparamref name="TSelf"/>, expressed in some arbitrary unit.</param>
    /// <param name="z">The magnitude of the Z-component of the constructed <typeparamref name="TSelf"/>, expressed in some arbitrary unit.</param>
    /// <param name="w">The magnitude of the W-component of the constructed <typeparamref name="TSelf"/>, expressed in some arbitrary unit.</param>
    /// <remarks>In most cases, this should be avoided - instead, construction of <typeparamref name="TSelf"/> should be done by also specifying the intended unit.</remarks>
    /// <returns>The constructed <typeparamref name="TSelf"/>, representing the provided <see cref="Scalar"/> components.</returns>
    [SuppressMessage("Design", "CA1000: Do not declare static members on generic types", Justification = "Member is not accessed a generic fashion.")]
    public static abstract TSelf WithComponents(Scalar x, Scalar y, Scalar z, Scalar w);

    /// <summary>Constructs the <typeparamref name="TSelf"/> representing the provided <see cref="Vector2"/> components, expressed in some arbitrary unit.</summary>
    /// <param name="components">The magnitudes of the X, Y, Z, and W components of the constructed <typeparamref name="TSelf"/>, expressed in some arbitrary unit.</param>
    /// <remarks>In most cases, this should be avoided - instead, construction of <typeparamref name="TSelf"/> should be done by also specifying the intended unit.</remarks>
    /// <returns>The constructed <typeparamref name="TSelf"/>, representing the provided <see cref="Vector2"/> components.</returns>
    [SuppressMessage("Design", "CA1000: Do not declare static members on generic types", Justification = "Member is not accessed a generic fashion.")]
    public static abstract TSelf WithComponents(Vector4 components);

    /// <summary>Computes the normalized <typeparamref name="TSelf"/> - the <typeparamref name="TSelf"/> with the same direction, but magnitude { 1 }.</summary>
    /// <returns>The normalized <typeparamref name="TSelf"/>.</returns>
    public abstract TSelf Normalize();

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
    /// <param name="a">The <typeparamref name="TSelf"/>, to which the unary plus is applied.</param>
    /// <returns>The same <typeparamref name="TSelf"/>, { <paramref name="a"/> }.</returns>
    public static abstract TSelf operator +(TSelf a);

    /// <summary>Negates the provided <typeparamref name="TSelf"/>.</summary>
    /// <param name="a">The <typeparamref name="TSelf"/> that is negated.</param>
    /// <returns>The negated <typeparamref name="TSelf"/>, { -<paramref name="a"/> }.</returns>
    public static abstract TSelf operator -(TSelf a);

    /// <summary>Scales the provided <typeparamref name="TSelf"/> by the provided <see cref="Scalar"/> factor.</summary>
    /// <param name="a">The <typeparamref name="TSelf"/> that is scaled.</param>
    /// <param name="b">The <see cref="Scalar"/> factor, by which the <typeparamref name="TSelf"/> is scaled.</param>
    /// <returns>The scaled <typeparamref name="TSelf"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static abstract TSelf operator *(TSelf a, Scalar b);

    /// <summary>Scales the provided <typeparamref name="TSelf"/> by the provided <see cref="Scalar"/> factor.</summary>
    /// <param name="a">The <see cref="Scalar"/> factor, by which the <typeparamref name="TSelf"/> is scaled.</param>
    /// <param name="b">The <typeparamref name="TSelf"/> that is scaled.</param>
    /// <returns>The scaled <typeparamref name="TSelf"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static abstract TSelf operator *(Scalar a, TSelf b);

    /// <summary>Scales the provided <typeparamref name="TSelf"/> through division by the provided <see cref="Scalar"/> value.</summary>
    /// <param name="a">The <typeparamref name="TSelf"/> that is scaled.</param>
    /// <param name="b">The <see cref="Scalar"/> value, scaling the <typeparamref name="TSelf"/> through division.</param>
    /// <returns>The scaled <typeparamref name="TSelf"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    public static abstract TSelf operator /(TSelf a, Scalar b);
}
