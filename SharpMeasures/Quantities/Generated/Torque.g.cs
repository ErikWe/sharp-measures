namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Torque"/>, describes <see cref="AngularAcceleration"/> of an object with
/// <see cref="Mass"/>. This is the magnitude of the vector quantity <see cref="Torque3"/>, and is expressed in
/// <see cref="UnitOfTorque"/>, with the SI unit being [N * m].
/// <para>
/// New instances of <see cref="Torque"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfTorque"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Torque"/> a = 3 * <see cref="Torque.OneNewtonMetre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Torque"/> d = <see cref="Torque.From(Distance, Force, Angle)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Torque"/> e = <see cref="Work.AsTorque"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfTorque"/>.
/// </para>
/// </summary>
/// <remarks>
/// <see cref="Torque"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="Energy"/></term>
/// <description>Describes the capability to perform <see cref="Work"/>.</description>
/// </item>
/// <item>
/// <term><see cref="Work"/></term>
/// <description>Describes the effect of a <see cref="Force"/> on an object, which transfers <see cref="Energy"/>.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct Torque :
    IComparable<Torque>,
    IScalarQuantity,
    IScalableScalarQuantity<Torque>,
    IMultiplicableScalarQuantity<Torque, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Torque, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<Torque3, Vector3>
{
    /// <summary>The zero-valued <see cref="Torque"/>.</summary>
    public static Torque Zero { get; } = new(0);

    /// <summary>The <see cref="Torque"/> with magnitude 1, when expressed in unit <see cref="UnitOfTorque.NewtonMetre"/>.</summary>
    public static Torque OneNewtonMetre { get; } = new(1, UnitOfTorque.NewtonMetre);

    /// <summary>The magnitude of the <see cref="Torque"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Torque.InNewtonMetres"/>.
    /// <para>This value should only be used (to maximize efficiency) when implementing mathematical operations with other quantities.</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Torque"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfTorque"/> <paramref name="unitOfTorque"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Torque"/>, in <see cref="UnitOfTorque"/> <paramref name="unitOfTorque"/>.</param>
    /// <param name="unitOfTorque">The <see cref="UnitOfTorque"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Torque"/> a = 3 * <see cref="Torque.OneNewtonMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Torque(Scalar magnitude, UnitOfTorque unitOfTorque) : this(magnitude.Magnitude, unitOfTorque) { }
    /// <summary>Constructs a new <see cref="Torque"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfTorque"/> <paramref name="unitOfTorque"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Torque"/>, in <see cref="UnitOfTorque"/> <paramref name="unitOfTorque"/>.</param>
    /// <param name="unitOfTorque">The <see cref="UnitOfTorque"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Torque"/> a = 3 * <see cref="Torque.OneNewtonMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Torque(double magnitude, UnitOfTorque unitOfTorque) : this(magnitude * unitOfTorque.Factor) { }
    /// <summary>Constructs a new <see cref="Torque"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Torque"/>.</param>
    /// <remarks>Consider preffering a constructor that requires a <see cref="UnitOfTorque"/> to be specified.</remarks>
    public Torque(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Torque"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Torque"/>.</param>
    /// <remarks>Consider preferring a constructor that requires a <see cref="UnitOfTorque"/> to be specified.</remarks>
    public Torque(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="Torque"/> to an instance of the associated quantity <see cref="Energy"/>, of equal magnitude.</summary>
    public Energy AsEnergy => new(Magnitude);
    /// <summary>Converts the <see cref="Torque"/> to an instance of the associated quantity <see cref="Work"/>, of equal magnitude.</summary>
    public Work AsWork => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="Torque"/>, expressed in unit <see cref="UnitOfTorque.NewtonMetre"/>.</summary>
    public Scalar InNewtonMetres => InUnit(UnitOfTorque.NewtonMetre);

    /// <summary>Indicates whether the magnitude of the <see cref="Torque"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Torque"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Torque"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Torque"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Torque"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Torque"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Torque"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Torque"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="Torque"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public Torque Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="Torque"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public Torque Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="Torque"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public Torque Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="Torque"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public Torque Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Torque other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Torque"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [N * m]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Torque"/>, expressed in <see cref="UnitOfTorque"/>
    /// <paramref name="unitOfTorque"/>.</summary>
    /// <param name="unitOfTorque">The <see cref="UnitOfTorque"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfTorque unitOfTorque) => InUnit(this, unitOfTorque);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Torque"/>, expressed in <see cref="UnitOfTorque"/>
    /// <paramref name="unitOfTorque"/>.</summary>
    /// <param name="torque">The <see cref="Torque"/> to be expressed in <see cref="UnitOfTorque"/> <paramref name="unitOfTorque"/>.</param>
    /// <param name="unitOfTorque">The <see cref="UnitOfTorque"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Torque torque, UnitOfTorque unitOfTorque) => new(torque.Magnitude / unitOfTorque.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Torque"/>.</summary>
    public Torque Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Torque"/> with negated magnitude.</summary>
    public Torque Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Torque"/>.</param>
    public static Torque operator +(Torque x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Torque"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Torque"/>.</param>
    public static Torque operator -(Torque x) => x.Negate();

    /// <summary>Multiplies the <see cref="Torque"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Torque"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Torque"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Torque"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Torque"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Torque"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Torque"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Torque x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Torque"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Torque"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Torque"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Torque y) => y.Multiply(x);
    /// <summary>Divides the <see cref="Torque"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Torque"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Torque"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Torque x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Torque"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Torque Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Torque"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Torque"/> is scaled.</param>
    public Torque Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Torque"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Torque"/> is divided.</param>
    public Torque Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Torque"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Torque"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="Torque"/> <paramref name="x"/> by this value.</param>
    public static Torque operator %(Torque x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Torque"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Torque"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Torque"/> <paramref name="x"/>.</param>
    public static Torque operator *(Torque x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Torque"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Torque"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Torque"/>, which is scaled by <paramref name="x"/>.</param>
    public static Torque operator *(double x, Torque y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Torque"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Torque"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Torque"/> <paramref name="x"/>.</param>
    public static Torque operator /(Torque x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="Torque"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Torque Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Torque"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Torque"/> is scaled.</param>
    public Torque Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Torque"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Torque"/> is divided.</param>
    public Torque Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Torque"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Torque"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="Torque"/> <paramref name="x"/> by this value.</param>
    public static Torque operator %(Torque x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Torque"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Torque"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Torque"/> <paramref name="x"/>.</param>
    public static Torque operator *(Torque x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Torque"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Torque"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Torque"/>, which is scaled by <paramref name="x"/>.</param>
    public static Torque operator *(Scalar x, Torque y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Torque"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Torque"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Torque"/> <paramref name="x"/>.</param>
    public static Torque operator /(Torque x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="Torque"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Torque"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Torque"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Torque"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Torque"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Torque"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Torque"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Torque.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Torque x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Torque"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Torque"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Torque"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Torque.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Torque x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Multiplies the <see cref="Torque"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="Torque3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Torque"/>.</param>
    public Torque3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="Torque"/> with the <see cref="ValueTuple"/> <paramref name="components"/> to produce a <see cref="Torque3"/>.</summary>
    /// <param name="components">This <see cref="ValueTuple"/> is multiplied by the <see cref="Torque"/>.</param>
    public Torque3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="Torque"/> with the <see cref="ValueTuple"/> <paramref name="components"/> to produce a <see cref="Torque3"/>.</summary>
    /// <param name="components">This <see cref="ValueTuple"/> is multiplied by the <see cref="Torque"/>.</param>
    public Torque3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="Torque"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="Torque3"/>.</summary>
    /// <param name="a">This <see cref="Torque"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Torque"/> <paramref name="a"/>.</param>
    public static Torque3 operator *(Torque a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Torque"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="Torque3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Torque"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Torque"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Torque3 operator *(Vector3 a, Torque b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="Torque"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="Torque3"/>.</summary>
    /// <param name="a">This <see cref="Torque"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="Torque"/> <paramref name="a"/>.</param>
    public static Torque3 operator *(Torque a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Torque"/> <parmref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="Torque3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="Torque"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Torque"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static Torque3 operator *((double x, double y, double z) a, Torque b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="Torque"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="Torque3"/>.</summary>
    /// <param name="a">This <see cref="Torque"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="Torque"/> <paramref name="a"/>.</param>
    public static Torque3 operator *(Torque a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Torque"/> <parmref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="Torque3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="Torque"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Torque"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static Torque3 operator *((Scalar x, Scalar y, Scalar z) a, Torque b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Torque x, Torque y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Torque x, Torque y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Torque x, Torque y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Torque x, Torque y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Torque"/> to a <see cref="double"/> with value <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts the <see cref="Torque"/> to a <see cref="double"/> based on the magnitude of the <see cref="Torque"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Torque x) => x.ToDouble();

    /// <summary>Converts the <see cref="Torque"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts the <see cref="Torque"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Torque x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Torque"/> of magnitude <paramref name="x"/>.</summary>
    public static Torque FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Torque"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Torque(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Torque"/> of equivalent magnitude.</summary>
    public static Torque FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Torque"/> of equivalent magnitude.</summary>
    public static explicit operator Torque(Scalar x) => FromScalar(x);
}
