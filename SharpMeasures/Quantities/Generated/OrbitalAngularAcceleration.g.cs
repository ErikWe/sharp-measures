#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="OrbitalAngularAcceleration"/>, describing the <see cref="AngularAcceleration"/> of an object about an external point.
/// This is the magnitude of the vector quantity <see cref="OrbitalAngularAcceleration3"/>, and is expressed in <see cref="UnitOfAngularAcceleration"/>,
/// with the SI unit being [rad∙s⁻²].
/// <para>
/// New instances of <see cref="OrbitalAngularAcceleration"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfAngularAcceleration"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code><see cref="OrbitalAngularAcceleration"/> a = 3 * <see cref="OrbitalAngularAcceleration.OneRadianPerSecondSquared"/>;</code>
/// </item>
/// <item>
/// <code><see cref="OrbitalAngularAcceleration"/> d = <see cref="OrbitalAngularAcceleration.From(OrbitalAngularSpeed, Time)"/>;</code>
/// </item>
/// <item>
/// <code>
/// <see cref="OrbitalAngularAcceleration"/> e = <see cref="AngularAcceleration.AsOrbitalAngularAcceleration"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="OrbitalAngularAcceleration"/> can be retrieved in the desired <see cref="UnitOfAngularAcceleration"/> using pre-defined properties,
/// such as <see cref="RadiansPerSecondSquared"/>
/// </para>
/// </summary>
/// <remarks>
/// <see cref="OrbitalAngularAcceleration"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="AngularAcceleration"/></term>
/// <description>Describes any type of angular acceleration.</description>
/// </item>
/// <item>
/// <term><see cref="SpinAngularAcceleration"/></term>
/// <description>Describes the <see cref="AngularAcceleration"/> of an object about the internal center of rotation.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct OrbitalAngularAcceleration :
    IComparable<OrbitalAngularAcceleration>,
    IScalarQuantity,
    IScalableScalarQuantity<OrbitalAngularAcceleration>,
    IMultiplicableScalarQuantity<OrbitalAngularAcceleration, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<OrbitalAngularAcceleration, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<OrbitalAngularAcceleration3, Vector3>
{
    /// <summary>The zero-valued <see cref="OrbitalAngularAcceleration"/>.</summary>
    public static OrbitalAngularAcceleration Zero { get; } = new(0);

    /// <summary>The <see cref="OrbitalAngularAcceleration"/> of magnitude 1, when expressed in <see cref="UnitOfAngularAcceleration.RadianPerSecondSquared"/>.</summary>
    public static OrbitalAngularAcceleration OneRadianPerSecondSquared { get; } = UnitOfAngularAcceleration.RadianPerSecondSquared.AngularAcceleration.AsOrbitalAngularAcceleration;

    /// <summary>The magnitude of the <see cref="OrbitalAngularAcceleration"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAngularAcceleration)"/> or a pre-defined property
    /// - such as <see cref="RadiansPerSecondSquared"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="OrbitalAngularAcceleration"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAngularAcceleration"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="OrbitalAngularAcceleration"/>, expressed in <paramref name="unitOfAngularAcceleration"/>.</param>
    /// <param name="unitOfAngularAcceleration">The <see cref="UnitOfAngularAcceleration"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="OrbitalAngularAcceleration"/> a = 3 * <see cref="OrbitalAngularAcceleration.OneRadianPerSecondSquared"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public OrbitalAngularAcceleration(Scalar magnitude, UnitOfAngularAcceleration unitOfAngularAcceleration) : this(magnitude.Magnitude, unitOfAngularAcceleration) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularAcceleration"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAngularAcceleration"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="OrbitalAngularAcceleration"/>, expressed in <paramref name="unitOfAngularAcceleration"/>.</param>
    /// <param name="unitOfAngularAcceleration">The <see cref="UnitOfAngularAcceleration"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="OrbitalAngularAcceleration"/> a = 3 * <see cref="OrbitalAngularAcceleration.OneRadianPerSecondSquared"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public OrbitalAngularAcceleration(double magnitude, UnitOfAngularAcceleration unitOfAngularAcceleration) : 
    	this(magnitude * unitOfAngularAcceleration.AngularAcceleration.Magnitude) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularAcceleration"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="OrbitalAngularAcceleration"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularAcceleration(Scalar, UnitOfAngularAcceleration)"/>.</remarks>
    public OrbitalAngularAcceleration(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularAcceleration"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="OrbitalAngularAcceleration"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularAcceleration(double, UnitOfAngularAcceleration)"/>.</remarks>
    public OrbitalAngularAcceleration(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="OrbitalAngularAcceleration"/> to an instance of the associated quantity <see cref="AngularAcceleration"/>, of equal magnitude.</summary>
    public AngularAcceleration AsAngularAcceleration => new(Magnitude);
    /// <summary>Converts the <see cref="OrbitalAngularAcceleration"/> to an instance of the associated quantity <see cref="SpinAngularAcceleration"/>, of equal magnitude.</summary>
    public SpinAngularAcceleration AsSpinAngularAcceleration => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="OrbitalAngularAcceleration"/>, expressed in <see cref="UnitOfAngularAcceleration.RadianPerSecondSquared"/>.</summary>
    public Scalar RadiansPerSecondSquared => InUnit(UnitOfAngularAcceleration.RadianPerSecondSquared);

    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularAcceleration"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularAcceleration"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularAcceleration"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularAcceleration"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularAcceleration"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularAcceleration"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularAcceleration"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularAcceleration"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="OrbitalAngularAcceleration"/>.</summary>
    public OrbitalAngularAcceleration Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="OrbitalAngularAcceleration"/>.</summary>
    public OrbitalAngularAcceleration Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="OrbitalAngularAcceleration"/>.</summary>
    public OrbitalAngularAcceleration Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="OrbitalAngularAcceleration"/> to the nearest integer value.</summary>
    public OrbitalAngularAcceleration Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(OrbitalAngularAcceleration other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="OrbitalAngularAcceleration"/> in the default unit
    /// <see cref="UnitOfAngularAcceleration.RadianPerSecondSquared"/>, followed by the symbol [rad∙s⁻²].</summary>
    public override string ToString() => $"{RadiansPerSecondSquared} [rad∙s⁻²]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="OrbitalAngularAcceleration"/>,
    /// expressed in <paramref name="unitOfAngularAcceleration"/>.</summary>
    /// <param name="unitOfAngularAcceleration">The <see cref="UnitOfAngularAcceleration"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfAngularAcceleration unitOfAngularAcceleration) => InUnit(this, unitOfAngularAcceleration);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="OrbitalAngularAcceleration"/>,
    /// expressed in <paramref name="unitOfAngularAcceleration"/>.</summary>
    /// <param name="orbitalAngularAcceleration">The <see cref="OrbitalAngularAcceleration"/> to be expressed in <paramref name="unitOfAngularAcceleration"/>.</param>
    /// <param name="unitOfAngularAcceleration">The <see cref="UnitOfAngularAcceleration"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(OrbitalAngularAcceleration orbitalAngularAcceleration, UnitOfAngularAcceleration unitOfAngularAcceleration) 
    	=> new(orbitalAngularAcceleration.Magnitude / unitOfAngularAcceleration.AngularAcceleration.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="OrbitalAngularAcceleration"/>.</summary>
    public OrbitalAngularAcceleration Plus() => this;
    /// <summary>Negation, resulting in a <see cref="OrbitalAngularAcceleration"/> with negated magnitude.</summary>
    public OrbitalAngularAcceleration Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="OrbitalAngularAcceleration"/>.</param>
    public static OrbitalAngularAcceleration operator +(OrbitalAngularAcceleration x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="OrbitalAngularAcceleration"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="OrbitalAngularAcceleration"/>.</param>
    public static OrbitalAngularAcceleration operator -(OrbitalAngularAcceleration x) => x.Negate();

    /// <summary>Multiplicates the <see cref="OrbitalAngularAcceleration"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="OrbitalAngularAcceleration"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="OrbitalAngularAcceleration"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="OrbitalAngularAcceleration"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="OrbitalAngularAcceleration"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="OrbitalAngularAcceleration"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="OrbitalAngularAcceleration"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(OrbitalAngularAcceleration x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="OrbitalAngularAcceleration"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="OrbitalAngularAcceleration"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="OrbitalAngularAcceleration"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, OrbitalAngularAcceleration y) => y.Multiply(x);
    /// <summary>Division of the <see cref="OrbitalAngularAcceleration"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="OrbitalAngularAcceleration"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="OrbitalAngularAcceleration"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(OrbitalAngularAcceleration x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="OrbitalAngularAcceleration"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="OrbitalAngularAcceleration"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="OrbitalAngularAcceleration"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, OrbitalAngularAcceleration y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="OrbitalAngularAcceleration"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public OrbitalAngularAcceleration Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="OrbitalAngularAcceleration"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="OrbitalAngularAcceleration"/> is scaled.</param>
    public OrbitalAngularAcceleration Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="OrbitalAngularAcceleration"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="OrbitalAngularAcceleration"/> is divided.</param>
    public OrbitalAngularAcceleration Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="OrbitalAngularAcceleration"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="OrbitalAngularAcceleration"/> <paramref name="x"/> by this value.</param>
    public static OrbitalAngularAcceleration operator %(OrbitalAngularAcceleration x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="OrbitalAngularAcceleration"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="OrbitalAngularAcceleration"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="OrbitalAngularAcceleration"/> <paramref name="x"/>.</param>
    public static OrbitalAngularAcceleration operator *(OrbitalAngularAcceleration x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="OrbitalAngularAcceleration"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="OrbitalAngularAcceleration"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="OrbitalAngularAcceleration"/>, which is scaled by <paramref name="x"/>.</param>
    public static OrbitalAngularAcceleration operator *(double x, OrbitalAngularAcceleration y) => y.Multiply(x);
    /// <summary>Scales the <see cref="OrbitalAngularAcceleration"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="OrbitalAngularAcceleration"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="OrbitalAngularAcceleration"/> <paramref name="x"/>.</param>
    public static OrbitalAngularAcceleration operator /(OrbitalAngularAcceleration x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="OrbitalAngularAcceleration"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public OrbitalAngularAcceleration Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="OrbitalAngularAcceleration"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="OrbitalAngularAcceleration"/> is scaled.</param>
    public OrbitalAngularAcceleration Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="OrbitalAngularAcceleration"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="OrbitalAngularAcceleration"/> is divided.</param>
    public OrbitalAngularAcceleration Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="OrbitalAngularAcceleration"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="OrbitalAngularAcceleration"/> <paramref name="x"/> by this value.</param>
    public static OrbitalAngularAcceleration operator %(OrbitalAngularAcceleration x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="OrbitalAngularAcceleration"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="OrbitalAngularAcceleration"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="OrbitalAngularAcceleration"/> <paramref name="x"/>.</param>
    public static OrbitalAngularAcceleration operator *(OrbitalAngularAcceleration x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="OrbitalAngularAcceleration"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="OrbitalAngularAcceleration"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="OrbitalAngularAcceleration"/>, which is scaled by <paramref name="x"/>.</param>
    public static OrbitalAngularAcceleration operator *(Scalar x, OrbitalAngularAcceleration y) => y.Multiply(x);
    /// <summary>Scales the <see cref="OrbitalAngularAcceleration"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="OrbitalAngularAcceleration"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="OrbitalAngularAcceleration"/> <paramref name="x"/>.</param>
    public static OrbitalAngularAcceleration operator /(OrbitalAngularAcceleration x, Scalar y) => x.Divide(y);

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TProductScalarQuantity Multiply<TProductScalarQuantity, TFactorScalarQuantity>(TFactorScalarQuantity factor, Func<double, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorScalarQuantity : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        ArgumentNullException.ThrowIfNull(factor, nameof(factor));

        return factory(Magnitude * factor.Magnitude);

    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TQuotientScalarQuantity Divide<TQuotientScalarQuantity, TDivisorScalarQuantity>(TDivisorScalarQuantity divisor, Func<double, TQuotientScalarQuantity> factory)
        where TQuotientScalarQuantity : IScalarQuantity
        where TDivisorScalarQuantity : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        ArgumentNullException.ThrowIfNull(divisor, nameof(divisor));

        return factory(Magnitude / divisor.Magnitude);
    }

    /// <summary>Multiplication of the <see cref="OrbitalAngularAcceleration"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="OrbitalAngularAcceleration"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="OrbitalAngularAcceleration"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(OrbitalAngularAcceleration x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="OrbitalAngularAcceleration"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="OrbitalAngularAcceleration"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="OrbitalAngularAcceleration"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(OrbitalAngularAcceleration x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Multiplicates the <see cref="OrbitalAngularAcceleration"/> with the <see cref="Vector3"/> <paramref name="factor"/> to produce
    /// a <see cref="OrbitalAngularAcceleration3"/>.</summary>
    /// <param name="factor">This <see cref="Vector3"/> is multiplied by the <see cref="OrbitalAngularAcceleration"/>.</param>
    public OrbitalAngularAcceleration3 Multiply(Vector3 factor) => new(factor * Magnitude);
    /// <summary>Multiplicates the <see cref="OrbitalAngularAcceleration"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="OrbitalAngularAcceleration3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="OrbitalAngularAcceleration"/>.</param>
    public OrbitalAngularAcceleration3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplicates the <see cref="OrbitalAngularAcceleration"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="OrbitalAngularAcceleration3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="OrbitalAngularAcceleration"/>.</param>
    public OrbitalAngularAcceleration3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplication of the <see cref="OrbitalAngularAcceleration"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/>
    /// to produce a <see cref="OrbitalAngularAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="OrbitalAngularAcceleration"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="OrbitalAngularAcceleration"/> <paramref name="a"/>.</param>
    public static OrbitalAngularAcceleration3 operator *(OrbitalAngularAcceleration a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="OrbitalAngularAcceleration"/> <paramref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/>
    /// to produce a <see cref="OrbitalAngularAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="OrbitalAngularAcceleration"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="OrbitalAngularAcceleration"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static OrbitalAngularAcceleration3 operator *(Vector3 a, OrbitalAngularAcceleration b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="OrbitalAngularAcceleration"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="OrbitalAngularAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="OrbitalAngularAcceleration"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="OrbitalAngularAcceleration"/> <paramref name="a"/>.</param>
    public static OrbitalAngularAcceleration3 operator *(OrbitalAngularAcceleration a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="OrbitalAngularAcceleration"/> <paramref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="OrbitalAngularAcceleration3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="OrbitalAngularAcceleration"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="OrbitalAngularAcceleration"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static OrbitalAngularAcceleration3 operator *((double x, double y, double z) a, OrbitalAngularAcceleration b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="OrbitalAngularAcceleration"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="OrbitalAngularAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="OrbitalAngularAcceleration"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="OrbitalAngularAcceleration"/> <paramref name="a"/>.</param>
    public static OrbitalAngularAcceleration3 operator *(OrbitalAngularAcceleration a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="OrbitalAngularAcceleration"/> <paramref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="OrbitalAngularAcceleration3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="OrbitalAngularAcceleration"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="OrbitalAngularAcceleration"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static OrbitalAngularAcceleration3 operator *((Scalar x, Scalar y, Scalar z) a, OrbitalAngularAcceleration b) => b.Multiply(a);

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="OrbitalAngularAcceleration"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="OrbitalAngularAcceleration"/>.</param>
    public static bool operator <(OrbitalAngularAcceleration x, OrbitalAngularAcceleration y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="OrbitalAngularAcceleration"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="OrbitalAngularAcceleration"/>.</param>
    public static bool operator >(OrbitalAngularAcceleration x, OrbitalAngularAcceleration y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="OrbitalAngularAcceleration"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="OrbitalAngularAcceleration"/>.</param>
    public static bool operator <=(OrbitalAngularAcceleration x, OrbitalAngularAcceleration y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="OrbitalAngularAcceleration"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="OrbitalAngularAcceleration"/>.</param>
    public static bool operator >=(OrbitalAngularAcceleration x, OrbitalAngularAcceleration y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="OrbitalAngularAcceleration"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(OrbitalAngularAcceleration x) => x.ToDouble();

    /// <summary>Converts the <see cref="OrbitalAngularAcceleration"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(OrbitalAngularAcceleration x) => x.ToScalar();

    /// <summary>Constructs the <see cref="OrbitalAngularAcceleration"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static OrbitalAngularAcceleration FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="OrbitalAngularAcceleration"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator OrbitalAngularAcceleration(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="OrbitalAngularAcceleration"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static OrbitalAngularAcceleration FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="OrbitalAngularAcceleration"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator OrbitalAngularAcceleration(Scalar x) => FromScalar(x);
}
