namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="OrbitalAngularSpeed"/>, describing the <see cref="AngularSpeed"/> of an object about an external point.
/// This is the magnitude of the vector quantity <see cref="OrbitalAngularVelocity3"/>, and is expressed in <see cref="UnitOfAngularVelocity"/>, with the SI unit being [rad∙s⁻¹].
/// <para>
/// New instances of <see cref="OrbitalAngularSpeed"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfAngularVelocity"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="OrbitalAngularSpeed"/> a = 3 * <see cref="OrbitalAngularSpeed.OneRadianPerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="OrbitalAngularSpeed"/> d = <see cref="OrbitalAngularSpeed.From(Angle, Time)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="OrbitalAngularSpeed"/> e = <see cref="AngularSpeed.AsOrbitalAngularSpeed()"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="OrbitalAngularSpeed"/> can be retrieved in the desired <see cref="UnitOfAngularVelocity"/> using pre-defined properties,
/// such as <see cref="RadiansPerSecond"/>
/// </para>
/// </summary>
/// <remarks>
/// <see cref="OrbitalAngularSpeed"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="AngularSpeed"/></term>
/// <description>Describes any type of angular speed.</description>
/// </item>
/// <item>
/// <term><see cref="SpinAngularSpeed"/></term>
/// <description>Describes the <see cref="AngularSpeed"/> of an object about the internal center of rotation.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct OrbitalAngularSpeed :
    IComparable<OrbitalAngularSpeed>,
    IScalarQuantity,
    IScalableScalarQuantity<OrbitalAngularSpeed>,
    IMultiplicableScalarQuantity<OrbitalAngularSpeed, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<OrbitalAngularSpeed, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<OrbitalAngularVelocity3, Vector3>
{
    /// <summary>The zero-valued <see cref="OrbitalAngularSpeed"/>.</summary>
    public static OrbitalAngularSpeed Zero { get; } = new(0);

    /// <summary>The <see cref="OrbitalAngularSpeed"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngularVelocity.RadianPerSecond"/>.</summary>
    public static OrbitalAngularSpeed OneRadianPerSecond { get; } = new(1, UnitOfAngularVelocity.RadianPerSecond);
    /// <summary>The <see cref="OrbitalAngularSpeed"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngularVelocity.DegreePerSecond"/>.</summary>
    public static OrbitalAngularSpeed OneDegreePerSecond { get; } = new(1, UnitOfAngularVelocity.DegreePerSecond);
    /// <summary>The <see cref="OrbitalAngularSpeed"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngularVelocity.RevolutionPerSecond"/>.</summary>
    public static OrbitalAngularSpeed OneRevolutionPerSecond { get; } = new(1, UnitOfAngularVelocity.RevolutionPerSecond);
    /// <summary>The <see cref="OrbitalAngularSpeed"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngularVelocity.RevolutionPerMinute"/>.</summary>
    public static OrbitalAngularSpeed OneRevolutionPerMinute { get; } = new(1, UnitOfAngularVelocity.RevolutionPerMinute);

    /// <summary>The magnitude of the <see cref="OrbitalAngularSpeed"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAngularVelocity)"/> or a pre-defined property
    /// - such as <see cref="RadiansPerSecond"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="OrbitalAngularSpeed"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="OrbitalAngularSpeed"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="unitOfAngularVelocity">The <see cref="UnitOfAngularVelocity"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="OrbitalAngularSpeed"/> a = 3 * <see cref="OrbitalAngularSpeed.OneRadianPerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public OrbitalAngularSpeed(Scalar magnitude, UnitOfAngularVelocity unitOfAngularVelocity) : this(magnitude.Magnitude, unitOfAngularVelocity) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularSpeed"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="OrbitalAngularSpeed"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="unitOfAngularVelocity">The <see cref="UnitOfAngularVelocity"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="OrbitalAngularSpeed"/> a = 3 * <see cref="OrbitalAngularSpeed.OneRadianPerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public OrbitalAngularSpeed(double magnitude, UnitOfAngularVelocity unitOfAngularVelocity) : this(magnitude * unitOfAngularVelocity.AngularSpeed.Magnitude) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularSpeed"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="OrbitalAngularSpeed"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularSpeed(Scalar, UnitOfAngularVelocity)"/>.</remarks>
    public OrbitalAngularSpeed(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularSpeed"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="OrbitalAngularSpeed"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularSpeed(double, UnitOfAngularVelocity)"/>.</remarks>
    public OrbitalAngularSpeed(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="OrbitalAngularSpeed"/> to an instance of the associated quantity <see cref="AngularSpeed"/>, of equal magnitude.</summary>
    public AngularSpeed AsAngularSpeed => new(Magnitude);
    /// <summary>Converts the <see cref="OrbitalAngularSpeed"/> to an instance of the associated quantity <see cref="SpinAngularSpeed"/>, of equal magnitude.</summary>
    public SpinAngularSpeed AsSpinAngularSpeed => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="OrbitalAngularSpeed"/>, expressed in <see cref="UnitOfAngularVelocity.RadianPerSecond"/>.</summary>
    public Scalar RadiansPerSecond => InUnit(UnitOfAngularVelocity.RadianPerSecond);
    /// <summary>Retrieves the magnitude of the <see cref="OrbitalAngularSpeed"/>, expressed in <see cref="UnitOfAngularVelocity.DegreePerSecond"/>.</summary>
    public Scalar DegreesPerSecond => InUnit(UnitOfAngularVelocity.DegreePerSecond);
    /// <summary>Retrieves the magnitude of the <see cref="OrbitalAngularSpeed"/>, expressed in <see cref="UnitOfAngularVelocity.RevolutionPerSecond"/>.</summary>
    public Scalar RevolutionsPerSecond => InUnit(UnitOfAngularVelocity.RevolutionPerSecond);
    /// <summary>Retrieves the magnitude of the <see cref="OrbitalAngularSpeed"/>, expressed in <see cref="UnitOfAngularVelocity.RevolutionPerMinute"/>.</summary>
    public Scalar RevolutionsPerMinute => InUnit(UnitOfAngularVelocity.RevolutionPerMinute);

    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularSpeed"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularSpeed"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularSpeed"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularSpeed"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularSpeed"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularSpeed"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularSpeed"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularSpeed"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="OrbitalAngularSpeed"/>.</summary>
    public OrbitalAngularSpeed Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="OrbitalAngularSpeed"/>.</summary>
    public OrbitalAngularSpeed Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="OrbitalAngularSpeed"/>.</summary>
    public OrbitalAngularSpeed Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="OrbitalAngularSpeed"/> to the nearest integer value.</summary>
    public OrbitalAngularSpeed Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(OrbitalAngularSpeed other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="OrbitalAngularSpeed"/> in the default unit
    /// <see cref="UnitOfAngularVelocity.RadianPerSecond"/>, followed by the symbol [rad∙s⁻¹].</summary>
    public override string ToString() => $"{RadiansPerSecond} [rad∙s⁻¹]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="OrbitalAngularSpeed"/>,
    /// expressed in <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="unitOfAngularVelocity">The <see cref="UnitOfAngularVelocity"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfAngularVelocity unitOfAngularVelocity) => InUnit(this, unitOfAngularVelocity);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="OrbitalAngularSpeed"/>,
    /// expressed in <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="orbitalAngularSpeed">The <see cref="OrbitalAngularSpeed"/> to be expressed in <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="unitOfAngularVelocity">The <see cref="UnitOfAngularVelocity"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(OrbitalAngularSpeed orbitalAngularSpeed, UnitOfAngularVelocity unitOfAngularVelocity) 
    	=> new(orbitalAngularSpeed.Magnitude / unitOfAngularVelocity.AngularSpeed.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="OrbitalAngularSpeed"/>.</summary>
    public OrbitalAngularSpeed Plus() => this;
    /// <summary>Negation, resulting in a <see cref="OrbitalAngularSpeed"/> with negated magnitude.</summary>
    public OrbitalAngularSpeed Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="OrbitalAngularSpeed"/>.</param>
    public static OrbitalAngularSpeed operator +(OrbitalAngularSpeed x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="OrbitalAngularSpeed"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="OrbitalAngularSpeed"/>.</param>
    public static OrbitalAngularSpeed operator -(OrbitalAngularSpeed x) => x.Negate();

    /// <summary>Multiplicates the <see cref="OrbitalAngularSpeed"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="OrbitalAngularSpeed"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="OrbitalAngularSpeed"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="OrbitalAngularSpeed"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="OrbitalAngularSpeed"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="OrbitalAngularSpeed"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="OrbitalAngularSpeed"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(OrbitalAngularSpeed x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="OrbitalAngularSpeed"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="OrbitalAngularSpeed"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="OrbitalAngularSpeed"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, OrbitalAngularSpeed y) => y.Multiply(x);
    /// <summary>Division of the <see cref="OrbitalAngularSpeed"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="OrbitalAngularSpeed"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="OrbitalAngularSpeed"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(OrbitalAngularSpeed x, Unhandled y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="OrbitalAngularSpeed"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public OrbitalAngularSpeed Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="OrbitalAngularSpeed"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="OrbitalAngularSpeed"/> is scaled.</param>
    public OrbitalAngularSpeed Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="OrbitalAngularSpeed"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="OrbitalAngularSpeed"/> is divided.</param>
    public OrbitalAngularSpeed Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="OrbitalAngularSpeed"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="OrbitalAngularSpeed"/> <paramref name="x"/> by this value.</param>
    public static OrbitalAngularSpeed operator %(OrbitalAngularSpeed x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="OrbitalAngularSpeed"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="OrbitalAngularSpeed"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="OrbitalAngularSpeed"/> <paramref name="x"/>.</param>
    public static OrbitalAngularSpeed operator *(OrbitalAngularSpeed x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="OrbitalAngularSpeed"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="OrbitalAngularSpeed"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="OrbitalAngularSpeed"/>, which is scaled by <paramref name="x"/>.</param>
    public static OrbitalAngularSpeed operator *(double x, OrbitalAngularSpeed y) => y.Multiply(x);
    /// <summary>Scales the <see cref="OrbitalAngularSpeed"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="OrbitalAngularSpeed"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="OrbitalAngularSpeed"/> <paramref name="x"/>.</param>
    public static OrbitalAngularSpeed operator /(OrbitalAngularSpeed x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="OrbitalAngularSpeed"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public OrbitalAngularSpeed Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="OrbitalAngularSpeed"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="OrbitalAngularSpeed"/> is scaled.</param>
    public OrbitalAngularSpeed Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="OrbitalAngularSpeed"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="OrbitalAngularSpeed"/> is divided.</param>
    public OrbitalAngularSpeed Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="OrbitalAngularSpeed"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="OrbitalAngularSpeed"/> <paramref name="x"/> by this value.</param>
    public static OrbitalAngularSpeed operator %(OrbitalAngularSpeed x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="OrbitalAngularSpeed"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="OrbitalAngularSpeed"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="OrbitalAngularSpeed"/> <paramref name="x"/>.</param>
    public static OrbitalAngularSpeed operator *(OrbitalAngularSpeed x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="OrbitalAngularSpeed"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="OrbitalAngularSpeed"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="OrbitalAngularSpeed"/>, which is scaled by <paramref name="x"/>.</param>
    public static OrbitalAngularSpeed operator *(Scalar x, OrbitalAngularSpeed y) => y.Multiply(x);
    /// <summary>Scales the <see cref="OrbitalAngularSpeed"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="OrbitalAngularSpeed"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="OrbitalAngularSpeed"/> <paramref name="x"/>.</param>
    public static OrbitalAngularSpeed operator /(OrbitalAngularSpeed x, Scalar y) => x.Divide(y);

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TProductScalarQuantity Multiply<TProductScalarQuantity, TFactorScalarQuantity>(TFactorScalarQuantity factor, Func<double, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorScalarQuantity : IScalarQuantity
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }
        else if (factor == null)
        {
            throw new ArgumentNullException(nameof(factor));
        }
        else
        {
            return factory(Magnitude * factor.Magnitude);
        }
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TQuotientScalarQuantity Divide<TQuotientScalarQuantity, TDivisorScalarQuantity>(TDivisorScalarQuantity divisor, Func<double, TQuotientScalarQuantity> factory)
        where TQuotientScalarQuantity : IScalarQuantity
        where TDivisorScalarQuantity : IScalarQuantity
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }
        else if (divisor == null)
        {
            throw new ArgumentNullException(nameof(divisor));
        }
        else
        {
            return factory(Magnitude / divisor.Magnitude);
        }
    }

    /// <summary>Multiplication of the <see cref="OrbitalAngularSpeed"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="OrbitalAngularSpeed"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="OrbitalAngularSpeed"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(OrbitalAngularSpeed x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="OrbitalAngularSpeed"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="OrbitalAngularSpeed"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="OrbitalAngularSpeed"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(OrbitalAngularSpeed x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Multiplicates the <see cref="OrbitalAngularSpeed"/> with the <see cref="Vector3"/> <paramref name="factor"/> to produce
    /// a <see cref="OrbitalAngularVelocity3"/>.</summary>
    /// <param name="factor">This <see cref="Vector3"/> is multiplied by the <see cref="OrbitalAngularSpeed"/>.</param>
    public OrbitalAngularVelocity3 Multiply(Vector3 factor) => new(factor * Magnitude);
    /// <summary>Multiplicates the <see cref="OrbitalAngularSpeed"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="OrbitalAngularVelocity3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="OrbitalAngularSpeed"/>.</param>
    public OrbitalAngularVelocity3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplicates the <see cref="OrbitalAngularSpeed"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="OrbitalAngularVelocity3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="OrbitalAngularSpeed"/>.</param>
    public OrbitalAngularVelocity3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplication of the <see cref="OrbitalAngularSpeed"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/>
    /// to produce a <see cref="OrbitalAngularVelocity3"/>.</summary>
    /// <param name="a">This <see cref="OrbitalAngularSpeed"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="OrbitalAngularSpeed"/> <paramref name="a"/>.</param>
    public static OrbitalAngularVelocity3 operator *(OrbitalAngularSpeed a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="OrbitalAngularSpeed"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/>
    /// to produce a <see cref="OrbitalAngularVelocity3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="OrbitalAngularSpeed"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="OrbitalAngularSpeed"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static OrbitalAngularVelocity3 operator *(Vector3 a, OrbitalAngularSpeed b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="OrbitalAngularSpeed"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="OrbitalAngularVelocity3"/>.</summary>
    /// <param name="a">This <see cref="OrbitalAngularSpeed"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="OrbitalAngularSpeed"/> <paramref name="a"/>.</param>
    public static OrbitalAngularVelocity3 operator *(OrbitalAngularSpeed a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="OrbitalAngularSpeed"/> <parmref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="OrbitalAngularVelocity3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="OrbitalAngularSpeed"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="OrbitalAngularSpeed"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static OrbitalAngularVelocity3 operator *((double x, double y, double z) a, OrbitalAngularSpeed b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="OrbitalAngularSpeed"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="OrbitalAngularVelocity3"/>.</summary>
    /// <param name="a">This <see cref="OrbitalAngularSpeed"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="OrbitalAngularSpeed"/> <paramref name="a"/>.</param>
    public static OrbitalAngularVelocity3 operator *(OrbitalAngularSpeed a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="OrbitalAngularSpeed"/> <parmref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="OrbitalAngularVelocity3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="OrbitalAngularSpeed"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="OrbitalAngularSpeed"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static OrbitalAngularVelocity3 operator *((Scalar x, Scalar y, Scalar z) a, OrbitalAngularSpeed b) => b.Multiply(a);

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="OrbitalAngularSpeed"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="OrbitalAngularSpeed"/>.</param>
    public static bool operator <(OrbitalAngularSpeed x, OrbitalAngularSpeed y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="OrbitalAngularSpeed"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="OrbitalAngularSpeed"/>.</param>
    public static bool operator >(OrbitalAngularSpeed x, OrbitalAngularSpeed y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="OrbitalAngularSpeed"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="OrbitalAngularSpeed"/>.</param>
    public static bool operator <=(OrbitalAngularSpeed x, OrbitalAngularSpeed y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="OrbitalAngularSpeed"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="OrbitalAngularSpeed"/>.</param>
    public static bool operator >=(OrbitalAngularSpeed x, OrbitalAngularSpeed y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="OrbitalAngularSpeed"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(OrbitalAngularSpeed x) => x.ToDouble();

    /// <summary>Converts the <see cref="OrbitalAngularSpeed"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(OrbitalAngularSpeed x) => x.ToScalar();

    /// <summary>Constructs the <see cref="OrbitalAngularSpeed"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static OrbitalAngularSpeed FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="OrbitalAngularSpeed"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator OrbitalAngularSpeed(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="OrbitalAngularSpeed"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static OrbitalAngularSpeed FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="OrbitalAngularSpeed"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator OrbitalAngularSpeed(Scalar x) => FromScalar(x);
}
