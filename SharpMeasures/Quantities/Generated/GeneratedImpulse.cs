namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct Impulse :
    IComparable<Impulse>,
    IScalarQuantity<Impulse>,
    IAddableScalarQuantity<Impulse, Impulse>,
    ISubtractableScalarQuantity<Impulse, Impulse>,
    IDivisibleScalarQuantity<Scalar, Impulse>,
    IVector3izableScalarQuantity<Impulse3>
{
    /// <summary>The zero-valued <see cref="Impulse"/>.</summary>
    public static Impulse Zero { get; } = new(0);

    /// <summary>The <see cref="Impulse"/> with magnitude 1, when expressed in unit <see cref="UnitOfImpulse.NewtonSecond"/>.</summary>
    public static Impulse OneNewtonSecond { get; } = new(1, UnitOfImpulse.NewtonSecond);

    /// <summary>The magnitude of the <see cref="Impulse"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Impulse.InNewtonSeconds"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Impulse"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfImpulse"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Impulse"/>, in unit <paramref name="unitOfImpulse"/>.</param>
    /// <param name="unitOfImpulse">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Impulse"/> a = 2.6 * <see cref="Impulse.OneNewtonSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Impulse(double magnitude, UnitOfImpulse unitOfImpulse) : this(magnitude * unitOfImpulse.Factor) { }
    /// <summary>Constructs a new <see cref="Impulse"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Impulse"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfImpulse"/> to be specified.</remarks>
    public Impulse(double magnitude)
    {
        Magnitude = magnitude;
    }

    public Momentum AsMomentum => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="Impulse"/>, expressed in unit <see cref="UnitOfImpulse.NewtonSecond"/>.</summary>
    public Scalar InNewtonSeconds => InUnit(UnitOfImpulse.NewtonSecond);

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
    public Impulse Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public Impulse Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public Impulse Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public Impulse Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Impulse other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Impulse"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [N * s]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Impulse"/>, expressed in unit <paramref name="unitOfImpulse"/>.</summary>
    /// <param name="unitOfImpulse">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfImpulse unitOfImpulse) => InUnit(Magnitude, unitOfImpulse);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Impulse"/>, expressed in unit <paramref name="unitOfImpulse"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="Impulse"/>.</param>
    /// <param name="unitOfImpulse">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfImpulse unitOfImpulse) => new(magnitude / unitOfImpulse.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Impulse"/>.</summary>
    public Impulse Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Impulse"/> with negated magnitude.</summary>
    public Impulse Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Impulse"/>.</param>
    public static Impulse operator +(Impulse x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Impulse"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Impulse"/>.</param>
    public static Impulse operator -(Impulse x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="Impulse"/> <paramref name="term"/>, producing another <see cref="Impulse"/>.</summary>
    /// <param name="term">This <see cref="Impulse"/> is added to this instance.</param>
    public Impulse Add(Impulse term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="Impulse"/> <paramref name="term"/> from this instance, producing another <see cref="Impulse"/>.</summary>
    /// <param name="term">This <see cref="Impulse"/> is subtracted from this instance.</param>
    public Impulse Subtract(Impulse term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="Impulse"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="Impulse"/>.</summary>
    /// <param name="x">This <see cref="Impulse"/> is added to the <see cref="Impulse"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="Impulse"/> is added to the <see cref="Impulse"/> <paramref name="x"/>.</param>
    public static Impulse operator +(Impulse x, Impulse y) => x.Add(y);
    /// <summary>Subtract the <see cref="Impulse"/> <paramref name="y"/> from the <see cref="Impulse"/> <paramref name="x"/>, producing another <see cref="Impulse"/>.</summary>
    /// <param name="x">The <see cref="Impulse"/> <paramref name="y"/> is subtracted from this <see cref="Impulse"/>.</param>
    /// <param name="y">This <see cref="Impulse"/> is subtracted from the <see cref="Impulse"/> <paramref name="x"/>.</param>
    public static Impulse operator -(Impulse x, Impulse y) => x.Subtract(y);

    /// <summary>Divides this <see cref="Impulse"/> by the <see cref="Impulse"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Impulse"/> is divided by this <see cref="Impulse"/>.</param>
    public Scalar Divide(Impulse divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="Impulse"/> <paramref name="x"/> by the <see cref="Impulse"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="Impulse"/> is divided by the <see cref="Impulse"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Impulse"/> <paramref name="x"/> is divided by this <see cref="Impulse"/>.</param>
    public static Scalar operator /(Impulse x, Impulse y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="Impulse"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Impulse"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Impulse"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Impulse"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Impulse"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Impulse"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Impulse x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Impulse"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Impulse"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Impulse x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Impulse"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Impulse Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Impulse"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Impulse"/> is scaled.</param>
    public Impulse Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Impulse"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Impulse"/> is divided.</param>
    public Impulse Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Impulse"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Impulse operator %(Impulse x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Impulse"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Impulse"/> <paramref name="x"/>.</param>
    public static Impulse operator *(Impulse x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Impulse"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Impulse"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Impulse"/>, which is scaled by <paramref name="x"/>.</param>
    public static Impulse operator *(double x, Impulse y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Impulse"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Impulse"/> <paramref name="x"/>.</param>
    public static Impulse operator /(Impulse x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="Impulse"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Impulse Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Impulse"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Impulse"/> is scaled.</param>
    public Impulse Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Impulse"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Impulse"/> is divided.</param>
    public Impulse Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Impulse"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Impulse operator %(Impulse x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Impulse"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Impulse"/> <paramref name="x"/>.</param>
    public static Impulse operator *(Impulse x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Impulse"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Impulse"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Impulse"/>, which is scaled by <paramref name="x"/>.</param>
    public static Impulse operator *(Scalar x, Impulse y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Impulse"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Impulse"/> <paramref name="x"/>.</param>
    public static Impulse operator /(Impulse x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="Impulse"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Impulse"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Impulse"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Impulse"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Impulse"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Impulse"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Impulse.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Impulse x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Impulse"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Impulse"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Impulse.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Impulse x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Multiplies the <see cref="Impulse"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="Impulse3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Impulse"/>.</param>
    public Impulse3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="Impulse"/> with the <see cref="ValueTuple"/> <paramref name="vector"/> to produce a <see cref="Impulse3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Impulse"/>.</param>
    public Impulse3 Multiply((double x, double y, double z) vector) => new(Magnitude * vector.x, Magnitude * vector.y, Magnitude * vector.z);
    /// <summary>Multiplies the <see cref="Impulse"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="Impulse3"/>.</summary>
    /// <param name="a">This <see cref="Impulse"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Impulse"/> <paramref name="a"/>.</param>
    public static Impulse3 operator *(Impulse a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Impulse"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="Impulse3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Impulse"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Impulse"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Impulse3 operator *(Vector3 a, Impulse b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="Impulse"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="Impulse3"/>.</summary>
    /// <param name="a">This <see cref="Impulse"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="Impulse"/> <paramref name="a"/>.</param>
    public static Impulse3 operator *(Impulse a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Impulse"/> <paramref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="Impulse3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="Impulse"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Impulse"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static Impulse3 operator *((double x, double y, double z) a, Impulse b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Impulse x, Impulse y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Impulse x, Impulse y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Impulse x, Impulse y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Impulse x, Impulse y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="Impulse"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Impulse x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Impulse x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Impulse"/> of magnitude <paramref name="x"/>.</summary>
    public static Impulse FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Impulse"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Impulse(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Impulse"/> of equivalent magnitude.</summary>
    public static Impulse FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Impulse"/> of equivalent magnitude.</summary>
    public static explicit operator Impulse(Scalar x) => FromScalar(x);
}