namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="OrbitalAngularMomentum"/>, a property of an object with <see cref="Mass"/> rotating about an external point.
/// This is the magnitude of the vector quantity <see cref="OrbitalAngularMomentum3"/>, and is expressed in <see cref="UnitOfAngularMomentum"/>, with the SI unit being [kg∙m²∙s⁻¹].
/// <para>
/// New instances of <see cref="OrbitalAngularMomentum"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfAngularMomentum"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="OrbitalAngularMomentum"/> a = 3 * <see cref="OrbitalAngularMomentum.OneKilogramSquareMetrePerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="OrbitalAngularMomentum"/> d = <see cref="OrbitalAngularMomentum.From(MomentOfInertia, OrbitalAngularSpeed)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="OrbitalAngularMomentum"/> e = <see cref="AngularMomentum.AsOrbitalAngularMomentum"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="OrbitalAngularMomentum"/> can be retrieved in the desired <see cref="UnitOfAngularMomentum"/> using pre-defined properties,
/// such as <see cref="KilogramSquareMetresPerSecond"/>
/// </para>
/// </summary>
/// <remarks>
/// <see cref="OrbitalAngularMomentum"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="AngularMomentum"/></term>
/// <description>Describes any type of angular momentum.</description>
/// </item>
/// <item>
/// <term><see cref="SpinAngularMomentum"/></term>
/// <description>Describes the <see cref="AngularMomentum"/> of an object in rotation about the center of mass of the object.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct OrbitalAngularMomentum :
    IComparable<OrbitalAngularMomentum>,
    IScalarQuantity,
    IScalableScalarQuantity<OrbitalAngularMomentum>,
    IMultiplicableScalarQuantity<OrbitalAngularMomentum, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<OrbitalAngularMomentum, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<OrbitalAngularMomentum3, Vector3>
{
    /// <summary>The zero-valued <see cref="OrbitalAngularMomentum"/>.</summary>
    public static OrbitalAngularMomentum Zero { get; } = new(0);

    /// <summary>The <see cref="OrbitalAngularMomentum"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngularMomentum.KilogramSquareMetrePerSecond"/>.</summary>
    public static OrbitalAngularMomentum OneKilogramSquareMetrePerSecond { get; } = new(1, UnitOfAngularMomentum.KilogramSquareMetrePerSecond);

    /// <summary>The magnitude of the <see cref="OrbitalAngularMomentum"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAngularMomentum)"/> or a pre-defined property
    /// - such as <see cref="KilogramSquareMetresPerSecond"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="OrbitalAngularMomentum"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="OrbitalAngularMomentum"/> a = 3 * <see cref="OrbitalAngularMomentum.OneKilogramSquareMetrePerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public OrbitalAngularMomentum(Scalar magnitude, UnitOfAngularMomentum unitOfAngularMomentum) : this(magnitude.Magnitude, unitOfAngularMomentum) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="OrbitalAngularMomentum"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="OrbitalAngularMomentum"/> a = 3 * <see cref="OrbitalAngularMomentum.OneKilogramSquareMetrePerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public OrbitalAngularMomentum(double magnitude, UnitOfAngularMomentum unitOfAngularMomentum) : this(magnitude * unitOfAngularMomentum.AngularMomentum.Magnitude) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="OrbitalAngularMomentum"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularMomentum(Scalar, UnitOfAngularMomentum)"/>.</remarks>
    public OrbitalAngularMomentum(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="OrbitalAngularMomentum"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularMomentum(double, UnitOfAngularMomentum)"/>.</remarks>
    public OrbitalAngularMomentum(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="OrbitalAngularMomentum"/> to an instance of the associated quantity <see cref="AngularMomentum"/>, of equal magnitude.</summary>
    public AngularMomentum AsAngularMomentum => new(Magnitude);
    /// <summary>Converts the <see cref="OrbitalAngularMomentum"/> to an instance of the associated quantity <see cref="SpinAngularMomentum"/>, of equal magnitude.</summary>
    public SpinAngularMomentum AsSpinAngularMomentum => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="OrbitalAngularMomentum"/>, expressed in <see cref="UnitOfAngularMomentum.KilogramSquareMetrePerSecond"/>.</summary>
    public Scalar KilogramSquareMetresPerSecond => InUnit(UnitOfAngularMomentum.KilogramSquareMetrePerSecond);

    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularMomentum"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularMomentum"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularMomentum"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularMomentum"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularMomentum"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularMomentum"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularMomentum"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="OrbitalAngularMomentum"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="OrbitalAngularMomentum"/>.</summary>
    public OrbitalAngularMomentum Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="OrbitalAngularMomentum"/>.</summary>
    public OrbitalAngularMomentum Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="OrbitalAngularMomentum"/>.</summary>
    public OrbitalAngularMomentum Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="OrbitalAngularMomentum"/> to the nearest integer value.</summary>
    public OrbitalAngularMomentum Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(OrbitalAngularMomentum other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="OrbitalAngularMomentum"/> in the default unit
    /// <see cref="UnitOfAngularMomentum.KilogramSquareMetrePerSecond"/>, followed by the symbol [kg∙m²∙s⁻¹].</summary>
    public override string ToString() => $"{KilogramSquareMetresPerSecond} [kg∙m²∙s⁻¹]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="OrbitalAngularMomentum"/>,
    /// expressed in <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfAngularMomentum unitOfAngularMomentum) => InUnit(this, unitOfAngularMomentum);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="OrbitalAngularMomentum"/>,
    /// expressed in <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="orbitalAngularMomentum">The <see cref="OrbitalAngularMomentum"/> to be expressed in <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(OrbitalAngularMomentum orbitalAngularMomentum, UnitOfAngularMomentum unitOfAngularMomentum) 
    	=> new(orbitalAngularMomentum.Magnitude / unitOfAngularMomentum.AngularMomentum.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="OrbitalAngularMomentum"/>.</summary>
    public OrbitalAngularMomentum Plus() => this;
    /// <summary>Negation, resulting in a <see cref="OrbitalAngularMomentum"/> with negated magnitude.</summary>
    public OrbitalAngularMomentum Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="OrbitalAngularMomentum"/>.</param>
    public static OrbitalAngularMomentum operator +(OrbitalAngularMomentum x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="OrbitalAngularMomentum"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="OrbitalAngularMomentum"/>.</param>
    public static OrbitalAngularMomentum operator -(OrbitalAngularMomentum x) => x.Negate();

    /// <summary>Multiplicates the <see cref="OrbitalAngularMomentum"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="OrbitalAngularMomentum"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="OrbitalAngularMomentum"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="OrbitalAngularMomentum"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="OrbitalAngularMomentum"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="OrbitalAngularMomentum"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="OrbitalAngularMomentum"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(OrbitalAngularMomentum x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="OrbitalAngularMomentum"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="OrbitalAngularMomentum"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="OrbitalAngularMomentum"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, OrbitalAngularMomentum y) => y.Multiply(x);
    /// <summary>Division of the <see cref="OrbitalAngularMomentum"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="OrbitalAngularMomentum"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="OrbitalAngularMomentum"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(OrbitalAngularMomentum x, Unhandled y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="OrbitalAngularMomentum"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public OrbitalAngularMomentum Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="OrbitalAngularMomentum"/> is scaled.</param>
    public OrbitalAngularMomentum Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="OrbitalAngularMomentum"/> is divided.</param>
    public OrbitalAngularMomentum Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="OrbitalAngularMomentum"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="OrbitalAngularMomentum"/> <paramref name="x"/> by this value.</param>
    public static OrbitalAngularMomentum operator %(OrbitalAngularMomentum x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="OrbitalAngularMomentum"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="OrbitalAngularMomentum"/> <paramref name="x"/>.</param>
    public static OrbitalAngularMomentum operator *(OrbitalAngularMomentum x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="OrbitalAngularMomentum"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="OrbitalAngularMomentum"/>, which is scaled by <paramref name="x"/>.</param>
    public static OrbitalAngularMomentum operator *(double x, OrbitalAngularMomentum y) => y.Multiply(x);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="OrbitalAngularMomentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="OrbitalAngularMomentum"/> <paramref name="x"/>.</param>
    public static OrbitalAngularMomentum operator /(OrbitalAngularMomentum x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="OrbitalAngularMomentum"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public OrbitalAngularMomentum Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="OrbitalAngularMomentum"/> is scaled.</param>
    public OrbitalAngularMomentum Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="OrbitalAngularMomentum"/> is divided.</param>
    public OrbitalAngularMomentum Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="OrbitalAngularMomentum"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="OrbitalAngularMomentum"/> <paramref name="x"/> by this value.</param>
    public static OrbitalAngularMomentum operator %(OrbitalAngularMomentum x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="OrbitalAngularMomentum"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="OrbitalAngularMomentum"/> <paramref name="x"/>.</param>
    public static OrbitalAngularMomentum operator *(OrbitalAngularMomentum x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="OrbitalAngularMomentum"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="OrbitalAngularMomentum"/>, which is scaled by <paramref name="x"/>.</param>
    public static OrbitalAngularMomentum operator *(Scalar x, OrbitalAngularMomentum y) => y.Multiply(x);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="OrbitalAngularMomentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="OrbitalAngularMomentum"/> <paramref name="x"/>.</param>
    public static OrbitalAngularMomentum operator /(OrbitalAngularMomentum x, Scalar y) => x.Divide(y);

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
        else
        {
            return factory(Magnitude / divisor.Magnitude);
        }
    }

    /// <summary>Multiplication of the <see cref="OrbitalAngularMomentum"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="OrbitalAngularMomentum"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="OrbitalAngularMomentum"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(OrbitalAngularMomentum x, IScalarQuantity y) => x.Multiply<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="OrbitalAngularMomentum"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="OrbitalAngularMomentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="OrbitalAngularMomentum"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(OrbitalAngularMomentum x, IScalarQuantity y) => x.Divide<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));

    /// <summary>Multiplicates the <see cref="OrbitalAngularMomentum"/> with the <see cref="Vector3"/> <paramref name="factor"/> to produce
    /// a <see cref="OrbitalAngularMomentum3"/>.</summary>
    /// <param name="factor">This <see cref="Vector3"/> is multiplied by the <see cref="OrbitalAngularMomentum"/>.</param>
    public OrbitalAngularMomentum3 Multiply(Vector3 factor) => new(factor * Magnitude);
    /// <summary>Multiplicates the <see cref="OrbitalAngularMomentum"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="OrbitalAngularMomentum3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="OrbitalAngularMomentum"/>.</param>
    public OrbitalAngularMomentum3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplicates the <see cref="OrbitalAngularMomentum"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="OrbitalAngularMomentum3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="OrbitalAngularMomentum"/>.</param>
    public OrbitalAngularMomentum3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplication of the <see cref="OrbitalAngularMomentum"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/>
    /// to produce a <see cref="OrbitalAngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="OrbitalAngularMomentum"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="OrbitalAngularMomentum"/> <paramref name="a"/>.</param>
    public static OrbitalAngularMomentum3 operator *(OrbitalAngularMomentum a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="OrbitalAngularMomentum"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/>
    /// to produce a <see cref="OrbitalAngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="OrbitalAngularMomentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="OrbitalAngularMomentum"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static OrbitalAngularMomentum3 operator *(Vector3 a, OrbitalAngularMomentum b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="OrbitalAngularMomentum"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="OrbitalAngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="OrbitalAngularMomentum"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="OrbitalAngularMomentum"/> <paramref name="a"/>.</param>
    public static OrbitalAngularMomentum3 operator *(OrbitalAngularMomentum a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="OrbitalAngularMomentum"/> <parmref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="OrbitalAngularMomentum3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="OrbitalAngularMomentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="OrbitalAngularMomentum"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static OrbitalAngularMomentum3 operator *((double x, double y, double z) a, OrbitalAngularMomentum b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="OrbitalAngularMomentum"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="OrbitalAngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="OrbitalAngularMomentum"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="OrbitalAngularMomentum"/> <paramref name="a"/>.</param>
    public static OrbitalAngularMomentum3 operator *(OrbitalAngularMomentum a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="OrbitalAngularMomentum"/> <parmref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="OrbitalAngularMomentum3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="OrbitalAngularMomentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="OrbitalAngularMomentum"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static OrbitalAngularMomentum3 operator *((Scalar x, Scalar y, Scalar z) a, OrbitalAngularMomentum b) => b.Multiply(a);

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="OrbitalAngularMomentum"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="OrbitalAngularMomentum"/>.</param>
    public static bool operator <(OrbitalAngularMomentum x, OrbitalAngularMomentum y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="OrbitalAngularMomentum"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="OrbitalAngularMomentum"/>.</param>
    public static bool operator >(OrbitalAngularMomentum x, OrbitalAngularMomentum y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="OrbitalAngularMomentum"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="OrbitalAngularMomentum"/>.</param>
    public static bool operator <=(OrbitalAngularMomentum x, OrbitalAngularMomentum y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="OrbitalAngularMomentum"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="OrbitalAngularMomentum"/>.</param>
    public static bool operator >=(OrbitalAngularMomentum x, OrbitalAngularMomentum y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="OrbitalAngularMomentum"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(OrbitalAngularMomentum x) => x.ToDouble();

    /// <summary>Converts the <see cref="OrbitalAngularMomentum"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(OrbitalAngularMomentum x) => x.ToScalar();

    /// <summary>Constructs the <see cref="OrbitalAngularMomentum"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static OrbitalAngularMomentum FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="OrbitalAngularMomentum"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator OrbitalAngularMomentum(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="OrbitalAngularMomentum"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static OrbitalAngularMomentum FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="OrbitalAngularMomentum"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator OrbitalAngularMomentum(Scalar x) => FromScalar(x);
}
