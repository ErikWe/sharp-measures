namespace SharpMeasures.Maths;

using System;
using System.Numerics;

/// <summary>Handles construction of maths-related components.</summary>
public static class MathFactory
{
    /// <summary>Retrieves a description of mathematical operations that result in a pure <see cref="Scalar"/>.</summary>
    public static IScalarResultingMaths<Scalar> ScalarResult()
    {
        return PureScalarResultingMaths.Instance;
    }

    /// <summary>Retrieves a description of mathematical operations that result in a scalar quantity <typeparamref name="TResult"/>.</summary>
    /// <typeparam name="TResult">The scalar quantity that is the result of the described mathematical operations.</typeparam>
    public static IScalarResultingMaths<TResult> ScalarResult<TResult>()
        where TResult : IScalarQuantity<TResult>
    {
        return ScalarResultingMaths<TResult>.Instance;
    }

    /// <summary>Retrieves a description of mathematical operations that result in a pure <see cref="SharpMeasures.Vector3"/>.</summary>
    public static IVector3ResultingMaths<SharpMeasures.Vector3> Vector3Result()
    {
        return PureVector3ResultingMaths.Instance;
    }

    /// <summary>Retrieves a description of mathematical operations that result in a three-dimensional vector quantity <typeparamref name="TResult"/>.</summary>
    /// <typeparam name="TResult">The three-dimensional vector quantity that is the result of the described mathematical operations.</typeparam>
    public static IVector3ResultingMaths<TResult> Vector3Result<TResult>()
        where TResult : IVector3Quantity<TResult>
    {
        return Vector3ResultingMaths<TResult>.Instance;
    }

    /// <summary>Provides implementations of common mathematical operations.</summary>
    private static class MathImplementations
    {
        /// <summary>Computes the absolute value of <paramref name="scalar"/>.</summary>
        /// <typeparam name="TScalar">The scalar quantity for which the absolute value is computed.</typeparam>
        /// <param name="scalar">The absolute value of this scalar is computed.</param>
        public static Scalar Absolute<TScalar>(TScalar scalar)
            where TScalar : IScalarQuantity
        {
            return Math.Abs(scalar.Magnitude);
        }

        /// <summary>Computes { 1 / <paramref name="reciprocal"/> }.</summary>
        /// <typeparam name="TReciprocal">The scalar quantity for which the reciprocal is computed.</typeparam>
        /// <param name="reciprocal">The divisor of { 1 / <paramref name="reciprocal"/> }.</param>
        public static Scalar Reciprocal<TReciprocal>(TReciprocal reciprocal)
            where TReciprocal : IScalarQuantity
        {
            return 1 / reciprocal.Magnitude;
        }

        /// <summary>Computes { <paramref name="squareRoot"/> ² }.</summary>
        /// <typeparam name="TSquareRoot">The scalar quantity for which the square is computed.</typeparam>
        /// <param name="squareRoot">The base of { <paramref name="squareRoot"/> ² }.</param>
        public static Scalar Square<TSquareRoot>(TSquareRoot squareRoot)
            where TSquareRoot : IScalarQuantity
        {
            return squareRoot.Magnitude * squareRoot.Magnitude;
        }

        /// <summary>Computes { <paramref name="cubeRoot"/> ³ }.</summary>
        /// <typeparam name="TCubeRoot">The scalar quantity for which the cube is computed.</typeparam>
        /// <param name="cubeRoot">The base of { <paramref name="cubeRoot"/> ³ }.</param>
        public static Scalar Cube<TCubeRoot>(TCubeRoot cubeRoot)
            where TCubeRoot : IScalarQuantity
        {
            return cubeRoot.Magnitude * cubeRoot.Magnitude * cubeRoot.Magnitude;
        }

        /// <summary>Computes { √ <paramref name="square"/> }.</summary>
        /// <typeparam name="TSquare">The scalar quantity for which the square root is computed.</typeparam>
        /// <param name="square">The radicand of { √ <paramref name="square"/> }.</param>
        public static Scalar SquareRoot<TSquare>(TSquare square)
            where TSquare : IScalarQuantity
        {
            return Math.Sqrt(square.Magnitude);
        }

