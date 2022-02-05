namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="SpinAngularAcceleration"/>, describing the <see cref="AngularAcceleration"/> of an object about the internal center of rotation.
/// This is the magnitude of the vector quantity <see cref="SpinAngularAcceleration3"/>, and is expressed in <see cref="UnitOfSpinAngularAcceleration"/>,
/// with the SI unit being [rad / s²].
/// <para>
/// New instances of <see cref="SpinAngularAcceleration"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfSpinAngularAcceleration"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="SpinAngularAcceleration"/> a = 3 * <see cref="SpinAngularAcceleration.OneRadianPerSecondSquared"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="SpinAngularAcceleration"/> d = <see cref="SpinAngularAcceleration.From(SpinAngularSpeed, Time)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="SpinAngularAcceleration"/> e = <see cref="AngularAcceleration.AsSpinAngularAcceleration()"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfSpinAngularAcceleration"/>.
/// </para>
/// </summary>
/// <remarks>
/// <see cref="SpinAngularAcceleration"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="AngularAcceleration"/></term>
/// <description>Describes any type of angular acceleration.</description>
/// </item>
/// <item>
/// <term><see cref="OrbitalAngularAcceleration"/></term>
/// <description>Describes the angular acceleration of an object about an external point.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct SpinAngularAcceleration :
    IComparable<SpinAngularAcceleration>,
    IScalarQuantity,
    IScalableScalarQuantity<SpinAngularAcceleration>,
    IMultiplicableScalarQuantity<SpinAngularAcceleration, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<SpinAngularAcceleration, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<SpinAngularAcceleration3, Vector3>
{
    /// <summary>The zero-valued <see cref="SpinAngularAcceleration"/>.</summary>
    public static SpinAngularAcceleration Zero { get; } = new(0);

    /// <summary>The <see cref="SpinAngularAcceleration"/> with magnitude 1, when expressed in unit <see cref="UnitOfSpinAngularAcceleration.RadianPerSecondSquared"/>.</summary>
    public static SpinAngularAcceleration OneRadianPerSecondSquared { get; } = new(1, UnitOfSpinAngularAcceleration.RadianPerSecondSquared);

    /// <summary>The magnitude of the <see cref="SpinAngularAcceleration"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="SpinAngularAcceleration.InRadiansPerSecondSquared"/>.
    /// <para>This value should only be used (to maximize efficiency) when implementing mathematical operations with other quantities.</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="SpinAngularAcceleration"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfSpinAngularAcceleration"/> <paramref name="unitOfSpinAngularAcceleration"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpinAngularAcceleration"/>, in <see cref="UnitOfSpinAngularAcceleration"/> <paramref name="unitOfSpinAngularAcceleration"/>.</param>
    /// <param name="unitOfSpinAngularAcceleration">The <see cref="UnitOfSpinAngularAcceleration"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SpinAngularAcceleration"/> a = 3 * <see cref="SpinAngularAcceleration.OneRadianPerSecondSquared"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SpinAngularAcceleration(Scalar magnitude, UnitOfSpinAngularAcceleration unitOfSpinAngularAcceleration) : this(magnitude.Magnitude, unitOfSpinAngularAcceleration) { }
    /// <summary>Constructs a new <see cref="SpinAngularAcceleration"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfSpinAngularAcceleration"/> <paramref name="unitOfSpinAngularAcceleration"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpinAngularAcceleration"/>, in <see cref="UnitOfSpinAngularAcceleration"/> <paramref name="unitOfSpinAngularAcceleration"/>.</param>
    /// <param name="unitOfSpinAngularAcceleration">The <see cref="UnitOfSpinAngularAcceleration"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SpinAngularAcceleration"/> a = 3 * <see cref="SpinAngularAcceleration.OneRadianPerSecondSquared"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SpinAngularAcceleration(double magnitude, UnitOfSpinAngularAcceleration unitOfSpinAngularAcceleration) : this(magnitude * unitOfSpinAngularAcceleration.Factor) { }
    /// <summary>Constructs a new <see cref="SpinAngularAcceleration"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpinAngularAcceleration"/>.</param>
    /// <remarks>Consider preffering a constructor that requires a <see cref="UnitOfSpinAngularAcceleration"/> to be specified.</remarks>
    public SpinAngularAcceleration(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="SpinAngularAcceleration"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpinAngularAcceleration"/>.</param>
    /// <remarks>Consider preferring a constructor that requires a <see cref="UnitOfSpinAngularAcceleration"/> to be specified.</remarks>
    public SpinAngularAcceleration(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="SpinAngularAcceleration"/> to an instance of the associated quantity <see cref="AngularAcceleration"/>, of equal magnitude.</summary>
    public AngularAcceleration AsAngularAcceleration => new(Magnitude);
    /// <summary>Converts the <see cref="SpinAngularAcceleration"/> to an instance of the associated quantity <see cref="OrbitalAngularAcceleration"/>, of equal magnitude.</summary>
    public OrbitalAngularAcceleration AsOrbitalAngularAcceleration => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="SpinAngularAcceleration"/>, expressed in unit <see cref="UnitOfSpinAngularAcceleration.RadianPerSecondSquared"/>.</summary>
    public Scalar InRadiansPerSecondSquared => InUnit(UnitOfSpinAngularAcceleration.RadianPerSecondSquared);

    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularAcceleration"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularAcceleration"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularAcceleration"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularAcceleration"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularAcceleration"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularAcceleration"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularAcceleration"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularAcceleration"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="SpinAngularAcceleration"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public SpinAngularAcceleration Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="SpinAngularAcceleration"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public SpinAngularAcceleration Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="SpinAngularAcceleration"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public SpinAngularAcceleration Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="SpinAngularAcceleration"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public SpinAngularAcceleration Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(SpinAngularAcceleration other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="SpinAngularAcceleration"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [rad / s^2]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="SpinAngularAcceleration"/>, expressed in <see cref="UnitOfSpinAngularAcceleration"/>
    /// <paramref name="unitOfSpinAngularAcceleration"/>.</summary>
    /// <param name="unitOfSpinAngularAcceleration">The <see cref="UnitOfSpinAngularAcceleration"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfSpinAngularAcceleration unitOfSpinAngularAcceleration) => InUnit(this, unitOfSpinAngularAcceleration);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="SpinAngularAcceleration"/>, expressed in <see cref="UnitOfSpinAngularAcceleration"/>
    /// <paramref name="unitOfSpinAngularAcceleration"/>.</summary>
    /// <param name="spinAngularAcceleration">The <see cref="SpinAngularAcceleration"/> to be expressed in <see cref="UnitOfSpinAngularAcceleration"/> <paramref name="unitOfSpinAngularAcceleration"/>.</param>
    /// <param name="unitOfSpinAngularAcceleration">The <see cref="UnitOfSpinAngularAcceleration"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(SpinAngularAcceleration spinAngularAcceleration, UnitOfSpinAngularAcceleration unitOfSpinAngularAcceleration) => new(spinAngularAcceleration.Magnitude / unitOfSpinAngularAcceleration.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="SpinAngularAcceleration"/>.</summary>
    public SpinAngularAcceleration Plus() => this;
    /// <summary>Negation, resulting in a <see cref="SpinAngularAcceleration"/> with negated magnitude.</summary>
    public SpinAngularAcceleration Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="SpinAngularAcceleration"/>.</param>
    public static SpinAngularAcceleration operator +(SpinAngularAcceleration x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="SpinAngularAcceleration"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="SpinAngularAcceleration"/>.</param>
    public static SpinAngularAcceleration operator -(SpinAngularAcceleration x) => x.Negate();

    /// <summary>Multiplies the <see cref="SpinAngularAcceleration"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="SpinAngularAcceleration"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="SpinAngularAcceleration"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="SpinAngularAcceleration"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="SpinAngularAcceleration"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpinAngularAcceleration"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SpinAngularAcceleration"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(SpinAngularAcceleration x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="SpinAngularAcceleration"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="SpinAngularAcceleration"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="SpinAngularAcceleration"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, SpinAngularAcceleration y) => y.Multiply(x);
    /// <summary>Divides the <see cref="SpinAngularAcceleration"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpinAngularAcceleration"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SpinAngularAcceleration"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(SpinAngularAcceleration x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="SpinAngularAcceleration"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public SpinAngularAcceleration Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="SpinAngularAcceleration"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SpinAngularAcceleration"/> is scaled.</param>
    public SpinAngularAcceleration Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="SpinAngularAcceleration"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SpinAngularAcceleration"/> is divided.</param>
    public SpinAngularAcceleration Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="SpinAngularAcceleration"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpinAngularAcceleration"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="SpinAngularAcceleration"/> <paramref name="x"/> by this value.</param>
    public static SpinAngularAcceleration operator %(SpinAngularAcceleration x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SpinAngularAcceleration"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpinAngularAcceleration"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SpinAngularAcceleration"/> <paramref name="x"/>.</param>
    public static SpinAngularAcceleration operator *(SpinAngularAcceleration x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SpinAngularAcceleration"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SpinAngularAcceleration"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpinAngularAcceleration"/>, which is scaled by <paramref name="x"/>.</param>
    public static SpinAngularAcceleration operator *(double x, SpinAngularAcceleration y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SpinAngularAcceleration"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpinAngularAcceleration"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SpinAngularAcceleration"/> <paramref name="x"/>.</param>
    public static SpinAngularAcceleration operator /(SpinAngularAcceleration x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="SpinAngularAcceleration"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public SpinAngularAcceleration Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="SpinAngularAcceleration"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SpinAngularAcceleration"/> is scaled.</param>
    public SpinAngularAcceleration Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="SpinAngularAcceleration"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SpinAngularAcceleration"/> is divided.</param>
    public SpinAngularAcceleration Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="SpinAngularAcceleration"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpinAngularAcceleration"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="SpinAngularAcceleration"/> <paramref name="x"/> by this value.</param>
    public static SpinAngularAcceleration operator %(SpinAngularAcceleration x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SpinAngularAcceleration"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpinAngularAcceleration"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SpinAngularAcceleration"/> <paramref name="x"/>.</param>
    public static SpinAngularAcceleration operator *(SpinAngularAcceleration x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SpinAngularAcceleration"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SpinAngularAcceleration"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpinAngularAcceleration"/>, which is scaled by <paramref name="x"/>.</param>
    public static SpinAngularAcceleration operator *(Scalar x, SpinAngularAcceleration y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SpinAngularAcceleration"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpinAngularAcceleration"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SpinAngularAcceleration"/> <paramref name="x"/>.</param>
    public static SpinAngularAcceleration operator /(SpinAngularAcceleration x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="SpinAngularAcceleration"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="SpinAngularAcceleration"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="SpinAngularAcceleration"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="SpinAngularAcceleration"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="SpinAngularAcceleration"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpinAngularAcceleration"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="SpinAngularAcceleration"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="SpinAngularAcceleration.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(SpinAngularAcceleration x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="SpinAngularAcceleration"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpinAngularAcceleration"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="SpinAngularAcceleration"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="SpinAngularAcceleration.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(SpinAngularAcceleration x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Multiplies the <see cref="SpinAngularAcceleration"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="SpinAngularAcceleration3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="SpinAngularAcceleration"/>.</param>
    public SpinAngularAcceleration3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="SpinAngularAcceleration"/> with the <see cref="ValueTuple"/> <paramref name="components"/> to produce a <see cref="SpinAngularAcceleration3"/>.</summary>
    /// <param name="components">This <see cref="ValueTuple"/> is multiplied by the <see cref="SpinAngularAcceleration"/>.</param>
    public SpinAngularAcceleration3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="SpinAngularAcceleration"/> with the <see cref="ValueTuple"/> <paramref name="components"/> to produce a <see cref="SpinAngularAcceleration3"/>.</summary>
    /// <param name="components">This <see cref="ValueTuple"/> is multiplied by the <see cref="SpinAngularAcceleration"/>.</param>
    public SpinAngularAcceleration3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="SpinAngularAcceleration"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="SpinAngularAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="SpinAngularAcceleration"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="SpinAngularAcceleration"/> <paramref name="a"/>.</param>
    public static SpinAngularAcceleration3 operator *(SpinAngularAcceleration a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="SpinAngularAcceleration"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="SpinAngularAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="SpinAngularAcceleration"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="SpinAngularAcceleration"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static SpinAngularAcceleration3 operator *(Vector3 a, SpinAngularAcceleration b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="SpinAngularAcceleration"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="SpinAngularAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="SpinAngularAcceleration"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="SpinAngularAcceleration"/> <paramref name="a"/>.</param>
    public static SpinAngularAcceleration3 operator *(SpinAngularAcceleration a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="SpinAngularAcceleration"/> <parmref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="SpinAngularAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="SpinAngularAcceleration"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="SpinAngularAcceleration"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static SpinAngularAcceleration3 operator *((double x, double y, double z) a, SpinAngularAcceleration b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="SpinAngularAcceleration"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="SpinAngularAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="SpinAngularAcceleration"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="SpinAngularAcceleration"/> <paramref name="a"/>.</param>
    public static SpinAngularAcceleration3 operator *(SpinAngularAcceleration a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="SpinAngularAcceleration"/> <parmref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="SpinAngularAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="SpinAngularAcceleration"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="SpinAngularAcceleration"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static SpinAngularAcceleration3 operator *((Scalar x, Scalar y, Scalar z) a, SpinAngularAcceleration b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(SpinAngularAcceleration x, SpinAngularAcceleration y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(SpinAngularAcceleration x, SpinAngularAcceleration y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(SpinAngularAcceleration x, SpinAngularAcceleration y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(SpinAngularAcceleration x, SpinAngularAcceleration y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="SpinAngularAcceleration"/> to a <see cref="double"/> with value <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts the <see cref="SpinAngularAcceleration"/> to a <see cref="double"/> based on the magnitude of the <see cref="SpinAngularAcceleration"/> <paramref name="x"/>.</summary>
    public static implicit operator double(SpinAngularAcceleration x) => x.ToDouble();

    /// <summary>Converts the <see cref="SpinAngularAcceleration"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts the <see cref="SpinAngularAcceleration"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(SpinAngularAcceleration x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="SpinAngularAcceleration"/> of magnitude <paramref name="x"/>.</summary>
    public static SpinAngularAcceleration FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="SpinAngularAcceleration"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator SpinAngularAcceleration(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="SpinAngularAcceleration"/> of equivalent magnitude.</summary>
    public static SpinAngularAcceleration FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="SpinAngularAcceleration"/> of equivalent magnitude.</summary>
    public static explicit operator SpinAngularAcceleration(Scalar x) => FromScalar(x);
}
