namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Volume"/>, describing the size of a three-dimensional section of space - the cube of <see cref="Length"/>.
/// The quantity is expressed in <see cref="UnitOfVolume"/>, with the SI unit being [m³].
/// <para>
/// New instances of <see cref="Volume"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfVolume"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Volume"/> a = 3 * <see cref="Volume.OneLitre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Volume"/> d = <see cref="Volume.From(Mass, Density)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfVolume"/>.
/// </para>
/// </summary>
public readonly partial record struct Volume :
    IComparable<Volume>,
    IScalarQuantity,
    IScalableScalarQuantity<Volume>,
    ICubeRootableScalarQuantity<Length>,
    IMultiplicableScalarQuantity<Volume, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Volume, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="Volume"/>.</summary>
    public static Volume Zero { get; } = new(0);

    /// <summary>The <see cref="Volume"/> with magnitude 1, when expressed in unit <see cref="UnitOfVolume.CubicDecimetre"/>.</summary>
    public static Volume OneCubicDecimetre { get; } = new(1, UnitOfVolume.CubicDecimetre);
    /// <summary>The <see cref="Volume"/> with magnitude 1, when expressed in unit <see cref="UnitOfVolume.CubicMetre"/>.</summary>
    public static Volume OneCubicMetre { get; } = new(1, UnitOfVolume.CubicMetre);
    /// <summary>The <see cref="Volume"/> with magnitude 1, when expressed in unit <see cref="UnitOfVolume.Millilitre"/>.</summary>
    public static Volume OneMillilitre { get; } = new(1, UnitOfVolume.Millilitre);
    /// <summary>The <see cref="Volume"/> with magnitude 1, when expressed in unit <see cref="UnitOfVolume.Centilitre"/>.</summary>
    public static Volume OneCentilitre { get; } = new(1, UnitOfVolume.Centilitre);
    /// <summary>The <see cref="Volume"/> with magnitude 1, when expressed in unit <see cref="UnitOfVolume.Decilitre"/>.</summary>
    public static Volume OneDecilitre { get; } = new(1, UnitOfVolume.Decilitre);
    /// <summary>The <see cref="Volume"/> with magnitude 1, when expressed in unit <see cref="UnitOfVolume.Litre"/>.</summary>
    public static Volume OneLitre { get; } = new(1, UnitOfVolume.Litre);

    /// <summary>Computes <see cref="Volume"/> according to { <see cref="Volume"/> = <paramref name="length"/>³ }.</summary>
    /// <param name="length">This <see cref="Length"/> is cubed to produce a <see cref="Volume"/>.</param>
    public static Volume From(Length length) => new(Math.Pow(length.Magnitude, 3));
    /// <summary>Computes <see cref="Volume"/> according to { <see cref="Volume"/> = <paramref name="distance"/>³ }.</summary>
    /// <param name="distance">This <see cref="Distance"/> is cubed to produce a <see cref="Volume"/>.</param>
    public static Volume From(Distance distance) => new(Math.Pow(distance.Magnitude, 3));

    /// <summary>The magnitude of the <see cref="Volume"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Volume.InCubicDecimetres"/>.
    /// <para>This value should only be used (to maximize efficiency) when implementing mathematical operations with other quantities.</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Volume"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfVolume"/> <paramref name="unitOfVolume"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Volume"/>, in <see cref="UnitOfVolume"/> <paramref name="unitOfVolume"/>.</param>
    /// <param name="unitOfVolume">The <see cref="UnitOfVolume"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Volume"/> a = 3 * <see cref="Volume.OneCubicDecimetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Volume(Scalar magnitude, UnitOfVolume unitOfVolume) : this(magnitude.Magnitude, unitOfVolume) { }
    /// <summary>Constructs a new <see cref="Volume"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfVolume"/> <paramref name="unitOfVolume"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Volume"/>, in <see cref="UnitOfVolume"/> <paramref name="unitOfVolume"/>.</param>
    /// <param name="unitOfVolume">The <see cref="UnitOfVolume"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Volume"/> a = 3 * <see cref="Volume.OneCubicDecimetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Volume(double magnitude, UnitOfVolume unitOfVolume) : this(magnitude * unitOfVolume.Factor) { }
    /// <summary>Constructs a new <see cref="Volume"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Volume"/>.</param>
    /// <remarks>Consider preffering a constructor that requires a <see cref="UnitOfVolume"/> to be specified.</remarks>
    public Volume(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Volume"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Volume"/>.</param>
    /// <remarks>Consider preferring a constructor that requires a <see cref="UnitOfVolume"/> to be specified.</remarks>
    public Volume(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Volume"/>, expressed in unit <see cref="UnitOfVolume.CubicDecimetre"/>.</summary>
    public Scalar InCubicDecimetres => InUnit(UnitOfVolume.CubicDecimetre);
    /// <summary>Retrieves the magnitude of the <see cref="Volume"/>, expressed in unit <see cref="UnitOfVolume.CubicMetre"/>.</summary>
    public Scalar InCubicMetres => InUnit(UnitOfVolume.CubicMetre);

    /// <summary>Retrieves the magnitude of the <see cref="Volume"/>, expressed in unit <see cref="UnitOfVolume.Millilitre"/>.</summary>
    public Scalar InMillilitres => InUnit(UnitOfVolume.Millilitre);
    /// <summary>Retrieves the magnitude of the <see cref="Volume"/>, expressed in unit <see cref="UnitOfVolume.Centilitre"/>.</summary>
    public Scalar InCentilitres => InUnit(UnitOfVolume.Centilitre);
    /// <summary>Retrieves the magnitude of the <see cref="Volume"/>, expressed in unit <see cref="UnitOfVolume.Decilitre"/>.</summary>
    public Scalar InDecilitres => InUnit(UnitOfVolume.Decilitre);
    /// <summary>Retrieves the magnitude of the <see cref="Volume"/>, expressed in unit <see cref="UnitOfVolume.Litre"/>.</summary>
    public Scalar InLitres => InUnit(UnitOfVolume.Litre);

    /// <summary>Indicates whether the magnitude of the <see cref="Volume"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Volume"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Volume"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Volume"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Volume"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Volume"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Volume"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Volume"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="Volume"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public Volume Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="Volume"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public Volume Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="Volume"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public Volume Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="Volume"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public Volume Round() => new(Math.Round(Magnitude));

    /// <summary>Takes the cube root of the <see cref="Volume"/>, producing a <see cref="Length"/>.</summary>
    public Length CubeRoot() => Length.From(this);

    /// <inheritdoc/>
    public int CompareTo(Volume other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Volume"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [m^3]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Volume"/>, expressed in <see cref="UnitOfVolume"/>
    /// <paramref name="unitOfVolume"/>.</summary>
    /// <param name="unitOfVolume">The <see cref="UnitOfVolume"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfVolume unitOfVolume) => InUnit(this, unitOfVolume);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Volume"/>, expressed in <see cref="UnitOfVolume"/>
    /// <paramref name="unitOfVolume"/>.</summary>
    /// <param name="volume">The <see cref="Volume"/> to be expressed in <see cref="UnitOfVolume"/> <paramref name="unitOfVolume"/>.</param>
    /// <param name="unitOfVolume">The <see cref="UnitOfVolume"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Volume volume, UnitOfVolume unitOfVolume) => new(volume.Magnitude / unitOfVolume.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Volume"/>.</summary>
    public Volume Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Volume"/> with negated magnitude.</summary>
    public Volume Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Volume"/>.</param>
    public static Volume operator +(Volume x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Volume"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Volume"/>.</param>
    public static Volume operator -(Volume x) => x.Negate();

    /// <summary>Multiplies the <see cref="Volume"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Volume"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Volume"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Volume"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Volume"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Volume"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Volume"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Volume x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Volume"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Volume"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Volume"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Volume y) => y.Multiply(x);
    /// <summary>Divides the <see cref="Volume"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Volume"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Volume"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Volume x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Volume"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Volume Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Volume"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Volume"/> is scaled.</param>
    public Volume Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Volume"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Volume"/> is divided.</param>
    public Volume Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Volume"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Volume"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="Volume"/> <paramref name="x"/> by this value.</param>
    public static Volume operator %(Volume x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Volume"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Volume"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Volume"/> <paramref name="x"/>.</param>
    public static Volume operator *(Volume x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Volume"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Volume"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Volume"/>, which is scaled by <paramref name="x"/>.</param>
    public static Volume operator *(double x, Volume y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Volume"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Volume"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Volume"/> <paramref name="x"/>.</param>
    public static Volume operator /(Volume x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="Volume"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Volume Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Volume"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Volume"/> is scaled.</param>
    public Volume Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Volume"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Volume"/> is divided.</param>
    public Volume Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Volume"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Volume"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="Volume"/> <paramref name="x"/> by this value.</param>
    public static Volume operator %(Volume x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Volume"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Volume"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Volume"/> <paramref name="x"/>.</param>
    public static Volume operator *(Volume x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Volume"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Volume"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Volume"/>, which is scaled by <paramref name="x"/>.</param>
    public static Volume operator *(Scalar x, Volume y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Volume"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Volume"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Volume"/> <paramref name="x"/>.</param>
    public static Volume operator /(Volume x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="Volume"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Volume"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Volume"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Volume"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Volume"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Volume"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Volume"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Volume.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Volume x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Volume"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Volume"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Volume"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Volume.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Volume x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Volume x, Volume y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Volume x, Volume y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Volume x, Volume y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Volume x, Volume y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Volume"/> to a <see cref="double"/> with value <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts the <see cref="Volume"/> to a <see cref="double"/> based on the magnitude of the <see cref="Volume"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Volume x) => x.ToDouble();

    /// <summary>Converts the <see cref="Volume"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts the <see cref="Volume"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Volume x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Volume"/> of magnitude <paramref name="x"/>.</summary>
    public static Volume FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Volume"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Volume(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Volume"/> of equivalent magnitude.</summary>
    public static Volume FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Volume"/> of equivalent magnitude.</summary>
    public static explicit operator Volume(Scalar x) => FromScalar(x);
}
