namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Density"/>, used for describing the amount of <see cref="Mass"/> over a <see cref="Volume"/>.
/// <para>
/// New instances of <see cref="Density"/> can be constructed according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Density"/> a = 5 * <see cref="Density.OneKilogramPerCubicMetre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Density"/> b = new(7, <see cref="UnitOfDensity.KilogramPerCubicMetre"/>);
/// </code>
/// </item>
/// <item>
/// <code>
/// </code>
/// </item>
/// </list>
/// Instances of <see cref="Density"/> may be applied according to:
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
/// <see cref="Density.InKilogramsPerCubicMetre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Density.InUnit(UnitOfDensity)"/>;
/// </code>
/// </item>
/// </list>
/// </para>
/// </summary>
public readonly partial record struct Density :
    IComparable<Density>,
    IScalarQuantity<Density>,
    IInvertibleScalarQuantity<SpecificVolume>,
    IAddableScalarQuantity<Density, Density>,
    ISubtractableScalarQuantity<Density, Density>,
    IDivisibleScalarQuantity<Scalar, Density>
{
    /// <summary>The zero-valued <see cref="Density"/>.</summary>
    public static Density Zero { get; } = new(0);

    /// <summary>The <see cref="Density"/> with magnitude 1, when expressed in unit <see cref="UnitOfDensity.KilogramPerCubicMetre"/>.</summary>
    public static Density OneKilogramPerCubicMetre { get; } = new(1, UnitOfDensity.KilogramPerCubicMetre);

    /// <summary>Constructs a <see cref="Density"/> by inverting the <see cref="SpecificVolume"/> <paramref name="specificVolume"/>.</summary>
    /// <param name="specificVolume">This <see cref="SpecificVolume"/> is inverted to produce a <see cref="Density"/>.</param>
    public static Density From(SpecificVolume specificVolume) => new(1 / specificVolume.InCubicMetresPerKilogram);

    /// <summary>The magnitude of the <see cref="Density"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Density.InKilogramsPerCubicMetre"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Density"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfDensity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Density"/>, in unit <paramref name="unitOfDensity"/>.</param>
    /// <param name="unitOfDensity">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Density"/> a = 2.6 * <see cref="Density.OneKilogramPerCubicMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Density(double magnitude, UnitOfDensity unitOfDensity) : this(magnitude * unitOfDensity.Factor) { }
    /// <summary>Constructs a new <see cref="Density"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Density"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfDensity"/> to be specified.</remarks>
    public Density(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Density"/>, expressed in unit <see cref="UnitOfDensity.KilogramPerCubicMetre"/>.</summary>
    public Scalar InKilogramsPerCubicMetre => InUnit(UnitOfDensity.KilogramPerCubicMetre);

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
    public Density Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public Density Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public Density Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public Density Round() => new(Math.Round(Magnitude));

    /// <summary>Inverts the <see cref="Density"/>, producing a <see cref="SpecificVolume"/>.</summary>
    public SpecificVolume Invert() => SpecificVolume.From(this);

    /// <inheritdoc/>
    public int CompareTo(Density other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Density"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [kg * m^-3]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Density"/>, expressed in unit <paramref name="unitOfDensity"/>.</summary>
    /// <param name="unitOfDensity">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfDensity unitOfDensity) => InUnit(Magnitude, unitOfDensity);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Density"/>, expressed in unit <paramref name="unitOfDensity"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="Density"/>.</param>
    /// <param name="unitOfDensity">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfDensity unitOfDensity) => new(magnitude / unitOfDensity.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Density"/>.</summary>
    public Density Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Density"/> with negated magnitude.</summary>
    public Density Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Density"/>.</param>
    public static Density operator +(Density x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Density"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Density"/>.</param>
    public static Density operator -(Density x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="Density"/> <paramref name="term"/>, producing another <see cref="Density"/>.</summary>
    /// <param name="term">This <see cref="Density"/> is added to this instance.</param>
    public Density Add(Density term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="Density"/> <paramref name="term"/> from this instance, producing another <see cref="Density"/>.</summary>
    /// <param name="term">This <see cref="Density"/> is subtracted from this instance.</param>
    public Density Subtract(Density term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="Density"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="Density"/>.</summary>
    /// <param name="x">This <see cref="Density"/> is added to the <see cref="Density"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="Density"/> is added to the <see cref="Density"/> <paramref name="x"/>.</param>
    public static Density operator +(Density x, Density y) => x.Add(y);
    /// <summary>Subtract the <see cref="Density"/> <paramref name="y"/> from the <see cref="Density"/> <paramref name="x"/>, producing another <see cref="Density"/>.</summary>
    /// <param name="x">The <see cref="Density"/> <paramref name="y"/> is subtracted from this <see cref="Density"/>.</param>
    /// <param name="y">This <see cref="Density"/> is subtracted from the <see cref="Density"/> <paramref name="x"/>.</param>
    public static Density operator -(Density x, Density y) => x.Subtract(y);

    /// <summary>Divides this <see cref="Density"/> by the <see cref="Density"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Density"/> is divided by this <see cref="Density"/>.</param>
    public Scalar Divide(Density divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="Density"/> <paramref name="x"/> by the <see cref="Density"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="Density"/> is divided by the <see cref="Density"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Density"/> <paramref name="x"/> is divided by this <see cref="Density"/>.</param>
    public static Scalar operator /(Density x, Density y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="Density"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Density"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Density"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Density"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Density"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Density"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Density"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Density x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Density"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Density"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Density"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Density x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Density"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Density Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Density"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Density"/> is scaled.</param>
    public Density Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Density"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Density"/> is divided.</param>
    public Density Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Density"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Density"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Density operator %(Density x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Density"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Density"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Density"/> <paramref name="x"/>.</param>
    public static Density operator *(Density x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Density"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Density"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Density"/>, which is scaled by <paramref name="x"/>.</param>
    public static Density operator *(double x, Density y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Density"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Density"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Density"/> <paramref name="x"/>.</param>
    public static Density operator /(Density x, double y) => x.Divide(y);
    /// <summary>Inverts the <see cref="Density"/> <paramref name="y"/> to produce a <see cref="SpecificVolume"/>, which is then scaled by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="Density"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Density"/>, which is inverted to a <see cref="SpecificVolume"/> and scaled by <paramref name="x"/>.</param>
    public static SpecificVolume operator /(double x, Density y) => x * y.Invert();

    /// <summary>Produces a <see cref="Density"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Density Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Density"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Density"/> is scaled.</param>
    public Density Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Density"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Density"/> is divided.</param>
    public Density Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Density"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Density"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Density operator %(Density x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Density"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Density"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Density"/> <paramref name="x"/>.</param>
    public static Density operator *(Density x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Density"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Density"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Density"/>, which is scaled by <paramref name="x"/>.</param>
    public static Density operator *(Scalar x, Density y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Density"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Density"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Density"/> <paramref name="x"/>.</param>
    public static Density operator /(Density x, Scalar y) => x.Divide(y);
    /// <summary>Inverts the <see cref="Density"/> <paramref name="y"/> to produce a <see cref="SpecificVolume"/>, which is then scaled by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="Density"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Density"/>, which is inverted to a <see cref="SpecificVolume"/> and scaled by <paramref name="x"/>.</param>
    public static SpecificVolume operator /(Scalar x, Density y) => x * y.Invert();

    /// <summary>Multiplies the <see cref="Density"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Density"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Density"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Density"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Density"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Density"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Density"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Density.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Density x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Density"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Density"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Density"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Density.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Density x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Density x, Density y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Density x, Density y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Density x, Density y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Density x, Density y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="Density"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Density x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Density x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Density"/> of magnitude <paramref name="x"/>.</summary>
    public static Density FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Density"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Density(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Density"/> of equivalent magnitude.</summary>
    public static Density FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Density"/> of equivalent magnitude.</summary>
    public static explicit operator Density(Scalar x) => FromScalar(x);
}