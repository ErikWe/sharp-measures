namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="AngularMomentum"/>, a property of a rotating object with <see cref="Mass"/>.
/// This is the magnitude of the vector quantity <see cref="AngularMomentum3"/>, and is expressed in <see cref="UnitOfAngularMomentum"/>, with the SI unit being [kg * m^2 / s].
/// <para>
/// New instances of <see cref="AngularMomentum"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfAngularMomentum"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="AngularMomentum"/> a = 3 * <see cref="AngularMomentum.OneKilogramMetreSquaredPerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="#Param:quantity"/> d = <see cref="AngularMomentum.From(MomentOfInertia, AngularSpeed)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="AngularMomentum"/> e = <see cref="SpinAngularMomentum.AsAngularMomentum"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfAngularMomentum"/>.
/// </para>
/// </summary>
/// <remarks>
/// <see cref="AngularMomentum"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="OrbitalAngularMomentum"/></term>
/// <description>Describes the <see cref="AngularMomentum"/> of an object in rotation about an external point.</description>
/// </item>
/// <item>
/// <term><see cref="SpinAngularMomentum"/></term>
/// <description>Describes the <see cref="AngularMomentum"/> of an object in rotation about the center of mass of the object.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct AngularMomentum :
    IComparable<AngularMomentum>,
    IScalarQuantity,
    IScalableScalarQuantity<AngularMomentum>,
    IMultiplicableScalarQuantity<AngularMomentum, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<AngularMomentum, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<AngularMomentum3, Vector3>
{
    /// <summary>The zero-valued <see cref="AngularMomentum"/>.</summary>
    public static AngularMomentum Zero { get; } = new(0);

    /// <summary>The <see cref="AngularMomentum"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngularMomentum.KilogramMetreSquaredPerSecond"/>.</summary>
    public static AngularMomentum OneKilogramMetreSquaredPerSecond { get; } = new(1, UnitOfAngularMomentum.KilogramMetreSquaredPerSecond);

    /// <summary>The magnitude of the <see cref="AngularMomentum"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="AngularMomentum.InKilogramMetresSquaredPerSecond"/>.
    /// <para>This value should only be used (to maximize efficiency) when implementing mathematical operations with other quantities.</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="AngularMomentum"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfAngularMomentum"/> <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularMomentum"/>, in <see cref="UnitOfAngularMomentum"/> <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="AngularMomentum"/> a = 3 * <see cref="AngularMomentum.OneKilogramMetreSquaredPerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public AngularMomentum(Scalar magnitude, UnitOfAngularMomentum unitOfAngularMomentum) : this(magnitude.Magnitude, unitOfAngularMomentum) { }
    /// <summary>Constructs a new <see cref="AngularMomentum"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfAngularMomentum"/> <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularMomentum"/>, in <see cref="UnitOfAngularMomentum"/> <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="AngularMomentum"/> a = 3 * <see cref="AngularMomentum.OneKilogramMetreSquaredPerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public AngularMomentum(double magnitude, UnitOfAngularMomentum unitOfAngularMomentum) : this(magnitude * unitOfAngularMomentum.Factor) { }
    /// <summary>Constructs a new <see cref="AngularMomentum"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularMomentum"/>.</param>
    /// <remarks>Consider preffering a constructor that requires a <see cref="UnitOfAngularMomentum"/> to be specified.</remarks>
    public AngularMomentum(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="AngularMomentum"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularMomentum"/>.</param>
    /// <remarks>Consider preferring a constructor that requires a <see cref="UnitOfAngularMomentum"/> to be specified.</remarks>
    public AngularMomentum(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="AngularMomentum"/> to an instance of the associated quantity <see cref="OrbitalAngularMomentum"/>, of equal magnitude.</summary>
    public OrbitalAngularMomentum AsOrbitalAngularMomentum => new(Magnitude);
    /// <summary>Converts the <see cref="AngularMomentum"/> to an instance of the associated quantity <see cref="SpinAngularMomentum"/>, of equal magnitude.</summary>
    public SpinAngularMomentum AsSpinAngularMomentum => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="AngularMomentum"/>, expressed in unit <see cref="UnitOfAngularMomentum.KilogramMetreSquaredPerSecond"/>.</summary>
    public Scalar InKilogramMetresSquaredPerSecond => InUnit(UnitOfAngularMomentum.KilogramMetreSquaredPerSecond);

    /// <summary>Indicates whether the magnitude of the <see cref="AngularMomentum"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="AngularMomentum"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="AngularMomentum"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="AngularMomentum"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="AngularMomentum"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="AngularMomentum"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="AngularMomentum"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="AngularMomentum"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="AngularMomentum"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public AngularMomentum Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="AngularMomentum"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public AngularMomentum Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="AngularMomentum"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public AngularMomentum Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="AngularMomentum"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public AngularMomentum Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(AngularMomentum other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="AngularMomentum"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [kg * m^2 / s]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="AngularMomentum"/>, expressed in <see cref="UnitOfAngularMomentum"/>
    /// <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfAngularMomentum unitOfAngularMomentum) => InUnit(this, unitOfAngularMomentum);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="AngularMomentum"/>, expressed in <see cref="UnitOfAngularMomentum"/>
    /// <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="angularMomentum">The <see cref="AngularMomentum"/> to be expressed in <see cref="UnitOfAngularMomentum"/> <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(AngularMomentum angularMomentum, UnitOfAngularMomentum unitOfAngularMomentum) => new(angularMomentum.Magnitude / unitOfAngularMomentum.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="AngularMomentum"/>.</summary>
    public AngularMomentum Plus() => this;
    /// <summary>Negation, resulting in a <see cref="AngularMomentum"/> with negated magnitude.</summary>
    public AngularMomentum Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="AngularMomentum"/>.</param>
    public static AngularMomentum operator +(AngularMomentum x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="AngularMomentum"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="AngularMomentum"/>.</param>
    public static AngularMomentum operator -(AngularMomentum x) => x.Negate();

    /// <summary>Multiplies the <see cref="AngularMomentum"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="AngularMomentum"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="AngularMomentum"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="AngularMomentum"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="AngularMomentum"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="AngularMomentum"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(AngularMomentum x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="AngularMomentum"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="AngularMomentum"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="AngularMomentum"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, AngularMomentum y) => y.Multiply(x);
    /// <summary>Divides the <see cref="AngularMomentum"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="AngularMomentum"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(AngularMomentum x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="AngularMomentum"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public AngularMomentum Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="AngularMomentum"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="AngularMomentum"/> is scaled.</param>
    public AngularMomentum Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="AngularMomentum"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="AngularMomentum"/> is divided.</param>
    public AngularMomentum Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="AngularMomentum"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="AngularMomentum"/> <paramref name="x"/> by this value.</param>
    public static AngularMomentum operator %(AngularMomentum x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="AngularMomentum"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="AngularMomentum"/> <paramref name="x"/>.</param>
    public static AngularMomentum operator *(AngularMomentum x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="AngularMomentum"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="AngularMomentum"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="AngularMomentum"/>, which is scaled by <paramref name="x"/>.</param>
    public static AngularMomentum operator *(double x, AngularMomentum y) => y.Multiply(x);
    /// <summary>Scales the <see cref="AngularMomentum"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="AngularMomentum"/> <paramref name="x"/>.</param>
    public static AngularMomentum operator /(AngularMomentum x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="AngularMomentum"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public AngularMomentum Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="AngularMomentum"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="AngularMomentum"/> is scaled.</param>
    public AngularMomentum Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="AngularMomentum"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="AngularMomentum"/> is divided.</param>
    public AngularMomentum Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="AngularMomentum"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="AngularMomentum"/> <paramref name="x"/> by this value.</param>
    public static AngularMomentum operator %(AngularMomentum x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="AngularMomentum"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="AngularMomentum"/> <paramref name="x"/>.</param>
    public static AngularMomentum operator *(AngularMomentum x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="AngularMomentum"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="AngularMomentum"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="AngularMomentum"/>, which is scaled by <paramref name="x"/>.</param>
    public static AngularMomentum operator *(Scalar x, AngularMomentum y) => y.Multiply(x);
    /// <summary>Scales the <see cref="AngularMomentum"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="AngularMomentum"/> <paramref name="x"/>.</param>
    public static AngularMomentum operator /(AngularMomentum x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="AngularMomentum"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="AngularMomentum"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="AngularMomentum"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="AngularMomentum"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="AngularMomentum"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="AngularMomentum"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="AngularMomentum.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(AngularMomentum x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="AngularMomentum"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="AngularMomentum"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="AngularMomentum.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(AngularMomentum x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Multiplies the <see cref="AngularMomentum"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="AngularMomentum"/>.</param>
    public AngularMomentum3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="AngularMomentum"/> with the <see cref="ValueTuple"/> <paramref name="components"/> to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="components">This <see cref="ValueTuple"/> is multiplied by the <see cref="AngularMomentum"/>.</param>
    public AngularMomentum3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="AngularMomentum"/> with the <see cref="ValueTuple"/> <paramref name="components"/> to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="components">This <see cref="ValueTuple"/> is multiplied by the <see cref="AngularMomentum"/>.</param>
    public AngularMomentum3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="AngularMomentum"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="AngularMomentum"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="AngularMomentum"/> <paramref name="a"/>.</param>
    public static AngularMomentum3 operator *(AngularMomentum a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="AngularMomentum"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="AngularMomentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="AngularMomentum"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static AngularMomentum3 operator *(Vector3 a, AngularMomentum b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="AngularMomentum"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="AngularMomentum"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="AngularMomentum"/> <paramref name="a"/>.</param>
    public static AngularMomentum3 operator *(AngularMomentum a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="AngularMomentum"/> <parmref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="AngularMomentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="AngularMomentum"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static AngularMomentum3 operator *((double x, double y, double z) a, AngularMomentum b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="AngularMomentum"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="AngularMomentum"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="AngularMomentum"/> <paramref name="a"/>.</param>
    public static AngularMomentum3 operator *(AngularMomentum a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="AngularMomentum"/> <parmref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="AngularMomentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="AngularMomentum"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static AngularMomentum3 operator *((Scalar x, Scalar y, Scalar z) a, AngularMomentum b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(AngularMomentum x, AngularMomentum y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(AngularMomentum x, AngularMomentum y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(AngularMomentum x, AngularMomentum y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(AngularMomentum x, AngularMomentum y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="AngularMomentum"/> to a <see cref="double"/> with value <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts the <see cref="AngularMomentum"/> to a <see cref="double"/> based on the magnitude of the <see cref="AngularMomentum"/> <paramref name="x"/>.</summary>
    public static implicit operator double(AngularMomentum x) => x.ToDouble();

    /// <summary>Converts the <see cref="AngularMomentum"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts the <see cref="AngularMomentum"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(AngularMomentum x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularMomentum"/> of magnitude <paramref name="x"/>.</summary>
    public static AngularMomentum FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularMomentum"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator AngularMomentum(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularMomentum"/> of equivalent magnitude.</summary>
    public static AngularMomentum FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularMomentum"/> of equivalent magnitude.</summary>
    public static explicit operator AngularMomentum(Scalar x) => FromScalar(x);
}
