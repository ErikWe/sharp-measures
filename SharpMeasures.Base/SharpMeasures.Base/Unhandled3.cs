﻿namespace SharpMeasures;

using SharpMeasures.Maths;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

/// <summary>A measure of a three-dimensional vector quantity that is not covered by a designated type.</summary>
public readonly record struct Unhandled3 : IVector3Quantity<Unhandled3>
{
    /// <summary>The <see cref="Unhandled3"/> representing { 0, 0, 0 }.</summary>
    public static readonly Unhandled3 Zero = new(0, 0, 0);
    /// <summary>The <see cref="Unhandled3"/> representing { 1, 1, 1 }.</summary>
    public static readonly Unhandled3 Ones = new(1, 1, 1);

    /// <inheritdoc/>
    static Unhandled3 IVector3Quantity<Unhandled3>.WithComponents(Vector3 components) => new(components);
    /// <inheritdoc/>
    static Unhandled3 IVector3Quantity<Unhandled3>.WithComponents(Scalar x, Scalar y, Scalar z) => new(x, y, z);

    /// <summary>The X-component of <see langword="this"/>.</summary>
    public Unhandled X { get; }
    /// <summary>The Y-component of <see langword="this"/>.</summary>
    public Unhandled Y { get; }
    /// <summary>The Z-component of <see langword="this"/>.</summary>
    public Unhandled Z { get; }

    /// <inheritdoc/>
    Scalar IVector3Quantity.X => X.Magnitude;
    /// <inheritdoc/>
    Scalar IVector3Quantity.Y => Y.Magnitude;
    /// <inheritdoc/>
    Scalar IVector3Quantity.Z => Z.Magnitude;

    /// <inheritdoc/>
    public Vector3 Components => (X.Magnitude, Y.Magnitude, Z.Magnitude);

    /// <summary>Constructs a new <see cref="Unhandled3"/> representing { <paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/> }.</summary>
    /// <param name="x">The X-component of the constructed <see cref="Unhandled3"/>.</param>
    /// <param name="y">The Y-component of the constructed <see cref="Unhandled3"/>.</param>
    /// <param name="z">The Z-component of the constructed <see cref="Unhandled3"/>.</param>
    public Unhandled3(Unhandled x, Unhandled y, Unhandled z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>Constructs a new <see cref="Unhandled3"/> with components of magnitudes { <paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/> }.</summary>
    /// <param name="x">The magnitude of the X-component of the constructed <see cref="Unhandled3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the constructed <see cref="Unhandled3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the constructed <see cref="Unhandled3"/>.</param>
    public Unhandled3(Scalar x, Scalar y, Scalar z) : this(new Unhandled(x), new Unhandled(y), new Unhandled(z)) { }

    /// <summary>Constructs a new <see cref="Unhandled3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the constructed <see cref="Unhandled3"/>.</param>
    public Unhandled3(Vector3 components) : this(components.X, components.Y, components.Z) { }

    /// <inheritdoc cref="Vector3.IsNaN"/>
    public bool IsNaN => X.IsNaN || Y.IsNaN || Z.IsNaN;
    /// <inheritdoc cref="Vector3.IsZero"/>
    public bool IsZero => (X.Magnitude.Value, Y.Magnitude.Value, Z.Magnitude.Value) is (0, 0, 0);
    /// <inheritdoc cref="Vector3.IsFinite"/>
    public bool IsFinite => X.IsFinite && Y.IsFinite && Z.IsFinite;
    /// <inheritdoc cref="Vector3.IsInfinite"/>
    public bool IsInfinite => X.IsInfinite || Y.IsInfinite || Z.IsInfinite;

    /// <inheritdoc/>
    Scalar IVector3Quantity.Magnitude() => PureScalarMaths.Magnitude3(this);
    /// <inheritdoc/>
    Scalar IVector3Quantity.SquaredMagnitude() => PureScalarMaths.SquaredMagnitude3(this);

    /// <summary>Computes the magnitude / norm / length of <see langword="this"/>.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when applicable.</remarks>
    public Unhandled Magnitude() => ScalarMaths.Magnitude3(this);
    /// <summary>Computes the square of the magnitude / norm / length of <see langword="this"/>.</summary>
    public Unhandled SquaredMagnitude() => ScalarMaths.SquaredMagnitude3(this);

    /// <inheritdoc/>
    public Unhandled3 Normalize() => VectorMaths.Normalize(this);
    /// <inheritdoc/>
    public Unhandled3 Transform(Matrix4x4 transform) => VectorMaths.Transform(this, transform);

    /// <summary>Produces a description of <see langword="this"/> containing the represented components.</summary>
    public override string ToString() => $"({X.Magnitude.Value}, {Y.Magnitude.Value}, {Z.Magnitude.Value})";

    /// <summary>Deconstructs <see langword="this"/> into the components X, Y, and Z.</summary>
    /// <param name="x">The X-component of <see langword="this"/>.</param>
    /// <param name="y">The Y-component of <see langword="this"/>.</param>
    /// <param name="z">The Z-component of <see langword="this"/>.</param>
    public void Deconstruct(out Unhandled x, out Unhandled y, out Unhandled z)
    {
        x = X;
        y = Y;
        z = Z;
    }

    /// <inheritdoc/>
    public Unhandled3 Plus() => this;
    /// <inheritdoc/>
    public Unhandled3 Negate() => -this;

    /// <inheritdoc cref="Unhandled.Add(Unhandled)"/>
    public Unhandled3 Add(Unhandled3 addend) => this + addend;
    /// <inheritdoc cref="Unhandled.Subtract(Unhandled)"/>
    public Unhandled3 Subtract(Unhandled3 subtrahend) => this - subtrahend;
    /// <inheritdoc cref="Unhandled.Multiply(Unhandled)"/>
    public Unhandled3 Multiply(Unhandled factor) => this * factor;
    /// <inheritdoc cref="Unhandled.Divide(Unhandled)"/>
    public Unhandled3 Divide(Unhandled divisor) => this / divisor;
    /// <inheritdoc cref="Vector3.Dot(Vector3)"/>
    public Unhandled Dot(Unhandled3 factor) => ScalarMaths.Dot3(this, factor);
    /// <inheritdoc cref="Vector3.Cross(Vector3)"/>
    public Unhandled3 Cross(Unhandled3 factor) => VectorMaths.Cross(this, factor);

    /// <inheritdoc/>
    public Unhandled3 Multiply(Scalar factor) => this * factor;
    /// <inheritdoc/>
    public Unhandled3 Divide(Scalar divisor) => this / divisor;

    /// <inheritdoc cref="Cross(Unhandled3)"/>
    public Unhandled3 Cross(Vector3 factor) => VectorMaths.Cross(this, factor);

    /// <inheritdoc/>
    public static Unhandled3 operator +(Unhandled3 a) => a;
    /// <inheritdoc/>
    public static Unhandled3 operator -(Unhandled3 a) => (-a.X, -a.Y, -a.Z);

    /// <inheritdoc cref="Vector3.operator +(Vector3, Vector3)"/>
    public static Unhandled3 operator +(Unhandled3 a, Unhandled3 b) => (a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    /// <inheritdoc cref="Vector3.operator -(Vector3, Vector3)"/>
    public static Unhandled3 operator -(Unhandled3 a, Unhandled3 b) => (a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    /// <inheritdoc cref="Vector3.operator *(Vector3, Scalar)"/>
    public static Unhandled3 operator *(Unhandled3 a, Unhandled b) => (a.X * b, a.Y * b, a.Z * b);
    /// <inheritdoc cref="Vector3.operator *(Scalar, Vector3)"/>
    public static Unhandled3 operator *(Unhandled a, Unhandled3 b) => (a * b.X, a * b.Y, a * b.Z);
    /// <inheritdoc cref="Vector3.operator /(Vector3, Scalar)"/>
    public static Unhandled3 operator /(Unhandled3 a, Unhandled b) => (a.X / b, a.Y / b, a.Z / b);

    /// <inheritdoc/>
    public static Unhandled3 operator *(Unhandled3 a, Scalar b) => (a.X * b, a.Y * b, a.Z * b);
    /// <inheritdoc/>
    public static Unhandled3 operator *(Scalar a, Unhandled3 b) => (a * b.X, a * b.Y, a * b.Z);
    /// <inheritdoc/>
    public static Unhandled3 operator /(Unhandled3 a, Scalar b) => (a.X / b, a.Y / b, a.Z / b);

    /// <inheritdoc cref="Vector3.operator +(Vector3, Vector3)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator +(Unhandled3 a, IVector3Quantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return (a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

    /// <inheritdoc cref="Vector3.operator +(Vector3, Vector3)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator +(IVector3Quantity a, Unhandled3 b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return (a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

    /// <inheritdoc cref="Vector3.operator -(Vector3, Vector3)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator -(Unhandled3 a, IVector3Quantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return (a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }

    /// <inheritdoc cref="Vector3.operator -(Vector3, Vector3)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator -(IVector3Quantity a, Unhandled3 b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return (a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }

    /// <inheritdoc cref="Vector3.operator *(Vector3, Scalar)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(Unhandled3 a, IScalarQuantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return (a.X * b, a.Y * b, a.Z * b);
    }

    /// <inheritdoc cref="Vector3.operator *(Scalar, Vector3)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(IScalarQuantity a, Unhandled3 b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return (a * b.X, a * b.Y, a * b.Z);
    }

    /// <inheritdoc cref="Vector3.operator /(Vector3, Scalar)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator /(Unhandled3 a, IScalarQuantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return (a.X / b, a.Y / b, a.Z / b);
    }

    /// <summary>Constructs the <see cref="Unhandled3"/> with the elements of <paramref name="components"/> as components.</summary>
    [SuppressMessage("Usage", "CA2225", Justification = "Behaviour can be achieved through a constructor")]
    public static implicit operator Unhandled3((Unhandled X, Unhandled Y, Unhandled Z) components) => new(components.X, components.Y, components.Z);

    /// <summary>Constructs the tuple (<see cref="Unhandled"/>, <see cref="Unhandled"/>, <see cref="Unhandled"/>) with the elements of <paramref name="vector"/>.</summary>
    [SuppressMessage("Usage", "CA2225", Justification = "Behaviour can be achieved through the deconstructor")]
    public static implicit operator (Unhandled X, Unhandled Y, Unhandled Z)(Unhandled3 vector) => (vector.X, vector.Y, vector.Z);

    /// <summary>Describes mathematical operations that result in a pure <see cref="Scalar"/>.</summary>
    private static IScalarResultingMaths<Scalar> PureScalarMaths { get; } = MathFactory.ScalarResult();
    /// <summary>Describes mathematical operations that result in <see cref="Unhandled"/>.</summary>
    private static IScalarResultingMaths<Unhandled> ScalarMaths { get; } = MathFactory.ScalarResult<Unhandled>();
    /// <summary>Describes mathematical operations that result in <see cref="Unhandled3"/>.</summary>
    private static IVector3ResultingMaths<Unhandled3> VectorMaths { get; } = MathFactory.Vector3Result<Unhandled3>();
}