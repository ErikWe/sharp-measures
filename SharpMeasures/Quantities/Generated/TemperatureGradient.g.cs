#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="TemperatureGradient"/>, describing a <see cref="TemperatureDifference"/> over some <see cref="Distance"/>.
/// The quantity is expressed in <see cref="UnitOfTemperatureGradient"/>, with the SI unit being [K∙m⁻¹].
/// <para>
/// New instances of <see cref="TemperatureGradient"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfTemperatureGradient"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="TemperatureGradient"/> a = 3 * <see cref="TemperatureGradient.OneKelvinPerMetre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="TemperatureGradient"/> d = <see cref="TemperatureGradient.From(TemperatureDifference, Distance)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="TemperatureGradient"/> can be retrieved in the desired <see cref="UnitOfTemperatureGradient"/> using pre-defined properties,
/// such as <see cref="KelvinPerMetre"/>.
/// </para>
/// </summary>
public readonly partial record struct TemperatureGradient :
    IComparable<TemperatureGradient>,
    IScalarQuantity,
    IScalableScalarQuantity<TemperatureGradient>,
    IMultiplicableScalarQuantity<TemperatureGradient, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<TemperatureGradient, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="TemperatureGradient"/>.</summary>
    public static TemperatureGradient Zero { get; } = new(0);

    /// <summary>The <see cref="TemperatureGradient"/> with magnitude 1, when expressed in unit <see cref="UnitOfTemperatureGradient.KelvinPerMetre"/>.</summary>
    public static TemperatureGradient OneKelvinPerMetre { get; } = UnitOfTemperatureGradient.KelvinPerMetre.TemperatureGradient;
    /// <summary>The <see cref="TemperatureGradient"/> with magnitude 1, when expressed in unit <see cref="UnitOfTemperatureGradient.CelsiusPerMetre"/>.</summary>
    public static TemperatureGradient OneCelsiusPerMetre { get; } = UnitOfTemperatureGradient.CelsiusPerMetre.TemperatureGradient;
    /// <summary>The <see cref="TemperatureGradient"/> with magnitude 1, when expressed in unit <see cref="UnitOfTemperatureGradient.RankinePerMetre"/>.</summary>
    public static TemperatureGradient OneRankinePerMetre { get; } = UnitOfTemperatureGradient.RankinePerMetre.TemperatureGradient;
    /// <summary>The <see cref="TemperatureGradient"/> with magnitude 1, when expressed in unit <see cref="UnitOfTemperatureGradient.FahrenheitPerMetre"/>.</summary>
    public static TemperatureGradient OneFahrenheitPerMetre { get; } = UnitOfTemperatureGradient.FahrenheitPerMetre.TemperatureGradient;
    /// <summary>The <see cref="TemperatureGradient"/> with magnitude 1, when expressed in unit <see cref="UnitOfTemperatureGradient.FahrenheitPerFoot"/>.</summary>
    public static TemperatureGradient OneFahrenheitPerFoot { get; } = UnitOfTemperatureGradient.FahrenheitPerFoot.TemperatureGradient;

    /// <summary>The magnitude of the <see cref="TemperatureGradient"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfTemperatureGradient)"/> or a pre-defined property
    /// - such as <see cref="KelvinPerMetre"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="TemperatureGradient"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfTemperatureGradient"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TemperatureGradient"/>, expressed in <paramref name="unitOfTemperatureGradient"/>.</param>
    /// <param name="unitOfTemperatureGradient">The <see cref="UnitOfTemperatureGradient"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="TemperatureGradient"/> a = 3 * <see cref="TemperatureGradient.OneKelvinPerMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public TemperatureGradient(Scalar magnitude, UnitOfTemperatureGradient unitOfTemperatureGradient) : this(magnitude.Magnitude, unitOfTemperatureGradient) { }
    /// <summary>Constructs a new <see cref="TemperatureGradient"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfTemperatureGradient"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TemperatureGradient"/>, expressed in <paramref name="unitOfTemperatureGradient"/>.</param>
    /// <param name="unitOfTemperatureGradient">The <see cref="UnitOfTemperatureGradient"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="TemperatureGradient"/> a = 3 * <see cref="TemperatureGradient.OneKelvinPerMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public TemperatureGradient(double magnitude, UnitOfTemperatureGradient unitOfTemperatureGradient) : 
    	this(magnitude * unitOfTemperatureGradient.TemperatureGradient.Magnitude) { }
    /// <summary>Constructs a new <see cref="TemperatureGradient"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TemperatureGradient"/>.</param>
    /// <remarks>Consider preferring <see cref="TemperatureGradient(Scalar, UnitOfTemperatureGradient)"/>.</remarks>
    public TemperatureGradient(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="TemperatureGradient"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TemperatureGradient"/>.</param>
    /// <remarks>Consider preferring <see cref="TemperatureGradient(double, UnitOfTemperatureGradient)"/>.</remarks>
    public TemperatureGradient(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="TemperatureGradient"/>, expressed in <see cref="UnitOfTemperatureGradient.KelvinPerMetre"/>.</summary>
    public Scalar KelvinPerMetre => InUnit(UnitOfTemperatureGradient.KelvinPerMetre);
    /// <summary>Retrieves the magnitude of the <see cref="TemperatureGradient"/>, expressed in <see cref="UnitOfTemperatureGradient.CelsiusPerMetre"/>.</summary>
    public Scalar CelsiusPerMetre => InUnit(UnitOfTemperatureGradient.CelsiusPerMetre);
    /// <summary>Retrieves the magnitude of the <see cref="TemperatureGradient"/>, expressed in <see cref="UnitOfTemperatureGradient.RankinePerMetre"/>.</summary>
    public Scalar RankinePerMetre => InUnit(UnitOfTemperatureGradient.RankinePerMetre);
    /// <summary>Retrieves the magnitude of the <see cref="TemperatureGradient"/>, expressed in <see cref="UnitOfTemperatureGradient.FahrenheitPerMetre"/>.</summary>
    public Scalar FahrenheitPerMetre => InUnit(UnitOfTemperatureGradient.FahrenheitPerMetre);
    /// <summary>Retrieves the magnitude of the <see cref="TemperatureGradient"/>, expressed in <see cref="UnitOfTemperatureGradient.FahrenheitPerFoot"/>.</summary>
    public Scalar FahrenheitPerFoot => InUnit(UnitOfTemperatureGradient.FahrenheitPerFoot);

    /// <summary>Indicates whether the magnitude of the <see cref="TemperatureGradient"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="TemperatureGradient"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="TemperatureGradient"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="TemperatureGradient"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="TemperatureGradient"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="TemperatureGradient"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="TemperatureGradient"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="TemperatureGradient"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="TemperatureGradient"/>.</summary>
    public TemperatureGradient Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="TemperatureGradient"/>.</summary>
    public TemperatureGradient Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="TemperatureGradient"/>.</summary>
    public TemperatureGradient Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="TemperatureGradient"/> to the nearest integer value.</summary>
    public TemperatureGradient Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(TemperatureGradient other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="TemperatureGradient"/> in the default unit
    /// <see cref="UnitOfTemperatureGradient.KelvinPerMetre"/>, followed by the symbol [K∙m⁻¹].</summary>
    public override string ToString() => $"{KelvinPerMetre} [K∙m⁻¹]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="TemperatureGradient"/>,
    /// expressed in <paramref name="unitOfTemperatureGradient"/>.</summary>
    /// <param name="unitOfTemperatureGradient">The <see cref="UnitOfTemperatureGradient"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfTemperatureGradient unitOfTemperatureGradient) => InUnit(this, unitOfTemperatureGradient);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="TemperatureGradient"/>,
    /// expressed in <paramref name="unitOfTemperatureGradient"/>.</summary>
    /// <param name="temperatureGradient">The <see cref="TemperatureGradient"/> to be expressed in <paramref name="unitOfTemperatureGradient"/>.</param>
    /// <param name="unitOfTemperatureGradient">The <see cref="UnitOfTemperatureGradient"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(TemperatureGradient temperatureGradient, UnitOfTemperatureGradient unitOfTemperatureGradient) 
    	=> new(temperatureGradient.Magnitude / unitOfTemperatureGradient.TemperatureGradient.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="TemperatureGradient"/>.</summary>
    public TemperatureGradient Plus() => this;
    /// <summary>Negation, resulting in a <see cref="TemperatureGradient"/> with negated magnitude.</summary>
    public TemperatureGradient Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="TemperatureGradient"/>.</param>
    public static TemperatureGradient operator +(TemperatureGradient x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="TemperatureGradient"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="TemperatureGradient"/>.</param>
    public static TemperatureGradient operator -(TemperatureGradient x) => x.Negate();

    /// <summary>Multiplicates the <see cref="TemperatureGradient"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="TemperatureGradient"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="TemperatureGradient"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="TemperatureGradient"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="TemperatureGradient"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureGradient"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="TemperatureGradient"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(TemperatureGradient x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="TemperatureGradient"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="TemperatureGradient"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="TemperatureGradient"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, TemperatureGradient y) => y.Multiply(x);
    /// <summary>Division of the <see cref="TemperatureGradient"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureGradient"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="TemperatureGradient"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(TemperatureGradient x, Unhandled y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="TemperatureGradient"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public TemperatureGradient Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="TemperatureGradient"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="TemperatureGradient"/> is scaled.</param>
    public TemperatureGradient Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="TemperatureGradient"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="TemperatureGradient"/> is divided.</param>
    public TemperatureGradient Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureGradient"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="TemperatureGradient"/> <paramref name="x"/> by this value.</param>
    public static TemperatureGradient operator %(TemperatureGradient x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="TemperatureGradient"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureGradient"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="TemperatureGradient"/> <paramref name="x"/>.</param>
    public static TemperatureGradient operator *(TemperatureGradient x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="TemperatureGradient"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="TemperatureGradient"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TemperatureGradient"/>, which is scaled by <paramref name="x"/>.</param>
    public static TemperatureGradient operator *(double x, TemperatureGradient y) => y.Multiply(x);
    /// <summary>Scales the <see cref="TemperatureGradient"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureGradient"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="TemperatureGradient"/> <paramref name="x"/>.</param>
    public static TemperatureGradient operator /(TemperatureGradient x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="TemperatureGradient"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public TemperatureGradient Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="TemperatureGradient"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="TemperatureGradient"/> is scaled.</param>
    public TemperatureGradient Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="TemperatureGradient"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="TemperatureGradient"/> is divided.</param>
    public TemperatureGradient Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureGradient"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="TemperatureGradient"/> <paramref name="x"/> by this value.</param>
    public static TemperatureGradient operator %(TemperatureGradient x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="TemperatureGradient"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureGradient"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="TemperatureGradient"/> <paramref name="x"/>.</param>
    public static TemperatureGradient operator *(TemperatureGradient x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="TemperatureGradient"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="TemperatureGradient"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TemperatureGradient"/>, which is scaled by <paramref name="x"/>.</param>
    public static TemperatureGradient operator *(Scalar x, TemperatureGradient y) => y.Multiply(x);
    /// <summary>Scales the <see cref="TemperatureGradient"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureGradient"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="TemperatureGradient"/> <paramref name="x"/>.</param>
    public static TemperatureGradient operator /(TemperatureGradient x, Scalar y) => x.Divide(y);

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

    /// <summary>Multiplication of the <see cref="TemperatureGradient"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureGradient"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="TemperatureGradient"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(TemperatureGradient x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="TemperatureGradient"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureGradient"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="TemperatureGradient"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(TemperatureGradient x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="TemperatureGradient"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="TemperatureGradient"/>.</param>
    public static bool operator <(TemperatureGradient x, TemperatureGradient y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="TemperatureGradient"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="TemperatureGradient"/>.</param>
    public static bool operator >(TemperatureGradient x, TemperatureGradient y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="TemperatureGradient"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="TemperatureGradient"/>.</param>
    public static bool operator <=(TemperatureGradient x, TemperatureGradient y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="TemperatureGradient"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="TemperatureGradient"/>.</param>
    public static bool operator >=(TemperatureGradient x, TemperatureGradient y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="TemperatureGradient"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(TemperatureGradient x) => x.ToDouble();

    /// <summary>Converts the <see cref="TemperatureGradient"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(TemperatureGradient x) => x.ToScalar();

    /// <summary>Constructs the <see cref="TemperatureGradient"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static TemperatureGradient FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="TemperatureGradient"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator TemperatureGradient(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="TemperatureGradient"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static TemperatureGradient FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="TemperatureGradient"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator TemperatureGradient(Scalar x) => FromScalar(x);
}
