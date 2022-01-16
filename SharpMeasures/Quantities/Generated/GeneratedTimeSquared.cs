namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="TimeSquared"/>, describing the square of <see cref="Time"/>.
/// <para>
/// New instances of <see cref="TimeSquared"/> can be constructed according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="TimeSquared"/> a = 5 * <see cref="TimeSquared.OneSquareSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="TimeSquared"/> b = new(7, <see cref="UnitOfTimeSquared.SquareSecond"/>);
/// </code>
/// </item>
/// <item>
/// <code>
/// </code>
/// </item>
/// </list>
/// Instances of <see cref="TimeSquared"/> may be applied according to:
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
/// <see cref="TimeSquared.InSquareSeconds"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="TimeSquared.InUnit(UnitOfTimeSquared)"/>;
/// </code>
/// </item>
/// </list>
/// </para>
/// </summary>
public readonly partial record struct TimeSquared :
    IComparable<TimeSquared>,
    IScalarQuantity<TimeSquared>,
    IInvertibleScalarQuantity<FrequencyDrift>,
    ISquareRootableScalarQuantity<Time>,
    IAddableScalarQuantity<TimeSquared, TimeSquared>,
    ISubtractableScalarQuantity<TimeSquared, TimeSquared>,
    IDivisibleScalarQuantity<Scalar, TimeSquared>
{
    /// <summary>The zero-valued <see cref="TimeSquared"/>.</summary>
    public static TimeSquared Zero { get; } = new(0);

    /// <summary>The <see cref="TimeSquared"/> with magnitude 1, when expressed in unit <see cref="UnitOfTimeSquared.SquareSecond"/>.</summary>
    public static TimeSquared OneSquareSecond { get; } = new(1, UnitOfTimeSquared.SquareSecond);

    /// <summary>Constructs a <see cref="TimeSquared"/> by inverting the <see cref="FrequencyDrift"/> <paramref name="frequencyDrift"/>.</summary>
    /// <param name="frequencyDrift">This <see cref="FrequencyDrift"/> is inverted to produce a <see cref="TimeSquared"/>.</param>
    public static TimeSquared From(FrequencyDrift frequencyDrift) => new(1 / frequencyDrift.InHertzPerSecond);
    /// <summary>Constructs a <see cref="TimeSquared"/> by squaring the <see cref="Time"/> <paramref name="time"/>.</summary>
    /// <param name="time">This <see cref="Time"/> is squared to produce a <see cref="#Quantity"/>.</param>
    public static TimeSquared From(Time time) => new(Math.Pow(time.InSeconds, 2));

    /// <summary>The magnitude of the <see cref="TimeSquared"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="TimeSquared.InSquareSeconds"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="TimeSquared"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfTimeSquared"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TimeSquared"/>, in unit <paramref name="unitOfTimeSquared"/>.</param>
    /// <param name="unitOfTimeSquared">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="TimeSquared"/> a = 2.6 * <see cref="TimeSquared.OneSquareSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public TimeSquared(double magnitude, UnitOfTimeSquared unitOfTimeSquared) : this(magnitude * unitOfTimeSquared.Factor) { }
    /// <summary>Constructs a new <see cref="TimeSquared"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TimeSquared"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfTimeSquared"/> to be specified.</remarks>
    public TimeSquared(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="TimeSquared"/>, expressed in unit <see cref="UnitOfTimeSquared.SquareSecond"/>.</summary>
    public Scalar InSquareSeconds => InUnit(UnitOfTimeSquared.SquareSecond);

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
    public TimeSquared Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public TimeSquared Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public TimeSquared Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public TimeSquared Round() => new(Math.Round(Magnitude));

    /// <summary>Inverts the <see cref="TimeSquared"/>, producing a <see cref="FrequencyDrift"/>.</summary>
    public FrequencyDrift Invert() => FrequencyDrift.From(this);
    /// <summary>Takes the square root of the <see cref="TimeSquared"/>, producing a <see cref="Time"/>.</summary>
    public Time SquareRoot() => Time.From(this);

    /// <inheritdoc/>
    public int CompareTo(TimeSquared other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="TimeSquared"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [#Abbreviation#]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="TimeSquared"/>, expressed in unit <paramref name="unitOfTimeSquared"/>.</summary>
    /// <param name="unitOfTimeSquared">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfTimeSquared unitOfTimeSquared) => InUnit(Magnitude, unitOfTimeSquared);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="TimeSquared"/>, expressed in unit <paramref name="unitOfTimeSquared"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="TimeSquared"/>.</param>
    /// <param name="unitOfTimeSquared">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfTimeSquared unitOfTimeSquared) => new(magnitude / unitOfTimeSquared.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="TimeSquared"/>.</summary>
    public TimeSquared Plus() => this;
    /// <summary>Negation, resulting in a <see cref="TimeSquared"/> with negated magnitude.</summary>
    public TimeSquared Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="TimeSquared"/>.</param>
    public static TimeSquared operator +(TimeSquared x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="TimeSquared"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="TimeSquared"/>.</param>
    public static TimeSquared operator -(TimeSquared x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="TimeSquared"/> <paramref name="term"/>, producing another <see cref="TimeSquared"/>.</summary>
    /// <param name="term">This <see cref="TimeSquared"/> is added to this instance.</param>
    public TimeSquared Add(TimeSquared term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="TimeSquared"/> <paramref name="term"/> from this instance, producing another <see cref="TimeSquared"/>.</summary>
    /// <param name="term">This <see cref="TimeSquared"/> is subtracted from this instance.</param>
    public TimeSquared Subtract(TimeSquared term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="TimeSquared"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="TimeSquared"/>.</summary>
    /// <param name="x">This <see cref="TimeSquared"/> is added to the <see cref="TimeSquared"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="TimeSquared"/> is added to the <see cref="TimeSquared"/> <paramref name="x"/>.</param>
    public static TimeSquared operator +(TimeSquared x, TimeSquared y) => x.Add(y);
    /// <summary>Subtract the <see cref="TimeSquared"/> <paramref name="y"/> from the <see cref="TimeSquared"/> <paramref name="x"/>, producing another <see cref="TimeSquared"/>.</summary>
    /// <param name="x">The <see cref="TimeSquared"/> <paramref name="y"/> is subtracted from this <see cref="TimeSquared"/>.</param>
    /// <param name="y">This <see cref="TimeSquared"/> is subtracted from the <see cref="TimeSquared"/> <paramref name="x"/>.</param>
    public static TimeSquared operator -(TimeSquared x, TimeSquared y) => x.Subtract(y);

    /// <summary>Divides this <see cref="TimeSquared"/> by the <see cref="TimeSquared"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="TimeSquared"/> is divided by this <see cref="TimeSquared"/>.</param>
    public Scalar Divide(TimeSquared divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="TimeSquared"/> <paramref name="x"/> by the <see cref="TimeSquared"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="TimeSquared"/> is divided by the <see cref="TimeSquared"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TimeSquared"/> <paramref name="x"/> is divided by this <see cref="TimeSquared"/>.</param>
    public static Scalar operator /(TimeSquared x, TimeSquared y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="TimeSquared"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="TimeSquared"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="TimeSquared"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="TimeSquared"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="TimeSquared"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="TimeSquared"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(TimeSquared x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="TimeSquared"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="TimeSquared"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(TimeSquared x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="TimeSquared"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public TimeSquared Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="TimeSquared"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="TimeSquared"/> is scaled.</param>
    public TimeSquared Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="TimeSquared"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="TimeSquared"/> is divided.</param>
    public TimeSquared Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="TimeSquared"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static TimeSquared operator %(TimeSquared x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="TimeSquared"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="TimeSquared"/> <paramref name="x"/>.</param>
    public static TimeSquared operator *(TimeSquared x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="TimeSquared"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="TimeSquared"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TimeSquared"/>, which is scaled by <paramref name="x"/>.</param>
    public static TimeSquared operator *(double x, TimeSquared y) => y.Multiply(x);
    /// <summary>Scales the <see cref="TimeSquared"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="TimeSquared"/> <paramref name="x"/>.</param>
    public static TimeSquared operator /(TimeSquared x, double y) => x.Divide(y);
    /// <summary>Inverts the <see cref="TimeSquared"/> <paramref name="y"/> to produce a <see cref="FrequencyDrift"/>, which is then scaled by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="TimeSquared"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TimeSquared"/>, which is inverted to a <see cref="FrequencyDrift"/> and scaled by <paramref name="x"/>.</param>
    public static FrequencyDrift operator /(double x, TimeSquared y) => x * y.Invert();

    /// <summary>Produces a <see cref="TimeSquared"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public TimeSquared Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="TimeSquared"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="TimeSquared"/> is scaled.</param>
    public TimeSquared Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="TimeSquared"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="TimeSquared"/> is divided.</param>
    public TimeSquared Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="TimeSquared"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static TimeSquared operator %(TimeSquared x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="TimeSquared"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="TimeSquared"/> <paramref name="x"/>.</param>
    public static TimeSquared operator *(TimeSquared x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="TimeSquared"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="TimeSquared"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TimeSquared"/>, which is scaled by <paramref name="x"/>.</param>
    public static TimeSquared operator *(Scalar x, TimeSquared y) => y.Multiply(x);
    /// <summary>Scales the <see cref="TimeSquared"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="TimeSquared"/> <paramref name="x"/>.</param>
    public static TimeSquared operator /(TimeSquared x, Scalar y) => x.Divide(y);
    /// <summary>Inverts the <see cref="TimeSquared"/> <paramref name="y"/> to produce a <see cref="FrequencyDrift"/>, which is then scaled by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="TimeSquared"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TimeSquared"/>, which is inverted to a <see cref="FrequencyDrift"/> and scaled by <paramref name="x"/>.</param>
    public static FrequencyDrift operator /(Scalar x, TimeSquared y) => x * y.Invert();

    /// <summary>Multiplies the <see cref="TimeSquared"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="TimeSquared"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="TimeSquared"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="TimeSquared"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="TimeSquared"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="TimeSquared"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="TimeSquared.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(TimeSquared x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="TimeSquared"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="TimeSquared"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="TimeSquared.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(TimeSquared x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(TimeSquared x, TimeSquared y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(TimeSquared x, TimeSquared y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(TimeSquared x, TimeSquared y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(TimeSquared x, TimeSquared y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="TimeSquared"/> <paramref name="x"/>.</summary>
    public static implicit operator double(TimeSquared x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(TimeSquared x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="TimeSquared"/> of magnitude <paramref name="x"/>.</summary>
    public static TimeSquared FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="TimeSquared"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator TimeSquared(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="TimeSquared"/> of equivalent magnitude.</summary>
    public static TimeSquared FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="TimeSquared"/> of equivalent magnitude.</summary>
    public static explicit operator TimeSquared(Scalar x) => FromScalar(x);
}