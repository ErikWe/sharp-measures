namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="TemperatureDifference"/>, describing a change in <see cref="Temperature"/>.
/// The quantity is expressed in <see cref="UnitOfTemperatureDifference"/>, with the SI unit being [K].
/// <para>
/// New instances of <see cref="TemperatureDifference"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfTemperatureDifference"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="TemperatureDifference"/> a = 3 * <see cref="TemperatureDifference.OneKelvin"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="#Param:quantity"/> d = <see cref="TemperatureDifference.From(Temperature, Temperature)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="TemperatureDifference"/> e = <see cref="Temperature.AsTemperatureDifference"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="TemperatureDifference"/> can be retrieved in the desired <see cref="UnitOfTemperatureDifference"/> using pre-defined properties,
/// such as <see cref="Kelvin"/>
/// </para>
/// </summary>
/// <remarks>
/// <see cref="TemperatureDifference"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="Temperature"/></term>
/// <description>Describes an absolute temperature, not a difference in temperature.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct TemperatureDifference :
    IComparable<TemperatureDifference>,
    IScalarQuantity,
    IScalableScalarQuantity<TemperatureDifference>,
    IMultiplicableScalarQuantity<TemperatureDifference, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<TemperatureDifference, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="TemperatureDifference"/>.</summary>
    public static TemperatureDifference Zero { get; } = new(0);

    /// <summary>The <see cref="TemperatureDifference"/> with magnitude 1, when expressed in unit <see cref="UnitOfTemperatureDifference.Kelvin"/>.</summary>
    public static TemperatureDifference OneKelvin { get; } = new(1, UnitOfTemperatureDifference.Kelvin);
    /// <summary>The <see cref="TemperatureDifference"/> with magnitude 1, when expressed in unit <see cref="UnitOfTemperatureDifference.Celsius"/>.</summary>
    public static TemperatureDifference OneCelsius { get; } = new(1, UnitOfTemperatureDifference.Celsius);
    /// <summary>The <see cref="TemperatureDifference"/> with magnitude 1, when expressed in unit <see cref="UnitOfTemperatureDifference.Rankine"/>.</summary>
    public static TemperatureDifference OneRankine { get; } = new(1, UnitOfTemperatureDifference.Rankine);
    /// <summary>The <see cref="TemperatureDifference"/> with magnitude 1, when expressed in unit <see cref="UnitOfTemperatureDifference.Fahrenheit"/>.</summary>
    public static TemperatureDifference OneFahrenheit { get; } = new(1, UnitOfTemperatureDifference.Fahrenheit);

    /// <summary>The magnitude of the <see cref="TemperatureDifference"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfTemperatureDifference)"/> or a pre-defined property
    /// - such as <see cref="Kelvin"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="TemperatureDifference"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfTemperatureDifference"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TemperatureDifference"/>, expressed in <paramref name="unitOfTemperatureDifference"/>.</param>
    /// <param name="unitOfTemperatureDifference">The <see cref="UnitOfTemperatureDifference"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="TemperatureDifference"/> a = 3 * <see cref="TemperatureDifference.OneKelvin"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public TemperatureDifference(Scalar magnitude, UnitOfTemperatureDifference unitOfTemperatureDifference) : this(magnitude.Magnitude, unitOfTemperatureDifference) { }
    /// <summary>Constructs a new <see cref="TemperatureDifference"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfTemperatureDifference"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TemperatureDifference"/>, expressed in <paramref name="unitOfTemperatureDifference"/>.</param>
    /// <param name="unitOfTemperatureDifference">The <see cref="UnitOfTemperatureDifference"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="TemperatureDifference"/> a = 3 * <see cref="TemperatureDifference.OneKelvin"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public TemperatureDifference(double magnitude, UnitOfTemperatureDifference unitOfTemperatureDifference) : this(magnitude * unitOfTemperatureDifference.Factor) { }
    /// <summary>Constructs a new <see cref="TemperatureDifference"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TemperatureDifference"/>.</param>
    /// <remarks>Consider preferring <see cref="TemperatureDifference(Scalar, UnitOfTemperatureDifference)"/>.</remarks>
    public TemperatureDifference(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="TemperatureDifference"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TemperatureDifference"/>.</param>
    /// <remarks>Consider preferring <see cref="TemperatureDifference(double, UnitOfTemperatureDifference)"/>.</remarks>
    public TemperatureDifference(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="TemperatureDifference"/> to an instance of the associated quantity <see cref="Temperature"/>, of equal magnitude.</summary>
    public Temperature AsTemperature => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="TemperatureDifference"/>, expressed in <see cref="UnitOfTemperatureDifference.Kelvin"/>.</summary>
    public Scalar Kelvin => InUnit(UnitOfTemperatureDifference.Kelvin);
    /// <summary>Retrieves the magnitude of the <see cref="TemperatureDifference"/>, expressed in <see cref="UnitOfTemperatureDifference.Celsius"/>.</summary>
    public Scalar Celsius => InUnit(UnitOfTemperatureDifference.Celsius);
    /// <summary>Retrieves the magnitude of the <see cref="TemperatureDifference"/>, expressed in <see cref="UnitOfTemperatureDifference.Rankine"/>.</summary>
    public Scalar Rankine => InUnit(UnitOfTemperatureDifference.Rankine);
    /// <summary>Retrieves the magnitude of the <see cref="TemperatureDifference"/>, expressed in <see cref="UnitOfTemperatureDifference.Fahrenheit"/>.</summary>
    public Scalar Fahrenheit => InUnit(UnitOfTemperatureDifference.Fahrenheit);

    /// <summary>Indicates whether the magnitude of the <see cref="TemperatureDifference"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="TemperatureDifference"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="TemperatureDifference"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="TemperatureDifference"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="TemperatureDifference"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="TemperatureDifference"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="TemperatureDifference"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="TemperatureDifference"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="TemperatureDifference"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public TemperatureDifference Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="TemperatureDifference"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public TemperatureDifference Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="TemperatureDifference"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public TemperatureDifference Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="TemperatureDifference"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public TemperatureDifference Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(TemperatureDifference other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="TemperatureDifference"/> (in SI units), and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [K]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="TemperatureDifference"/>,
    /// expressed in <paramref name="unitOfTemperatureDifference"/>.</summary>
    /// <param name="unitOfTemperatureDifference">The <see cref="UnitOfTemperatureDifference"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfTemperatureDifference unitOfTemperatureDifference) => InUnit(this, unitOfTemperatureDifference);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="TemperatureDifference"/>,
    /// expressed in <paramref name="unitOfTemperatureDifference"/>.</summary>
    /// <param name="temperatureDifference">The <see cref="TemperatureDifference"/> to be expressed in <paramref name="unitOfTemperatureDifference"/>.</param>
    /// <param name="unitOfTemperatureDifference">The <see cref="UnitOfTemperatureDifference"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(TemperatureDifference temperatureDifference, UnitOfTemperatureDifference unitOfTemperatureDifference) => new(temperatureDifference.Magnitude / unitOfTemperatureDifference.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="TemperatureDifference"/>.</summary>
    public TemperatureDifference Plus() => this;
    /// <summary>Negation, resulting in a <see cref="TemperatureDifference"/> with negated magnitude.</summary>
    public TemperatureDifference Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="TemperatureDifference"/>.</param>
    public static TemperatureDifference operator +(TemperatureDifference x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="TemperatureDifference"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="TemperatureDifference"/>.</param>
    public static TemperatureDifference operator -(TemperatureDifference x) => x.Negate();

    /// <summary>Multiplies the <see cref="TemperatureDifference"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="TemperatureDifference"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="TemperatureDifference"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="TemperatureDifference"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="TemperatureDifference"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="TemperatureDifference"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(TemperatureDifference x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="TemperatureDifference"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="TemperatureDifference"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="TemperatureDifference"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, TemperatureDifference y) => y.Multiply(x);
    /// <summary>Divides the <see cref="TemperatureDifference"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="TemperatureDifference"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(TemperatureDifference x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="TemperatureDifference"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public TemperatureDifference Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="TemperatureDifference"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="TemperatureDifference"/> is scaled.</param>
    public TemperatureDifference Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="TemperatureDifference"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="TemperatureDifference"/> is divided.</param>
    public TemperatureDifference Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="TemperatureDifference"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="TemperatureDifference"/> <paramref name="x"/> by this value.</param>
    public static TemperatureDifference operator %(TemperatureDifference x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="TemperatureDifference"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="TemperatureDifference"/> <paramref name="x"/>.</param>
    public static TemperatureDifference operator *(TemperatureDifference x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="TemperatureDifference"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="TemperatureDifference"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TemperatureDifference"/>, which is scaled by <paramref name="x"/>.</param>
    public static TemperatureDifference operator *(double x, TemperatureDifference y) => y.Multiply(x);
    /// <summary>Scales the <see cref="TemperatureDifference"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="TemperatureDifference"/> <paramref name="x"/>.</param>
    public static TemperatureDifference operator /(TemperatureDifference x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="TemperatureDifference"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public TemperatureDifference Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="TemperatureDifference"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="TemperatureDifference"/> is scaled.</param>
    public TemperatureDifference Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="TemperatureDifference"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="TemperatureDifference"/> is divided.</param>
    public TemperatureDifference Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="TemperatureDifference"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="TemperatureDifference"/> <paramref name="x"/> by this value.</param>
    public static TemperatureDifference operator %(TemperatureDifference x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="TemperatureDifference"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="TemperatureDifference"/> <paramref name="x"/>.</param>
    public static TemperatureDifference operator *(TemperatureDifference x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="TemperatureDifference"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="TemperatureDifference"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TemperatureDifference"/>, which is scaled by <paramref name="x"/>.</param>
    public static TemperatureDifference operator *(Scalar x, TemperatureDifference y) => y.Multiply(x);
    /// <summary>Scales the <see cref="TemperatureDifference"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="TemperatureDifference"/> <paramref name="x"/>.</param>
    public static TemperatureDifference operator /(TemperatureDifference x, Scalar y) => x.Divide(y);

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
    /// <summary>Multiples the <see cref="TemperatureDifference"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="TemperatureDifference"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity, Func{double, TProductScalarQuantity})"/>.</remarks>
    public static Unhandled operator *(TemperatureDifference x, IScalarQuantity y) => x.Multiply<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));
    /// <summary>Divides the <see cref="TemperatureDifference"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="TemperatureDifference"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity, Func{double, TQuotientScalarQuantity})"/>.</remarks>
    public static Unhandled operator /(TemperatureDifference x, IScalarQuantity y) => x.Divide<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(TemperatureDifference x, TemperatureDifference y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(TemperatureDifference x, TemperatureDifference y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(TemperatureDifference x, TemperatureDifference y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(TemperatureDifference x, TemperatureDifference y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="TemperatureDifference"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static implicit operator double(TemperatureDifference x) => x.ToDouble();

    /// <summary>Converts the <see cref="TemperatureDifference"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(TemperatureDifference x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="TemperatureDifference"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static TemperatureDifference FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="TemperatureDifference"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator TemperatureDifference(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="TemperatureDifference"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static TemperatureDifference FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="TemperatureDifference"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator TemperatureDifference(Scalar x) => FromScalar(x);
}
