namespace SharpMeasures;

using SharpMeasures.Maths;
using SharpMeasures.Vector3Abstractions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

/// <summary>A pure three-dimensional vector, representing { X, Y, Z }.</summary>
public readonly record struct Vector3 :
    IVector3Quantity<Vector3>,
    IAddendVector3Quantity<Vector3>,
    IMinuendVector3Quantity<Vector3>,
    ISubtrahendVector3Quantity<Vector3>,
    IFactorVector3Quantity<Vector3, Vector3, Scalar>,
    IDividendVector3Quantity<Vector3, Vector3, Scalar>,
    IDotFactorVector3Quantity<Scalar, Vector3>,
    ICrossFactorVector3Quantity<Vector3, Vector3>
{
    /// <summary>The <see cref="Vector3"/> representing { 0, 0, 0 }.</summary>
    public static Vector3 Zero { get; } = (0, 0, 0);
    /// <summary>The <see cref="Vector3"/> representing (1, 1, 1).</summary>
    public static Vector3 Ones { get; } = (1, 1, 1);

    /// <inheritdoc/>
    static Vector3 IVector3Quantity<Vector3>.WithComponents(Vector3 components) => components;
    /// <inheritdoc/>
    static Vector3 IVector3Quantity<Vector3>.WithComponents(Scalar x, Scalar y, Scalar z) => (x, y, z);

    /// <summary>The X-component of <see langword="this"/>.</summary>
    public Scalar X { get; }
    /// <summary>The Y-component of <see langword="this"/>.</summary>
    public Scalar Y { get; }
    /// <summary>The Z-component of <see langword="this"/>.</summary>
    public Scalar Z { get; }

    /// <inheritdoc/>
    Vector3 IVector3Quantity.Components => this;

    /// <summary>Constructs a new <see cref="Vector3"/> representing { <paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/> }.</summary>
    /// <param name="x">The X-component of the constructed <see cref="Vector3"/>.</param>
    /// <param name="y">The Y-component of the constructed <see cref="Vector3"/>.</param>
    /// <param name="z">The Z-component of the constructed <see cref="Vector3"/>.</param>
    public Vector3(Scalar x, Scalar y, Scalar z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>Indicates whether the X, Y, or Z component represented by <see langword="this"/> represents { <see cref="double.NaN"/> }.</summary>
    public bool IsNaN => X.IsNaN || Y.IsNaN || Z.IsNaN;
    /// <summary>Indicates whether <see langword="this"/> represents { 0, 0, 0 }.</summary>
    public bool IsZero => (X.Value, Y.Value, Z.Value) is (0, 0, 0);
    /// <summary>Indicates whether the X, Y, and Z components represented by <see langword="this"/> represent finite values.</summary>
    public bool IsFinite => X.IsFinite && Y.IsFinite && Z.IsFinite;
    /// <summary>Indicates whether the X, Y, or Z component represented by <see langword="this"/> represents an infinite value.</summary>
    public bool IsInfinite => X.IsInfinite || Y.IsInfinite || Z.IsInfinite;

    /// <inheritdoc/>
    public Scalar Magnitude() => ScalarMaths.Magnitude3(this);
    /// <inheritdoc/>
    public Scalar SquaredMagnitude() => ScalarMaths.SquaredMagnitude3(this);

    /// <inheritdoc/>
    public Vector3 Normalize() => VectorMaths.Normalize(this);
    /// <inheritdoc/>
    public Vector3 Transform(Matrix4x4 transform) => VectorMaths.Transform(this, transform);

    /// <summary>Produces a description of <see langword="this"/> containing the type and the represented components.</summary>
    public override string ToString() => $"{nameof(Vector3)}: ({X.Value}, {Y.Value}, {Z.Value})";

    /// <summary>Deconstructs <see langword="this"/> into the components X, Y, and Z.</summary>
    /// <param name="x">The X-component of <see langword="this"/>.</param>
    /// <param name="y">The Y-component of <see langword="this"/>.</param>
    /// <param name="z">The Z-component of <see langword="this"/>.</param>
    public void Deconstruct(out Scalar x, out Scalar y, out Scalar z)
    {
        x = X;
        y = Y;
        z = Z;
    }

    /// <inheritdoc/>
    public Vector3 Plus() => this;
    /// <inheritdoc/>
    public Vector3 Negate() => -this;

    /// <inheritdoc/>
    public Vector3 Add(Vector3 addend) => this + addend;
    /// <inheritdoc/>
    public Vector3 Subtract(Vector3 subtrahend) => this - subtrahend;
    /// <inheritdoc/>
    Vector3 ISubtrahendVector3Quantity<Vector3, Vector3>.SubtractFrom(Vector3 minuend) => minuend - this;
    /// <inheritdoc/>
    public Vector3 Multiply(Scalar factor) => this * factor;
    /// <inheritdoc/>
    public Vector3 Divide(Scalar divisor) => this / divisor;
    /// <inheritdoc/>
    public Vector3 Remainder(Scalar divisor) => this % divisor;
    /// <inheritdoc/>
    public Scalar Dot(Vector3 factor) => ScalarMaths.Dot3(this, factor);
    /// <inheritdoc/>
    public Vector3 Cross(Vector3 factor) => VectorMaths.Cross(this, factor);
    /// <inheritdoc/>
    Vector3 ICrossFactorVector3Quantity<Vector3, Vector3>.CrossInto(Vector3 factor) => VectorMaths.Cross(factor, this);

    /// <inheritdoc cref="Cross(Vector3)"/>
    /// <typeparam name="TFactor">The three-dimensional vector quantity that represents the second factor of { <see langword="this"/> ⨯ <paramref name="factor"/> }.</typeparam>
    public TFactor Cross<TFactor>(TFactor factor) where TFactor : IVector3Quantity<TFactor> => MathFactory.Vector3Result<TFactor>().Cross(this, factor);
    /// <inheritdoc cref="CrossInto(Vector3)"/>
    /// <typeparam name="TFactor">The three-dimensional vector quantity that represents the first factor of { <paramref name="factor"/> ⨯ <see langword="this"/> }.</typeparam>
    public TFactor CrossInto<TFactor>(TFactor factor) where TFactor : IVector3Quantity<TFactor> => MathFactory.Vector3Result<TFactor>().Cross(this, factor);

    /// <inheritdoc/>
    public static Vector3 operator +(Vector3 a) => a;
    /// <inheritdoc/>
    public static Vector3 operator -(Vector3 a) => (-a.X, -a.Y, -a.Z);
    /// <inheritdoc/>
    public static Vector3 operator +(Vector3 a, Vector3 b) => (a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    /// <inheritdoc/>
    public static Vector3 operator -(Vector3 a, Vector3 b) => (a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    /// <inheritdoc/>
    public static Vector3 operator *(Vector3 a, Scalar b) => (a.X * b, a.Y * b, a.Z * b);
    /// <inheritdoc/>
    public static Vector3 operator *(Scalar a, Vector3 b) => (a * b.X, a * b.Y, a * b.Z);
    /// <inheritdoc/>
    public static Vector3 operator /(Vector3 a, Scalar b) => (a.X / b, a.Y / b, a.Z / b);
    /// <inheritdoc/>
    public static Vector3 operator %(Vector3 a, Scalar b) => (a.X % b, a.Y % b, a.Z % b);

    /// <summary>Constructs the <see cref="Vector3"/> with components equal to the values of <paramref name="components"/>.</summary>
    [SuppressMessage("Usage", "CA2225", Justification = "Behaviour can be achieved through a constructor")]
    public static implicit operator Vector3((Scalar X, Scalar Y, Scalar Z) components) => new(components.X, components.Y, components.Z);

    /// <summary>Describes mathematical operations that result in a pure <see cref="Scalar"/>.</summary>
    private static IScalarResultingMaths<Scalar> ScalarMaths { get; } = MathFactory.ScalarResult();
    /// <summary>Describes mathematical operations that result in a pure <see cref="Vector3"/>.</summary>
    private static IVector3ResultingMaths<Vector3> VectorMaths { get; } = MathFactory.Vector3Result();
}
