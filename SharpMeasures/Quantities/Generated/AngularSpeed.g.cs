namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="AngularSpeed"/>, describing change in <see cref="Angle"/> over <see cref="Time"/>.
/// This is the magnitude of the vector quantity <see cref="AngularSpeed3"/>, and is expressed in <see cref="UnitOfAngularVelocity"/>, with the SI unit being [rad / s].
/// <para>
/// New instances of <see cref="AngularSpeed"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfAngularVelocity"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="AngularSpeed"/> a = 3 * <see cref="AngularSpeed.OneRadianPerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="AngularSpeed"/> d = <see cref="AngularSpeed.From(Angle, Time)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="AngularSpeed"/> can be retrieved in the desired <see cref="UnitOfAngularVelocity"/> using pre-defined properties,
/// such as <see cref="RadiansPerSecond"/>.
/// </para>
/// </summary>
/// <remarks>
/// <see cref="AngularSpeed"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="OrbitalAngularSpeed"/></term>
/// <description>Describes the <see cref="AngularSpeed"/> of an object about an external point.</description>
/// </item>
/// <item>
/// <term><see cref="SpinAngularSpeed"/></term>
/// <description>Describes the <see cref="AngularSpeed"/> of an object about the internal center of rotation.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct AngularSpeed :
    IComparable<AngularSpeed>,
    IScalarQuantity,
    IScalableScalarQuantity<AngularSpeed>,
    IMultiplicableScalarQuantity<AngularSpeed, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<AngularSpeed, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<AngularVelocity3, Vector3>
{
    /// <summary>The zero-valued <see cref="AngularSpeed"/>.</summary>
    public static AngularSpeed Zero { get; } = new(0);

    /// <summary>The <see cref="AngularSpeed"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngularVelocity.RadianPerSecond"/>.</summary>
    public static AngularSpeed OneRadianPerSecond { get; } = new(1, UnitOfAngularVelocity.RadianPerSecond);
    /// <summary>The <see cref="AngularSpeed"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngularVelocity.DegreePerSecond"/>.</summary>
    public static AngularSpeed OneDegreePerSecond { get; } = new(1, UnitOfAngularVelocity.DegreePerSecond);
    /// <summary>The <see cref="AngularSpeed"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngularVelocity.TurnPerSecond"/>.</summary>
    public static AngularSpeed OneTurnPerSecond { get; } = new(1, UnitOfAngularVelocity.TurnPerSecond);

    /// <summary>The magnitude of the <see cref="AngularSpeed"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAngularVelocity)"/> or a pre-defined property
    /// - such as <see cref="RadiansPerSecond"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="AngularSpeed"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularSpeed"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="unitOfAngularVelocity">The <see cref="UnitOfAngularVelocity"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="AngularSpeed"/> a = 3 * <see cref="AngularSpeed.OneRadianPerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public AngularSpeed(Scalar magnitude, UnitOfAngularVelocity unitOfAngularVelocity) : this(magnitude.Magnitude, unitOfAngularVelocity) { }
    /// <summary>Constructs a new <see cref="AngularSpeed"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularSpeed"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="unitOfAngularVelocity">The <see cref="UnitOfAngularVelocity"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="AngularSpeed"/> a = 3 * <see cref="AngularSpeed.OneRadianPerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public AngularSpeed(double magnitude, UnitOfAngularVelocity unitOfAngularVelocity) : this(magnitude * unitOfAngularVelocity.Factor) { }
    /// <summary>Constructs a new <see cref="AngularSpeed"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularSpeed"/>.</param>
    /// <remarks>Consider preferring <see cref="AngularSpeed(Scalar, UnitOfAngularVelocity)"/>.</remarks>
    public AngularSpeed(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="AngularSpeed"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularSpeed"/>.</param>
    /// <remarks>Consider preferring <see cref="AngularSpeed(double, UnitOfAngularVelocity)"/>.</remarks>
    public AngularSpeed(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="AngularSpeed"/> to an instance of the associated quantity <see cref="OrbitalAngularSpeed"/>, of equal magnitude.</summary>
    public OrbitalAngularSpeed AsOrbitalAngularSpeed => new(Magnitude);
    /// <summary>Converts the <see cref="AngularSpeed"/> to an instance of the associated quantity <see cref="SpinAngularSpeed"/>, of equal magnitude.</summary>
    public SpinAngularSpeed AsSpinAngularSpeed => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="AngularSpeed"/>, expressed in <see cref="UnitOfAngularVelocity.RadianPerSecond"/>.</summary>
    public Scalar RadiansPerSecond => InUnit(UnitOfAngularVelocity.RadianPerSecond);
    /// <summary>Retrieves the magnitude of the <see cref="AngularSpeed"/>, expressed in <see cref="UnitOfAngularVelocity.DegreePerSecond"/>.</summary>
    public Scalar DegreesPerSecond => InUnit(UnitOfAngularVelocity.DegreePerSecond);
    /// <summary>Retrieves the magnitude of the <see cref="AngularSpeed"/>, expressed in <see cref="UnitOfAngularVelocity.TurnPerSecond"/>.</summary>
    public Scalar TurnsPerSecond => InUnit(UnitOfAngularVelocity.TurnPerSecond);

    /// <summary>Indicates whether the magnitude of the <see cref="AngularSpeed"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="AngularSpeed"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="AngularSpeed"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="AngularSpeed"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="AngularSpeed"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="AngularSpeed"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="AngularSpeed"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="AngularSpeed"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="AngularSpeed"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public AngularSpeed Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="AngularSpeed"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public AngularSpeed Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="AngularSpeed"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public AngularSpeed Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="AngularSpeed"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public AngularSpeed Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(AngularSpeed other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="AngularSpeed"/> (in SI units), and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [rad / s]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="AngularSpeed"/>,
    /// expressed in <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="unitOfAngularVelocity">The <see cref="UnitOfAngularVelocity"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfAngularVelocity unitOfAngularVelocity) => InUnit(this, unitOfAngularVelocity);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="AngularSpeed"/>,
    /// expressed in <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="angularSpeed">The <see cref="AngularSpeed"/> to be expressed in <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="unitOfAngularVelocity">The <see cref="UnitOfAngularVelocity"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(AngularSpeed angularSpeed, UnitOfAngularVelocity unitOfAngularVelocity) => new(angularSpeed.Magnitude / unitOfAngularVelocity.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="AngularSpeed"/>.</summary>
    public AngularSpeed Plus() => this;
    /// <summary>Negation, resulting in a <see cref="AngularSpeed"/> with negated magnitude.</summary>
    public AngularSpeed Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="AngularSpeed"/>.</param>
    public static AngularSpeed operator +(AngularSpeed x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="AngularSpeed"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="AngularSpeed"/>.</param>
    public static AngularSpeed operator -(AngularSpeed x) => x.Negate();

    /// <summary>Multiplies the <see cref="AngularSpeed"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="AngularSpeed"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="AngularSpeed"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="AngularSpeed"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="AngularSpeed"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularSpeed"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="AngularSpeed"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(AngularSpeed x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="AngularSpeed"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="AngularSpeed"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="AngularSpeed"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, AngularSpeed y) => y.Multiply(x);
    /// <summary>Divides the <see cref="AngularSpeed"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularSpeed"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="AngularSpeed"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(AngularSpeed x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="AngularSpeed"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public AngularSpeed Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="AngularSpeed"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="AngularSpeed"/> is scaled.</param>
    public AngularSpeed Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="AngularSpeed"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="AngularSpeed"/> is divided.</param>
    public AngularSpeed Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="AngularSpeed"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularSpeed"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="AngularSpeed"/> <paramref name="x"/> by this value.</param>
    public static AngularSpeed operator %(AngularSpeed x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="AngularSpeed"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularSpeed"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="AngularSpeed"/> <paramref name="x"/>.</param>
    public static AngularSpeed operator *(AngularSpeed x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="AngularSpeed"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="AngularSpeed"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="AngularSpeed"/>, which is scaled by <paramref name="x"/>.</param>
    public static AngularSpeed operator *(double x, AngularSpeed y) => y.Multiply(x);
    /// <summary>Scales the <see cref="AngularSpeed"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularSpeed"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="AngularSpeed"/> <paramref name="x"/>.</param>
    public static AngularSpeed operator /(AngularSpeed x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="AngularSpeed"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public AngularSpeed Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="AngularSpeed"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="AngularSpeed"/> is scaled.</param>
    public AngularSpeed Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="AngularSpeed"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="AngularSpeed"/> is divided.</param>
    public AngularSpeed Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="AngularSpeed"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularSpeed"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="AngularSpeed"/> <paramref name="x"/> by this value.</param>
    public static AngularSpeed operator %(AngularSpeed x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="AngularSpeed"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularSpeed"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="AngularSpeed"/> <paramref name="x"/>.</param>
    public static AngularSpeed operator *(AngularSpeed x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="AngularSpeed"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="AngularSpeed"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="AngularSpeed"/>, which is scaled by <paramref name="x"/>.</param>
    public static AngularSpeed operator *(Scalar x, AngularSpeed y) => y.Multiply(x);
    /// <summary>Scales the <see cref="AngularSpeed"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularSpeed"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="AngularSpeed"/> <paramref name="x"/>.</param>
    public static AngularSpeed operator /(AngularSpeed x, Scalar y) => x.Divide(y);

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
    /// <summary>Multiples the <see cref="AngularSpeed"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularSpeed"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="AngularSpeed"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity, Func{double, TProductScalarQuantity})"/>.</remarks>
    public static Unhandled operator *(AngularSpeed x, IScalarQuantity y) => x.Multiply<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));
    /// <summary>Divides the <see cref="AngularSpeed"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularSpeed"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="AngularSpeed"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity, Func{double, TQuotientScalarQuantity})"/>.</remarks>
    public static Unhandled operator /(AngularSpeed x, IScalarQuantity y) => x.Divide<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));

    /// <summary>Multiplies the <see cref="AngularSpeed"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="AngularVelocity3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="AngularSpeed"/>.</param>
    public AngularVelocity3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="AngularSpeed"/> with the values of <paramref name="components"/> to produce a <see cref="AngularVelocity3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="AngularSpeed"/>.</param>
    public AngularVelocity3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="AngularSpeed"/> with the values of <paramref name="components"/> to produce a <see cref="AngularVelocity3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="AngularSpeed"/>.</param>
    public AngularVelocity3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="AngularSpeed"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="AngularVelocity3"/>.</summary>
    /// <param name="a">This <see cref="AngularSpeed"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="AngularSpeed"/> <paramref name="a"/>.</param>
    public static AngularVelocity3 operator *(AngularSpeed a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="AngularSpeed"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="AngularVelocity3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="AngularSpeed"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="AngularSpeed"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static AngularVelocity3 operator *(Vector3 a, AngularSpeed b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="AngularSpeed"/> <paramref name="a"/> with the values of <paramref name="b"/> to produce a <see cref="AngularVelocity3"/>.</summary>
    /// <param name="a">This <see cref="AngularSpeed"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="AngularSpeed"/> <paramref name="a"/>.</param>
    public static AngularVelocity3 operator *(AngularSpeed a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="AngularSpeed"/> <parmref name="b"/> with the values of <paramref name="a"/> to produce a <see cref="AngularVelocity3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="AngularSpeed"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="AngularSpeed"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static AngularVelocity3 operator *((double x, double y, double z) a, AngularSpeed b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="AngularSpeed"/> <paramref name="a"/> with the values of <paramref name="b"/> to produce a <see cref="AngularVelocity3"/>.</summary>
    /// <param name="a">This <see cref="AngularSpeed"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="AngularSpeed"/> <paramref name="a"/>.</param>
    public static AngularVelocity3 operator *(AngularSpeed a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="AngularSpeed"/> <parmref name="b"/> with the values of <paramref name="a"/> to produce a <see cref="AngularVelocity3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="AngularSpeed"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="AngularSpeed"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static AngularVelocity3 operator *((Scalar x, Scalar y, Scalar z) a, AngularSpeed b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(AngularSpeed x, AngularSpeed y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(AngularSpeed x, AngularSpeed y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(AngularSpeed x, AngularSpeed y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(AngularSpeed x, AngularSpeed y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="AngularSpeed"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static implicit operator double(AngularSpeed x) => x.ToDouble();

    /// <summary>Converts the <see cref="AngularSpeed"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(AngularSpeed x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularSpeed"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static AngularSpeed FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularSpeed"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator AngularSpeed(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularSpeed"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static AngularSpeed FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularSpeed"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator AngularSpeed(Scalar x) => FromScalar(x);
}
