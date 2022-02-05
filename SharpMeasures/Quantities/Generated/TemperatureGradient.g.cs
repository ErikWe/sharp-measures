namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="TemperatureGradient"/>, describing a <see cref="TemperatureDifference"/> over <see cref="Distance"/>.
/// This is the magnitude of the vector quantity <see cref="TemperatureGradient3"/>, and is expressed in <see cref="UnitOfTemperatureGradient"/>, with the SI unit being [K / m].
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
/// The magnitude of the measure can be retrieved using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfTemperatureGradient"/>.
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
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<TemperatureGradient3, Vector3>
{
    /// <summary>The zero-valued <see cref="TemperatureGradient"/>.</summary>
    public static TemperatureGradient Zero { get; } = new(0);

    /// <summary>The <see cref="TemperatureGradient"/> with magnitude 1, when expressed in unit <see cref="UnitOfTemperatureGradient.KelvinPerMetre"/>.</summary>
    public static TemperatureGradient OneKelvinPerMetre { get; } = new(1, UnitOfTemperatureGradient.KelvinPerMetre);
    /// <summary>The <see cref="TemperatureGradient"/> with magnitude 1, when expressed in unit <see cref="UnitOfTemperatureGradient.CelsiusPerMetre"/>.</summary>
    public static TemperatureGradient OneCelsiusPerMetre { get; } = new(1, UnitOfTemperatureGradient.CelsiusPerMetre);
    /// <summary>The <see cref="TemperatureGradient"/> with magnitude 1, when expressed in unit <see cref="UnitOfTemperatureGradient.RankinePerMetre"/>.</summary>
    public static TemperatureGradient OneRankinePerMetre { get; } = new(1, UnitOfTemperatureGradient.RankinePerMetre);
    /// <summary>The <see cref="TemperatureGradient"/> with magnitude 1, when expressed in unit <see cref="UnitOfTemperatureGradient.FahrenheitPerMetre"/>.</summary>
    public static TemperatureGradient OneFahrenheitPerMetre { get; } = new(1, UnitOfTemperatureGradient.FahrenheitPerMetre);

    /// <summary>The magnitude of the <see cref="TemperatureGradient"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="TemperatureGradient.InKelvinPerMetre"/>.
    /// <para>This value should only be used (to maximize efficiency) when implementing mathematical operations with other quantities.</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="TemperatureGradient"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfTemperatureGradient"/> <paramref name="unitOfTemperatureGradient"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TemperatureGradient"/>, in <see cref="UnitOfTemperatureGradient"/> <paramref name="unitOfTemperatureGradient"/>.</param>
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
    /// <summary>Constructs a new <see cref="TemperatureGradient"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfTemperatureGradient"/> <paramref name="unitOfTemperatureGradient"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TemperatureGradient"/>, in <see cref="UnitOfTemperatureGradient"/> <paramref name="unitOfTemperatureGradient"/>.</param>
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
    public TemperatureGradient(double magnitude, UnitOfTemperatureGradient unitOfTemperatureGradient) : this(magnitude * unitOfTemperatureGradient.Factor) { }
    /// <summary>Constructs a new <see cref="TemperatureGradient"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TemperatureGradient"/>.</param>
    /// <remarks>Consider preffering a constructor that requires a <see cref="UnitOfTemperatureGradient"/> to be specified.</remarks>
    public TemperatureGradient(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="TemperatureGradient"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TemperatureGradient"/>.</param>
    /// <remarks>Consider preferring a constructor that requires a <see cref="UnitOfTemperatureGradient"/> to be specified.</remarks>
    public TemperatureGradient(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="TemperatureGradient"/>, expressed in unit <see cref="UnitOfTemperatureGradient.KelvinPerMetre"/>.</summary>
    public Scalar InKelvinPerMetre => InUnit(UnitOfTemperatureGradient.KelvinPerMetre);
    /// <summary>Retrieves the magnitude of the <see cref="TemperatureGradient"/>, expressed in unit <see cref="UnitOfTemperatureGradient.CelsiusPerMetre"/>.</summary>
    public Scalar InCelsiusPerMetre => InUnit(UnitOfTemperatureGradient.CelsiusPerMetre);
    /// <summary>Retrieves the magnitude of the <see cref="TemperatureGradient"/>, expressed in unit <see cref="UnitOfTemperatureGradient.RankinePerMetre"/>.</summary>
    public Scalar InRankinePerMetre => InUnit(UnitOfTemperatureGradient.RankinePerMetre);
    /// <summary>Retrieves the magnitude of the <see cref="TemperatureGradient"/>, expressed in unit <see cref="UnitOfTemperatureGradient.FahrenheitPerMetre"/>.</summary>
    public Scalar InFahrenheitPerMetre => InUnit(UnitOfTemperatureGradient.FahrenheitPerMetre);

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

    /// <summary>Produces a <see cref="TemperatureGradient"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public TemperatureGradient Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="TemperatureGradient"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public TemperatureGradient Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="TemperatureGradient"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public TemperatureGradient Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="TemperatureGradient"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public TemperatureGradient Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(TemperatureGradient other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="TemperatureGradient"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [K / m]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="TemperatureGradient"/>, expressed in <see cref="UnitOfTemperatureGradient"/>
    /// <paramref name="unitOfTemperatureGradient"/>.</summary>
    /// <param name="unitOfTemperatureGradient">The <see cref="UnitOfTemperatureGradient"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfTemperatureGradient unitOfTemperatureGradient) => InUnit(this, unitOfTemperatureGradient);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="TemperatureGradient"/>, expressed in <see cref="UnitOfTemperatureGradient"/>
    /// <paramref name="unitOfTemperatureGradient"/>.</summary>
    /// <param name="temperatureGradient">The <see cref="TemperatureGradient"/> to be expressed in <see cref="UnitOfTemperatureGradient"/> <paramref name="unitOfTemperatureGradient"/>.</param>
    /// <param name="unitOfTemperatureGradient">The <see cref="UnitOfTemperatureGradient"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(TemperatureGradient temperatureGradient, UnitOfTemperatureGradient unitOfTemperatureGradient) => new(temperatureGradient.Magnitude / unitOfTemperatureGradient.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="TemperatureGradient"/>.</summary>
    public TemperatureGradient Plus() => this;
    /// <summary>Negation, resulting in a <see cref="TemperatureGradient"/> with negated magnitude.</summary>
    public TemperatureGradient Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="TemperatureGradient"/>.</param>
    public static TemperatureGradient operator +(TemperatureGradient x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="TemperatureGradient"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="TemperatureGradient"/>.</param>
    public static TemperatureGradient operator -(TemperatureGradient x) => x.Negate();

    /// <summary>Multiplies the <see cref="TemperatureGradient"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="TemperatureGradient"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="TemperatureGradient"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="TemperatureGradient"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="TemperatureGradient"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureGradient"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="TemperatureGradient"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(TemperatureGradient x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="TemperatureGradient"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="TemperatureGradient"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="TemperatureGradient"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, TemperatureGradient y) => y.Multiply(x);
    /// <summary>Divides the <see cref="TemperatureGradient"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureGradient"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="TemperatureGradient"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(TemperatureGradient x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="TemperatureGradient"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public TemperatureGradient Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="TemperatureGradient"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="TemperatureGradient"/> is scaled.</param>
    public TemperatureGradient Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="TemperatureGradient"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="TemperatureGradient"/> is divided.</param>
    public TemperatureGradient Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="TemperatureGradient"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureGradient"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="TemperatureGradient"/> <paramref name="x"/> by this value.</param>
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

    /// <summary>Produces a <see cref="TemperatureGradient"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public TemperatureGradient Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="TemperatureGradient"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="TemperatureGradient"/> is scaled.</param>
    public TemperatureGradient Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="TemperatureGradient"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="TemperatureGradient"/> is divided.</param>
    public TemperatureGradient Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="TemperatureGradient"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureGradient"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="TemperatureGradient"/> <paramref name="x"/> by this value.</param>
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

    /// <summary>Multiplies the <see cref="TemperatureGradient"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="TemperatureGradient"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="TemperatureGradient"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="TemperatureGradient"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="TemperatureGradient"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureGradient"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="TemperatureGradient"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="TemperatureGradient.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(TemperatureGradient x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="TemperatureGradient"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureGradient"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="TemperatureGradient"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="TemperatureGradient.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(TemperatureGradient x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Multiplies the <see cref="TemperatureGradient"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="TemperatureGradient3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="TemperatureGradient"/>.</param>
    public TemperatureGradient3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="TemperatureGradient"/> with the <see cref="ValueTuple"/> <paramref name="components"/> to produce a <see cref="TemperatureGradient3"/>.</summary>
    /// <param name="components">This <see cref="ValueTuple"/> is multiplied by the <see cref="TemperatureGradient"/>.</param>
    public TemperatureGradient3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="TemperatureGradient"/> with the <see cref="ValueTuple"/> <paramref name="components"/> to produce a <see cref="TemperatureGradient3"/>.</summary>
    /// <param name="components">This <see cref="ValueTuple"/> is multiplied by the <see cref="TemperatureGradient"/>.</param>
    public TemperatureGradient3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="TemperatureGradient"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="TemperatureGradient3"/>.</summary>
    /// <param name="a">This <see cref="TemperatureGradient"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="TemperatureGradient"/> <paramref name="a"/>.</param>
    public static TemperatureGradient3 operator *(TemperatureGradient a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="TemperatureGradient"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="TemperatureGradient3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="TemperatureGradient"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="TemperatureGradient"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static TemperatureGradient3 operator *(Vector3 a, TemperatureGradient b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="TemperatureGradient"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="TemperatureGradient3"/>.</summary>
    /// <param name="a">This <see cref="TemperatureGradient"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="TemperatureGradient"/> <paramref name="a"/>.</param>
    public static TemperatureGradient3 operator *(TemperatureGradient a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="TemperatureGradient"/> <parmref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="TemperatureGradient3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="TemperatureGradient"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="TemperatureGradient"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static TemperatureGradient3 operator *((double x, double y, double z) a, TemperatureGradient b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="TemperatureGradient"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="TemperatureGradient3"/>.</summary>
    /// <param name="a">This <see cref="TemperatureGradient"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="TemperatureGradient"/> <paramref name="a"/>.</param>
    public static TemperatureGradient3 operator *(TemperatureGradient a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="TemperatureGradient"/> <parmref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="TemperatureGradient3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="TemperatureGradient"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="TemperatureGradient"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static TemperatureGradient3 operator *((Scalar x, Scalar y, Scalar z) a, TemperatureGradient b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(TemperatureGradient x, TemperatureGradient y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(TemperatureGradient x, TemperatureGradient y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(TemperatureGradient x, TemperatureGradient y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(TemperatureGradient x, TemperatureGradient y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="TemperatureGradient"/> to a <see cref="double"/> with value <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts the <see cref="TemperatureGradient"/> to a <see cref="double"/> based on the magnitude of the <see cref="TemperatureGradient"/> <paramref name="x"/>.</summary>
    public static implicit operator double(TemperatureGradient x) => x.ToDouble();

    /// <summary>Converts the <see cref="TemperatureGradient"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts the <see cref="TemperatureGradient"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(TemperatureGradient x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="TemperatureGradient"/> of magnitude <paramref name="x"/>.</summary>
    public static TemperatureGradient FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="TemperatureGradient"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator TemperatureGradient(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="TemperatureGradient"/> of equivalent magnitude.</summary>
    public static TemperatureGradient FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="TemperatureGradient"/> of equivalent magnitude.</summary>
    public static explicit operator TemperatureGradient(Scalar x) => FromScalar(x);
}
