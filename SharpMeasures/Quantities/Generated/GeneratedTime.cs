namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Time"/>, used for describing durations in time.
/// <para>
/// New instances of <see cref="Time"/> can be constructed according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Time"/> a = 5 * <see cref="Time.OneSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Time"/> b = new(7, <see cref="UnitOfTime.Day"/>);
/// </code>
/// </item>
/// </list>
/// Instances of <see cref="Time"/> may be applied according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Velocity"/> c = 3 * <see cref="Length.OneParsec"/> / <see cref="Time.OneWeek"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved in a desired unit according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Time.InWeeks"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Time.InUnit(UnitOfTime)"/>;
/// </code>
/// </item>
/// </list>
/// </para>
/// </summary>
public readonly partial record struct Time :
    IComparable<Time>,
    IScalarQuantity<Time>,
    IInvertibleScalarQuantity<Frequency>,
    ISquarableScalarQuantity<TimeSquared>,
    IAddableScalarQuantity<Time, Time>,
    ISubtractableScalarQuantity<Time, Time>,
    IDivisibleScalarQuantity<Scalar, Time>
{
    /// <summary>The zero-valued <see cref="Time"/>.</summary>
    public static Time Zero { get; } = new(0);

    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Femtosecond"/>.</summary>
    public static Time OneFemtosecond { get; } = new(1, UnitOfTime.Femtosecond);
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Picosecond"/>.</summary>
    public static Time OnePicosecond { get; } = new(1, UnitOfTime.Picosecond);
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Nanosecond"/>.</summary>
    public static Time OneNanosecond { get; } = new(1, UnitOfTime.Nanosecond);
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Microsecond"/>.</summary>
    public static Time OneMicrosecond { get; } = new(1, UnitOfTime.Microsecond);
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Millisecond"/>.</summary>
    public static Time OneMillisecond { get; } = new(1, UnitOfTime.Millisecond);
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Second"/>.</summary>
    public static Time OneSecond { get; } = new(1, UnitOfTime.Second);

    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Minute"/>.</summary>
    public static Time OneMinute { get; } = new(1, UnitOfTime.Minute);
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Hour"/>.</summary>
    public static Time OneHour { get; } = new(1, UnitOfTime.Hour);
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Day"/>.</summary>
    public static Time OneDay { get; } = new(1, UnitOfTime.Day);
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.Week"/>.</summary>
    public static Time OneWeek { get; } = new(1, UnitOfTime.Week);
    /// <summary>The <see cref="Time"/> with magnitude 1, when expressed in unit <see cref="UnitOfTime.CommonYear"/>.</summary>
    public static Time OneCommonYear { get; } = new(1, UnitOfTime.CommonYear);

    /// <summary>Constructs a <see cref="Time"/> by inverting the <see cref="Frequency"/> <paramref name="frequency"/>.</summary>
    /// <param name="frequency">This <see cref="Frequency"/> is inverted to produce a <see cref="Time"/>.</param>
    public static Time From(Frequency frequency) => new(1 / frequency.InHertz);
    /// <summary>Constructs a <see cref="Time"/> by taking the square root of the <see cref="TimeSquared"/> <paramref name="timeSquared"/>.</summary>
    /// <param name="timeSquared">The square root of this <see cref="TimeSquared"/> is taken to produce a <see cref="Time"/>.</param>
    public static Time From(TimeSquared timeSquared) => new(Math.Sqrt(timeSquared.InSquareSeconds));

    /// <summary>The magnitude of the <see cref="Time"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Time.InMilliseconds"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Time"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfTime"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Time"/>, in unit <paramref name="unitOfTime"/>.</param>
    /// <param name="unitOfTime">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Time"/> a = 2.6 * <see cref="Time.OneSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Time(double magnitude, UnitOfTime unitOfTime) : this(magnitude * unitOfTime.Factor) { }
    /// <summary>Constructs a new <see cref="Time"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Time"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfTime"/> to be specified.</remarks>
    public Time(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in unit <see cref="UnitOfTime.Femtosecond"/>.</summary>
    public Scalar InFemtoseconds => InUnit(UnitOfTime.Femtosecond);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in unit <see cref="UnitOfTime.Picosecond"/>.</summary>
    public Scalar InPicoseconds => InUnit(UnitOfTime.Picosecond);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in unit <see cref="UnitOfTime.Nanosecond"/>.</summary>
    public Scalar InNanoseconds => InUnit(UnitOfTime.Nanosecond);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in unit <see cref="UnitOfTime.Microsecond"/>.</summary>
    public Scalar InMicroseconds => InUnit(UnitOfTime.Microsecond);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in unit <see cref="UnitOfTime.Millisecond"/>.</summary>
    public Scalar InMilliseconds => InUnit(UnitOfTime.Millisecond);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in unit <see cref="UnitOfTime.Second"/>.</summary>
    public Scalar InSeconds => InUnit(UnitOfTime.Second);

    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in unit <see cref="UnitOfTime.Minute"/>.</summary>
    public Scalar InMinutes => InUnit(UnitOfTime.Minute);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in unit <see cref="UnitOfTime.Hour"/>.</summary>
    public Scalar InHours => InUnit(UnitOfTime.Hour);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in unit <see cref="UnitOfTime.Day"/>.</summary>
    public Scalar InDays => InUnit(UnitOfTime.Day);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in unit <see cref="UnitOfTime.Week"/>.</summary>
    public Scalar InWeeks => InUnit(UnitOfTime.Week);
    /// <summary>Retrieves the magnitude of the <see cref="Time"/>, expressed in unit <see cref="UnitOfTime.CommonYear"/>.</summary>
    public Scalar InCommonYears => InUnit(UnitOfTime.CommonYear);

    /// <inheritdoc/>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <inheritdoc/>
    public bool IsZero => Magnitude == 0;
    /// <inheritdoc/>
    public bool IsPositive => Magnitude > 0;
    /// <inheritdoc/>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <inheritdoc/>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <inheritdoc/>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <inheritdoc/>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <inheritdoc/>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <inheritdoc/>
    public Time Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public Time Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public Time Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public Time Round() => new(Math.Round(Magnitude));

    /// <summary>Inverts the <see cref="Time"/>, producing a <see cref="Frequency"/>.</summary>
    public Frequency Invert() => Frequency.From(this);
    /// <summary>Squares the <see cref="Time"/>, producing a <see cref="TimeSquared"/>.</summary>
    public TimeSquared Square() => TimeSquared.From(this);

    /// <inheritdoc/>
    public int CompareTo(Time other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Time"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [#Abbreviation#]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Time"/>, expressed in unit <paramref name="unitOfTime"/>.</summary>
    /// <param name="unitOfTime">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfTime unitOfTime) => InUnit(Magnitude, unitOfTime);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Time"/>, expressed in unit <paramref name="unitOfTime"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="Time"/>.</param>
    /// <param name="unitOfTime">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfTime unitOfTime) => new(magnitude / unitOfTime.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Time"/>.</summary>
    public Time Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Time"/> with negated magnitude.</summary>
    public Time Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Time"/>.</param>
    public static Time operator +(Time x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Time"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Time"/>.</param>
    public static Time operator -(Time x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="Time"/> <paramref name="term"/>, producing another <see cref="Time"/>.</summary>
    /// <param name="term">This <see cref="Time"/> is added to this instance.</param>
    public Time Add(Time term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="Time"/> <paramref name="term"/> from this instance, producing another <see cref="Time"/>.</summary>
    /// <param name="term">This <see cref="Time"/> is subtracted from this instance.</param>
    public Time Subtract(Time term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="Time"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="Time"/>.</summary>
    /// <param name="x">This <see cref="Time"/> is added to the <see cref="Time"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="Time"/> is added to the <see cref="Time"/> <paramref name="x"/>.</param>
    public static Time operator +(Time x, Time y) => x.Add(y);
    /// <summary>Subtract the <see cref="Time"/> <paramref name="y"/> from the <see cref="Time"/> <paramref name="x"/>, producing another <see cref="Time"/>.</summary>
    /// <param name="x">The <see cref="Time"/> <paramref name="y"/> is subtracted from this <see cref="Time"/>.</param>
    /// <param name="y">This <see cref="Time"/> is subtracted from the <see cref="Time"/> <paramref name="x"/>.</param>
    public static Time operator -(Time x, Time y) => x.Subtract(y);

    /// <summary>Divides this <see cref="Time"/> by the <see cref="Time"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Time"/> is divided by this <see cref="Time"/>.</param>
    public Scalar Divide(Time divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="Time"/> <paramref name="x"/> by the <see cref="Time"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="Time"/> is divided by the <see cref="Time"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Time"/> <paramref name="x"/> is divided by this <see cref="Time"/>.</param>
    public static Scalar operator /(Time x, Time y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="Time"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Time"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Time"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Time"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Time"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Time"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Time"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Time x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Time"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Time"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Time"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Time x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Time"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Time Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Time"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Time"/> is scaled.</param>
    public Time Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Time"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Time"/> is divided.</param>
    public Time Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Time"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Time"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
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
    public static Frequency operator /(double x, Time y) => x * y.Invert();

    /// <summary>Produces a <see cref="Time"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Time Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Time"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Time"/> is scaled.</param>
    public Time Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Time"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Time"/> is divided.</param>
    public Time Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Time"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Time"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
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
    /// <summary>Inverts the <see cref="Time"/> <paramref name="y"/> to produce a <see cref="Frequency"/>, which is then scaled by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="Time"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Time"/>, which is inverted to a <see cref="Frequency"/> and scaled by <paramref name="x"/>.</param>
    public static Frequency operator /(Scalar x, Time y) => x * y.Invert();

    /// <summary>Multiplies the <see cref="Time"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Time"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Time"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Time"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Time"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Time"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Time"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Time.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Time x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Time"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Time"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Time"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Time.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Time x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Time x, Time y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Time x, Time y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Time x, Time y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Time x, Time y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="Time"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Time x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Time x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Time"/> of magnitude <paramref name="x"/>.</summary>
    public static Time FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Time"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Time(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Time"/> of equivalent magnitude.</summary>
    public static Time FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Time"/> of equivalent magnitude.</summary>
    public static explicit operator Time(Scalar x) => FromScalar(x);
}