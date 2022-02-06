namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="SpeedSquared"/>, describing the square of <see cref="Speed"/>.
/// This is the magnitude of the vector quantity <see cref="VelocitySquared3"/>, and is expressed in <see cref="UnitOfVelocitySquared"/>, with the SI unit being [m² / s²].
/// <para>
/// New instances of <see cref="SpeedSquared"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfVelocitySquared"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="SpeedSquared"/> a = 3 * <see cref="SpeedSquared.OneSquareMetrePerSecondSquared"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="SpeedSquared"/> d = <see cref="SpeedSquared.From(Speed, Speed)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="SpeedSquared"/> can be retrieved in the desired <see cref="UnitOfVelocitySquared"/> using pre-defined properties,
/// such as <see cref="SquareMetresPerSecondSquared"/>.
/// </para>
/// </summary>
public readonly partial record struct SpeedSquared :
    IComparable<SpeedSquared>,
    IScalarQuantity,
    IScalableScalarQuantity<SpeedSquared>,
    ISquareRootableScalarQuantity<Speed>,
    IMultiplicableScalarQuantity<SpeedSquared, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<SpeedSquared, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<VelocitySquared3, Vector3>
{
    /// <summary>The zero-valued <see cref="SpeedSquared"/>.</summary>
    public static SpeedSquared Zero { get; } = new(0);

    /// <summary>The <see cref="SpeedSquared"/> with magnitude 1, when expressed in unit <see cref="UnitOfVelocitySquared.SquareMetrePerSecondSquared"/>.</summary>
    public static SpeedSquared OneSquareMetrePerSecondSquared { get; } = new(1, UnitOfVelocitySquared.SquareMetrePerSecondSquared);

    /// <summary>Computes <see cref="SpeedSquared"/> according to { <see cref="SpeedSquared"/> = <paramref name="speed"/>² }.</summary>
    /// <param name="speed">This <see cref="Speed"/> is squared to produce a <see cref="SpeedSquared"/>.</param>
    public static SpeedSquared From(Speed speed) => new(Math.Pow(speed.Magnitude, 2));

    /// <summary>The magnitude of the <see cref="SpeedSquared"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfVelocitySquared)"/> or a pre-defined property
    /// - such as <see cref="SquareMetresPerSecondSquared"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="SpeedSquared"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfVelocitySquared"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpeedSquared"/>, expressed in <paramref name="unitOfVelocitySquared"/>.</param>
    /// <param name="unitOfVelocitySquared">The <see cref="UnitOfVelocitySquared"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SpeedSquared"/> a = 3 * <see cref="SpeedSquared.OneSquareMetrePerSecondSquared"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SpeedSquared(Scalar magnitude, UnitOfVelocitySquared unitOfVelocitySquared) : this(magnitude.Magnitude, unitOfVelocitySquared) { }
    /// <summary>Constructs a new <see cref="SpeedSquared"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfVelocitySquared"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpeedSquared"/>, expressed in <paramref name="unitOfVelocitySquared"/>.</param>
    /// <param name="unitOfVelocitySquared">The <see cref="UnitOfVelocitySquared"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SpeedSquared"/> a = 3 * <see cref="SpeedSquared.OneSquareMetrePerSecondSquared"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SpeedSquared(double magnitude, UnitOfVelocitySquared unitOfVelocitySquared) : this(magnitude * unitOfVelocitySquared.Factor) { }
    /// <summary>Constructs a new <see cref="SpeedSquared"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpeedSquared"/>.</param>
    /// <remarks>Consider preferring <see cref="SpeedSquared(Scalar, UnitOfVelocitySquared)"/>.</remarks>
    public SpeedSquared(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="SpeedSquared"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpeedSquared"/>.</param>
    /// <remarks>Consider preferring <see cref="SpeedSquared(double, UnitOfVelocitySquared)"/>.</remarks>
    public SpeedSquared(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="SpeedSquared"/>, expressed in <see cref="UnitOfVelocitySquared.SquareMetrePerSecondSquared"/>.</summary>
    public Scalar SquareMetresPerSecondSquared => InUnit(UnitOfVelocitySquared.SquareMetrePerSecondSquared);

    /// <summary>Indicates whether the magnitude of the <see cref="SpeedSquared"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpeedSquared"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="SpeedSquared"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="SpeedSquared"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpeedSquared"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpeedSquared"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="SpeedSquared"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpeedSquared"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="SpeedSquared"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public SpeedSquared Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="SpeedSquared"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public SpeedSquared Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="SpeedSquared"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public SpeedSquared Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="SpeedSquared"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public SpeedSquared Round() => new(Math.Round(Magnitude));

    /// <summary>Takes the square root of the <see cref="SpeedSquared"/>, producing a <see cref="Speed"/>.</summary>
    public Speed SquareRoot() => Speed.From(this);

    /// <inheritdoc/>
    public int CompareTo(SpeedSquared other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="SpeedSquared"/> (in SI units), and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [m^2 / s^2]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="SpeedSquared"/>,
    /// expressed in <paramref name="unitOfVelocitySquared"/>.</summary>
    /// <param name="unitOfVelocitySquared">The <see cref="UnitOfVelocitySquared"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfVelocitySquared unitOfVelocitySquared) => InUnit(this, unitOfVelocitySquared);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="SpeedSquared"/>,
    /// expressed in <paramref name="unitOfVelocitySquared"/>.</summary>
    /// <param name="speedSquared">The <see cref="SpeedSquared"/> to be expressed in <paramref name="unitOfVelocitySquared"/>.</param>
    /// <param name="unitOfVelocitySquared">The <see cref="UnitOfVelocitySquared"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(SpeedSquared speedSquared, UnitOfVelocitySquared unitOfVelocitySquared) => new(speedSquared.Magnitude / unitOfVelocitySquared.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="SpeedSquared"/>.</summary>
    public SpeedSquared Plus() => this;
    /// <summary>Negation, resulting in a <see cref="SpeedSquared"/> with negated magnitude.</summary>
    public SpeedSquared Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="SpeedSquared"/>.</param>
    public static SpeedSquared operator +(SpeedSquared x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="SpeedSquared"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="SpeedSquared"/>.</param>
    public static SpeedSquared operator -(SpeedSquared x) => x.Negate();

    /// <summary>Multiplies the <see cref="SpeedSquared"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="SpeedSquared"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="SpeedSquared"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="SpeedSquared"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="SpeedSquared"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpeedSquared"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SpeedSquared"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(SpeedSquared x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="SpeedSquared"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="SpeedSquared"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="SpeedSquared"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, SpeedSquared y) => y.Multiply(x);
    /// <summary>Divides the <see cref="SpeedSquared"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpeedSquared"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SpeedSquared"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(SpeedSquared x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="SpeedSquared"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public SpeedSquared Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="SpeedSquared"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SpeedSquared"/> is scaled.</param>
    public SpeedSquared Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="SpeedSquared"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SpeedSquared"/> is divided.</param>
    public SpeedSquared Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="SpeedSquared"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpeedSquared"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="SpeedSquared"/> <paramref name="x"/> by this value.</param>
    public static SpeedSquared operator %(SpeedSquared x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SpeedSquared"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpeedSquared"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SpeedSquared"/> <paramref name="x"/>.</param>
    public static SpeedSquared operator *(SpeedSquared x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SpeedSquared"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SpeedSquared"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpeedSquared"/>, which is scaled by <paramref name="x"/>.</param>
    public static SpeedSquared operator *(double x, SpeedSquared y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SpeedSquared"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpeedSquared"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SpeedSquared"/> <paramref name="x"/>.</param>
    public static SpeedSquared operator /(SpeedSquared x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="SpeedSquared"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public SpeedSquared Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="SpeedSquared"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SpeedSquared"/> is scaled.</param>
    public SpeedSquared Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="SpeedSquared"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SpeedSquared"/> is divided.</param>
    public SpeedSquared Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="SpeedSquared"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpeedSquared"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="SpeedSquared"/> <paramref name="x"/> by this value.</param>
    public static SpeedSquared operator %(SpeedSquared x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SpeedSquared"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpeedSquared"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SpeedSquared"/> <paramref name="x"/>.</param>
    public static SpeedSquared operator *(SpeedSquared x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SpeedSquared"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SpeedSquared"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpeedSquared"/>, which is scaled by <paramref name="x"/>.</param>
    public static SpeedSquared operator *(Scalar x, SpeedSquared y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SpeedSquared"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpeedSquared"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SpeedSquared"/> <paramref name="x"/>.</param>
    public static SpeedSquared operator /(SpeedSquared x, Scalar y) => x.Divide(y);

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
    /// <summary>Multiples the <see cref="SpeedSquared"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpeedSquared"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="SpeedSquared"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity, Func{double, TProductScalarQuantity})"/>.</remarks>
    public static Unhandled operator *(SpeedSquared x, IScalarQuantity y) => x.Multiply<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));
    /// <summary>Divides the <see cref="SpeedSquared"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpeedSquared"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="SpeedSquared"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity, Func{double, TQuotientScalarQuantity})"/>.</remarks>
    public static Unhandled operator /(SpeedSquared x, IScalarQuantity y) => x.Divide<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));

    /// <summary>Multiplies the <see cref="SpeedSquared"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="VelocitySquared3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="SpeedSquared"/>.</param>
    public VelocitySquared3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="SpeedSquared"/> with the values of <paramref name="components"/> to produce a <see cref="VelocitySquared3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="SpeedSquared"/>.</param>
    public VelocitySquared3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="SpeedSquared"/> with the values of <paramref name="components"/> to produce a <see cref="VelocitySquared3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="SpeedSquared"/>.</param>
    public VelocitySquared3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="SpeedSquared"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="VelocitySquared3"/>.</summary>
    /// <param name="a">This <see cref="SpeedSquared"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="SpeedSquared"/> <paramref name="a"/>.</param>
    public static VelocitySquared3 operator *(SpeedSquared a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="SpeedSquared"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="VelocitySquared3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="SpeedSquared"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="SpeedSquared"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static VelocitySquared3 operator *(Vector3 a, SpeedSquared b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="SpeedSquared"/> <paramref name="a"/> with the values of <paramref name="b"/> to produce a <see cref="VelocitySquared3"/>.</summary>
    /// <param name="a">This <see cref="SpeedSquared"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="SpeedSquared"/> <paramref name="a"/>.</param>
    public static VelocitySquared3 operator *(SpeedSquared a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="SpeedSquared"/> <parmref name="b"/> with the values of <paramref name="a"/> to produce a <see cref="VelocitySquared3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="SpeedSquared"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="SpeedSquared"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static VelocitySquared3 operator *((double x, double y, double z) a, SpeedSquared b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="SpeedSquared"/> <paramref name="a"/> with the values of <paramref name="b"/> to produce a <see cref="VelocitySquared3"/>.</summary>
    /// <param name="a">This <see cref="SpeedSquared"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="SpeedSquared"/> <paramref name="a"/>.</param>
    public static VelocitySquared3 operator *(SpeedSquared a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="SpeedSquared"/> <parmref name="b"/> with the values of <paramref name="a"/> to produce a <see cref="VelocitySquared3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="SpeedSquared"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="SpeedSquared"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static VelocitySquared3 operator *((Scalar x, Scalar y, Scalar z) a, SpeedSquared b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(SpeedSquared x, SpeedSquared y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(SpeedSquared x, SpeedSquared y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(SpeedSquared x, SpeedSquared y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(SpeedSquared x, SpeedSquared y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="SpeedSquared"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static implicit operator double(SpeedSquared x) => x.ToDouble();

    /// <summary>Converts the <see cref="SpeedSquared"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(SpeedSquared x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="SpeedSquared"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static SpeedSquared FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="SpeedSquared"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator SpeedSquared(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="SpeedSquared"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static SpeedSquared FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="SpeedSquared"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator SpeedSquared(Scalar x) => FromScalar(x);
}
