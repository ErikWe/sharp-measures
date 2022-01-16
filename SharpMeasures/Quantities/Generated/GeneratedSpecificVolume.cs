namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="SpecificVolume"/>, used for describing the <see cref="Volume"/> required for an amount of <see cref="Mass"/>.
/// This is the inverse of <see cref="Density"/>.
/// <para>
/// New instances of <see cref="SpecificVolume"/> can be constructed according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="SpecificVolume"/> a = 5 * <see cref="SpecificVolume.OneCubicMetrePerKilogram"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="SpecificVolume"/> b = new(7, <see cref="UnitOfSpecificVolume.CubicMetrePerKilogram"/>);
/// </code>
/// </item>
/// </list>
/// Instances of <see cref="SpecificVolume"/> may be applied according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Volume"/> c = 3 * <see cref="SpecificVolume.OneCubicMetrePerKilogram"/> * <see cref="Mass.OneOunce"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved in a desired unit according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="SpecificVolume.InCubicMetresPerKilogram"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="SpecificVolume.InUnit(UnitOfSpecificVolume)"/>;
/// </code>
/// </item>
/// </list>
/// </para>
/// </summary>
public readonly partial record struct SpecificVolume :
    IComparable<SpecificVolume>,
    IScalarQuantity<SpecificVolume>,
    IInvertibleScalarQuantity<Density>,
    IAddableScalarQuantity<SpecificVolume, SpecificVolume>,
    ISubtractableScalarQuantity<SpecificVolume, SpecificVolume>,
    IDivisibleScalarQuantity<Scalar, SpecificVolume>
{
    /// <summary>The zero-valued <see cref="SpecificVolume"/>.</summary>
    public static SpecificVolume Zero { get; } = new(0);

    /// <summary>The <see cref="SpecificVolume"/> with magnitude 1, when expressed in unit <see cref="UnitOfSpecificVolume.CubicMetrePerKilogram"/>.</summary>
    public static SpecificVolume OneCubicMetrePerKilogram { get; } = new(1, UnitOfSpecificVolume.CubicMetrePerKilogram);

    /// <summary>Constructs a <see cref="SpecificVolume"/> by inverting the <see cref="Density"/> <paramref name="density"/>.</summary>
    /// <param name="density">This <see cref="Density"/> is inverted to produce a <see cref="SpecificVolume"/>.</param>
    public static SpecificVolume From(Density density) => new(1 / density.InKilogramsPerCubicMetre);

    /// <summary>The magnitude of the <see cref="SpecificVolume"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="SpecificVolume.InCubicMetresPerKilogram"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="SpecificVolume"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfSpecificVolume"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpecificVolume"/>, in unit <paramref name="unitOfSpecificVolume"/>.</param>
    /// <param name="unitOfSpecificVolume">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SpecificVolume"/> a = 2.6 * <see cref="SpecificVolume.OneCubicMetrePerKilogram"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SpecificVolume(double magnitude, UnitOfSpecificVolume unitOfSpecificVolume) : this(magnitude * unitOfSpecificVolume.Factor) { }
    /// <summary>Constructs a new <see cref="SpecificVolume"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpecificVolume"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfSpecificVolume"/> to be specified.</remarks>
    public SpecificVolume(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="SpecificVolume"/>, expressed in unit <see cref="UnitOfSpecificVolume.CubicMetrePerKilogram"/>.</summary>
    public Scalar InCubicMetresPerKilogram => InUnit(UnitOfSpecificVolume.CubicMetrePerKilogram);

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
    public SpecificVolume Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public SpecificVolume Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public SpecificVolume Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public SpecificVolume Round() => new(Math.Round(Magnitude));

    /// <summary>Inverts the <see cref="SpecificVolume"/>, producing a <see cref="Density"/>.</summary>
    public Density Invert() => Density.From(this);

    /// <inheritdoc/>
    public int CompareTo(SpecificVolume other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="SpecificVolume"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [kg^-1 * m^3]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="SpecificVolume"/>, expressed in unit <paramref name="unitOfSpecificVolume"/>.</summary>
    /// <param name="unitOfSpecificVolume">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfSpecificVolume unitOfSpecificVolume) => InUnit(Magnitude, unitOfSpecificVolume);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="SpecificVolume"/>, expressed in unit <paramref name="unitOfSpecificVolume"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="SpecificVolume"/>.</param>
    /// <param name="unitOfSpecificVolume">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfSpecificVolume unitOfSpecificVolume) => new(magnitude / unitOfSpecificVolume.Factor);

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

    /// <summary>Adds this instance and the <see cref="SpecificVolume"/> <paramref name="term"/>, producing another <see cref="SpecificVolume"/>.</summary>
    /// <param name="term">This <see cref="SpecificVolume"/> is added to this instance.</param>
    public SpecificVolume Add(SpecificVolume term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="SpecificVolume"/> <paramref name="term"/> from this instance, producing another <see cref="SpecificVolume"/>.</summary>
    /// <param name="term">This <see cref="SpecificVolume"/> is subtracted from this instance.</param>
    public SpecificVolume Subtract(SpecificVolume term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="SpecificVolume"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="SpecificVolume"/>.</summary>
    /// <param name="x">This <see cref="SpecificVolume"/> is added to the <see cref="SpecificVolume"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="SpecificVolume"/> is added to the <see cref="SpecificVolume"/> <paramref name="x"/>.</param>
    public static SpecificVolume operator +(SpecificVolume x, SpecificVolume y) => x.Add(y);
    /// <summary>Subtract the <see cref="SpecificVolume"/> <paramref name="y"/> from the <see cref="SpecificVolume"/> <paramref name="x"/>, producing another <see cref="SpecificVolume"/>.</summary>
    /// <param name="x">The <see cref="SpecificVolume"/> <paramref name="y"/> is subtracted from this <see cref="SpecificVolume"/>.</param>
    /// <param name="y">This <see cref="SpecificVolume"/> is subtracted from the <see cref="SpecificVolume"/> <paramref name="x"/>.</param>
    public static SpecificVolume operator -(SpecificVolume x, SpecificVolume y) => x.Subtract(y);

    /// <summary>Divides this <see cref="SpecificVolume"/> by the <see cref="SpecificVolume"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="SpecificVolume"/> is divided by this <see cref="SpecificVolume"/>.</param>
    public Scalar Divide(SpecificVolume divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="SpecificVolume"/> <paramref name="x"/> by the <see cref="SpecificVolume"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="SpecificVolume"/> is divided by the <see cref="SpecificVolume"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpecificVolume"/> <paramref name="x"/> is divided by this <see cref="SpecificVolume"/>.</param>
    public static Scalar operator /(SpecificVolume x, SpecificVolume y) => x.Divide(y)
;

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
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
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
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
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
    public static bool operator <=(SpecificVolume x, SpecificVolume y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(SpecificVolume x, SpecificVolume y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="SpecificVolume"/> <paramref name="x"/>.</summary>
    public static implicit operator double(SpecificVolume x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
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