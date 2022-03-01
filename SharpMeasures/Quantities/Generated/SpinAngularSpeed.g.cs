#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="SpinAngularSpeed"/>, describing the <see cref="AngularSpeed"/> of an object about the internal center of rotation.
/// This is the magnitude of the vector quantity <see cref="SpinAngularVelocity3"/>, and is expressed in <see cref="UnitOfAngularVelocity"/>, with the SI unit being [rad∙s⁻¹].
/// <para>
/// New instances of <see cref="SpinAngularSpeed"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfAngularVelocity"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="SpinAngularSpeed"/> a = 3 * <see cref="SpinAngularSpeed.OneRadianPerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="SpinAngularSpeed"/> d = <see cref="SpinAngularSpeed.From(Angle, Time)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="SpinAngularSpeed"/> e = <see cref="AngularSpeed.AsSpinAngularSpeed"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="SpinAngularSpeed"/> can be retrieved in the desired <see cref="UnitOfAngularVelocity"/> using pre-defined properties,
/// such as <see cref="RadiansPerSecond"/>
/// </para>
/// </summary>
/// <remarks>
/// <see cref="SpinAngularSpeed"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="AngularSpeed"/></term>
/// <description>Describes any type of angular speed.</description>
/// </item>
/// <item>
/// <term><see cref="OrbitalAngularSpeed"/></term>
/// <description>Describes the <see cref="AngularSpeed"/> of an object about an external point.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct SpinAngularSpeed :
    IComparable<SpinAngularSpeed>,
    IScalarQuantity,
    IScalableScalarQuantity<SpinAngularSpeed>,
    IMultiplicableScalarQuantity<SpinAngularSpeed, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<SpinAngularSpeed, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<SpinAngularVelocity3, Vector3>
{
    /// <summary>The zero-valued <see cref="SpinAngularSpeed"/>.</summary>
    public static SpinAngularSpeed Zero { get; } = new(0);

    /// <summary>The <see cref="SpinAngularSpeed"/> of magnitude 1, when expressed in <see cref="UnitOfAngularVelocity.RadianPerSecond"/>.</summary>
    public static SpinAngularSpeed OneRadianPerSecond { get; } = UnitOfAngularVelocity.RadianPerSecond.AngularSpeed.AsSpinAngularSpeed;
    /// <summary>The <see cref="SpinAngularSpeed"/> of magnitude 1, when expressed in <see cref="UnitOfAngularVelocity.DegreePerSecond"/>.</summary>
    public static SpinAngularSpeed OneDegreePerSecond { get; } = UnitOfAngularVelocity.DegreePerSecond.AngularSpeed.AsSpinAngularSpeed;
    /// <summary>The <see cref="SpinAngularSpeed"/> of magnitude 1, when expressed in <see cref="UnitOfAngularVelocity.RevolutionPerSecond"/>.</summary>
    public static SpinAngularSpeed OneRevolutionPerSecond { get; } = UnitOfAngularVelocity.RevolutionPerSecond.AngularSpeed.AsSpinAngularSpeed;
    /// <summary>The <see cref="SpinAngularSpeed"/> of magnitude 1, when expressed in <see cref="UnitOfAngularVelocity.RevolutionPerMinute"/>.</summary>
    public static SpinAngularSpeed OneRevolutionPerMinute { get; } = UnitOfAngularVelocity.RevolutionPerMinute.AngularSpeed.AsSpinAngularSpeed;

    /// <summary>The magnitude of the <see cref="SpinAngularSpeed"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAngularVelocity)"/> or a pre-defined property
    /// - such as <see cref="RadiansPerSecond"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="SpinAngularSpeed"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpinAngularSpeed"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="unitOfAngularVelocity">The <see cref="UnitOfAngularVelocity"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SpinAngularSpeed"/> a = 3 * <see cref="SpinAngularSpeed.OneRadianPerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SpinAngularSpeed(Scalar magnitude, UnitOfAngularVelocity unitOfAngularVelocity) : this(magnitude.Magnitude, unitOfAngularVelocity) { }
    /// <summary>Constructs a new <see cref="SpinAngularSpeed"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpinAngularSpeed"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="unitOfAngularVelocity">The <see cref="UnitOfAngularVelocity"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SpinAngularSpeed"/> a = 3 * <see cref="SpinAngularSpeed.OneRadianPerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SpinAngularSpeed(double magnitude, UnitOfAngularVelocity unitOfAngularVelocity) : this(magnitude * unitOfAngularVelocity.AngularSpeed.Magnitude) { }
    /// <summary>Constructs a new <see cref="SpinAngularSpeed"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpinAngularSpeed"/>.</param>
    /// <remarks>Consider preferring <see cref="SpinAngularSpeed(Scalar, UnitOfAngularVelocity)"/>.</remarks>
    public SpinAngularSpeed(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="SpinAngularSpeed"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpinAngularSpeed"/>.</param>
    /// <remarks>Consider preferring <see cref="SpinAngularSpeed(double, UnitOfAngularVelocity)"/>.</remarks>
    public SpinAngularSpeed(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="SpinAngularSpeed"/> to an instance of the associated quantity <see cref="AngularSpeed"/>, of equal magnitude.</summary>
    public AngularSpeed AsAngularSpeed => new(Magnitude);
    /// <summary>Converts the <see cref="SpinAngularSpeed"/> to an instance of the associated quantity <see cref="OrbitalAngularSpeed"/>, of equal magnitude.</summary>
    public OrbitalAngularSpeed AsOrbitalAngularSpeed => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="SpinAngularSpeed"/>, expressed in <see cref="UnitOfAngularVelocity.RadianPerSecond"/>.</summary>
    public Scalar RadiansPerSecond => InUnit(UnitOfAngularVelocity.RadianPerSecond);
    /// <summary>Retrieves the magnitude of the <see cref="SpinAngularSpeed"/>, expressed in <see cref="UnitOfAngularVelocity.DegreePerSecond"/>.</summary>
    public Scalar DegreesPerSecond => InUnit(UnitOfAngularVelocity.DegreePerSecond);
    /// <summary>Retrieves the magnitude of the <see cref="SpinAngularSpeed"/>, expressed in <see cref="UnitOfAngularVelocity.RevolutionPerSecond"/>.</summary>
    public Scalar RevolutionsPerSecond => InUnit(UnitOfAngularVelocity.RevolutionPerSecond);
    /// <summary>Retrieves the magnitude of the <see cref="SpinAngularSpeed"/>, expressed in <see cref="UnitOfAngularVelocity.RevolutionPerMinute"/>.</summary>
    public Scalar RevolutionsPerMinute => InUnit(UnitOfAngularVelocity.RevolutionPerMinute);

    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularSpeed"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularSpeed"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularSpeed"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularSpeed"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularSpeed"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularSpeed"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularSpeed"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularSpeed"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="SpinAngularSpeed"/>.</summary>
    public SpinAngularSpeed Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="SpinAngularSpeed"/>.</summary>
    public SpinAngularSpeed Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="SpinAngularSpeed"/>.</summary>
    public SpinAngularSpeed Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="SpinAngularSpeed"/> to the nearest integer value.</summary>
    public SpinAngularSpeed Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(SpinAngularSpeed other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="SpinAngularSpeed"/> in the default unit
    /// <see cref="UnitOfAngularVelocity.RadianPerSecond"/>, followed by the symbol [rad∙s⁻¹].</summary>
    public override string ToString() => $"{RadiansPerSecond} [rad∙s⁻¹]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="SpinAngularSpeed"/>,
    /// expressed in <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="unitOfAngularVelocity">The <see cref="UnitOfAngularVelocity"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfAngularVelocity unitOfAngularVelocity) => InUnit(this, unitOfAngularVelocity);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="SpinAngularSpeed"/>,
    /// expressed in <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="spinAngularSpeed">The <see cref="SpinAngularSpeed"/> to be expressed in <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="unitOfAngularVelocity">The <see cref="UnitOfAngularVelocity"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(SpinAngularSpeed spinAngularSpeed, UnitOfAngularVelocity unitOfAngularVelocity) 
    	=> new(spinAngularSpeed.Magnitude / unitOfAngularVelocity.AngularSpeed.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="SpinAngularSpeed"/>.</summary>
    public SpinAngularSpeed Plus() => this;
    /// <summary>Negation, resulting in a <see cref="SpinAngularSpeed"/> with negated magnitude.</summary>
    public SpinAngularSpeed Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="SpinAngularSpeed"/>.</param>
    public static SpinAngularSpeed operator +(SpinAngularSpeed x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="SpinAngularSpeed"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="SpinAngularSpeed"/>.</param>
    public static SpinAngularSpeed operator -(SpinAngularSpeed x) => x.Negate();

    /// <summary>Multiplicates the <see cref="SpinAngularSpeed"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="SpinAngularSpeed"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="SpinAngularSpeed"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="SpinAngularSpeed"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="SpinAngularSpeed"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpinAngularSpeed"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SpinAngularSpeed"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(SpinAngularSpeed x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="SpinAngularSpeed"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="SpinAngularSpeed"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="SpinAngularSpeed"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, SpinAngularSpeed y) => y.Multiply(x);
    /// <summary>Division of the <see cref="SpinAngularSpeed"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpinAngularSpeed"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SpinAngularSpeed"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(SpinAngularSpeed x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="SpinAngularSpeed"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="SpinAngularSpeed"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpinAngularSpeed"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, SpinAngularSpeed y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="SpinAngularSpeed"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public SpinAngularSpeed Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="SpinAngularSpeed"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SpinAngularSpeed"/> is scaled.</param>
    public SpinAngularSpeed Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="SpinAngularSpeed"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SpinAngularSpeed"/> is divided.</param>
    public SpinAngularSpeed Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpinAngularSpeed"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="SpinAngularSpeed"/> <paramref name="x"/> by this value.</param>
    public static SpinAngularSpeed operator %(SpinAngularSpeed x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SpinAngularSpeed"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpinAngularSpeed"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SpinAngularSpeed"/> <paramref name="x"/>.</param>
    public static SpinAngularSpeed operator *(SpinAngularSpeed x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SpinAngularSpeed"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SpinAngularSpeed"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpinAngularSpeed"/>, which is scaled by <paramref name="x"/>.</param>
    public static SpinAngularSpeed operator *(double x, SpinAngularSpeed y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SpinAngularSpeed"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpinAngularSpeed"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SpinAngularSpeed"/> <paramref name="x"/>.</param>
    public static SpinAngularSpeed operator /(SpinAngularSpeed x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="SpinAngularSpeed"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public SpinAngularSpeed Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="SpinAngularSpeed"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SpinAngularSpeed"/> is scaled.</param>
    public SpinAngularSpeed Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="SpinAngularSpeed"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SpinAngularSpeed"/> is divided.</param>
    public SpinAngularSpeed Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpinAngularSpeed"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="SpinAngularSpeed"/> <paramref name="x"/> by this value.</param>
    public static SpinAngularSpeed operator %(SpinAngularSpeed x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SpinAngularSpeed"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpinAngularSpeed"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SpinAngularSpeed"/> <paramref name="x"/>.</param>
    public static SpinAngularSpeed operator *(SpinAngularSpeed x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SpinAngularSpeed"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SpinAngularSpeed"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpinAngularSpeed"/>, which is scaled by <paramref name="x"/>.</param>
    public static SpinAngularSpeed operator *(Scalar x, SpinAngularSpeed y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SpinAngularSpeed"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpinAngularSpeed"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SpinAngularSpeed"/> <paramref name="x"/>.</param>
    public static SpinAngularSpeed operator /(SpinAngularSpeed x, Scalar y) => x.Divide(y);

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

    /// <summary>Multiplication of the <see cref="SpinAngularSpeed"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpinAngularSpeed"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="SpinAngularSpeed"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(SpinAngularSpeed x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="SpinAngularSpeed"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpinAngularSpeed"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="SpinAngularSpeed"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(SpinAngularSpeed x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Multiplicates the <see cref="SpinAngularSpeed"/> with the <see cref="Vector3"/> <paramref name="factor"/> to produce
    /// a <see cref="SpinAngularVelocity3"/>.</summary>
    /// <param name="factor">This <see cref="Vector3"/> is multiplied by the <see cref="SpinAngularSpeed"/>.</param>
    public SpinAngularVelocity3 Multiply(Vector3 factor) => new(factor * Magnitude);
    /// <summary>Multiplicates the <see cref="SpinAngularSpeed"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="SpinAngularVelocity3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="SpinAngularSpeed"/>.</param>
    public SpinAngularVelocity3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplicates the <see cref="SpinAngularSpeed"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="SpinAngularVelocity3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="SpinAngularSpeed"/>.</param>
    public SpinAngularVelocity3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplication of the <see cref="SpinAngularSpeed"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/>
    /// to produce a <see cref="SpinAngularVelocity3"/>.</summary>
    /// <param name="a">This <see cref="SpinAngularSpeed"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="SpinAngularSpeed"/> <paramref name="a"/>.</param>
    public static SpinAngularVelocity3 operator *(SpinAngularSpeed a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="SpinAngularSpeed"/> <paramref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/>
    /// to produce a <see cref="SpinAngularVelocity3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="SpinAngularSpeed"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="SpinAngularSpeed"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static SpinAngularVelocity3 operator *(Vector3 a, SpinAngularSpeed b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="SpinAngularSpeed"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="SpinAngularVelocity3"/>.</summary>
    /// <param name="a">This <see cref="SpinAngularSpeed"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="SpinAngularSpeed"/> <paramref name="a"/>.</param>
    public static SpinAngularVelocity3 operator *(SpinAngularSpeed a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="SpinAngularSpeed"/> <paramref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="SpinAngularVelocity3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="SpinAngularSpeed"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="SpinAngularSpeed"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static SpinAngularVelocity3 operator *((double x, double y, double z) a, SpinAngularSpeed b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="SpinAngularSpeed"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="SpinAngularVelocity3"/>.</summary>
    /// <param name="a">This <see cref="SpinAngularSpeed"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="SpinAngularSpeed"/> <paramref name="a"/>.</param>
    public static SpinAngularVelocity3 operator *(SpinAngularSpeed a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="SpinAngularSpeed"/> <paramref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="SpinAngularVelocity3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="SpinAngularSpeed"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="SpinAngularSpeed"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static SpinAngularVelocity3 operator *((Scalar x, Scalar y, Scalar z) a, SpinAngularSpeed b) => b.Multiply(a);

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SpinAngularSpeed"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="SpinAngularSpeed"/>.</param>
    public static bool operator <(SpinAngularSpeed x, SpinAngularSpeed y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SpinAngularSpeed"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="SpinAngularSpeed"/>.</param>
    public static bool operator >(SpinAngularSpeed x, SpinAngularSpeed y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SpinAngularSpeed"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="SpinAngularSpeed"/>.</param>
    public static bool operator <=(SpinAngularSpeed x, SpinAngularSpeed y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SpinAngularSpeed"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="SpinAngularSpeed"/>.</param>
    public static bool operator >=(SpinAngularSpeed x, SpinAngularSpeed y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="SpinAngularSpeed"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(SpinAngularSpeed x) => x.ToDouble();

    /// <summary>Converts the <see cref="SpinAngularSpeed"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(SpinAngularSpeed x) => x.ToScalar();

    /// <summary>Constructs the <see cref="SpinAngularSpeed"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static SpinAngularSpeed FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="SpinAngularSpeed"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator SpinAngularSpeed(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="SpinAngularSpeed"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static SpinAngularSpeed FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="SpinAngularSpeed"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator SpinAngularSpeed(Scalar x) => FromScalar(x);
}
