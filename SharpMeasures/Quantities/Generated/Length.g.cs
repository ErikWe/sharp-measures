#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Length"/>, describing the size of a one-dimensional section of space.
/// The quantity is expressed in <see cref="UnitOfLength"/>, with the SI unit being [m].
/// <para>
/// New instances of <see cref="Length"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfLength"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Length"/> a = 3 * <see cref="Length.OneMetre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Length"/> d = <see cref="Length.From(Mass, LinearDensity)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Length"/> e = <see cref="Distance.AsLength"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="Length"/> can be retrieved in the desired <see cref="UnitOfLength"/> using pre-defined properties,
/// such as <see cref="Metres"/>
/// </para>
/// </summary>
/// <remarks>
/// <see cref="Length"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="Distance"/></term>
/// <description>A more specialized form of <see cref="Length"/>, describing the distance between points in space.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct Length :
    IComparable<Length>,
    IScalarQuantity,
    IScalableScalarQuantity<Length>,
    IInvertibleScalarQuantity<SpatialFrequency>,
    ISquarableScalarQuantity<Area>,
    ICubableScalarQuantity<Volume>,
    IMultiplicableScalarQuantity<Length, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Length, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<Displacement3, Vector3>
{
    /// <summary>The zero-valued <see cref="Length"/>.</summary>
    public static Length Zero { get; } = new(0);

    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Metre"/>.</summary>
    public static Length OneMetre { get; } = UnitOfLength.Metre.Length;
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Femtometre"/>.</summary>
    public static Length OneFemtometre { get; } = UnitOfLength.Femtometre.Length;
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Picometre"/>.</summary>
    public static Length OnePicometre { get; } = UnitOfLength.Picometre.Length;
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Nanometre"/>.</summary>
    public static Length OneNanometre { get; } = UnitOfLength.Nanometre.Length;
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Micrometre"/>.</summary>
    public static Length OneMicrometre { get; } = UnitOfLength.Micrometre.Length;
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Millimetre"/>.</summary>
    public static Length OneMillimetre { get; } = UnitOfLength.Millimetre.Length;
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Centimetre"/>.</summary>
    public static Length OneCentimetre { get; } = UnitOfLength.Centimetre.Length;
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Decimetre"/>.</summary>
    public static Length OneDecimetre { get; } = UnitOfLength.Decimetre.Length;
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Kilometre"/>.</summary>
    public static Length OneKilometre { get; } = UnitOfLength.Kilometre.Length;
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.AstronomicalUnit"/>.</summary>
    public static Length OneAstronomicalUnit { get; } = UnitOfLength.AstronomicalUnit.Length;
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.LightYear"/>.</summary>
    public static Length OneLightYear { get; } = UnitOfLength.LightYear.Length;
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Parsec"/>.</summary>
    public static Length OneParsec { get; } = UnitOfLength.Parsec.Length;
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Kiloparsec"/>.</summary>
    public static Length OneKiloparsec { get; } = UnitOfLength.Kiloparsec.Length;
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Megaparsec"/>.</summary>
    public static Length OneMegaparsec { get; } = UnitOfLength.Megaparsec.Length;
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Gigaparsec"/>.</summary>
    public static Length OneGigaparsec { get; } = UnitOfLength.Gigaparsec.Length;
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Inch"/>.</summary>
    public static Length OneInch { get; } = UnitOfLength.Inch.Length;
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Foot"/>.</summary>
    public static Length OneFoot { get; } = UnitOfLength.Foot.Length;
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Yard"/>.</summary>
    public static Length OneYard { get; } = UnitOfLength.Yard.Length;
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Mile"/>.</summary>
    public static Length OneMile { get; } = UnitOfLength.Mile.Length;

    /// <summary>Computes <see cref="Length"/> according to { 1 / <paramref name="spatialFrequency"/> }.</summary>
    /// <summary>Constructs a <see cref="Length"/> by inverting the <see cref="SpatialFrequency"/> <paramref name="spatialFrequency"/>.</summary>
    public static Length From(SpatialFrequency spatialFrequency) => new(1 / spatialFrequency.Magnitude);
    /// <summary>Computes <see cref="Length"/> according to { √<paramref name="area"/> }.</summary>
    /// <param name="area">The square root of this <see cref="Area"/> is taken to produce a <see cref="Length"/>.</param>
    public static Length From(Area area) => new(Math.Sqrt(area.Magnitude));
    /// <summary>Computes <see cref="Length"/> according to { ∛<paramref name="volume"/> }.</summary>
    /// <param name="volume">The cube root of this <see cref="Volume"/> is taken to produce a <see cref="Length"/>.</param>
    public static Length From(Volume volume) => new(Math.Cbrt(volume.Magnitude));

    /// <summary>The magnitude of the <see cref="Length"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfLength)"/> or a pre-defined property
    /// - such as <see cref="Metres"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Length"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfLength"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Length"/>, expressed in <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">The <see cref="UnitOfLength"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Length"/> a = 3 * <see cref="Length.OneMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Length(Scalar magnitude, UnitOfLength unitOfLength) : this(magnitude.Magnitude, unitOfLength) { }
    /// <summary>Constructs a new <see cref="Length"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfLength"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Length"/>, expressed in <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">The <see cref="UnitOfLength"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Length"/> a = 3 * <see cref="Length.OneMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Length(double magnitude, UnitOfLength unitOfLength) : this(magnitude * unitOfLength.Length.Magnitude) { }
    /// <summary>Constructs a new <see cref="Length"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Length"/>.</param>
    /// <remarks>Consider preferring <see cref="Length(Scalar, UnitOfLength)"/>.</remarks>
    public Length(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Length"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Length"/>.</param>
    /// <remarks>Consider preferring <see cref="Length(double, UnitOfLength)"/>.</remarks>
    public Length(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="Length"/> to an instance of the associated quantity <see cref="Distance"/>, of equal magnitude.</summary>
    public Distance AsDistance => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in <see cref="UnitOfLength.Metre"/>.</summary>
    public Scalar Metres => InUnit(UnitOfLength.Metre);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in <see cref="UnitOfLength.Femtometre"/>.</summary>
    public Scalar Femtometres => InUnit(UnitOfLength.Femtometre);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in <see cref="UnitOfLength.Picometre"/>.</summary>
    public Scalar Picometres => InUnit(UnitOfLength.Picometre);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in <see cref="UnitOfLength.Nanometre"/>.</summary>
    public Scalar Nanometres => InUnit(UnitOfLength.Nanometre);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in <see cref="UnitOfLength.Micrometre"/>.</summary>
    public Scalar Micrometres => InUnit(UnitOfLength.Micrometre);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in <see cref="UnitOfLength.Millimetre"/>.</summary>
    public Scalar Millimetres => InUnit(UnitOfLength.Millimetre);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in <see cref="UnitOfLength.Centimetre"/>.</summary>
    public Scalar Centimetres => InUnit(UnitOfLength.Centimetre);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in <see cref="UnitOfLength.Decimetre"/>.</summary>
    public Scalar Decimetres => InUnit(UnitOfLength.Decimetre);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in <see cref="UnitOfLength.Kilometre"/>.</summary>
    public Scalar Kilometres => InUnit(UnitOfLength.Kilometre);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in <see cref="UnitOfLength.AstronomicalUnit"/>.</summary>
    public Scalar AstronomicalUnits => InUnit(UnitOfLength.AstronomicalUnit);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in <see cref="UnitOfLength.LightYear"/>.</summary>
    public Scalar LightYears => InUnit(UnitOfLength.LightYear);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in <see cref="UnitOfLength.Parsec"/>.</summary>
    public Scalar Parsecs => InUnit(UnitOfLength.Parsec);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in <see cref="UnitOfLength.Kiloparsec"/>.</summary>
    public Scalar Kiloparsecs => InUnit(UnitOfLength.Kiloparsec);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in <see cref="UnitOfLength.Megaparsec"/>.</summary>
    public Scalar Megaparsecs => InUnit(UnitOfLength.Megaparsec);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in <see cref="UnitOfLength.Gigaparsec"/>.</summary>
    public Scalar Gigaparsecs => InUnit(UnitOfLength.Gigaparsec);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in <see cref="UnitOfLength.Inch"/>.</summary>
    public Scalar Inches => InUnit(UnitOfLength.Inch);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in <see cref="UnitOfLength.Foot"/>.</summary>
    public Scalar Feet => InUnit(UnitOfLength.Foot);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in <see cref="UnitOfLength.Yard"/>.</summary>
    public Scalar Yards => InUnit(UnitOfLength.Yard);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in <see cref="UnitOfLength.Mile"/>.</summary>
    public Scalar Miles => InUnit(UnitOfLength.Mile);

    /// <summary>Indicates whether the magnitude of the <see cref="Length"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Length"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Length"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Length"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Length"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Length"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Length"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Length"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="Length"/>.</summary>
    public Length Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="Length"/>.</summary>
    public Length Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="Length"/>.</summary>
    public Length Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="Length"/> to the nearest integer value.</summary>
    public Length Round() => new(Math.Round(Magnitude));

    /// <summary>Computes the inverse of the <see cref="Length"/>, producing a <see cref="SpatialFrequency"/>.</summary>
    public SpatialFrequency Invert() => SpatialFrequency.From(this);
    /// <summary>Computes the square of the <see cref="Length"/>, producing a <see cref="Area"/>.</summary>
    public Area Square() => Area.From(this);
    /// <summary>Computes the cube of the <see cref="Length"/>, producing a <see cref="Volume"/>.</summary>
    public Volume Cube() => Volume.From(this);

    /// <inheritdoc/>
    public int CompareTo(Length other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Length"/> in the default unit
    /// <see cref="UnitOfLength.Metre"/>, followed by the symbol [m].</summary>
    public override string ToString() => $"{Metres} [m]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Length"/>,
    /// expressed in <paramref name="unitOfLength"/>.</summary>
    /// <param name="unitOfLength">The <see cref="UnitOfLength"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfLength unitOfLength) => InUnit(this, unitOfLength);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Length"/>,
    /// expressed in <paramref name="unitOfLength"/>.</summary>
    /// <param name="length">The <see cref="Length"/> to be expressed in <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">The <see cref="UnitOfLength"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Length length, UnitOfLength unitOfLength) => new(length.Magnitude / unitOfLength.Length.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Length"/>.</summary>
    public Length Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Length"/> with negated magnitude.</summary>
    public Length Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Length"/>.</param>
    public static Length operator +(Length x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Length"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Length"/>.</param>
    public static Length operator -(Length x) => x.Negate();

    /// <summary>Multiplicates the <see cref="Length"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Length"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Length"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Length"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Length"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Length"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Length"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Length x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Length"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Length"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Length"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Length y) => y.Multiply(x);
    /// <summary>Division of the <see cref="Length"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Length"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Length"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Length x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="Length"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="Length"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Length"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, Length y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="Length"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Length Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Length"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Length"/> is scaled.</param>
    public Length Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Length"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Length"/> is divided.</param>
    public Length Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Length"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Length"/> <paramref name="x"/> by this value.</param>
    public static Length operator %(Length x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Length"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Length"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Length"/> <paramref name="x"/>.</param>
    public static Length operator *(Length x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Length"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Length"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Length"/>, which is scaled by <paramref name="x"/>.</param>
    public static Length operator *(double x, Length y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Length"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Length"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Length"/> <paramref name="x"/>.</param>
    public static Length operator /(Length x, double y) => x.Divide(y);
    /// <summary>Inverts the <see cref="Length"/> <paramref name="y"/> to produce a <see cref="SpatialFrequency"/>, which is then scaled by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="Length"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Length"/>, which is inverted to a <see cref="SpatialFrequency"/> and scaled by <paramref name="x"/>.</param>
    public static SpatialFrequency operator /(double x, Length y) => new(x / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="Length"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Length Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Length"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Length"/> is scaled.</param>
    public Length Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Length"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Length"/> is divided.</param>
    public Length Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Length"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Length"/> <paramref name="x"/> by this value.</param>
    public static Length operator %(Length x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Length"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Length"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Length"/> <paramref name="x"/>.</param>
    public static Length operator *(Length x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Length"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Length"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Length"/>, which is scaled by <paramref name="x"/>.</param>
    public static Length operator *(Scalar x, Length y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Length"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Length"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Length"/> <paramref name="x"/>.</param>
    public static Length operator /(Length x, Scalar y) => x.Divide(y);
    /// <summary>Inverts the <see cref="Length"/> <paramref name="y"/> to produce a <see cref="SpatialFrequency"/>,
    /// which is then scaled by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="Length"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Length"/>, which is inverted to a <see cref="SpatialFrequency"/> and scaled by <paramref name="x"/>.</param>
    public static SpatialFrequency operator /(Scalar x, Length y) => new(x / y.Magnitude);

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

    /// <summary>Multiplication of the <see cref="Length"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Length"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Length"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Length x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="Length"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Length"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Length"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Length x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Multiplicates the <see cref="Length"/> with the <see cref="Vector3"/> <paramref name="factor"/> to produce
    /// a <see cref="Displacement3"/>.</summary>
    /// <param name="factor">This <see cref="Vector3"/> is multiplied by the <see cref="Length"/>.</param>
    public Displacement3 Multiply(Vector3 factor) => new(factor * Magnitude);
    /// <summary>Multiplicates the <see cref="Length"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="Displacement3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Length"/>.</param>
    public Displacement3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplicates the <see cref="Length"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="Displacement3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Length"/>.</param>
    public Displacement3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplication of the <see cref="Length"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/>
    /// to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="a">This <see cref="Length"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Length"/> <paramref name="a"/>.</param>
    public static Displacement3 operator *(Length a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Length"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/>
    /// to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Length"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Length"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Displacement3 operator *(Vector3 a, Length b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="Length"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="a">This <see cref="Length"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Length"/> <paramref name="a"/>.</param>
    public static Displacement3 operator *(Length a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Length"/> <parmref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Length"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Length"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Displacement3 operator *((double x, double y, double z) a, Length b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="Length"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="a">This <see cref="Length"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Length"/> <paramref name="a"/>.</param>
    public static Displacement3 operator *(Length a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Length"/> <parmref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Length"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Length"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Displacement3 operator *((Scalar x, Scalar y, Scalar z) a, Length b) => b.Multiply(a);

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Length"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Length"/>.</param>
    public static bool operator <(Length x, Length y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Length"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Length"/>.</param>
    public static bool operator >(Length x, Length y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Length"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Length"/>.</param>
    public static bool operator <=(Length x, Length y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Length"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Length"/>.</param>
    public static bool operator >=(Length x, Length y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Length"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(Length x) => x.ToDouble();

    /// <summary>Converts the <see cref="Length"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Length x) => x.ToScalar();

    /// <summary>Constructs the <see cref="Length"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Length FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Length"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Length(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="Length"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static Length FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="Length"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator Length(Scalar x) => FromScalar(x);
}
