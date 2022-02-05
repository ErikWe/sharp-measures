namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="AngularAcceleration"/>, describing change in <see cref="AngularSpeed"/> over <see cref="Time"/>.
/// This is the magnitude of the vector quantity <see cref="AngularAcceleration3"/>, and is expressed in <see cref="UnitOfAngularAcceleration"/>,
/// with the SI unit being [rad / s²].
/// <para>
/// New instances of <see cref="AngularAcceleration"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfAngularAcceleration"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="AngularAcceleration"/> a = 3 * <see cref="AngularAcceleration.OneRadianPerSecondSquared"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="AngularAcceleration"/> d = <see cref="AngularAcceleration.From(AngularSpeed, Time)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="AngularAcceleration"/> e = <see cref="SpinAngularAcceleration.AsAngularAcceleration()"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfAngularAcceleration"/>.
/// </para>
/// </summary>
/// <remarks>
/// <see cref="AngularAcceleration"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="OrbitalAngularAcceleration"/></term>
/// <description>Describes the angular acceleration of an object about an external point.</description>
/// </item>
/// <item>
/// <term><see cref="SpinAngularAcceleration"/></term>
/// <description>Describes the angular acceleration of an object about the internal center of rotation.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct AngularAcceleration :
    IComparable<AngularAcceleration>,
    IScalarQuantity,
    IScalableScalarQuantity<AngularAcceleration>,
    IMultiplicableScalarQuantity<AngularAcceleration, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<AngularAcceleration, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<AngularAcceleration3, Vector3>
{
    /// <summary>The zero-valued <see cref="AngularAcceleration"/>.</summary>
    public static AngularAcceleration Zero { get; } = new(0);

    /// <summary>The <see cref="AngularAcceleration"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngularAcceleration.RadianPerSecondSquared"/>.</summary>
    public static AngularAcceleration OneRadianPerSecondSquared { get; } = new(1, UnitOfAngularAcceleration.RadianPerSecondSquared);

    /// <summary>The magnitude of the <see cref="AngularAcceleration"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="AngularAcceleration.InRadiansPerSecondSquared"/>.
    /// <para>This value should only be used (to maximize efficiency) when implementing mathematical operations with other quantities.</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="AngularAcceleration"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfAngularAcceleration"/> <paramref name="unitOfAngularAcceleration"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularAcceleration"/>, in <see cref="UnitOfAngularAcceleration"/> <paramref name="unitOfAngularAcceleration"/>.</param>
    /// <param name="unitOfAngularAcceleration">The <see cref="UnitOfAngularAcceleration"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="AngularAcceleration"/> a = 3 * <see cref="AngularAcceleration.OneRadianPerSecondSquared"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public AngularAcceleration(Scalar magnitude, UnitOfAngularAcceleration unitOfAngularAcceleration) : this(magnitude.Magnitude, unitOfAngularAcceleration) { }
    /// <summary>Constructs a new <see cref="AngularAcceleration"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfAngularAcceleration"/> <paramref name="unitOfAngularAcceleration"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularAcceleration"/>, in <see cref="UnitOfAngularAcceleration"/> <paramref name="unitOfAngularAcceleration"/>.</param>
    /// <param name="unitOfAngularAcceleration">The <see cref="UnitOfAngularAcceleration"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="AngularAcceleration"/> a = 3 * <see cref="AngularAcceleration.OneRadianPerSecondSquared"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public AngularAcceleration(double magnitude, UnitOfAngularAcceleration unitOfAngularAcceleration) : this(magnitude * unitOfAngularAcceleration.Factor) { }
    /// <summary>Constructs a new <see cref="AngularAcceleration"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularAcceleration"/>.</param>
    /// <remarks>Consider preffering a constructor that requires a <see cref="UnitOfAngularAcceleration"/> to be specified.</remarks>
    public AngularAcceleration(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="AngularAcceleration"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularAcceleration"/>.</param>
    /// <remarks>Consider preferring a constructor that requires a <see cref="UnitOfAngularAcceleration"/> to be specified.</remarks>
    public AngularAcceleration(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="AngularAcceleration"/> to an instance of the associated quantity <see cref="OrbitalAngularAcceleration"/>, of equal magnitude.</summary>
    public OrbitalAngularAcceleration AsOrbitalAngularAcceleration => new(Magnitude);
    /// <summary>Converts the <see cref="AngularAcceleration"/> to an instance of the associated quantity <see cref="SpinAngularAcceleration"/>, of equal magnitude.</summary>
    public SpinAngularAcceleration AsSpinAngularAcceleration => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="AngularAcceleration"/>, expressed in unit <see cref="UnitOfAngularAcceleration.RadianPerSecondSquared"/>.</summary>
    public Scalar InRadiansPerSecondSquared => InUnit(UnitOfAngularAcceleration.RadianPerSecondSquared);

    /// <summary>Indicates whether the magnitude of the <see cref="AngularAcceleration"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="AngularAcceleration"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="AngularAcceleration"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="AngularAcceleration"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="AngularAcceleration"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="AngularAcceleration"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="AngularAcceleration"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="AngularAcceleration"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="AngularAcceleration"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public AngularAcceleration Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="AngularAcceleration"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public AngularAcceleration Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="AngularAcceleration"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public AngularAcceleration Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="AngularAcceleration"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public AngularAcceleration Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(AngularAcceleration other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="AngularAcceleration"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [rad / s^2]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="AngularAcceleration"/>, expressed in <see cref="UnitOfAngularAcceleration"/>
    /// <paramref name="unitOfAngularAcceleration"/>.</summary>
    /// <param name="unitOfAngularAcceleration">The <see cref="UnitOfAngularAcceleration"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfAngularAcceleration unitOfAngularAcceleration) => InUnit(this, unitOfAngularAcceleration);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="AngularAcceleration"/>, expressed in <see cref="UnitOfAngularAcceleration"/>
    /// <paramref name="unitOfAngularAcceleration"/>.</summary>
    /// <param name="angularAcceleration">The <see cref="AngularAcceleration"/> to be expressed in <see cref="UnitOfAngularAcceleration"/> <paramref name="unitOfAngularAcceleration"/>.</param>
    /// <param name="unitOfAngularAcceleration">The <see cref="UnitOfAngularAcceleration"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(AngularAcceleration angularAcceleration, UnitOfAngularAcceleration unitOfAngularAcceleration) => new(angularAcceleration.Magnitude / unitOfAngularAcceleration.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="AngularAcceleration"/>.</summary>
    public AngularAcceleration Plus() => this;
    /// <summary>Negation, resulting in a <see cref="AngularAcceleration"/> with negated magnitude.</summary>
    public AngularAcceleration Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="AngularAcceleration"/>.</param>
    public static AngularAcceleration operator +(AngularAcceleration x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="AngularAcceleration"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="AngularAcceleration"/>.</param>
    public static AngularAcceleration operator -(AngularAcceleration x) => x.Negate();

    /// <summary>Multiplies the <see cref="AngularAcceleration"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="AngularAcceleration"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="AngularAcceleration"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="AngularAcceleration"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="AngularAcceleration"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularAcceleration"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="AngularAcceleration"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(AngularAcceleration x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="AngularAcceleration"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="AngularAcceleration"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="AngularAcceleration"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, AngularAcceleration y) => y.Multiply(x);
    /// <summary>Divides the <see cref="AngularAcceleration"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularAcceleration"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="AngularAcceleration"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(AngularAcceleration x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="AngularAcceleration"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public AngularAcceleration Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="AngularAcceleration"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="AngularAcceleration"/> is scaled.</param>
    public AngularAcceleration Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="AngularAcceleration"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="AngularAcceleration"/> is divided.</param>
    public AngularAcceleration Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="AngularAcceleration"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularAcceleration"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="AngularAcceleration"/> <paramref name="x"/> by this value.</param>
    public static AngularAcceleration operator %(AngularAcceleration x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="AngularAcceleration"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularAcceleration"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="AngularAcceleration"/> <paramref name="x"/>.</param>
    public static AngularAcceleration operator *(AngularAcceleration x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="AngularAcceleration"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="AngularAcceleration"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="AngularAcceleration"/>, which is scaled by <paramref name="x"/>.</param>
    public static AngularAcceleration operator *(double x, AngularAcceleration y) => y.Multiply(x);
    /// <summary>Scales the <see cref="AngularAcceleration"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularAcceleration"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="AngularAcceleration"/> <paramref name="x"/>.</param>
    public static AngularAcceleration operator /(AngularAcceleration x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="AngularAcceleration"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public AngularAcceleration Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="AngularAcceleration"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="AngularAcceleration"/> is scaled.</param>
    public AngularAcceleration Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="AngularAcceleration"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="AngularAcceleration"/> is divided.</param>
    public AngularAcceleration Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="AngularAcceleration"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularAcceleration"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="AngularAcceleration"/> <paramref name="x"/> by this value.</param>
    public static AngularAcceleration operator %(AngularAcceleration x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="AngularAcceleration"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularAcceleration"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="AngularAcceleration"/> <paramref name="x"/>.</param>
    public static AngularAcceleration operator *(AngularAcceleration x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="AngularAcceleration"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="AngularAcceleration"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="AngularAcceleration"/>, which is scaled by <paramref name="x"/>.</param>
    public static AngularAcceleration operator *(Scalar x, AngularAcceleration y) => y.Multiply(x);
    /// <summary>Scales the <see cref="AngularAcceleration"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularAcceleration"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="AngularAcceleration"/> <paramref name="x"/>.</param>
    public static AngularAcceleration operator /(AngularAcceleration x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="AngularAcceleration"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="AngularAcceleration"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="AngularAcceleration"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="AngularAcceleration"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="AngularAcceleration"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularAcceleration"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="AngularAcceleration"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="AngularAcceleration.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(AngularAcceleration x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="AngularAcceleration"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularAcceleration"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="AngularAcceleration"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="AngularAcceleration.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(AngularAcceleration x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Multiplies the <see cref="AngularAcceleration"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="AngularAcceleration3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="AngularAcceleration"/>.</param>
    public AngularAcceleration3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="AngularAcceleration"/> with the <see cref="ValueTuple"/> <paramref name="components"/> to produce a <see cref="AngularAcceleration3"/>.</summary>
    /// <param name="components">This <see cref="ValueTuple"/> is multiplied by the <see cref="AngularAcceleration"/>.</param>
    public AngularAcceleration3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="AngularAcceleration"/> with the <see cref="ValueTuple"/> <paramref name="components"/> to produce a <see cref="AngularAcceleration3"/>.</summary>
    /// <param name="components">This <see cref="ValueTuple"/> is multiplied by the <see cref="AngularAcceleration"/>.</param>
    public AngularAcceleration3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="AngularAcceleration"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="AngularAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="AngularAcceleration"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="AngularAcceleration"/> <paramref name="a"/>.</param>
    public static AngularAcceleration3 operator *(AngularAcceleration a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="AngularAcceleration"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="AngularAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="AngularAcceleration"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="AngularAcceleration"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static AngularAcceleration3 operator *(Vector3 a, AngularAcceleration b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="AngularAcceleration"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="AngularAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="AngularAcceleration"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="AngularAcceleration"/> <paramref name="a"/>.</param>
    public static AngularAcceleration3 operator *(AngularAcceleration a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="AngularAcceleration"/> <parmref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="AngularAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="AngularAcceleration"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="AngularAcceleration"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static AngularAcceleration3 operator *((double x, double y, double z) a, AngularAcceleration b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="AngularAcceleration"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="AngularAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="AngularAcceleration"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="AngularAcceleration"/> <paramref name="a"/>.</param>
    public static AngularAcceleration3 operator *(AngularAcceleration a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="AngularAcceleration"/> <parmref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="AngularAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="AngularAcceleration"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="AngularAcceleration"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static AngularAcceleration3 operator *((Scalar x, Scalar y, Scalar z) a, AngularAcceleration b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(AngularAcceleration x, AngularAcceleration y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(AngularAcceleration x, AngularAcceleration y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(AngularAcceleration x, AngularAcceleration y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(AngularAcceleration x, AngularAcceleration y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="AngularAcceleration"/> to a <see cref="double"/> with value <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts the <see cref="AngularAcceleration"/> to a <see cref="double"/> based on the magnitude of the <see cref="AngularAcceleration"/> <paramref name="x"/>.</summary>
    public static implicit operator double(AngularAcceleration x) => x.ToDouble();

    /// <summary>Converts the <see cref="AngularAcceleration"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts the <see cref="AngularAcceleration"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(AngularAcceleration x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularAcceleration"/> of magnitude <paramref name="x"/>.</summary>
    public static AngularAcceleration FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularAcceleration"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator AngularAcceleration(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularAcceleration"/> of equivalent magnitude.</summary>
    public static AngularAcceleration FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularAcceleration"/> of equivalent magnitude.</summary>
    public static explicit operator AngularAcceleration(Scalar x) => FromScalar(x);
}
