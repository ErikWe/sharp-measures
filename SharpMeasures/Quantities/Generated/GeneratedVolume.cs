namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Volume"/>, used for describing three-dimensional sections of space - volumes.
/// <para>
/// New instances of <see cref="Volume"/> can be constructed according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Volume"/> a = 5 * <see cref="Volume.OneLitre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Volume"/> b = new(7, <see cref="UnitOfVolume.CubicMetre"/>);
/// </code>
/// </item>
/// <item>
/// <code>
/// </code>
/// </item>
/// </list>
/// Instances of <see cref="Volume"/> may be applied according to:
/// <list type="bullet">
/// <item>
/// <code>
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved in a desired unit according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Volume.InCubicMetres"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Volume.InUnit(UnitOfVolume)"/>;
/// </code>
/// </item>
/// </list>
/// </para>
/// </summary>
public readonly partial record struct Volume :
    IComparable<Volume>,
    IScalarQuantity<Volume>,
    ICubeRootableScalarQuantity<Length>,
    IAddableScalarQuantity<Volume, Volume>,
    ISubtractableScalarQuantity<Volume, Volume>,
    IDivisibleScalarQuantity<Scalar, Volume>
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

    /// <summary>Constructs a <see cref="Volume"/> by cubing the <see cref="Length"/> <paramref name="#cubeRootQuantity#"/>.</summary>
    /// <param name="#cubeRootQuantity#">This <see cref="Length"/> is cubed to produce a <see cref="#Quantity"/>.</param>
    public static Volume From(Length length) => new(Math.Pow(length.InMetres, 3));

    /// <summary>The magnitude of the <see cref="Volume"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Volume.InLitres"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Volume"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfVolume"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Volume"/>, in unit <paramref name="unitOfVolume"/>.</param>
    /// <param name="unitOfVolume">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Volume"/> a = 2.6 * <see cref="Volume.OneCubicMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Volume(double magnitude, UnitOfVolume unitOfVolume) : this(magnitude * unitOfVolume.Factor) { }
    /// <summary>Constructs a new <see cref="Volume"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Volume"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfVolume"/> to be specified.</remarks>
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
    public Volume Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public Volume Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public Volume Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public Volume Round() => new(Math.Round(Magnitude));

    /// <summary>Takes the cube root of the <see cref="Volume"/>, producing a <see cref="Length"/>.</summary>
    public Length CubeRoot() => Length.From(this);

    /// <inheritdoc/>
    public int CompareTo(Volume other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Volume"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [m^3]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Volume"/>, expressed in unit <paramref name="unitOfVolume"/>.</summary>
    /// <param name="unitOfVolume">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfVolume unitOfVolume) => InUnit(Magnitude, unitOfVolume);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Volume"/>, expressed in unit <paramref name="unitOfVolume"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="Volume"/>.</param>
    /// <param name="unitOfVolume">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfVolume unitOfVolume) => new(magnitude / unitOfVolume.Factor);

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

    /// <summary>Adds this instance and the <see cref="Volume"/> <paramref name="term"/>, producing another <see cref="Volume"/>.</summary>
    /// <param name="term">This <see cref="Volume"/> is added to this instance.</param>
    public Volume Add(Volume term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="Volume"/> <paramref name="term"/> from this instance, producing another <see cref="Volume"/>.</summary>
    /// <param name="term">This <see cref="Volume"/> is subtracted from this instance.</param>
    public Volume Subtract(Volume term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="Volume"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="Volume"/>.</summary>
    /// <param name="x">This <see cref="Volume"/> is added to the <see cref="Volume"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="Volume"/> is added to the <see cref="Volume"/> <paramref name="x"/>.</param>
    public static Volume operator +(Volume x, Volume y) => x.Add(y);
    /// <summary>Subtract the <see cref="Volume"/> <paramref name="y"/> from the <see cref="Volume"/> <paramref name="x"/>, producing another <see cref="Volume"/>.</summary>
    /// <param name="x">The <see cref="Volume"/> <paramref name="y"/> is subtracted from this <see cref="Volume"/>.</param>
    /// <param name="y">This <see cref="Volume"/> is subtracted from the <see cref="Volume"/> <paramref name="x"/>.</param>
    public static Volume operator -(Volume x, Volume y) => x.Subtract(y);

    /// <summary>Divides this <see cref="Volume"/> by the <see cref="Volume"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Volume"/> is divided by this <see cref="Volume"/>.</param>
    public Scalar Divide(Volume divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="Volume"/> <paramref name="x"/> by the <see cref="Volume"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="Volume"/> is divided by the <see cref="Volume"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Volume"/> <paramref name="x"/> is divided by this <see cref="Volume"/>.</param>
    public static Scalar operator /(Volume x, Volume y) => x.Divide(y)
;

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
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
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
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
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
    public static bool operator <=(Volume x, Volume y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Volume x, Volume y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="Volume"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Volume x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
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