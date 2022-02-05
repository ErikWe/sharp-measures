namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="TranslationalKineticEnergy"/>, describing the <see cref="Energy"/> of an object tied to the
/// translational motion of the object.
/// <para>
/// New instances of <see cref="TranslationalKineticEnergy"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfEnergy"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="TranslationalKineticEnergy"/> a = 3 * <see cref="TranslationalKineticEnergy.OneJoule"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="TranslationalKineticEnergy"/> d = <see cref="TranslationalKineticEnergy.From(Mass, Speed)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="TranslationalKineticEnergy"/> e = <see cref="KineticEnergy.AsTranslationalKineticEnergy"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfEnergy"/>.
/// </para>
/// </summary>
/// <remarks>
/// <see cref="TranslationalKineticEnergy"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="Energy"/></term>
/// <description>Describes the capability to perform <see cref="Work"/>.</description>
/// </item>
/// <item>
/// <term><see cref="KineticEnergy"/></term>
/// <description>Describes the <see cref="Energy"/> of an object tied to the position of the object, and internal states of the object.</description>
/// </item>
/// <item>
/// <term><see cref="RotationalKineticEnergy"/></term>
/// <description>Describes the <see cref="TranslationalKineticEnergy"/> of an object tied to the rotational motion of the object.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct TranslationalKineticEnergy :
    IComparable<TranslationalKineticEnergy>,
    IScalarQuantity,
    IScalableScalarQuantity<TranslationalKineticEnergy>,
    IMultiplicableScalarQuantity<TranslationalKineticEnergy, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<TranslationalKineticEnergy, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="TranslationalKineticEnergy"/>.</summary>
    public static TranslationalKineticEnergy Zero { get; } = new(0);

    /// <summary>The <see cref="TranslationalKineticEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Joule"/>.</summary>
    public static TranslationalKineticEnergy OneJoule { get; } = new(1, UnitOfEnergy.Joule);
    /// <summary>The <see cref="TranslationalKineticEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Kilojoule"/>.</summary>
    public static TranslationalKineticEnergy OneKilojoule { get; } = new(1, UnitOfEnergy.Kilojoule);
    /// <summary>The <see cref="TranslationalKineticEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Megajoule"/>.</summary>
    public static TranslationalKineticEnergy OneMegajoule { get; } = new(1, UnitOfEnergy.Megajoule);
    /// <summary>The <see cref="TranslationalKineticEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Gigajoule"/>.</summary>
    public static TranslationalKineticEnergy OneGigajoule { get; } = new(1, UnitOfEnergy.Gigajoule);
    /// <summary>The <see cref="TranslationalKineticEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.KilowattHour"/>.</summary>
    public static TranslationalKineticEnergy OneKilowattHour { get; } = new(1, UnitOfEnergy.KilowattHour);
    /// <summary>The <see cref="TranslationalKineticEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Calorie"/>.</summary>
    public static TranslationalKineticEnergy OneCalorie { get; } = new(1, UnitOfEnergy.Calorie);
    /// <summary>The <see cref="TranslationalKineticEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Kilocalorie"/>.</summary>
    public static TranslationalKineticEnergy OneKilocalorie { get; } = new(1, UnitOfEnergy.Kilocalorie);

    /// <summary>The magnitude of the <see cref="TranslationalKineticEnergy"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="TranslationalKineticEnergy.InJoules"/>.
    /// <para>This value should only be used (to maximize efficiency) when implementing mathematical operations with other quantities.</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="TranslationalKineticEnergy"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfEnergy"/> <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TranslationalKineticEnergy"/>, in <see cref="UnitOfEnergy"/> <paramref name="unitOfEnergy"/>.</param>
    /// <param name="unitOfEnergy">The <see cref="UnitOfEnergy"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="TranslationalKineticEnergy"/> a = 3 * <see cref="TranslationalKineticEnergy.OneJoule"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public TranslationalKineticEnergy(Scalar magnitude, UnitOfEnergy unitOfEnergy) : this(magnitude.Magnitude, unitOfEnergy) { }
    /// <summary>Constructs a new <see cref="TranslationalKineticEnergy"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfEnergy"/> <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TranslationalKineticEnergy"/>, in <see cref="UnitOfEnergy"/> <paramref name="unitOfEnergy"/>.</param>
    /// <param name="unitOfEnergy">The <see cref="UnitOfEnergy"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="TranslationalKineticEnergy"/> a = 3 * <see cref="TranslationalKineticEnergy.OneJoule"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public TranslationalKineticEnergy(double magnitude, UnitOfEnergy unitOfEnergy) : this(magnitude * unitOfEnergy.Factor) { }
    /// <summary>Constructs a new <see cref="TranslationalKineticEnergy"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TranslationalKineticEnergy"/>.</param>
    /// <remarks>Consider preffering a constructor that requires a <see cref="UnitOfEnergy"/> to be specified.</remarks>
    public TranslationalKineticEnergy(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="TranslationalKineticEnergy"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TranslationalKineticEnergy"/>.</param>
    /// <remarks>Consider preferring a constructor that requires a <see cref="UnitOfEnergy"/> to be specified.</remarks>
    public TranslationalKineticEnergy(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="TranslationalKineticEnergy"/> to an instance of the associated quantity <see cref="Energy"/>, of equal magnitude.</summary>
    public Energy AsEnergy => new(Magnitude);
    /// <summary>Converts the <see cref="TranslationalKineticEnergy"/> to an instance of the associated quantity <see cref="KineticEnergy"/>, of equal magnitude.</summary>
    public KineticEnergy AsKineticEnergy => new(Magnitude);
    /// <summary>Converts the <see cref="TranslationalKineticEnergy"/> to an instance of the associated quantity <see cref="RotationalKineticEnergy"/>, of equal magnitude.</summary>
    public RotationalKineticEnergy AsRotationalKineticEnergy => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="TranslationalKineticEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Joule"/>.</summary>
    public Scalar InJoules => InUnit(UnitOfEnergy.Joule);
    /// <summary>Retrieves the magnitude of the <see cref="TranslationalKineticEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Kilojoule"/>.</summary>
    public Scalar InKilojoules => InUnit(UnitOfEnergy.Kilojoule);
    /// <summary>Retrieves the magnitude of the <see cref="TranslationalKineticEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Megajoule"/>.</summary>
    public Scalar InMegajoules => InUnit(UnitOfEnergy.Megajoule);
    /// <summary>Retrieves the magnitude of the <see cref="TranslationalKineticEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Gigajoule"/>.</summary>
    public Scalar InGigajoules => InUnit(UnitOfEnergy.Gigajoule);

    /// <summary>Retrieves the magnitude of the <see cref="TranslationalKineticEnergy"/>, expressed in unit <see cref="UnitOfEnergy.KilowattHour"/>.</summary>
    public Scalar InKilowattHours => InUnit(UnitOfEnergy.KilowattHour);
    /// <summary>Retrieves the magnitude of the <see cref="TranslationalKineticEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Calorie"/>.</summary>
    public Scalar InCalories => InUnit(UnitOfEnergy.Calorie);
    /// <summary>Retrieves the magnitude of the <see cref="TranslationalKineticEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Kilocalorie"/>.</summary>
    public Scalar InKilocalories => InUnit(UnitOfEnergy.Kilocalorie);

    /// <summary>Indicates whether the magnitude of the <see cref="TranslationalKineticEnergy"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="TranslationalKineticEnergy"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="TranslationalKineticEnergy"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="TranslationalKineticEnergy"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="TranslationalKineticEnergy"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="TranslationalKineticEnergy"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="TranslationalKineticEnergy"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="TranslationalKineticEnergy"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="TranslationalKineticEnergy"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public TranslationalKineticEnergy Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="TranslationalKineticEnergy"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public TranslationalKineticEnergy Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="TranslationalKineticEnergy"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public TranslationalKineticEnergy Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="TranslationalKineticEnergy"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public TranslationalKineticEnergy Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(TranslationalKineticEnergy other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="TranslationalKineticEnergy"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [J]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="TranslationalKineticEnergy"/>, expressed in <see cref="UnitOfEnergy"/>
    /// <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="unitOfEnergy">The <see cref="UnitOfEnergy"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfEnergy unitOfEnergy) => InUnit(this, unitOfEnergy);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="TranslationalKineticEnergy"/>, expressed in <see cref="UnitOfEnergy"/>
    /// <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="translationalKineticEnergy">The <see cref="TranslationalKineticEnergy"/> to be expressed in <see cref="UnitOfEnergy"/> <paramref name="unitOfEnergy"/>.</param>
    /// <param name="unitOfEnergy">The <see cref="UnitOfEnergy"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(TranslationalKineticEnergy translationalKineticEnergy, UnitOfEnergy unitOfEnergy) => new(translationalKineticEnergy.Magnitude / unitOfEnergy.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="TranslationalKineticEnergy"/>.</summary>
    public TranslationalKineticEnergy Plus() => this;
    /// <summary>Negation, resulting in a <see cref="TranslationalKineticEnergy"/> with negated magnitude.</summary>
    public TranslationalKineticEnergy Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="TranslationalKineticEnergy"/>.</param>
    public static TranslationalKineticEnergy operator +(TranslationalKineticEnergy x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="TranslationalKineticEnergy"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="TranslationalKineticEnergy"/>.</param>
    public static TranslationalKineticEnergy operator -(TranslationalKineticEnergy x) => x.Negate();

    /// <summary>Multiplies the <see cref="TranslationalKineticEnergy"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="TranslationalKineticEnergy"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="TranslationalKineticEnergy"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="TranslationalKineticEnergy"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="TranslationalKineticEnergy"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TranslationalKineticEnergy"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="TranslationalKineticEnergy"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(TranslationalKineticEnergy x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="TranslationalKineticEnergy"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="TranslationalKineticEnergy"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="TranslationalKineticEnergy"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, TranslationalKineticEnergy y) => y.Multiply(x);
    /// <summary>Divides the <see cref="TranslationalKineticEnergy"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TranslationalKineticEnergy"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="TranslationalKineticEnergy"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(TranslationalKineticEnergy x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="TranslationalKineticEnergy"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public TranslationalKineticEnergy Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="TranslationalKineticEnergy"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="TranslationalKineticEnergy"/> is scaled.</param>
    public TranslationalKineticEnergy Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="TranslationalKineticEnergy"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="TranslationalKineticEnergy"/> is divided.</param>
    public TranslationalKineticEnergy Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="TranslationalKineticEnergy"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TranslationalKineticEnergy"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="TranslationalKineticEnergy"/> <paramref name="x"/> by this value.</param>
    public static TranslationalKineticEnergy operator %(TranslationalKineticEnergy x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="TranslationalKineticEnergy"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TranslationalKineticEnergy"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="TranslationalKineticEnergy"/> <paramref name="x"/>.</param>
    public static TranslationalKineticEnergy operator *(TranslationalKineticEnergy x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="TranslationalKineticEnergy"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="TranslationalKineticEnergy"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TranslationalKineticEnergy"/>, which is scaled by <paramref name="x"/>.</param>
    public static TranslationalKineticEnergy operator *(double x, TranslationalKineticEnergy y) => y.Multiply(x);
    /// <summary>Scales the <see cref="TranslationalKineticEnergy"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TranslationalKineticEnergy"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="TranslationalKineticEnergy"/> <paramref name="x"/>.</param>
    public static TranslationalKineticEnergy operator /(TranslationalKineticEnergy x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="TranslationalKineticEnergy"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public TranslationalKineticEnergy Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="TranslationalKineticEnergy"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="TranslationalKineticEnergy"/> is scaled.</param>
    public TranslationalKineticEnergy Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="TranslationalKineticEnergy"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="TranslationalKineticEnergy"/> is divided.</param>
    public TranslationalKineticEnergy Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="TranslationalKineticEnergy"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TranslationalKineticEnergy"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="TranslationalKineticEnergy"/> <paramref name="x"/> by this value.</param>
    public static TranslationalKineticEnergy operator %(TranslationalKineticEnergy x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="TranslationalKineticEnergy"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TranslationalKineticEnergy"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="TranslationalKineticEnergy"/> <paramref name="x"/>.</param>
    public static TranslationalKineticEnergy operator *(TranslationalKineticEnergy x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="TranslationalKineticEnergy"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="TranslationalKineticEnergy"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TranslationalKineticEnergy"/>, which is scaled by <paramref name="x"/>.</param>
    public static TranslationalKineticEnergy operator *(Scalar x, TranslationalKineticEnergy y) => y.Multiply(x);
    /// <summary>Scales the <see cref="TranslationalKineticEnergy"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TranslationalKineticEnergy"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="TranslationalKineticEnergy"/> <paramref name="x"/>.</param>
    public static TranslationalKineticEnergy operator /(TranslationalKineticEnergy x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="TranslationalKineticEnergy"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="TranslationalKineticEnergy"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="TranslationalKineticEnergy"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="TranslationalKineticEnergy"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="TranslationalKineticEnergy"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TranslationalKineticEnergy"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="TranslationalKineticEnergy"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="TranslationalKineticEnergy.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(TranslationalKineticEnergy x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="TranslationalKineticEnergy"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TranslationalKineticEnergy"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="TranslationalKineticEnergy"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="TranslationalKineticEnergy.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(TranslationalKineticEnergy x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(TranslationalKineticEnergy x, TranslationalKineticEnergy y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(TranslationalKineticEnergy x, TranslationalKineticEnergy y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(TranslationalKineticEnergy x, TranslationalKineticEnergy y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(TranslationalKineticEnergy x, TranslationalKineticEnergy y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="TranslationalKineticEnergy"/> to a <see cref="double"/> with value <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts the <see cref="TranslationalKineticEnergy"/> to a <see cref="double"/> based on the magnitude of the <see cref="TranslationalKineticEnergy"/> <paramref name="x"/>.</summary>
    public static implicit operator double(TranslationalKineticEnergy x) => x.ToDouble();

    /// <summary>Converts the <see cref="TranslationalKineticEnergy"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts the <see cref="TranslationalKineticEnergy"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(TranslationalKineticEnergy x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="TranslationalKineticEnergy"/> of magnitude <paramref name="x"/>.</summary>
    public static TranslationalKineticEnergy FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="TranslationalKineticEnergy"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator TranslationalKineticEnergy(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="TranslationalKineticEnergy"/> of equivalent magnitude.</summary>
    public static TranslationalKineticEnergy FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="TranslationalKineticEnergy"/> of equivalent magnitude.</summary>
    public static explicit operator TranslationalKineticEnergy(Scalar x) => FromScalar(x);
}
