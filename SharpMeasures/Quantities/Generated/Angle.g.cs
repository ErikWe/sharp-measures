namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Angle"/>, describing a <see cref="Distance"/> along a circle. This is often used
/// to describe an amount of rotation, and constructs the components of the vector quantity <see cref="Rotation"/>. The quantity is expressed in
/// <see cref="UnitOfAngle"/>, with the SI unit being [rad].
/// <para>
/// New instances of <see cref="Angle"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfAngle"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Angle"/> a = 3 * <see cref="Angle.OneRadian"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Angle"/> d = <see cref="Angle.From(AngularSpeed, Time)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="Angle"/> can be retrieved in the desired <see cref="UnitOfAngle"/> using pre-defined properties,
/// such as <see cref="Radians"/>.
/// </para>
/// </summary>
public readonly partial record struct Angle :
    IComparable<Angle>,
    IScalarQuantity,
    IScalableScalarQuantity<Angle>,
    IMultiplicableScalarQuantity<Angle, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Angle, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<Rotation3, Vector3>
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
    /// <summary>The <see cref="Angle"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngle.Gradian"/>.</summary>
    public static Angle OneGradian { get; } = new(1, UnitOfAngle.Gradian);

    /// <summary>The magnitude of the <see cref="Angle"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAngle)"/> or a pre-defined property
    /// - such as <see cref="Radians"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Angle"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAngle"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Angle"/>, expressed in <paramref name="unitOfAngle"/>.</param>
    /// <param name="unitOfAngle">The <see cref="UnitOfAngle"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Angle"/> a = 3 * <see cref="Angle.OneRadian"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Angle(Scalar magnitude, UnitOfAngle unitOfAngle) : this(magnitude.Magnitude, unitOfAngle) { }
    /// <summary>Constructs a new <see cref="Angle"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAngle"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Angle"/>, expressed in <paramref name="unitOfAngle"/>.</param>
    /// <param name="unitOfAngle">The <see cref="UnitOfAngle"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Angle"/> a = 3 * <see cref="Angle.OneRadian"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Angle(double magnitude, UnitOfAngle unitOfAngle) : this(magnitude * unitOfAngle.Factor) { }
    /// <summary>Constructs a new <see cref="Angle"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Angle"/>.</param>
    /// <remarks>Consider preferring <see cref="Angle(Scalar, UnitOfAngle)"/>.</remarks>
    public Angle(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Angle"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Angle"/>.</param>
    /// <remarks>Consider preferring <see cref="Angle(double, UnitOfAngle)"/>.</remarks>
    public Angle(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Angle"/>, expressed in <see cref="UnitOfAngle.Radian"/>.</summary>
    public Scalar Radians => InUnit(UnitOfAngle.Radian);
    /// <summary>Retrieves the magnitude of the <see cref="Angle"/>, expressed in <see cref="UnitOfAngle.Degree"/>.</summary>
    public Scalar Degrees => InUnit(UnitOfAngle.Degree);
    /// <summary>Retrieves the magnitude of the <see cref="Angle"/>, expressed in <see cref="UnitOfAngle.ArcMinute"/>.</summary>
    public Scalar ArcMinutes => InUnit(UnitOfAngle.ArcMinute);
    /// <summary>Retrieves the magnitude of the <see cref="Angle"/>, expressed in <see cref="UnitOfAngle.ArcSecond"/>.</summary>
    public Scalar ArcSeconds => InUnit(UnitOfAngle.ArcSecond);
    /// <summary>Retrieves the magnitude of the <see cref="Angle"/>, expressed in <see cref="UnitOfAngle.Turn"/>.</summary>
    public Scalar Turns => InUnit(UnitOfAngle.Turn);
    /// <summary>Retrieves the magnitude of the <see cref="Angle"/>, expressed in <see cref="UnitOfAngle.Gradian"/>.</summary>
    public Scalar Gradians => InUnit(UnitOfAngle.Gradian);

    /// <summary>Indicates whether the magnitude of the <see cref="Angle"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Angle"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Angle"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Angle"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Angle"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Angle"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Angle"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Angle"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="Angle"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public Angle Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="Angle"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public Angle Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="Angle"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public Angle Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="Angle"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public Angle Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Angle other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Angle"/> (in SI units), and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [rad]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Angle"/>,
    /// expressed in <paramref name="unitOfAngle"/>.</summary>
    /// <param name="unitOfAngle">The <see cref="UnitOfAngle"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfAngle unitOfAngle) => InUnit(this, unitOfAngle);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Angle"/>,
    /// expressed in <paramref name="unitOfAngle"/>.</summary>
    /// <param name="angle">The <see cref="Angle"/> to be expressed in <paramref name="unitOfAngle"/>.</param>
    /// <param name="unitOfAngle">The <see cref="UnitOfAngle"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Angle angle, UnitOfAngle unitOfAngle) => new(angle.Magnitude / unitOfAngle.Factor);

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
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Angle"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Angle"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Angle"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Angle y) => y.Multiply(x);
    /// <summary>Divides the <see cref="Angle"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Angle"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Angle"/> <paramref name="x"/> is divided.</param>
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
    /// <param name="y">The remainder is retrieved from division of <see cref="Angle"/> <paramref name="x"/> by this value.</param>
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
    /// <param name="y">The remainder is retrieved from division of the <see cref="Angle"/> <paramref name="x"/> by this value.</param>
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
    /// <summary>Multiples the <see cref="Angle"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Angle"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Angle"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity, Func{double, TProductScalarQuantity})"/>.</remarks>
    public static Unhandled operator *(Angle x, IScalarQuantity y) => x.Multiply<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));
    /// <summary>Divides the <see cref="Angle"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Angle"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Angle"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity, Func{double, TQuotientScalarQuantity})"/>.</remarks>
    public static Unhandled operator /(Angle x, IScalarQuantity y) => x.Divide<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));

    /// <summary>Multiplies the <see cref="Angle"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="Rotation3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Angle"/>.</param>
    public Rotation3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="Angle"/> with the values of <paramref name="components"/> to produce a <see cref="Rotation3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Angle"/>.</param>
    public Rotation3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="Angle"/> with the values of <paramref name="components"/> to produce a <see cref="Rotation3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Angle"/>.</param>
    public Rotation3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="Angle"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="Rotation3"/>.</summary>
    /// <param name="a">This <see cref="Angle"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Angle"/> <paramref name="a"/>.</param>
    public static Rotation3 operator *(Angle a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Angle"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="Rotation3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Angle"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Angle"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Rotation3 operator *(Vector3 a, Angle b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="Angle"/> <paramref name="a"/> with the values of <paramref name="b"/> to produce a <see cref="Rotation3"/>.</summary>
    /// <param name="a">This <see cref="Angle"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Angle"/> <paramref name="a"/>.</param>
    public static Rotation3 operator *(Angle a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Angle"/> <parmref name="b"/> with the values of <paramref name="a"/> to produce a <see cref="Rotation3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Angle"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Angle"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Rotation3 operator *((double x, double y, double z) a, Angle b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="Angle"/> <paramref name="a"/> with the values of <paramref name="b"/> to produce a <see cref="Rotation3"/>.</summary>
    /// <param name="a">This <see cref="Angle"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Angle"/> <paramref name="a"/>.</param>
    public static Rotation3 operator *(Angle a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Angle"/> <parmref name="b"/> with the values of <paramref name="a"/> to produce a <see cref="Rotation3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Angle"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Angle"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Rotation3 operator *((Scalar x, Scalar y, Scalar z) a, Angle b) => b.Multiply(a);

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
    public static bool operator <=(Angle x, Angle y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Angle x, Angle y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Angle"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static implicit operator double(Angle x) => x.ToDouble();

    /// <summary>Converts the <see cref="Angle"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Angle x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Angle"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Angle FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Angle"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Angle(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Angle"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static Angle FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Angle"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Angle(Scalar x) => FromScalar(x);
}
