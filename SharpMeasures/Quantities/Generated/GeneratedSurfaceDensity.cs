namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="SurfaceDensity"/>, used for describing the amount of <see cref="Mass"/> over an <see cref="Area"/>.
/// <para>
/// New instances of <see cref="SurfaceDensity"/> can be constructed according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="SurfaceDensity"/> a = 5 * <see cref="SurfaceDensity.OneKilogramPerSquareMetre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="SurfaceDensity"/> b = new(7, <see cref="UnitOfSurfaceDensity.KilogramPerSquareMetre"/>);
/// </code>
/// </item>
/// </list>
/// Instances of <see cref="SurfaceDensity"/> may be applied according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Mass"/> c = 3 * <see cref="SurfaceDensity.OneKilogramPerSquareMetre"/> * <see cref="Area.OneHectare"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved in a desired unit according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="SurfaceDensity.InKilogramsPerSquareMetre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="SurfaceDensity.InUnit(UnitOfSurfaceDensity)"/>;
/// </code>
/// </item>
/// </list>
/// </para>
/// </summary>
public readonly partial record struct SurfaceDensity :
    IComparable<SurfaceDensity>,
    IScalarQuantity<SurfaceDensity>,
    IAddableScalarQuantity<SurfaceDensity, SurfaceDensity>,
    ISubtractableScalarQuantity<SurfaceDensity, SurfaceDensity>,
    IDivisibleScalarQuantity<Scalar, SurfaceDensity>
{
    /// <summary>The zero-valued <see cref="SurfaceDensity"/>.</summary>
    public static SurfaceDensity Zero { get; } = new(0);

    /// <summary>The <see cref="SurfaceDensity"/> with magnitude 1, when expressed in unit <see cref="UnitOfSurfaceDensity.KilogramPerSquareMetre"/>.</summary>
    public static SurfaceDensity OneKilogramPerSquareMetre { get; } = new(1, UnitOfSurfaceDensity.KilogramPerSquareMetre);

    /// <summary>The magnitude of the <see cref="SurfaceDensity"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="SurfaceDensity.InKilogramsPerSquareMetre"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="SurfaceDensity"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfSurfaceDensity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SurfaceDensity"/>, in unit <paramref name="unitOfSurfaceDensity"/>.</param>
    /// <param name="unitOfSurfaceDensity">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SurfaceDensity"/> a = 2.6 * <see cref="SurfaceDensity.OneKilogramPerSquareMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SurfaceDensity(double magnitude, UnitOfSurfaceDensity unitOfSurfaceDensity) : this(magnitude * unitOfSurfaceDensity.Factor) { }
    /// <summary>Constructs a new <see cref="SurfaceDensity"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SurfaceDensity"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfSurfaceDensity"/> to be specified.</remarks>
    public SurfaceDensity(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="SurfaceDensity"/>, expressed in unit <see cref="UnitOfSurfaceDensity.KilogramPerSquareMetre"/>.</summary>
    public Scalar InKilogramsPerSquareMetre => InUnit(UnitOfSurfaceDensity.KilogramPerSquareMetre);

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
    public SurfaceDensity Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public SurfaceDensity Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public SurfaceDensity Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public SurfaceDensity Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(SurfaceDensity other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="SurfaceDensity"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [kg * m^-2]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="SurfaceDensity"/>, expressed in unit <paramref name="unitOfSurfaceDensity"/>.</summary>
    /// <param name="unitOfSurfaceDensity">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfSurfaceDensity unitOfSurfaceDensity) => InUnit(Magnitude, unitOfSurfaceDensity);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="SurfaceDensity"/>, expressed in unit <paramref name="unitOfSurfaceDensity"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="SurfaceDensity"/>.</param>
    /// <param name="unitOfSurfaceDensity">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfSurfaceDensity unitOfSurfaceDensity) => new(magnitude / unitOfSurfaceDensity.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="SurfaceDensity"/>.</summary>
    public SurfaceDensity Plus() => this;
    /// <summary>Negation, resulting in a <see cref="SurfaceDensity"/> with negated magnitude.</summary>
    public SurfaceDensity Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="SurfaceDensity"/>.</param>
    public static SurfaceDensity operator +(SurfaceDensity x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="SurfaceDensity"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="SurfaceDensity"/>.</param>
    public static SurfaceDensity operator -(SurfaceDensity x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="SurfaceDensity"/> <paramref name="term"/>, producing another <see cref="SurfaceDensity"/>.</summary>
    /// <param name="term">This <see cref="SurfaceDensity"/> is added to this instance.</param>
    public SurfaceDensity Add(SurfaceDensity term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="SurfaceDensity"/> <paramref name="term"/> from this instance, producing another <see cref="SurfaceDensity"/>.</summary>
    /// <param name="term">This <see cref="SurfaceDensity"/> is subtracted from this instance.</param>
    public SurfaceDensity Subtract(SurfaceDensity term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="SurfaceDensity"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="SurfaceDensity"/>.</summary>
    /// <param name="x">This <see cref="SurfaceDensity"/> is added to the <see cref="SurfaceDensity"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="SurfaceDensity"/> is added to the <see cref="SurfaceDensity"/> <paramref name="x"/>.</param>
    public static SurfaceDensity operator +(SurfaceDensity x, SurfaceDensity y) => x.Add(y);
    /// <summary>Subtract the <see cref="SurfaceDensity"/> <paramref name="y"/> from the <see cref="SurfaceDensity"/> <paramref name="x"/>, producing another <see cref="SurfaceDensity"/>.</summary>
    /// <param name="x">The <see cref="SurfaceDensity"/> <paramref name="y"/> is subtracted from this <see cref="SurfaceDensity"/>.</param>
    /// <param name="y">This <see cref="SurfaceDensity"/> is subtracted from the <see cref="SurfaceDensity"/> <paramref name="x"/>.</param>
    public static SurfaceDensity operator -(SurfaceDensity x, SurfaceDensity y) => x.Subtract(y);

    /// <summary>Divides this <see cref="SurfaceDensity"/> by the <see cref="SurfaceDensity"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="SurfaceDensity"/> is divided by this <see cref="SurfaceDensity"/>.</param>
    public Scalar Divide(SurfaceDensity divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="SurfaceDensity"/> <paramref name="x"/> by the <see cref="SurfaceDensity"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="SurfaceDensity"/> is divided by the <see cref="SurfaceDensity"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SurfaceDensity"/> <paramref name="x"/> is divided by this <see cref="SurfaceDensity"/>.</param>
    public static Scalar operator /(SurfaceDensity x, SurfaceDensity y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="SurfaceDensity"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="SurfaceDensity"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="SurfaceDensity"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="SurfaceDensity"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="SurfaceDensity"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SurfaceDensity"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SurfaceDensity"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(SurfaceDensity x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="SurfaceDensity"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SurfaceDensity"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SurfaceDensity"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(SurfaceDensity x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="SurfaceDensity"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public SurfaceDensity Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="SurfaceDensity"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SurfaceDensity"/> is scaled.</param>
    public SurfaceDensity Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="SurfaceDensity"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SurfaceDensity"/> is divided.</param>
    public SurfaceDensity Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="SurfaceDensity"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SurfaceDensity"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static SurfaceDensity operator %(SurfaceDensity x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SurfaceDensity"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SurfaceDensity"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SurfaceDensity"/> <paramref name="x"/>.</param>
    public static SurfaceDensity operator *(SurfaceDensity x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SurfaceDensity"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SurfaceDensity"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SurfaceDensity"/>, which is scaled by <paramref name="x"/>.</param>
    public static SurfaceDensity operator *(double x, SurfaceDensity y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SurfaceDensity"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SurfaceDensity"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SurfaceDensity"/> <paramref name="x"/>.</param>
    public static SurfaceDensity operator /(SurfaceDensity x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="SurfaceDensity"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public SurfaceDensity Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="SurfaceDensity"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SurfaceDensity"/> is scaled.</param>
    public SurfaceDensity Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="SurfaceDensity"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SurfaceDensity"/> is divided.</param>
    public SurfaceDensity Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="SurfaceDensity"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SurfaceDensity"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static SurfaceDensity operator %(SurfaceDensity x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SurfaceDensity"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SurfaceDensity"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SurfaceDensity"/> <paramref name="x"/>.</param>
    public static SurfaceDensity operator *(SurfaceDensity x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SurfaceDensity"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SurfaceDensity"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SurfaceDensity"/>, which is scaled by <paramref name="x"/>.</param>
    public static SurfaceDensity operator *(Scalar x, SurfaceDensity y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SurfaceDensity"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SurfaceDensity"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SurfaceDensity"/> <paramref name="x"/>.</param>
    public static SurfaceDensity operator /(SurfaceDensity x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="SurfaceDensity"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="SurfaceDensity"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="SurfaceDensity"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="SurfaceDensity"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="SurfaceDensity"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SurfaceDensity"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="SurfaceDensity"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="SurfaceDensity.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(SurfaceDensity x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="SurfaceDensity"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SurfaceDensity"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="SurfaceDensity"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="SurfaceDensity.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(SurfaceDensity x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(SurfaceDensity x, SurfaceDensity y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(SurfaceDensity x, SurfaceDensity y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(SurfaceDensity x, SurfaceDensity y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(SurfaceDensity x, SurfaceDensity y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="SurfaceDensity"/> <paramref name="x"/>.</summary>
    public static implicit operator double(SurfaceDensity x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(SurfaceDensity x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="SurfaceDensity"/> of magnitude <paramref name="x"/>.</summary>
    public static SurfaceDensity FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="SurfaceDensity"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator SurfaceDensity(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="SurfaceDensity"/> of equivalent magnitude.</summary>
    public static SurfaceDensity FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="SurfaceDensity"/> of equivalent magnitude.</summary>
    public static explicit operator SurfaceDensity(Scalar x) => FromScalar(x);
}