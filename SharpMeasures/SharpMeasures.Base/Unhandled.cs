namespace SharpMeasures;

using SharpMeasures.Maths;
using SharpMeasures.ScalarAbstractions;
using SharpMeasures.Vector3Abstractions;

using System;

/// <summary>A measure of a scalar quantity that is not covered by a designated type.</summary>
public readonly record struct Unhandled :
    IComparable<Unhandled>,
    IScalarQuantity<Unhandled>,
    IReciprocalScalarQuantity<Unhandled>,
    ISquareScalarQuantity<Unhandled>,
    ICubeScalarQuantity<Unhandled>,
    ISquareRootScalarQuantity<Unhandled>,
    ICubeRootScalarQuantity<Unhandled>,
    IAddendScalarQuantity<Unhandled>,
    IMinuendScalarQuantity<Unhandled>,
    ISubtrahendScalarQuantity<Unhandled>,
    IFactorScalarQuantity<Unhandled, Unhandled, Unhandled>,
    IDividendScalarQuantity<Unhandled, Unhandled, Unhandled>,
    IDivisorScalarQuantity<Unhandled, Unhandled, Unhandled>,
    IFactor3ScalarQuantity<Unhandled, Unhandled3, Unhandled3>,
    IDivisor3ScalarQuantity<Unhandled, Unhandled3, Unhandled3>,
    IAddendScalarQuantity<Unhandled, Unhandled, IScalarQuantity>,
    IMinuendScalarQuantity<Unhandled, Unhandled, IScalarQuantity>,
    ISubtrahendScalarQuantity<Unhandled, Unhandled, IScalarQuantity>,
    IFactorScalarQuantity<Unhandled, Unhandled, IScalarQuantity>,
    IDividendScalarQuantity<Unhandled, Unhandled, IScalarQuantity>,
    IDivisorScalarQuantity<Unhandled, Unhandled, IScalarQuantity>,
    IFactor3ScalarQuantity<Unhandled, Unhandled3, IVector3Quantity>,
    IDivisor3ScalarQuantity<Unhandled, Unhandled3, IVector3Quantity>
{
    /// <summary>The <see cref="Unhandled"/> representing { 0 }.</summary>
    public static Unhandled Zero { get; } = new(0);
    /// <summary>The <see cref="Unhandled"/> representing { 1 }.</summary>
    public static Unhandled One { get; } = new(1);

    /// <inheritdoc/>
    static Unhandled IScalarQuantity<Unhandled>.WithMagnitude(Scalar magnitude) => new(magnitude);

    /// <summary>The magnitude of <see langword="this"/>.</summary>
    public Scalar Magnitude { get; }

    /// <summary>Constructs a new <see cref="Unhandled"/> representing { <paramref name="magnitude"/> }.</summary>
    /// <param name="magnitude">The magnitude of the constructed <see cref="Unhandled"/>.</param>
    public Unhandled(Scalar magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts <see langword="this"/> to a scalar quantity <typeparamref name="TScalar"/> representing the same magnitude.</summary>
    /// <typeparam name="TScalar"><see langword="this"/> is converted to a scalar quantity of this type.</typeparam>
    public TScalar As<TScalar>() where TScalar : IScalarQuantity<TScalar> => TScalar.WithMagnitude(Magnitude);

    /// <inheritdoc cref="Scalar.IsNaN"/>
    public bool IsNaN => Magnitude.IsNaN;
    /// <inheritdoc cref="Scalar.IsZero"/>
    public bool IsZero => Magnitude.IsZero;
    /// <inheritdoc cref="Scalar.IsPositive"/>
    public bool IsPositive => Magnitude.IsPositive;
    /// <inheritdoc cref="Scalar.IsNegative"/>
    public bool IsNegative => Magnitude.IsNegative;
    /// <inheritdoc cref="Scalar.IsFinite"/>
    public bool IsFinite => Magnitude.IsFinite;
    /// <inheritdoc cref="Scalar.IsInfinite"/>
    public bool IsInfinite => Magnitude.IsInfinite;
    /// <inheritdoc cref="Scalar.IsPositiveInfinity"/>
    public bool IsPositiveInfinity => Magnitude.IsPositiveInfinity;
    /// <inheritdoc cref="Scalar.IsNegativeInfinity"/>v
    public bool IsNegativeInfinity => Magnitude.IsNegativeInfinity;

    /// <inheritdoc cref="Scalar.Absolute"/>
    public Unhandled Absolute() => ScalarMaths.Absolute(this);

    /// <inheritdoc/>
    public Unhandled Reciprocal() => ScalarMaths.Reciprocal(this);
    /// <inheritdoc/>
    public Unhandled Square() => ScalarMaths.Square(this);
    /// <inheritdoc/>
    public Unhandled Cube() => ScalarMaths.Cube(this);
    /// <inheritdoc/>
    public Unhandled SquareRoot() => ScalarMaths.SquareRoot(this);
    /// <inheritdoc/>
    public Unhandled CubeRoot() => ScalarMaths.CubeRoot(this);

    /// <inheritdoc cref="Scalar.CompareTo(Scalar)"/>
    public int CompareTo(Unhandled other) => Magnitude.CompareTo(other.Magnitude);
    /// <inheritdoc cref="Scalar.ToString"/>
    public override string ToString() => $"{nameof(Unhandled)}: {Magnitude.Value}";

    /// <inheritdoc/>
    public Unhandled Plus() => this;
    /// <inheritdoc/>
    public Unhandled Negate() => -this;

    /// <inheritdoc/>
    public Unhandled Add(Unhandled addend) => this + addend;
    /// <inheritdoc/>
    public Unhandled Subtract(Unhandled subtrahend) => this - subtrahend;
    /// <inheritdoc/>
    Unhandled ISubtrahendScalarQuantity<Unhandled, Unhandled>.SubtractFrom(Unhandled minuend) => minuend - this;
    /// <inheritdoc/>
    public Unhandled Multiply(Unhandled factor) => this * factor;
    /// <inheritdoc/>
    public Unhandled Divide(Unhandled divisor) => this / divisor;
    /// <inheritdoc/>
    Unhandled IDivisorScalarQuantity<Unhandled, Unhandled>.DivideInto(Unhandled dividend) => dividend / this;

    /// <inheritdoc/>
    public Unhandled Multiply(Scalar factor) => this * factor;
    /// <inheritdoc/>
    public Unhandled Divide(Scalar divisor) => this / divisor;

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    Unhandled IAddendScalarQuantity<Unhandled, IScalarQuantity>.Add(IScalarQuantity addend) => this + addend;
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    Unhandled IMinuendScalarQuantity<Unhandled, IScalarQuantity>.Subtract(IScalarQuantity subtrahend) => this - subtrahend;
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    Unhandled ISubtrahendScalarQuantity<Unhandled, IScalarQuantity>.SubtractFrom(IScalarQuantity minuend) => minuend - this;
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    Unhandled IFactorScalarQuantity<Unhandled, IScalarQuantity>.Multiply(IScalarQuantity factor) => this * factor;
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    Unhandled IDividendScalarQuantity<Unhandled, IScalarQuantity>.Divide(IScalarQuantity divisor) => this / divisor;
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    Unhandled IDivisorScalarQuantity<Unhandled, IScalarQuantity>.DivideInto(IScalarQuantity dividend) => dividend / this;

    /// <inheritdoc/>
    Unhandled3 IFactor3ScalarQuantity<Unhandled3, Unhandled3>.Multiply(Unhandled3 factor) => this * factor;
    /// <inheritdoc/>
    Unhandled3 IDivisor3ScalarQuantity<Unhandled3, Unhandled3>.DivideInto(Unhandled3 dividend) => dividend / this;

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    Unhandled3 IFactor3ScalarQuantity<Unhandled3, IVector3Quantity>.Multiply(IVector3Quantity factor) => this * factor;
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    Unhandled3 IDivisor3ScalarQuantity<Unhandled3, IVector3Quantity>.DivideInto(IVector3Quantity dividend) => dividend / this;

    /// <inheritdoc/>
    public static Unhandled operator +(Unhandled x) => x;
    /// <inheritdoc/>
    public static Unhandled operator -(Unhandled x) => new(-x.Magnitude);

    /// <inheritdoc/>
    public static Unhandled operator +(Unhandled x, Unhandled y) => new(x.Magnitude + y.Magnitude);
    /// <inheritdoc/>
    public static Unhandled operator -(Unhandled x, Unhandled y) => new(x.Magnitude - y.Magnitude);
    /// <inheritdoc/>
    public static Unhandled operator *(Unhandled x, Unhandled y) => new(x.Magnitude * y.Magnitude);
    /// <inheritdoc/>
    public static Unhandled operator /(Unhandled x, Unhandled y) => new(x.Magnitude / y.Magnitude);

    /// <inheritdoc/>
    public static Unhandled operator *(Unhandled x, Scalar y) => new(x.Magnitude * y);
    /// <inheritdoc/>
    public static Unhandled operator *(Scalar x, Unhandled y) => new(x * y.Magnitude);
    /// <inheritdoc/>
    public static Unhandled operator /(Unhandled x, Scalar y) => new(x.Magnitude / y);

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator +(Unhandled x, IScalarQuantity y)
    {
        ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude + y.Magnitude);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator +(IScalarQuantity x, Unhandled y)
    {
        ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude + y.Magnitude);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator -(Unhandled x, IScalarQuantity y)
    {
        ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude - y.Magnitude);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator -(IScalarQuantity x, Unhandled y)
    {
        ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude - y.Magnitude);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Unhandled x, IScalarQuantity y)
    {
        ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude * y.Magnitude);
    }

    /// <inheritdoc cref="operator *(Unhandled, IScalarQuantity)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(IScalarQuantity x, Unhandled y)
    {
        ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude * y.Magnitude);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Unhandled x, IScalarQuantity y)
    {
        ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude / y.Magnitude);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(IScalarQuantity x, Unhandled y)
    {
        ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude / y.Magnitude);
    }

    /// <inheritdoc/>
    public static Unhandled3 operator *(Unhandled a, Unhandled3 b) => new(a * b.X, a * b.Y, a * b.Z);
    /// <inheritdoc/>
    static Unhandled3 IFactor3ScalarQuantity<Unhandled, Unhandled3, Unhandled3>.operator *(Unhandled3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    /// <inheritdoc/>
    static Unhandled3 IDivisor3ScalarQuantity<Unhandled, Unhandled3, Unhandled3>.operator /(Unhandled3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(Unhandled a, IVector3Quantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return new(a * b.X, a * b.Y, a * b.Z);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(IVector3Quantity a, Unhandled b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return new(a.X * b, a.Y * b, a.Z * b);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator /(IVector3Quantity a, Unhandled b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return new(a.X / b, a.Y / b, a.Z / b);
    }

    /// <inheritdoc cref="Scalar.operator &lt;(Scalar, Scalar)"/>
    public static bool operator <(Unhandled x, Unhandled y) => x.Magnitude.Value < y.Magnitude.Value;
    /// <inheritdoc cref="Scalar.operator &gt;(Scalar, Scalar)"/>
    public static bool operator >(Unhandled x, Unhandled y) => x.Magnitude.Value > y.Magnitude.Value;
    /// <inheritdoc cref="Scalar.operator &lt;=(Scalar, Scalar)"/>
    public static bool operator <=(Unhandled x, Unhandled y) => x.Magnitude.Value <= y.Magnitude.Value;
    /// <inheritdoc cref="Scalar.operator &gt;=(Scalar, Scalar)"/>
    public static bool operator >=(Unhandled x, Unhandled y) => x.Magnitude.Value >= y.Magnitude.Value;

    /// <summary>Describes mathematical operations that result in <see cref="Unhandled"/>.</summary>
    private static IScalarResultingMaths<Unhandled> ScalarMaths { get; } = MathFactory.ScalarResult<Unhandled>();
}
