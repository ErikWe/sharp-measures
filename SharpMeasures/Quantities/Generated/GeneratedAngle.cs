namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct Angle :
    IComparable<Angle>,
    IScalarQuantity<Angle>,
    IAddableScalarQuantity<Angle, Angle>,
    ISubtractableScalarQuantity<Angle, Angle>,
    IDivisibleScalarQuantity<Scalar, Angle>,
    IVector3izableScalarQuantity<Rotation3>
{
    /// <summary>The zero-valued <see cref="Angle"/>.</summary>
    public static Angle Zero { get; } = new(0);

    /// <summary>The <see cref="Angle"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngle.Radian"/>.</summary>
    public static Angle OneRadian { get; } = new(1, UnitOfAngle.Radian);
    /// <summary>The <see cref="Angle"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngle.Degree"/>.</summary>
    public static Angle OneDegree { get; } = new(1, UnitOfAngle.Degree);
    /// <summary>The <see cref="Angle"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngle.ArcMinute"/>.</summary>
    public static Angle OneArcMinute { get; } = new(1, UnitOfAngle.ArcMinute);
    /// <summary>The <see cref="Angle"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngle.ArcSecond"/>.</summary>
    public static Angle OneArcSecond { get; } = new(1, UnitOfAngle.ArcSecond);
    /// <summary>The <see cref="Angle"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngle.Turn"/>.</summary>
    public static Angle OneTurn { get; } = new(1, UnitOfAngle.Turn);

    /// <summary>The magnitude of the <see cref="Angle"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Angle.InTurns"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Angle"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfAngle"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Angle"/>, in unit <paramref name="unitOfAngle"/>.</param>
    /// <param name="unitOfAngle">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Angle"/> a = 2.6 * <see cref="Angle.OneRadian"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Angle(double magnitude, UnitOfAngle unitOfAngle) : this(magnitude * unitOfAngle.Factor) { }
    /// <summary>Constructs a new <see cref="Angle"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Angle"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfAngle"/> to be specified.</remarks>
    public Angle(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Angle"/>, expressed in unit <see cref="UnitOfAngle.Radian"/>.</summary>
    public Scalar InRadians => InUnit(UnitOfAngle.Radian);
    /// <summary>Retrieves the magnitude of the <see cref="Angle"/>, expressed in unit <see cref="UnitOfAngle.Degree"/>.</summary>
    public Scalar InDegrees => InUnit(UnitOfAngle.Degree);
    /// <summary>Retrieves the magnitude of the <see cref="Angle"/>, expressed in unit <see cref="UnitOfAngle.ArcMinute"/>.</summary>
    public Scalar InArcMinutes => InUnit(UnitOfAngle.ArcMinute);
    /// <summary>Retrieves the magnitude of the <see cref="Angle"/>, expressed in unit <see cref="UnitOfAngle.ArcSecond"/>.</summary>
    public Scalar InArcSeconds => InUnit(UnitOfAngle.ArcSecond);
    /// <summary>Retrieves the magnitude of the <see cref="Angle"/>, expressed in unit <see cref="UnitOfAngle.Turn"/>.</summary>
    public Scalar InTurns => InUnit(UnitOfAngle.Turn);

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
    public Angle Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public Angle Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public Angle Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public Angle Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Angle other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Angle"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [rad]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Angle"/>, expressed in unit <paramref name="unitOfAngle"/>.</summary>
    /// <param name="unitOfAngle">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfAngle unitOfAngle) => InUnit(Magnitude, unitOfAngle);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Angle"/>, expressed in unit <paramref name="unitOfAngle"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="Angle"/>.</param>
    /// <param name="unitOfAngle">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfAngle unitOfAngle) => new(magnitude / unitOfAngle.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Angle"/>.</summary>
    public Angle Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Angle"/> with negated magnitude.</summary>
    public Angle Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Angle"/>.</param>
    public static Angle operator +(Angle x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Angle"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Angle"/>.</param>
    public static Angle operator -(Angle x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="Angle"/> <paramref name="term"/>, producing another <see cref="Angle"/>.</summary>
    /// <param name="term">This <see cref="Angle"/> is added to this instance.</param>
    public Angle Add(Angle term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="Angle"/> <paramref name="term"/> from this instance, producing another <see cref="Angle"/>.</summary>
    /// <param name="term">This <see cref="Angle"/> is subtracted from this instance.</param>
    public Angle Subtract(Angle term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="Angle"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="Angle"/>.</summary>
    /// <param name="x">This <see cref="Angle"/> is added to the <see cref="Angle"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="Angle"/> is added to the <see cref="Angle"/> <paramref name="x"/>.</param>
    public static Angle operator +(Angle x, Angle y) => x.Add(y);
    /// <summary>Subtract the <see cref="Angle"/> <paramref name="y"/> from the <see cref="Angle"/> <paramref name="x"/>, producing another <see cref="Angle"/>.</summary>
    /// <param name="x">The <see cref="Angle"/> <paramref name="y"/> is subtracted from this <see cref="Angle"/>.</param>
    /// <param name="y">This <see cref="Angle"/> is subtracted from the <see cref="Angle"/> <paramref name="x"/>.</param>
    public static Angle operator -(Angle x, Angle y) => x.Subtract(y);

    /// <summary>Divides this <see cref="Angle"/> by the <see cref="Angle"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Angle"/> is divided by this <see cref="Angle"/>.</param>
    public Scalar Divide(Angle divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="Angle"/> <paramref name="x"/> by the <see cref="Angle"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="Angle"/> is divided by the <see cref="Angle"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Angle"/> <paramref name="x"/> is divided by this <see cref="Angle"/>.</param>
    public static Scalar operator /(Angle x, Angle y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="Angle"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Angle"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Angle"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Angle"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Angle"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Angle"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Angle"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Angle x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Angle"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Angle"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Angle"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Angle x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Angle"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Angle Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Angle"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Angle"/> is scaled.</param>
    public Angle Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Angle"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Angle"/> is divided.</param>
    public Angle Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Angle"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Angle"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Angle operator %(Angle x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Angle"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Angle"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Angle"/> <paramref name="x"/>.</param>
    public static Angle operator *(Angle x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Angle"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Angle"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Angle"/>, which is scaled by <paramref name="x"/>.</param>
    public static Angle operator *(double x, Angle y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Angle"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Angle"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Angle"/> <paramref name="x"/>.</param>
    public static Angle operator /(Angle x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="Angle"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Angle Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Angle"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Angle"/> is scaled.</param>
    public Angle Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Angle"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Angle"/> is divided.</param>
    public Angle Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Angle"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Angle"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Angle operator %(Angle x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Angle"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Angle"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Angle"/> <paramref name="x"/>.</param>
    public static Angle operator *(Angle x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Angle"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Angle"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Angle"/>, which is scaled by <paramref name="x"/>.</param>
    public static Angle operator *(Scalar x, Angle y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Angle"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Angle"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Angle"/> <paramref name="x"/>.</param>
    public static Angle operator /(Angle x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="Angle"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Angle"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Angle"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Angle"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Angle"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Angle"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Angle"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Angle.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Angle x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Angle"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Angle"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Angle"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Angle.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Angle x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Multiplies the <see cref="Angle"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="Rotation3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Angle"/>.</param>
    public Rotation3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="Angle"/> with the <see cref="ValueTuple"/> <paramref name="vector"/> to produce a <see cref="Rotation3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Angle"/>.</param>
    public Rotation3 Multiply((double x, double y, double z) vector) => new(Magnitude * vector.x, Magnitude * vector.y, Magnitude * vector.z);
    /// <summary>Multiplies the <see cref="Angle"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="Rotation3"/>.</summary>
    /// <param name="a">This <see cref="Angle"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Angle"/> <paramref name="a"/>.</param>
    public static Rotation3 operator *(Angle a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Angle"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="Rotation3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Angle"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Angle"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Rotation3 operator *(Vector3 a, Angle b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="Angle"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="Rotation3"/>.</summary>
    /// <param name="a">This <see cref="Angle"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="Angle"/> <paramref name="a"/>.</param>
    public static Rotation3 operator *(Angle a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Angle"/> <paramref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="Rotation3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="Angle"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Angle"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static Rotation3 operator *((double x, double y, double z) a, Angle b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Angle x, Angle y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Angle x, Angle y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Angle x, Angle y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Angle x, Angle y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="Angle"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Angle x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Angle x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Angle"/> of magnitude <paramref name="x"/>.</summary>
    public static Angle FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Angle"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Angle(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Angle"/> of equivalent magnitude.</summary>
    public static Angle FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Angle"/> of equivalent magnitude.</summary>
    public static explicit operator Angle(Scalar x) => FromScalar(x);
}