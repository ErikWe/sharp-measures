namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="PotentialEnergy"/>, describes the <see cref="Energy"/> of an object tied to the position of the object,
/// and the internal state of the object.
/// <para>
/// New instances of <see cref="PotentialEnergy"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfEnergy"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="PotentialEnergy"/> a = 3 * <see cref="PotentialEnergy.OneJoule"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="PotentialEnergy"/> d = <see cref="PotentialEnergy.From(PotentialEnergy, Work)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="PotentialEnergy"/> e = <see cref="Energy.AsPotentialEnergy"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfEnergy"/>.
/// </para>
/// </summary>
/// <remarks>
/// <see cref="PotentialEnergy"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="Energy"/></term>
/// <description>Describes the capability to perform <see cref="Work"/>.</description>
/// </item>
/// <item>
/// <term><see cref="GravitationalEnergy"/></term>
/// <description>Describes the <see cref="PotentialEnergy"/> of an object tied to the position of the object in a gravitational field.</description>
/// </item>
/// <item>
/// <term><see cref="KineticEnergy"/></term>
/// <description>Describes the <see cref="Energy"/> of an object tied to the motion of the object.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct PotentialEnergy :
    IComparable<PotentialEnergy>,
    IScalarQuantity,
    IScalableScalarQuantity<PotentialEnergy>,
    IMultiplicableScalarQuantity<PotentialEnergy, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<PotentialEnergy, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="PotentialEnergy"/>.</summary>
    public static PotentialEnergy Zero { get; } = new(0);

    /// <summary>The <see cref="PotentialEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Joule"/>.</summary>
    public static PotentialEnergy OneJoule { get; } = new(1, UnitOfEnergy.Joule);
    /// <summary>The <see cref="PotentialEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Kilojoule"/>.</summary>
    public static PotentialEnergy OneKilojoule { get; } = new(1, UnitOfEnergy.Kilojoule);
    /// <summary>The <see cref="PotentialEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Megajoule"/>.</summary>
    public static PotentialEnergy OneMegajoule { get; } = new(1, UnitOfEnergy.Megajoule);
    /// <summary>The <see cref="PotentialEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Gigajoule"/>.</summary>
    public static PotentialEnergy OneGigajoule { get; } = new(1, UnitOfEnergy.Gigajoule);
    /// <summary>The <see cref="PotentialEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.KilowattHour"/>.</summary>
    public static PotentialEnergy OneKilowattHour { get; } = new(1, UnitOfEnergy.KilowattHour);
    /// <summary>The <see cref="PotentialEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Calorie"/>.</summary>
    public static PotentialEnergy OneCalorie { get; } = new(1, UnitOfEnergy.Calorie);
    /// <summary>The <see cref="PotentialEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Kilocalorie"/>.</summary>
    public static PotentialEnergy OneKilocalorie { get; } = new(1, UnitOfEnergy.Kilocalorie);

    /// <summary>The magnitude of the <see cref="PotentialEnergy"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="PotentialEnergy.InJoules"/>.
    /// <para>This value should only be used (to maximize efficiency) when implementing mathematical operations with other quantities.</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="PotentialEnergy"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfEnergy"/> <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="PotentialEnergy"/>, in <see cref="UnitOfEnergy"/> <paramref name="unitOfEnergy"/>.</param>
    /// <param name="unitOfEnergy">The <see cref="UnitOfEnergy"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="PotentialEnergy"/> a = 3 * <see cref="PotentialEnergy.OneJoule"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public PotentialEnergy(Scalar magnitude, UnitOfEnergy unitOfEnergy) : this(magnitude.Magnitude, unitOfEnergy) { }
    /// <summary>Constructs a new <see cref="PotentialEnergy"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfEnergy"/> <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="PotentialEnergy"/>, in <see cref="UnitOfEnergy"/> <paramref name="unitOfEnergy"/>.</param>
    /// <param name="unitOfEnergy">The <see cref="UnitOfEnergy"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="PotentialEnergy"/> a = 3 * <see cref="PotentialEnergy.OneJoule"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public PotentialEnergy(double magnitude, UnitOfEnergy unitOfEnergy) : this(magnitude * unitOfEnergy.Factor) { }
    /// <summary>Constructs a new <see cref="PotentialEnergy"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="PotentialEnergy"/>.</param>
    /// <remarks>Consider preffering a constructor that requires a <see cref="UnitOfEnergy"/> to be specified.</remarks>
    public PotentialEnergy(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="PotentialEnergy"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="PotentialEnergy"/>.</param>
    /// <remarks>Consider preferring a constructor that requires a <see cref="UnitOfEnergy"/> to be specified.</remarks>
    public PotentialEnergy(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="PotentialEnergy"/> to an instance of the associated quantity <see cref="Energy"/>, of equal magnitude.</summary>
    public Energy AsEnergy => new(Magnitude);
    /// <summary>Converts the <see cref="PotentialEnergy"/> to an instance of the associated quantity <see cref="GravitationalEnergy"/>, of equal magnitude.</summary>
    public GravitationalEnergy AsGravitationalEnergy => new(Magnitude);
    /// <summary>Converts the <see cref="PotentialEnergy"/> to an instance of the associated quantity <see cref="KineticEnergy"/>, of equal magnitude.</summary>
    public KineticEnergy AsKineticEnergy => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="PotentialEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Joule"/>.</summary>
    public Scalar InJoules => InUnit(UnitOfEnergy.Joule);
    /// <summary>Retrieves the magnitude of the <see cref="PotentialEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Kilojoule"/>.</summary>
    public Scalar InKilojoules => InUnit(UnitOfEnergy.Kilojoule);
    /// <summary>Retrieves the magnitude of the <see cref="PotentialEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Megajoule"/>.</summary>
    public Scalar InMegajoules => InUnit(UnitOfEnergy.Megajoule);
    /// <summary>Retrieves the magnitude of the <see cref="PotentialEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Gigajoule"/>.</summary>
    public Scalar InGigajoules => InUnit(UnitOfEnergy.Gigajoule);

    /// <summary>Retrieves the magnitude of the <see cref="PotentialEnergy"/>, expressed in unit <see cref="UnitOfEnergy.KilowattHour"/>.</summary>
    public Scalar InKilowattHours => InUnit(UnitOfEnergy.KilowattHour);
    /// <summary>Retrieves the magnitude of the <see cref="PotentialEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Calorie"/>.</summary>
    public Scalar InCalories => InUnit(UnitOfEnergy.Calorie);
    /// <summary>Retrieves the magnitude of the <see cref="PotentialEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Kilocalorie"/>.</summary>
    public Scalar InKilocalories => InUnit(UnitOfEnergy.Kilocalorie);

    /// <summary>Indicates whether the magnitude of the <see cref="PotentialEnergy"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="PotentialEnergy"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="PotentialEnergy"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="PotentialEnergy"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="PotentialEnergy"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="PotentialEnergy"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="PotentialEnergy"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="PotentialEnergy"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="PotentialEnergy"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public PotentialEnergy Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="PotentialEnergy"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public PotentialEnergy Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="PotentialEnergy"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public PotentialEnergy Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="PotentialEnergy"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public PotentialEnergy Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(PotentialEnergy other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="PotentialEnergy"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [J]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="PotentialEnergy"/>, expressed in <see cref="UnitOfEnergy"/>
    /// <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="unitOfEnergy">The <see cref="UnitOfEnergy"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfEnergy unitOfEnergy) => InUnit(this, unitOfEnergy);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="PotentialEnergy"/>, expressed in <see cref="UnitOfEnergy"/>
    /// <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="potentialEnergy">The <see cref="PotentialEnergy"/> to be expressed in <see cref="UnitOfEnergy"/> <paramref name="unitOfEnergy"/>.</param>
    /// <param name="unitOfEnergy">The <see cref="UnitOfEnergy"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(PotentialEnergy potentialEnergy, UnitOfEnergy unitOfEnergy) => new(potentialEnergy.Magnitude / unitOfEnergy.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="PotentialEnergy"/>.</summary>
    public PotentialEnergy Plus() => this;
    /// <summary>Negation, resulting in a <see cref="PotentialEnergy"/> with negated magnitude.</summary>
    public PotentialEnergy Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="PotentialEnergy"/>.</param>
    public static PotentialEnergy operator +(PotentialEnergy x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="PotentialEnergy"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="PotentialEnergy"/>.</param>
    public static PotentialEnergy operator -(PotentialEnergy x) => x.Negate();

    /// <summary>Multiplies the <see cref="PotentialEnergy"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="PotentialEnergy"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="PotentialEnergy"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="PotentialEnergy"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="PotentialEnergy"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="PotentialEnergy"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="PotentialEnergy"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(PotentialEnergy x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="PotentialEnergy"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="PotentialEnergy"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="PotentialEnergy"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, PotentialEnergy y) => y.Multiply(x);
    /// <summary>Divides the <see cref="PotentialEnergy"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="PotentialEnergy"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="PotentialEnergy"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(PotentialEnergy x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="PotentialEnergy"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public PotentialEnergy Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="PotentialEnergy"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="PotentialEnergy"/> is scaled.</param>
    public PotentialEnergy Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="PotentialEnergy"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="PotentialEnergy"/> is divided.</param>
    public PotentialEnergy Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="PotentialEnergy"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="PotentialEnergy"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="PotentialEnergy"/> <paramref name="x"/> by this value.</param>
    public static PotentialEnergy operator %(PotentialEnergy x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="PotentialEnergy"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="PotentialEnergy"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="PotentialEnergy"/> <paramref name="x"/>.</param>
    public static PotentialEnergy operator *(PotentialEnergy x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="PotentialEnergy"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="PotentialEnergy"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="PotentialEnergy"/>, which is scaled by <paramref name="x"/>.</param>
    public static PotentialEnergy operator *(double x, PotentialEnergy y) => y.Multiply(x);
    /// <summary>Scales the <see cref="PotentialEnergy"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="PotentialEnergy"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="PotentialEnergy"/> <paramref name="x"/>.</param>
    public static PotentialEnergy operator /(PotentialEnergy x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="PotentialEnergy"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public PotentialEnergy Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="PotentialEnergy"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="PotentialEnergy"/> is scaled.</param>
    public PotentialEnergy Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="PotentialEnergy"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="PotentialEnergy"/> is divided.</param>
    public PotentialEnergy Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="PotentialEnergy"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="PotentialEnergy"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="PotentialEnergy"/> <paramref name="x"/> by this value.</param>
    public static PotentialEnergy operator %(PotentialEnergy x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="PotentialEnergy"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="PotentialEnergy"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="PotentialEnergy"/> <paramref name="x"/>.</param>
    public static PotentialEnergy operator *(PotentialEnergy x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="PotentialEnergy"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="PotentialEnergy"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="PotentialEnergy"/>, which is scaled by <paramref name="x"/>.</param>
    public static PotentialEnergy operator *(Scalar x, PotentialEnergy y) => y.Multiply(x);
    /// <summary>Scales the <see cref="PotentialEnergy"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="PotentialEnergy"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="PotentialEnergy"/> <paramref name="x"/>.</param>
    public static PotentialEnergy operator /(PotentialEnergy x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="PotentialEnergy"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="PotentialEnergy"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="PotentialEnergy"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="PotentialEnergy"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="PotentialEnergy"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="PotentialEnergy"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="PotentialEnergy"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="PotentialEnergy.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(PotentialEnergy x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="PotentialEnergy"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="PotentialEnergy"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="PotentialEnergy"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="PotentialEnergy.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(PotentialEnergy x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(PotentialEnergy x, PotentialEnergy y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(PotentialEnergy x, PotentialEnergy y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(PotentialEnergy x, PotentialEnergy y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(PotentialEnergy x, PotentialEnergy y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="PotentialEnergy"/> to a <see cref="double"/> with value <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts the <see cref="PotentialEnergy"/> to a <see cref="double"/> based on the magnitude of the <see cref="PotentialEnergy"/> <paramref name="x"/>.</summary>
    public static implicit operator double(PotentialEnergy x) => x.ToDouble();

    /// <summary>Converts the <see cref="PotentialEnergy"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts the <see cref="PotentialEnergy"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(PotentialEnergy x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="PotentialEnergy"/> of magnitude <paramref name="x"/>.</summary>
    public static PotentialEnergy FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="PotentialEnergy"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator PotentialEnergy(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="PotentialEnergy"/> of equivalent magnitude.</summary>
    public static PotentialEnergy FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="PotentialEnergy"/> of equivalent magnitude.</summary>
    public static explicit operator PotentialEnergy(Scalar x) => FromScalar(x);
}
