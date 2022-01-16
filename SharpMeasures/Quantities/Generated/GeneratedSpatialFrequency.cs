namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="SpatialFrequency"/>, used for describing spatial periodicity.
/// <see cref="SpatialFrequency"/> is related to <see cref="Length"/> similarly to how <see cref="Frequency"/> is related to <see cref="Time"/>.
/// <para>
/// New instances of <see cref="SpatialFrequency"/> can be constructed according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="SpatialFrequency"/> a = 5 * <see cref="SpatialFrequency.OnePerMetre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="SpatialFrequency"/> b = new(7, <see cref="UnitOfSpatialFrequency.PerMetre"/>);
/// </code>
/// </item>
/// <item>
/// <code>
/// </code>
/// </item>
/// </list>
/// Instances of <see cref="SpatialFrequency"/> may be applied according to:
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
/// <see cref="SpatialFrequency.InPerMetre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="SpatialFrequency.InUnit(UnitOfSpatialFrequency)"/>;
/// </code>
/// </item>
/// </list>
/// </para>
/// </summary>
public readonly partial record struct SpatialFrequency :
    IComparable<SpatialFrequency>,
    IScalarQuantity<SpatialFrequency>,
    IInvertibleScalarQuantity<Length>,
    IAddableScalarQuantity<SpatialFrequency, SpatialFrequency>,
    ISubtractableScalarQuantity<SpatialFrequency, SpatialFrequency>,
    IDivisibleScalarQuantity<Scalar, SpatialFrequency>
{
    /// <summary>The zero-valued <see cref="SpatialFrequency"/>.</summary>
    public static SpatialFrequency Zero { get; } = new(0);

    /// <summary>The <see cref="SpatialFrequency"/> with magnitude 1, when expressed in unit <see cref="UnitOfSpatialFrequency.PerMetre"/>.</summary>
    public static SpatialFrequency OnePerMetre { get; } = new(1, UnitOfSpatialFrequency.PerMetre);

    /// <summary>Constructs a <see cref="SpatialFrequency"/> by inverting the <see cref="Length"/> <paramref name="length"/>.</summary>
    /// <param name="length">This <see cref="Length"/> is inverted to produce a <see cref="SpatialFrequency"/>.</param>
    public static SpatialFrequency From(Length length) => new(1 / length.InMetres);

    /// <summary>The magnitude of the <see cref="SpatialFrequency"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="SpatialFrequency.InPerMetre"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="SpatialFrequency"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfSpatialFrequency"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpatialFrequency"/>, in unit <paramref name="unitOfSpatialFrequency"/>.</param>
    /// <param name="unitOfSpatialFrequency">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SpatialFrequency"/> a = 2.6 * <see cref="SpatialFrequency.OnePerMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SpatialFrequency(double magnitude, UnitOfSpatialFrequency unitOfSpatialFrequency) : this(magnitude * unitOfSpatialFrequency.Factor) { }
    /// <summary>Constructs a new <see cref="SpatialFrequency"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpatialFrequency"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfSpatialFrequency"/> to be specified.</remarks>
    public SpatialFrequency(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="SpatialFrequency"/>, expressed in unit <see cref="UnitOfSpatialFrequency.PerMetre"/>.</summary>
    public Scalar InPerMetre => InUnit(UnitOfSpatialFrequency.PerMetre);

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
    public SpatialFrequency Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public SpatialFrequency Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public SpatialFrequency Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public SpatialFrequency Round() => new(Math.Round(Magnitude));

    /// <summary>Inverts the <see cref="SpatialFrequency"/>, producing a <see cref="Length"/>.</summary>
    public Length Invert() => Length.From(this);

    /// <inheritdoc/>
    public int CompareTo(SpatialFrequency other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="SpatialFrequency"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [m^-1]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="SpatialFrequency"/>, expressed in unit <paramref name="unitOfSpatialFrequency"/>.</summary>
    /// <param name="unitOfSpatialFrequency">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfSpatialFrequency unitOfSpatialFrequency) => InUnit(Magnitude, unitOfSpatialFrequency);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="SpatialFrequency"/>, expressed in unit <paramref name="unitOfSpatialFrequency"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="SpatialFrequency"/>.</param>
    /// <param name="unitOfSpatialFrequency">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfSpatialFrequency unitOfSpatialFrequency) => new(magnitude / unitOfSpatialFrequency.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="SpatialFrequency"/>.</summary>
    public SpatialFrequency Plus() => this;
    /// <summary>Negation, resulting in a <see cref="SpatialFrequency"/> with negated magnitude.</summary>
    public SpatialFrequency Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="SpatialFrequency"/>.</param>
    public static SpatialFrequency operator +(SpatialFrequency x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="SpatialFrequency"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="SpatialFrequency"/>.</param>
    public static SpatialFrequency operator -(SpatialFrequency x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="SpatialFrequency"/> <paramref name="term"/>, producing another <see cref="SpatialFrequency"/>.</summary>
    /// <param name="term">This <see cref="SpatialFrequency"/> is added to this instance.</param>
    public SpatialFrequency Add(SpatialFrequency term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="SpatialFrequency"/> <paramref name="term"/> from this instance, producing another <see cref="SpatialFrequency"/>.</summary>
    /// <param name="term">This <see cref="SpatialFrequency"/> is subtracted from this instance.</param>
    public SpatialFrequency Subtract(SpatialFrequency term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="SpatialFrequency"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="SpatialFrequency"/>.</summary>
    /// <param name="x">This <see cref="SpatialFrequency"/> is added to the <see cref="SpatialFrequency"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="SpatialFrequency"/> is added to the <see cref="SpatialFrequency"/> <paramref name="x"/>.</param>
    public static SpatialFrequency operator +(SpatialFrequency x, SpatialFrequency y) => x.Add(y);
    /// <summary>Subtract the <see cref="SpatialFrequency"/> <paramref name="y"/> from the <see cref="SpatialFrequency"/> <paramref name="x"/>, producing another <see cref="SpatialFrequency"/>.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/> <paramref name="y"/> is subtracted from this <see cref="SpatialFrequency"/>.</param>
    /// <param name="y">This <see cref="SpatialFrequency"/> is subtracted from the <see cref="SpatialFrequency"/> <paramref name="x"/>.</param>
    public static SpatialFrequency operator -(SpatialFrequency x, SpatialFrequency y) => x.Subtract(y);

    /// <summary>Divides this <see cref="SpatialFrequency"/> by the <see cref="SpatialFrequency"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="SpatialFrequency"/> is divided by this <see cref="SpatialFrequency"/>.</param>
    public Scalar Divide(SpatialFrequency divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="SpatialFrequency"/> <paramref name="x"/> by the <see cref="SpatialFrequency"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="SpatialFrequency"/> is divided by the <see cref="SpatialFrequency"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpatialFrequency"/> <paramref name="x"/> is divided by this <see cref="SpatialFrequency"/>.</param>
    public static Scalar operator /(SpatialFrequency x, SpatialFrequency y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="SpatialFrequency"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="SpatialFrequency"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="SpatialFrequency"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="SpatialFrequency"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="SpatialFrequency"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SpatialFrequency"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(SpatialFrequency x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="SpatialFrequency"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SpatialFrequency"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(SpatialFrequency x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="SpatialFrequency"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public SpatialFrequency Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="SpatialFrequency"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SpatialFrequency"/> is scaled.</param>
    public SpatialFrequency Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="SpatialFrequency"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SpatialFrequency"/> is divided.</param>
    public SpatialFrequency Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="SpatialFrequency"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static SpatialFrequency operator %(SpatialFrequency x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SpatialFrequency"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SpatialFrequency"/> <paramref name="x"/>.</param>
    public static SpatialFrequency operator *(SpatialFrequency x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SpatialFrequency"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SpatialFrequency"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpatialFrequency"/>, which is scaled by <paramref name="x"/>.</param>
    public static SpatialFrequency operator *(double x, SpatialFrequency y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SpatialFrequency"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SpatialFrequency"/> <paramref name="x"/>.</param>
    public static SpatialFrequency operator /(SpatialFrequency x, double y) => x.Divide(y);
    /// <summary>Inverts the <see cref="SpatialFrequency"/> <paramref name="y"/> to produce a <see cref="Length"/>, which is then scaled by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="SpatialFrequency"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpatialFrequency"/>, which is inverted to a <see cref="Length"/> and scaled by <paramref name="x"/>.</param>
    public static Length operator /(double x, SpatialFrequency y) => x * y.Invert();

    /// <summary>Produces a <see cref="SpatialFrequency"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public SpatialFrequency Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="SpatialFrequency"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SpatialFrequency"/> is scaled.</param>
    public SpatialFrequency Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="SpatialFrequency"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SpatialFrequency"/> is divided.</param>
    public SpatialFrequency Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="SpatialFrequency"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static SpatialFrequency operator %(SpatialFrequency x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SpatialFrequency"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SpatialFrequency"/> <paramref name="x"/>.</param>
    public static SpatialFrequency operator *(SpatialFrequency x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SpatialFrequency"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SpatialFrequency"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpatialFrequency"/>, which is scaled by <paramref name="x"/>.</param>
    public static SpatialFrequency operator *(Scalar x, SpatialFrequency y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SpatialFrequency"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SpatialFrequency"/> <paramref name="x"/>.</param>
    public static SpatialFrequency operator /(SpatialFrequency x, Scalar y) => x.Divide(y);
    /// <summary>Inverts the <see cref="SpatialFrequency"/> <paramref name="y"/> to produce a <see cref="Length"/>, which is then scaled by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="SpatialFrequency"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpatialFrequency"/>, which is inverted to a <see cref="Length"/> and scaled by <paramref name="x"/>.</param>
    public static Length operator /(Scalar x, SpatialFrequency y) => x * y.Invert();

    /// <summary>Multiplies the <see cref="SpatialFrequency"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="SpatialFrequency"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="SpatialFrequency"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="SpatialFrequency"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="SpatialFrequency"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="SpatialFrequency"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="SpatialFrequency.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(SpatialFrequency x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="SpatialFrequency"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="SpatialFrequency"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="SpatialFrequency.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(SpatialFrequency x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(SpatialFrequency x, SpatialFrequency y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(SpatialFrequency x, SpatialFrequency y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(SpatialFrequency x, SpatialFrequency y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(SpatialFrequency x, SpatialFrequency y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="SpatialFrequency"/> <paramref name="x"/>.</summary>
    public static implicit operator double(SpatialFrequency x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(SpatialFrequency x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="SpatialFrequency"/> of magnitude <paramref name="x"/>.</summary>
    public static SpatialFrequency FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="SpatialFrequency"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator SpatialFrequency(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="SpatialFrequency"/> of equivalent magnitude.</summary>
    public static SpatialFrequency FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="SpatialFrequency"/> of equivalent magnitude.</summary>
    public static explicit operator SpatialFrequency(Scalar x) => FromScalar(x);
}