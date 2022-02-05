namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="GravitationalEnergy"/>, describes the <see cref="PotentialEnergy"/> of an object tied to the position of the object
/// in a gravitational field.
/// <para>
/// New instances of <see cref="GravitationalEnergy"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfEnergy"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="GravitationalEnergy"/> a = 3 * <see cref="GravitationalEnergy.OneJoule"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="GravitationalEnergy"/> d = <see cref="GravitationalEnergy.From(Weight, Distance)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="GravitationalEnergy"/> e = <see cref="PotentialEnergy.AsGravitationalEnergy"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfEnergy"/>.
/// </para>
/// </summary>
/// <remarks>
/// <see cref="GravitationalEnergy"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="Energy"/></term>
/// <description>Describes the capability to perform <see cref="Work"/>.</description>
/// </item>
/// <item>
/// <term><see cref="PotentialEnergy"/></term>
/// <description>Describes the <see cref="Energy"/> of an object tied to the position of the object, and internal state of the object.</description>
/// </item>
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

    /// <summary>The <see cref="GravitationalEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Joule"/>.</summary>
    public static GravitationalEnergy OneJoule { get; } = new(1, UnitOfEnergy.Joule);
    /// <summary>The <see cref="GravitationalEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Kilojoule"/>.</summary>
    public static GravitationalEnergy OneKilojoule { get; } = new(1, UnitOfEnergy.Kilojoule);
    /// <summary>The <see cref="GravitationalEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Megajoule"/>.</summary>
    public static GravitationalEnergy OneMegajoule { get; } = new(1, UnitOfEnergy.Megajoule);
    /// <summary>The <see cref="GravitationalEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Gigajoule"/>.</summary>
    public static GravitationalEnergy OneGigajoule { get; } = new(1, UnitOfEnergy.Gigajoule);
    /// <summary>The <see cref="GravitationalEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.KilowattHour"/>.</summary>
    public static GravitationalEnergy OneKilowattHour { get; } = new(1, UnitOfEnergy.KilowattHour);
    /// <summary>The <see cref="GravitationalEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Calorie"/>.</summary>
    public static GravitationalEnergy OneCalorie { get; } = new(1, UnitOfEnergy.Calorie);
    /// <summary>The <see cref="GravitationalEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Kilocalorie"/>.</summary>
    public static GravitationalEnergy OneKilocalorie { get; } = new(1, UnitOfEnergy.Kilocalorie);

    /// <summary>The magnitude of the <see cref="GravitationalEnergy"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="GravitationalEnergy.InJoules"/>.
    /// <para>This value should only be used (to maximize efficiency) when implementing mathematical operations with other quantities.</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="GravitationalEnergy"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfEnergy"/> <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="GravitationalEnergy"/>, in <see cref="UnitOfEnergy"/> <paramref name="unitOfEnergy"/>.</param>
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
    /// <summary>Constructs a new <see cref="GravitationalEnergy"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfEnergy"/> <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="GravitationalEnergy"/>, in <see cref="UnitOfEnergy"/> <paramref name="unitOfEnergy"/>.</param>
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
    public GravitationalEnergy(double magnitude, UnitOfEnergy unitOfEnergy) : this(magnitude * unitOfEnergy.Factor) { }
    /// <summary>Constructs a new <see cref="GravitationalEnergy"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="GravitationalEnergy"/>.</param>
    /// <remarks>Consider preffering a constructor that requires a <see cref="UnitOfEnergy"/> to be specified.</remarks>
    public GravitationalEnergy(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="GravitationalEnergy"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="GravitationalEnergy"/>.</param>
    /// <remarks>Consider preferring a constructor that requires a <see cref="UnitOfEnergy"/> to be specified.</remarks>
    public GravitationalEnergy(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="GravitationalEnergy"/> to an instance of the associated quantity <see cref="Energy"/>, of equal magnitude.</summary>
    public Energy AsEnergy => new(Magnitude);
    /// <summary>Converts the <see cref="GravitationalEnergy"/> to an instance of the associated quantity <see cref="PotentialEnergy"/>, of equal magnitude.</summary>
    public PotentialEnergy AsPotentialEnergy => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="GravitationalEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Joule"/>.</summary>
    public Scalar InJoules => InUnit(UnitOfEnergy.Joule);
    /// <summary>Retrieves the magnitude of the <see cref="GravitationalEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Kilojoule"/>.</summary>
    public Scalar InKilojoules => InUnit(UnitOfEnergy.Kilojoule);
    /// <summary>Retrieves the magnitude of the <see cref="GravitationalEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Megajoule"/>.</summary>
    public Scalar InMegajoules => InUnit(UnitOfEnergy.Megajoule);
    /// <summary>Retrieves the magnitude of the <see cref="GravitationalEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Gigajoule"/>.</summary>
    public Scalar InGigajoules => InUnit(UnitOfEnergy.Gigajoule);

    /// <summary>Retrieves the magnitude of the <see cref="GravitationalEnergy"/>, expressed in unit <see cref="UnitOfEnergy.KilowattHour"/>.</summary>
    public Scalar InKilowattHours => InUnit(UnitOfEnergy.KilowattHour);
    /// <summary>Retrieves the magnitude of the <see cref="GravitationalEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Calorie"/>.</summary>
    public Scalar InCalories => InUnit(UnitOfEnergy.Calorie);
    /// <summary>Retrieves the magnitude of the <see cref="GravitationalEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Kilocalorie"/>.</summary>
    public Scalar InKilocalories => InUnit(UnitOfEnergy.Kilocalorie);

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

    /// <summary>Produces a <see cref="GravitationalEnergy"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public GravitationalEnergy Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="GravitationalEnergy"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public GravitationalEnergy Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="GravitationalEnergy"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public GravitationalEnergy Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="GravitationalEnergy"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public GravitationalEnergy Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(GravitationalEnergy other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="GravitationalEnergy"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [J]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="GravitationalEnergy"/>, expressed in <see cref="UnitOfEnergy"/>
    /// <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="unitOfEnergy">The <see cref="UnitOfEnergy"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfEnergy unitOfEnergy) => InUnit(this, unitOfEnergy);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="GravitationalEnergy"/>, expressed in <see cref="UnitOfEnergy"/>
    /// <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="gravitationalEnergy">The <see cref="GravitationalEnergy"/> to be expressed in <see cref="UnitOfEnergy"/> <paramref name="unitOfEnergy"/>.</param>
    /// <param name="unitOfEnergy">The <see cref="UnitOfEnergy"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(GravitationalEnergy gravitationalEnergy, UnitOfEnergy unitOfEnergy) => new(gravitationalEnergy.Magnitude / unitOfEnergy.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="GravitationalEnergy"/>.</summary>
    public GravitationalEnergy Plus() => this;
    /// <summary>Negation, resulting in a <see cref="GravitationalEnergy"/> with negated magnitude.</summary>
    public GravitationalEnergy Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="GravitationalEnergy"/>.</param>
    public static GravitationalEnergy operator +(GravitationalEnergy x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="GravitationalEnergy"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="GravitationalEnergy"/>.</param>
    public static GravitationalEnergy operator -(GravitationalEnergy x) => x.Negate();

    /// <summary>Multiplies the <see cref="GravitationalEnergy"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="GravitationalEnergy"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="GravitationalEnergy"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="GravitationalEnergy"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="GravitationalEnergy"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="GravitationalEnergy"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="GravitationalEnergy"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(GravitationalEnergy x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="GravitationalEnergy"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="GravitationalEnergy"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="GravitationalEnergy"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, GravitationalEnergy y) => y.Multiply(x);
    /// <summary>Divides the <see cref="GravitationalEnergy"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="GravitationalEnergy"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="GravitationalEnergy"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(GravitationalEnergy x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="GravitationalEnergy"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public GravitationalEnergy Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="GravitationalEnergy"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="GravitationalEnergy"/> is scaled.</param>
    public GravitationalEnergy Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="GravitationalEnergy"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="GravitationalEnergy"/> is divided.</param>
    public GravitationalEnergy Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="GravitationalEnergy"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalEnergy"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="GravitationalEnergy"/> <paramref name="x"/> by this value.</param>
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

    /// <summary>Produces a <see cref="GravitationalEnergy"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public GravitationalEnergy Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="GravitationalEnergy"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="GravitationalEnergy"/> is scaled.</param>
    public GravitationalEnergy Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="GravitationalEnergy"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="GravitationalEnergy"/> is divided.</param>
    public GravitationalEnergy Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="GravitationalEnergy"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalEnergy"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="GravitationalEnergy"/> <paramref name="x"/> by this value.</param>
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

    /// <summary>Multiplies the <see cref="GravitationalEnergy"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="GravitationalEnergy"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="GravitationalEnergy"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="GravitationalEnergy"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="GravitationalEnergy"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="GravitationalEnergy"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="GravitationalEnergy"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="GravitationalEnergy.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(GravitationalEnergy x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="GravitationalEnergy"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="GravitationalEnergy"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="GravitationalEnergy"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="GravitationalEnergy.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(GravitationalEnergy x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(GravitationalEnergy x, GravitationalEnergy y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(GravitationalEnergy x, GravitationalEnergy y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(GravitationalEnergy x, GravitationalEnergy y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(GravitationalEnergy x, GravitationalEnergy y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="GravitationalEnergy"/> to a <see cref="double"/> with value <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts the <see cref="GravitationalEnergy"/> to a <see cref="double"/> based on the magnitude of the <see cref="GravitationalEnergy"/> <paramref name="x"/>.</summary>
    public static implicit operator double(GravitationalEnergy x) => x.ToDouble();

    /// <summary>Converts the <see cref="GravitationalEnergy"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts the <see cref="GravitationalEnergy"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(GravitationalEnergy x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="GravitationalEnergy"/> of magnitude <paramref name="x"/>.</summary>
    public static GravitationalEnergy FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="GravitationalEnergy"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator GravitationalEnergy(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="GravitationalEnergy"/> of equivalent magnitude.</summary>
    public static GravitationalEnergy FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="GravitationalEnergy"/> of equivalent magnitude.</summary>
    public static explicit operator GravitationalEnergy(Scalar x) => FromScalar(x);
}
