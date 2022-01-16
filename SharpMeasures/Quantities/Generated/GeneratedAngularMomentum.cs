namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct AngularMomentum :
    IComparable<AngularMomentum>,
    IScalarQuantity<AngularMomentum>,
    IAddableScalarQuantity<AngularMomentum, AngularMomentum>,
    ISubtractableScalarQuantity<AngularMomentum, AngularMomentum>,
    IDivisibleScalarQuantity<Scalar, AngularMomentum>,
    IVector3izableScalarQuantity<AngularMomentum3>
{
    /// <summary>The zero-valued <see cref="AngularMomentum"/>.</summary>
    public static AngularMomentum Zero { get; } = new(0);

    /// <summary>The <see cref="AngularMomentum"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngularMomentum.KilogramMetreSquaredPerSecond"/>.</summary>
    public static AngularMomentum OneKilogramMetreSquaredPerSecond { get; } = new(1, UnitOfAngularMomentum.KilogramMetreSquaredPerSecond);

    /// <summary>The magnitude of the <see cref="AngularMomentum"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="AngularMomentum.InKilogramMetresSquaredPerSecond"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="AngularMomentum"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularMomentum"/>, in unit <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="AngularMomentum"/> a = 2.6 * <see cref="AngularMomentum.OneKilogramMetreSquaredPerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public AngularMomentum(double magnitude, UnitOfAngularMomentum unitOfAngularMomentum) : this(magnitude * unitOfAngularMomentum.Factor) { }
    /// <summary>Constructs a new <see cref="AngularMomentum"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularMomentum"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfAngularMomentum"/> to be specified.</remarks>
    public AngularMomentum(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="AngularMomentum"/>, expressed in unit <see cref="UnitOfAngularMomentum.KilogramMetreSquaredPerSecond"/>.</summary>
    public Scalar InKilogramMetresSquaredPerSecond => InUnit(UnitOfAngularMomentum.KilogramMetreSquaredPerSecond);

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
    public AngularMomentum Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public AngularMomentum Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public AngularMomentum Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public AngularMomentum Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(AngularMomentum other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="AngularMomentum"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [kg * m^2 * s^-1]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="AngularMomentum"/>, expressed in unit <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="unitOfAngularMomentum">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfAngularMomentum unitOfAngularMomentum) => InUnit(Magnitude, unitOfAngularMomentum);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="AngularMomentum"/>, expressed in unit <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="AngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfAngularMomentum unitOfAngularMomentum) => new(magnitude / unitOfAngularMomentum.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="AngularMomentum"/>.</summary>
    public AngularMomentum Plus() => this;
    /// <summary>Negation, resulting in a <see cref="AngularMomentum"/> with negated magnitude.</summary>
    public AngularMomentum Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="AngularMomentum"/>.</param>
    public static AngularMomentum operator +(AngularMomentum x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="AngularMomentum"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="AngularMomentum"/>.</param>
    public static AngularMomentum operator -(AngularMomentum x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="AngularMomentum"/> <paramref name="term"/>, producing another <see cref="AngularMomentum"/>.</summary>
    /// <param name="term">This <see cref="AngularMomentum"/> is added to this instance.</param>
    public AngularMomentum Add(AngularMomentum term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="AngularMomentum"/> <paramref name="term"/> from this instance, producing another <see cref="AngularMomentum"/>.</summary>
    /// <param name="term">This <see cref="AngularMomentum"/> is subtracted from this instance.</param>
    public AngularMomentum Subtract(AngularMomentum term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="AngularMomentum"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="AngularMomentum"/>.</summary>
    /// <param name="x">This <see cref="AngularMomentum"/> is added to the <see cref="AngularMomentum"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="AngularMomentum"/> is added to the <see cref="AngularMomentum"/> <paramref name="x"/>.</param>
    public static AngularMomentum operator +(AngularMomentum x, AngularMomentum y) => x.Add(y);
    /// <summary>Subtract the <see cref="AngularMomentum"/> <paramref name="y"/> from the <see cref="AngularMomentum"/> <paramref name="x"/>, producing another <see cref="AngularMomentum"/>.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/> <paramref name="y"/> is subtracted from this <see cref="AngularMomentum"/>.</param>
    /// <param name="y">This <see cref="AngularMomentum"/> is subtracted from the <see cref="AngularMomentum"/> <paramref name="x"/>.</param>
    public static AngularMomentum operator -(AngularMomentum x, AngularMomentum y) => x.Subtract(y);

    /// <summary>Divides this <see cref="AngularMomentum"/> by the <see cref="AngularMomentum"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="AngularMomentum"/> is divided by this <see cref="AngularMomentum"/>.</param>
    public Scalar Divide(AngularMomentum divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="AngularMomentum"/> <paramref name="x"/> by the <see cref="AngularMomentum"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="AngularMomentum"/> is divided by the <see cref="AngularMomentum"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="AngularMomentum"/> <paramref name="x"/> is divided by this <see cref="AngularMomentum"/>.</param>
    public static Scalar operator /(AngularMomentum x, AngularMomentum y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="AngularMomentum"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="AngularMomentum"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="AngularMomentum"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="AngularMomentum"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="AngularMomentum"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="AngularMomentum"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(AngularMomentum x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="AngularMomentum"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="AngularMomentum"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(AngularMomentum x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="AngularMomentum"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public AngularMomentum Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="AngularMomentum"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="AngularMomentum"/> is scaled.</param>
    public AngularMomentum Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="AngularMomentum"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="AngularMomentum"/> is divided.</param>
    public AngularMomentum Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="AngularMomentum"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static AngularMomentum operator %(AngularMomentum x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="AngularMomentum"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="AngularMomentum"/> <paramref name="x"/>.</param>
    public static AngularMomentum operator *(AngularMomentum x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="AngularMomentum"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="AngularMomentum"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="AngularMomentum"/>, which is scaled by <paramref name="x"/>.</param>
    public static AngularMomentum operator *(double x, AngularMomentum y) => y.Multiply(x);
    /// <summary>Scales the <see cref="AngularMomentum"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="AngularMomentum"/> <paramref name="x"/>.</param>
    public static AngularMomentum operator /(AngularMomentum x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="AngularMomentum"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public AngularMomentum Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="AngularMomentum"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="AngularMomentum"/> is scaled.</param>
    public AngularMomentum Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="AngularMomentum"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="AngularMomentum"/> is divided.</param>
    public AngularMomentum Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="AngularMomentum"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static AngularMomentum operator %(AngularMomentum x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="AngularMomentum"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="AngularMomentum"/> <paramref name="x"/>.</param>
    public static AngularMomentum operator *(AngularMomentum x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="AngularMomentum"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="AngularMomentum"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="AngularMomentum"/>, which is scaled by <paramref name="x"/>.</param>
    public static AngularMomentum operator *(Scalar x, AngularMomentum y) => y.Multiply(x);
    /// <summary>Scales the <see cref="AngularMomentum"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="AngularMomentum"/> <paramref name="x"/>.</param>
    public static AngularMomentum operator /(AngularMomentum x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="AngularMomentum"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="AngularMomentum"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="AngularMomentum"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="AngularMomentum"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="AngularMomentum"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="AngularMomentum"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="AngularMomentum.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(AngularMomentum x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="AngularMomentum"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="AngularMomentum"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="AngularMomentum.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(AngularMomentum x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Multiplies the <see cref="AngularMomentum"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="AngularMomentum"/>.</param>
    public AngularMomentum3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="AngularMomentum"/> with the <see cref="ValueTuple"/> <paramref name="vector"/> to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="AngularMomentum"/>.</param>
    public AngularMomentum3 Multiply((double x, double y, double z) vector) => new(Magnitude * vector.x, Magnitude * vector.y, Magnitude * vector.z);
    /// <summary>Multiplies the <see cref="AngularMomentum"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="AngularMomentum"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="AngularMomentum"/> <paramref name="a"/>.</param>
    public static AngularMomentum3 operator *(AngularMomentum a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="AngularMomentum"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="AngularMomentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="AngularMomentum"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static AngularMomentum3 operator *(Vector3 a, AngularMomentum b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="AngularMomentum"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="AngularMomentum"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="AngularMomentum"/> <paramref name="a"/>.</param>
    public static AngularMomentum3 operator *(AngularMomentum a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="AngularMomentum"/> <paramref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="AngularMomentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="AngularMomentum"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static AngularMomentum3 operator *((double x, double y, double z) a, AngularMomentum b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(AngularMomentum x, AngularMomentum y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(AngularMomentum x, AngularMomentum y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(AngularMomentum x, AngularMomentum y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(AngularMomentum x, AngularMomentum y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="AngularMomentum"/> <paramref name="x"/>.</summary>
    public static implicit operator double(AngularMomentum x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(AngularMomentum x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularMomentum"/> of magnitude <paramref name="x"/>.</summary>
    public static AngularMomentum FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularMomentum"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator AngularMomentum(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularMomentum"/> of equivalent magnitude.</summary>
    public static AngularMomentum FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularMomentum"/> of equivalent magnitude.</summary>
    public static explicit operator AngularMomentum(Scalar x) => FromScalar(x);
}