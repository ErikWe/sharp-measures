namespace SharpMeasures;

using System.Diagnostics.CodeAnalysis;
using System.Numerics;

/// <summary>Describes a four-dimensional vector quantity, consisting of X, Y, Z, and W components.</summary>
public interface IVector4Quantity
{
    /// <summary>The magnitude of the X-component of <see langword="this"/>.</summary>
    public abstract Scalar X { get; }
    /// <summary>The magnitude of the Y-component of <see langword="this"/>.</summary>
    public abstract Scalar Y { get; }
    /// <summary>The magnitude of the Z-component of <see langword="this"/>.</summary>
    public abstract Scalar Z { get; }
    /// <summary>The magnitude of the W-component of <see langword="this"/>.</summary>
    public abstract Scalar W { get; }

    /// <summary>The magnitudes of the X, Y, Z, and W components of <see langword="this"/>.</summary>
    public abstract Vector4 Components { get; }

    /// <summary>Computes the magnitude / norm / length of <see langword="this"/>.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when applicable.</remarks>
    public abstract Scalar Magnitude();
    /// <summary>Computes the square of the magnitude / norm / length of <see langword="this"/>.</summary>
    public abstract Scalar SquaredMagnitude();
}

/// <inheritdoc cref="IVector4Quantity" path="/summary"/>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
public interface IVector4Quantity<TSelf> : IVector4Quantity where TSelf : IVector4Quantity<TSelf>
{
    /// <summary>Constructs a new <typeparamref name="TSelf"/> representing { <paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>, <paramref name="w"/> }.</summary>
    /// <param name="x">The magnitude of the X-component of the constructed <typeparamref name="TSelf"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the constructed <typeparamref name="TSelf"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the constructed <typeparamref name="TSelf"/>.</param>
    /// <param name="w">The magnitude of the W-component of the constructed <typeparamref name="TSelf"/>.</param>
    [SuppressMessage("Design", "CA1000", Justification = "Implementing type is not necessarily generic")]
    public static abstract TSelf WithComponents(Scalar x, Scalar y, Scalar z, Scalar w);

    /// <summary>Constructs a new <typeparamref name="TSelf"/> representing { <paramref name="components"/> }.</summary>
    /// <param name="components">The magnitudes of components X, Y, Z, and W of the constructed <typeparamref name="TSelf"/>.</param>
    [SuppressMessage("Design", "CA1000", Justification = "Implementing type is not necessarily generic")]
    public static abstract TSelf WithComponents(Vector4 components);

    /// <summary>Computes the normalized <see langword="this"/>.</summary>
    public abstract TSelf Normalize();

    /// <summary>Computes { +<see langword="this"/> }.</summary>
    public abstract TSelf Plus();
    /// <summary>Computes { -<see langword="this"/> }.</summary>
    public abstract TSelf Negate();

    /// <inheritdoc cref="IScalarQuantity{TSelf}.Multiply(Scalar)"/>
    public abstract TSelf Multiply(Scalar factor);

    /// <inheritdoc cref="IScalarQuantity{TSelf}.Divide(Scalar)"/>
    public abstract TSelf Divide(Scalar divisor);

    /// <summary>Computes { +<paramref name="a"/> }.</summary>
    /// <param name="a">The factor of { +<paramref name="a"/> }.</param>
    public static abstract TSelf operator +(TSelf a);
    /// <summary>Computes { -<paramref name="a"/> }.</summary>
    /// <param name="a">The factor of { -<paramref name="a"/> }.</param>
    public static abstract TSelf operator -(TSelf a);

    /// <summary>Computes { <paramref name="a"/> ∙ <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    public static abstract TSelf operator *(TSelf a, Scalar b);
    /// <inheritdoc cref="operator *(TSelf, Scalar)"/>
    public static abstract TSelf operator *(Scalar a, TSelf b);

    /// <summary>Computes { <paramref name="a"/> / <paramref name="b"/> }.</summary>
    /// <param name="a">The dividend of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    /// <param name="b">The divisor of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    public static abstract TSelf operator /(TSelf a, Scalar b);
}
