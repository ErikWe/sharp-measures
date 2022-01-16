namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Frequency"/>, used for describing temporal periodicity.
/// <para>
/// New instances of <see cref="Frequency"/> can be constructed according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Frequency"/> a = 5 * <see cref="Frequency.OneHertz"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Frequency"/> b = new(7, <see cref="UnitOfFrequency.PerSecond"/>);
/// </code>
/// </item>
/// </list>
/// Instances of <see cref="Frequency"/> may be applied according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Velocity"/> c = 3 * <see cref="Length.OneMillimetre"/> * <see cref="Frequency.OneHertz"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved in a desired unit according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Frequency.InHertz"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Frequency.InUnit(UnitOfFrequency)"/>;
/// </code>
/// </item>
/// </list>
/// </para>
/// </summary>
public readonly partial record struct Frequency :
    IComparable<Frequency>,
    IScalarQuantity<Frequency>,
    IInvertibleScalarQuantity<Time>,
    ISquarableScalarQuantity<FrequencyDrift>,
    IAddableScalarQuantity<Frequency, Frequency>,
    ISubtractableScalarQuantity<Frequency, Frequency>,
    IDivisibleScalarQuantity<Scalar, Frequency>
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

    /// <summary>Constructs a <see cref="Frequency"/> by inverting the <see cref="Time"/> <paramref name="time"/>.</summary>
    /// <param name="time">This <see cref="Time"/> is inverted to produce a <see cref="Frequency"/>.</param>
    public static Frequency From(Time time) => new(1 / time.InSeconds);
    /// <summary>Constructs a <see cref="Frequency"/> by taking the square root of the <see cref="FrequencyDrift"/> <paramref name="frequencyDrift"/>.</summary>
    /// <param name="frequencyDrift">The square root of this <see cref="FrequencyDrift"/> is taken to produce a <see cref="Frequency"/>.</param>
    public static Frequency From(FrequencyDrift frequencyDrift) => new(Math.Sqrt(frequencyDrift.InHertzPerSecond));

    /// <summary>The magnitude of the <see cref="Frequency"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Frequency.InPerSecond"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Frequency"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfFrequency"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Frequency"/>, in unit <paramref name="unitOfFrequency"/>.</param>
    /// <param name="unitOfFrequency">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Frequency"/> a = 2.6 * <see cref="Frequency.OneKilohertz"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Frequency(double magnitude, UnitOfFrequency unitOfFrequency) : this(magnitude * unitOfFrequency.Factor) { }
    /// <summary>Constructs a new <see cref="Frequency"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Frequency"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfFrequency"/> to be specified.</remarks>
    public Frequency(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Frequency"/>, expressed in unit <see cref="UnitOfFrequency.PerSecond"/>.</summary>
    public Scalar InPerSecond => InUnit(UnitOfFrequency.PerSecond);

    /// <summary>Retrieves the magnitude of the <see cref="Frequency"/>, expressed in unit <see cref="UnitOfFrequency.Hertz"/>.</summary>
    public Scalar InHertz => InUnit(UnitOfFrequency.Hertz);
    /// <summary>Retrieves the magnitude of the <see cref="Frequency"/>, expressed in unit <see cref="UnitOfFrequency.Kilohertz"/>.</summary>
    public Scalar InKilohertz => InUnit(UnitOfFrequency.Kilohertz);
    /// <summary>Retrieves the magnitude of the <see cref="Frequency"/>, expressed in unit <see cref="UnitOfFrequency.Megahertz"/>.</summary>
    public Scalar InMegahertz => InUnit(UnitOfFrequency.Megahertz);
    /// <summary>Retrieves the magnitude of the <see cref="Frequency"/>, expressed in unit <see cref="UnitOfFrequency.Gigahertz"/>.</summary>
    public Scalar InGigahertz => InUnit(UnitOfFrequency.Gigahertz);

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
    public Frequency Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public Frequency Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public Frequency Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public Frequency Round() => new(Math.Round(Magnitude));

    /// <summary>Inverts the <see cref="Frequency"/>, producing a <see cref="Time"/>.</summary>
    public Time Invert() => Time.From(this);
    /// <summary>Squares the <see cref="Frequency"/>, producing a <see cref="FrequencyDrift"/>.</summary>
    public FrequencyDrift Square() => FrequencyDrift.From(this);

    /// <inheritdoc/>
    public int CompareTo(Frequency other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Frequency"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [Hz]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Frequency"/>, expressed in unit <paramref name="unitOfFrequency"/>.</summary>
    /// <param name="unitOfFrequency">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfFrequency unitOfFrequency) => InUnit(Magnitude, unitOfFrequency);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Frequency"/>, expressed in unit <paramref name="unitOfFrequency"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="Frequency"/>.</param>
    /// <param name="unitOfFrequency">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfFrequency unitOfFrequency) => new(magnitude / unitOfFrequency.Factor);

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

    /// <summary>Adds this instance and the <see cref="Frequency"/> <paramref name="term"/>, producing another <see cref="Frequency"/>.</summary>
    /// <param name="term">This <see cref="Frequency"/> is added to this instance.</param>
    public Frequency Add(Frequency term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="Frequency"/> <paramref name="term"/> from this instance, producing another <see cref="Frequency"/>.</summary>
    /// <param name="term">This <see cref="Frequency"/> is subtracted from this instance.</param>
    public Frequency Subtract(Frequency term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="Frequency"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="Frequency"/>.</summary>
    /// <param name="x">This <see cref="Frequency"/> is added to the <see cref="Frequency"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="Frequency"/> is added to the <see cref="Frequency"/> <paramref name="x"/>.</param>
    public static Frequency operator +(Frequency x, Frequency y) => x.Add(y);
    /// <summary>Subtract the <see cref="Frequency"/> <paramref name="y"/> from the <see cref="Frequency"/> <paramref name="x"/>, producing another <see cref="Frequency"/>.</summary>
    /// <param name="x">The <see cref="Frequency"/> <paramref name="y"/> is subtracted from this <see cref="Frequency"/>.</param>
    /// <param name="y">This <see cref="Frequency"/> is subtracted from the <see cref="Frequency"/> <paramref name="x"/>.</param>
    public static Frequency operator -(Frequency x, Frequency y) => x.Subtract(y);

    /// <summary>Divides this <see cref="Frequency"/> by the <see cref="Frequency"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Frequency"/> is divided by this <see cref="Frequency"/>.</param>
    public Scalar Divide(Frequency divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="Frequency"/> <paramref name="x"/> by the <see cref="Frequency"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="Frequency"/> is divided by the <see cref="Frequency"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Frequency"/> <paramref name="x"/> is divided by this <see cref="Frequency"/>.</param>
    public static Scalar operator /(Frequency x, Frequency y) => x.Divide(y)
;

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
    /// <summary>Divides the <see cref="Frequency"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Frequency"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Frequency"/> <paramref name="x"/> is divded.</param>
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
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
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
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
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

    /// <summary>Multiplies the <see cref="Frequency"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Frequency"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Frequency"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Frequency"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Frequency"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Frequency"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Frequency"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Frequency.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Frequency x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Frequency"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Frequency"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Frequency"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Frequency.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Frequency x, IScalarQuantity y) => x.Multiply(y);

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
    public static bool operator <=(Frequency x, Frequency y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Frequency x, Frequency y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="Frequency"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Frequency x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Frequency x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Frequency"/> of magnitude <paramref name="x"/>.</summary>
    public static Frequency FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Frequency"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Frequency(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Frequency"/> of equivalent magnitude.</summary>
    public static Frequency FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Frequency"/> of equivalent magnitude.</summary>
    public static explicit operator Frequency(Scalar x) => FromScalar(x);
}