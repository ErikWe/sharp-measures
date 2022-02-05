﻿namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="GravitationalAcceleration"/>, describing <see cref="Acceleration"/> caused by gravity.
/// This is the magnitude of the vector quantity <see cref="GravitationalAcceleration3"/>, and is expressed in <see cref="UnitOfAcceleration"/>,
/// with the SI unit being [m / s²].
/// <para>
/// New instances of <see cref="GravitationalAcceleration"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfAcceleration"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="GravitationalAcceleration"/> a = 3 * <see cref="GravitationalAcceleration.OneStandardGravity"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="GravitationalAcceleration"/> d = <see cref="GravitationalAcceleration.From(Weight, Mass)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfAcceleration"/>.
/// </para>
/// </summary>
/// <remarks>
/// <see cref="GravitationalAcceleration"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="Acceleration"/></term>
/// <description>A more general form of <see cref="GravitationalAcceleration"/>, describing any form of acceleration.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct GravitationalAcceleration :
    IComparable<GravitationalAcceleration>,
    IScalarQuantity,
    IScalableScalarQuantity<GravitationalAcceleration>,
    IMultiplicableScalarQuantity<GravitationalAcceleration, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<GravitationalAcceleration, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<GravitationalAcceleration3, Vector3>
{
    /// <summary>The zero-valued <see cref="GravitationalAcceleration"/>.</summary>
    public static GravitationalAcceleration Zero { get; } = new(0);

    /// <summary>The <see cref="GravitationalAcceleration"/> with magnitude 1, when expressed in unit <see cref="UnitOfAcceleration.StandardGravity"/>.</summary>
    public static GravitationalAcceleration OneStandardGravity { get; } = new(1, UnitOfAcceleration.StandardGravity);
    /// <summary>The <see cref="GravitationalAcceleration"/> with magnitude 1, when expressed in unit <see cref="UnitOfAcceleration.MetrePerSecondSquared"/>.</summary>
    public static GravitationalAcceleration OneMetrePerSecondSquared { get; } = new(1, UnitOfAcceleration.MetrePerSecondSquared);

    /// <summary>The magnitude of the <see cref="GravitationalAcceleration"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="GravitationalAcceleration.InStandardGravity"/>.
    /// <para>This value should only be used (to maximize efficiency) when implementing mathematical operations with other quantities.</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="GravitationalAcceleration"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfAcceleration"/> <paramref name="unitOfAcceleration"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="GravitationalAcceleration"/>, in <see cref="UnitOfAcceleration"/> <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="unitOfAcceleration">The <see cref="UnitOfAcceleration"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="GravitationalAcceleration"/> a = 3 * <see cref="GravitationalAcceleration.OneStandardGravity"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public GravitationalAcceleration(Scalar magnitude, UnitOfAcceleration unitOfAcceleration) : this(magnitude.Magnitude, unitOfAcceleration) { }
    /// <summary>Constructs a new <see cref="GravitationalAcceleration"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfAcceleration"/> <paramref name="unitOfAcceleration"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="GravitationalAcceleration"/>, in <see cref="UnitOfAcceleration"/> <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="unitOfAcceleration">The <see cref="UnitOfAcceleration"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="GravitationalAcceleration"/> a = 3 * <see cref="GravitationalAcceleration.OneStandardGravity"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public GravitationalAcceleration(double magnitude, UnitOfAcceleration unitOfAcceleration) : this(magnitude * unitOfAcceleration.Factor) { }
    /// <summary>Constructs a new <see cref="GravitationalAcceleration"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="GravitationalAcceleration"/>.</param>
    /// <remarks>Consider preffering a constructor that requires a <see cref="UnitOfAcceleration"/> to be specified.</remarks>
    public GravitationalAcceleration(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="GravitationalAcceleration"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="GravitationalAcceleration"/>.</param>
    /// <remarks>Consider preferring a constructor that requires a <see cref="UnitOfAcceleration"/> to be specified.</remarks>
    public GravitationalAcceleration(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="GravitationalAcceleration"/> to an instance of the associated quantity <see cref="Acceleration"/>, of equal magnitude.</summary>
    public Acceleration AsAcceleration => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="GravitationalAcceleration"/>, expressed in unit <see cref="UnitOfAcceleration.StandardGravity"/>.</summary>
    public Scalar InStandardGravity => InUnit(UnitOfAcceleration.StandardGravity);
    /// <summary>Retrieves the magnitude of the <see cref="GravitationalAcceleration"/>, expressed in unit <see cref="UnitOfAcceleration.MetrePerSecondSquared"/>.</summary>
    public Scalar InMetresPerSecondSquared => InUnit(UnitOfAcceleration.MetrePerSecondSquared);

    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalAcceleration"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalAcceleration"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalAcceleration"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalAcceleration"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalAcceleration"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalAcceleration"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalAcceleration"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalAcceleration"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="GravitationalAcceleration"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public GravitationalAcceleration Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="GravitationalAcceleration"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public GravitationalAcceleration Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="GravitationalAcceleration"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public GravitationalAcceleration Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="GravitationalAcceleration"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public GravitationalAcceleration Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(GravitationalAcceleration other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="GravitationalAcceleration"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [m / s^2]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="GravitationalAcceleration"/>, expressed in <see cref="UnitOfAcceleration"/>
    /// <paramref name="unitOfAcceleration"/>.</summary>
    /// <param name="unitOfAcceleration">The <see cref="UnitOfAcceleration"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfAcceleration unitOfAcceleration) => InUnit(this, unitOfAcceleration);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="GravitationalAcceleration"/>, expressed in <see cref="UnitOfAcceleration"/>
    /// <paramref name="unitOfAcceleration"/>.</summary>
    /// <param name="gravitationalAcceleration">The <see cref="GravitationalAcceleration"/> to be expressed in <see cref="UnitOfAcceleration"/> <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="unitOfAcceleration">The <see cref="UnitOfAcceleration"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(GravitationalAcceleration gravitationalAcceleration, UnitOfAcceleration unitOfAcceleration) => new(gravitationalAcceleration.Magnitude / unitOfAcceleration.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="GravitationalAcceleration"/>.</summary>
    public GravitationalAcceleration Plus() => this;
    /// <summary>Negation, resulting in a <see cref="GravitationalAcceleration"/> with negated magnitude.</summary>
    public GravitationalAcceleration Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="GravitationalAcceleration"/>.</param>
    public static GravitationalAcceleration operator +(GravitationalAcceleration x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="GravitationalAcceleration"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="GravitationalAcceleration"/>.</param>
    public static GravitationalAcceleration operator -(GravitationalAcceleration x) => x.Negate();

    /// <summary>Multiplies the <see cref="GravitationalAcceleration"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="GravitationalAcceleration"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="GravitationalAcceleration"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="GravitationalAcceleration"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="GravitationalAcceleration"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="GravitationalAcceleration"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(GravitationalAcceleration x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="GravitationalAcceleration"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="GravitationalAcceleration"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="GravitationalAcceleration"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, GravitationalAcceleration y) => y.Multiply(x);
    /// <summary>Divides the <see cref="GravitationalAcceleration"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="GravitationalAcceleration"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(GravitationalAcceleration x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="GravitationalAcceleration"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public GravitationalAcceleration Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="GravitationalAcceleration"/> is scaled.</param>
    public GravitationalAcceleration Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="GravitationalAcceleration"/> is divided.</param>
    public GravitationalAcceleration Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="GravitationalAcceleration"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="GravitationalAcceleration"/> <paramref name="x"/> by this value.</param>
    public static GravitationalAcceleration operator %(GravitationalAcceleration x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="GravitationalAcceleration"/> <paramref name="x"/>.</param>
    public static GravitationalAcceleration operator *(GravitationalAcceleration x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="GravitationalAcceleration"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="GravitationalAcceleration"/>, which is scaled by <paramref name="x"/>.</param>
    public static GravitationalAcceleration operator *(double x, GravitationalAcceleration y) => y.Multiply(x);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="GravitationalAcceleration"/> <paramref name="x"/>.</param>
    public static GravitationalAcceleration operator /(GravitationalAcceleration x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="GravitationalAcceleration"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public GravitationalAcceleration Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="GravitationalAcceleration"/> is scaled.</param>
    public GravitationalAcceleration Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="GravitationalAcceleration"/> is divided.</param>
    public GravitationalAcceleration Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="GravitationalAcceleration"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="GravitationalAcceleration"/> <paramref name="x"/> by this value.</param>
    public static GravitationalAcceleration operator %(GravitationalAcceleration x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="GravitationalAcceleration"/> <paramref name="x"/>.</param>
    public static GravitationalAcceleration operator *(GravitationalAcceleration x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="GravitationalAcceleration"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="GravitationalAcceleration"/>, which is scaled by <paramref name="x"/>.</param>
    public static GravitationalAcceleration operator *(Scalar x, GravitationalAcceleration y) => y.Multiply(x);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="GravitationalAcceleration"/> <paramref name="x"/>.</param>
    public static GravitationalAcceleration operator /(GravitationalAcceleration x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="GravitationalAcceleration"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="GravitationalAcceleration"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="GravitationalAcceleration"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="GravitationalAcceleration"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="GravitationalAcceleration"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="GravitationalAcceleration"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="GravitationalAcceleration.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(GravitationalAcceleration x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="GravitationalAcceleration"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="GravitationalAcceleration"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="GravitationalAcceleration.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(GravitationalAcceleration x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Multiplies the <see cref="GravitationalAcceleration"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="GravitationalAcceleration3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="GravitationalAcceleration"/>.</param>
    public GravitationalAcceleration3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="GravitationalAcceleration"/> with the <see cref="ValueTuple"/> <paramref name="components"/> to produce a <see cref="GravitationalAcceleration3"/>.</summary>
    /// <param name="components">This <see cref="ValueTuple"/> is multiplied by the <see cref="GravitationalAcceleration"/>.</param>
    public GravitationalAcceleration3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="GravitationalAcceleration"/> with the <see cref="ValueTuple"/> <paramref name="components"/> to produce a <see cref="GravitationalAcceleration3"/>.</summary>
    /// <param name="components">This <see cref="ValueTuple"/> is multiplied by the <see cref="GravitationalAcceleration"/>.</param>
    public GravitationalAcceleration3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="GravitationalAcceleration"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="GravitationalAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="GravitationalAcceleration"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="GravitationalAcceleration"/> <paramref name="a"/>.</param>
    public static GravitationalAcceleration3 operator *(GravitationalAcceleration a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="GravitationalAcceleration"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="GravitationalAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="GravitationalAcceleration"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="GravitationalAcceleration"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static GravitationalAcceleration3 operator *(Vector3 a, GravitationalAcceleration b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="GravitationalAcceleration"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="GravitationalAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="GravitationalAcceleration"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="GravitationalAcceleration"/> <paramref name="a"/>.</param>
    public static GravitationalAcceleration3 operator *(GravitationalAcceleration a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="GravitationalAcceleration"/> <parmref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="GravitationalAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="GravitationalAcceleration"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="GravitationalAcceleration"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static GravitationalAcceleration3 operator *((double x, double y, double z) a, GravitationalAcceleration b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="GravitationalAcceleration"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="GravitationalAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="GravitationalAcceleration"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="GravitationalAcceleration"/> <paramref name="a"/>.</param>
    public static GravitationalAcceleration3 operator *(GravitationalAcceleration a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="GravitationalAcceleration"/> <parmref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="GravitationalAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="GravitationalAcceleration"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="GravitationalAcceleration"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static GravitationalAcceleration3 operator *((Scalar x, Scalar y, Scalar z) a, GravitationalAcceleration b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(GravitationalAcceleration x, GravitationalAcceleration y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(GravitationalAcceleration x, GravitationalAcceleration y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(GravitationalAcceleration x, GravitationalAcceleration y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(GravitationalAcceleration x, GravitationalAcceleration y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="GravitationalAcceleration"/> to a <see cref="double"/> with value <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts the <see cref="GravitationalAcceleration"/> to a <see cref="double"/> based on the magnitude of the <see cref="GravitationalAcceleration"/> <paramref name="x"/>.</summary>
    public static implicit operator double(GravitationalAcceleration x) => x.ToDouble();

    /// <summary>Converts the <see cref="GravitationalAcceleration"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts the <see cref="GravitationalAcceleration"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(GravitationalAcceleration x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="GravitationalAcceleration"/> of magnitude <paramref name="x"/>.</summary>
    public static GravitationalAcceleration FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="GravitationalAcceleration"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator GravitationalAcceleration(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="GravitationalAcceleration"/> of equivalent magnitude.</summary>
    public static GravitationalAcceleration FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="GravitationalAcceleration"/> of equivalent magnitude.</summary>
    public static explicit operator GravitationalAcceleration(Scalar x) => FromScalar(x);
}
