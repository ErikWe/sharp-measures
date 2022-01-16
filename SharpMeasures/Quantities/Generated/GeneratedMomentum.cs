namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct Momentum :
    IComparable<Momentum>,
    IScalarQuantity<Momentum>,
    IAddableScalarQuantity<Momentum, Momentum>,
    IDivisibleScalarQuantity<Scalar, Momentum>,
    IVector3izableScalarQuantity<Momentum3>
{
    /// <summary>The zero-valued <see cref="Momentum"/>.</summary>
    public static Momentum Zero { get; } = new(0);

    /// <summary>The <see cref="Momentum"/> with magnitude 1, when expressed in unit <see cref="UnitOfMomentum.KilogramMetrePerSecond"/>.</summary>
    public static Momentum OneKilogramMetrePerSecond { get; } = new(1, UnitOfMomentum.KilogramMetrePerSecond);

    /// <summary>The magnitude of the <see cref="Momentum"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Momentum.InKilogramMetresPerSecond"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Momentum"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfMomentum"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Momentum"/>, in unit <paramref name="unitOfMomentum"/>.</param>
    /// <param name="unitOfMomentum">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Momentum"/> a = 2.6 * <see cref="Momentum.OneKilogramMetrePerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Momentum(double magnitude, UnitOfMomentum unitOfMomentum) : this(magnitude * unitOfMomentum.Factor) { }
    /// <summary>Constructs a new <see cref="Momentum"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Momentum"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfMomentum"/> to be specified.</remarks>
    public Momentum(double magnitude)
    {
        Magnitude = magnitude;
    }

    public Impulse AsImpulse => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="Momentum"/>, expressed in unit <see cref="UnitOfMomentum.KilogramMetrePerSecond"/>.</summary>
    public Scalar InKilogramMetresPerSecond => InUnit(UnitOfMomentum.KilogramMetrePerSecond);

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
    public Momentum Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public Momentum Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public Momentum Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public Momentum Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Momentum other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Momentum"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [kg * m * s^-1]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Momentum"/>, expressed in unit <paramref name="unitOfMomentum"/>.</summary>
    /// <param name="unitOfMomentum">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfMomentum unitOfMomentum) => InUnit(Magnitude, unitOfMomentum);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Momentum"/>, expressed in unit <paramref name="unitOfMomentum"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="Momentum"/>.</param>
    /// <param name="unitOfMomentum">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfMomentum unitOfMomentum) => new(magnitude / unitOfMomentum.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Momentum"/>.</summary>
    public Momentum Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Momentum"/> with negated magnitude.</summary>
    public Momentum Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Momentum"/>.</param>
    public static Momentum operator +(Momentum x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Momentum"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Momentum"/>.</param>
    public static Momentum operator -(Momentum x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="Momentum"/> <paramref name="term"/>, producing another <see cref="Momentum"/>.</summary>
    /// <param name="term">This <see cref="Momentum"/> is added to this instance.</param>
    public Momentum Add(Momentum term) => new(Magnitude + term.Magnitude);
    /// <summary>Adds the instances of <see cref="Momentum"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="Momentum"/>.</summary>
    /// <param name="x">This <see cref="Momentum"/> is added to the <see cref="Momentum"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="Momentum"/> is added to the <see cref="Momentum"/> <paramref name="x"/>.</param>
    public static Momentum operator +(Momentum x, Momentum y) => x.Add(y);

    /// <summary>Divides this <see cref="Momentum"/> by the <see cref="Momentum"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Momentum"/> is divided by this <see cref="Momentum"/>.</param>
    public Scalar Divide(Momentum divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="Momentum"/> <paramref name="x"/> by the <see cref="Momentum"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="Momentum"/> is divided by the <see cref="Momentum"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Momentum"/> <paramref name="x"/> is divided by this <see cref="Momentum"/>.</param>
    public static Scalar operator /(Momentum x, Momentum y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="Momentum"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Momentum"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Momentum"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Momentum"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Momentum"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Momentum"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Momentum x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Momentum"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Momentum"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Momentum x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Momentum"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Momentum Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Momentum"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Momentum"/> is scaled.</param>
    public Momentum Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Momentum"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Momentum"/> is divided.</param>
    public Momentum Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Momentum"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Momentum operator %(Momentum x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Momentum"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Momentum"/> <paramref name="x"/>.</param>
    public static Momentum operator *(Momentum x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Momentum"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Momentum"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Momentum"/>, which is scaled by <paramref name="x"/>.</param>
    public static Momentum operator *(double x, Momentum y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Momentum"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Momentum"/> <paramref name="x"/>.</param>
    public static Momentum operator /(Momentum x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="Momentum"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Momentum Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Momentum"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Momentum"/> is scaled.</param>
    public Momentum Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Momentum"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Momentum"/> is divided.</param>
    public Momentum Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Momentum"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Momentum operator %(Momentum x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Momentum"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Momentum"/> <paramref name="x"/>.</param>
    public static Momentum operator *(Momentum x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Momentum"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Momentum"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Momentum"/>, which is scaled by <paramref name="x"/>.</param>
    public static Momentum operator *(Scalar x, Momentum y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Momentum"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Momentum"/> <paramref name="x"/>.</param>
    public static Momentum operator /(Momentum x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="Momentum"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Momentum"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Momentum"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Momentum"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Momentum"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Momentum"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Momentum.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Momentum x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Momentum"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Momentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Momentum"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Momentum.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Momentum x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Multiplies the <see cref="Momentum"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="Momentum3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Momentum"/>.</param>
    public Momentum3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="Momentum"/> with the <see cref="ValueTuple"/> <paramref name="vector"/> to produce a <see cref="Momentum3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Momentum"/>.</param>
    public Momentum3 Multiply((double x, double y, double z) vector) => new(Magnitude * vector.x, Magnitude * vector.y, Magnitude * vector.z);
    /// <summary>Multiplies the <see cref="Momentum"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="Momentum3"/>.</summary>
    /// <param name="a">This <see cref="Momentum"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Momentum"/> <paramref name="a"/>.</param>
    public static Momentum3 operator *(Momentum a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Momentum"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="Momentum3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Momentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Momentum"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Momentum3 operator *(Vector3 a, Momentum b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="Momentum"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="Momentum3"/>.</summary>
    /// <param name="a">This <see cref="Momentum"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="Momentum"/> <paramref name="a"/>.</param>
    public static Momentum3 operator *(Momentum a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Momentum"/> <paramref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="Momentum3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="Momentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Momentum"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static Momentum3 operator *((double x, double y, double z) a, Momentum b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Momentum x, Momentum y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Momentum x, Momentum y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Momentum x, Momentum y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Momentum x, Momentum y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="Momentum"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Momentum x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Momentum x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Momentum"/> of magnitude <paramref name="x"/>.</summary>
    public static Momentum FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Momentum"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Momentum(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Momentum"/> of equivalent magnitude.</summary>
    public static Momentum FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Momentum"/> of equivalent magnitude.</summary>
    public static explicit operator Momentum(Scalar x) => FromScalar(x);
}