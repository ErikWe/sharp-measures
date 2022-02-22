#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="VolumetricFlowRate"/>, describing the amount of <see cref="Volume"/> that flows through some
/// point over some <see cref="Time"/>. The quantity is expressed in <see cref="UnitOfVolumetricFlowRate"/>, with the SI unit being [m³∙s⁻¹].
/// <para>
/// New instances of <see cref="VolumetricFlowRate"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfVolumetricFlowRate"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="VolumetricFlowRate"/> a = 3 * <see cref="VolumetricFlowRate.OneCubicMetrePerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="VolumetricFlowRate"/> d = <see cref="VolumetricFlowRate.From(Volume, Time)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="VolumetricFlowRate"/> can be retrieved in the desired <see cref="UnitOfVolumetricFlowRate"/> using pre-defined properties,
/// such as <see cref="CubicMetresPerSecond"/>.
/// </para>
/// </summary>
public readonly partial record struct VolumetricFlowRate :
    IComparable<VolumetricFlowRate>,
    IScalarQuantity,
    IScalableScalarQuantity<VolumetricFlowRate>,
    IMultiplicableScalarQuantity<VolumetricFlowRate, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<VolumetricFlowRate, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="VolumetricFlowRate"/>.</summary>
    public static VolumetricFlowRate Zero { get; } = new(0);

    /// <summary>The <see cref="VolumetricFlowRate"/> with magnitude 1, when expressed in unit <see cref="UnitOfVolumetricFlowRate.CubicMetrePerSecond"/>.</summary>
    public static VolumetricFlowRate OneCubicMetrePerSecond { get; } = UnitOfVolumetricFlowRate.CubicMetrePerSecond.VolumetricFlowRate;
    /// <summary>The <see cref="VolumetricFlowRate"/> with magnitude 1, when expressed in unit <see cref="UnitOfVolumetricFlowRate.LitrePerSecond"/>.</summary>
    public static VolumetricFlowRate OneLitrePerSecond { get; } = UnitOfVolumetricFlowRate.LitrePerSecond.VolumetricFlowRate;

    /// <summary>The magnitude of the <see cref="VolumetricFlowRate"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfVolumetricFlowRate)"/> or a pre-defined property
    /// - such as <see cref="CubicMetresPerSecond"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="VolumetricFlowRate"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfVolumetricFlowRate"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="VolumetricFlowRate"/>, expressed in <paramref name="unitOfVolumetricFlowRate"/>.</param>
    /// <param name="unitOfVolumetricFlowRate">The <see cref="UnitOfVolumetricFlowRate"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="VolumetricFlowRate"/> a = 3 * <see cref="VolumetricFlowRate.OneCubicMetrePerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public VolumetricFlowRate(Scalar magnitude, UnitOfVolumetricFlowRate unitOfVolumetricFlowRate) : this(magnitude.Magnitude, unitOfVolumetricFlowRate) { }
    /// <summary>Constructs a new <see cref="VolumetricFlowRate"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfVolumetricFlowRate"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="VolumetricFlowRate"/>, expressed in <paramref name="unitOfVolumetricFlowRate"/>.</param>
    /// <param name="unitOfVolumetricFlowRate">The <see cref="UnitOfVolumetricFlowRate"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="VolumetricFlowRate"/> a = 3 * <see cref="VolumetricFlowRate.OneCubicMetrePerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public VolumetricFlowRate(double magnitude, UnitOfVolumetricFlowRate unitOfVolumetricFlowRate) : 
    	this(magnitude * unitOfVolumetricFlowRate.VolumetricFlowRate.Magnitude) { }
    /// <summary>Constructs a new <see cref="VolumetricFlowRate"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="VolumetricFlowRate"/>.</param>
    /// <remarks>Consider preferring <see cref="VolumetricFlowRate(Scalar, UnitOfVolumetricFlowRate)"/>.</remarks>
    public VolumetricFlowRate(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="VolumetricFlowRate"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="VolumetricFlowRate"/>.</param>
    /// <remarks>Consider preferring <see cref="VolumetricFlowRate(double, UnitOfVolumetricFlowRate)"/>.</remarks>
    public VolumetricFlowRate(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="VolumetricFlowRate"/>, expressed in <see cref="UnitOfVolumetricFlowRate.CubicMetrePerSecond"/>.</summary>
    public Scalar CubicMetresPerSecond => InUnit(UnitOfVolumetricFlowRate.CubicMetrePerSecond);
    /// <summary>Retrieves the magnitude of the <see cref="VolumetricFlowRate"/>, expressed in <see cref="UnitOfVolumetricFlowRate.LitrePerSecond"/>.</summary>
    public Scalar LitresPerSecond => InUnit(UnitOfVolumetricFlowRate.LitrePerSecond);

    /// <summary>Indicates whether the magnitude of the <see cref="VolumetricFlowRate"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="VolumetricFlowRate"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="VolumetricFlowRate"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="VolumetricFlowRate"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="VolumetricFlowRate"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="VolumetricFlowRate"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="VolumetricFlowRate"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="VolumetricFlowRate"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="VolumetricFlowRate"/>.</summary>
    public VolumetricFlowRate Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="VolumetricFlowRate"/>.</summary>
    public VolumetricFlowRate Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="VolumetricFlowRate"/>.</summary>
    public VolumetricFlowRate Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="VolumetricFlowRate"/> to the nearest integer value.</summary>
    public VolumetricFlowRate Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(VolumetricFlowRate other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="VolumetricFlowRate"/> in the default unit
    /// <see cref="UnitOfVolumetricFlowRate.CubicMetrePerSecond"/>, followed by the symbol [m³∙s⁻¹].</summary>
    public override string ToString() => $"{CubicMetresPerSecond} [m³∙s⁻¹]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="VolumetricFlowRate"/>,
    /// expressed in <paramref name="unitOfVolumetricFlowRate"/>.</summary>
    /// <param name="unitOfVolumetricFlowRate">The <see cref="UnitOfVolumetricFlowRate"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfVolumetricFlowRate unitOfVolumetricFlowRate) => InUnit(this, unitOfVolumetricFlowRate);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="VolumetricFlowRate"/>,
    /// expressed in <paramref name="unitOfVolumetricFlowRate"/>.</summary>
    /// <param name="volumetricFlowRate">The <see cref="VolumetricFlowRate"/> to be expressed in <paramref name="unitOfVolumetricFlowRate"/>.</param>
    /// <param name="unitOfVolumetricFlowRate">The <see cref="UnitOfVolumetricFlowRate"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(VolumetricFlowRate volumetricFlowRate, UnitOfVolumetricFlowRate unitOfVolumetricFlowRate) 
    	=> new(volumetricFlowRate.Magnitude / unitOfVolumetricFlowRate.VolumetricFlowRate.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="VolumetricFlowRate"/>.</summary>
    public VolumetricFlowRate Plus() => this;
    /// <summary>Negation, resulting in a <see cref="VolumetricFlowRate"/> with negated magnitude.</summary>
    public VolumetricFlowRate Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="VolumetricFlowRate"/>.</param>
    public static VolumetricFlowRate operator +(VolumetricFlowRate x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="VolumetricFlowRate"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="VolumetricFlowRate"/>.</param>
    public static VolumetricFlowRate operator -(VolumetricFlowRate x) => x.Negate();

    /// <summary>Multiplicates the <see cref="VolumetricFlowRate"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="VolumetricFlowRate"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="VolumetricFlowRate"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="VolumetricFlowRate"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="VolumetricFlowRate"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="VolumetricFlowRate"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(VolumetricFlowRate x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="VolumetricFlowRate"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="VolumetricFlowRate"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="VolumetricFlowRate"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, VolumetricFlowRate y) => y.Multiply(x);
    /// <summary>Division of the <see cref="VolumetricFlowRate"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="VolumetricFlowRate"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(VolumetricFlowRate x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="VolumetricFlowRate"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="VolumetricFlowRate"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="VolumetricFlowRate"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, VolumetricFlowRate y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="VolumetricFlowRate"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public VolumetricFlowRate Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="VolumetricFlowRate"/> is scaled.</param>
    public VolumetricFlowRate Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="VolumetricFlowRate"/> is divided.</param>
    public VolumetricFlowRate Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="VolumetricFlowRate"/> <paramref name="x"/> by this value.</param>
    public static VolumetricFlowRate operator %(VolumetricFlowRate x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="VolumetricFlowRate"/> <paramref name="x"/>.</param>
    public static VolumetricFlowRate operator *(VolumetricFlowRate x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="VolumetricFlowRate"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="VolumetricFlowRate"/>, which is scaled by <paramref name="x"/>.</param>
    public static VolumetricFlowRate operator *(double x, VolumetricFlowRate y) => y.Multiply(x);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="VolumetricFlowRate"/> <paramref name="x"/>.</param>
    public static VolumetricFlowRate operator /(VolumetricFlowRate x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="VolumetricFlowRate"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public VolumetricFlowRate Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="VolumetricFlowRate"/> is scaled.</param>
    public VolumetricFlowRate Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="VolumetricFlowRate"/> is divided.</param>
    public VolumetricFlowRate Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="VolumetricFlowRate"/> <paramref name="x"/> by this value.</param>
    public static VolumetricFlowRate operator %(VolumetricFlowRate x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="VolumetricFlowRate"/> <paramref name="x"/>.</param>
    public static VolumetricFlowRate operator *(VolumetricFlowRate x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="VolumetricFlowRate"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="VolumetricFlowRate"/>, which is scaled by <paramref name="x"/>.</param>
    public static VolumetricFlowRate operator *(Scalar x, VolumetricFlowRate y) => y.Multiply(x);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="VolumetricFlowRate"/> <paramref name="x"/>.</param>
    public static VolumetricFlowRate operator /(VolumetricFlowRate x, Scalar y) => x.Divide(y);

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

    /// <summary>Multiplication of the <see cref="VolumetricFlowRate"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="VolumetricFlowRate"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(VolumetricFlowRate x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="VolumetricFlowRate"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="VolumetricFlowRate"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(VolumetricFlowRate x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="VolumetricFlowRate"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="VolumetricFlowRate"/>.</param>
    public static bool operator <(VolumetricFlowRate x, VolumetricFlowRate y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="VolumetricFlowRate"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="VolumetricFlowRate"/>.</param>
    public static bool operator >(VolumetricFlowRate x, VolumetricFlowRate y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="VolumetricFlowRate"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="VolumetricFlowRate"/>.</param>
    public static bool operator <=(VolumetricFlowRate x, VolumetricFlowRate y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="VolumetricFlowRate"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="VolumetricFlowRate"/>.</param>
    public static bool operator >=(VolumetricFlowRate x, VolumetricFlowRate y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="VolumetricFlowRate"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(VolumetricFlowRate x) => x.ToDouble();

    /// <summary>Converts the <see cref="VolumetricFlowRate"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(VolumetricFlowRate x) => x.ToScalar();

    /// <summary>Constructs the <see cref="VolumetricFlowRate"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static VolumetricFlowRate FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="VolumetricFlowRate"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator VolumetricFlowRate(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="VolumetricFlowRate"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static VolumetricFlowRate FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="VolumetricFlowRate"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator VolumetricFlowRate(Scalar x) => FromScalar(x);
}
