namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="FrequencyDrift"/>, used for describing change in <see cref="Frequency"/> over time.
/// <para>
/// New instances of <see cref="FrequencyDrift"/> can be constructed according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="FrequencyDrift"/> a = 5 * <see cref="FrequencyDrift.OneHertzPerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="FrequencyDrift"/> b = new(7, <see cref="UnitOfFrequencyDrift.HertzPerSecond"/>);
/// </code>
/// </item>
/// <item>
/// <code>
/// </code>
/// </item>
/// </list>
/// Instances of <see cref="FrequencyDrift"/> may be applied according to:
/// <list type="bullet">
/// <item>
/// <code>
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved in a desired unit according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="FrequencyDrift.InHertzPerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="FrequencyDrift.InUnit(UnitOfFrequencyDrift)"/>;
/// </code>
/// </item>
/// </list>
/// </para>
/// </summary>
public readonly partial record struct FrequencyDrift :
    IComparable<FrequencyDrift>,
    IScalarQuantity<FrequencyDrift>,
    IInvertibleScalarQuantity<TimeSquared>,
    ISquareRootableScalarQuantity<Frequency>,
    IAddableScalarQuantity<FrequencyDrift, FrequencyDrift>,
    ISubtractableScalarQuantity<FrequencyDrift, FrequencyDrift>,
    IDivisibleScalarQuantity<Scalar, FrequencyDrift>
{
    /// <summary>The zero-valued <see cref="FrequencyDrift"/>.</summary>
    public static FrequencyDrift Zero { get; } = new(0);

    /// <summary>The <see cref="FrequencyDrift"/> with magnitude 1, when expressed in unit <see cref="UnitOfFrequencyDrift.HertzPerSecond"/>.</summary>
    public static FrequencyDrift OneHertzPerSecond { get; } = new(1, UnitOfFrequencyDrift.HertzPerSecond);

    /// <summary>Constructs a <see cref="FrequencyDrift"/> by inverting the <see cref="TimeSquared"/> <paramref name="timeSquared"/>.</summary>
    /// <param name="timeSquared">This <see cref="TimeSquared"/> is inverted to produce a <see cref="FrequencyDrift"/>.</param>
    public static FrequencyDrift From(TimeSquared timeSquared) => new(1 / timeSquared.InSquareSeconds);
    /// <summary>Constructs a <see cref="FrequencyDrift"/> by squaring the <see cref="Frequency"/> <paramref name="frequency"/>.</summary>
    /// <param name="frequency">This <see cref="Frequency"/> is squared to produce a <see cref="#Quantity"/>.</param>
    public static FrequencyDrift From(Frequency frequency) => new(Math.Pow(frequency.InHertz, 2));

    /// <summary>The magnitude of the <see cref="FrequencyDrift"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="FrequencyDrift.InHertzPerSecond"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="FrequencyDrift"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfFrequencyDrift"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="FrequencyDrift"/>, in unit <paramref name="unitOfFrequencyDrift"/>.</param>
    /// <param name="unitOfFrequencyDrift">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="FrequencyDrift"/> a = 2.6 * <see cref="FrequencyDrift.OneHertzPerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public FrequencyDrift(double magnitude, UnitOfFrequencyDrift unitOfFrequencyDrift) : this(magnitude * unitOfFrequencyDrift.Factor) { }
    /// <summary>Constructs a new <see cref="FrequencyDrift"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="FrequencyDrift"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfFrequencyDrift"/> to be specified.</remarks>
    public FrequencyDrift(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="FrequencyDrift"/>, expressed in unit <see cref="UnitOfFrequencyDrift.HertzPerSecond"/>.</summary>
    public Scalar InHertzPerSecond => InUnit(UnitOfFrequencyDrift.HertzPerSecond);

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
    public FrequencyDrift Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public FrequencyDrift Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public FrequencyDrift Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public FrequencyDrift Round() => new(Math.Round(Magnitude));

    /// <summary>Inverts the <see cref="FrequencyDrift"/>, producing a <see cref="TimeSquared"/>.</summary>
    public TimeSquared Invert() => TimeSquared.From(this);
    /// <summary>Takes the square root of the <see cref="FrequencyDrift"/>, producing a <see cref="Frequency"/>.</summary>
    public Frequency SquareRoot() => Frequency.From(this);

    /// <inheritdoc/>
    public int CompareTo(FrequencyDrift other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="FrequencyDrift"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [Hz * s^-1]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="FrequencyDrift"/>, expressed in unit <paramref name="unitOfFrequencyDrift"/>.</summary>
    /// <param name="unitOfFrequencyDrift">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfFrequencyDrift unitOfFrequencyDrift) => InUnit(Magnitude, unitOfFrequencyDrift);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="FrequencyDrift"/>, expressed in unit <paramref name="unitOfFrequencyDrift"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="FrequencyDrift"/>.</param>
    /// <param name="unitOfFrequencyDrift">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfFrequencyDrift unitOfFrequencyDrift) => new(magnitude / unitOfFrequencyDrift.Factor);

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

    /// <summary>Adds this instance and the <see cref="FrequencyDrift"/> <paramref name="term"/>, producing another <see cref="FrequencyDrift"/>.</summary>
    /// <param name="term">This <see cref="FrequencyDrift"/> is added to this instance.</param>
    public FrequencyDrift Add(FrequencyDrift term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="FrequencyDrift"/> <paramref name="term"/> from this instance, producing another <see cref="FrequencyDrift"/>.</summary>
    /// <param name="term">This <see cref="FrequencyDrift"/> is subtracted from this instance.</param>
    public FrequencyDrift Subtract(FrequencyDrift term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="FrequencyDrift"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="FrequencyDrift"/>.</summary>
    /// <param name="x">This <see cref="FrequencyDrift"/> is added to the <see cref="FrequencyDrift"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="FrequencyDrift"/> is added to the <see cref="FrequencyDrift"/> <paramref name="x"/>.</param>
    public static FrequencyDrift operator +(FrequencyDrift x, FrequencyDrift y) => x.Add(y);
    /// <summary>Subtract the <see cref="FrequencyDrift"/> <paramref name="y"/> from the <see cref="FrequencyDrift"/> <paramref name="x"/>, producing another <see cref="FrequencyDrift"/>.</summary>
    /// <param name="x">The <see cref="FrequencyDrift"/> <paramref name="y"/> is subtracted from this <see cref="FrequencyDrift"/>.</param>
    /// <param name="y">This <see cref="FrequencyDrift"/> is subtracted from the <see cref="FrequencyDrift"/> <paramref name="x"/>.</param>
    public static FrequencyDrift operator -(FrequencyDrift x, FrequencyDrift y) => x.Subtract(y);

    /// <summary>Divides this <see cref="FrequencyDrift"/> by the <see cref="FrequencyDrift"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="FrequencyDrift"/> is divided by this <see cref="FrequencyDrift"/>.</param>
    public Scalar Divide(FrequencyDrift divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="FrequencyDrift"/> <paramref name="x"/> by the <see cref="FrequencyDrift"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="FrequencyDrift"/> is divided by the <see cref="FrequencyDrift"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="FrequencyDrift"/> <paramref name="x"/> is divided by this <see cref="FrequencyDrift"/>.</param>
    public static Scalar operator /(FrequencyDrift x, FrequencyDrift y) => x.Divide(y)
;

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
    /// <summary>Divides the <see cref="FrequencyDrift"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="FrequencyDrift"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="FrequencyDrift"/> <paramref name="x"/> is divded.</param>
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
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
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
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
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

    /// <summary>Multiplies the <see cref="FrequencyDrift"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="FrequencyDrift"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="FrequencyDrift"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="FrequencyDrift"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="FrequencyDrift"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="FrequencyDrift"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="FrequencyDrift"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="FrequencyDrift.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(FrequencyDrift x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="FrequencyDrift"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="FrequencyDrift"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="FrequencyDrift"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="FrequencyDrift.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(FrequencyDrift x, IScalarQuantity y) => x.Multiply(y);

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
    public static bool operator <=(FrequencyDrift x, FrequencyDrift y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(FrequencyDrift x, FrequencyDrift y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="FrequencyDrift"/> <paramref name="x"/>.</summary>
    public static implicit operator double(FrequencyDrift x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(FrequencyDrift x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="FrequencyDrift"/> of magnitude <paramref name="x"/>.</summary>
    public static FrequencyDrift FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="FrequencyDrift"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator FrequencyDrift(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="FrequencyDrift"/> of equivalent magnitude.</summary>
    public static FrequencyDrift FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="FrequencyDrift"/> of equivalent magnitude.</summary>
    public static explicit operator FrequencyDrift(Scalar x) => FromScalar(x);
}