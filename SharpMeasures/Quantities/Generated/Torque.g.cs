﻿#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Torque"/>, describing <see cref="AngularAcceleration"/> of an object with
/// <see cref="Mass"/>. This is the magnitude of the vector quantity <see cref="Torque3"/>, and is expressed in
/// <see cref="UnitOfTorque"/>, with the SI unit being [N∙m].
/// <para>
/// New instances of <see cref="Torque"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfTorque"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code><see cref="Torque"/> a = 3 * <see cref="Torque.OneNewtonMetre"/>;</code>
/// </item>
/// <item>
/// <code><see cref="Torque"/> d = <see cref="Torque.From(Distance, Force, Angle)"/>;</code>
/// </item>
/// <item>
/// <code><see cref="Torque"/> e = <see cref="Work.AsTorque"/>;</code>
/// </item>
/// </list>
/// The magnitude of the <see cref="Torque"/> can be retrieved in the desired <see cref="UnitOfTorque"/> using pre-defined properties,
/// such as <see cref="NewtonMetres"/>
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

    /// <summary>The <see cref="Torque"/> of magnitude 1, when expressed in <see cref="UnitOfTorque.NewtonMetre"/>.</summary>
    public static Torque OneNewtonMetre { get; } = UnitOfTorque.NewtonMetre.Torque;

    /// <summary>The magnitude of the <see cref="Torque"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfTorque)"/> or a pre-defined property
    /// - such as <see cref="NewtonMetres"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Torque"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfTorque"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Torque"/>, expressed in <paramref name="unitOfTorque"/>.</param>
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
    /// <summary>Constructs a new <see cref="Torque"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfTorque"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Torque"/>, expressed in <paramref name="unitOfTorque"/>.</param>
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
    public Torque(double magnitude, UnitOfTorque unitOfTorque) : this(magnitude * unitOfTorque.Torque.Magnitude) { }
    /// <summary>Constructs a new <see cref="Torque"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Torque"/>.</param>
    /// <remarks>Consider preferring <see cref="Torque(Scalar, UnitOfTorque)"/>.</remarks>
    public Torque(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Torque"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Torque"/>.</param>
    /// <remarks>Consider preferring <see cref="Torque(double, UnitOfTorque)"/>.</remarks>
    public Torque(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="Torque"/> to an instance of the associated quantity <see cref="Energy"/>, of equal magnitude.</summary>
    public Energy AsEnergy => new(Magnitude);
    /// <summary>Converts the <see cref="Torque"/> to an instance of the associated quantity <see cref="Work"/>, of equal magnitude.</summary>
    public Work AsWork => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="Torque"/>, expressed in <see cref="UnitOfTorque.NewtonMetre"/>.</summary>
    public Scalar NewtonMetres => InUnit(UnitOfTorque.NewtonMetre);

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

    /// <summary>Computes the absolute of the <see cref="Torque"/>.</summary>
    public Torque Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="Torque"/>.</summary>
    public Torque Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="Torque"/>.</summary>
    public Torque Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="Torque"/> to the nearest integer value.</summary>
    public Torque Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Torque other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Torque"/> in the default unit
    /// <see cref="UnitOfTorque.NewtonMetre"/>, followed by the symbol [N∙m].</summary>
    public override string ToString() => $"{NewtonMetres} [N∙m]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Torque"/>,
    /// expressed in <paramref name="unitOfTorque"/>.</summary>
    /// <param name="unitOfTorque">The <see cref="UnitOfTorque"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfTorque unitOfTorque) => InUnit(this, unitOfTorque);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Torque"/>,
    /// expressed in <paramref name="unitOfTorque"/>.</summary>
    /// <param name="torque">The <see cref="Torque"/> to be expressed in <paramref name="unitOfTorque"/>.</param>
    /// <param name="unitOfTorque">The <see cref="UnitOfTorque"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Torque torque, UnitOfTorque unitOfTorque) => new(torque.Magnitude / unitOfTorque.Torque.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Torque"/>.</summary>
    public Torque Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Torque"/> with negated magnitude.</summary>
    public Torque Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Torque"/>.</param>
    public static Torque operator +(Torque x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Torque"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Torque"/>.</param>
    public static Torque operator -(Torque x) => x.Negate();

    /// <summary>Multiplicates the <see cref="Torque"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Torque"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Torque"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Torque"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Torque"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Torque"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Torque"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Torque x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Torque"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Torque"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Torque"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Torque y) => y.Multiply(x);
    /// <summary>Division of the <see cref="Torque"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Torque"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Torque"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Torque x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="Torque"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="Torque"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Torque"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, Torque y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="Torque"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Torque Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Torque"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Torque"/> is scaled.</param>
    public Torque Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Torque"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Torque"/> is divided.</param>
    public Torque Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Torque"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Torque"/> <paramref name="x"/> by this value.</param>
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

    /// <summary>Computes the remainder from division of the <see cref="Torque"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Torque Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Torque"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Torque"/> is scaled.</param>
    public Torque Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Torque"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Torque"/> is divided.</param>
    public Torque Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Torque"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Torque"/> <paramref name="x"/> by this value.</param>
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

    /// <summary>Multiplication of the <see cref="Torque"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Torque"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Torque"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Torque x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="Torque"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Torque"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Torque"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Torque x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Multiplicates the <see cref="Torque"/> with the <see cref="Vector3"/> <paramref name="factor"/> to produce
    /// a <see cref="Torque3"/>.</summary>
    /// <param name="factor">This <see cref="Vector3"/> is multiplied by the <see cref="Torque"/>.</param>
    public Torque3 Multiply(Vector3 factor) => new(factor * Magnitude);
    /// <summary>Multiplicates the <see cref="Torque"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="Torque3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Torque"/>.</param>
    public Torque3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplicates the <see cref="Torque"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="Torque3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Torque"/>.</param>
    public Torque3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplication of the <see cref="Torque"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/>
    /// to produce a <see cref="Torque3"/>.</summary>
    /// <param name="a">This <see cref="Torque"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Torque"/> <paramref name="a"/>.</param>
    public static Torque3 operator *(Torque a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Torque"/> <paramref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/>
    /// to produce a <see cref="Torque3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Torque"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Torque"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Torque3 operator *(Vector3 a, Torque b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="Torque"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="Torque3"/>.</summary>
    /// <param name="a">This <see cref="Torque"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Torque"/> <paramref name="a"/>.</param>
    public static Torque3 operator *(Torque a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Torque"/> <paramref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="Torque3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Torque"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Torque"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Torque3 operator *((double x, double y, double z) a, Torque b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="Torque"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="Torque3"/>.</summary>
    /// <param name="a">This <see cref="Torque"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Torque"/> <paramref name="a"/>.</param>
    public static Torque3 operator *(Torque a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Torque"/> <paramref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="Torque3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Torque"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Torque"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Torque3 operator *((Scalar x, Scalar y, Scalar z) a, Torque b) => b.Multiply(a);

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Torque"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Torque"/>.</param>
    public static bool operator <(Torque x, Torque y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Torque"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Torque"/>.</param>
    public static bool operator >(Torque x, Torque y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Torque"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Torque"/>.</param>
    public static bool operator <=(Torque x, Torque y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Torque"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Torque"/>.</param>
    public static bool operator >=(Torque x, Torque y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Torque"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(Torque x) => x.ToDouble();

    /// <summary>Converts the <see cref="Torque"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Torque x) => x.ToScalar();

    /// <summary>Constructs the <see cref="Torque"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Torque FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Torque"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Torque(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="Torque"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static Torque FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="Torque"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator Torque(Scalar x) => FromScalar(x);
}