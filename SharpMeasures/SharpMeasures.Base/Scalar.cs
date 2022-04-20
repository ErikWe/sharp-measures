namespace SharpMeasures;

using SharpMeasures.ScalarAbstractions;

using System;

/// <summary>A pure scalar.</summary>
public readonly partial record struct Scalar :
    IComparable<Scalar>,
    IScalarQuantity,
    IScalableScalarQuantity<Scalar>,
    IInvertibleScalarQuantity<Scalar>,
    ISquarableScalarQuantity<Scalar>,
    ICubableScalarQuantity<Scalar>,
    ISquareRootableScalarQuantity<Scalar>,
    ICubeRootableScalarQuantity<Scalar>,
    IAddableScalarQuantity<Scalar, Scalar>,
    ISubtractableScalarQuantity<Scalar, Scalar>,
    IMultiplicableScalarQuantity<Scalar, Scalar>,
    IDivisibleScalarQuantity<Scalar, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-magnitude <see cref="Scalar"/>.</summary>
    public static Scalar Zero { get; } = new(0);
    /// <summary>The <see cref="Scalar"/> with magnitude 1.</summary>
    public static Scalar One { get; } = new(1);

    /// <summary>The value of the <see cref="Scalar"/>.</summary>
    public double Value { get; init; }

    /// <inheritdoc/>
    Scalar IScalarQuantity.Magnitude => this;

    /// <summary>Constructs a new <see cref="Scalar"/> with <paramref name="value"/>.</summary>
    /// <param name="value">The value of the <see cref="Scalar"/>.</param>
    public Scalar(double value)
    {
        Value = value;
    }

    /// <summary>Indicates whether the value of the <see cref="Scalar"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Value);
    /// <summary>Indicates whether the value of the <see cref="Scalar"/> is zero.</summary>
    public bool IsZero => Value == 0;
    /// <summary>Indicates whether the value of the <see cref="Scalar"/> is positive.</summary>
    public bool IsPositive => Value > 0;
    /// <summary>Indicates whether the value of the <see cref="Scalar"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Value);
    /// <summary>Indicates whether the value of the <see cref="Scalar"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Value);
    /// <summary>Indicates whether the value of the <see cref="Scalar"/> is infinite.</summary>
    public bool IsInfinite => double.IsInfinity(Value);
    /// <summary>Indicates whether the value of the <see cref="Scalar"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Value);
    /// <summary>Indicates whether the value of the <see cref="Scalar"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Value);

    /// <summary>Computes the absolute of the <see cref="Scalar"/>.</summary>
    public Scalar Absolute() => new(Math.Abs(Value));
    /// <summary>Computes the floor of the <see cref="Scalar"/>.</summary>
    public Scalar Floor() => new(Math.Floor(Value));
    /// <summary>Computes the ceiling of the <see cref="Scalar"/>.</summary>
    public Scalar Ceiling() => new(Math.Ceiling(Value));
    /// <summary>Rounds the <see cref="Scalar"/> to the nearest integer value.</summary>
    public Scalar Round() => new(Math.Round(Value));

    /// <summary>Computes the <see cref="Scalar"/> raised to the power <paramref name="exponent"/>.</summary>
    /// <param name="exponent">The exponent, to which the <see cref="Scalar"/> is raised.</param>
    public Scalar Power(double exponent) => new(Math.Pow(Value, exponent));
    /// <summary>Computes the inverse of the <see cref="Scalar"/>.</summary>
    public Scalar Invert() => new(1 / Value);
    /// <summary>Computes the square of the <see cref="Scalar"/>.</summary>
    public Scalar Square() => new(Value * Value);
    /// <summary>Computes the cube of the <see cref="Scalar"/>.</summary>
    public Scalar Cube() => new(Value * Value * Value);
    /// <summary>Computes the square root of the <see cref="Scalar"/>.</summary>
    public Scalar SquareRoot() => new(Math.Sqrt(Value));
    /// <summary>Computes the cube root of the <see cref="Scalar"/>.</summary>
    public Scalar CubeRoot() => new(Math.Cbrt(Value));

    /// <inheritdoc/>
    public int CompareTo(Scalar other) => Value.CompareTo(other.Value);
    /// <summary>Produces a formatted string from the value of the <see cref="Scalar"/>.</summary>
    public override string ToString() => $"{Value}";

    /// <summary>Unary plus, resulting in the unmodified <see cref="Scalar"/>.</summary>
    public Scalar Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Scalar"/> with negated magnitude.</summary>
    public Scalar Negate() => new(-Value);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Scalar"/>.</param>
    public static Scalar operator +(Scalar x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Scalar"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Scalar"/>.</param>
    public static Scalar operator -(Scalar x) => x.Negate();

    /// <summary>Adds <paramref name="term"/> to the <see cref="Scalar"/>.</summary>
    /// <param name="term">This value is added to the <see cref="Scalar"/>.</param>
    public Scalar Add(double term) => new(Value + term);
    /// <summary>Subtracts <paramref name="term"/> from the <see cref="Scalar"/>.</summary>
    /// <param name="term">This value is subtracted from the <see cref="Scalar"/>.</param>
    public Scalar Subtract(double term) => new(Value - term);
    /// <summary>Addition of <paramref name="x"/> and <paramref name="y"/>.</summary>
    /// <param name="x">The first term of the addition.</param>
    /// <param name="y">The second term of the addition.</param>
    public static Scalar operator +(Scalar x, double y) => x.Add(y);
    /// <summary>Addition <paramref name="x"/> and <paramref name="y"/>.</summary>
    /// <param name="x">The first term of the addition.</param>
    /// <param name="y">The second term of the addition.</param>
    public static Scalar operator +(double x, Scalar y) => y.Add(x);
    /// <summary>Subtraction of <paramref name="y"/> from <paramref name="x"/>.</summary>
    /// <param name="x">The original value, from which <paramref name="y"/> is subtracted.</param>
    /// <param name="y">This value is subtracted from <paramref name="x"/>.</param>
    public static Scalar operator -(Scalar x, double y) => x.Subtract(y);
    /// <summary>Subtraction of <paramref name="y"/> from <paramref name="x"/>.</summary>
    /// <param name="x">The original value, from which <paramref name="y"/> is subtracted.</param>
    /// <param name="y">This value is subtracted from <paramref name="x"/>.</param>
    public static Scalar operator -(double x, Scalar y) => y.Negate().Add(x);

    /// <summary>Adds <paramref name="term"/> to the <see cref="Scalar"/>.</summary>
    /// <param name="term">This value is added to the <see cref="Scalar"/>.</param>
    public Scalar Add(Scalar term) => new(Value + term.Value);
    /// <summary>Subtracts <paramref name="term"/> from the <see cref="Scalar"/>.</summary>
    /// <param name="term">This value is subtracted from the <see cref="Scalar"/>.</param>
    public Scalar Subtract(Scalar term) => new(Value - term.Value);
    /// <summary>Addition of <paramref name="x"/> and <paramref name="y"/>.</summary>
    /// <param name="x">The first term of the addition.</param>
    /// <param name="y">The second term of the addition.</param>
    public static Scalar operator +(Scalar x, Scalar y) => x.Add(y);
    /// <summary>Subtraction of <paramref name="y"/> from <paramref name="x"/>.</summary>
    /// <param name="x">The original value, from which <paramref name="y"/> is subtracted.</param>
    /// <param name="y">This value is subtracted from <paramref name="x"/>.</param>
    public static Scalar operator -(Scalar x, Scalar y) => x.Subtract(y);

    /// <summary>Multiplies the <see cref="Scalar"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Scalar"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Value * factor.Magnitude);
    /// <summary>Divides the <see cref="Scalar"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Scalar"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Value / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Scalar"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Scalar"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Scalar"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Scalar x, Unhandled y) => x.Multiply(y);
    /// <summary>Division of the the <see cref="Scalar"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Scalar"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Scalar"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Scalar x, Unhandled y) => x.Divide(y);

    /// <summary>Computes the remainder from division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Scalar Remainder(double divisor) => new(Value % divisor);
    /// <summary>Multiplies the <see cref="Scalar"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Scalar"/> is multiplied.</param>
    public Scalar Multiply(double factor) => new(Value * factor);
    /// <summary>Divides the <see cref="Scalar"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Scalar"/> is divided.</param>
    public Scalar Divide(double divisor) => new(Value / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">This value is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of <paramref name="x"/> by this value.</param>
    public static Scalar operator %(Scalar x, double y) => x.Remainder(y);
    /// <summary>Multiplication of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The first factor of the multiplication.</param>
    /// <param name="y">The second factor of the multiplication.</param>
    public static Scalar operator *(Scalar x, double y) => x.Multiply(y);
    /// <summary>Multiplication of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The first factor of the multiplication.</param>
    /// <param name="y">The second factor of the multiplication.</param>
    public static Scalar operator *(double x, Scalar y) => y.Multiply(x);
    /// <summary>Division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The numerator, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The denominator, which divides <paramref name="x"/>.</param>
    public static Scalar operator /(Scalar x, double y) => x.Divide(y);
    /// <summary>Division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The numerator, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The denominator, which divides <paramref name="x"/>.</param>
    public static Scalar operator /(double x, Scalar y) => new(x / y.Value);

    /// <summary>Computes the remainder from division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Scalar Remainder(Scalar divisor) => new(Value % divisor.Value);
    /// <summary>Multiplies the <see cref="Scalar"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Scalar"/> is multiplied.</param>
    public Scalar Multiply(Scalar factor) => new(Value * factor.Value);
    /// <summary>Divides the <see cref="Scalar"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Scalar"/> is divided.</param>
    public Scalar Divide(Scalar divisor) => new(Value / divisor.Value);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">This value is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of <paramref name="x"/> by this value.</param>
    public static Scalar operator %(Scalar x, Scalar y) => x.Remainder(y);
    /// <summary>Multiplication of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The first factor of the multiplication.</param>
    /// <param name="y">The second factor of the multiplication.</param>
    public static Scalar operator *(Scalar x, Scalar y) => x.Multiply(y);
    /// <summary>Division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The numerator, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The denominator, which divides <paramref name="x"/>.</param>
    public static Scalar operator /(Scalar x, Scalar y) => x.Divide(y);

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"></exception>
    public TProductScalarQuantity Multiply<TProductScalarQuantity, TFactorScalarQuantity>(TFactorScalarQuantity factor, Func<Scalar, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorScalarQuantity : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        ArgumentNullException.ThrowIfNull(factor, nameof(factor));

        return factory(Value * factor.Magnitude);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"></exception>
    public TQuotientScalarQuantity Divide<TQuotientScalarQuantity, TDivisorScalarQuantity>(TDivisorScalarQuantity divisor, Func<Scalar, TQuotientScalarQuantity> factory)
        where TQuotientScalarQuantity : IScalarQuantity
        where TDivisorScalarQuantity : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        ArgumentNullException.ThrowIfNull(divisor, nameof(divisor));

        return factory(Value / divisor.Magnitude);
    }

    /// <summary>Multiplication of <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Scalar"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Scalar"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity, Func{Scalar, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Scalar x, IScalarQuantity y) => x.Multiply(y, static (product) => new Unhandled(product));
    /// <summary>Division of <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Scalar"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Scalar"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity, Func{Scalar, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Scalar x, IScalarQuantity y) => x.Divide(y, static (quotient) => new Unhandled(quotient));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Scalar"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Scalar"/>.</param>
    public static bool operator <(Scalar x, Scalar y) => x.Value < y.Value;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Scalar"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Scalar"/>.</param>
    public static bool operator >(Scalar x, Scalar y) => x.Value > y.Value;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Scalar"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Scalar"/>.</param>
    public static bool operator <=(Scalar x, Scalar y) => x.Value <= y.Value;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Scalar"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Scalar"/>.</param>
    public static bool operator >=(Scalar x, Scalar y) => x.Value >= y.Value;

    /// <summary>Converts the <see cref="Scalar"/> to a <see cref="double"/> with value <see cref="Value"/>.</summary>
    public double ToDouble() => Value;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Value"/>.</summary>
    public static implicit operator double(Scalar x) => x.Value;

    /// <summary>Constructs the <see cref="Scalar"/> of magnitude <paramref name="x"/>.</summary>
    public static Scalar FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Scalar"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Scalar(double x) => new(x);
}