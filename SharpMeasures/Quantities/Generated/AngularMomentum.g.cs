namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="AngularMomentum"/>, a property of a rotating object with <see cref="Mass"/>.
/// This is the magnitude of the vector quantity <see cref="AngularMomentum3"/>, and is expressed in <see cref="UnitOfAngularMomentum"/>, with the SI unit being [kg∙m²∙s⁻¹].
/// <para>
/// New instances of <see cref="AngularMomentum"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfAngularMomentum"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="AngularMomentum"/> a = 3 * <see cref="AngularMomentum.OneKilogramSquareMetrePerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="AngularMomentum"/> d = <see cref="AngularMomentum.From(MomentOfInertia, AngularSpeed)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="AngularMomentum"/> e = <see cref="SpinAngularMomentum.AsAngularMomentum"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="AngularMomentum"/> can be retrieved in the desired <see cref="UnitOfAngularMomentum"/> using pre-defined properties,
/// such as <see cref="KilogramSquareMetresPerSecond"/>
/// </para>
/// </summary>
/// <remarks>
/// <see cref="AngularMomentum"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="OrbitalAngularMomentum"/></term>
/// <description>Describes the <see cref="AngularMomentum"/> of an object in rotation about an external point.</description>
/// </item>
/// <item>
/// <term><see cref="SpinAngularMomentum"/></term>
/// <description>Describes the <see cref="AngularMomentum"/> of an object in rotation about the center of mass of the object.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct AngularMomentum :
    IComparable<AngularMomentum>,
    IScalarQuantity,
    IScalableScalarQuantity<AngularMomentum>,
    IMultiplicableScalarQuantity<AngularMomentum, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<AngularMomentum, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<AngularMomentum3, Vector3>
{
    /// <summary>The zero-valued <see cref="AngularMomentum"/>.</summary>
    public static AngularMomentum Zero { get; } = new(0);

    /// <summary>The <see cref="AngularMomentum"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngularMomentum.KilogramSquareMetrePerSecond"/>.</summary>
    public static AngularMomentum OneKilogramSquareMetrePerSecond { get; } = new(1, UnitOfAngularMomentum.KilogramSquareMetrePerSecond);

    /// <summary>The magnitude of the <see cref="AngularMomentum"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAngularMomentum)"/> or a pre-defined property
    /// - such as <see cref="KilogramSquareMetresPerSecond"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="AngularMomentum"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularMomentum"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="AngularMomentum"/> a = 3 * <see cref="AngularMomentum.OneKilogramSquareMetrePerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public AngularMomentum(Scalar magnitude, UnitOfAngularMomentum unitOfAngularMomentum) : this(magnitude.Magnitude, unitOfAngularMomentum) { }
    /// <summary>Constructs a new <see cref="AngularMomentum"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularMomentum"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="AngularMomentum"/> a = 3 * <see cref="AngularMomentum.OneKilogramSquareMetrePerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public AngularMomentum(double magnitude, UnitOfAngularMomentum unitOfAngularMomentum) : this(magnitude * unitOfAngularMomentum.AngularMomentum.Magnitude) { }
    /// <summary>Constructs a new <see cref="AngularMomentum"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularMomentum"/>.</param>
    /// <remarks>Consider preferring <see cref="AngularMomentum(Scalar, UnitOfAngularMomentum)"/>.</remarks>
    public AngularMomentum(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="AngularMomentum"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularMomentum"/>.</param>
    /// <remarks>Consider preferring <see cref="AngularMomentum(double, UnitOfAngularMomentum)"/>.</remarks>
    public AngularMomentum(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="AngularMomentum"/> to an instance of the associated quantity <see cref="OrbitalAngularMomentum"/>, of equal magnitude.</summary>
    public OrbitalAngularMomentum AsOrbitalAngularMomentum => new(Magnitude);
    /// <summary>Converts the <see cref="AngularMomentum"/> to an instance of the associated quantity <see cref="SpinAngularMomentum"/>, of equal magnitude.</summary>
    public SpinAngularMomentum AsSpinAngularMomentum => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="AngularMomentum"/>, expressed in <see cref="UnitOfAngularMomentum.KilogramSquareMetrePerSecond"/>.</summary>
    public Scalar KilogramSquareMetresPerSecond => InUnit(UnitOfAngularMomentum.KilogramSquareMetrePerSecond);

    /// <summary>Indicates whether the magnitude of the <see cref="AngularMomentum"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="AngularMomentum"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="AngularMomentum"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="AngularMomentum"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="AngularMomentum"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="AngularMomentum"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="AngularMomentum"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="AngularMomentum"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="AngularMomentum"/>.</summary>
    public AngularMomentum Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="AngularMomentum"/>.</summary>
    public AngularMomentum Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="AngularMomentum"/>.</summary>
    public AngularMomentum Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="AngularMomentum"/> to the nearest integer value.</summary>
    public AngularMomentum Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(AngularMomentum other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="AngularMomentum"/> in the default unit
    /// <see cref="UnitOfAngularMomentum.KilogramSquareMetrePerSecond"/>, followed by the symbol [kg∙m²∙s⁻¹].</summary>
    public override string ToString() => $"{KilogramSquareMetresPerSecond} [kg∙m²∙s⁻¹]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="AngularMomentum"/>,
    /// expressed in <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfAngularMomentum unitOfAngularMomentum) => InUnit(this, unitOfAngularMomentum);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="AngularMomentum"/>,
    /// expressed in <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="angularMomentum">The <see cref="AngularMomentum"/> to be expressed in <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(AngularMomentum angularMomentum, UnitOfAngularMomentum unitOfAngularMomentum) 
    	=> new(angularMomentum.Magnitude / unitOfAngularMomentum.AngularMomentum.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="AngularMomentum"/>.</summary>
    public AngularMomentum Plus() => this;
    /// <summary>Negation, resulting in a <see cref="AngularMomentum"/> with negated magnitude.</summary>
    public AngularMomentum Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="AngularMomentum"/>.</param>
    public static AngularMomentum operator +(AngularMomentum x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="AngularMomentum"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="AngularMomentum"/>.</param>
    public static AngularMomentum operator -(AngularMomentum x) => x.Negate();

    /// <summary>Multiplicates the <see cref="AngularMomentum"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="AngularMomentum"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="AngularMomentum"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="AngularMomentum"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="AngularMomentum"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="AngularMomentum"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(AngularMomentum x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="AngularMomentum"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="AngularMomentum"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="AngularMomentum"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, AngularMomentum y) => y.Multiply(x);
    /// <summary>Division of the <see cref="AngularMomentum"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="AngularMomentum"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(AngularMomentum x, Unhandled y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="AngularMomentum"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public AngularMomentum Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="AngularMomentum"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="AngularMomentum"/> is scaled.</param>
    public AngularMomentum Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="AngularMomentum"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="AngularMomentum"/> is divided.</param>
    public AngularMomentum Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="AngularMomentum"/> <paramref name="x"/> by this value.</param>
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

    /// <summary>Computes the remainder from division of the <see cref="AngularMomentum"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public AngularMomentum Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="AngularMomentum"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="AngularMomentum"/> is scaled.</param>
    public AngularMomentum Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="AngularMomentum"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="AngularMomentum"/> is divided.</param>
    public AngularMomentum Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="AngularMomentum"/> <paramref name="x"/> by this value.</param>
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

    /// <summary>Multiplication of the <see cref="AngularMomentum"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="AngularMomentum"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(AngularMomentum x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="AngularMomentum"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularMomentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="AngularMomentum"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(AngularMomentum x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Multiplicates the <see cref="AngularMomentum"/> with the <see cref="Vector3"/> <paramref name="factor"/> to produce
    /// a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="factor">This <see cref="Vector3"/> is multiplied by the <see cref="AngularMomentum"/>.</param>
    public AngularMomentum3 Multiply(Vector3 factor) => new(factor * Magnitude);
    /// <summary>Multiplicates the <see cref="AngularMomentum"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="AngularMomentum"/>.</param>
    public AngularMomentum3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplicates the <see cref="AngularMomentum"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="AngularMomentum"/>.</param>
    public AngularMomentum3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplication of the <see cref="AngularMomentum"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/>
    /// to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="AngularMomentum"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="AngularMomentum"/> <paramref name="a"/>.</param>
    public static AngularMomentum3 operator *(AngularMomentum a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="AngularMomentum"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/>
    /// to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="AngularMomentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="AngularMomentum"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static AngularMomentum3 operator *(Vector3 a, AngularMomentum b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="AngularMomentum"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="AngularMomentum"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="AngularMomentum"/> <paramref name="a"/>.</param>
    public static AngularMomentum3 operator *(AngularMomentum a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="AngularMomentum"/> <parmref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="AngularMomentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="AngularMomentum"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static AngularMomentum3 operator *((double x, double y, double z) a, AngularMomentum b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="AngularMomentum"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="AngularMomentum"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="AngularMomentum"/> <paramref name="a"/>.</param>
    public static AngularMomentum3 operator *(AngularMomentum a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="AngularMomentum"/> <parmref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="AngularMomentum3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="AngularMomentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="AngularMomentum"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static AngularMomentum3 operator *((Scalar x, Scalar y, Scalar z) a, AngularMomentum b) => b.Multiply(a);

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="AngularMomentum"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="AngularMomentum"/>.</param>
    public static bool operator <(AngularMomentum x, AngularMomentum y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="AngularMomentum"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="AngularMomentum"/>.</param>
    public static bool operator >(AngularMomentum x, AngularMomentum y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="AngularMomentum"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="AngularMomentum"/>.</param>
    public static bool operator <=(AngularMomentum x, AngularMomentum y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="AngularMomentum"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="AngularMomentum"/>.</param>
    public static bool operator >=(AngularMomentum x, AngularMomentum y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="AngularMomentum"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(AngularMomentum x) => x.ToDouble();

    /// <summary>Converts the <see cref="AngularMomentum"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(AngularMomentum x) => x.ToScalar();

    /// <summary>Constructs the <see cref="AngularMomentum"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static AngularMomentum FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="AngularMomentum"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator AngularMomentum(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="AngularMomentum"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static AngularMomentum FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="AngularMomentum"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator AngularMomentum(Scalar x) => FromScalar(x);
}
