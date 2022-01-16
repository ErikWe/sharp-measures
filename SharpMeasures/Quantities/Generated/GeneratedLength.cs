namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Length"/>, used for describing one-dimensional sections of space - such as distances and lengths of objects.
/// <para>
/// New instances of <see cref="Length"/> can be constructed according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Length"/> a = 5 * <see cref="Length.OneMetre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Length"/> b = new(7, <see cref="UnitOfLength.Foot"/>);
/// </code>
/// </item>
/// </list>
/// Instances of <see cref="Length"/> may be applied according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Velocity"/> c = 3 * <see cref="Length.OneParsec"/> / <see cref="Time.OneSecond"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved in a desired unit according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Length.InYards"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Length.InUnit(UnitOfLength)"/>;
/// </code>
/// </item>
/// </list>
/// </para>
/// </summary>
public readonly partial record struct Length :
    IComparable<Length>,
    IScalarQuantity<Length>,
    IInvertibleScalarQuantity<SpatialFrequency>,
    ISquarableScalarQuantity<Area>,
    ICubableScalarQuantity<Volume>,
    IAddableScalarQuantity<Length, Length>,
    ISubtractableScalarQuantity<Length, Length>,
    IDivisibleScalarQuantity<Scalar, Length>,
    IVector3izableScalarQuantity<Displacement3>
{
    /// <summary>The zero-valued <see cref="Length"/>.</summary>
    public static Length Zero { get; } = new(0);

    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Femtometre"/>.</summary>
    public static Length OneFemtometre { get; } = new(1, UnitOfLength.Femtometre);
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Picometre"/>.</summary>
    public static Length OnePicometre { get; } = new(1, UnitOfLength.Picometre);
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Nanometre"/>.</summary>
    public static Length OneNanometre { get; } = new(1, UnitOfLength.Nanometre);
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Micrometre"/>.</summary>
    public static Length OneMicrometre { get; } = new(1, UnitOfLength.Micrometre);
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Millimetre"/>.</summary>
    public static Length OneMillimetre { get; } = new(1, UnitOfLength.Millimetre);
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Centimetre"/>.</summary>
    public static Length OneCentimetre { get; } = new(1, UnitOfLength.Centimetre);
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Decimetre"/>.</summary>
    public static Length OneDecimetre { get; } = new(1, UnitOfLength.Decimetre);
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Metre"/>.</summary>
    public static Length OneMetre { get; } = new(1, UnitOfLength.Metre);
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Kilometre"/>.</summary>
    public static Length OneKilometre { get; } = new(1, UnitOfLength.Kilometre);

    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.AstronomicalUnit"/>.</summary>
    public static Length OneAstronomicalUnit { get; } = new(1, UnitOfLength.AstronomicalUnit);
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Lightyear"/>.</summary>
    public static Length OneLightyear { get; } = new(1, UnitOfLength.Lightyear);
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Parsec"/>.</summary>
    public static Length OneParsec { get; } = new(1, UnitOfLength.Parsec);

    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Inch"/>.</summary>
    public static Length OneInch { get; } = new(1, UnitOfLength.Inch);
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Foot"/>.</summary>
    public static Length OneFoot { get; } = new(1, UnitOfLength.Foot);
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Yard"/>.</summary>
    public static Length OneYard { get; } = new(1, UnitOfLength.Yard);
    /// <summary>The <see cref="Length"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Mile"/>.</summary>
    public static Length OneMile { get; } = new(1, UnitOfLength.Mile);

    /// <summary>Constructs a <see cref="Length"/> by inverting the <see cref="SpatialFrequency"/> <paramref name="spatialFrequency"/>.</summary>
    /// <param name="spatialFrequency">This <see cref="SpatialFrequency"/> is inverted to produce a <see cref="Length"/>.</param>
    public static Length From(SpatialFrequency spatialFrequency) => new(1 / spatialFrequency.InPerMetre);
    /// <summary>Constructs a <see cref="Length"/> by taking the square root of the <see cref="Area"/> <paramref name="area"/>.</summary>
    /// <param name="area">The square root of this <see cref="Area"/> is taken to produce a <see cref="Length"/>.</param>
    public static Length From(Area area) => new(Math.Sqrt(area.InSquareMetres));
    /// <summary>Constructs a <see cref="Length"/> by taking the cube root of the <see cref="Volume"/> <paramref name="volume"/>.</summary>
    /// <param name="volume">The cube root of this <see cref="Volume"/> is taken to produce a <see cref="Length"/>.</param>
    public static Length From(Volume volume) => new(Math.Cbrt(volume.InCubicMetres));

    /// <summary>The magnitude of the <see cref="Length"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Length.InMiles"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Length"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfLength"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Length"/>, in unit <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Length"/> a = 2.6 * <see cref="Length.OneLightyear"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Length(double magnitude, UnitOfLength unitOfLength) : this(magnitude * unitOfLength.Factor) { }
    /// <summary>Constructs a new <see cref="Length"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Length"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfLength"/> to be specified.</remarks>
    public Length(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in unit <see cref="UnitOfLength.Femtometre"/>.</summary>
    public Scalar InFemtometres => InUnit(UnitOfLength.Femtometre);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in unit <see cref="UnitOfLength.Picometre"/>.</summary>
    public Scalar InPicometres => InUnit(UnitOfLength.Picometre);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in unit <see cref="UnitOfLength.Nanometre"/>.</summary>
    public Scalar InNanometres => InUnit(UnitOfLength.Nanometre);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in unit <see cref="UnitOfLength.Micrometre"/>.</summary>
    public Scalar InMicrometres => InUnit(UnitOfLength.Micrometre);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in unit <see cref="UnitOfLength.Millimetre"/>.</summary>
    public Scalar InMillimetres => InUnit(UnitOfLength.Millimetre);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in unit <see cref="UnitOfLength.Centimetre"/>.</summary>
    public Scalar InCentimetres => InUnit(UnitOfLength.Centimetre);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in unit <see cref="UnitOfLength.Decimetre"/>.</summary>
    public Scalar InDecimetres => InUnit(UnitOfLength.Decimetre);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in unit <see cref="UnitOfLength.Metre"/>.</summary>
    public Scalar InMetres => InUnit(UnitOfLength.Metre);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in unit <see cref="UnitOfLength.Kilometre"/>.</summary>
    public Scalar InKilometres => InUnit(UnitOfLength.Kilometre);

    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in unit <see cref="UnitOfLength.AstronomicalUnit"/>.</summary>
    public Scalar InAstronomicalUnits => InUnit(UnitOfLength.AstronomicalUnit);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in unit <see cref="UnitOfLength.Lightyear"/>.</summary>
    public Scalar InLightyears => InUnit(UnitOfLength.Lightyear);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in unit <see cref="UnitOfLength.Parsec"/>.</summary>
    public Scalar InParsecs => InUnit(UnitOfLength.Parsec);

    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in unit <see cref="UnitOfLength.Inch"/>.</summary>
    public Scalar InInches => InUnit(UnitOfLength.Inch);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in unit <see cref="UnitOfLength.Foot"/>.</summary>
    public Scalar InFeet => InUnit(UnitOfLength.Foot);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in unit <see cref="UnitOfLength.Yard"/>.</summary>
    public Scalar InYards => InUnit(UnitOfLength.Yard);
    /// <summary>Retrieves the magnitude of the <see cref="Length"/>, expressed in unit <see cref="UnitOfLength.Mile"/>.</summary>
    public Scalar InMiles => InUnit(UnitOfLength.Mile);

    /// <inheritdoc/>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <inheritdoc/>
    public bool IsZero => Magnitude == 0;
    /// <inheritdoc/>
    public bool IsPositive => Magnitude > 0;
    /// <inheritdoc/>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <inheritdoc/>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <inheritdoc/>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <inheritdoc/>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <inheritdoc/>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <inheritdoc/>
    public Length Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public Length Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public Length Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public Length Round() => new(Math.Round(Magnitude));

    /// <summary>Inverts the <see cref="Length"/>, producing a <see cref="SpatialFrequency"/>.</summary>
    public SpatialFrequency Invert() => SpatialFrequency.From(this);
    /// <summary>Squares the <see cref="Length"/>, producing a <see cref="Area"/>.</summary>
    public Area Square() => Area.From(this);
    /// <summary>Cubes the <see cref="Length"/>, producing a <see cref="Volume"/>.</summary>
    public Volume Cube() => Volume.From(this);

    /// <inheritdoc/>
    public int CompareTo(Length other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Length"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [m]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Length"/>, expressed in unit <paramref name="unitOfLength"/>.</summary>
    /// <param name="unitOfLength">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfLength unitOfLength) => InUnit(Magnitude, unitOfLength);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Length"/>, expressed in unit <paramref name="unitOfLength"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="Length"/>.</param>
    /// <param name="unitOfLength">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfLength unitOfLength) => new(magnitude / unitOfLength.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Length"/>.</summary>
    public Length Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Length"/> with negated magnitude.</summary>
    public Length Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Length"/>.</param>
    public static Length operator +(Length x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Length"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Length"/>.</param>
    public static Length operator -(Length x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="Length"/> <paramref name="term"/>, producing another <see cref="Length"/>.</summary>
    /// <param name="term">This <see cref="Length"/> is added to this instance.</param>
    public Length Add(Length term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="Length"/> <paramref name="term"/> from this instance, producing another <see cref="Length"/>.</summary>
    /// <param name="term">This <see cref="Length"/> is subtracted from this instance.</param>
    public Length Subtract(Length term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="Length"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="Length"/>.</summary>
    /// <param name="x">This <see cref="Length"/> is added to the <see cref="Length"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="Length"/> is added to the <see cref="Length"/> <paramref name="x"/>.</param>
    public static Length operator +(Length x, Length y) => x.Add(y);
    /// <summary>Subtract the <see cref="Length"/> <paramref name="y"/> from the <see cref="Length"/> <paramref name="x"/>, producing another <see cref="Length"/>.</summary>
    /// <param name="x">The <see cref="Length"/> <paramref name="y"/> is subtracted from this <see cref="Length"/>.</param>
    /// <param name="y">This <see cref="Length"/> is subtracted from the <see cref="Length"/> <paramref name="x"/>.</param>
    public static Length operator -(Length x, Length y) => x.Subtract(y);

    /// <summary>Divides this <see cref="Length"/> by the <see cref="Length"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Length"/> is divided by this <see cref="Length"/>.</param>
    public Scalar Divide(Length divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="Length"/> <paramref name="x"/> by the <see cref="Length"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="Length"/> is divided by the <see cref="Length"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Length"/> <paramref name="x"/> is divided by this <see cref="Length"/>.</param>
    public static Scalar operator /(Length x, Length y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="Length"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Length"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Length"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Length"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Length"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Length"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Length"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Length x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Length"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Length"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Length"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Length x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Length"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Length Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Length"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Length"/> is scaled.</param>
    public Length Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Length"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Length"/> is divided.</param>
    public Length Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Length"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Length"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
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
    public static SpatialFrequency operator /(double x, Length y) => x * y.Invert();

    /// <summary>Produces a <see cref="Length"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Length Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Length"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Length"/> is scaled.</param>
    public Length Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Length"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Length"/> is divided.</param>
    public Length Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Length"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Length"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
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
    /// <summary>Inverts the <see cref="Length"/> <paramref name="y"/> to produce a <see cref="SpatialFrequency"/>, which is then scaled by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="Length"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Length"/>, which is inverted to a <see cref="SpatialFrequency"/> and scaled by <paramref name="x"/>.</param>
    public static SpatialFrequency operator /(Scalar x, Length y) => x * y.Invert();

    /// <summary>Multiplies the <see cref="Length"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Length"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Length"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Length"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Length"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Length"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Length"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Length.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Length x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Length"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Length"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Length"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Length.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Length x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Multiplies the <see cref="Length"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Length"/>.</param>
    public Displacement3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="Length"/> with the <see cref="ValueTuple"/> <paramref name="vector"/> to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Length"/>.</param>
    public Displacement3 Multiply((double x, double y, double z) vector) => new(Magnitude * vector.x, Magnitude * vector.y, Magnitude * vector.z);
    /// <summary>Multiplies the <see cref="Length"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="a">This <see cref="Length"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Length"/> <paramref name="a"/>.</param>
    public static Displacement3 operator *(Length a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Length"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Length"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Length"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Displacement3 operator *(Vector3 a, Length b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="Length"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="a">This <see cref="Length"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="Length"/> <paramref name="a"/>.</param>
    public static Displacement3 operator *(Length a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Length"/> <paramref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="Length"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Length"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static Displacement3 operator *((double x, double y, double z) a, Length b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Length x, Length y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Length x, Length y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Length x, Length y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Length x, Length y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="Length"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Length x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Length x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Length"/> of magnitude <paramref name="x"/>.</summary>
    public static Length FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Length"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Length(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Length"/> of equivalent magnitude.</summary>
    public static Length FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Length"/> of equivalent magnitude.</summary>
    public static explicit operator Length(Scalar x) => FromScalar(x);
}