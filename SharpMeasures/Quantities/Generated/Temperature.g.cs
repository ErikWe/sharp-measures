#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Temperature"/>.
/// The quantity is expressed in <see cref="UnitOfTemperature"/>, with the SI unit being [K]. This is different from <see cref="TemperatureDifference"/>, as
/// <see cref="UnitOfTemperature"/> often features a bias, such as for <see cref="UnitOfTemperature.Celsius"/>.
/// <para>
/// New instances of <see cref="Temperature"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfTemperature"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code><see cref="Temperature"/> a = 3 * <see cref="Temperature.OneKelvin"/>;</code>
/// </item>
/// <item>
/// <code><see cref="Temperature"/> d = <see cref="Temperature.From(Temperature, TemperatureDifference)"/>;</code>
/// </item>
/// <item>
/// <code>
/// <see cref="Temperature"/> e = <see cref="TemperatureDifference.AsTemperature"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="Temperature"/> can be retrieved in the desired <see cref="UnitOfTemperature"/> using pre-defined properties,
/// such as <see cref="Kelvin"/>
/// </para>
/// </summary>
/// <remarks>
/// <see cref="Temperature"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="TemperatureDifference"/></term>
/// <description>Describes a change in <see cref="Temperature"/>.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct Temperature :
    IComparable<Temperature>,
    IScalarQuantity,
    IScalableScalarQuantity<Temperature>,
    IMultiplicableScalarQuantity<Temperature, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Temperature, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The <see cref="Temperature"/> of magnitude 1, when expressed in <see cref="UnitOfTemperature.Kelvin"/>.</summary>
    public static Temperature OneKelvin { get; } = UnitOfTemperature.Kelvin.TemperatureDifference.AsTemperature;
    /// <summary>The <see cref="Temperature"/> of magnitude 1, when expressed in <see cref="UnitOfTemperature.Rankine"/>.</summary>
    public static Temperature OneRankine { get; } = UnitOfTemperature.Rankine.TemperatureDifference.AsTemperature;

    /// <summary>The magnitude of the <see cref="Temperature"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfTemperature)"/> or a pre-defined property
    /// - such as <see cref="Kelvin"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Temperature"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfTemperature"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Temperature"/>, expressed in <paramref name="unitOfTemperature"/>.</param>
    /// <param name="unitOfTemperature">The <see cref="UnitOfTemperature"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Temperature"/> a = 3 * <see cref="Temperature.OneKelvin"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Temperature(Scalar magnitude, UnitOfTemperature unitOfTemperature) : this(magnitude.Magnitude, unitOfTemperature) { }
    /// <summary>Constructs a new <see cref="Temperature"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfTemperature"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Temperature"/>, expressed in <paramref name="unitOfTemperature"/>.</param>
    /// <param name="unitOfTemperature">The <see cref="UnitOfTemperature"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Temperature"/> a = 3 * <see cref="Temperature.OneKelvin"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Temperature(double magnitude, UnitOfTemperature unitOfTemperature) : 
    	this((magnitude - unitOfTemperature.Offset) * unitOfTemperature.TemperatureDifference.Magnitude) { }
    /// <summary>Constructs a new <see cref="Temperature"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Temperature"/>.</param>
    /// <remarks>Consider preferring <see cref="Temperature(Scalar, UnitOfTemperature)"/>.</remarks>
    public Temperature(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Temperature"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Temperature"/>.</param>
    /// <remarks>Consider preferring <see cref="Temperature(double, UnitOfTemperature)"/>.</remarks>
    public Temperature(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="Temperature"/> to an instance of the associated quantity <see cref="TemperatureDifference"/>, of equal magnitude.</summary>
    public TemperatureDifference AsTemperatureDifference => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="Temperature"/>, expressed in <see cref="UnitOfTemperature.Kelvin"/>.</summary>
    public Scalar Kelvin => InUnit(UnitOfTemperature.Kelvin);
    /// <summary>Retrieves the magnitude of the <see cref="Temperature"/>, expressed in <see cref="UnitOfTemperature.Celsius"/>.</summary>
    public Scalar Celsius => InUnit(UnitOfTemperature.Celsius);
    /// <summary>Retrieves the magnitude of the <see cref="Temperature"/>, expressed in <see cref="UnitOfTemperature.Rankine"/>.</summary>
    public Scalar Rankine => InUnit(UnitOfTemperature.Rankine);
    /// <summary>Retrieves the magnitude of the <see cref="Temperature"/>, expressed in <see cref="UnitOfTemperature.Fahrenheit"/>.</summary>
    public Scalar Fahrenheit => InUnit(UnitOfTemperature.Fahrenheit);

    /// <summary>Indicates whether the magnitude of the <see cref="Temperature"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Temperature"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Temperature"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Temperature"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Temperature"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Temperature"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Temperature"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Temperature"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="Temperature"/>.</summary>
    public Temperature Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="Temperature"/>.</summary>
    public Temperature Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="Temperature"/>.</summary>
    public Temperature Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="Temperature"/> to the nearest integer value.</summary>
    public Temperature Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Temperature other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Temperature"/> in the default unit
    /// <see cref="UnitOfTemperature.Kelvin"/>, followed by the symbol [K].</summary>
    public override string ToString() => $"{Kelvin} [K]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Temperature"/>,
    /// expressed in <paramref name="unitOfTemperature"/>.</summary>
    /// <param name="unitOfTemperature">The <see cref="UnitOfTemperature"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfTemperature unitOfTemperature) => InUnit(this, unitOfTemperature);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Temperature"/>,
    /// expressed in <paramref name="unitOfTemperature"/>.</summary>
    /// <param name="temperature">The <see cref="Temperature"/> to be expressed in <paramref name="unitOfTemperature"/>.</param>
    /// <param name="unitOfTemperature">The <see cref="UnitOfTemperature"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Temperature temperature, UnitOfTemperature unitOfTemperature) 
    	=> new(temperature.Magnitude / unitOfTemperature.TemperatureDifference.Magnitude + unitOfTemperature.Offset);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Temperature"/>.</summary>
    public Temperature Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Temperature"/> with negated magnitude.</summary>
    public Temperature Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Temperature"/>.</param>
    public static Temperature operator +(Temperature x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Temperature"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Temperature"/>.</param>
    public static Temperature operator -(Temperature x) => x.Negate();

    /// <summary>Multiplicates the <see cref="Temperature"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Temperature"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Temperature"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Temperature"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Temperature"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Temperature"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Temperature x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Temperature"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Temperature"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Temperature"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Temperature y) => y.Multiply(x);
    /// <summary>Division of the <see cref="Temperature"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Temperature"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Temperature x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="Temperature"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="Temperature"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Temperature"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, Temperature y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="Temperature"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Temperature Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Temperature"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Temperature"/> is scaled.</param>
    public Temperature Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Temperature"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Temperature"/> is divided.</param>
    public Temperature Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Temperature"/> <paramref name="x"/> by this value.</param>
    public static Temperature operator %(Temperature x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Temperature"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Temperature"/> <paramref name="x"/>.</param>
    public static Temperature operator *(Temperature x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Temperature"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Temperature"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Temperature"/>, which is scaled by <paramref name="x"/>.</param>
    public static Temperature operator *(double x, Temperature y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Temperature"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Temperature"/> <paramref name="x"/>.</param>
    public static Temperature operator /(Temperature x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="Temperature"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Temperature Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Temperature"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Temperature"/> is scaled.</param>
    public Temperature Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Temperature"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Temperature"/> is divided.</param>
    public Temperature Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Temperature"/> <paramref name="x"/> by this value.</param>
    public static Temperature operator %(Temperature x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Temperature"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Temperature"/> <paramref name="x"/>.</param>
    public static Temperature operator *(Temperature x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Temperature"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Temperature"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Temperature"/>, which is scaled by <paramref name="x"/>.</param>
    public static Temperature operator *(Scalar x, Temperature y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Temperature"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Temperature"/> <paramref name="x"/>.</param>
    public static Temperature operator /(Temperature x, Scalar y) => x.Divide(y);

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

    /// <summary>Multiplication of the <see cref="Temperature"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Temperature"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Temperature x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="Temperature"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Temperature"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Temperature x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Temperature"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Temperature"/>.</param>
    public static bool operator <(Temperature x, Temperature y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Temperature"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Temperature"/>.</param>
    public static bool operator >(Temperature x, Temperature y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Temperature"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Temperature"/>.</param>
    public static bool operator <=(Temperature x, Temperature y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Temperature"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Temperature"/>.</param>
    public static bool operator >=(Temperature x, Temperature y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Temperature"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(Temperature x) => x.ToDouble();

    /// <summary>Converts the <see cref="Temperature"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Temperature x) => x.ToScalar();

    /// <summary>Constructs the <see cref="Temperature"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Temperature FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Temperature"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Temperature(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="Temperature"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static Temperature FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="Temperature"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator Temperature(Scalar x) => FromScalar(x);
}
