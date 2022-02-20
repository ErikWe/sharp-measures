#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Time"/>.
/// The quantity is expressed in <see cref="UnitOfTime"/>, with the SI unit being [s].
/// <para>
/// New instances of <see cref="Time"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfTime"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Time"/> a = 3 * <see cref="Time.OneSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Time"/> d = <see cref="Time.From(Distance, Speed)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="Time"/> can be retrieved in the desired <see cref="UnitOfTime"/> using pre-defined properties,
/// such as <see cref="Seconds"/>.
/// </para>
/// </summary>
public readonly partial record struct Time :
    IComparable<Time>,
    IScalarQuantity,
    IScalableScalarQuantity<Time>,
    IInvertibleScalarQuantity<Frequency>,
    ISquarableScalarQuantity<TimeSquared>,
    IMultiplicableScalarQuantity<Time, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Time, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="Time"/>.</summary>
    public static Time Zero { get; } = new(0);

    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Second"/>.</summary>
    public static Time OneSecond { get; } = UnitOfTime.Second.Time;
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Minute"/>.</summary>
    public static Time OneMinute { get; } = UnitOfTime.Minute.Time;
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Hour"/>.</summary>
    public static Time OneHour { get; } = UnitOfTime.Hour.Time;
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Day"/>.</summary>
    public static Time OneDay { get; } = UnitOfTime.Day.Time;
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Week"/>.</summary>
    public static Time OneWeek { get; } = UnitOfTime.Week.Time;
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.CommonYear"/>.</summary>
    public static Time OneCommonYear { get; } = UnitOfTime.CommonYear.Time;
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.JulianYear"/>.</summary>
    public static Time OneJulianYear { get; } = UnitOfTime.JulianYear.Time;
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Femtosecond"/>.</summary>
    public static Time OneFemtosecond { get; } = UnitOfTime.Femtosecond.Time;
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Picosecond"/>.</summary>
    public static Time OnePicosecond { get; } = UnitOfTime.Picosecond.Time;
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Nanosecond"/>.</summary>
    public static Time OneNanosecond { get; } = UnitOfTime.Nanosecond.Time;
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Microsecond"/>.</summary>
    public static Time OneMicrosecond { get; } = UnitOfTime.Microsecond.Time;
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Millisecond"/>.</summary>
    public static Time OneMillisecond { get; } = UnitOfTime.Millisecond.Time;

    /// <summary>Computes <see cref="Time"/> according to { 1 / <paramref name="frequency"/> }.</summary>
    /// <summary>Constructs a <see cref="Time"/> by inverting the <see cref="Frequency"/> <paramref name="frequency"/>.</summary>
    public static Time From(Frequency frequency) => new(1 / frequency.Magnitude);
    /// <summary>Computes <see cref="Time"/> according to { √<paramref name="timeSquared"/> }.</summary>
    /// <param name="timeSquared">The square root of this <see cref="TimeSquared"/> is taken to produce a <see cref="Time"/>.</param>
    public static Time From(TimeSquared timeSquared) => new(Math.Sqrt(timeSquared.Magnitude));

    /// <summary>The magnitude of the <see cref="Time"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfTime)"/> or a pre-defined property
    /// - such as <see cref="Seconds"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Time"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfTime"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Time"/>, expressed in <paramref name="unitOfTime"/>.</param>
    /// <param name="unitOfTime">The <see cref="UnitOfTime"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Time"/> a = 3 * <see cref="Time.OneSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Time(Scalar magnitude, UnitOfTime unitOfTime) : this(magnitude.Magnitude, unitOfTime) { }
    /// <summary>Constructs a new <see cref="Time"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfTime"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Time"/>, expressed in <paramref name="unitOfTime"/>.</param>
    /// <param name="unitOfTime">The <see cref="UnitOfTime"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Time"/> a = 3 * <see cref="Time.OneSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Time(double magnitude, UnitOfTime unitOfTime) : this(magnitude * unitOfTime.Time.Magnitude) { }
    /// <summary>Constructs a new <see cref="Time"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Time"/>.</param>
    /// <remarks>Consider preferring <see cref="Time(Scalar, UnitOfTime)"/>.</remarks>
    public Time(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Time"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Time"/>.</param>
    /// <remarks>Consider preferring <see cref="Time(double, UnitOfTime)"/>.</remarks>
    public Time(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in <see cref="UnitOfTime.Second"/>.</summary>
    public Scalar Seconds => InUnit(UnitOfTime.Second);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in <see cref="UnitOfTime.Minute"/>.</summary>
    public Scalar Minutes => InUnit(UnitOfTime.Minute);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in <see cref="UnitOfTime.Hour"/>.</summary>
    public Scalar Hours => InUnit(UnitOfTime.Hour);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in <see cref="UnitOfTime.Day"/>.</summary>
    public Scalar Days => InUnit(UnitOfTime.Day);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in <see cref="UnitOfTime.Week"/>.</summary>
    public Scalar Weeks => InUnit(UnitOfTime.Week);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in <see cref="UnitOfTime.CommonYear"/>.</summary>
    public Scalar CommonYears => InUnit(UnitOfTime.CommonYear);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in <see cref="UnitOfTime.JulianYear"/>.</summary>
    public Scalar JulianYears => InUnit(UnitOfTime.JulianYear);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in <see cref="UnitOfTime.Femtosecond"/>.</summary>
    public Scalar Femtoseconds => InUnit(UnitOfTime.Femtosecond);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in <see cref="UnitOfTime.Picosecond"/>.</summary>
    public Scalar Picoseconds => InUnit(UnitOfTime.Picosecond);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in <see cref="UnitOfTime.Nanosecond"/>.</summary>
    public Scalar Nanoseconds => InUnit(UnitOfTime.Nanosecond);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in <see cref="UnitOfTime.Microsecond"/>.</summary>
    public Scalar Microseconds => InUnit(UnitOfTime.Microsecond);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in <see cref="UnitOfTime.Millisecond"/>.</summary>
    public Scalar Milliseconds => InUnit(UnitOfTime.Millisecond);

    /// <summary>Indicates whether the magnitude of the <see cref="Time"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Time"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Time"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Time"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Time"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Time"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Time"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Time"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="Time"/>.</summary>
    public Time Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="Time"/>.</summary>
    public Time Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="Time"/>.</summary>
    public Time Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="Time"/> to the nearest integer value.</summary>
    public Time Round() => new(Math.Round(Magnitude));

    /// <summary>Computes the inverse of the <see cref="Time"/>, producing a <see cref="Frequency"/>.</summary>
    public Frequency Invert() => Frequency.From(this);
    /// <summary>Computes the square of the <see cref="Time"/>, producing a <see cref="TimeSquared"/>.</summary>
    public TimeSquared Square() => TimeSquared.From(this);

    /// <inheritdoc/>
    public int CompareTo(Time other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Time"/> in the default unit
    /// <see cref="UnitOfTime.Second"/>, followed by the symbol [s].</summary>
    public override string ToString() => $"{Seconds} [s]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Time"/>,
    /// expressed in <paramref name="unitOfTime"/>.</summary>
    /// <param name="unitOfTime">The <see cref="UnitOfTime"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfTime unitOfTime) => InUnit(this, unitOfTime);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Time"/>,
    /// expressed in <paramref name="unitOfTime"/>.</summary>
    /// <param name="time">The <see cref="Time"/> to be expressed in <paramref name="unitOfTime"/>.</param>
    /// <param name="unitOfTime">The <see cref="UnitOfTime"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Time time, UnitOfTime unitOfTime) => new(time.Magnitude / unitOfTime.Time.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Time"/>.</summary>
    public Time Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Time"/> with negated magnitude.</summary>
    public Time Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Time"/>.</param>
    public static Time operator +(Time x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Time"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Time"/>.</param>
    public static Time operator -(Time x) => x.Negate();

    /// <summary>Multiplicates the <see cref="Time"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Time"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Time"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Time"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Time"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Time"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Time"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Time x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Time"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Time"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Time"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Time y) => y.Multiply(x);
    /// <summary>Division of the <see cref="Time"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Time"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Time"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Time x, Unhandled y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="Time"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Time Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Time"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Time"/> is scaled.</param>
    public Time Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Time"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Time"/> is divided.</param>
    public Time Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Time"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Time"/> <paramref name="x"/> by this value.</param>
    public static Time operator %(Time x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Time"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Time"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Time"/> <paramref name="x"/>.</param>
    public static Time operator *(Time x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Time"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Time"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Time"/>, which is scaled by <paramref name="x"/>.</param>
    public static Time operator *(double x, Time y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Time"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Time"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Time"/> <paramref name="x"/>.</param>
    public static Time operator /(Time x, double y) => x.Divide(y);
    /// <summary>Inverts the <see cref="Time"/> <paramref name="y"/> to produce a <see cref="Frequency"/>, which is then scaled by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="Time"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Time"/>, which is inverted to a <see cref="Frequency"/> and scaled by <paramref name="x"/>.</param>
    public static Frequency operator /(double x, Time y) => new(x / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="Time"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Time Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Time"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Time"/> is scaled.</param>
    public Time Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Time"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Time"/> is divided.</param>
    public Time Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Time"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Time"/> <paramref name="x"/> by this value.</param>
    public static Time operator %(Time x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Time"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Time"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Time"/> <paramref name="x"/>.</param>
    public static Time operator *(Time x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Time"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Time"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Time"/>, which is scaled by <paramref name="x"/>.</param>
    public static Time operator *(Scalar x, Time y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Time"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Time"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Time"/> <paramref name="x"/>.</param>
    public static Time operator /(Time x, Scalar y) => x.Divide(y);
    /// <summary>Inverts the <see cref="Time"/> <paramref name="y"/> to produce a <see cref="Frequency"/>,
    /// which is then scaled by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="Time"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Time"/>, which is inverted to a <see cref="Frequency"/> and scaled by <paramref name="x"/>.</param>
    public static Frequency operator /(Scalar x, Time y) => new(x / y.Magnitude);

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

    /// <summary>Multiplication of the <see cref="Time"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Time"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Time"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Time x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="Time"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Time"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Time"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Time x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Time"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Time"/>.</param>
    public static bool operator <(Time x, Time y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Time"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Time"/>.</param>
    public static bool operator >(Time x, Time y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Time"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Time"/>.</param>
    public static bool operator <=(Time x, Time y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Time"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Time"/>.</param>
    public static bool operator >=(Time x, Time y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Time"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(Time x) => x.ToDouble();

    /// <summary>Converts the <see cref="Time"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Time x) => x.ToScalar();

    /// <summary>Constructs the <see cref="Time"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Time FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Time"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Time(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="Time"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static Time FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="Time"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator Time(Scalar x) => FromScalar(x);
}
