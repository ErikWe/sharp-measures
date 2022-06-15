namespace SharpMeasures;

using System.Diagnostics.CodeAnalysis;
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

    /// <summary>The magnitudes of the X, Y, and Z component of <see langword="this"/>.</summary>
    public abstract Vector3 Components { get; }

    /// <summary>Computes the magnitude / norm / length of <see langword="this"/>.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when applicable.</remarks>
    public abstract Scalar Magnitude();
    /// <summary>Computes the square of the magnitude / norm / length of <see langword="this"/>.</summary>
    public abstract Scalar SquaredMagnitude();

    /// <inheritdoc cref="IScalarQuantity.Multiply{TFactor}(TFactor)"/>
    public abstract Unhandled3 Multiply<TFactor>(TFactor factor) where TFactor : IScalarQuantity;

    /// <inheritdoc cref="IScalarQuantity.Divide{TDivisor}(TDivisor)"/>
    public abstract Unhandled3 Divide<TDivisor>(TDivisor divisor) where TDivisor : IScalarQuantity;

    /// <summary>Computes { <see langword="this"/> ∙ <paramref name="factor"/> }.</summary>
    /// <typeparam name="TFactor">The three-dimensional vector quantity that represents the second factor of { <see langword="this"/> ∙ <paramref name="factor"/> }.</typeparam>
    /// <param name="factor">The second factor of { <see langword="this"/> ∙ <paramref name="factor"/> }.</param>
    public abstract Unhandled Dot<TFactor>(TFactor factor) where TFactor : IVector3Quantity;

    /// <summary>Computes { <see langword="this"/> ⨯ <paramref name="factor"/> }.</summary>
    /// <typeparam name="TFactor">The three-dimensional vector quantity that represents the second factor of { <see langword="this"/> ⨯ <paramref name="factor"/> }.</typeparam>
    /// <param name="factor">The second factor of { <see langword="this"/> ⨯ <paramref name="factor"/> }.</param>
    public abstract Unhandled3 Cross<TFactor>(TFactor factor) where TFactor : IVector3Quantity;

    /// <summary>Computes { <paramref name="factor"/> ⨯ <see langword="this"/> }.</summary>
    /// <typeparam name="TFactor">The three-dimensional vector quantity that represents the first factor of { <paramref name="factor"/> ⨯ <see langword="this"/> }.</typeparam>
    /// <param name="factor">The first factor of { <paramref name="factor"/> ⨯ <see langword="this"/> }.</param>
    public abstract Unhandled3 CrossInto<TFactor>(TFactor factor) where TFactor : IVector3Quantity;

    /// <inheritdoc cref="Multiply{TFactor}(TFactor)"/>
    /// <typeparam name="TProduct">The three-dimensional vector quantity that represents the result of { <see langword="this"/> ∙ <paramref name="factor"/> }.</typeparam>
    /// <typeparam name="TFactor">The scalar quantity that represents the second factor of { <see langword="this"/> ∙ <paramref name="factor"/> }.</typeparam>
    public abstract TProduct Multiply<TProduct, TFactor>(TFactor factor) where TProduct : IVector3Quantity<TProduct> where TFactor : IScalarQuantity;

    /// <inheritdoc cref="Divide{TDivisor}(TDivisor)"/>
    /// <typeparam name="TQuotient">The three-dimensional vector quantity that represents the result of { <see langword="this"/> / <paramref name="divisor"/> }.</typeparam>
    /// <typeparam name="TDivisor">The scalar quantity that represents the divisor of { <see langword="this"/> / <paramref name="divisor"/> }.</typeparam>
    public abstract TQuotient Divide<TQuotient, TDivisor>(TDivisor divisor) where TQuotient : IVector3Quantity<TQuotient> where TDivisor : IScalarQuantity;

    /// <inheritdoc cref="Dot{TFactor}(TFactor)"/>
    /// <typeparam name="TProduct">The scalar quantity that represents the result of { <see langword="this"/> ∙ <paramref name="factor"/> }.</typeparam>
    /// <typeparam name="TFactor">The three-dimensional vector quantity that represents the second factor of { <see langword="this"/> ∙ <paramref name="factor"/> }.</typeparam>
    public abstract TProduct Dot<TProduct, TFactor>(TFactor factor) where TProduct : IScalarQuantity<TProduct> where TFactor : IVector3Quantity;

    /// <inheritdoc cref="Cross{TFactor}(TFactor)"/>
    /// <typeparam name="TProduct">The three-dimensional vector quantity that represents the result of { <see langword="this"/> ⨯ <paramref name="factor"/> }.</typeparam>
    /// <typeparam name="TFactor">The three-dimensional vector quantity that represents the second factor of { <see langword="this"/> ⨯ <paramref name="factor"/> }.</typeparam>
    public abstract TProduct Cross<TProduct, TFactor>(TFactor factor) where TProduct : IVector3Quantity<TProduct> where TFactor : IVector3Quantity;

    /// <inheritdoc cref="CrossInto{TFactor}(TFactor)"/>
    /// <typeparam name="TProduct">The three-dimensional vector quantity that represents the result of { <paramref name="factor"/> ⨯ <see langword="this"/> }.</typeparam>
    /// <typeparam name="TFactor">The three-dimensional vector quantity that represents the second factor of { <paramref name="factor"/> ⨯ <see langword="this"/> }.</typeparam>
    public abstract TProduct CrossInto<TProduct, TFactor>(TFactor factor) where TProduct : IVector3Quantity<TProduct> where TFactor : IVector3Quantity;
}