        /// <summary>Computes { ³√ <paramref name="cube"/> }.</summary>
        /// <typeparam name="TCube">The scalar quantity for which the cube root is computed.</typeparam>
        /// <param name="cube">The radicand of { ³√ <paramref name="cube"/> }.</param>
        public static Scalar CubeRoot<TCube>(TCube cube)
            where TCube : IScalarQuantity
        {
            return Math.Cbrt(cube.Magnitude);
        }

        /// <summary>Computes the magnitude / norm / length of <paramref name="vector"/>.</summary>
        /// <typeparam name="TVector">The three-dimensional vector quantity for which the magnitude is computed.</typeparam>
        /// <param name="vector">The magnitude of this <typeparamref name="TVector"/> is computed.</param>
        /// <remarks>For improved performance, consider prefering <see cref="SquaredMagnitude3{TVector}(TVector)"/> when applicable.</remarks>
        public static Scalar Magnitude3<TVector>(TVector vector)
            where TVector : IVector3Quantity
        {
            return SquaredMagnitude3(vector).SquareRoot();
        }

        /// <summary>Computes the square of the magnitude / norm / length of <paramref name="vector"/>.</summary>
        /// <typeparam name="TVector">The three-dimensional vector quantity for which the squared magnitude is computed.</typeparam>
        /// <param name="vector">The squared magnitude of this <typeparamref name="TVector"/> is computed.</param>
        public static Scalar SquaredMagnitude3<TVector>(TVector vector)
            where TVector : IVector3Quantity
        {
            return Dot3(vector, vector);
        }

        /// <inheritdoc cref="IScalarResultingMaths{TResult}.Dot3{TFactor1, TFactor2}(TFactor1, TFactor2)"/>
        public static Scalar Dot3<TFactor1, TFactor2>(TFactor1 a, TFactor2 b)
            where TFactor1 : IVector3Quantity
            where TFactor2 : IVector3Quantity
        {
            return a.X.Value * b.X.Value + a.Y.Value * b.Y.Value + a.Z.Value * b.Z.Value;
        }

        /// <inheritdoc cref="IVector3ResultingMaths{TResult}.Cross{TFactor1, TFactor2}(TFactor1, TFactor2)"/>
        public static SharpMeasures.Vector3 Cross<TFactor1, TFactor2>(TFactor1 a, TFactor2 b)
            where TFactor1 : IVector3Quantity
            where TFactor2 : IVector3Quantity
        {
            return
            (
                a.Y.Value * b.Z.Value - a.Z.Value * b.Y.Value,
                a.Z.Value * b.X.Value - a.X.Value * b.Z.Value,
                a.X.Value * b.Y.Value - a.Y.Value * b.X.Value
            );
        }

        /// <inheritdoc cref="IVector3ResultingMaths{TResult}.Normalize(TResult)"/>
        /// <typeparam name="TVector">The three-dimensional vector quantity that is normalized.</typeparam>
        public static TVector Normalize<TVector>(TVector vector)
            where TVector : IVector3Quantity<TVector>
        {
            return vector / Magnitude3(vector);
        }

        /// <inheritdoc cref="IVector3ResultingMaths{TResult}.Transform(TResult, Matrix4x4)"/>
        public static SharpMeasures.Vector3 Transform<TFactor>(TFactor factor, Matrix4x4 transform)
            where TFactor : IVector3Quantity
        {
            return
            (
                factor.X * transform.M11 + factor.Y * transform.M21 + factor.Z * transform.M31 + transform.M41,
                factor.X * transform.M12 + factor.Y * transform.M22 + factor.Z * transform.M32 + transform.M42,
                factor.X * transform.M13 + factor.Y * transform.M23 + factor.Z * transform.M33 + transform.M43
            );
        }
    }

