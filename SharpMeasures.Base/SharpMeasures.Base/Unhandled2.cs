namespace SharpMeasures;

using SharpMeasures.Maths;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>A measure of a two-dimensional vector quantity that is not covered by a designated type.</summary>
public readonly record struct Unhandled2 : IVector2Quantity<Unhandled2>
{
    /// <summary>The <see cref="Unhandled2"/> representing { 0, 0 }.</summary>
    public static readonly Unhandled2 Zero = new(0, 0);
    /// <summary>The <see cref="Unhandled2"/> representing { 1, 1 }.</summary>
    public static readonly Unhandled2 Ones = new(1, 1);

    /// <inheritdoc/>
    static Unhandled2 IVector2Quantity<Unhandled2>.WithComponents(Vector2 components) => new(components);
    /// <inheritdoc/>
    static Unhandled2 IVector2Quantity<Unhandled2>.WithComponents(Scalar x, Scalar y) => new(x, y);

    /// <summary>The X-component of <see langword="this"/>.</summary>
    public Unhandled X { get; }
    /// <summary>The Y-component of <see langword="this"/>.</summary>
    public Unhandled Y { get; }

    /// <inheritdoc/>
    Scalar IVector2Quantity.X => X.Magnitude;
    /// <inheritdoc/>
    Scalar IVector2Quantity.Y => Y.Magnitude;

    /// <inheritdoc/>
    public Vector2 Components => (X.Magnitude, Y.Magnitude);

    /// <summary>Constructs a new <see cref="Unhandled2"/> representing { <paramref name="x"/>, <paramref name="y"/> }.</summary>
    /// <param name="x">The X-component of the constructed <see cref="Unhandled2"/>.</param>
    /// <param name="y">The Y-component of the constructed <see cref="Unhandled2"/>.</param>
    public Unhandled2(Unhandled x, Unhandled y)
    {
        X = x;
        Y = y;
    }

    /// <summary>Constructs a new <see cref="Unhandled2"/> with components of magnitudes { <paramref name="x"/>, <paramref name="y"/> }.</summary>
    /// <param name="x">The magnitude of the X-component of the constructed <see cref="Unhandled2"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the constructed <see cref="Unhandled2"/>.</param>
    public Unhandled2(Scalar x, Scalar y) : this(new Unhandled(x), new Unhandled(y)) { }

    /// <summary>Constructs a new <see cref="Unhandled2"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the constructed <see cref="Unhandled2"/>.</param>
    public Unhandled2(Vector2 components) : this(components.X, components.Y) { }

    /// <inheritdoc cref="Vector2.IsNaN"/>
    public bool IsNaN => X.IsNaN || Y.IsNaN;
    /// <inheritdoc cref="Vector2.IsZero"/>
    public bool IsZero => (X.Magnitude.Value, Y.Magnitude.Value) is (0, 0);
    /// <inheritdoc cref="Vector2.IsFinite"/>
    public bool IsFinite => X.IsFinite && Y.IsFinite;
    /// <inheritdoc cref="Vector2.IsInfinite"/>
    public bool IsInfinite => X.IsInfinite || Y.IsInfinite;

    /// <inheritdoc/>
    Scalar IVector2Quantity.Magnitude() => PureScalarMaths.Magnitude2(this);
    /// <inheritdoc/>
    Scalar IVector2Quantity.SquaredMagnitude() => PureScalarMaths.SquaredMagnitude2(this);

    /// <summary>Computes the magnitude / norm / length of <see langword="this"/>.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when applicable.</remarks>
    public Unhandled Magnitude() => ScalarMaths.Magnitude2(this);
    /// <summary>Computes the square of the magnitude / norm / length of <see langword="this"/>.</summary>
    public Unhandled SquaredMagnitude() => ScalarMaths.SquaredMagnitude2(this);

    /// <inheritdoc/>
    public Unhandled2 Normalize() => VectorMaths.Normalize(this);

    /// <summary>Produces a description of <see langword="this"/> containing the represented components.</summary>
    public override string ToString() => $"({X.Magnitude.Value}, {Y.Magnitude.Value})";

    /// <summary>Deconstructs <see langword="this"/> into the components X and Y.</summary>
    /// <param name="x">The X-component of <see langword="this"/>.</param>
    /// <param name="y">The Y-component of <see langword="this"/>.</param>
    public void Deconstruct(out Unhandled x, out Unhandled y)
    {
        x = X;
        y = Y;
    }

    /// <inheritdoc/>
    public Unhandled2 Plus() => this;
    /// <inheritdoc/>
    public Unhandled2 Negate() => -this;

    /// <inheritdoc cref="Unhandled.Add(Unhandled)"/>
    public Unhandled2 Add(Unhandled2 addend) => this + addend;
    /// <inheritdoc cref="Unhandled.Subtract(Unhandled)"/>
    public Unhandled2 Subtract(Unhandled2 subtrahend) => this - subtrahend;
    /// <inheritdoc cref="Unhandled.Multiply(Unhandled)"/>
    public Unhandled2 Multiply(Unhandled factor) => this * factor;
    /// <inheritdoc cref="Unhandled.Divide(Unhandled)"/>
    public Unhandled2 Divide(Unhandled divisor) => this / divisor;
    /// <inheritdoc cref="Vector2.Dot(Vector2)"/>
    public Unhandled Dot(Unhandled2 factor) => ScalarMaths.Dot2(this, factor);

    /// <inheritdoc/>
    public Unhandled2 Multiply(Scalar factor) => this * factor;
    /// <inheritdoc/>
    public Unhandled2 Divide(Scalar divisor) => this / divisor;

    /// <inheritdoc/>
    public static Unhandled2 operator +(Unhandled2 a) => a;
    /// <inheritdoc/>
    public static Unhandled2 operator -(Unhandled2 a) => (-a.X, -a.Y);

    /// <inheritdoc cref="Vector2.operator +(Vector2, Vector2)"/>
    public static Unhandled2 operator +(Unhandled2 a, Unhandled2 b) => (a.X + b.X, a.Y + b.Y);
    /// <inheritdoc cref="Vector2.operator -(Vector2, Vector2)"/>
    public static Unhandled2 operator -(Unhandled2 a, Unhandled2 b) => (a.X - b.X, a.Y - b.Y);
    /// <inheritdoc cref="Vector2.operator *(Vector2, Scalar)"/>
    public static Unhandled2 operator *(Unhandled2 a, Unhandled b) => (a.X * b, a.Y * b);
    /// <inheritdoc cref="Vector2.operator *(Scalar, Vector2)"/>
    public static Unhandled2 operator *(Unhandled a, Unhandled2 b) => (a * b.X, a * b.Y);
    /// <inheritdoc cref="Vector2.operator /(Vector2, Scalar)"/>
    public static Unhandled2 operator /(Unhandled2 a, Unhandled b) => (a.X / b, a.Y / b);

    /// <inheritdoc/>
    public static Unhandled2 operator *(Unhandled2 a, Scalar b) => (a.X * b, a.Y * b);
    /// <inheritdoc/>
    public static Unhandled2 operator *(Scalar a, Unhandled2 b) => (a * b.X, a * b.Y);
    /// <inheritdoc/>
    public static Unhandled2 operator /(Unhandled2 a, Scalar b) => (a.X / b, a.Y / b);

    /// <inheritdoc cref="Vector2.operator +(Vector2, Vector2)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 operator +(Unhandled2 a, IVector2Quantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return (a.X + b.X, a.Y + b.Y);
    }

    /// <inheritdoc cref="Vector2.operator +(Vector2, Vector2)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 operator +(IVector2Quantity a, Unhandled2 b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return (a.X + b.X, a.Y + b.Y);
    }

    /// <inheritdoc cref="Vector2.operator -(Vector2, Vector2)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 operator -(Unhandled2 a, IVector2Quantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return (a.X - b.X, a.Y - b.Y);
    }

    /// <inheritdoc cref="Vector2.operator -(Vector2, Vector2)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 operator -(IVector2Quantity a, Unhandled2 b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return (a.X - b.X, a.Y - b.Y);
    }

    /// <inheritdoc cref="Vector2.operator *(Vector2, Scalar)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 operator *(Unhandled2 a, IScalarQuantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return (a.X * b, a.Y * b);
    }

    /// <inheritdoc cref="Vector2.operator *(Scalar, Vector2)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 operator *(IScalarQuantity a, Unhandled2 b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return (a * b.X, a * b.Y);
    }

    /// <inheritdoc cref="Vector2.operator /(Vector2, Scalar)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 operator /(Unhandled2 a, IScalarQuantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return (a.X / b, a.Y / b);
    }

    /// <summary>Constructs the <see cref="Unhandled2"/> with the elements of <paramref name="components"/> as components.</summary>
    [SuppressMessage("Usage", "CA2225", Justification = "Behaviour can be achieved through a constructor")]
    public static implicit operator Unhandled2((Unhandled X, Unhandled Y) components) => new(components.X, components.Y);

    /// <summary>Constructs the tuple (<see cref="Unhandled"/>, <see cref="Unhandled"/>) with the elements of <paramref name="vector"/>.</summary>
    [SuppressMessage("Usage", "CA2225", Justification = "Behaviour can be achieved through the deconstructor")]
    public static implicit operator (Unhandled X, Unhandled Y)(Unhandled2 vector) => (vector.X, vector.Y);

    /// <summary>Describes mathematical operations that result in a pure <see cref="Scalar"/>.</summary>
    private static IScalarResultingMaths<Scalar> PureScalarMaths { get; } = MathFactory.ScalarResult();
    /// <summary>Describes mathematical operations that result in <see cref="Unhandled"/>.</summary>
    private static IScalarResultingMaths<Unhandled> ScalarMaths { get; } = MathFactory.ScalarResult<Unhandled>();
    /// <summary>Describes mathematical operations that result in <see cref="Unhandled2"/>.</summary>
    private static IVector2ResultingMaths<Unhandled2> VectorMaths { get; } = MathFactory.Vector2Result<Unhandled2>();
}