/// <inheritdoc cref="IVector3Quantity" path="/summary"/>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
public interface IVector3Quantity<TSelf> :
    IVector3Quantity
    where TSelf : IVector3Quantity<TSelf>
{
    /// <summary>Constructs a new <typeparamref name="TSelf"/> representing { <paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/> }.</summary>
    /// <param name="x">The magnitude of the X-component represented by the constructed <typeparamref name="TSelf"/>.</param>
    /// <param name="y">The magnitude of the Y-component represented by the constructed <typeparamref name="TSelf"/>.</param>
    /// <param name="z">The magnitude of the Z-component represented by the constructed <typeparamref name="TSelf"/>.</param>
    [SuppressMessage("Design", "CA1000", Justification = "Implementing type is not necessarily generic")]
    public static abstract TSelf WithComponents(Scalar x, Scalar y, Scalar z);

    /// <summary>Constructs a new <typeparamref name="TSelf"/> representing { <paramref name="components"/> }.</summary>
    /// <param name="components">The magnitude of components X, Y, and Z represented by the constructed <typeparamref name="TSelf"/>.</param>
    [SuppressMessage("Design", "CA1000", Justification = "Implementing type is not necessarily generic")]
    public static abstract TSelf WithComponents(Vector3 components);

    /// <summary>Computes the normalized <see langword="this"/>.</summary>
    public abstract TSelf Normalize();

    /// <summary>Transforms <see langword="this"/> by <paramref name="transform"/>.</summary>
    /// <param name="transform"><see langword="this"/> is transformed by this transform.</param>
    public abstract TSelf Transform(Matrix4x4 transform);

    /// <inheritdoc cref="IScalarQuantity{TSelf}.Plus"/>
    public abstract TSelf Plus();
    /// <inheritdoc cref="IScalarQuantity{TSelf}.Negate"/>
    public abstract TSelf Negate();

    /// <inheritdoc cref="IVector3Quantity.Multiply{TFactor}(TFactor)"/>
    public abstract TSelf Multiply(Scalar factor);

    /// <inheritdoc cref="IVector3Quantity.Divide{TDivisor}(TDivisor)"/>
    public abstract TSelf Divide(Scalar divisor);

    /// <inheritdoc cref="IVector3Quantity.Cross{TFactor}(TFactor)"/>
    public abstract TSelf Cross(Vector3 factor);

    /// <inheritdoc cref="IVector3Quantity.CrossInto{TFactor}(TFactor)"/>
    public abstract TSelf CrossInto(Vector3 factor);

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
