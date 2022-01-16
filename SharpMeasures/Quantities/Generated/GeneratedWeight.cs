namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct Weight :
    IComparable<Weight>,
    IScalarQuantity<Weight>,
    IAddableScalarQuantity<Weight, Weight>,
    ISubtractableScalarQuantity<Weight, Weight>,
    IDivisibleScalarQuantity<Scalar, Weight>,
    IVector3izableScalarQuantity<Weight3>
{
    /// <summary>The zero-valued <see cref="Weight"/>.</summary>
    public static Weight Zero { get; } = new(0);

    /// <summary>The <see cref="Weight"/> with magnitude 1, when expressed in unit <see cref="UnitOfForce.Newton"/>.</summary>
    public static Weight OneNewton { get; } = new(1, UnitOfForce.Newton);
    /// <summary>The <see cref="Weight"/> with magnitude 1, when expressed in unit <see cref="UnitOfForce.PoundForce"/>.</summary>
    public static Weight OnePoundForce { get; } = new(1, UnitOfForce.PoundForce);

    /// <summary>The magnitude of the <see cref="Weight"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Weight.InNewtons"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Weight"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfForce"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Weight"/>, in unit <paramref name="unitOfForce"/>.</param>
    /// <param name="unitOfForce">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Weight"/> a = 2.6 * <see cref="Weight.OnePoundForce"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Weight(double magnitude, UnitOfForce unitOfForce) : this(magnitude * unitOfForce.Factor) { }
    /// <summary>Constructs a new <see cref="Weight"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Weight"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfForce"/> to be specified.</remarks>
    public Weight(double magnitude)
    {
        Magnitude = magnitude;
    }

    public Force AsForce => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="Weight"/>, expressed in unit <see cref="UnitOfForce.Newton"/>.</summary>
    public Scalar InNewtons => InUnit(UnitOfForce.Newton);
    /// <summary>Retrieves the magnitude of the <see cref="Weight"/>, expressed in unit <see cref="UnitOfForce.PoundForce"/>.</summary>
    public Scalar InPoundsForce => InUnit(UnitOfForce.PoundForce);

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
    public Weight Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public Weight Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public Weight Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public Weight Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Weight other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Weight"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [N]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Weight"/>, expressed in unit <paramref name="unitOfForce"/>.</summary>
    /// <param name="unitOfForce">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfForce unitOfForce) => InUnit(Magnitude, unitOfForce);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Weight"/>, expressed in unit <paramref name="unitOfForce"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="Weight"/>.</param>
    /// <param name="unitOfForce">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfForce unitOfForce) => new(magnitude / unitOfForce.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Weight"/>.</summary>
    public Weight Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Weight"/> with negated magnitude.</summary>
    public Weight Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Weight"/>.</param>
    public static Weight operator +(Weight x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Weight"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Weight"/>.</param>
    public static Weight operator -(Weight x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="Weight"/> <paramref name="term"/>, producing another <see cref="Weight"/>.</summary>
    /// <param name="term">This <see cref="Weight"/> is added to this instance.</param>
    public Weight Add(Weight term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="Weight"/> <paramref name="term"/> from this instance, producing another <see cref="Weight"/>.</summary>
    /// <param name="term">This <see cref="Weight"/> is subtracted from this instance.</param>
    public Weight Subtract(Weight term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="Weight"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="Weight"/>.</summary>
    /// <param name="x">This <see cref="Weight"/> is added to the <see cref="Weight"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="Weight"/> is added to the <see cref="Weight"/> <paramref name="x"/>.</param>
    public static Weight operator +(Weight x, Weight y) => x.Add(y);
    /// <summary>Subtract the <see cref="Weight"/> <paramref name="y"/> from the <see cref="Weight"/> <paramref name="x"/>, producing another <see cref="Weight"/>.</summary>
    /// <param name="x">The <see cref="Weight"/> <paramref name="y"/> is subtracted from this <see cref="Weight"/>.</param>
    /// <param name="y">This <see cref="Weight"/> is subtracted from the <see cref="Weight"/> <paramref name="x"/>.</param>
    public static Weight operator -(Weight x, Weight y) => x.Subtract(y);

    /// <summary>Divides this <see cref="Weight"/> by the <see cref="Weight"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Weight"/> is divided by this <see cref="Weight"/>.</param>
    public Scalar Divide(Weight divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="Weight"/> <paramref name="x"/> by the <see cref="Weight"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="Weight"/> is divided by the <see cref="Weight"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Weight"/> <paramref name="x"/> is divided by this <see cref="Weight"/>.</param>
    public static Scalar operator /(Weight x, Weight y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="Weight"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Weight"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Weight"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Weight"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Weight"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Weight"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Weight x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Weight"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Weight"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Weight x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Weight"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Weight Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Weight"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Weight"/> is scaled.</param>
    public Weight Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Weight"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Weight"/> is divided.</param>
    public Weight Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Weight"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Weight operator %(Weight x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Weight"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Weight"/> <paramref name="x"/>.</param>
    public static Weight operator *(Weight x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Weight"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Weight"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Weight"/>, which is scaled by <paramref name="x"/>.</param>
    public static Weight operator *(double x, Weight y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Weight"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Weight"/> <paramref name="x"/>.</param>
    public static Weight operator /(Weight x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="Weight"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Weight Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Weight"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Weight"/> is scaled.</param>
    public Weight Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Weight"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Weight"/> is divided.</param>
    public Weight Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Weight"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Weight operator %(Weight x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Weight"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Weight"/> <paramref name="x"/>.</param>
    public static Weight operator *(Weight x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Weight"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Weight"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Weight"/>, which is scaled by <paramref name="x"/>.</param>
    public static Weight operator *(Scalar x, Weight y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Weight"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Weight"/> <paramref name="x"/>.</param>
    public static Weight operator /(Weight x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="Weight"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Weight"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Weight"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Weight"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Weight"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Weight"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Weight.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Weight x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Weight"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Weight"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Weight.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Weight x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Multiplies the <see cref="Weight"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="Weight3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Weight"/>.</param>
    public Weight3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="Weight"/> with the <see cref="ValueTuple"/> <paramref name="vector"/> to produce a <see cref="Weight3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Weight"/>.</param>
    public Weight3 Multiply((double x, double y, double z) vector) => new(Magnitude * vector.x, Magnitude * vector.y, Magnitude * vector.z);
    /// <summary>Multiplies the <see cref="Weight"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="Weight3"/>.</summary>
    /// <param name="a">This <see cref="Weight"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Weight"/> <paramref name="a"/>.</param>
    public static Weight3 operator *(Weight a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Weight"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="Weight3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Weight"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Weight"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Weight3 operator *(Vector3 a, Weight b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="Weight"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="Weight3"/>.</summary>
    /// <param name="a">This <see cref="Weight"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="Weight"/> <paramref name="a"/>.</param>
    public static Weight3 operator *(Weight a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Weight"/> <paramref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="Weight3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="Weight"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Weight"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static Weight3 operator *((double x, double y, double z) a, Weight b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Weight x, Weight y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Weight x, Weight y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Weight x, Weight y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Weight x, Weight y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="Weight"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Weight x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Weight x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Weight"/> of magnitude <paramref name="x"/>.</summary>
    public static Weight FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Weight"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Weight(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Weight"/> of equivalent magnitude.</summary>
    public static Weight FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Weight"/> of equivalent magnitude.</summary>
    public static explicit operator Weight(Scalar x) => FromScalar(x);
}