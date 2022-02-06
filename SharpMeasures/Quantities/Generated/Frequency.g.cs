namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Frequency"/>, describing temporal periodicity.
/// The quantity is expressed in <see cref="UnitOfFrequency"/>, with the SI unit being [Hz].
/// <para>
/// New instances of <see cref="Frequency"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfFrequency"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Frequency"/> a = 3 * <see cref="Frequency.OneHertz"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Frequency"/> d = <see cref="Frequency.From(Time)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="Frequency"/> can be retrieved in the desired <see cref="UnitOfFrequency"/> using pre-defined properties,
/// such as <see cref="Hertz"/>.
/// </para>
/// </summary>
public readonly partial record struct Frequency :
    IComparable<Frequency>,
    IScalarQuantity,
    IScalableScalarQuantity<Frequency>,
    IInvertibleScalarQuantity<Time>,
    ISquarableScalarQuantity<FrequencyDrift>,
    IMultiplicableScalarQuantity<Frequency, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Frequency, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="Frequency"/>.</summary>
    public static Frequency Zero { get; } = new(0);

    /// <summary>The <see cref="Frequency"/> with magnitude 1, when expressed in unit <see cref="UnitOfFrequency.PerSecond"/>.</summary>
    public static Frequency OnePerSecond { get; } = new(1, UnitOfFrequency.PerSecond);
    /// <summary>The <see cref="Frequency"/> with magnitude 1, when expressed in unit <see cref="UnitOfFrequency.Hertz"/>.</summary>
    public static Frequency OneHertz { get; } = new(1, UnitOfFrequency.Hertz);
    /// <summary>The <see cref="Frequency"/> with magnitude 1, when expressed in unit <see cref="UnitOfFrequency.Kilohertz"/>.</summary>
    public static Frequency OneKilohertz { get; } = new(1, UnitOfFrequency.Kilohertz);
    /// <summary>The <see cref="Frequency"/> with magnitude 1, when expressed in unit <see cref="UnitOfFrequency.Megahertz"/>.</summary>
    public static Frequency OneMegahertz { get; } = new(1, UnitOfFrequency.Megahertz);
    /// <summary>The <see cref="Frequency"/> with magnitude 1, when expressed in unit <see cref="UnitOfFrequency.Gigahertz"/>.</summary>
    public static Frequency OneGigahertz { get; } = new(1, UnitOfFrequency.Gigahertz);

    /// <summary>Computes <see cref="Frequency"/> according to { <see cref="Frequency"/> = 1 / <paramref name="time"/> }.</summary>
    /// <summary>Constructs a <see cref="Frequency"/> by inverting the <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Frequency From(Time time) => new(1 / time.Magnitude);
    /// <summary>Computes <see cref="Frequency"/> according to { <see cref="Frequency"/> = √<paramref name="frequencyDrift"/> }.</summary>
    /// <param name="frequencyDrift">The square root of this <see cref="FrequencyDrift"/> is taken to produce a <see cref="Frequency"/>.</param>
    public static Frequency From(FrequencyDrift frequencyDrift) => new(Math.Sqrt(frequencyDrift.Magnitude));

    /// <summary>The magnitude of the <see cref="Frequency"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfFrequency)"/> or a pre-defined property
    /// - such as <see cref="PerSecond"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Frequency"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfFrequency"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Frequency"/>, expressed in <paramref name="unitOfFrequency"/>.</param>
    /// <param name="unitOfFrequency">The <see cref="UnitOfFrequency"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Frequency"/> a = 3 * <see cref="Frequency.OnePerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Frequency(Scalar magnitude, UnitOfFrequency unitOfFrequency) : this(magnitude.Magnitude, unitOfFrequency) { }
    /// <summary>Constructs a new <see cref="Frequency"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfFrequency"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Frequency"/>, expressed in <paramref name="unitOfFrequency"/>.</param>
    /// <param name="unitOfFrequency">The <see cref="UnitOfFrequency"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Frequency"/> a = 3 * <see cref="Frequency.OnePerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Frequency(double magnitude, UnitOfFrequency unitOfFrequency) : this(magnitude * unitOfFrequency.Factor) { }
    /// <summary>Constructs a new <see cref="Frequency"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Frequency"/>.</param>
    /// <remarks>Consider preferring <see cref="Frequency(Scalar, UnitOfFrequency)"/>.</remarks>
    public Frequency(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Frequency"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Frequency"/>.</param>
    /// <remarks>Consider preferring <see cref="Frequency(double, UnitOfFrequency)"/>.</remarks>
    public Frequency(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Frequency"/>, expressed in <see cref="UnitOfFrequency.PerSecond"/>.</summary>
    public Scalar PerSecond => InUnit(UnitOfFrequency.PerSecond);

    /// <summary>Retrieves the magnitude of the <see cref="Frequency"/>, expressed in <see cref="UnitOfFrequency.Hertz"/>.</summary>
    public Scalar Hertz => InUnit(UnitOfFrequency.Hertz);
    /// <summary>Retrieves the magnitude of the <see cref="Frequency"/>, expressed in <see cref="UnitOfFrequency.Kilohertz"/>.</summary>
    public Scalar Kilohertz => InUnit(UnitOfFrequency.Kilohertz);
    /// <summary>Retrieves the magnitude of the <see cref="Frequency"/>, expressed in <see cref="UnitOfFrequency.Megahertz"/>.</summary>
    public Scalar Megahertz => InUnit(UnitOfFrequency.Megahertz);
    /// <summary>Retrieves the magnitude of the <see cref="Frequency"/>, expressed in <see cref="UnitOfFrequency.Gigahertz"/>.</summary>
    public Scalar Gigahertz => InUnit(UnitOfFrequency.Gigahertz);

    /// <summary>Indicates whether the magnitude of the <see cref="Frequency"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Frequency"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Frequency"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Frequency"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Frequency"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Frequency"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Frequency"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Frequency"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="Frequency"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public Frequency Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="Frequency"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public Frequency Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="Frequency"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public Frequency Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="Frequency"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public Frequency Round() => new(Math.Round(Magnitude));

    /// <summary>Inverts the <see cref="Frequency"/>, producing a <see cref="Time"/>.</summary>
    public Time Invert() => Time.From(this);
    /// <summary>Squares the <see cref="Frequency"/>, producing a <see cref="FrequencyDrift"/>.</summary>
    public FrequencyDrift Square() => FrequencyDrift.From(this);

    /// <inheritdoc/>
    public int CompareTo(Frequency other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Frequency"/> (in SI units), and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [Hz]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Frequency"/>,
    /// expressed in <paramref name="unitOfFrequency"/>.</summary>
    /// <param name="unitOfFrequency">The <see cref="UnitOfFrequency"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfFrequency unitOfFrequency) => InUnit(this, unitOfFrequency);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Frequency"/>,
    /// expressed in <paramref name="unitOfFrequency"/>.</summary>
    /// <param name="frequency">The <see cref="Frequency"/> to be expressed in <paramref name="unitOfFrequency"/>.</param>
    /// <param name="unitOfFrequency">The <see cref="UnitOfFrequency"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Frequency frequency, UnitOfFrequency unitOfFrequency) => new(frequency.Magnitude / unitOfFrequency.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Frequency"/>.</summary>
    public Frequency Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Frequency"/> with negated magnitude.</summary>
    public Frequency Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Frequency"/>.</param>
    public static Frequency operator +(Frequency x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Frequency"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Frequency"/>.</param>
    public static Frequency operator -(Frequency x) => x.Negate();

    /// <summary>Multiplies the <see cref="Frequency"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Frequency"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Frequency"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Frequency"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Frequency"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Frequency"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Frequency"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Frequency x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Frequency"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Frequency"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Frequency"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Frequency y) => y.Multiply(x);
    /// <summary>Divides the <see cref="Frequency"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Frequency"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Frequency"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Frequency x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Frequency"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Frequency Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Frequency"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Frequency"/> is scaled.</param>
    public Frequency Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Frequency"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Frequency"/> is divided.</param>
    public Frequency Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Frequency"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Frequency"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="Frequency"/> <paramref name="x"/> by this value.</param>
    public static Frequency operator %(Frequency x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Frequency"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Frequency"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Frequency"/> <paramref name="x"/>.</param>
    public static Frequency operator *(Frequency x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Frequency"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Frequency"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Frequency"/>, which is scaled by <paramref name="x"/>.</param>
    public static Frequency operator *(double x, Frequency y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Frequency"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Frequency"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Frequency"/> <paramref name="x"/>.</param>
    public static Frequency operator /(Frequency x, double y) => x.Divide(y);
/// <summary>Inverts the <see cref="Frequency"/> <paramref name="y"/> to produce a <see cref="Time"/>, which is then scaled by <paramref name="x"/>.</summary>
/// <param name="x">This value is used to scale the inverted <see cref="Frequency"/> <paramref name="y"/>.</param>
/// <param name="y">The <see cref="Frequency"/>, which is inverted to a <see cref="Time"/> and scaled by <paramref name="x"/>.</param>
    public static Time operator /(double x, Frequency y) => x * y.Invert();

    /// <summary>Produces a <see cref="Frequency"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Frequency Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Frequency"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Frequency"/> is scaled.</param>
    public Frequency Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Frequency"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Frequency"/> is divided.</param>
    public Frequency Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Frequency"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Frequency"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="Frequency"/> <paramref name="x"/> by this value.</param>
    public static Frequency operator %(Frequency x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Frequency"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Frequency"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Frequency"/> <paramref name="x"/>.</param>
    public static Frequency operator *(Frequency x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Frequency"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Frequency"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Frequency"/>, which is scaled by <paramref name="x"/>.</param>
    public static Frequency operator *(Scalar x, Frequency y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Frequency"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Frequency"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Frequency"/> <paramref name="x"/>.</param>
    public static Frequency operator /(Frequency x, Scalar y) => x.Divide(y);
/// <summary>Inverts the <see cref="Frequency"/> <paramref name="y"/> to produce a <see cref="Time"/>, which is then scaled by <paramref name="x"/>.</summary>
/// <param name="x">This value is used to scale the inverted <see cref="Frequency"/> <paramref name="y"/>.</param>
/// <param name="y">The <see cref="Frequency"/>, which is inverted to a <see cref="Time"/> and scaled by <paramref name="x"/>.</param>
    public static Time operator /(Scalar x, Frequency y) => x * y.Invert();

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
    /// <summary>Multiples the <see cref="Frequency"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Frequency"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Frequency"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity, Func{double, TProductScalarQuantity})"/>.</remarks>
    public static Unhandled operator *(Frequency x, IScalarQuantity y) => x.Multiply<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));
    /// <summary>Divides the <see cref="Frequency"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Frequency"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Frequency"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity, Func{double, TQuotientScalarQuantity})"/>.</remarks>
    public static Unhandled operator /(Frequency x, IScalarQuantity y) => x.Divide<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Frequency x, Frequency y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Frequency x, Frequency y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Frequency x, Frequency y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Frequency x, Frequency y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Frequency"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static implicit operator double(Frequency x) => x.ToDouble();

    /// <summary>Converts the <see cref="Frequency"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Frequency x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Frequency"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Frequency FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Frequency"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Frequency(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Frequency"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static Frequency FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Frequency"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Frequency(Scalar x) => FromScalar(x);
}