    /// <summary>Describes mathematical operations that result in a pure <see cref="Scalar"/>.</summary>
    private class PureScalarResultingMaths : IScalarResultingMaths<Scalar>
    {
        /// <summary>Retrieves the singleton instance of <see cref="PureScalarResultingMaths"/>.</summary>
        public static PureScalarResultingMaths Instance { get; } = new();

        /// <summary>Constructs a new <see cref="PureScalarResultingMaths"/>.</summary>
        private PureScalarResultingMaths() { }

        /// <inheritdoc/>
        public Scalar Absolute(Scalar scalar)
        {
            return MathImplementations.Absolute(scalar);
        }

        /// <inheritdoc/>
        public Scalar Reciprocal<TReciprocal>(TReciprocal reciprocal)
            where TReciprocal : IScalarQuantity
        {
            return MathImplementations.Reciprocal(reciprocal);
        }

        /// <inheritdoc/>
        public Scalar Square<TSquareRoot>(TSquareRoot squareRoot)
            where TSquareRoot : IScalarQuantity
        {
            return MathImplementations.Square(squareRoot);
        }

        /// <inheritdoc/>
        public Scalar Cube<TCubeRoot>(TCubeRoot cubeRoot)
            where TCubeRoot : IScalarQuantity
        {
            return MathImplementations.Cube(cubeRoot);
        }

        /// <inheritdoc/>
        public Scalar SquareRoot<TSquare>(TSquare square)
            where TSquare : IScalarQuantity
        {
            return MathImplementations.SquareRoot(square);
        }

        /// <inheritdoc/>
        public Scalar CubeRoot<TCube>(TCube cube)
            where TCube : IScalarQuantity
        {
            return MathImplementations.CubeRoot(cube);
        }

        /// <inheritdoc/>
        public Scalar Magnitude3<TVector>(TVector vector)
            where TVector : IVector3Quantity
        {
            return MathImplementations.Magnitude3(vector);
        }

        /// <inheritdoc/>
        public Scalar SquaredMagnitude3<TVector>(TVector vector)
            where TVector : IVector3Quantity
        {
            return MathImplementations.SquaredMagnitude3(vector);
        }

        /// <inheritdoc/>
        public Scalar Dot3<TFactor1, TFactor2>(TFactor1 a, TFactor2 b)
            where TFactor1 : IVector3Quantity
            where TFactor2 : IVector3Quantity
        {
            return MathImplementations.Dot3(a, b);
        }
    }

    /// <summary>Describes mathematical operations that result in a scalar quantity <typeparamref name="TResult"/>.</summary>
    /// <typeparam name="TResult">The scalar quantity that is the result of the described mathematical operations.</typeparam>
    private class ScalarResultingMaths<TResult> : IScalarResultingMaths<TResult>
        where TResult : IScalarQuantity<TResult>
    {
        /// <summary>Retrieves the singleton instance of <see cref="ScalarResultingMaths{TResult}"/>.</summary>
        public static ScalarResultingMaths<TResult> Instance { get; } = new();

        /// <summary>Constructs a new <see cref="ScalarResultingMaths{TResult}"/>.</summary>
        private ScalarResultingMaths() { }

        /// <inheritdoc/>
        public TResult Absolute(TResult scalar)
        {
            return TResult.WithMagnitude(MathImplementations.Absolute(scalar));
        }

        /// <inheritdoc/>
        public TResult Reciprocal<TReciprocal>(TReciprocal reciprocal)
            where TReciprocal : IScalarQuantity
        {
            return TResult.WithMagnitude(MathImplementations.Reciprocal(reciprocal));
        }

        /// <inheritdoc/>
        public TResult Square<TSquareRoot>(TSquareRoot squareRoot)
            where TSquareRoot : IScalarQuantity
        {
            return TResult.WithMagnitude(MathImplementations.Square(squareRoot));
        }

        /// <inheritdoc/>
        public TResult Cube<TCubeRoot>(TCubeRoot cubeRoot)
            where TCubeRoot : IScalarQuantity
        {
            return TResult.WithMagnitude(MathImplementations.Cube(cubeRoot));
        }

        /// <inheritdoc/>
        public TResult SquareRoot<TSquare>(TSquare square)
            where TSquare : IScalarQuantity
        {
            return TResult.WithMagnitude(MathImplementations.SquareRoot(square));
        }

        /// <inheritdoc/>
        public TResult CubeRoot<TCube>(TCube cube)
            where TCube : IScalarQuantity
        {
            return TResult.WithMagnitude(MathImplementations.CubeRoot(cube));
        }

        /// <inheritdoc/>
        public TResult Magnitude3<TVector>(TVector vector)
            where TVector : IVector3Quantity
        {
            return TResult.WithMagnitude(MathImplementations.Magnitude3(vector));
        }

        /// <inheritdoc/>
        public TResult SquaredMagnitude3<TVector>(TVector vector)
            where TVector : IVector3Quantity
        {
            return TResult.WithMagnitude(MathImplementations.SquaredMagnitude3(vector));
        }

        /// <inheritdoc/>
        public TResult Dot3<TFactor1, TFactor2>(TFactor1 a, TFactor2 b)
            where TFactor1 : IVector3Quantity
            where TFactor2 : IVector3Quantity
        {
            return TResult.WithMagnitude(MathImplementations.Dot3(a, b));
        }
    }

