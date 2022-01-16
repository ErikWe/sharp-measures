﻿namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct Torque :
    IComparable<Torque>,
    IScalarQuantity<Torque>,
    IAddableScalarQuantity<Torque, Torque>,
    ISubtractableScalarQuantity<Torque, Torque>,
    IDivisibleScalarQuantity<Scalar, Torque>,
    IVector3izableScalarQuantity<Torque3>
{
    /// <summary>The zero-valued <see cref="Torque"/>.</summary>
    public static Torque Zero { get; } = new(0);

    /// <summary>The <see cref="Torque"/> with magnitude 1, when expressed in unit <see cref="UnitOfTorque.NewtonMetre"/>.</summary>
    public static Torque OneNewtonMetre { get; } = new(1, UnitOfTorque.NewtonMetre);

    /// <summary>The magnitude of the <see cref="Torque"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Torque.InNewtonMetres"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Torque"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfTorque"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Torque"/>, in unit <paramref name="unitOfTorque"/>.</param>
    /// <param name="unitOfTorque">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Torque"/> a = 2.6 * <see cref="Torque.OneNewtonMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Torque(double magnitude, UnitOfTorque unitOfTorque) : this(magnitude * unitOfTorque.Factor) { }
    /// <summary>Constructs a new <see cref="Torque"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Torque"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfTorque"/> to be specified.</remarks>
    public Torque(double magnitude)
    {
        Magnitude = magnitude;
    }

    public PotentialEnergy AsPotentialEnergy => new(Magnitude);
    public KineticEnergy AsKineticEnergy => new(Magnitude);
    public Work AsWork => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="Torque"/>, expressed in unit <see cref="UnitOfTorque.NewtonMetre"/>.</summary>
    public Scalar InNewtonMetres => InUnit(UnitOfTorque.NewtonMetre);

    /// <inheritdoc/>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <inheritdoc/>
    public bool IsZero => Magnitude == 0;
    /// <inheritdoc/>
    public bool IsPositive => Magnitude > 0;
    /// <inheritdoc/>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <inheritdoc/>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <inheritdoc/>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <inheritdoc/>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <inheritdoc/>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <inheritdoc/>
    public Torque Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public Torque Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public Torque Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public Torque Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Torque other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Torque"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [N * m]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Torque"/>, expressed in unit <paramref name="unitOfTorque"/>.</summary>
    /// <param name="unitOfTorque">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfTorque unitOfTorque) => InUnit(Magnitude, unitOfTorque);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Torque"/>, expressed in unit <paramref name="unitOfTorque"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="Torque"/>.</param>
    /// <param name="unitOfTorque">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfTorque unitOfTorque) => new(magnitude / unitOfTorque.Factor);

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

    /// <summary>Adds this instance and the <see cref="Torque"/> <paramref name="term"/>, producing another <see cref="Torque"/>.</summary>
    /// <param name="term">This <see cref="Torque"/> is added to this instance.</param>
    public Torque Add(Torque term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="Torque"/> <paramref name="term"/> from this instance, producing another <see cref="Torque"/>.</summary>
    /// <param name="term">This <see cref="Torque"/> is subtracted from this instance.</param>
    public Torque Subtract(Torque term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="Torque"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="Torque"/>.</summary>
    /// <param name="x">This <see cref="Torque"/> is added to the <see cref="Torque"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="Torque"/> is added to the <see cref="Torque"/> <paramref name="x"/>.</param>
    public static Torque operator +(Torque x, Torque y) => x.Add(y);
    /// <summary>Subtract the <see cref="Torque"/> <paramref name="y"/> from the <see cref="Torque"/> <paramref name="x"/>, producing another <see cref="Torque"/>.</summary>
    /// <param name="x">The <see cref="Torque"/> <paramref name="y"/> is subtracted from this <see cref="Torque"/>.</param>
    /// <param name="y">This <see cref="Torque"/> is subtracted from the <see cref="Torque"/> <paramref name="x"/>.</param>
    public static Torque operator -(Torque x, Torque y) => x.Subtract(y);

    /// <summary>Divides this <see cref="Torque"/> by the <see cref="Torque"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Torque"/> is divided by this <see cref="Torque"/>.</param>
    public Scalar Divide(Torque divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="Torque"/> <paramref name="x"/> by the <see cref="Torque"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="Torque"/> is divided by the <see cref="Torque"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Torque"/> <paramref name="x"/> is divided by this <see cref="Torque"/>.</param>
    public static Scalar operator /(Torque x, Torque y) => x.Divide(y)
;

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
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
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
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
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
    /// <summary>Multiplies the <see cref="Torque"/> with the <see cref="ValueTuple"/> <paramref name="vector"/> to produce a <see cref="Torque3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Torque"/>.</param>
    public Torque3 Multiply((double x, double y, double z) vector) => new(Magnitude * vector.x, Magnitude * vector.y, Magnitude * vector.z);
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
    /// <summary>Multiplies the <see cref="Torque"/> <paramref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="Torque3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="Torque"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Torque"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static Torque3 operator *((double x, double y, double z) a, Torque b) => b.Multiply(a);

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
    public static bool operator <=(Torque x, Torque y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Torque x, Torque y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="Torque"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Torque x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
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