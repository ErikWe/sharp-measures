namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="SpecificVolume"/>, describing the <see cref="Volume"/> required for some amount of <see cref="Mass"/>.
/// This is the inverse of <see cref="Density"/>. The quantity is expressed in <see cref="UnitOfSpecificVolume"/>, with the SI unit being [m³ / kg].
/// <para>
/// New instances of <see cref="SpecificVolume"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfSpecificVolume"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="SpecificVolume"/> a = 3 * <see cref="SpecificVolume.OneCubicMetrePerKilogram"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="SpecificVolume"/> d = <see cref="SpecificVolume.From(Volume, Mass)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfSpecificVolume"/>.
/// </para>
/// </summary>
public readonly partial record struct SpecificVolume :
    IComparable<SpecificVolume>,
    IScalarQuantity,
    IScalableScalarQuantity<SpecificVolume>,
    IInvertibleScalarQuantity<Density>,
    IMultiplicableScalarQuantity<SpecificVolume, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<SpecificVolume, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="SpecificVolume"/>.</summary>
    public static SpecificVolume Zero { get; } = new(0);

    /// <summary>The <see cref="SpecificVolume"/> with magnitude 1, when expressed in unit <see cref="UnitOfSpecificVolume.CubicMetrePerKilogram"/>.</summary>
    public static SpecificVolume OneCubicMetrePerKilogram { get; } = new(1, UnitOfSpecificVolume.CubicMetrePerKilogram);

    /// <summary>Computes <see cref="SpecificVolume"/> according to { <see cref="SpecificVolume"/> = 1 / <paramref name="density"/> }.</summary>
    /// <summary>Constructs a <see cref="SpecificVolume"/> by inverting the <see cref="Density"/> <paramref name="density"/>.</summary>
    public static SpecificVolume From(Density density) => new(1 / density.Magnitude);

    /// <summary>The magnitude of the <see cref="SpecificVolume"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="SpecificVolume.InCubicMetresPerKilogram"/>.
    /// <para>This value should only be used (to maximize efficiency) when implementing mathematical operations with other quantities.</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="SpecificVolume"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfSpecificVolume"/> <paramref name="unitOfSpecificVolume"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpecificVolume"/>, in <see cref="UnitOfSpecificVolume"/> <paramref name="unitOfSpecificVolume"/>.</param>
    /// <param name="unitOfSpecificVolume">The <see cref="UnitOfSpecificVolume"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SpecificVolume"/> a = 3 * <see cref="SpecificVolume.OneCubicMetrePerKilogram"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SpecificVolume(Scalar magnitude, UnitOfSpecificVolume unitOfSpecificVolume) : this(magnitude.Magnitude, unitOfSpecificVolume) { }
    /// <summary>Constructs a new <see cref="SpecificVolume"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfSpecificVolume"/> <paramref name="unitOfSpecificVolume"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpecificVolume"/>, in <see cref="UnitOfSpecificVolume"/> <paramref name="unitOfSpecificVolume"/>.</param>
    /// <param name="unitOfSpecificVolume">The <see cref="UnitOfSpecificVolume"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SpecificVolume"/> a = 3 * <see cref="SpecificVolume.OneCubicMetrePerKilogram"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SpecificVolume(double magnitude, UnitOfSpecificVolume unitOfSpecificVolume) : this(magnitude * unitOfSpecificVolume.Factor) { }
    /// <summary>Constructs a new <see cref="SpecificVolume"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpecificVolume"/>.</param>
    /// <remarks>Consider preffering a constructor that requires a <see cref="UnitOfSpecificVolume"/> to be specified.</remarks>
    public SpecificVolume(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="SpecificVolume"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpecificVolume"/>.</param>
    /// <remarks>Consider preferring a constructor that requires a <see cref="UnitOfSpecificVolume"/> to be specified.</remarks>
    public SpecificVolume(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="SpecificVolume"/>, expressed in unit <see cref="UnitOfSpecificVolume.CubicMetrePerKilogram"/>.</summary>
    public Scalar InCubicMetresPerKilogram => InUnit(UnitOfSpecificVolume.CubicMetrePerKilogram);

    /// <summary>Indicates whether the magnitude of the <see cref="SpecificVolume"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpecificVolume"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="SpecificVolume"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="SpecificVolume"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpecificVolume"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpecificVolume"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="SpecificVolume"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpecificVolume"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="SpecificVolume"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public SpecificVolume Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="SpecificVolume"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public SpecificVolume Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="SpecificVolume"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public SpecificVolume Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="SpecificVolume"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public SpecificVolume Round() => new(Math.Round(Magnitude));

    /// <summary>Inverts the <see cref="SpecificVolume"/>, producing a <see cref="Density"/>.</summary>
    public Density Invert() => Density.From(this);

    /// <inheritdoc/>
    public int CompareTo(SpecificVolume other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="SpecificVolume"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [m^3 / kg]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="SpecificVolume"/>, expressed in <see cref="UnitOfSpecificVolume"/>
    /// <paramref name="unitOfSpecificVolume"/>.</summary>
    /// <param name="unitOfSpecificVolume">The <see cref="UnitOfSpecificVolume"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfSpecificVolume unitOfSpecificVolume) => InUnit(this, unitOfSpecificVolume);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="SpecificVolume"/>, expressed in <see cref="UnitOfSpecificVolume"/>
    /// <paramref name="unitOfSpecificVolume"/>.</summary>
    /// <param name="specificVolume">The <see cref="SpecificVolume"/> to be expressed in <see cref="UnitOfSpecificVolume"/> <paramref name="unitOfSpecificVolume"/>.</param>
    /// <param name="unitOfSpecificVolume">The <see cref="UnitOfSpecificVolume"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(SpecificVolume specificVolume, UnitOfSpecificVolume unitOfSpecificVolume) => new(specificVolume.Magnitude / unitOfSpecificVolume.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="SpecificVolume"/>.</summary>
    public SpecificVolume Plus() => this;
    /// <summary>Negation, resulting in a <see cref="SpecificVolume"/> with negated magnitude.</summary>
    public SpecificVolume Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="SpecificVolume"/>.</param>
    public static SpecificVolume operator +(SpecificVolume x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="SpecificVolume"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="SpecificVolume"/>.</param>
    public static SpecificVolume operator -(SpecificVolume x) => x.Negate();

    /// <summary>Multiplies the <see cref="SpecificVolume"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="SpecificVolume"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="SpecificVolume"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="SpecificVolume"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="SpecificVolume"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpecificVolume"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SpecificVolume"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(SpecificVolume x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="SpecificVolume"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="SpecificVolume"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="SpecificVolume"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, SpecificVolume y) => y.Multiply(x);
    /// <summary>Divides the <see cref="SpecificVolume"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpecificVolume"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SpecificVolume"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(SpecificVolume x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="SpecificVolume"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public SpecificVolume Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="SpecificVolume"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SpecificVolume"/> is scaled.</param>
    public SpecificVolume Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="SpecificVolume"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SpecificVolume"/> is divided.</param>
    public SpecificVolume Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="SpecificVolume"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpecificVolume"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="SpecificVolume"/> <paramref name="x"/> by this value.</param>
    public static SpecificVolume operator %(SpecificVolume x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SpecificVolume"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpecificVolume"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SpecificVolume"/> <paramref name="x"/>.</param>
    public static SpecificVolume operator *(SpecificVolume x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SpecificVolume"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SpecificVolume"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpecificVolume"/>, which is scaled by <paramref name="x"/>.</param>
    public static SpecificVolume operator *(double x, SpecificVolume y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SpecificVolume"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpecificVolume"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SpecificVolume"/> <paramref name="x"/>.</param>
    public static SpecificVolume operator /(SpecificVolume x, double y) => x.Divide(y);
/// <summary>Inverts the <see cref="SpecificVolume"/> <paramref name="y"/> to produce a <see cref="Density"/>, which is then scaled by <paramref name="x"/>.</summary>
/// <param name="x">This value is used to scale the inverted <see cref="SpecificVolume"/> <paramref name="y"/>.</param>
/// <param name="y">The <see cref="SpecificVolume"/>, which is inverted to a <see cref="Density"/> and scaled by <paramref name="x"/>.</param>
    public static Density operator /(double x, SpecificVolume y) => x * y.Invert();

    /// <summary>Produces a <see cref="SpecificVolume"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public SpecificVolume Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="SpecificVolume"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SpecificVolume"/> is scaled.</param>
    public SpecificVolume Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="SpecificVolume"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SpecificVolume"/> is divided.</param>
    public SpecificVolume Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="SpecificVolume"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpecificVolume"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="SpecificVolume"/> <paramref name="x"/> by this value.</param>
    public static SpecificVolume operator %(SpecificVolume x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SpecificVolume"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpecificVolume"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SpecificVolume"/> <paramref name="x"/>.</param>
    public static SpecificVolume operator *(SpecificVolume x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SpecificVolume"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SpecificVolume"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpecificVolume"/>, which is scaled by <paramref name="x"/>.</param>
    public static SpecificVolume operator *(Scalar x, SpecificVolume y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SpecificVolume"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpecificVolume"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SpecificVolume"/> <paramref name="x"/>.</param>
    public static SpecificVolume operator /(SpecificVolume x, Scalar y) => x.Divide(y);
/// <summary>Inverts the <see cref="SpecificVolume"/> <paramref name="y"/> to produce a <see cref="Density"/>, which is then scaled by <paramref name="x"/>.</summary>
/// <param name="x">This value is used to scale the inverted <see cref="SpecificVolume"/> <paramref name="y"/>.</param>
/// <param name="y">The <see cref="SpecificVolume"/>, which is inverted to a <see cref="Density"/> and scaled by <paramref name="x"/>.</param>
    public static Density operator /(Scalar x, SpecificVolume y) => x * y.Invert();

    /// <summary>Multiplies the <see cref="SpecificVolume"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="SpecificVolume"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="SpecificVolume"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="SpecificVolume"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="SpecificVolume"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpecificVolume"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="SpecificVolume"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="SpecificVolume.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(SpecificVolume x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="SpecificVolume"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpecificVolume"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="SpecificVolume"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="SpecificVolume.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(SpecificVolume x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(SpecificVolume x, SpecificVolume y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(SpecificVolume x, SpecificVolume y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(SpecificVolume x, SpecificVolume y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(SpecificVolume x, SpecificVolume y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="SpecificVolume"/> to a <see cref="double"/> with value <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts the <see cref="SpecificVolume"/> to a <see cref="double"/> based on the magnitude of the <see cref="SpecificVolume"/> <paramref name="x"/>.</summary>
    public static implicit operator double(SpecificVolume x) => x.ToDouble();

    /// <summary>Converts the <see cref="SpecificVolume"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts the <see cref="SpecificVolume"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(SpecificVolume x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="SpecificVolume"/> of magnitude <paramref name="x"/>.</summary>
    public static SpecificVolume FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="SpecificVolume"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator SpecificVolume(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="SpecificVolume"/> of equivalent magnitude.</summary>
    public static SpecificVolume FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="SpecificVolume"/> of equivalent magnitude.</summary>
    public static explicit operator SpecificVolume(Scalar x) => FromScalar(x);
}
