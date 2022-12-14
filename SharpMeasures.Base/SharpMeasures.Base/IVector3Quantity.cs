namespace SharpMeasures;

using System.Numerics;

/// <summary>Describes a three-dimensional vector quantity, consisting of X, Y, and Z components.</summary>
public interface IVector3Quantity
{
    /// <summary>The magnitude of the X-component of <see langword="this"/>.</summary>
    public abstract Scalar X { get; }
    /// <summary>The magnitude of the Y-component of <see langword="this"/>.</summary>
    public abstract Scalar Y { get; }
    /// <summary>The magnitude of the Z-component of <see langword="this"/>.</summary>
    public abstract Scalar Z { get; }

    /// <summary>The magnitudes of the X, Y, and Z components of <see langword="this"/>.</summary>
    public abstract Vector3 Components { get; }

    /// <summary>Computes the magnitude of <see langword="this"/>.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when applicable.</remarks>
    public abstract Scalar Magnitude();
    /// <summary>Computes the square of the magnitude of <see langword="this"/>.</summary>
    public abstract Scalar SquaredMagnitude();
}

/// <inheritdoc cref="IVector3Quantity" path="/summary"/>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
public interface IVector3Quantity<TSelf> : IVector3Quantity where TSelf : IVector3Quantity<TSelf>
{
    /// <summary>Constructs a new <typeparamref name="TSelf"/> representing { <paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/> }.</summary>
    /// <param name="x">The magnitude of the X-component of the constructed <typeparamref name="TSelf"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the constructed <typeparamref name="TSelf"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the constructed <typeparamref name="TSelf"/>.</param>
    public static abstract TSelf WithComponents(Scalar x, Scalar y, Scalar z);

    /// <summary>Constructs a new <typeparamref name="TSelf"/> representing { <paramref name="components"/> }.</summary>
    /// <param name="components">The magnitudes of components X, Y, and Z of the constructed <typeparamref name="TSelf"/>.</param>
    public static abstract TSelf WithComponents(Vector3 components);

    /// <summary>Computes the normalized <see langword="this"/>.</summary>
    public abstract TSelf Normalize();

    /// <summary>Transforms <see langword="this"/> by <paramref name="transform"/>.</summary>
    /// <param name="transform"><see langword="this"/> is transformed by this transform.</param>
    public abstract TSelf Transform(Matrix4x4 transform);

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
