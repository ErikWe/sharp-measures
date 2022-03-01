#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Power"/>, describes the rate of <see cref="Work"/> over <see cref="Time"/>. The quantity
/// is expressed in <see cref="UnitOfPower"/>, with the SI unit being [W].
/// <para>
/// New instances of <see cref="Power"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfPower"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Power"/> a = 3 * <see cref="Power.OneWatt"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Power"/> d = <see cref="Power.From(Work, Time)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="Power"/> can be retrieved in the desired <see cref="UnitOfPower"/> using pre-defined properties,
/// such as <see cref="Watts"/>.
/// </para>
/// </summary>
public readonly partial record struct Power :
    IComparable<Power>,
    IScalarQuantity,
    IScalableScalarQuantity<Power>,
    IMultiplicableScalarQuantity<Power, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Power, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="Power"/>.</summary>
    public static Power Zero { get; } = new(0);

    /// <summary>The <see cref="Power"/> of magnitude 1, when expressed in <see cref="UnitOfPower.Watt"/>.</summary>
    public static Power OneWatt { get; } = UnitOfPower.Watt.Power;
    /// <summary>The <see cref="Power"/> of magnitude 1, when expressed in <see cref="UnitOfPower.Kilowatt"/>.</summary>
    public static Power OneKilowatt { get; } = UnitOfPower.Kilowatt.Power;
    /// <summary>The <see cref="Power"/> of magnitude 1, when expressed in <see cref="UnitOfPower.Megawatt"/>.</summary>
    public static Power OneMegawatt { get; } = UnitOfPower.Megawatt.Power;
    /// <summary>The <see cref="Power"/> of magnitude 1, when expressed in <see cref="UnitOfPower.Gigawatt"/>.</summary>
    public static Power OneGigawatt { get; } = UnitOfPower.Gigawatt.Power;
    /// <summary>The <see cref="Power"/> of magnitude 1, when expressed in <see cref="UnitOfPower.Terawatt"/>.</summary>
    public static Power OneTerawatt { get; } = UnitOfPower.Terawatt.Power;

    /// <summary>The magnitude of the <see cref="Power"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfPower)"/> or a pre-defined property
    /// - such as <see cref="Watts"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Power"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfPower"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Power"/>, expressed in <paramref name="unitOfPower"/>.</param>
    /// <param name="unitOfPower">The <see cref="UnitOfPower"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Power"/> a = 3 * <see cref="Power.OneWatt"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Power(Scalar magnitude, UnitOfPower unitOfPower) : this(magnitude.Magnitude, unitOfPower) { }
    /// <summary>Constructs a new <see cref="Power"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfPower"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Power"/>, expressed in <paramref name="unitOfPower"/>.</param>
    /// <param name="unitOfPower">The <see cref="UnitOfPower"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Power"/> a = 3 * <see cref="Power.OneWatt"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Power(double magnitude, UnitOfPower unitOfPower) : this(magnitude * unitOfPower.Power.Magnitude) { }
    /// <summary>Constructs a new <see cref="Power"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Power"/>.</param>
    /// <remarks>Consider preferring <see cref="Power(Scalar, UnitOfPower)"/>.</remarks>
    public Power(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Power"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Power"/>.</param>
    /// <remarks>Consider preferring <see cref="Power(double, UnitOfPower)"/>.</remarks>
    public Power(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Power"/>, expressed in <see cref="UnitOfPower.Watt"/>.</summary>
    public Scalar Watts => InUnit(UnitOfPower.Watt);
    /// <summary>Retrieves the magnitude of the <see cref="Power"/>, expressed in <see cref="UnitOfPower.Kilowatt"/>.</summary>
    public Scalar Kilowatts => InUnit(UnitOfPower.Kilowatt);
    /// <summary>Retrieves the magnitude of the <see cref="Power"/>, expressed in <see cref="UnitOfPower.Megawatt"/>.</summary>
    public Scalar Megawatts => InUnit(UnitOfPower.Megawatt);
    /// <summary>Retrieves the magnitude of the <see cref="Power"/>, expressed in <see cref="UnitOfPower.Gigawatt"/>.</summary>
    public Scalar Gigawatts => InUnit(UnitOfPower.Gigawatt);
    /// <summary>Retrieves the magnitude of the <see cref="Power"/>, expressed in <see cref="UnitOfPower.Terawatt"/>.</summary>
    public Scalar Terawatts => InUnit(UnitOfPower.Terawatt);

    /// <summary>Indicates whether the magnitude of the <see cref="Power"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Power"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Power"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Power"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Power"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Power"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Power"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Power"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="Power"/>.</summary>
    public Power Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="Power"/>.</summary>
    public Power Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="Power"/>.</summary>
    public Power Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="Power"/> to the nearest integer value.</summary>
    public Power Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Power other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Power"/> in the default unit
    /// <see cref="UnitOfPower.Watt"/>, followed by the symbol [W].</summary>
    public override string ToString() => $"{Watts} [W]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Power"/>,
    /// expressed in <paramref name="unitOfPower"/>.</summary>
    /// <param name="unitOfPower">The <see cref="UnitOfPower"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfPower unitOfPower) => InUnit(this, unitOfPower);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Power"/>,
    /// expressed in <paramref name="unitOfPower"/>.</summary>
    /// <param name="power">The <see cref="Power"/> to be expressed in <paramref name="unitOfPower"/>.</param>
    /// <param name="unitOfPower">The <see cref="UnitOfPower"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Power power, UnitOfPower unitOfPower) => new(power.Magnitude / unitOfPower.Power.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Power"/>.</summary>
    public Power Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Power"/> with negated magnitude.</summary>
    public Power Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Power"/>.</param>
    public static Power operator +(Power x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Power"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Power"/>.</param>
    public static Power operator -(Power x) => x.Negate();

    /// <summary>Multiplicates the <see cref="Power"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Power"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Power"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Power"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Power"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Power"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Power"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Power x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Power"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Power"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Power"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Power y) => y.Multiply(x);
    /// <summary>Division of the <see cref="Power"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Power"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Power"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Power x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="Power"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="Power"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Power"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, Power y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="Power"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Power Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Power"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Power"/> is scaled.</param>
    public Power Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Power"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Power"/> is divided.</param>
    public Power Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Power"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Power"/> <paramref name="x"/> by this value.</param>
    public static Power operator %(Power x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Power"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Power"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Power"/> <paramref name="x"/>.</param>
    public static Power operator *(Power x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Power"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Power"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Power"/>, which is scaled by <paramref name="x"/>.</param>
    public static Power operator *(double x, Power y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Power"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Power"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Power"/> <paramref name="x"/>.</param>
    public static Power operator /(Power x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="Power"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Power Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Power"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Power"/> is scaled.</param>
    public Power Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Power"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Power"/> is divided.</param>
    public Power Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Power"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Power"/> <paramref name="x"/> by this value.</param>
    public static Power operator %(Power x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Power"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Power"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Power"/> <paramref name="x"/>.</param>
    public static Power operator *(Power x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Power"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Power"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Power"/>, which is scaled by <paramref name="x"/>.</param>
    public static Power operator *(Scalar x, Power y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Power"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Power"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Power"/> <paramref name="x"/>.</param>
    public static Power operator /(Power x, Scalar y) => x.Divide(y);

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TProductScalarQuantity Multiply<TProductScalarQuantity, TFactorScalarQuantity>(TFactorScalarQuantity factor, Func<double, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorScalarQuantity : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        ArgumentNullException.ThrowIfNull(factor, nameof(factor));

        return factory(Magnitude * factor.Magnitude);

    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TQuotientScalarQuantity Divide<TQuotientScalarQuantity, TDivisorScalarQuantity>(TDivisorScalarQuantity divisor, Func<double, TQuotientScalarQuantity> factory)
        where TQuotientScalarQuantity : IScalarQuantity
        where TDivisorScalarQuantity : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        ArgumentNullException.ThrowIfNull(divisor, nameof(divisor));

        return factory(Magnitude / divisor.Magnitude);
    }

    /// <summary>Multiplication of the <see cref="Power"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Power"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Power"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Power x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="Power"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Power"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Power"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Power x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Power"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Power"/>.</param>
    public static bool operator <(Power x, Power y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Power"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Power"/>.</param>
    public static bool operator >(Power x, Power y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Power"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Power"/>.</param>
    public static bool operator <=(Power x, Power y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Power"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Power"/>.</param>
    public static bool operator >=(Power x, Power y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Power"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(Power x) => x.ToDouble();

    /// <summary>Converts the <see cref="Power"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Power x) => x.ToScalar();

    /// <summary>Constructs the <see cref="Power"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Power FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Power"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Power(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="Power"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static Power FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="Power"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator Power(Scalar x) => FromScalar(x);
}