    /// <summary>Describes mathematical operations that result in a pure <see cref="SharpMeasures.Vector3"/>.</summary>
    private class PureVector3ResultingMaths : IVector3ResultingMaths<SharpMeasures.Vector3>
    {
        /// <summary>Retrieves the singleton instance of <see cref="PureVector3ResultingMaths"/>.</summary>
        public static PureVector3ResultingMaths Instance { get; } = new();

        /// <summary>Constructs a new <see cref="PureVector3ResultingMaths"/>.</summary>
        private PureVector3ResultingMaths() { }

        /// <inheritdoc/>
        public SharpMeasures.Vector3 Cross<TFactor1, TFactor2>(TFactor1 a, TFactor2 b)
            where TFactor1 : IVector3Quantity
            where TFactor2 : IVector3Quantity
        {
            return MathImplementations.Cross(a, b);
        }

        /// <inheritdoc/>
        public SharpMeasures.Vector3 Normalize(SharpMeasures.Vector3 vector)
        {
            return MathImplementations.Normalize(vector);
        }

        /// <inheritdoc/>
        public SharpMeasures.Vector3 Transform(SharpMeasures.Vector3 factor, Matrix4x4 transform)
        {
            return MathImplementations.Transform(factor, transform);
        }
    }

    /// <summary>Describes mathematical operations that result in a three-dimensional vector quantity <typeparamref name="TResult"/>.</summary>
    /// <typeparam name="TResult">The three-dimensional vector quantity that is the result of the described mathematical operations.</typeparam>
    private class Vector3ResultingMaths<TResult> : IVector3ResultingMaths<TResult>
        where TResult : IVector3Quantity<TResult>
    {
        /// <summary>Retrieves the singleton instance of <see cref="Vector3ResultingMaths{TResult}"/>.</summary>
        public static Vector3ResultingMaths<TResult> Instance { get; } = new();

        /// <summary>Constructs a new <see cref="PureVector3ResultingMaths"/>.</summary>
        private Vector3ResultingMaths() { }

        /// <inheritdoc/>
        public TResult Cross<TFactor1, TFactor2>(TFactor1 a, TFactor2 b)
            where TFactor1 : IVector3Quantity
            where TFactor2 : IVector3Quantity
        {
            return TResult.WithComponents(MathImplementations.Cross(a, b));
        }

        /// <inheritdoc/>
        public TResult Normalize(TResult vector)
        {
            return MathImplementations.Normalize(vector);
        }

        /// <inheritdoc/>
        public TResult Transform(TResult factor, Matrix4x4 transform)
        {
            return TResult.WithComponents(MathImplementations.Transform(factor, transform));
        }
    }
}
