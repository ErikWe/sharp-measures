namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="FrequencyDrift"/>, describing change in <see cref="Frequency"/> over time.
/// The quantity is expressed in <see cref="UnitOfFrequencyDrift"/>, with the SI unit being [Hz / s].
/// <para>
/// New instances of <see cref="FrequencyDrift"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfFrequencyDrift"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="FrequencyDrift"/> a = 3 * <see cref="FrequencyDrift.OneHertzPerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="FrequencyDrift"/> d = <see cref="FrequencyDrift.From(TimeSquared)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="FrequencyDrift"/> can be retrieved in the desired <see cref="UnitOfFrequencyDrift"/> using pre-defined properties,
/// such as <see cref="HertzPerSecond"/>.
/// </para>
/// </summary>
public readonly partial record struct FrequencyDrift :
    IComparable<FrequencyDrift>,
    IScalarQuantity,
    IScalableScalarQuantity<FrequencyDrift>,
    IInvertibleScalarQuantity<TimeSquared>,
    ISquareRootableScalarQuantity<Frequency>,
    IMultiplicableScalarQuantity<FrequencyDrift, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<FrequencyDrift, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="FrequencyDrift"/>.</summary>
    public static FrequencyDrift Zero { get; } = new(0);

    /// <summary>The <see cref="FrequencyDrift"/> with magnitude 1, when expressed in unit <see cref="UnitOfFrequencyDrift.HertzPerSecond"/>.</summary>
    public static FrequencyDrift OneHertzPerSecond { get; } = new(1, UnitOfFrequencyDrift.HertzPerSecond);

    /// <summary>Computes <see cref="FrequencyDrift"/> according to { <see cref="FrequencyDrift"/> = 1 / <paramref name="timeSquared"/> }.</summary>
    /// <summary>Constructs a <see cref="FrequencyDrift"/> by inverting the <see cref="TimeSquared"/> <paramref name="timeSquared"/>.</summary>
    public static FrequencyDrift From(TimeSquared timeSquared) => new(1 / timeSquared.Magnitude);
    /// <summary>Computes <see cref="FrequencyDrift"/> according to { <see cref="FrequencyDrift"/> = <paramref name="frequency"/>² }.</summary>
    /// <param name="frequency">This <see cref="Frequency"/> is squared to produce a <see cref="FrequencyDrift"/>.</param>
    public static FrequencyDrift From(Frequency frequency) => new(Math.Pow(frequency.Magnitude, 2));

    /// <summary>The magnitude of the <see cref="FrequencyDrift"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfFrequencyDrift)"/> or a pre-defined property
    /// - such as <see cref="HertzPerSecond"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="FrequencyDrift"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfFrequencyDrift"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="FrequencyDrift"/>, expressed in <paramref name="unitOfFrequencyDrift"/>.</param>
    /// <param name="unitOfFrequencyDrift">The <see cref="UnitOfFrequencyDrift"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="FrequencyDrift"/> a = 3 * <see cref="FrequencyDrift.OneHertzPerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public FrequencyDrift(Scalar magnitude, UnitOfFrequencyDrift unitOfFrequencyDrift) : this(magnitude.Magnitude, unitOfFrequencyDrift) { }
    /// <summary>Constructs a new <see cref="FrequencyDrift"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfFrequencyDrift"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="FrequencyDrift"/>, expressed in <paramref name="unitOfFrequencyDrift"/>.</param>
    /// <param name="unitOfFrequencyDrift">The <see cref="UnitOfFrequencyDrift"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="FrequencyDrift"/> a = 3 * <see cref="FrequencyDrift.OneHertzPerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public FrequencyDrift(double magnitude, UnitOfFrequencyDrift unitOfFrequencyDrift) : this(magnitude * unitOfFrequencyDrift.Factor) { }
    /// <summary>Constructs a new <see cref="FrequencyDrift"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="FrequencyDrift"/>.</param>
    /// <remarks>Consider preferring <see cref="FrequencyDrift(Scalar, UnitOfFrequencyDrift)"/>.</remarks>
    public FrequencyDrift(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="FrequencyDrift"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="FrequencyDrift"/>.</param>
    /// <remarks>Consider preferring <see cref="FrequencyDrift(double, UnitOfFrequencyDrift)"/>.</remarks>
    public FrequencyDrift(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="FrequencyDrift"/>, expressed in <see cref="UnitOfFrequencyDrift.HertzPerSecond"/>.</summary>
    public Scalar HertzPerSecond => InUnit(UnitOfFrequencyDrift.HertzPerSecond);

    /// <summary>Indicates whether the magnitude of the <see cref="FrequencyDrift"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="FrequencyDrift"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="FrequencyDrift"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="FrequencyDrift"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="FrequencyDrift"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="FrequencyDrift"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="FrequencyDrift"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="FrequencyDrift"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="FrequencyDrift"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public FrequencyDrift Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="FrequencyDrift"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public FrequencyDrift Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="FrequencyDrift"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public FrequencyDrift Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="FrequencyDrift"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public FrequencyDrift Round() => new(Math.Round(Magnitude));

    /// <summary>Inverts the <see cref="FrequencyDrift"/>, producing a <see cref="TimeSquared"/>.</summary>
    public TimeSquared Invert() => TimeSquared.From(this);
    /// <summary>Takes the square root of the <see cref="FrequencyDrift"/>, producing a <see cref="Frequency"/>.</summary>
    public Frequency SquareRoot() => Frequency.From(this);

    /// <inheritdoc/>
    public int CompareTo(FrequencyDrift other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="FrequencyDrift"/> (in SI units), and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [Hz / s]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="FrequencyDrift"/>,
    /// expressed in <paramref name="unitOfFrequencyDrift"/>.</summary>
    /// <param name="unitOfFrequencyDrift">The <see cref="UnitOfFrequencyDrift"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfFrequencyDrift unitOfFrequencyDrift) => InUnit(this, unitOfFrequencyDrift);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="FrequencyDrift"/>,
    /// expressed in <paramref name="unitOfFrequencyDrift"/>.</summary>
    /// <param name="frequencyDrift">The <see cref="FrequencyDrift"/> to be expressed in <paramref name="unitOfFrequencyDrift"/>.</param>
    /// <param name="unitOfFrequencyDrift">The <see cref="UnitOfFrequencyDrift"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(FrequencyDrift frequencyDrift, UnitOfFrequencyDrift unitOfFrequencyDrift) => new(frequencyDrift.Magnitude / unitOfFrequencyDrift.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="FrequencyDrift"/>.</summary>
    public FrequencyDrift Plus() => this;
    /// <summary>Negation, resulting in a <see cref="FrequencyDrift"/> with negated magnitude.</summary>
    public FrequencyDrift Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="FrequencyDrift"/>.</param>
    public static FrequencyDrift operator +(FrequencyDrift x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="FrequencyDrift"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="FrequencyDrift"/>.</param>
    public static FrequencyDrift operator -(FrequencyDrift x) => x.Negate();

    /// <summary>Multiplies the <see cref="FrequencyDrift"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="FrequencyDrift"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="FrequencyDrift"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="FrequencyDrift"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="FrequencyDrift"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="FrequencyDrift"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="FrequencyDrift"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(FrequencyDrift x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="FrequencyDrift"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="FrequencyDrift"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="FrequencyDrift"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, FrequencyDrift y) => y.Multiply(x);
    /// <summary>Divides the <see cref="FrequencyDrift"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="FrequencyDrift"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="FrequencyDrift"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(FrequencyDrift x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="FrequencyDrift"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public FrequencyDrift Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="FrequencyDrift"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="FrequencyDrift"/> is scaled.</param>
    public FrequencyDrift Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="FrequencyDrift"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="FrequencyDrift"/> is divided.</param>
    public FrequencyDrift Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="FrequencyDrift"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="FrequencyDrift"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="FrequencyDrift"/> <paramref name="x"/> by this value.</param>
    public static FrequencyDrift operator %(FrequencyDrift x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="FrequencyDrift"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="FrequencyDrift"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="FrequencyDrift"/> <paramref name="x"/>.</param>
    public static FrequencyDrift operator *(FrequencyDrift x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="FrequencyDrift"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="FrequencyDrift"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="FrequencyDrift"/>, which is scaled by <paramref name="x"/>.</param>
    public static FrequencyDrift operator *(double x, FrequencyDrift y) => y.Multiply(x);
    /// <summary>Scales the <see cref="FrequencyDrift"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="FrequencyDrift"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="FrequencyDrift"/> <paramref name="x"/>.</param>
    public static FrequencyDrift operator /(FrequencyDrift x, double y) => x.Divide(y);
/// <summary>Inverts the <see cref="FrequencyDrift"/> <paramref name="y"/> to produce a <see cref="TimeSquared"/>, which is then scaled by <paramref name="x"/>.</summary>
/// <param name="x">This value is used to scale the inverted <see cref="FrequencyDrift"/> <paramref name="y"/>.</param>
/// <param name="y">The <see cref="FrequencyDrift"/>, which is inverted to a <see cref="TimeSquared"/> and scaled by <paramref name="x"/>.</param>
    public static TimeSquared operator /(double x, FrequencyDrift y) => x * y.Invert();

    /// <summary>Produces a <see cref="FrequencyDrift"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public FrequencyDrift Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="FrequencyDrift"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="FrequencyDrift"/> is scaled.</param>
    public FrequencyDrift Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="FrequencyDrift"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="FrequencyDrift"/> is divided.</param>
    public FrequencyDrift Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="FrequencyDrift"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="FrequencyDrift"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="FrequencyDrift"/> <paramref name="x"/> by this value.</param>
    public static FrequencyDrift operator %(FrequencyDrift x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="FrequencyDrift"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="FrequencyDrift"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="FrequencyDrift"/> <paramref name="x"/>.</param>
    public static FrequencyDrift operator *(FrequencyDrift x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="FrequencyDrift"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="FrequencyDrift"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="FrequencyDrift"/>, which is scaled by <paramref name="x"/>.</param>
    public static FrequencyDrift operator *(Scalar x, FrequencyDrift y) => y.Multiply(x);
    /// <summary>Scales the <see cref="FrequencyDrift"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="FrequencyDrift"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="FrequencyDrift"/> <paramref name="x"/>.</param>
    public static FrequencyDrift operator /(FrequencyDrift x, Scalar y) => x.Divide(y);
/// <summary>Inverts the <see cref="FrequencyDrift"/> <paramref name="y"/> to produce a <see cref="TimeSquared"/>, which is then scaled by <paramref name="x"/>.</summary>
/// <param name="x">This value is used to scale the inverted <see cref="FrequencyDrift"/> <paramref name="y"/>.</param>
/// <param name="y">The <see cref="FrequencyDrift"/>, which is inverted to a <see cref="TimeSquared"/> and scaled by <paramref name="x"/>.</param>
    public static TimeSquared operator /(Scalar x, FrequencyDrift y) => x * y.Invert();

    /// <inheritdoc/>
    public TProductScalarQuantity Multiply<TProductScalarQuantity, TFactorScalarQuantity>(TFactorScalarQuantity factor, Func<double, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorScalarQuantity : IScalarQuantity
        => factory(Magnitude * factor.Magnitude);
    /// <inheritdoc/>
    public TQuotientScalarQuantity Divide<TQuotientScalarQuantity, TDivisorScalarQuantity>(TDivisorScalarQuantity divisor, Func<double, TQuotientScalarQuantity> factory)
        where TQuotientScalarQuantity : IScalarQuantity
        where TDivisorScalarQuantity : IScalarQuantity
        => factory(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="FrequencyDrift"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="FrequencyDrift"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="FrequencyDrift"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity, Func{double, TProductScalarQuantity})"/>.</remarks>
    public static Unhandled operator *(FrequencyDrift x, IScalarQuantity y) => x.Multiply<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));
    /// <summary>Divides the <see cref="FrequencyDrift"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="FrequencyDrift"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="FrequencyDrift"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity, Func{double, TQuotientScalarQuantity})"/>.</remarks>
    public static Unhandled operator /(FrequencyDrift x, IScalarQuantity y) => x.Divide<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(FrequencyDrift x, FrequencyDrift y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(FrequencyDrift x, FrequencyDrift y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(FrequencyDrift x, FrequencyDrift y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(FrequencyDrift x, FrequencyDrift y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="FrequencyDrift"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static implicit operator double(FrequencyDrift x) => x.ToDouble();

    /// <summary>Converts the <see cref="FrequencyDrift"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(FrequencyDrift x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="FrequencyDrift"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static FrequencyDrift FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="FrequencyDrift"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator FrequencyDrift(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="FrequencyDrift"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static FrequencyDrift FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="FrequencyDrift"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator FrequencyDrift(Scalar x) => FromScalar(x);
}
