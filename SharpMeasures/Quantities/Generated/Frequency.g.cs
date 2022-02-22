#nullable enable

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
    public static Frequency OnePerSecond { get; } = UnitOfFrequency.PerSecond.Frequency;
    /// <summary>The <see cref="Frequency"/> with magnitude 1, when expressed in unit <see cref="UnitOfFrequency.PerMinute"/>.</summary>
    public static Frequency OnePerMinute { get; } = UnitOfFrequency.PerMinute.Frequency;
    /// <summary>The <see cref="Frequency"/> with magnitude 1, when expressed in unit <see cref="UnitOfFrequency.PerHour"/>.</summary>
    public static Frequency OnePerHour { get; } = UnitOfFrequency.PerHour.Frequency;
    /// <summary>The <see cref="Frequency"/> with magnitude 1, when expressed in unit <see cref="UnitOfFrequency.Hertz"/>.</summary>
    public static Frequency OneHertz { get; } = UnitOfFrequency.Hertz.Frequency;
    /// <summary>The <see cref="Frequency"/> with magnitude 1, when expressed in unit <see cref="UnitOfFrequency.Kilohertz"/>.</summary>
    public static Frequency OneKilohertz { get; } = UnitOfFrequency.Kilohertz.Frequency;
    /// <summary>The <see cref="Frequency"/> with magnitude 1, when expressed in unit <see cref="UnitOfFrequency.Megahertz"/>.</summary>
    public static Frequency OneMegahertz { get; } = UnitOfFrequency.Megahertz.Frequency;
    /// <summary>The <see cref="Frequency"/> with magnitude 1, when expressed in unit <see cref="UnitOfFrequency.Gigahertz"/>.</summary>
    public static Frequency OneGigahertz { get; } = UnitOfFrequency.Gigahertz.Frequency;
    /// <summary>The <see cref="Frequency"/> with magnitude 1, when expressed in unit <see cref="UnitOfFrequency.Terahertz"/>.</summary>
    public static Frequency OneTerahertz { get; } = UnitOfFrequency.Terahertz.Frequency;

    /// <summary>Computes <see cref="Frequency"/> according to { 1 / <paramref name="time"/> }.</summary>
    /// <summary>Constructs a <see cref="Frequency"/> by inverting the <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Frequency From(Time time) => new(1 / time.Magnitude);
    /// <summary>Computes <see cref="Frequency"/> according to { √<paramref name="frequencyDrift"/> }.</summary>
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
    public Frequency(double magnitude, UnitOfFrequency unitOfFrequency) : this(magnitude * unitOfFrequency.Frequency.Magnitude) { }
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
    /// <summary>Retrieves the magnitude of the <see cref="Frequency"/>, expressed in <see cref="UnitOfFrequency.PerMinute"/>.</summary>
    public Scalar PerMinute => InUnit(UnitOfFrequency.PerMinute);
    /// <summary>Retrieves the magnitude of the <see cref="Frequency"/>, expressed in <see cref="UnitOfFrequency.PerHour"/>.</summary>
    public Scalar PerHour => InUnit(UnitOfFrequency.PerHour);
    /// <summary>Retrieves the magnitude of the <see cref="Frequency"/>, expressed in <see cref="UnitOfFrequency.Hertz"/>.</summary>
    public Scalar Hertz => InUnit(UnitOfFrequency.Hertz);
    /// <summary>Retrieves the magnitude of the <see cref="Frequency"/>, expressed in <see cref="UnitOfFrequency.Kilohertz"/>.</summary>
    public Scalar Kilohertz => InUnit(UnitOfFrequency.Kilohertz);
    /// <summary>Retrieves the magnitude of the <see cref="Frequency"/>, expressed in <see cref="UnitOfFrequency.Megahertz"/>.</summary>
    public Scalar Megahertz => InUnit(UnitOfFrequency.Megahertz);
    /// <summary>Retrieves the magnitude of the <see cref="Frequency"/>, expressed in <see cref="UnitOfFrequency.Gigahertz"/>.</summary>
    public Scalar Gigahertz => InUnit(UnitOfFrequency.Gigahertz);
    /// <summary>Retrieves the magnitude of the <see cref="Frequency"/>, expressed in <see cref="UnitOfFrequency.Terahertz"/>.</summary>
    public Scalar Terahertz => InUnit(UnitOfFrequency.Terahertz);

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

    /// <summary>Computes the absolute of the <see cref="Frequency"/>.</summary>
    public Frequency Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="Frequency"/>.</summary>
    public Frequency Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="Frequency"/>.</summary>
    public Frequency Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="Frequency"/> to the nearest integer value.</summary>
    public Frequency Round() => new(Math.Round(Magnitude));

    /// <summary>Computes the inverse of the <see cref="Frequency"/>, producing a <see cref="Time"/>.</summary>
    public Time Invert() => Time.From(this);
    /// <summary>Computes the square of the <see cref="Frequency"/>, producing a <see cref="FrequencyDrift"/>.</summary>
    public FrequencyDrift Square() => FrequencyDrift.From(this);

    /// <inheritdoc/>
    public int CompareTo(Frequency other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Frequency"/> in the default unit
    /// <see cref="UnitOfFrequency.Hertz"/>, followed by the symbol [Hz].</summary>
    public override string ToString() => $"{Hertz} [Hz]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Frequency"/>,
    /// expressed in <paramref name="unitOfFrequency"/>.</summary>
    /// <param name="unitOfFrequency">The <see cref="UnitOfFrequency"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfFrequency unitOfFrequency) => InUnit(this, unitOfFrequency);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Frequency"/>,
    /// expressed in <paramref name="unitOfFrequency"/>.</summary>
    /// <param name="frequency">The <see cref="Frequency"/> to be expressed in <paramref name="unitOfFrequency"/>.</param>
    /// <param name="unitOfFrequency">The <see cref="UnitOfFrequency"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Frequency frequency, UnitOfFrequency unitOfFrequency) => new(frequency.Magnitude / unitOfFrequency.Frequency.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Frequency"/>.</summary>
    public Frequency Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Frequency"/> with negated magnitude.</summary>
    public Frequency Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Frequency"/>.</param>
    public static Frequency operator +(Frequency x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Frequency"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Frequency"/>.</param>
    public static Frequency operator -(Frequency x) => x.Negate();

    /// <summary>Multiplicates the <see cref="Frequency"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Frequency"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Frequency"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Frequency"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Frequency"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Frequency"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Frequency"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Frequency x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Frequency"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Frequency"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Frequency"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Frequency y) => y.Multiply(x);
    /// <summary>Division of the <see cref="Frequency"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Frequency"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Frequency"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Frequency x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="Frequency"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="Frequency"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Frequency"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, Frequency y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="Frequency"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Frequency Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Frequency"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Frequency"/> is scaled.</param>
    public Frequency Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Frequency"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Frequency"/> is divided.</param>
    public Frequency Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Frequency"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Frequency"/> <paramref name="x"/> by this value.</param>
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
    public static Time operator /(double x, Frequency y) => new(x / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="Frequency"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Frequency Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Frequency"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Frequency"/> is scaled.</param>
    public Frequency Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Frequency"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Frequency"/> is divided.</param>
    public Frequency Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Frequency"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Frequency"/> <paramref name="x"/> by this value.</param>
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
    /// <summary>Inverts the <see cref="Frequency"/> <paramref name="y"/> to produce a <see cref="Time"/>,
    /// which is then scaled by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="Frequency"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Frequency"/>, which is inverted to a <see cref="Time"/> and scaled by <paramref name="x"/>.</param>
    public static Time operator /(Scalar x, Frequency y) => new(x / y.Magnitude);

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TProductScalarQuantity Multiply<TProductScalarQuantity, TFactorScalarQuantity>(TFactorScalarQuantity factor, Func<double, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorScalarQuantity : IScalarQuantity
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }
        else if (factor == null)
        {
            throw new ArgumentNullException(nameof(factor));
        }
        else
        {
            return factory(Magnitude * factor.Magnitude);
        }
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TQuotientScalarQuantity Divide<TQuotientScalarQuantity, TDivisorScalarQuantity>(TDivisorScalarQuantity divisor, Func<double, TQuotientScalarQuantity> factory)
        where TQuotientScalarQuantity : IScalarQuantity
        where TDivisorScalarQuantity : IScalarQuantity
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }
        else if (divisor == null)
        {
            throw new ArgumentNullException(nameof(divisor));
        }
        else
        {
            return factory(Magnitude / divisor.Magnitude);
        }
    }

    /// <summary>Multiplication of the <see cref="Frequency"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Frequency"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Frequency"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Frequency x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="Frequency"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Frequency"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Frequency"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Frequency x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Frequency"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Frequency"/>.</param>
    public static bool operator <(Frequency x, Frequency y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Frequency"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Frequency"/>.</param>
    public static bool operator >(Frequency x, Frequency y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Frequency"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Frequency"/>.</param>
    public static bool operator <=(Frequency x, Frequency y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Frequency"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Frequency"/>.</param>
    public static bool operator >=(Frequency x, Frequency y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Frequency"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(Frequency x) => x.ToDouble();

    /// <summary>Converts the <see cref="Frequency"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Frequency x) => x.ToScalar();

    /// <summary>Constructs the <see cref="Frequency"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Frequency FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Frequency"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Frequency(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="Frequency"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static Frequency FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="Frequency"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator Frequency(Scalar x) => FromScalar(x);
}
