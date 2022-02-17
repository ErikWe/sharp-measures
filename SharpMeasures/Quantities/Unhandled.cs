namespace ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>A measure of a scalar quantity that is not covered by a designated type.</summary>
public readonly record struct Unhandled :
    IComparable<Unhandled>,
    IScalarQuantity,
    IScalableScalarQuantity<Unhandled>,
    IInvertibleScalarQuantity<Unhandled>,
    ISquarableScalarQuantity<Unhandled>,
    ICubableScalarQuantity<Unhandled>,
    ISquareRootableScalarQuantity<Unhandled>,
    ICubeRootableScalarQuantity<Unhandled>,
    IAddableScalarQuantity<Unhandled, Unhandled>,
    ISubtractableScalarQuantity<Unhandled, Unhandled>,
    IMultiplicableScalarQuantity<Unhandled, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>
{
    /// <summary>A zero-magnitude <see cref="Unhandled"/> quantity.</summary>
    public static Unhandled Zero { get; } = new(0);
    /// <summary>An <see cref="Unhandled"/> quantity of magnitude 1.</summary>
    public static Unhandled One { get; } = new(1);

    /// <summary>The magnitude of the <see cref="Unhandled"/> quantity.</summary>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Unhandled"/> quantity with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Unhandled"/> quantity.</param>
    public Unhandled(Scalar magnitude) : this(magnitude.Magnitude) { }

    /// <summary>Constructs a new <see cref="Unhandled"/> quantity with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Unhandled"/> quantity.</param>
    public Unhandled(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Indicates whether the magnitude of the <see cref="Unhandled"/> quantity is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Unhandled"/> quantity is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Unhandled"/> quantity is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Unhandled"/> quantity is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Unhandled"/> quantity is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Unhandled"/> quantity is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Unhandled"/> quantity is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Unhandled"/> quantity is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="Unhandled"/> quantity, with magnitude equal to the absolute of the original magnitude.</summary>
    public Unhandled Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="Unhandled"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public Unhandled Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="Unhandled"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public Unhandled Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="Unhandled"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public Unhandled Round() => new(Math.Round(Magnitude));

    /// <summary>Computes the <see cref="Unhandled"/> quantity raised to the power <paramref name="exponent"/>.</summary>
    /// <param name="exponent">The exponent, to which the <see cref="Unhandled"/> quantity is raised.</param>
    public Unhandled Power(double exponent) => new(Math.Pow(Magnitude, exponent));
    /// <summary>Computes the inverse of the <see cref="Unhandled"/> quantity.</summary>
    public Unhandled Invert() => new(1 / Magnitude);
    /// <summary>Computes the square of the <see cref="Unhandled"/> quantity.</summary>
    public Unhandled Square() => new(Magnitude * Magnitude);
    /// <summary>Computes the cube of the <see cref="Unhandled"/> quantity.</summary>
    public Unhandled Cube() => new(Magnitude * Magnitude * Magnitude);
    /// <summary>Computes the square root of the <see cref="Unhandled"/> quantity.</summary>
    public Unhandled SquareRoot() => new(Math.Sqrt(Magnitude));
    /// <summary>Computes the cube root of the <see cref="Unhandled"/> quantity.</summary>
    public Unhandled CubeRoot() => new(Math.Cbrt(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Unhandled other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Unhandled"/> quantity, followed by the symbol '[undef]'.</summary>
    public override string ToString() => $"{Magnitude} [undef]";

    /// <summary>Unary plus, resulting in the unmodified <see cref="Unhandled"/> quantity.</summary>
    public Unhandled Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Unhandled"/> quantity with negated magnitude.</summary>
    public Unhandled Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Unhandled"/> quantity.</param>
    public static Unhandled operator +(Unhandled x) => x.Plus();
    /// <summary>Negation, resulting in an <see cref="Unhandled"/> quantity with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Unhandled"/> quantity.</param>
    public static Unhandled operator -(Unhandled x) => x.Negate();

    /// <summary>Adds <paramref name="term"/> to the <see cref="Unhandled"/> quantity.</summary>
    /// <param name="term">This value is added to the <see cref="Unhandled"/> quantity.</param>
    public Unhandled Add(Unhandled term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts <paramref name="term"/> from the <see cref="Unhandled"/> quantity.</summary>
    /// <param name="term">This value is subtracted from the <see cref="Unhandled"/> quantity.</param>
    public Unhandled Subtract(Unhandled term) => new(Magnitude - term.Magnitude);
    /// <summary>Addition of <paramref name="x"/> and <paramref name="y"/>.</summary>
    /// <param name="x">The first term of the addition.</param>
    /// <param name="y">The second term of the addition.</param>
    public static Unhandled operator +(Unhandled x, Unhandled y) => x.Add(y);
    /// <summary>Subtraction of <paramref name="y"/> from <paramref name="x"/>.</summary>
    /// <param name="x">The original value, from which <paramref name="y"/> is subtracted.</param>
    /// <param name="y">This value is subtracted from <paramref name="x"/>.</param>
    public static Unhandled operator -(Unhandled x, Unhandled y) => x.Subtract(y);

    /// <summary>Multiplies this <see cref="Unhandled"/> quantity by another <see cref="Unhandled"/> quantity <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the original <see cref="Unhandled"/> quantity is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides this <see cref="Unhandled"/> quantity by another <see cref="Unhandled"/> quantity <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor by which the original <see cref="Unhandled"/> quantity is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantities <paramref name="x"/> and <paramref name="y"/>.</summary>
    /// <param name="x">The first factor of the multiplication.</param>
    /// <param name="y">The second factor of the multiplication.</param>
    public static Unhandled operator *(Unhandled x, Unhandled y) => x.Multiply(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantities <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The numerator, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The denominator, which divides <paramref name="x"/>.</param>
    public static Unhandled operator /(Unhandled x, Unhandled y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="Unhandled"/> quantity by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Unhandled Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Unhandled"/> quantity by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Unhandled"/> quantity is scaled.</param>
    public Unhandled Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Unhandled"/> quantity through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Unhandled"/> quantity is divided.</param>
    public Unhandled Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by this value.</param>
    public static Unhandled operator %(Unhandled x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Unhandled"/> quantity <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Unhandled"/> quantity <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity, which is scaled by <paramref name="x"/>.</param>
    public static Unhandled operator *(double x, Unhandled y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Unhandled"/> quantity <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator /(Unhandled x, double y) => x.Divide(y);
    /// <summary>Inverts the <see cref="Unhandled"/> quantity <paramref name="y"/>, and then scales it by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity, which is inverted and then scaled by <paramref name="x"/>.</param>
    public static Unhandled operator /(double x, Unhandled y) => y.Invert().Multiply(x);

    /// <summary>Computes the remainder from division of the <see cref="Unhandled"/> quantity by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Unhandled Remainder(Scalar divisor) => new(Magnitude % divisor.Magnitude);
    /// <summary>Scales the <see cref="Unhandled"/> quantity by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Unhandled"/> quantity is scaled.</param>
    public Unhandled Multiply(Scalar factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Scales the <see cref="Unhandled"/> quantity through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Unhandled"/> quantity is divided.</param>
    public Unhandled Divide(Scalar divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by this value.</param>
    public static Unhandled operator %(Unhandled x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Unhandled"/> quantity <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Unhandled"/> quantity <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator /(Unhandled x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="Unhandled"/> quantity by <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which this <see cref="Unhandled"/> quantity is multiplied.</typeparam>
    /// <param name="factor">The <see cref="Unhandled"/> quantity is multiplied by this quantity.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Unhandled"/> quantity by <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which this <see cref="Unhandled"/> quantity is divided.</typeparam>
    /// <param name="divisor">The <see cref="Unhandled"/> quantity is divided by this quantity.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the quantity <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TFactorScalarQuantity}(TFactorScalarQuantity)"/>.</remarks>
    public static Unhandled operator *(Unhandled x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the quantity <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TFactorScalarQuantity}(TFactorScalarQuantity)"/>.</remarks>
    public static Unhandled operator /(Unhandled x, IScalarQuantity y) => x.Divide(y);

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Unhandled"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Unhandled"/>.</param>
    public static bool operator <(Unhandled x, Unhandled y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Unhandled"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Unhandled"/>.</param>
    public static bool operator >(Unhandled x, Unhandled y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Unhandled"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Unhandled"/>.</param>
    public static bool operator <=(Unhandled x, Unhandled y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Unhandled"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Unhandled"/>.</param>
    public static bool operator >=(Unhandled x, Unhandled y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Unhandled"/> quantity to a <see cref="double"/> with value <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>.</summary>
    public static explicit operator double(Unhandled x) => x.Magnitude;

    /// <summary>Converts the <see cref="Unhandled"/> quantity to a <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Unhandled x) => new(x.Magnitude);

    /// <summary>Constructs the <see cref="Unhandled"/> quantity of magnitude <paramref name="x"/>.</summary>
    public static Unhandled FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Unhandled"/> quantity of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Unhandled(double x) => new(x);

    /// <summary>Constructs the <see cref="Unhandled"/> quantity of magnitude <paramref name="x"/>.</summary>
    public static Unhandled FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="Unhandled"/> quantity of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Unhandled(Scalar x) => new(x);
}