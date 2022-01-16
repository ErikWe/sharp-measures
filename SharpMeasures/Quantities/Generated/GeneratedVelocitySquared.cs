namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct VelocitySquared :
    IComparable<VelocitySquared>,
    IScalarQuantity<VelocitySquared>,
    ISquareRootableScalarQuantity<Velocity>,
    IAddableScalarQuantity<VelocitySquared, VelocitySquared>,
    ISubtractableScalarQuantity<VelocitySquared, VelocitySquared>,
    IDivisibleScalarQuantity<Scalar, VelocitySquared>,
    IVector3izableScalarQuantity<VelocitySquared3>
{
    /// <summary>The zero-valued <see cref="VelocitySquared"/>.</summary>
    public static VelocitySquared Zero { get; } = new(0);

    /// <summary>The <see cref="VelocitySquared"/> with magnitude 1, when expressed in unit <see cref="UnitOfVelocitySquared.SquareMetrePerSecondSquared"/>.</summary>
    public static VelocitySquared OneSquareMetrePerSecondSquared { get; } = new(1, UnitOfVelocitySquared.SquareMetrePerSecondSquared);

    /// <summary>Constructs a <see cref="VelocitySquared"/> by squaring the <see cref="Velocity"/> <paramref name="velocity"/>.</summary>
    /// <param name="velocity">This <see cref="Velocity"/> is squared to produce a <see cref="#Quantity"/>.</param>
    public static VelocitySquared From(Velocity velocity) => new(Math.Pow(velocity.InMetresPerSecond, 2));

    /// <summary>The magnitude of the <see cref="VelocitySquared"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="VelocitySquared.InSquareMetresPerSecondSquared"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="VelocitySquared"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfVelocitySquared"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="VelocitySquared"/>, in unit <paramref name="unitOfVelocitySquared"/>.</param>
    /// <param name="unitOfVelocitySquared">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="VelocitySquared"/> a = 2.6 * <see cref="VelocitySquared.OneSquareMetrePerSecondSquared"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public VelocitySquared(double magnitude, UnitOfVelocitySquared unitOfVelocitySquared) : this(magnitude * unitOfVelocitySquared.Factor) { }
    /// <summary>Constructs a new <see cref="VelocitySquared"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="VelocitySquared"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfVelocitySquared"/> to be specified.</remarks>
    public VelocitySquared(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="VelocitySquared"/>, expressed in unit <see cref="UnitOfVelocitySquared.SquareMetrePerSecondSquared"/>.</summary>
    public Scalar InSquareMetresPerSecondSquared => InUnit(UnitOfVelocitySquared.SquareMetrePerSecondSquared);

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
    public VelocitySquared Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public VelocitySquared Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public VelocitySquared Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public VelocitySquared Round() => new(Math.Round(Magnitude));

    /// <summary>Takes the square root of the <see cref="VelocitySquared"/>, producing a <see cref="Velocity"/>.</summary>
    public Velocity SquareRoot() => Velocity.From(this);

    /// <inheritdoc/>
    public int CompareTo(VelocitySquared other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="VelocitySquared"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [m^2 * s^-2]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="VelocitySquared"/>, expressed in unit <paramref name="unitOfVelocitySquared"/>.</summary>
    /// <param name="unitOfVelocitySquared">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfVelocitySquared unitOfVelocitySquared) => InUnit(Magnitude, unitOfVelocitySquared);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="VelocitySquared"/>, expressed in unit <paramref name="unitOfVelocitySquared"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="VelocitySquared"/>.</param>
    /// <param name="unitOfVelocitySquared">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfVelocitySquared unitOfVelocitySquared) => new(magnitude / unitOfVelocitySquared.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="VelocitySquared"/>.</summary>
    public VelocitySquared Plus() => this;
    /// <summary>Negation, resulting in a <see cref="VelocitySquared"/> with negated magnitude.</summary>
    public VelocitySquared Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="VelocitySquared"/>.</param>
    public static VelocitySquared operator +(VelocitySquared x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="VelocitySquared"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="VelocitySquared"/>.</param>
    public static VelocitySquared operator -(VelocitySquared x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="VelocitySquared"/> <paramref name="term"/>, producing another <see cref="VelocitySquared"/>.</summary>
    /// <param name="term">This <see cref="VelocitySquared"/> is added to this instance.</param>
    public VelocitySquared Add(VelocitySquared term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="VelocitySquared"/> <paramref name="term"/> from this instance, producing another <see cref="VelocitySquared"/>.</summary>
    /// <param name="term">This <see cref="VelocitySquared"/> is subtracted from this instance.</param>
    public VelocitySquared Subtract(VelocitySquared term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="VelocitySquared"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="VelocitySquared"/>.</summary>
    /// <param name="x">This <see cref="VelocitySquared"/> is added to the <see cref="VelocitySquared"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="VelocitySquared"/> is added to the <see cref="VelocitySquared"/> <paramref name="x"/>.</param>
    public static VelocitySquared operator +(VelocitySquared x, VelocitySquared y) => x.Add(y);
    /// <summary>Subtract the <see cref="VelocitySquared"/> <paramref name="y"/> from the <see cref="VelocitySquared"/> <paramref name="x"/>, producing another <see cref="VelocitySquared"/>.</summary>
    /// <param name="x">The <see cref="VelocitySquared"/> <paramref name="y"/> is subtracted from this <see cref="VelocitySquared"/>.</param>
    /// <param name="y">This <see cref="VelocitySquared"/> is subtracted from the <see cref="VelocitySquared"/> <paramref name="x"/>.</param>
    public static VelocitySquared operator -(VelocitySquared x, VelocitySquared y) => x.Subtract(y);

    /// <summary>Divides this <see cref="VelocitySquared"/> by the <see cref="VelocitySquared"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="VelocitySquared"/> is divided by this <see cref="VelocitySquared"/>.</param>
    public Scalar Divide(VelocitySquared divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="VelocitySquared"/> <paramref name="x"/> by the <see cref="VelocitySquared"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="VelocitySquared"/> is divided by the <see cref="VelocitySquared"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="VelocitySquared"/> <paramref name="x"/> is divided by this <see cref="VelocitySquared"/>.</param>
    public static Scalar operator /(VelocitySquared x, VelocitySquared y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="VelocitySquared"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="VelocitySquared"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="VelocitySquared"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="VelocitySquared"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="VelocitySquared"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="VelocitySquared"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="VelocitySquared"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(VelocitySquared x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="VelocitySquared"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="VelocitySquared"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="VelocitySquared"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(VelocitySquared x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="VelocitySquared"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public VelocitySquared Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="VelocitySquared"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="VelocitySquared"/> is scaled.</param>
    public VelocitySquared Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="VelocitySquared"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="VelocitySquared"/> is divided.</param>
    public VelocitySquared Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="VelocitySquared"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="VelocitySquared"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static VelocitySquared operator %(VelocitySquared x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="VelocitySquared"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="VelocitySquared"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="VelocitySquared"/> <paramref name="x"/>.</param>
    public static VelocitySquared operator *(VelocitySquared x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="VelocitySquared"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="VelocitySquared"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="VelocitySquared"/>, which is scaled by <paramref name="x"/>.</param>
    public static VelocitySquared operator *(double x, VelocitySquared y) => y.Multiply(x);
    /// <summary>Scales the <see cref="VelocitySquared"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="VelocitySquared"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="VelocitySquared"/> <paramref name="x"/>.</param>
    public static VelocitySquared operator /(VelocitySquared x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="VelocitySquared"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public VelocitySquared Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="VelocitySquared"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="VelocitySquared"/> is scaled.</param>
    public VelocitySquared Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="VelocitySquared"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="VelocitySquared"/> is divided.</param>
    public VelocitySquared Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="VelocitySquared"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="VelocitySquared"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static VelocitySquared operator %(VelocitySquared x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="VelocitySquared"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="VelocitySquared"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="VelocitySquared"/> <paramref name="x"/>.</param>
    public static VelocitySquared operator *(VelocitySquared x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="VelocitySquared"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="VelocitySquared"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="VelocitySquared"/>, which is scaled by <paramref name="x"/>.</param>
    public static VelocitySquared operator *(Scalar x, VelocitySquared y) => y.Multiply(x);
    /// <summary>Scales the <see cref="VelocitySquared"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="VelocitySquared"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="VelocitySquared"/> <paramref name="x"/>.</param>
    public static VelocitySquared operator /(VelocitySquared x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="VelocitySquared"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="VelocitySquared"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="VelocitySquared"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="VelocitySquared"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="VelocitySquared"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="VelocitySquared"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="VelocitySquared"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="VelocitySquared.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(VelocitySquared x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="VelocitySquared"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="VelocitySquared"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="VelocitySquared"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="VelocitySquared.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(VelocitySquared x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Multiplies the <see cref="VelocitySquared"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="VelocitySquared3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="VelocitySquared"/>.</param>
    public VelocitySquared3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="VelocitySquared"/> with the <see cref="ValueTuple"/> <paramref name="vector"/> to produce a <see cref="VelocitySquared3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="VelocitySquared"/>.</param>
    public VelocitySquared3 Multiply((double x, double y, double z) vector) => new(Magnitude * vector.x, Magnitude * vector.y, Magnitude * vector.z);
    /// <summary>Multiplies the <see cref="VelocitySquared"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="VelocitySquared3"/>.</summary>
    /// <param name="a">This <see cref="VelocitySquared"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="VelocitySquared"/> <paramref name="a"/>.</param>
    public static VelocitySquared3 operator *(VelocitySquared a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="VelocitySquared"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="VelocitySquared3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="VelocitySquared"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="VelocitySquared"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static VelocitySquared3 operator *(Vector3 a, VelocitySquared b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="VelocitySquared"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="VelocitySquared3"/>.</summary>
    /// <param name="a">This <see cref="VelocitySquared"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="VelocitySquared"/> <paramref name="a"/>.</param>
    public static VelocitySquared3 operator *(VelocitySquared a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="VelocitySquared"/> <paramref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="VelocitySquared3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="VelocitySquared"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="VelocitySquared"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static VelocitySquared3 operator *((double x, double y, double z) a, VelocitySquared b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(VelocitySquared x, VelocitySquared y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(VelocitySquared x, VelocitySquared y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(VelocitySquared x, VelocitySquared y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(VelocitySquared x, VelocitySquared y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="VelocitySquared"/> <paramref name="x"/>.</summary>
    public static implicit operator double(VelocitySquared x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(VelocitySquared x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="VelocitySquared"/> of magnitude <paramref name="x"/>.</summary>
    public static VelocitySquared FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="VelocitySquared"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator VelocitySquared(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="VelocitySquared"/> of equivalent magnitude.</summary>
    public static VelocitySquared FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="VelocitySquared"/> of equivalent magnitude.</summary>
    public static explicit operator VelocitySquared(Scalar x) => FromScalar(x);
}