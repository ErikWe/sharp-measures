namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Momentum"/>, a property of an object with <see cref="Mass"/> in motion.
/// This is the magnitude of the vector quantity <see cref="Momentum3"/>, and is expressed in <see cref="UnitOfMomentum"/>, with the SI unit being [kg * m / s].
/// <para>
/// New instances of <see cref="Momentum"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfMomentum"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Momentum"/> a = 3 * <see cref="Momentum.OneKilogramMetrePerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="#Param:quantity"/> d = <see cref="Momentum.From(Mass, Speed)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Momentum"/> e = <see cref="Impulse.AsMomentum"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfMomentum"/>.
/// </para>
/// </summary>
/// <remarks>
/// <see cref="Momentum"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="Impulse"/></term>
/// <description>Describes a change in <see cref="Momentum"/>.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct Momentum :
    IComparable<Momentum>,
    IScalarQuantity,
    IScalableScalarQuantity<Momentum>,
    IMultiplicableScalarQuantity<Momentum, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Momentum, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<Momentum3, Vector3>
{
    /// <summary>The zero-valued <see cref="Momentum"/>.</summary>
    public static Momentum Zero { get; } = new(0);

    /// <summary>The <see cref="Momentum"/> with magnitude 1, when expressed in unit <see cref="UnitOfMomentum.KilogramMetrePerSecond"/>.</summary>
    public static Momentum OneKilogramMetrePerSecond { get; } = new(1, UnitOfMomentum.KilogramMetrePerSecond);

    /// <summary>The magnitude of the <see cref="Momentum"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Momentum.InKilogramMetresPerSecond"/>.
    /// <para>This value should only be used (to maximize efficiency) when implementing mathematical operations with other quantities.</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Momentum"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfMomentum"/> <paramref name="unitOfMomentum"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Momentum"/>, in <see cref="UnitOfMomentum"/> <paramref name="unitOfMomentum"/>.</param>
    /// <param name="unitOfMomentum">The <see cref="UnitOfMomentum"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Momentum"/> a = 3 * <see cref="Momentum.OneKilogramMetrePerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Momentum(Scalar magnitude, UnitOfMomentum unitOfMomentum) : this(magnitude.Magnitude, unitOfMomentum) { }
    /// <summary>Constructs a new <see cref="Momentum"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfMomentum"/> <paramref name="unitOfMomentum"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Momentum"/>, in <see cref="UnitOfMomentum"/> <paramref name="unitOfMomentum"/>.</param>
    /// <param name="unitOfMomentum">The <see cref="UnitOfMomentum"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Momentum"/> a = 3 * <see cref="Momentum.OneKilogramMetrePerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Momentum(double magnitude, UnitOfMomentum unitOfMomentum) : this(magnitude * unitOfMomentum.Factor) { }
    /// <summary>Constructs a new <see cref="Momentum"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Momentum"/>.</param>
    /// <remarks>Consider preffering a constructor that requires a <see cref="UnitOfMomentum"/> to be specified.</remarks>
    public Momentum(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Momentum"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Momentum"/>.</param>
    /// <remarks>Consider preferring a constructor that requires a <see cref="UnitOfMomentum"/> to be specified.</remarks>
    public Momentum(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="Momentum"/> to an instance of the associated quantity <see cref="Impulse"/>, of equal magnitude.</summary>
    public Impulse AsImpulse => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="Momentum"/>, expressed in unit <see cref="UnitOfMomentum.KilogramMetrePerSecond"/>.</summary>
    public Scalar InKilogramMetresPerSecond => InUnit(UnitOfMomentum.KilogramMetrePerSecond);

    /// <summary>Indicates whether the magnitude of the <see cref="Momentum"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Momentum"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Momentum"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Momentum"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Momentum"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Momentum"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Momentum"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Momentum"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="Momentum"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public Momentum Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="Momentum"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public Momentum Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="Momentum"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public Momentum Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="Momentum"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public Momentum Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Momentum other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Momentum"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [kg * m / s]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Momentum"/>, expressed in <see cref="UnitOfMomentum"/>
    /// <paramref name="unitOfMomentum"/>.</summary>
    /// <param name="unitOfMomentum">The <see cref="UnitOfMomentum"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfMomentum unitOfMomentum) => InUnit(this, unitOfMomentum);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Momentum"/>, expressed in <see cref="UnitOfMomentum"/>
    /// <paramref name="unitOfMomentum"/>.</summary>
    /// <param name="momentum">The <see cref="Momentum"/> to be expressed in <see cref="UnitOfMomentum"/> <paramref name="unitOfMomentum"/>.</param>
    /// <param name="unitOfMomentum">The <see cref="UnitOfMomentum"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Momentum momentum, UnitOfMomentum unitOfMomentum) => new(momentum.Magnitude / unitOfMomentum.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Momentum"/>.</summary>
    public Momentum Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Momentum"/> with negated magnitude.</summary>
    public Momentum Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Momentum"/>.</param>
    public static Momentum operator +(Momentum x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Momentum"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Momentum"/>.</param>
    public static Momentum operator -(Momentum x) => x.Negate();

    /// <summary>Multiplies the <see cref="Momentum"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Momentum"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Momentum"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Momentum"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Momentum"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Momentum"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Momentum x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Momentum"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Momentum"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Momentum"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Momentum y) => y.Multiply(x);
    /// <summary>Divides the <see cref="Momentum"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Momentum"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Momentum x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Momentum"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Momentum Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Momentum"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Momentum"/> is scaled.</param>
    public Momentum Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Momentum"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Momentum"/> is divided.</param>
    public Momentum Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Momentum"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="Momentum"/> <paramref name="x"/> by this value.</param>
    public static Momentum operator %(Momentum x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Momentum"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Momentum"/> <paramref name="x"/>.</param>
    public static Momentum operator *(Momentum x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Momentum"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Momentum"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Momentum"/>, which is scaled by <paramref name="x"/>.</param>
    public static Momentum operator *(double x, Momentum y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Momentum"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Momentum"/> <paramref name="x"/>.</param>
    public static Momentum operator /(Momentum x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="Momentum"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Momentum Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Momentum"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Momentum"/> is scaled.</param>
    public Momentum Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Momentum"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Momentum"/> is divided.</param>
    public Momentum Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Momentum"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="Momentum"/> <paramref name="x"/> by this value.</param>
    public static Momentum operator %(Momentum x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Momentum"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Momentum"/> <paramref name="x"/>.</param>
    public static Momentum operator *(Momentum x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Momentum"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Momentum"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Momentum"/>, which is scaled by <paramref name="x"/>.</param>
    public static Momentum operator *(Scalar x, Momentum y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Momentum"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Momentum"/> <paramref name="x"/>.</param>
    public static Momentum operator /(Momentum x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="Momentum"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Momentum"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Momentum"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Momentum"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Momentum"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Momentum"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Momentum.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Momentum x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Momentum"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Momentum"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Momentum.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Momentum x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Multiplies the <see cref="Momentum"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="Momentum3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Momentum"/>.</param>
    public Momentum3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="Momentum"/> with the <see cref="ValueTuple"/> <paramref name="components"/> to produce a <see cref="Momentum3"/>.</summary>
    /// <param name="components">This <see cref="ValueTuple"/> is multiplied by the <see cref="Momentum"/>.</param>
    public Momentum3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="Momentum"/> with the <see cref="ValueTuple"/> <paramref name="components"/> to produce a <see cref="Momentum3"/>.</summary>
    /// <param name="components">This <see cref="ValueTuple"/> is multiplied by the <see cref="Momentum"/>.</param>
    public Momentum3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="Momentum"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="Momentum3"/>.</summary>
    /// <param name="a">This <see cref="Momentum"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Momentum"/> <paramref name="a"/>.</param>
    public static Momentum3 operator *(Momentum a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Momentum"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="Momentum3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Momentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Momentum"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Momentum3 operator *(Vector3 a, Momentum b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="Momentum"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="Momentum3"/>.</summary>
    /// <param name="a">This <see cref="Momentum"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="Momentum"/> <paramref name="a"/>.</param>
    public static Momentum3 operator *(Momentum a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Momentum"/> <parmref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="Momentum3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="Momentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Momentum"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static Momentum3 operator *((double x, double y, double z) a, Momentum b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="Momentum"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="Momentum3"/>.</summary>
    /// <param name="a">This <see cref="Momentum"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="Momentum"/> <paramref name="a"/>.</param>
    public static Momentum3 operator *(Momentum a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Momentum"/> <parmref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="Momentum3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="Momentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Momentum"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static Momentum3 operator *((Scalar x, Scalar y, Scalar z) a, Momentum b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Momentum x, Momentum y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Momentum x, Momentum y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Momentum x, Momentum y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Momentum x, Momentum y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Momentum"/> to a <see cref="double"/> with value <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts the <see cref="Momentum"/> to a <see cref="double"/> based on the magnitude of the <see cref="Momentum"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Momentum x) => x.ToDouble();

    /// <summary>Converts the <see cref="Momentum"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts the <see cref="Momentum"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Momentum x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Momentum"/> of magnitude <paramref name="x"/>.</summary>
    public static Momentum FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Momentum"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Momentum(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Momentum"/> of equivalent magnitude.</summary>
    public static Momentum FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Momentum"/> of equivalent magnitude.</summary>
    public static explicit operator Momentum(Scalar x) => FromScalar(x);
}
