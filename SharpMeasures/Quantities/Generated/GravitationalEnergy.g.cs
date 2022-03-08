#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="GravitationalEnergy"/>, describes the <see cref="PotentialEnergy"/> of an object tied to the position of the object
/// in a gravitational field. The quantity is expressed in <see cref="UnitOfEnergy"/>, with the SI unit being [J].
/// <para>
/// New instances of <see cref="GravitationalEnergy"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfEnergy"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code><see cref="GravitationalEnergy"/> a = 3 * <see cref="GravitationalEnergy.OneJoule"/>;</code>
/// </item>
/// <item>
/// <code><see cref="GravitationalEnergy"/> d = <see cref="GravitationalEnergy.From(Weight, Distance)"/>;</code>
/// </item>
/// <item>
/// <code>
/// <see cref="GravitationalEnergy"/> e = <see cref="PotentialEnergy.AsGravitationalEnergy"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="GravitationalEnergy"/> can be retrieved in the desired <see cref="UnitOfEnergy"/> using pre-defined properties,
/// such as <see cref="Joules"/>
/// </para>
/// </summary>
/// <remarks>
/// <see cref="GravitationalEnergy"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="Energy"/></term>
/// <description>Describes any type of energy.</description>
/// </item>
/// <item>
/// <term><see cref="PotentialEnergy"/></term>
/// <description>Describes the <see cref="Energy"/> of an object tied to the position of the object, and internal state of the object.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct GravitationalEnergy :
    IComparable<GravitationalEnergy>,
    IScalarQuantity,
    IScalableScalarQuantity<GravitationalEnergy>,
    IMultiplicableScalarQuantity<GravitationalEnergy, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<GravitationalEnergy, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="GravitationalEnergy"/>.</summary>
    public static GravitationalEnergy Zero { get; } = new(0);

    /// <summary>The <see cref="GravitationalEnergy"/> of magnitude 1, when expressed in <see cref="UnitOfEnergy.Joule"/>.</summary>
    public static GravitationalEnergy OneJoule { get; } = new(1, UnitOfEnergy.Joule);
    /// <summary>The <see cref="GravitationalEnergy"/> of magnitude 1, when expressed in <see cref="UnitOfEnergy.Kilojoule"/>.</summary>
    public static GravitationalEnergy OneKilojoule { get; } = new(1, UnitOfEnergy.Kilojoule);
    /// <summary>The <see cref="GravitationalEnergy"/> of magnitude 1, when expressed in <see cref="UnitOfEnergy.Megajoule"/>.</summary>
    public static GravitationalEnergy OneMegajoule { get; } = new(1, UnitOfEnergy.Megajoule);
    /// <summary>The <see cref="GravitationalEnergy"/> of magnitude 1, when expressed in <see cref="UnitOfEnergy.Gigajoule"/>.</summary>
    public static GravitationalEnergy OneGigajoule { get; } = new(1, UnitOfEnergy.Gigajoule);
    /// <summary>The <see cref="GravitationalEnergy"/> of magnitude 1, when expressed in <see cref="UnitOfEnergy.KilowattHour"/>.</summary>
    public static GravitationalEnergy OneKilowattHour { get; } = new(1, UnitOfEnergy.KilowattHour);
    /// <summary>The <see cref="GravitationalEnergy"/> of magnitude 1, when expressed in <see cref="UnitOfEnergy.Calorie"/>.</summary>
    public static GravitationalEnergy OneCalorie { get; } = new(1, UnitOfEnergy.Calorie);
    /// <summary>The <see cref="GravitationalEnergy"/> of magnitude 1, when expressed in <see cref="UnitOfEnergy.Kilocalorie"/>.</summary>
    public static GravitationalEnergy OneKilocalorie { get; } = new(1, UnitOfEnergy.Kilocalorie);

    /// <summary>The magnitude of the <see cref="GravitationalEnergy"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfEnergy)"/> or a pre-defined property
    /// - such as <see cref="Joules"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="GravitationalEnergy"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="GravitationalEnergy"/>, expressed in <paramref name="unitOfEnergy"/>.</param>
    /// <param name="unitOfEnergy">The <see cref="UnitOfEnergy"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="GravitationalEnergy"/> a = 3 * <see cref="GravitationalEnergy.OneJoule"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public GravitationalEnergy(Scalar magnitude, UnitOfEnergy unitOfEnergy) : this(magnitude.Magnitude, unitOfEnergy) { }
    /// <summary>Constructs a new <see cref="GravitationalEnergy"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="GravitationalEnergy"/>, expressed in <paramref name="unitOfEnergy"/>.</param>
    /// <param name="unitOfEnergy">The <see cref="UnitOfEnergy"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="GravitationalEnergy"/> a = 3 * <see cref="GravitationalEnergy.OneJoule"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public GravitationalEnergy(double magnitude, UnitOfEnergy unitOfEnergy) : this(magnitude * unitOfEnergy.Energy.Magnitude) { }
    /// <summary>Constructs a new <see cref="GravitationalEnergy"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="GravitationalEnergy"/>.</param>
    /// <remarks>Consider preferring <see cref="GravitationalEnergy(Scalar, UnitOfEnergy)"/>.</remarks>
    public GravitationalEnergy(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="GravitationalEnergy"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="GravitationalEnergy"/>.</param>
    /// <remarks>Consider preferring <see cref="GravitationalEnergy(double, UnitOfEnergy)"/>.</remarks>
    public GravitationalEnergy(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="GravitationalEnergy"/> to an instance of the associated quantity <see cref="Energy"/>, of equal magnitude.</summary>
    public Energy AsEnergy => new(Magnitude);
    /// <summary>Converts the <see cref="GravitationalEnergy"/> to an instance of the associated quantity <see cref="PotentialEnergy"/>, of equal magnitude.</summary>
    public PotentialEnergy AsPotentialEnergy => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="GravitationalEnergy"/>, expressed in <see cref="UnitOfEnergy.Joule"/>.</summary>
    public Scalar Joules => InUnit(UnitOfEnergy.Joule);
    /// <summary>Retrieves the magnitude of the <see cref="GravitationalEnergy"/>, expressed in <see cref="UnitOfEnergy.Kilojoule"/>.</summary>
    public Scalar Kilojoules => InUnit(UnitOfEnergy.Kilojoule);
    /// <summary>Retrieves the magnitude of the <see cref="GravitationalEnergy"/>, expressed in <see cref="UnitOfEnergy.Megajoule"/>.</summary>
    public Scalar Megajoules => InUnit(UnitOfEnergy.Megajoule);
    /// <summary>Retrieves the magnitude of the <see cref="GravitationalEnergy"/>, expressed in <see cref="UnitOfEnergy.Gigajoule"/>.</summary>
    public Scalar Gigajoules => InUnit(UnitOfEnergy.Gigajoule);
    /// <summary>Retrieves the magnitude of the <see cref="GravitationalEnergy"/>, expressed in <see cref="UnitOfEnergy.KilowattHour"/>.</summary>
    public Scalar KilowattHours => InUnit(UnitOfEnergy.KilowattHour);
    /// <summary>Retrieves the magnitude of the <see cref="GravitationalEnergy"/>, expressed in <see cref="UnitOfEnergy.Calorie"/>.</summary>
    public Scalar Calories => InUnit(UnitOfEnergy.Calorie);
    /// <summary>Retrieves the magnitude of the <see cref="GravitationalEnergy"/>, expressed in <see cref="UnitOfEnergy.Kilocalorie"/>.</summary>
    public Scalar Kilocalories => InUnit(UnitOfEnergy.Kilocalorie);

    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalEnergy"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalEnergy"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalEnergy"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalEnergy"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalEnergy"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalEnergy"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalEnergy"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalEnergy"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="GravitationalEnergy"/>.</summary>
    public GravitationalEnergy Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="GravitationalEnergy"/>.</summary>
    public GravitationalEnergy Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="GravitationalEnergy"/>.</summary>
    public GravitationalEnergy Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="GravitationalEnergy"/> to the nearest integer value.</summary>
    public GravitationalEnergy Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(GravitationalEnergy other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="GravitationalEnergy"/> in the default unit
    /// <see cref="UnitOfEnergy.Joule"/>, followed by the symbol [J].</summary>
    public override string ToString() => $"{Joules} [J]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="GravitationalEnergy"/>,
    /// expressed in <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="unitOfEnergy">The <see cref="UnitOfEnergy"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfEnergy unitOfEnergy) => InUnit(this, unitOfEnergy);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="GravitationalEnergy"/>,
    /// expressed in <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="gravitationalEnergy">The <see cref="GravitationalEnergy"/> to be expressed in <paramref name="unitOfEnergy"/>.</param>
    /// <param name="unitOfEnergy">The <see cref="UnitOfEnergy"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(GravitationalEnergy gravitationalEnergy, UnitOfEnergy unitOfEnergy) => new(gravitationalEnergy.Magnitude / unitOfEnergy.Energy.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="GravitationalEnergy"/>.</summary>
    public GravitationalEnergy Plus() => this;
    /// <summary>Negation, resulting in a <see cref="GravitationalEnergy"/> with negated magnitude.</summary>
    public GravitationalEnergy Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="GravitationalEnergy"/>.</param>
    public static GravitationalEnergy operator +(GravitationalEnergy x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="GravitationalEnergy"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="GravitationalEnergy"/>.</param>
    public static GravitationalEnergy operator -(GravitationalEnergy x) => x.Negate();

    /// <summary>Multiplicates the <see cref="GravitationalEnergy"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="GravitationalEnergy"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="GravitationalEnergy"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="GravitationalEnergy"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="GravitationalEnergy"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="GravitationalEnergy"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="GravitationalEnergy"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(GravitationalEnergy x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="GravitationalEnergy"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="GravitationalEnergy"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="GravitationalEnergy"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, GravitationalEnergy y) => y.Multiply(x);
    /// <summary>Division of the <see cref="GravitationalEnergy"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="GravitationalEnergy"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="GravitationalEnergy"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(GravitationalEnergy x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="GravitationalEnergy"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="GravitationalEnergy"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="GravitationalEnergy"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, GravitationalEnergy y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="GravitationalEnergy"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public GravitationalEnergy Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="GravitationalEnergy"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="GravitationalEnergy"/> is scaled.</param>
    public GravitationalEnergy Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="GravitationalEnergy"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="GravitationalEnergy"/> is divided.</param>
    public GravitationalEnergy Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalEnergy"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="GravitationalEnergy"/> <paramref name="x"/> by this value.</param>
    public static GravitationalEnergy operator %(GravitationalEnergy x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="GravitationalEnergy"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalEnergy"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="GravitationalEnergy"/> <paramref name="x"/>.</param>
    public static GravitationalEnergy operator *(GravitationalEnergy x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="GravitationalEnergy"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="GravitationalEnergy"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="GravitationalEnergy"/>, which is scaled by <paramref name="x"/>.</param>
    public static GravitationalEnergy operator *(double x, GravitationalEnergy y) => y.Multiply(x);
    /// <summary>Scales the <see cref="GravitationalEnergy"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalEnergy"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="GravitationalEnergy"/> <paramref name="x"/>.</param>
    public static GravitationalEnergy operator /(GravitationalEnergy x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="GravitationalEnergy"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public GravitationalEnergy Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="GravitationalEnergy"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="GravitationalEnergy"/> is scaled.</param>
    public GravitationalEnergy Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="GravitationalEnergy"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="GravitationalEnergy"/> is divided.</param>
    public GravitationalEnergy Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalEnergy"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="GravitationalEnergy"/> <paramref name="x"/> by this value.</param>
    public static GravitationalEnergy operator %(GravitationalEnergy x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="GravitationalEnergy"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalEnergy"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="GravitationalEnergy"/> <paramref name="x"/>.</param>
    public static GravitationalEnergy operator *(GravitationalEnergy x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="GravitationalEnergy"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="GravitationalEnergy"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="GravitationalEnergy"/>, which is scaled by <paramref name="x"/>.</param>
    public static GravitationalEnergy operator *(Scalar x, GravitationalEnergy y) => y.Multiply(x);
    /// <summary>Scales the <see cref="GravitationalEnergy"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalEnergy"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="GravitationalEnergy"/> <paramref name="x"/>.</param>
    public static GravitationalEnergy operator /(GravitationalEnergy x, Scalar y) => x.Divide(y);

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

    /// <summary>Multiplication of the <see cref="GravitationalEnergy"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="GravitationalEnergy"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="GravitationalEnergy"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(GravitationalEnergy x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="GravitationalEnergy"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="GravitationalEnergy"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="GravitationalEnergy"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(GravitationalEnergy x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="GravitationalEnergy"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="GravitationalEnergy"/>.</param>
    public static bool operator <(GravitationalEnergy x, GravitationalEnergy y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="GravitationalEnergy"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="GravitationalEnergy"/>.</param>
    public static bool operator >(GravitationalEnergy x, GravitationalEnergy y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="GravitationalEnergy"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="GravitationalEnergy"/>.</param>
    public static bool operator <=(GravitationalEnergy x, GravitationalEnergy y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="GravitationalEnergy"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="GravitationalEnergy"/>.</param>
    public static bool operator >=(GravitationalEnergy x, GravitationalEnergy y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="GravitationalEnergy"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(GravitationalEnergy x) => x.ToDouble();

    /// <summary>Converts the <see cref="GravitationalEnergy"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(GravitationalEnergy x) => x.ToScalar();

    /// <summary>Constructs the <see cref="GravitationalEnergy"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static GravitationalEnergy FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="GravitationalEnergy"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator GravitationalEnergy(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="GravitationalEnergy"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static GravitationalEnergy FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="GravitationalEnergy"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator GravitationalEnergy(Scalar x) => FromScalar(x);
}
