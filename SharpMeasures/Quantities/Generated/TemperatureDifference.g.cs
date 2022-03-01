#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="TemperatureDifference"/>, describing a change in <see cref="Temperature"/>.
/// The quantity is expressed in <see cref="UnitOfTemperature"/>, with the SI unit being [K].
/// <para>
/// New instances of <see cref="TemperatureDifference"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfTemperature"/>. Instances can also be produced by combining other quantities, either through mathematical operators
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
/// <see cref="TemperatureDifference"/> d = <see cref="TemperatureDifference.From(Temperature, Temperature)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="TemperatureDifference"/> e = <see cref="Temperature.AsTemperatureDifference"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="TemperatureDifference"/> can be retrieved in the desired <see cref="UnitOfTemperature"/> using pre-defined properties,
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

    /// <summary>The <see cref="TemperatureDifference"/> of magnitude 1, when expressed in <see cref="UnitOfTemperature.Kelvin"/>.</summary>
    public static TemperatureDifference OneKelvin { get; } = UnitOfTemperature.Kelvin.TemperatureDifference;
    /// <summary>The <see cref="TemperatureDifference"/> of magnitude 1, when expressed in <see cref="UnitOfTemperature.Celsius"/>.</summary>
    public static TemperatureDifference OneCelsius { get; } = UnitOfTemperature.Celsius.TemperatureDifference;
    /// <summary>The <see cref="TemperatureDifference"/> of magnitude 1, when expressed in <see cref="UnitOfTemperature.Rankine"/>.</summary>
    public static TemperatureDifference OneRankine { get; } = UnitOfTemperature.Rankine.TemperatureDifference;
    /// <summary>The <see cref="TemperatureDifference"/> of magnitude 1, when expressed in <see cref="UnitOfTemperature.Fahrenheit"/>.</summary>
    public static TemperatureDifference OneFahrenheit { get; } = UnitOfTemperature.Fahrenheit.TemperatureDifference;

    /// <summary>The magnitude of the <see cref="TemperatureDifference"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfTemperature)"/> or a pre-defined property
    /// - such as <see cref="Kelvin"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="TemperatureDifference"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfTemperature"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TemperatureDifference"/>, expressed in <paramref name="unitOfTemperature"/>.</param>
    /// <param name="unitOfTemperature">The <see cref="UnitOfTemperature"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="TemperatureDifference"/> a = 3 * <see cref="TemperatureDifference.OneKelvin"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public TemperatureDifference(Scalar magnitude, UnitOfTemperature unitOfTemperature) : this(magnitude.Magnitude, unitOfTemperature) { }
    /// <summary>Constructs a new <see cref="TemperatureDifference"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfTemperature"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TemperatureDifference"/>, expressed in <paramref name="unitOfTemperature"/>.</param>
    /// <param name="unitOfTemperature">The <see cref="UnitOfTemperature"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="TemperatureDifference"/> a = 3 * <see cref="TemperatureDifference.OneKelvin"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public TemperatureDifference(double magnitude, UnitOfTemperature unitOfTemperature) : this(magnitude * unitOfTemperature.TemperatureDifference.Magnitude) { }
    /// <summary>Constructs a new <see cref="TemperatureDifference"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TemperatureDifference"/>.</param>
    /// <remarks>Consider preferring <see cref="TemperatureDifference(Scalar, UnitOfTemperature)"/>.</remarks>
    public TemperatureDifference(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="TemperatureDifference"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TemperatureDifference"/>.</param>
    /// <remarks>Consider preferring <see cref="TemperatureDifference(double, UnitOfTemperature)"/>.</remarks>
    public TemperatureDifference(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="TemperatureDifference"/> to an instance of the associated quantity <see cref="Temperature"/>, of equal magnitude.</summary>
    public Temperature AsTemperature => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="TemperatureDifference"/>, expressed in <see cref="UnitOfTemperature.Kelvin"/>.</summary>
    public Scalar Kelvin => InUnit(UnitOfTemperature.Kelvin);
    /// <summary>Retrieves the magnitude of the <see cref="TemperatureDifference"/>, expressed in <see cref="UnitOfTemperature.Celsius"/>.</summary>
    public Scalar Celsius => InUnit(UnitOfTemperature.Celsius);
    /// <summary>Retrieves the magnitude of the <see cref="TemperatureDifference"/>, expressed in <see cref="UnitOfTemperature.Rankine"/>.</summary>
    public Scalar Rankine => InUnit(UnitOfTemperature.Rankine);
    /// <summary>Retrieves the magnitude of the <see cref="TemperatureDifference"/>, expressed in <see cref="UnitOfTemperature.Fahrenheit"/>.</summary>
    public Scalar Fahrenheit => InUnit(UnitOfTemperature.Fahrenheit);

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

    /// <summary>Computes the absolute of the <see cref="TemperatureDifference"/>.</summary>
    public TemperatureDifference Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="TemperatureDifference"/>.</summary>
    public TemperatureDifference Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="TemperatureDifference"/>.</summary>
    public TemperatureDifference Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="TemperatureDifference"/> to the nearest integer value.</summary>
    public TemperatureDifference Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(TemperatureDifference other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="TemperatureDifference"/> in the default unit
    /// <see cref="UnitOfTemperature.Kelvin"/>, followed by the symbol [K].</summary>
    public override string ToString() => $"{Kelvin} [K]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="TemperatureDifference"/>,
    /// expressed in <paramref name="unitOfTemperature"/>.</summary>
    /// <param name="unitOfTemperature">The <see cref="UnitOfTemperature"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfTemperature unitOfTemperature) => InUnit(this, unitOfTemperature);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="TemperatureDifference"/>,
    /// expressed in <paramref name="unitOfTemperature"/>.</summary>
    /// <param name="temperatureDifference">The <see cref="TemperatureDifference"/> to be expressed in <paramref name="unitOfTemperature"/>.</param>
    /// <param name="unitOfTemperature">The <see cref="UnitOfTemperature"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(TemperatureDifference temperatureDifference, UnitOfTemperature unitOfTemperature) 
    	=> new(temperatureDifference.Magnitude / unitOfTemperature.TemperatureDifference.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="TemperatureDifference"/>.</summary>
    public TemperatureDifference Plus() => this;
    /// <summary>Negation, resulting in a <see cref="TemperatureDifference"/> with negated magnitude.</summary>
    public TemperatureDifference Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="TemperatureDifference"/>.</param>
    public static TemperatureDifference operator +(TemperatureDifference x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="TemperatureDifference"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="TemperatureDifference"/>.</param>
    public static TemperatureDifference operator -(TemperatureDifference x) => x.Negate();

    /// <summary>Multiplicates the <see cref="TemperatureDifference"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="TemperatureDifference"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="TemperatureDifference"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="TemperatureDifference"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="TemperatureDifference"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="TemperatureDifference"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(TemperatureDifference x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="TemperatureDifference"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="TemperatureDifference"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="TemperatureDifference"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, TemperatureDifference y) => y.Multiply(x);
    /// <summary>Division of the <see cref="TemperatureDifference"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="TemperatureDifference"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(TemperatureDifference x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="TemperatureDifference"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="TemperatureDifference"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TemperatureDifference"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, TemperatureDifference y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="TemperatureDifference"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public TemperatureDifference Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="TemperatureDifference"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="TemperatureDifference"/> is scaled.</param>
    public TemperatureDifference Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="TemperatureDifference"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="TemperatureDifference"/> is divided.</param>
    public TemperatureDifference Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="TemperatureDifference"/> <paramref name="x"/> by this value.</param>
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

    /// <summary>Computes the remainder from division of the <see cref="TemperatureDifference"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public TemperatureDifference Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="TemperatureDifference"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="TemperatureDifference"/> is scaled.</param>
    public TemperatureDifference Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="TemperatureDifference"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="TemperatureDifference"/> is divided.</param>
    public TemperatureDifference Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="TemperatureDifference"/> <paramref name="x"/> by this value.</param>
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

    /// <summary>Multiplication of the <see cref="TemperatureDifference"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="TemperatureDifference"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(TemperatureDifference x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="TemperatureDifference"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="TemperatureDifference"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(TemperatureDifference x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="TemperatureDifference"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="TemperatureDifference"/>.</param>
    public static bool operator <(TemperatureDifference x, TemperatureDifference y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="TemperatureDifference"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="TemperatureDifference"/>.</param>
    public static bool operator >(TemperatureDifference x, TemperatureDifference y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="TemperatureDifference"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="TemperatureDifference"/>.</param>
    public static bool operator <=(TemperatureDifference x, TemperatureDifference y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="TemperatureDifference"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="TemperatureDifference"/>.</param>
    public static bool operator >=(TemperatureDifference x, TemperatureDifference y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="TemperatureDifference"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(TemperatureDifference x) => x.ToDouble();

    /// <summary>Converts the <see cref="TemperatureDifference"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(TemperatureDifference x) => x.ToScalar();

    /// <summary>Constructs the <see cref="TemperatureDifference"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static TemperatureDifference FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="TemperatureDifference"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator TemperatureDifference(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="TemperatureDifference"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static TemperatureDifference FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="TemperatureDifference"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator TemperatureDifference(Scalar x) => FromScalar(x);
}
