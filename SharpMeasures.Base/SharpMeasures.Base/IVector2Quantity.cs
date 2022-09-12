namespace SharpMeasures;

using System.Diagnostics.CodeAnalysis;
using System.Numerics;

/// <summary>Describes a two-dimensional vector quantity, consisting of X and Y components.</summary>
public interface IVector2Quantity
{
    /// <summary>The magnitude of the X-component of <see langword="this"/>.</summary>
    public abstract Scalar X { get; }
    /// <summary>The magnitude of the Y-component of <see langword="this"/>.</summary>
    public abstract Scalar Y { get; }

    /// <summary>The magnitudes of the X and Y components of <see langword="this"/>.</summary>
    public abstract Vector2 Components { get; }

    /// <summary>Computes the magnitude / norm / length of <see langword="this"/>.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when applicable.</remarks>
    public abstract Scalar Magnitude();
    /// <summary>Computes the square of the magnitude / norm / length of <see langword="this"/>.</summary>
    public abstract Scalar SquaredMagnitude();
}

/// <inheritdoc cref="IVector2Quantity" path="/summary"/>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
public interface IVector2Quantity<TSelf> : IVector2Quantity where TSelf : IVector2Quantity<TSelf>
{
    /// <summary>Constructs a new <typeparamref name="TSelf"/> representing { <paramref name="x"/>, <paramref name="y"/> }.</summary>
    /// <param name="x">The magnitude of the X-component of the constructed <typeparamref name="TSelf"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the constructed <typeparamref name="TSelf"/>.</param>
    [SuppressMessage("Design", "CA1000", Justification = "Implementing type is not necessarily generic")]
    public static abstract TSelf WithComponents(Scalar x, Scalar y);

    /// <summary>Constructs a new <typeparamref name="TSelf"/> representing { <paramref name="components"/> }.</summary>
    /// <param name="components">The magnitudes of components X and Y of the constructed <typeparamref name="TSelf"/>.</param>
    [SuppressMessage("Design", "CA1000", Justification = "Implementing type is not necessarily generic")]
    public static abstract TSelf WithComponents(Vector2 components);

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
