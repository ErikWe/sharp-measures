#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Density"/>, describing the amount of <see cref="Mass"/> over a <see cref="Volume"/>.
/// The quantity is expressed in <see cref="UnitOfDensity"/>, with the SI unit being [kg∙m⁻³].
/// <para>
/// New instances of <see cref="Density"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfDensity"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code><see cref="Density"/> a = 3 * <see cref="Density.OneKilogramPerCubicMetre"/>;</code>
/// </item>
/// <item>
/// <code><see cref="Density"/> d = <see cref="Density.From(Mass, Volume)"/>;</code>
/// </item>
/// </list>
/// The magnitude of the <see cref="Density"/> can be retrieved in the desired <see cref="UnitOfDensity"/> using pre-defined properties,
/// such as <see cref="KilogramsPerCubicMetre"/>.
/// </para>
/// </summary>
public readonly partial record struct Density :
    IComparable<Density>,
    IScalarQuantity,
    IScalableScalarQuantity<Density>,
    IInvertibleScalarQuantity<SpecificVolume>,
    IMultiplicableScalarQuantity<Density, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Density, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="Density"/>.</summary>
    public static Density Zero { get; } = new(0);

    /// <summary>The <see cref="Density"/> of magnitude 1, when expressed in <see cref="UnitOfDensity.KilogramPerCubicMetre"/>.</summary>
    public static Density OneKilogramPerCubicMetre { get; } = UnitOfDensity.KilogramPerCubicMetre.Density;

    /// <summary>Computes <see cref="Density"/> according to { 1 / <paramref name="specificVolume"/> }.</summary>
    /// <summary>Constructs a <see cref="Density"/> by inverting the <see cref="SpecificVolume"/> <paramref name="specificVolume"/>.</summary>
    public static Density From(SpecificVolume specificVolume) => new(1 / specificVolume.Magnitude);

    /// <summary>The magnitude of the <see cref="Density"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfDensity)"/> or a pre-defined property
    /// - such as <see cref="KilogramsPerCubicMetre"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Density"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfDensity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Density"/>, expressed in <paramref name="unitOfDensity"/>.</param>
    /// <param name="unitOfDensity">The <see cref="UnitOfDensity"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Density"/> a = 3 * <see cref="Density.OneKilogramPerCubicMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Density(Scalar magnitude, UnitOfDensity unitOfDensity) : this(magnitude.Magnitude, unitOfDensity) { }
    /// <summary>Constructs a new <see cref="Density"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfDensity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Density"/>, expressed in <paramref name="unitOfDensity"/>.</param>
    /// <param name="unitOfDensity">The <see cref="UnitOfDensity"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Density"/> a = 3 * <see cref="Density.OneKilogramPerCubicMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Density(double magnitude, UnitOfDensity unitOfDensity) : this(magnitude * unitOfDensity.Density.Magnitude) { }
    /// <summary>Constructs a new <see cref="Density"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Density"/>.</param>
    /// <remarks>Consider preferring <see cref="Density(Scalar, UnitOfDensity)"/>.</remarks>
    public Density(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Density"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Density"/>.</param>
    /// <remarks>Consider preferring <see cref="Density(double, UnitOfDensity)"/>.</remarks>
    public Density(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Density"/>, expressed in <see cref="UnitOfDensity.KilogramPerCubicMetre"/>.</summary>
    public Scalar KilogramsPerCubicMetre => InUnit(UnitOfDensity.KilogramPerCubicMetre);

    /// <summary>Indicates whether the magnitude of the <see cref="Density"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Density"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Density"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Density"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Density"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Density"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Density"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Density"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="Density"/>.</summary>
    public Density Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="Density"/>.</summary>
    public Density Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="Density"/>.</summary>
    public Density Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="Density"/> to the nearest integer value.</summary>
    public Density Round() => new(Math.Round(Magnitude));

    /// <summary>Computes the inverse of the <see cref="Density"/>, producing a <see cref="SpecificVolume"/>.</summary>
    public SpecificVolume Invert() => SpecificVolume.From(this);

    /// <inheritdoc/>
    public int CompareTo(Density other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Density"/> in the default unit
    /// <see cref="UnitOfDensity.KilogramPerCubicMetre"/>, followed by the symbol [kg∙m⁻³].</summary>
    public override string ToString() => $"{KilogramsPerCubicMetre} [kg∙m⁻³]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Density"/>,
    /// expressed in <paramref name="unitOfDensity"/>.</summary>
    /// <param name="unitOfDensity">The <see cref="UnitOfDensity"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfDensity unitOfDensity) => InUnit(this, unitOfDensity);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Density"/>,
    /// expressed in <paramref name="unitOfDensity"/>.</summary>
    /// <param name="density">The <see cref="Density"/> to be expressed in <paramref name="unitOfDensity"/>.</param>
    /// <param name="unitOfDensity">The <see cref="UnitOfDensity"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Density density, UnitOfDensity unitOfDensity) => new(density.Magnitude / unitOfDensity.Density.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Density"/>.</summary>
    public Density Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Density"/> with negated magnitude.</summary>
    public Density Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Density"/>.</param>
    public static Density operator +(Density x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Density"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Density"/>.</param>
    public static Density operator -(Density x) => x.Negate();

    /// <summary>Multiplicates the <see cref="Density"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Density"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Density"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Density"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Density"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Density"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Density"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Density x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Density"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Density"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Density"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Density y) => y.Multiply(x);
    /// <summary>Division of the <see cref="Density"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Density"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Density"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Density x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="Density"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="Density"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Density"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, Density y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="Density"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Density Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Density"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Density"/> is scaled.</param>
    public Density Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Density"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Density"/> is divided.</param>
    public Density Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Density"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Density"/> <paramref name="x"/> by this value.</param>
    public static Density operator %(Density x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Density"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Density"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Density"/> <paramref name="x"/>.</param>
    public static Density operator *(Density x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Density"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Density"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Density"/>, which is scaled by <paramref name="x"/>.</param>
    public static Density operator *(double x, Density y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Density"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Density"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Density"/> <paramref name="x"/>.</param>
    public static Density operator /(Density x, double y) => x.Divide(y);
    /// <summary>Inverts the <see cref="Density"/> <paramref name="y"/> to produce a <see cref="SpecificVolume"/>, which is then scaled by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="Density"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Density"/>, which is inverted to a <see cref="SpecificVolume"/> and scaled by <paramref name="x"/>.</param>
    public static SpecificVolume operator /(double x, Density y) => new(x / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="Density"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Density Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Density"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Density"/> is scaled.</param>
    public Density Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Density"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Density"/> is divided.</param>
    public Density Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Density"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Density"/> <paramref name="x"/> by this value.</param>
    public static Density operator %(Density x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Density"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Density"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Density"/> <paramref name="x"/>.</param>
    public static Density operator *(Density x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Density"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Density"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Density"/>, which is scaled by <paramref name="x"/>.</param>
    public static Density operator *(Scalar x, Density y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Density"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Density"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Density"/> <paramref name="x"/>.</param>
    public static Density operator /(Density x, Scalar y) => x.Divide(y);
    /// <summary>Inverts the <see cref="Density"/> <paramref name="y"/> to produce a <see cref="SpecificVolume"/>,
    /// which is then scaled by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="Density"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Density"/>, which is inverted to a <see cref="SpecificVolume"/> and scaled by <paramref name="x"/>.</param>
    public static SpecificVolume operator /(Scalar x, Density y) => new(x / y.Magnitude);

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

    /// <summary>Multiplication of the <see cref="Density"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Density"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Density"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Density x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="Density"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Density"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Density"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Density x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Density"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Density"/>.</param>
    public static bool operator <(Density x, Density y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Density"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Density"/>.</param>
    public static bool operator >(Density x, Density y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Density"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Density"/>.</param>
    public static bool operator <=(Density x, Density y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Density"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Density"/>.</param>
    public static bool operator >=(Density x, Density y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Density"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(Density x) => x.ToDouble();

    /// <summary>Converts the <see cref="Density"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Density x) => x.ToScalar();

    /// <summary>Constructs the <see cref="Density"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Density FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Density"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Density(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="Density"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static Density FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="Density"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator Density(Scalar x) => FromScalar(x);
}
