namespace SharpMeasures;

using SharpMeasures.Maths;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>A measure of a four-dimensional vector quantity that is not covered by a designated type.</summary>
public readonly record struct Unhandled4 : IVector4Quantity<Unhandled4>
{
    /// <summary>The <see cref="Unhandled4"/> representing { 0, 0, 0 }.</summary>
    public static readonly Unhandled4 Zero = new(0, 0, 0, 0);
    /// <summary>The <see cref="Unhandled4"/> representing { 1, 1, 1 }.</summary>
    public static readonly Unhandled4 Ones = new(1, 1, 1, 1);

    /// <inheritdoc/>
    static Unhandled4 IVector4Quantity<Unhandled4>.WithComponents(Vector4 components) => new(components);
    /// <inheritdoc/>
    static Unhandled4 IVector4Quantity<Unhandled4>.WithComponents(Scalar x, Scalar y, Scalar z, Scalar w) => new(x, y, z, w);

    /// <summary>The X-component of <see langword="this"/>.</summary>
    public Unhandled X { get; }
    /// <summary>The Y-component of <see langword="this"/>.</summary>
    public Unhandled Y { get; }
    /// <summary>The Z-component of <see langword="this"/>.</summary>
    public Unhandled Z { get; }
    /// <summary>The W-component of <see langword="this"/>.</summary>
    public Unhandled W { get; }

    /// <inheritdoc/>
    Scalar IVector4Quantity.X => X.Magnitude;
    /// <inheritdoc/>
    Scalar IVector4Quantity.Y => Y.Magnitude;
    /// <inheritdoc/>
    Scalar IVector4Quantity.Z => Z.Magnitude;
    /// <inheritdoc/>
    Scalar IVector4Quantity.W => Z.Magnitude;

    /// <inheritdoc/>
    public Vector4 Components => (X.Magnitude, Y.Magnitude, Z.Magnitude, W.Magnitude);

    /// <summary>Constructs a new <see cref="Unhandled4"/> representing { <paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>, <paramref name="w"/> }.</summary>
    /// <param name="x">The X-component of the constructed <see cref="Unhandled4"/>.</param>
    /// <param name="y">The Y-component of the constructed <see cref="Unhandled4"/>.</param>
    /// <param name="z">The Z-component of the constructed <see cref="Unhandled4"/>.</param>
    /// <param name="w">The Z-component of the constructed <see cref="Unhandled4"/>.</param>
    public Unhandled4(Unhandled x, Unhandled y, Unhandled z, Unhandled w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>Constructs a new <see cref="Unhandled4"/> with components of magnitudes { <paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>, <paramref name="w"/> }.</summary>
    /// <param name="x">The magnitude of the X-component of the constructed <see cref="Unhandled4"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the constructed <see cref="Unhandled4"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the constructed <see cref="Unhandled4"/>.</param>
    /// <param name="w">The magnitude of the W-component of the constructed <see cref="Unhandled4"/>.</param>
    public Unhandled4(Scalar x, Scalar y, Scalar z, Scalar w) : this(new Unhandled(x), new Unhandled(y), new Unhandled(z), new Unhandled(w)) { }

    /// <summary>Constructs a new <see cref="Unhandled4"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the constructed <see cref="Unhandled3"/>.</param>
    public Unhandled4(Vector4 components) : this(components.X, components.Y, components.Z, components.W) { }

    /// <inheritdoc cref="Vector4.IsNaN"/>
    public bool IsNaN => X.IsNaN || Y.IsNaN || Z.IsNaN || W.IsNaN;
    /// <inheritdoc cref="Vector4.IsZero"/>
    public bool IsZero => (X.Magnitude.Value, Y.Magnitude.Value, Z.Magnitude.Value, W.Magnitude.Value) is (0, 0, 0, 0);
    /// <inheritdoc cref="Vector4.IsFinite"/>
    public bool IsFinite => X.IsFinite && Y.IsFinite && Z.IsFinite && W.IsFinite;
    /// <inheritdoc cref="Vector4.IsInfinite"/>
    public bool IsInfinite => X.IsInfinite || Y.IsInfinite || Z.IsInfinite || W.IsInfinite;

    /// <inheritdoc/>
    Scalar IVector4Quantity.Magnitude() => PureScalarMaths.Magnitude4(this);
    /// <inheritdoc/>
    Scalar IVector4Quantity.SquaredMagnitude() => PureScalarMaths.SquaredMagnitude4(this);

    /// <summary>Computes the magnitude / norm / length of <see langword="this"/>.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when applicable.</remarks>
    public Unhandled Magnitude() => ScalarMaths.Magnitude4(this);
    /// <summary>Computes the square of the magnitude / norm / length of <see langword="this"/>.</summary>
    public Unhandled SquaredMagnitude() => ScalarMaths.SquaredMagnitude4(this);

    /// <inheritdoc/>
    public Unhandled4 Normalize() => VectorMaths.Normalize(this);

    /// <summary>Produces a description of <see langword="this"/> containing the type and the represented components.</summary>
    public override string ToString() => $"{nameof(Unhandled3)}: ({X.Magnitude.Value}, {Y.Magnitude.Value}, {Z.Magnitude.Value}, {W.Magnitude.Value})";

    /// <summary>Deconstructs <see langword="this"/> into the components X, Y, Z, and W.</summary>
    /// <param name="x">The X-component of <see langword="this"/>.</param>
    /// <param name="y">The Y-component of <see langword="this"/>.</param>
    /// <param name="z">The Z-component of <see langword="this"/>.</param>
    /// <param name="w">The W-component of <see langword="this"/>.</param>
    public void Deconstruct(out Unhandled x, out Unhandled y, out Unhandled z, out Unhandled w)
    {
        x = X;
        y = Y;
        z = Z;
        w = W;
    }

    /// <inheritdoc/>
    public Unhandled4 Plus() => this;
    /// <inheritdoc/>
    public Unhandled4 Negate() => -this;

    /// <inheritdoc/>
    public Unhandled4 Add(Unhandled4 addend) => this + addend;
    /// <inheritdoc/>
    public Unhandled4 Subtract(Unhandled4 subtrahend) => this - subtrahend;
    /// <inheritdoc/>
    public Unhandled4 Multiply(Unhandled factor) => this * factor;
    /// <inheritdoc/>
    public Unhandled4 Divide(Unhandled divisor) => this / divisor;
    /// <inheritdoc/>
    public Unhandled Dot(Unhandled4 factor) => ScalarMaths.Dot4(this, factor);

    /// <inheritdoc/>
    public Unhandled4 Multiply(Scalar factor) => this * factor;
    /// <inheritdoc/>
    public Unhandled4 Divide(Scalar divisor) => this / divisor;

    /// <inheritdoc/>
    public static Unhandled4 operator +(Unhandled4 a) => a;
    /// <inheritdoc/>
    public static Unhandled4 operator -(Unhandled4 a) => (-a.X, -a.Y, -a.Z, -a.W);

    /// <inheritdoc/>
    public static Unhandled4 operator +(Unhandled4 a, Unhandled4 b) => (a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
    /// <inheritdoc/>
    public static Unhandled4 operator -(Unhandled4 a, Unhandled4 b) => (a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
    /// <inheritdoc/>
    public static Unhandled4 operator *(Unhandled4 a, Unhandled b) => (a.X * b, a.Y * b, a.Z * b, a.W * b);
    /// <inheritdoc/>
    public static Unhandled4 operator /(Unhandled4 a, Unhandled b) => (a.X / b, a.Y / b, a.Z / b, a.W / b);

    /// <inheritdoc/>
    public static Unhandled4 operator *(Unhandled4 a, Scalar b) => (a.X * b, a.Y * b, a.Z * b, a.W * b);
    /// <inheritdoc/>
    public static Unhandled4 operator *(Scalar a, Unhandled4 b) => (a * b.X, a * b.Y, a * b.Z, a * b.W);
    /// <inheritdoc/>
    public static Unhandled4 operator /(Unhandled4 a, Scalar b) => (a.X / b, a.Y / b, a.Z / b, a.W / b);

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 operator +(Unhandled4 a, IVector4Quantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return (a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 operator +(IVector4Quantity a, Unhandled4 b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return (a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 operator -(Unhandled4 a, IVector4Quantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return (a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 operator -(IVector4Quantity a, Unhandled4 b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return (a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 operator *(Unhandled4 a, IScalarQuantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return (a.X * b, a.Y * b, a.Z * b, a.W * b);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 operator *(IScalarQuantity a, Unhandled4 b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return (a * b.X, a * b.Y, a * b.Z, a * b.W);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 operator /(Unhandled4 a, IScalarQuantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return (a.X / b, a.Y / b, a.Z / b, a.W / b);
    }

    /// <summary>Constructs the <see cref="Unhandled4"/> with the elements of <paramref name="components"/> as components.</summary>
    [SuppressMessage("Usage", "CA2225", Justification = "Behaviour can be achieved through a constructor")]
    public static implicit operator Unhandled4((Unhandled X, Unhandled Y, Unhandled Z, Unhandled W) components) => new(components.X, components.Y, components.Z, components.W);

    /// <summary>Describes mathematical operations that result in a pure <see cref="Scalar"/>.</summary>
    private static IScalarResultingMaths<Scalar> PureScalarMaths { get; } = MathFactory.ScalarResult();
    /// <summary>Describes mathematical operations that result in <see cref="Unhandled"/>.</summary>
    private static IScalarResultingMaths<Unhandled> ScalarMaths { get; } = MathFactory.ScalarResult<Unhandled>();
    /// <summary>Describes mathematical operations that result in <see cref="Unhandled3"/>.</summary>
    private static IVector4ResultingMaths<Unhandled4> VectorMaths { get; } = MathFactory.Vector4Result<Unhandled4>();
}
