namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Distance"/>, describing the distance between points in space.
/// This is the magnitude of the vector quantities <see cref="Displacement3"/> and <see cref="Position3"/>, and is expressed in
/// <see cref="UnitOfLength"/>, with the SI unit being [m].
/// <para>
/// New instances of <see cref="Distance"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfLength"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Distance"/> a = 3 * <see cref="Distance.OneMetre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Distance"/> d = <see cref="Distance.From(Speed, Time)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Distance"/> e = <see cref="Length.AsDistance()"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfLength"/>.
/// </para>
/// </summary>
/// <remarks>
/// <see cref="Distance"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="Length"/></term>
/// <description>A more general form of <see cref="Distance"/>, describing sizes of one-dimensional sections of space.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct Distance :
    IComparable<Distance>,
    IScalarQuantity,
    IScalableScalarQuantity<Distance>,
    IInvertibleScalarQuantity<SpatialFrequency>,
    ISquarableScalarQuantity<Area>,
    ICubableScalarQuantity<Volume>,
    IMultiplicableScalarQuantity<Distance, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Distance, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<Displacement3, Vector3>
{
    /// <summary>The zero-valued <see cref="Distance"/>.</summary>
    public static Distance Zero { get; } = new(0);

    /// <summary>The <see cref="Distance"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Femtometre"/>.</summary>
    public static Distance OneFemtometre { get; } = new(1, UnitOfLength.Femtometre);
    /// <summary>The <see cref="Distance"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Picometre"/>.</summary>
    public static Distance OnePicometre { get; } = new(1, UnitOfLength.Picometre);
    /// <summary>The <see cref="Distance"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Nanometre"/>.</summary>
    public static Distance OneNanometre { get; } = new(1, UnitOfLength.Nanometre);
    /// <summary>The <see cref="Distance"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Micrometre"/>.</summary>
    public static Distance OneMicrometre { get; } = new(1, UnitOfLength.Micrometre);
    /// <summary>The <see cref="Distance"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Millimetre"/>.</summary>
    public static Distance OneMillimetre { get; } = new(1, UnitOfLength.Millimetre);
    /// <summary>The <see cref="Distance"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Centimetre"/>.</summary>
    public static Distance OneCentimetre { get; } = new(1, UnitOfLength.Centimetre);
    /// <summary>The <see cref="Distance"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Decimetre"/>.</summary>
    public static Distance OneDecimetre { get; } = new(1, UnitOfLength.Decimetre);
    /// <summary>The <see cref="Distance"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Metre"/>.</summary>
    public static Distance OneMetre { get; } = new(1, UnitOfLength.Metre);
    /// <summary>The <see cref="Distance"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Kilometre"/>.</summary>
    public static Distance OneKilometre { get; } = new(1, UnitOfLength.Kilometre);
    /// <summary>The <see cref="Distance"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.AstronomicalUnit"/>.</summary>
    public static Distance OneAstronomicalUnit { get; } = new(1, UnitOfLength.AstronomicalUnit);
    /// <summary>The <see cref="Distance"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Lightyear"/>.</summary>
    public static Distance OneLightyear { get; } = new(1, UnitOfLength.Lightyear);
    /// <summary>The <see cref="Distance"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Parsec"/>.</summary>
    public static Distance OneParsec { get; } = new(1, UnitOfLength.Parsec);
    /// <summary>The <see cref="Distance"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Inch"/>.</summary>
    public static Distance OneInch { get; } = new(1, UnitOfLength.Inch);
    /// <summary>The <see cref="Distance"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Foot"/>.</summary>
    public static Distance OneFoot { get; } = new(1, UnitOfLength.Foot);
    /// <summary>The <see cref="Distance"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Yard"/>.</summary>
    public static Distance OneYard { get; } = new(1, UnitOfLength.Yard);
    /// <summary>The <see cref="Distance"/> with magnitude 1, when expressed in unit <see cref="UnitOfLength.Mile"/>.</summary>
    public static Distance OneMile { get; } = new(1, UnitOfLength.Mile);

    /// <summary>Computes <see cref="Distance"/> according to { <see cref="Distance"/> = 1 / <paramref name="spatialFrequency"/> }.</summary>
    /// <summary>Constructs a <see cref="Distance"/> by inverting the <see cref="SpatialFrequency"/> <paramref name="spatialFrequency"/>.</summary>
    public static Distance From(SpatialFrequency spatialFrequency) => new(1 / spatialFrequency.Magnitude);
    /// <summary>Computes <see cref="Distance"/> according to { <see cref="Distance"/> = √<paramref name="area"/> }.</summary>
    /// <param name="area">The square root of this <see cref="Area"/> is taken to produce a <see cref="Distance"/>.</param>
    public static Distance From(Area area) => new(Math.Sqrt(area.Magnitude));
    /// <summary>Computes <see cref="Distance"/> according to { <see cref="Distance"/> = ∛<paramref name="volume"/> }.</summary>
    /// <param name="volume">The cube root of this <see cref="Volume"/> is taken to produce a <see cref="Distance"/>.</param>
    public static Distance From(Volume volume) => new(Math.Cbrt(volume.Magnitude));

    /// <summary>The magnitude of the <see cref="Distance"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Distance.InFemtometres"/>.
    /// <para>This value should only be used (to maximize efficiency) when implementing mathematical operations with other quantities.</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Distance"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfLength"/> <paramref name="unitOfLength"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Distance"/>, in <see cref="UnitOfLength"/> <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">The <see cref="UnitOfLength"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Distance"/> a = 3 * <see cref="Distance.OneFemtometre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Distance(Scalar magnitude, UnitOfLength unitOfLength) : this(magnitude.Magnitude, unitOfLength) { }
    /// <summary>Constructs a new <see cref="Distance"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfLength"/> <paramref name="unitOfLength"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Distance"/>, in <see cref="UnitOfLength"/> <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">The <see cref="UnitOfLength"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Distance"/> a = 3 * <see cref="Distance.OneFemtometre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Distance(double magnitude, UnitOfLength unitOfLength) : this(magnitude * unitOfLength.Factor) { }
    /// <summary>Constructs a new <see cref="Distance"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Distance"/>.</param>
    /// <remarks>Consider preffering a constructor that requires a <see cref="UnitOfLength"/> to be specified.</remarks>
    public Distance(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Distance"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Distance"/>.</param>
    /// <remarks>Consider preferring a constructor that requires a <see cref="UnitOfLength"/> to be specified.</remarks>
    public Distance(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="Distance"/> to an instance of the associated quantity <see cref="Length"/>, of equal magnitude.</summary>
    public Length AsLength => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="Distance"/>, expressed in unit <see cref="UnitOfLength.Femtometre"/>.</summary>
    public Scalar InFemtometres => InUnit(UnitOfLength.Femtometre);
    /// <summary>Retrieves the magnitude of the <see cref="Distance"/>, expressed in unit <see cref="UnitOfLength.Picometre"/>.</summary>
    public Scalar InPicometres => InUnit(UnitOfLength.Picometre);
    /// <summary>Retrieves the magnitude of the <see cref="Distance"/>, expressed in unit <see cref="UnitOfLength.Nanometre"/>.</summary>
    public Scalar InNanometres => InUnit(UnitOfLength.Nanometre);
    /// <summary>Retrieves the magnitude of the <see cref="Distance"/>, expressed in unit <see cref="UnitOfLength.Micrometre"/>.</summary>
    public Scalar InMicrometres => InUnit(UnitOfLength.Micrometre);
    /// <summary>Retrieves the magnitude of the <see cref="Distance"/>, expressed in unit <see cref="UnitOfLength.Millimetre"/>.</summary>
    public Scalar InMillimetres => InUnit(UnitOfLength.Millimetre);
    /// <summary>Retrieves the magnitude of the <see cref="Distance"/>, expressed in unit <see cref="UnitOfLength.Centimetre"/>.</summary>
    public Scalar InCentimetres => InUnit(UnitOfLength.Centimetre);
    /// <summary>Retrieves the magnitude of the <see cref="Distance"/>, expressed in unit <see cref="UnitOfLength.Decimetre"/>.</summary>
    public Scalar InDecimetres => InUnit(UnitOfLength.Decimetre);
    /// <summary>Retrieves the magnitude of the <see cref="Distance"/>, expressed in unit <see cref="UnitOfLength.Metre"/>.</summary>
    public Scalar InMetres => InUnit(UnitOfLength.Metre);
    /// <summary>Retrieves the magnitude of the <see cref="Distance"/>, expressed in unit <see cref="UnitOfLength.Kilometre"/>.</summary>
    public Scalar InKilometres => InUnit(UnitOfLength.Kilometre);

    /// <summary>Retrieves the magnitude of the <see cref="Distance"/>, expressed in unit <see cref="UnitOfLength.AstronomicalUnit"/>.</summary>
    public Scalar InAstronomicalUnits => InUnit(UnitOfLength.AstronomicalUnit);
    /// <summary>Retrieves the magnitude of the <see cref="Distance"/>, expressed in unit <see cref="UnitOfLength.Lightyear"/>.</summary>
    public Scalar InLightyears => InUnit(UnitOfLength.Lightyear);
    /// <summary>Retrieves the magnitude of the <see cref="Distance"/>, expressed in unit <see cref="UnitOfLength.Parsec"/>.</summary>
    public Scalar InParsecs => InUnit(UnitOfLength.Parsec);

    /// <summary>Retrieves the magnitude of the <see cref="Distance"/>, expressed in unit <see cref="UnitOfLength.Inch"/>.</summary>
    public Scalar InInches => InUnit(UnitOfLength.Inch);
    /// <summary>Retrieves the magnitude of the <see cref="Distance"/>, expressed in unit <see cref="UnitOfLength.Foot"/>.</summary>
    public Scalar InFeet => InUnit(UnitOfLength.Foot);
    /// <summary>Retrieves the magnitude of the <see cref="Distance"/>, expressed in unit <see cref="UnitOfLength.Yard"/>.</summary>
    public Scalar InYards => InUnit(UnitOfLength.Yard);
    /// <summary>Retrieves the magnitude of the <see cref="Distance"/>, expressed in unit <see cref="UnitOfLength.Mile"/>.</summary>
    public Scalar InMiles => InUnit(UnitOfLength.Mile);

    /// <summary>Indicates whether the magnitude of the <see cref="Distance"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Distance"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Distance"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Distance"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Distance"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Distance"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Distance"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Distance"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="Distance"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public Distance Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="Distance"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public Distance Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="Distance"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public Distance Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="Distance"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public Distance Round() => new(Math.Round(Magnitude));

    /// <summary>Inverts the <see cref="Distance"/>, producing a <see cref="SpatialFrequency"/>.</summary>
    public SpatialFrequency Invert() => SpatialFrequency.From(this);
    /// <summary>Squares the <see cref="Distance"/>, producing a <see cref="Area"/>.</summary>
    public Area Square() => Area.From(this);
    /// <summary>Cubes the <see cref="Distance"/>, producing a <see cref="Volume"/>.</summary>
    public Volume Cube() => Volume.From(this);

    /// <inheritdoc/>
    public int CompareTo(Distance other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Distance"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [m]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Distance"/>, expressed in <see cref="UnitOfLength"/>
    /// <paramref name="unitOfLength"/>.</summary>
    /// <param name="unitOfLength">The <see cref="UnitOfLength"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfLength unitOfLength) => InUnit(this, unitOfLength);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Distance"/>, expressed in <see cref="UnitOfLength"/>
    /// <paramref name="unitOfLength"/>.</summary>
    /// <param name="distance">The <see cref="Distance"/> to be expressed in <see cref="UnitOfLength"/> <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">The <see cref="UnitOfLength"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Distance distance, UnitOfLength unitOfLength) => new(distance.Magnitude / unitOfLength.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Distance"/>.</summary>
    public Distance Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Distance"/> with negated magnitude.</summary>
    public Distance Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Distance"/>.</param>
    public static Distance operator +(Distance x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Distance"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Distance"/>.</param>
    public static Distance operator -(Distance x) => x.Negate();

    /// <summary>Multiplies the <see cref="Distance"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Distance"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Distance"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Distance"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Distance"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Distance"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Distance"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Distance x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Distance"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Distance"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Distance"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Distance y) => y.Multiply(x);
    /// <summary>Divides the <see cref="Distance"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Distance"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Distance"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Distance x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Distance"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Distance Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Distance"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Distance"/> is scaled.</param>
    public Distance Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Distance"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Distance"/> is divided.</param>
    public Distance Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Distance"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Distance"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="Distance"/> <paramref name="x"/> by this value.</param>
    public static Distance operator %(Distance x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Distance"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Distance"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Distance"/> <paramref name="x"/>.</param>
    public static Distance operator *(Distance x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Distance"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Distance"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Distance"/>, which is scaled by <paramref name="x"/>.</param>
    public static Distance operator *(double x, Distance y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Distance"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Distance"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Distance"/> <paramref name="x"/>.</param>
    public static Distance operator /(Distance x, double y) => x.Divide(y);
/// <summary>Inverts the <see cref="Distance"/> <paramref name="y"/> to produce a <see cref="SpatialFrequency"/>, which is then scaled by <paramref name="x"/>.</summary>
/// <param name="x">This value is used to scale the inverted <see cref="Distance"/> <paramref name="y"/>.</param>
/// <param name="y">The <see cref="Distance"/>, which is inverted to a <see cref="SpatialFrequency"/> and scaled by <paramref name="x"/>.</param>
    public static SpatialFrequency operator /(double x, Distance y) => x * y.Invert();

    /// <summary>Produces a <see cref="Distance"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Distance Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Distance"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Distance"/> is scaled.</param>
    public Distance Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Distance"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Distance"/> is divided.</param>
    public Distance Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Distance"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Distance"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="Distance"/> <paramref name="x"/> by this value.</param>
    public static Distance operator %(Distance x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Distance"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Distance"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Distance"/> <paramref name="x"/>.</param>
    public static Distance operator *(Distance x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Distance"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Distance"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Distance"/>, which is scaled by <paramref name="x"/>.</param>
    public static Distance operator *(Scalar x, Distance y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Distance"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Distance"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Distance"/> <paramref name="x"/>.</param>
    public static Distance operator /(Distance x, Scalar y) => x.Divide(y);
/// <summary>Inverts the <see cref="Distance"/> <paramref name="y"/> to produce a <see cref="SpatialFrequency"/>, which is then scaled by <paramref name="x"/>.</summary>
/// <param name="x">This value is used to scale the inverted <see cref="Distance"/> <paramref name="y"/>.</param>
/// <param name="y">The <see cref="Distance"/>, which is inverted to a <see cref="SpatialFrequency"/> and scaled by <paramref name="x"/>.</param>
    public static SpatialFrequency operator /(Scalar x, Distance y) => x * y.Invert();

    /// <summary>Multiplies the <see cref="Distance"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Distance"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Distance"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Distance"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Distance"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Distance"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Distance"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Distance.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Distance x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Distance"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Distance"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Distance"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Distance.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Distance x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Multiplies the <see cref="Distance"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Distance"/>.</param>
    public Displacement3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="Distance"/> with the <see cref="ValueTuple"/> <paramref name="components"/> to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="components">This <see cref="ValueTuple"/> is multiplied by the <see cref="Distance"/>.</param>
    public Displacement3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="Distance"/> with the <see cref="ValueTuple"/> <paramref name="components"/> to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="components">This <see cref="ValueTuple"/> is multiplied by the <see cref="Distance"/>.</param>
    public Displacement3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplies the <see cref="Distance"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="a">This <see cref="Distance"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Distance"/> <paramref name="a"/>.</param>
    public static Displacement3 operator *(Distance a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Distance"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Distance"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Distance"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Displacement3 operator *(Vector3 a, Distance b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="Distance"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="a">This <see cref="Distance"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="Distance"/> <paramref name="a"/>.</param>
    public static Displacement3 operator *(Distance a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Distance"/> <parmref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="Distance"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Distance"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static Displacement3 operator *((double x, double y, double z) a, Distance b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="Distance"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="a">This <see cref="Distance"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="Distance"/> <paramref name="a"/>.</param>
    public static Displacement3 operator *(Distance a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Distance"/> <parmref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="Displacement3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="Distance"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Distance"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static Displacement3 operator *((Scalar x, Scalar y, Scalar z) a, Distance b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Distance x, Distance y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Distance x, Distance y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Distance x, Distance y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Distance x, Distance y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Distance"/> to a <see cref="double"/> with value <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts the <see cref="Distance"/> to a <see cref="double"/> based on the magnitude of the <see cref="Distance"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Distance x) => x.ToDouble();

    /// <summary>Converts the <see cref="Distance"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts the <see cref="Distance"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Distance x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Distance"/> of magnitude <paramref name="x"/>.</summary>
    public static Distance FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Distance"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Distance(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Distance"/> of equivalent magnitude.</summary>
    public static Distance FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Distance"/> of equivalent magnitude.</summary>
    public static explicit operator Distance(Scalar x) => FromScalar(x);
}
