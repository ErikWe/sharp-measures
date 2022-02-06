﻿namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Acceleration"/>, describing change in <see cref="Speed"/> over <see cref="Time"/>.
/// This is the magnitude of the vector quantity <see cref="Acceleration3"/>, and is expressed in <see cref="UnitOfAcceleration"/>, with the SI unit being [m / s²].
/// <para>
/// New instances of <see cref="Acceleration"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfAcceleration"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Acceleration"/> a = 3 * <see cref="Acceleration.OneMetrePerSecondSquared"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Acceleration"/> d = <see cref="Acceleration.From(Speed, Time)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Acceleration"/> e = <see cref="GravitationalAcceleration.AsAcceleration"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="Acceleration"/> can be retrieved in the desired <see cref="UnitOfAcceleration"/> using pre-defined properties,
/// such as <see cref="MetresPerSecondSquared"/>
/// </para>
/// </summary>
/// <remarks>
/// <see cref="Acceleration"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="GravitationalAcceleration"/></term>
/// <description>Describes <see cref="Acceleration"/> caused specifically by gravity.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct Acceleration :
    IComparable<Acceleration>,
    IScalarQuantity,
    IScalableScalarQuantity<Acceleration>,
    IMultiplicableScalarQuantity<Acceleration, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Acceleration, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<Acceleration3, Vector3>
{
    /// <summary>The zero-valued <see cref="Acceleration"/>.</summary>
    public static Acceleration Zero { get; } = new(0);

    /// <summary>The <see cref="Acceleration"/> with magnitude 1, when expressed in unit <see cref="UnitOfAcceleration.MetrePerSecondSquared"/>.</summary>
    public static Acceleration OneMetrePerSecondSquared { get; } = new(1, UnitOfAcceleration.MetrePerSecondSquared);

    /// <summary>The magnitude of the <see cref="Acceleration"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAcceleration)"/> or a pre-defined property
    /// - such as <see cref="MetresPerSecondSquared"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Acceleration"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAcceleration"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Acceleration"/>, expressed in <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="unitOfAcceleration">The <see cref="UnitOfAcceleration"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Acceleration"/> a = 3 * <see cref="Acceleration.OneMetrePerSecondSquared"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Acceleration(Scalar magnitude, UnitOfAcceleration unitOfAcceleration) : this(magnitude.Magnitude, unitOfAcceleration) { }
    /// <summary>Constructs a new <see cref="Acceleration"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAcceleration"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Acceleration"/>, expressed in <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="unitOfAcceleration">The <see cref="UnitOfAcceleration"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Acceleration"/> a = 3 * <see cref="Acceleration.OneMetrePerSecondSquared"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Acceleration(double magnitude, UnitOfAcceleration unitOfAcceleration) : this(magnitude * unitOfAcceleration.Factor) { }
    /// <summary>Constructs a new <see cref="Acceleration"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Acceleration"/>.</param>
    /// <remarks>Consider preferring <see cref="Acceleration(Scalar, UnitOfAcceleration)"/>.</remarks>
    public Acceleration(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Acceleration"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Acceleration"/>.</param>
    /// <remarks>Consider preferring <see cref="Acceleration(double, UnitOfAcceleration)"/>.</remarks>
    public Acceleration(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Acceleration"/>, expressed in <see cref="UnitOfAcceleration.MetrePerSecondSquared"/>.</summary>
    public Scalar MetresPerSecondSquared => InUnit(UnitOfAcceleration.MetrePerSecondSquared);

    /// <summary>Indicates whether the magnitude of the <see cref="Acceleration"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Acceleration"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Acceleration"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Acceleration"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Acceleration"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Acceleration"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Acceleration"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Acceleration"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="Acceleration"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public Acceleration Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="Acceleration"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public Acceleration Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="Acceleration"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public Acceleration Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="Acceleration"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public Acceleration Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Acceleration other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Acceleration"/> (in SI units), and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [m / s^2]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Acceleration"/>,
    /// expressed in <paramref name="unitOfAcceleration"/>.</summary>
    /// <param name="unitOfAcceleration">The <see cref="UnitOfAcceleration"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfAcceleration unitOfAcceleration) => InUnit(this, unitOfAcceleration);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Acceleration"/>,
    /// expressed in <paramref name="unitOfAcceleration"/>.</summary>
    /// <param name="acceleration">The <see cref="Acceleration"/> to be expressed in <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="unitOfAcceleration">The <see cref="UnitOfAcceleration"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Acceleration acceleration, UnitOfAcceleration unitOfAcceleration) => new(acceleration.Magnitude / unitOfAcceleration.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Acceleration"/>.</summary>
    public Acceleration Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Acceleration"/> with negated magnitude.</summary>
    public Acceleration Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Acceleration"/>.</param>
    public static Acceleration operator +(Acceleration x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Acceleration"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Acceleration"/>.</param>
    public static Acceleration operator -(Acceleration x) => x.Negate();

    /// <summary>Multiplies the <see cref="Acceleration"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Acceleration"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Acceleration"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Acceleration"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Acceleration"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Acceleration"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Acceleration"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Acceleration x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Acceleration"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Acceleration"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Acceleration"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Acceleration y) => y.Multiply(x);
    /// <summary>Divides the <see cref="Acceleration"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Acceleration"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Acceleration"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Acceleration x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Acceleration"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Acceleration Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Acceleration"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Acceleration"/> is scaled.</param>
    public Acceleration Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Acceleration"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Acceleration"/> is divided.</param>
    public Acceleration Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Acceleration"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Acceleration"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="Acceleration"/> <paramref name="x"/> by this value.</param>
    public static Acceleration operator %(Acceleration x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Acceleration"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Acceleration"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Acceleration"/> <paramref name="x"/>.</param>
    public static Acceleration operator *(Acceleration x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Acceleration"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Acceleration"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Acceleration"/>, which is scaled by <paramref name="x"/>.</param>
    public static Acceleration operator *(double x, Acceleration y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Acceleration"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Acceleration"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Acceleration"/> <paramref name="x"/>.</param>
    public static Acceleration operator /(Acceleration x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="Acceleration"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Acceleration Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Acceleration"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Acceleration"/> is scaled.</param>
    public Acceleration Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Acceleration"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Acceleration"/> is divided.</param>
    public Acceleration Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Acceleration"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Acceleration"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="Acceleration"/> <paramref name="x"/> by this value.</param>
    public static Acceleration operator %(Acceleration x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Acceleration"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Acceleration"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Acceleration"/> <paramref name="x"/>.</param>
    public static Acceleration operator *(Acceleration x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Acceleration"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Acceleration"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Acceleration"/>, which is scaled by <paramref name="x"/>.</param>
    public static Acceleration operator *(Scalar x, Acceleration y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Acceleration"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Acceleration"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Acceleration"/> <paramref name="x"/>.</param>
    public static Acceleration operator /(Acceleration x, Scalar y) => x.Divide(y);

    /// <inheritdoc/>
    public TProductScalarQuantity Multiply<TProductScalarQuantity, TFactorScalarQuantity>(TFactorScalarQuantity factor, Func<double, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorScalarQuantity : IScalarQuantity
        => factory(Magnitude * factor.Magnitude);
    /// <inheritdoc/>
    public TQuotientScalarQuantity Divide<TQuotientScalarQuantity, TDivisorScalarQuantity>(TDivisorScalarQuantity divisor, Func<double, TQuotientScalarQuantity> factory)
        where TQuotientScalarQuantity : IScalarQuantity
        where TDivisorScalarQuantity : IScalarQuantity
        => factory(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Acceleration"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Acceleration"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Acceleration"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity, Func{double, TProductScalarQuantity})"/>.</remarks>
    public static Unhandled operator *(Acceleration x, IScalarQuantity y) => x.Multiply<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));
    /// <summary>Divides the <see cref="Acceleration"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Acceleration"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Acceleration"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity, Func{double, TQuotientScalarQuantity})"/>.</remarks>
    public static Unhandled operator /(Acceleration x, IScalarQuantity y) => x.Divide<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));

    /// <summary>Multiplies the <see cref="Acceleration"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="Acceleration3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Acceleration"/>.</param>
    public Acceleration3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="Acceleration"/> with the values of <paramref name="components"/> to produce a <see cref="Acceleration3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Acceleration"/>.</param>
    public Acceleration3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="Acceleration"/> with the values of <paramref name="components"/> to produce a <see cref="Acceleration3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Acceleration"/>.</param>
    public Acceleration3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="Acceleration"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="Acceleration3"/>.</summary>
    /// <param name="a">This <see cref="Acceleration"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Acceleration"/> <paramref name="a"/>.</param>
    public static Acceleration3 operator *(Acceleration a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Acceleration"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="Acceleration3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Acceleration"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Acceleration"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Acceleration3 operator *(Vector3 a, Acceleration b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="Acceleration"/> <paramref name="a"/> with the values of <paramref name="b"/> to produce a <see cref="Acceleration3"/>.</summary>
    /// <param name="a">This <see cref="Acceleration"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Acceleration"/> <paramref name="a"/>.</param>
    public static Acceleration3 operator *(Acceleration a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Acceleration"/> <parmref name="b"/> with the values of <paramref name="a"/> to produce a <see cref="Acceleration3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Acceleration"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Acceleration"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Acceleration3 operator *((double x, double y, double z) a, Acceleration b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="Acceleration"/> <paramref name="a"/> with the values of <paramref name="b"/> to produce a <see cref="Acceleration3"/>.</summary>
    /// <param name="a">This <see cref="Acceleration"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Acceleration"/> <paramref name="a"/>.</param>
    public static Acceleration3 operator *(Acceleration a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Acceleration"/> <parmref name="b"/> with the values of <paramref name="a"/> to produce a <see cref="Acceleration3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Acceleration"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Acceleration"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Acceleration3 operator *((Scalar x, Scalar y, Scalar z) a, Acceleration b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Acceleration x, Acceleration y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Acceleration x, Acceleration y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Acceleration x, Acceleration y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Acceleration x, Acceleration y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Acceleration"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static implicit operator double(Acceleration x) => x.ToDouble();

    /// <summary>Converts the <see cref="Acceleration"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Acceleration x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Acceleration"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Acceleration FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Acceleration"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Acceleration(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Acceleration"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static Acceleration FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Acceleration"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Acceleration(Scalar x) => FromScalar(x);
}
