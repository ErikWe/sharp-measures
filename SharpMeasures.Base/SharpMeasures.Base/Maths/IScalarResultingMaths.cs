namespace SharpMeasures.Maths;

/// <summary>Describes mathematical operations that result in a scalar quantity <typeparamref name="TResult"/>.</summary>
/// <typeparam name="TResult">The scalar quantity that is the result of the described mathematical operations.</typeparam>
public interface IScalarResultingMaths<TResult> where TResult : IScalarQuantity<TResult>
{
    /// <summary>Computes the absolute value of <paramref name="scalar"/>.</summary>
    /// <param name="scalar">The absolute value of this scalar is computed.</param>
    public abstract TResult Absolute(TResult scalar);

    /// <summary>Computes { 1 / <paramref name="reciprocal"/> }.</summary>
    /// <typeparam name="TReciprocal">The scalar quantity that is the reciprocal of <typeparamref name="TResult"/>.</typeparam>
    /// <param name="reciprocal">The divisor of { 1 / <paramref name="reciprocal"/> }.</param>
    public abstract TResult Reciprocal<TReciprocal>(TReciprocal reciprocal)
        where TReciprocal : IScalarQuantity;

    /// <summary>Computes { <paramref name="squareRoot"/> ² }.</summary>
    /// <typeparam name="TSquareRoot">The scalar quantity that is the square root of <typeparamref name="TResult"/>.</typeparam>
    /// <param name="squareRoot">The base of { <paramref name="squareRoot"/> ² }.</param>
    public abstract TResult Square<TSquareRoot>(TSquareRoot squareRoot)
        where TSquareRoot : IScalarQuantity;

    /// <summary>Computes { <paramref name="cubeRoot"/> ³ }.</summary>
    /// <typeparam name="TCubeRoot">The scalar quantity that is the cube root of <typeparamref name="TResult"/>.</typeparam>
    /// <param name="cubeRoot">The base of { <paramref name="cubeRoot"/> ³ }.</param>
    public abstract TResult Cube<TCubeRoot>(TCubeRoot cubeRoot)
        where TCubeRoot : IScalarQuantity;

    /// <summary>Computes { √ <paramref name="square"/> }.</summary>
    /// <typeparam name="TSquare">The scalar quantity that is the square of <typeparamref name="TResult"/>.</typeparam>
    /// <param name="square">The radicand of { √ <paramref name="square"/> }.</param>
    public abstract TResult SquareRoot<TSquare>(TSquare square)
        where TSquare : IScalarQuantity;

    /// <summary>Computes { ³√ <paramref name="cube"/> }.</summary>
    /// <typeparam name="TCube">The scalar quantity that is the cube of <typeparamref name="TResult"/>.</typeparam>
    /// <param name="cube">The radicand of { ³√ <paramref name="cube"/> }.</param>
    public abstract TResult CubeRoot<TCube>(TCube cube)
        where TCube : IScalarQuantity;

    /// <summary>Computes the magnitude / norm / length of <paramref name="vector"/>.</summary>
    /// <typeparam name="TVector">The two-dimensional vector quantity for which the magnitude is computed.</typeparam>
    /// <param name="vector">The magnitude of this <typeparamref name="TVector"/> is computed.</param>
    /// <remarks>For improved performance, consider prefering <see cref="SquaredMagnitude2{TVector}(TVector)"/> when applicable.</remarks>
    public abstract TResult Magnitude2<TVector>(TVector vector)
        where TVector : IVector2Quantity;

    /// <summary>Computes the magnitude / norm / length of <paramref name="vector"/>.</summary>
    /// <typeparam name="TVector">The three-dimensional vector quantity for which the magnitude is computed.</typeparam>
    /// <param name="vector">The magnitude of this <typeparamref name="TVector"/> is computed.</param>
    /// <remarks>For improved performance, consider prefering <see cref="SquaredMagnitude3{TVector}(TVector)"/> when applicable.</remarks>
    public abstract TResult Magnitude3<TVector>(TVector vector)
        where TVector : IVector3Quantity;

    /// <summary>Computes the magnitude / norm / length of <paramref name="vector"/>.</summary>
    /// <typeparam name="TVector">The four-dimensional vector quantity for which the magnitude is computed.</typeparam>
    /// <param name="vector">The magnitude of this <typeparamref name="TVector"/> is computed.</param>
    /// <remarks>For improved performance, consider prefering <see cref="SquaredMagnitude4{TVector}(TVector)"/> when applicable.</remarks>
    public abstract TResult Magnitude4<TVector>(TVector vector)
        where TVector : IVector4Quantity;

    /// <summary>Computes the square of the magnitude / norm / length of <paramref name="vector"/>.</summary>
    /// <typeparam name="TVector">The two-dimensional vector quantity for which the squared magnitude is computed.</typeparam>
    /// <param name="vector">The squared magnitude of this <typeparamref name="TVector"/> is computed.</param>
    public abstract TResult SquaredMagnitude2<TVector>(TVector vector)
        where TVector : IVector2Quantity;

    /// <summary>Computes the square of the magnitude / norm / length of <paramref name="vector"/>.</summary>
    /// <typeparam name="TVector">The three-dimensional vector quantity for which the squared magnitude is computed.</typeparam>
    /// <param name="vector">The squared magnitude of this <typeparamref name="TVector"/> is computed.</param>
    public abstract TResult SquaredMagnitude3<TVector>(TVector vector)
        where TVector : IVector3Quantity;

    /// <summary>Computes the square of the magnitude / norm / length of <paramref name="vector"/>.</summary>
    /// <typeparam name="TVector">The four-dimensional vector quantity for which the squared magnitude is computed.</typeparam>
    /// <param name="vector">The squared magnitude of this <typeparamref name="TVector"/> is computed.</param>
    public abstract TResult SquaredMagnitude4<TVector>(TVector vector)
        where TVector : IVector4Quantity;

    /// <summary>Computes { <paramref name="a"/> ∙ <paramref name="b"/> }.</summary>
    /// <typeparam name="TFactor1">The two-dimensional vector quantity that represents the first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</typeparam>
    /// <typeparam name="TFactor2">The two-dimensional vector quantity that represents the second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }..</typeparam>
    /// <param name="a">The first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    public abstract TResult Dot2<TFactor1, TFactor2>(TFactor1 a, TFactor2 b)
        where TFactor1 : IVector2Quantity
        where TFactor2 : IVector2Quantity;

    /// <summary>Computes { <paramref name="a"/> ∙ <paramref name="b"/> }.</summary>
    /// <typeparam name="TFactor1">The three-dimensional vector quantity that represents the first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</typeparam>
    /// <typeparam name="TFactor2">The three-dimensional vector quantity that represents the second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }..</typeparam>
    /// <param name="a">The first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    public abstract TResult Dot3<TFactor1, TFactor2>(TFactor1 a, TFactor2 b)
        where TFactor1 : IVector3Quantity
        where TFactor2 : IVector3Quantity;

    /// <summary>Computes { <paramref name="a"/> ∙ <paramref name="b"/> }.</summary>
    /// <typeparam name="TFactor1">The four-dimensional vector quantity that represents the first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</typeparam>
    /// <typeparam name="TFactor2">The four-dimensional vector quantity that represents the second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }..</typeparam>
    /// <param name="a">The first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    public abstract TResult Dot4<TFactor1, TFactor2>(TFactor1 a, TFactor2 b)
        where TFactor1 : IVector4Quantity
        where TFactor2 : IVector4Quantity;
}
