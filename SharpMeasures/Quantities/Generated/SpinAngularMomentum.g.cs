#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="SpinAngularMomentum"/>, a property of an object with <see cref="Mass"/> rotating about the center of mass of the object.
/// This is the magnitude of the vector quantity <see cref="OrbitalAngularMomentum3"/>, and is expressed in <see cref="UnitOfAngularMomentum"/>, with the SI unit being [kg∙m²∙s⁻¹].
/// <para>
/// New instances of <see cref="SpinAngularMomentum"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfAngularMomentum"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code><see cref="SpinAngularMomentum"/> a = 3 * <see cref="SpinAngularMomentum.OneKilogramSquareMetrePerSecond"/>;</code>
/// </item>
/// <item>
/// <code><see cref="SpinAngularMomentum"/> d = <see cref="SpinAngularMomentum.From(MomentOfInertia, SpinAngularSpeed)"/>;</code>
/// </item>
/// <item>
/// <code><see cref="SpinAngularMomentum"/> e = <see cref="AngularMomentum.AsSpinAngularMomentum"/>;</code>
/// </item>
/// </list>
/// The magnitude of the <see cref="SpinAngularMomentum"/> can be retrieved in the desired <see cref="UnitOfAngularMomentum"/> using pre-defined properties,
/// such as <see cref="KilogramSquareMetresPerSecond"/>
/// </para>
/// </summary>
/// <remarks>
/// <see cref="SpinAngularMomentum"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="AngularMomentum"/></term>
/// <description>Describes any type of angular momentum.</description>
/// </item>
/// <item>
/// <term><see cref="OrbitalAngularMomentum"/></term>
/// <description>Describes the <see cref="AngularMomentum"/> of an object in rotation about an external point.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct SpinAngularMomentum :
    IComparable<SpinAngularMomentum>,
    IScalarQuantity,
    IScalableScalarQuantity<SpinAngularMomentum>,
    IMultiplicableScalarQuantity<SpinAngularMomentum, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<SpinAngularMomentum, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<SpinAngularMomentum3, Vector3>
{
    /// <summary>The zero-valued <see cref="SpinAngularMomentum"/>.</summary>
    public static SpinAngularMomentum Zero { get; } = new(0);

    /// <summary>The <see cref="SpinAngularMomentum"/> of magnitude 1, when expressed in <see cref="UnitOfAngularMomentum.KilogramSquareMetrePerSecond"/>.</summary>
    public static SpinAngularMomentum OneKilogramSquareMetrePerSecond { get; } = UnitOfAngularMomentum.KilogramSquareMetrePerSecond.AngularMomentum.AsSpinAngularMomentum;

    /// <summary>The magnitude of the <see cref="SpinAngularMomentum"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAngularMomentum)"/> or a pre-defined property
    /// - such as <see cref="KilogramSquareMetresPerSecond"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="SpinAngularMomentum"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpinAngularMomentum"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SpinAngularMomentum"/> a = 3 * <see cref="SpinAngularMomentum.OneKilogramSquareMetrePerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SpinAngularMomentum(Scalar magnitude, UnitOfAngularMomentum unitOfAngularMomentum) : this(magnitude.Magnitude, unitOfAngularMomentum) { }
    /// <summary>Constructs a new <see cref="SpinAngularMomentum"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpinAngularMomentum"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SpinAngularMomentum"/> a = 3 * <see cref="SpinAngularMomentum.OneKilogramSquareMetrePerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SpinAngularMomentum(double magnitude, UnitOfAngularMomentum unitOfAngularMomentum) : this(magnitude * unitOfAngularMomentum.AngularMomentum.Magnitude) { }
    /// <summary>Constructs a new <see cref="SpinAngularMomentum"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpinAngularMomentum"/>.</param>
    /// <remarks>Consider preferring <see cref="SpinAngularMomentum(Scalar, UnitOfAngularMomentum)"/>.</remarks>
    public SpinAngularMomentum(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="SpinAngularMomentum"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpinAngularMomentum"/>.</param>
    /// <remarks>Consider preferring <see cref="SpinAngularMomentum(double, UnitOfAngularMomentum)"/>.</remarks>
    public SpinAngularMomentum(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="SpinAngularMomentum"/> to an instance of the associated quantity <see cref="AngularMomentum"/>, of equal magnitude.</summary>
    public AngularMomentum AsAngularMomentum => new(Magnitude);
    /// <summary>Converts the <see cref="SpinAngularMomentum"/> to an instance of the associated quantity <see cref="OrbitalAngularMomentum"/>, of equal magnitude.</summary>
    public OrbitalAngularMomentum AsOrbitalAngularMomentum => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="SpinAngularMomentum"/>, expressed in <see cref="UnitOfAngularMomentum.KilogramSquareMetrePerSecond"/>.</summary>
    public Scalar KilogramSquareMetresPerSecond => InUnit(UnitOfAngularMomentum.KilogramSquareMetrePerSecond);

    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularMomentum"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularMomentum"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularMomentum"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularMomentum"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularMomentum"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularMomentum"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularMomentum"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpinAngularMomentum"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="SpinAngularMomentum"/>.</summary>
    public SpinAngularMomentum Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="SpinAngularMomentum"/>.</summary>
    public SpinAngularMomentum Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="SpinAngularMomentum"/>.</summary>
    public SpinAngularMomentum Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="SpinAngularMomentum"/> to the nearest integer value.</summary>
    public SpinAngularMomentum Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(SpinAngularMomentum other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="SpinAngularMomentum"/> in the default unit
    /// <see cref="UnitOfAngularMomentum.KilogramSquareMetrePerSecond"/>, followed by the symbol [kg∙m²∙s⁻¹].</summary>
    public override string ToString() => $"{KilogramSquareMetresPerSecond} [kg∙m²∙s⁻¹]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="SpinAngularMomentum"/>,
    /// expressed in <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfAngularMomentum unitOfAngularMomentum) => InUnit(this, unitOfAngularMomentum);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="SpinAngularMomentum"/>,
    /// expressed in <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="spinAngularMomentum">The <see cref="SpinAngularMomentum"/> to be expressed in <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(SpinAngularMomentum spinAngularMomentum, UnitOfAngularMomentum unitOfAngularMomentum) 
    	=> new(spinAngularMomentum.Magnitude / unitOfAngularMomentum.AngularMomentum.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="SpinAngularMomentum"/>.</summary>
    public SpinAngularMomentum Plus() => this;
    /// <summary>Negation, resulting in a <see cref="SpinAngularMomentum"/> with negated magnitude.</summary>
    public SpinAngularMomentum Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="SpinAngularMomentum"/>.</param>
    public static SpinAngularMomentum operator +(SpinAngularMomentum x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="SpinAngularMomentum"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="SpinAngularMomentum"/>.</param>
    public static SpinAngularMomentum operator -(SpinAngularMomentum x) => x.Negate();

    /// <summary>Multiplicates the <see cref="SpinAngularMomentum"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="SpinAngularMomentum"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="SpinAngularMomentum"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="SpinAngularMomentum"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="SpinAngularMomentum"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpinAngularMomentum"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SpinAngularMomentum"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(SpinAngularMomentum x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="SpinAngularMomentum"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="SpinAngularMomentum"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="SpinAngularMomentum"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, SpinAngularMomentum y) => y.Multiply(x);
    /// <summary>Division of the <see cref="SpinAngularMomentum"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpinAngularMomentum"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SpinAngularMomentum"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(SpinAngularMomentum x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="SpinAngularMomentum"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="SpinAngularMomentum"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpinAngularMomentum"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, SpinAngularMomentum y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="SpinAngularMomentum"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public SpinAngularMomentum Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="SpinAngularMomentum"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SpinAngularMomentum"/> is scaled.</param>
    public SpinAngularMomentum Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="SpinAngularMomentum"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SpinAngularMomentum"/> is divided.</param>
    public SpinAngularMomentum Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpinAngularMomentum"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="SpinAngularMomentum"/> <paramref name="x"/> by this value.</param>
    public static SpinAngularMomentum operator %(SpinAngularMomentum x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SpinAngularMomentum"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpinAngularMomentum"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SpinAngularMomentum"/> <paramref name="x"/>.</param>
    public static SpinAngularMomentum operator *(SpinAngularMomentum x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SpinAngularMomentum"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SpinAngularMomentum"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpinAngularMomentum"/>, which is scaled by <paramref name="x"/>.</param>
    public static SpinAngularMomentum operator *(double x, SpinAngularMomentum y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SpinAngularMomentum"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpinAngularMomentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SpinAngularMomentum"/> <paramref name="x"/>.</param>
    public static SpinAngularMomentum operator /(SpinAngularMomentum x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="SpinAngularMomentum"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public SpinAngularMomentum Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="SpinAngularMomentum"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SpinAngularMomentum"/> is scaled.</param>
    public SpinAngularMomentum Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="SpinAngularMomentum"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SpinAngularMomentum"/> is divided.</param>
    public SpinAngularMomentum Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpinAngularMomentum"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="SpinAngularMomentum"/> <paramref name="x"/> by this value.</param>
    public static SpinAngularMomentum operator %(SpinAngularMomentum x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SpinAngularMomentum"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpinAngularMomentum"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SpinAngularMomentum"/> <paramref name="x"/>.</param>
    public static SpinAngularMomentum operator *(SpinAngularMomentum x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SpinAngularMomentum"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SpinAngularMomentum"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpinAngularMomentum"/>, which is scaled by <paramref name="x"/>.</param>
    public static SpinAngularMomentum operator *(Scalar x, SpinAngularMomentum y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SpinAngularMomentum"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpinAngularMomentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SpinAngularMomentum"/> <paramref name="x"/>.</param>
    public static SpinAngularMomentum operator /(SpinAngularMomentum x, Scalar y) => x.Divide(y);

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

    /// <summary>Multiplication of the <see cref="SpinAngularMomentum"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpinAngularMomentum"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="SpinAngularMomentum"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(SpinAngularMomentum x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="SpinAngularMomentum"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpinAngularMomentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="SpinAngularMomentum"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(SpinAngularMomentum x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Multiplicates the <see cref="SpinAngularMomentum"/> with the <see cref="Vector3"/> <paramref name="factor"/> to produce
    /// a <see cref="SpinAngularMomentum3"/>.</summary>
    /// <param name="factor">This <see cref="Vector3"/> is multiplied by the <see cref="SpinAngularMomentum"/>.</param>
    public SpinAngularMomentum3 Multiply(Vector3 factor) => new(factor * Magnitude);
    /// <summary>Multiplicates the <see cref="SpinAngularMomentum"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="SpinAngularMomentum3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="SpinAngularMomentum"/>.</param>
    public SpinAngularMomentum3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplicates the <see cref="SpinAngularMomentum"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="SpinAngularMomentum3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="SpinAngularMomentum"/>.</param>
    public SpinAngularMomentum3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplication of the <see cref="SpinAngularMomentum"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/>
    /// to produce a <see cref="SpinAngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="SpinAngularMomentum"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="SpinAngularMomentum"/> <paramref name="a"/>.</param>
    public static SpinAngularMomentum3 operator *(SpinAngularMomentum a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="SpinAngularMomentum"/> <paramref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/>
    /// to produce a <see cref="SpinAngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="SpinAngularMomentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="SpinAngularMomentum"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static SpinAngularMomentum3 operator *(Vector3 a, SpinAngularMomentum b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="SpinAngularMomentum"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="SpinAngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="SpinAngularMomentum"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="SpinAngularMomentum"/> <paramref name="a"/>.</param>
    public static SpinAngularMomentum3 operator *(SpinAngularMomentum a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="SpinAngularMomentum"/> <paramref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="SpinAngularMomentum3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="SpinAngularMomentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="SpinAngularMomentum"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static SpinAngularMomentum3 operator *((double x, double y, double z) a, SpinAngularMomentum b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="SpinAngularMomentum"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="SpinAngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="SpinAngularMomentum"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="SpinAngularMomentum"/> <paramref name="a"/>.</param>
    public static SpinAngularMomentum3 operator *(SpinAngularMomentum a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="SpinAngularMomentum"/> <paramref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="SpinAngularMomentum3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="SpinAngularMomentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="SpinAngularMomentum"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static SpinAngularMomentum3 operator *((Scalar x, Scalar y, Scalar z) a, SpinAngularMomentum b) => b.Multiply(a);

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SpinAngularMomentum"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="SpinAngularMomentum"/>.</param>
    public static bool operator <(SpinAngularMomentum x, SpinAngularMomentum y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SpinAngularMomentum"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="SpinAngularMomentum"/>.</param>
    public static bool operator >(SpinAngularMomentum x, SpinAngularMomentum y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SpinAngularMomentum"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="SpinAngularMomentum"/>.</param>
    public static bool operator <=(SpinAngularMomentum x, SpinAngularMomentum y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SpinAngularMomentum"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="SpinAngularMomentum"/>.</param>
    public static bool operator >=(SpinAngularMomentum x, SpinAngularMomentum y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="SpinAngularMomentum"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(SpinAngularMomentum x) => x.ToDouble();

    /// <summary>Converts the <see cref="SpinAngularMomentum"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(SpinAngularMomentum x) => x.ToScalar();

    /// <summary>Constructs the <see cref="SpinAngularMomentum"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static SpinAngularMomentum FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="SpinAngularMomentum"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator SpinAngularMomentum(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="SpinAngularMomentum"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static SpinAngularMomentum FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="SpinAngularMomentum"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator SpinAngularMomentum(Scalar x) => FromScalar(x);
}
