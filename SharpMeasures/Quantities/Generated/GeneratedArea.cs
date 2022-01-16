namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Area"/>, used for describing two-dimensional sections of space - areas.
/// <para>
/// New instances of <see cref="Area"/> can be constructed according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Area"/> a = 5 * <see cref="Area.OneAre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Area"/> b = new(7, <see cref="UnitOfArea.SquareMetre"/>);
/// </code>
/// </item>
/// <item>
/// <code>
/// </code>
/// </item>
/// </list>
/// Instances of <see cref="Area"/> may be applied according to:
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
/// <see cref="Area.InSquareMiles"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Area.InUnit(UnitOfArea)"/>;
/// </code>
/// </item>
/// </list>
/// </para>
/// </summary>
public readonly partial record struct Area :
    IComparable<Area>,
    IScalarQuantity<Area>,
    ISquareRootableScalarQuantity<Length>,
    IAddableScalarQuantity<Area, Area>,
    ISubtractableScalarQuantity<Area, Area>,
    IDivisibleScalarQuantity<Scalar, Area>
{
    /// <summary>The zero-valued <see cref="Area"/>.</summary>
    public static Area Zero { get; } = new(0);

    /// <summary>The <see cref="Area"/> with magnitude 1, when expressed in unit <see cref="UnitOfArea.SquareMetre"/>.</summary>
    public static Area OneSquareMetre { get; } = new(1, UnitOfArea.SquareMetre);
    /// <summary>The <see cref="Area"/> with magnitude 1, when expressed in unit <see cref="UnitOfArea.SquareKilometre"/>.</summary>
    public static Area OneSquareKilometre { get; } = new(1, UnitOfArea.SquareKilometre);
    /// <summary>The <see cref="Area"/> with magnitude 1, when expressed in unit <see cref="UnitOfArea.SquareMile"/>.</summary>
    public static Area OneSquareMile { get; } = new(1, UnitOfArea.SquareMile);

    /// <summary>The <see cref="Area"/> with magnitude 1, when expressed in unit <see cref="UnitOfArea.Are"/>.</summary>
    public static Area OneAre { get; } = new(1, UnitOfArea.Are);
    /// <summary>The <see cref="Area"/> with magnitude 1, when expressed in unit <see cref="UnitOfArea.Hectare"/>.</summary>
    public static Area OneHectare { get; } = new(1, UnitOfArea.Hectare);
    /// <summary>The <see cref="Area"/> with magnitude 1, when expressed in unit <see cref="UnitOfArea.Acre"/>.</summary>
    public static Area OneAcre { get; } = new(1, UnitOfArea.Acre);

    /// <summary>Constructs a <see cref="Area"/> by squaring the <see cref="Length"/> <paramref name="length"/>.</summary>
    /// <param name="length">This <see cref="Length"/> is squared to produce a <see cref="#Quantity"/>.</param>
    public static Area From(Length length) => new(Math.Pow(length.InMetres, 2));

    /// <summary>The magnitude of the <see cref="Area"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Area.InAcres"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Area"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfArea"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Area"/>, in unit <paramref name="unitOfArea"/>.</param>
    /// <param name="unitOfArea">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Area"/> a = 2.6 * <see cref="Area.OneAre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Area(double magnitude, UnitOfArea unitOfArea) : this(magnitude * unitOfArea.Factor) { }
    /// <summary>Constructs a new <see cref="Area"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Area"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfArea"/> to be specified.</remarks>
    public Area(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Area"/>, expressed in unit <see cref="UnitOfArea.SquareMetre"/>.</summary>
    public Scalar InSquareMetres => InUnit(UnitOfArea.SquareMetre);
    /// <summary>Retrieves the magnitude of the <see cref="Area"/>, expressed in unit <see cref="UnitOfArea.SquareKilometre"/>.</summary>
    public Scalar InSquareKilometres => InUnit(UnitOfArea.SquareKilometre);
    /// <summary>Retrieves the magnitude of the <see cref="Area"/>, expressed in unit <see cref="UnitOfArea.SquareMile"/>.</summary>
    public Scalar InSquareMiles => InUnit(UnitOfArea.SquareMile);

    /// <summary>Retrieves the magnitude of the <see cref="Area"/>, expressed in unit <see cref="UnitOfArea.Are"/>.</summary>
    public Scalar InAres => InUnit(UnitOfArea.Are);
    /// <summary>Retrieves the magnitude of the <see cref="Area"/>, expressed in unit <see cref="UnitOfArea.Hectare"/>.</summary>
    public Scalar InHectares => InUnit(UnitOfArea.Hectare);
    /// <summary>Retrieves the magnitude of the <see cref="Area"/>, expressed in unit <see cref="UnitOfArea.Acre"/>.</summary>
    public Scalar InAcres => InUnit(UnitOfArea.Acre);

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
    public Area Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public Area Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public Area Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public Area Round() => new(Math.Round(Magnitude));

    /// <summary>Takes the square root of the <see cref="Area"/>, producing a <see cref="Length"/>.</summary>
    public Length SquareRoot() => Length.From(this);

    /// <inheritdoc/>
    public int CompareTo(Area other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Area"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [m^2]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Area"/>, expressed in unit <paramref name="unitOfArea"/>.</summary>
    /// <param name="unitOfArea">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfArea unitOfArea) => InUnit(Magnitude, unitOfArea);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Area"/>, expressed in unit <paramref name="unitOfArea"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="Area"/>.</param>
    /// <param name="unitOfArea">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfArea unitOfArea) => new(magnitude / unitOfArea.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Area"/>.</summary>
    public Area Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Area"/> with negated magnitude.</summary>
    public Area Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Area"/>.</param>
    public static Area operator +(Area x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Area"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Area"/>.</param>
    public static Area operator -(Area x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="Area"/> <paramref name="term"/>, producing another <see cref="Area"/>.</summary>
    /// <param name="term">This <see cref="Area"/> is added to this instance.</param>
    public Area Add(Area term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="Area"/> <paramref name="term"/> from this instance, producing another <see cref="Area"/>.</summary>
    /// <param name="term">This <see cref="Area"/> is subtracted from this instance.</param>
    public Area Subtract(Area term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="Area"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="Area"/>.</summary>
    /// <param name="x">This <see cref="Area"/> is added to the <see cref="Area"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="Area"/> is added to the <see cref="Area"/> <paramref name="x"/>.</param>
    public static Area operator +(Area x, Area y) => x.Add(y);
    /// <summary>Subtract the <see cref="Area"/> <paramref name="y"/> from the <see cref="Area"/> <paramref name="x"/>, producing another <see cref="Area"/>.</summary>
    /// <param name="x">The <see cref="Area"/> <paramref name="y"/> is subtracted from this <see cref="Area"/>.</param>
    /// <param name="y">This <see cref="Area"/> is subtracted from the <see cref="Area"/> <paramref name="x"/>.</param>
    public static Area operator -(Area x, Area y) => x.Subtract(y);

    /// <summary>Divides this <see cref="Area"/> by the <see cref="Area"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Area"/> is divided by this <see cref="Area"/>.</param>
    public Scalar Divide(Area divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="Area"/> <paramref name="x"/> by the <see cref="Area"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="Area"/> is divided by the <see cref="Area"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Area"/> <paramref name="x"/> is divided by this <see cref="Area"/>.</param>
    public static Scalar operator /(Area x, Area y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="Area"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Area"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Area"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Area"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Area"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Area"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Area"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Area x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Area"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Area"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Area"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Area x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Area"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Area Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Area"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Area"/> is scaled.</param>
    public Area Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Area"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Area"/> is divided.</param>
    public Area Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Area"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Area"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Area operator %(Area x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Area"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Area"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Area"/> <paramref name="x"/>.</param>
    public static Area operator *(Area x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Area"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Area"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Area"/>, which is scaled by <paramref name="x"/>.</param>
    public static Area operator *(double x, Area y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Area"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Area"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Area"/> <paramref name="x"/>.</param>
    public static Area operator /(Area x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="Area"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Area Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Area"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Area"/> is scaled.</param>
    public Area Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Area"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Area"/> is divided.</param>
    public Area Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Area"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Area"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Area operator %(Area x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Area"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Area"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Area"/> <paramref name="x"/>.</param>
    public static Area operator *(Area x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Area"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Area"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Area"/>, which is scaled by <paramref name="x"/>.</param>
    public static Area operator *(Scalar x, Area y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Area"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Area"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Area"/> <paramref name="x"/>.</param>
    public static Area operator /(Area x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="Area"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Area"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Area"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Area"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Area"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Area"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Area"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Area.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Area x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Area"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Area"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Area"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Area.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Area x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Area x, Area y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Area x, Area y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Area x, Area y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Area x, Area y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="Area"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Area x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Area x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Area"/> of magnitude <paramref name="x"/>.</summary>
    public static Area FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Area"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Area(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Area"/> of equivalent magnitude.</summary>
    public static Area FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Area"/> of equivalent magnitude.</summary>
    public static explicit operator Area(Scalar x) => FromScalar(x);
}