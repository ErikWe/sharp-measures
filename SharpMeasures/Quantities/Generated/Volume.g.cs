#nullable enable

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
/// The magnitude of the <see cref="Volume"/> can be retrieved in the desired <see cref="UnitOfVolume"/> using pre-defined properties,
/// such as <see cref="Litres"/>.
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

    /// <summary>The <see cref="Volume"/> with magnitude 1, when expressed in unit <see cref="UnitOfVolume.CubicMetre"/>.</summary>
    public static Volume OneCubicMetre { get; } = UnitOfVolume.CubicMetre.Volume;
    /// <summary>The <see cref="Volume"/> with magnitude 1, when expressed in unit <see cref="UnitOfVolume.CubicDecimetre"/>.</summary>
    public static Volume OneCubicDecimetre { get; } = UnitOfVolume.CubicDecimetre.Volume;
    /// <summary>The <see cref="Volume"/> with magnitude 1, when expressed in unit <see cref="UnitOfVolume.Litre"/>.</summary>
    public static Volume OneLitre { get; } = UnitOfVolume.Litre.Volume;
    /// <summary>The <see cref="Volume"/> with magnitude 1, when expressed in unit <see cref="UnitOfVolume.Millilitre"/>.</summary>
    public static Volume OneMillilitre { get; } = UnitOfVolume.Millilitre.Volume;
    /// <summary>The <see cref="Volume"/> with magnitude 1, when expressed in unit <see cref="UnitOfVolume.Centilitre"/>.</summary>
    public static Volume OneCentilitre { get; } = UnitOfVolume.Centilitre.Volume;
    /// <summary>The <see cref="Volume"/> with magnitude 1, when expressed in unit <see cref="UnitOfVolume.Decilitre"/>.</summary>
    public static Volume OneDecilitre { get; } = UnitOfVolume.Decilitre.Volume;

    /// <summary>Computes <see cref="Volume"/> according to { <paramref name="length"/>³ }.</summary>
    /// <param name="length">This <see cref="Length"/> is cubed to produce a <see cref="Volume"/>.</param>
    public static Volume From(Length length) => new(Math.Pow(length.Magnitude, 3));
    /// <summary>Computes <see cref="Volume"/> according to { <paramref name="distance"/>³ }.</summary>
    /// <param name="distance">This <see cref="Distance"/> is cubed to produce a <see cref="Volume"/>.</param>
    public static Volume From(Distance distance) => new(Math.Pow(distance.Magnitude, 3));
    /// <summary>Computes <see cref="Volume"/> according to { <paramref name="length1"/> ∙ <paramref name="length2"/>
    /// ∙ <paramref name="length3"/> }.</summary>
    /// <param name="length1">This <see cref="Length"/> is multiplied by <paramref name="length2"/> and
    /// <paramref name="length3"/> to produce a <see cref="Volume"/>.</param>
    /// <param name="length2">This <see cref="Length"/> is multiplied by <paramref name="length1"/> and
    /// <paramref name="length3"/> to produce a <see cref="Volume"/>.</param>
    /// <param name="length3">This <see cref="Length"/> is multiplied by <paramref name="length1"/> and
    /// <paramref name="length2"/> to produce a <see cref="Volume"/>.</param>
    public static Volume From(Length length1, Length length2, Length length3) => new(length1.Magnitude * length2.Magnitude * length3.Magnitude);
    /// <summary>Computes <see cref="Volume"/> according to { <paramref name="distance1"/> ∙ <paramref name="distance2"/>
    /// ∙ <paramref name="distance3"/> }.</summary>
    /// <param name="distance1">This <see cref="Distance"/> is multiplied by <paramref name="distance2"/> and
    /// <paramref name="distance3"/> to produce a <see cref="Volume"/>.</param>
    /// <param name="distance2">This <see cref="Distance"/> is multiplied by <paramref name="distance1"/> and
    /// <paramref name="distance3"/> to produce a <see cref="Volume"/>.</param>
    /// <param name="distance3">This <see cref="Distance"/> is multiplied by <paramref name="distance1"/> and
    /// <paramref name="distance2"/> to produce a <see cref="Volume"/>.</param>
    public static Volume From(Distance distance1, Distance distance2, Distance distance3) => new(distance1.Magnitude * distance2.Magnitude * distance3.Magnitude);

    /// <summary>The magnitude of the <see cref="Volume"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfVolume)"/> or a pre-defined property
    /// - such as <see cref="CubicMetres"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Volume"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfVolume"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Volume"/>, expressed in <paramref name="unitOfVolume"/>.</param>
    /// <param name="unitOfVolume">The <see cref="UnitOfVolume"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Volume"/> a = 3 * <see cref="Volume.OneCubicMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Volume(Scalar magnitude, UnitOfVolume unitOfVolume) : this(magnitude.Magnitude, unitOfVolume) { }
    /// <summary>Constructs a new <see cref="Volume"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfVolume"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Volume"/>, expressed in <paramref name="unitOfVolume"/>.</param>
    /// <param name="unitOfVolume">The <see cref="UnitOfVolume"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Volume"/> a = 3 * <see cref="Volume.OneCubicMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Volume(double magnitude, UnitOfVolume unitOfVolume) : this(magnitude * unitOfVolume.Volume.Magnitude) { }
    /// <summary>Constructs a new <see cref="Volume"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Volume"/>.</param>
    /// <remarks>Consider preferring <see cref="Volume(Scalar, UnitOfVolume)"/>.</remarks>
    public Volume(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Volume"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Volume"/>.</param>
    /// <remarks>Consider preferring <see cref="Volume(double, UnitOfVolume)"/>.</remarks>
    public Volume(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Volume"/>, expressed in <see cref="UnitOfVolume.CubicMetre"/>.</summary>
    public Scalar CubicMetres => InUnit(UnitOfVolume.CubicMetre);
    /// <summary>Retrieves the magnitude of the <see cref="Volume"/>, expressed in <see cref="UnitOfVolume.CubicDecimetre"/>.</summary>
    public Scalar CubicDecimetres => InUnit(UnitOfVolume.CubicDecimetre);
    /// <summary>Retrieves the magnitude of the <see cref="Volume"/>, expressed in <see cref="UnitOfVolume.Litre"/>.</summary>
    public Scalar Litres => InUnit(UnitOfVolume.Litre);
    /// <summary>Retrieves the magnitude of the <see cref="Volume"/>, expressed in <see cref="UnitOfVolume.Millilitre"/>.</summary>
    public Scalar Millilitres => InUnit(UnitOfVolume.Millilitre);
    /// <summary>Retrieves the magnitude of the <see cref="Volume"/>, expressed in <see cref="UnitOfVolume.Centilitre"/>.</summary>
    public Scalar Centilitres => InUnit(UnitOfVolume.Centilitre);
    /// <summary>Retrieves the magnitude of the <see cref="Volume"/>, expressed in <see cref="UnitOfVolume.Decilitre"/>.</summary>
    public Scalar Decilitres => InUnit(UnitOfVolume.Decilitre);

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

    /// <summary>Computes the absolute of the <see cref="Volume"/>.</summary>
    public Volume Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="Volume"/>.</summary>
    public Volume Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="Volume"/>.</summary>
    public Volume Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="Volume"/> to the nearest integer value.</summary>
    public Volume Round() => new(Math.Round(Magnitude));

    /// <summary>Computes the cube root of the <see cref="Volume"/>, producing a <see cref="Length"/>.</summary>
    public Length CubeRoot() => Length.From(this);

    /// <inheritdoc/>
    public int CompareTo(Volume other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Volume"/> in the default unit
    /// <see cref="UnitOfVolume.CubicMetre"/>, followed by the symbol [m³].</summary>
    public override string ToString() => $"{CubicMetres} [m³]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Volume"/>,
    /// expressed in <paramref name="unitOfVolume"/>.</summary>
    /// <param name="unitOfVolume">The <see cref="UnitOfVolume"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfVolume unitOfVolume) => InUnit(this, unitOfVolume);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Volume"/>,
    /// expressed in <paramref name="unitOfVolume"/>.</summary>
    /// <param name="volume">The <see cref="Volume"/> to be expressed in <paramref name="unitOfVolume"/>.</param>
    /// <param name="unitOfVolume">The <see cref="UnitOfVolume"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Volume volume, UnitOfVolume unitOfVolume) => new(volume.Magnitude / unitOfVolume.Volume.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Volume"/>.</summary>
    public Volume Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Volume"/> with negated magnitude.</summary>
    public Volume Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Volume"/>.</param>
    public static Volume operator +(Volume x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Volume"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Volume"/>.</param>
    public static Volume operator -(Volume x) => x.Negate();

    /// <summary>Multiplicates the <see cref="Volume"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Volume"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Volume"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Volume"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Volume"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Volume"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Volume"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Volume x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Volume"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Volume"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Volume"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Volume y) => y.Multiply(x);
    /// <summary>Division of the <see cref="Volume"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Volume"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Volume"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Volume x, Unhandled y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="Volume"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Volume Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Volume"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Volume"/> is scaled.</param>
    public Volume Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Volume"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Volume"/> is divided.</param>
    public Volume Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Volume"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Volume"/> <paramref name="x"/> by this value.</param>
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

    /// <summary>Computes the remainder from division of the <see cref="Volume"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Volume Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Volume"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Volume"/> is scaled.</param>
    public Volume Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Volume"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Volume"/> is divided.</param>
    public Volume Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Volume"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Volume"/> <paramref name="x"/> by this value.</param>
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

    /// <summary>Multiplication of the <see cref="Volume"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Volume"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Volume"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Volume x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="Volume"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Volume"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Volume"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Volume x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Volume"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Volume"/>.</param>
    public static bool operator <(Volume x, Volume y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Volume"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Volume"/>.</param>
    public static bool operator >(Volume x, Volume y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Volume"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Volume"/>.</param>
    public static bool operator <=(Volume x, Volume y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Volume"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Volume"/>.</param>
    public static bool operator >=(Volume x, Volume y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Volume"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(Volume x) => x.ToDouble();

    /// <summary>Converts the <see cref="Volume"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Volume x) => x.ToScalar();

    /// <summary>Constructs the <see cref="Volume"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Volume FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Volume"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Volume(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="Volume"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static Volume FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="Volume"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator Volume(Scalar x) => FromScalar(x);
}
