#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Area"/>, describing the size of a two-dimensional section of space - the square of <see cref="Length"/>.
/// The quantity is expressed in <see cref="UnitOfArea"/>, with the SI unit being [m²].
/// <para>
/// New instances of <see cref="Area"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfArea"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Area"/> a = 3 * <see cref="Area.OneAre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Area"/> d = <see cref="Area.From(Mass, ArealDensity)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="Area"/> can be retrieved in the desired <see cref="UnitOfArea"/> using pre-defined properties,
/// such as <see cref="Ares"/>.
/// </para>
/// </summary>
public readonly partial record struct Area :
    IComparable<Area>,
    IScalarQuantity,
    IScalableScalarQuantity<Area>,
    ISquareRootableScalarQuantity<Length>,
    IMultiplicableScalarQuantity<Area, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Area, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="Area"/>.</summary>
    public static Area Zero { get; } = new(0);

    /// <summary>The <see cref="Area"/> with magnitude 1, when expressed in unit <see cref="UnitOfArea.SquareMetre"/>.</summary>
    public static Area OneSquareMetre { get; } = UnitOfArea.SquareMetre.Area;
    /// <summary>The <see cref="Area"/> with magnitude 1, when expressed in unit <see cref="UnitOfArea.SquareKilometre"/>.</summary>
    public static Area OneSquareKilometre { get; } = UnitOfArea.SquareKilometre.Area;
    /// <summary>The <see cref="Area"/> with magnitude 1, when expressed in unit <see cref="UnitOfArea.SquareInch"/>.</summary>
    public static Area OneSquareInch { get; } = UnitOfArea.SquareInch.Area;
    /// <summary>The <see cref="Area"/> with magnitude 1, when expressed in unit <see cref="UnitOfArea.SquareMile"/>.</summary>
    public static Area OneSquareMile { get; } = UnitOfArea.SquareMile.Area;
    /// <summary>The <see cref="Area"/> with magnitude 1, when expressed in unit <see cref="UnitOfArea.Are"/>.</summary>
    public static Area OneAre { get; } = UnitOfArea.Are.Area;
    /// <summary>The <see cref="Area"/> with magnitude 1, when expressed in unit <see cref="UnitOfArea.Hectare"/>.</summary>
    public static Area OneHectare { get; } = UnitOfArea.Hectare.Area;
    /// <summary>The <see cref="Area"/> with magnitude 1, when expressed in unit <see cref="UnitOfArea.Acre"/>.</summary>
    public static Area OneAcre { get; } = UnitOfArea.Acre.Area;

    /// <summary>Computes <see cref="Area"/> according to { <paramref name="length"/>² }.</summary>
    /// <param name="length">This <see cref="Length"/> is squared to produce a <see cref="Area"/>.</param>
    public static Area From(Length length) => new(Math.Pow(length.Magnitude, 2));
    /// <summary>Computes <see cref="Area"/> according to { <paramref name="distance"/>² }.</summary>
    /// <param name="distance">This <see cref="Distance"/> is squared to produce a <see cref="Area"/>.</param>
    public static Area From(Distance distance) => new(Math.Pow(distance.Magnitude, 2));
    /// <summary>Computes <see cref="Area"/> according to { <paramref name="length1"/> ∙ <paramref name="length2"/> }.</summary>
    /// <param name="length1">This <see cref="Length"/> is multiplied by <paramref name="length2"/> to
    /// produce a <see cref="Area"/>.</param>
    /// <param name="length2">This <see cref="Length"/> is multiplied by <paramref name="length1"/> to
    /// produce a <see cref="Area"/>.</param>
    public static Area From(Length length1, Length length2) => new(length1.Magnitude * length2.Magnitude);
    /// <summary>Computes <see cref="Area"/> according to { <paramref name="distance1"/> ∙ <paramref name="distance2"/> }.</summary>
    /// <param name="distance1">This <see cref="Distance"/> is multiplied by <paramref name="distance2"/> to
    /// produce a <see cref="Area"/>.</param>
    /// <param name="distance2">This <see cref="Distance"/> is multiplied by <paramref name="distance1"/> to
    /// produce a <see cref="Area"/>.</param>
    public static Area From(Distance distance1, Distance distance2) => new(distance1.Magnitude * distance2.Magnitude);

    /// <summary>The magnitude of the <see cref="Area"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfArea)"/> or a pre-defined property
    /// - such as <see cref="SquareMetres"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Area"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfArea"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Area"/>, expressed in <paramref name="unitOfArea"/>.</param>
    /// <param name="unitOfArea">The <see cref="UnitOfArea"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Area"/> a = 3 * <see cref="Area.OneSquareMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Area(Scalar magnitude, UnitOfArea unitOfArea) : this(magnitude.Magnitude, unitOfArea) { }
    /// <summary>Constructs a new <see cref="Area"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfArea"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Area"/>, expressed in <paramref name="unitOfArea"/>.</param>
    /// <param name="unitOfArea">The <see cref="UnitOfArea"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Area"/> a = 3 * <see cref="Area.OneSquareMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Area(double magnitude, UnitOfArea unitOfArea) : this(magnitude * unitOfArea.Area.Magnitude) { }
    /// <summary>Constructs a new <see cref="Area"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Area"/>.</param>
    /// <remarks>Consider preferring <see cref="Area(Scalar, UnitOfArea)"/>.</remarks>
    public Area(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Area"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Area"/>.</param>
    /// <remarks>Consider preferring <see cref="Area(double, UnitOfArea)"/>.</remarks>
    public Area(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Area"/>, expressed in <see cref="UnitOfArea.SquareMetre"/>.</summary>
    public Scalar SquareMetres => InUnit(UnitOfArea.SquareMetre);
    /// <summary>Retrieves the magnitude of the <see cref="Area"/>, expressed in <see cref="UnitOfArea.SquareKilometre"/>.</summary>
    public Scalar SquareKilometres => InUnit(UnitOfArea.SquareKilometre);
    /// <summary>Retrieves the magnitude of the <see cref="Area"/>, expressed in <see cref="UnitOfArea.SquareInch"/>.</summary>
    public Scalar SquareInches => InUnit(UnitOfArea.SquareInch);
    /// <summary>Retrieves the magnitude of the <see cref="Area"/>, expressed in <see cref="UnitOfArea.SquareMile"/>.</summary>
    public Scalar SquareMiles => InUnit(UnitOfArea.SquareMile);
    /// <summary>Retrieves the magnitude of the <see cref="Area"/>, expressed in <see cref="UnitOfArea.Are"/>.</summary>
    public Scalar Ares => InUnit(UnitOfArea.Are);
    /// <summary>Retrieves the magnitude of the <see cref="Area"/>, expressed in <see cref="UnitOfArea.Hectare"/>.</summary>
    public Scalar Hectares => InUnit(UnitOfArea.Hectare);
    /// <summary>Retrieves the magnitude of the <see cref="Area"/>, expressed in <see cref="UnitOfArea.Acre"/>.</summary>
    public Scalar Acres => InUnit(UnitOfArea.Acre);

    /// <summary>Indicates whether the magnitude of the <see cref="Area"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Area"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Area"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Area"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Area"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Area"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Area"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Area"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="Area"/>.</summary>
    public Area Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="Area"/>.</summary>
    public Area Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="Area"/>.</summary>
    public Area Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="Area"/> to the nearest integer value.</summary>
    public Area Round() => new(Math.Round(Magnitude));

    /// <summary>Computes the square root of the <see cref="Area"/>, producing a <see cref="Length"/>.</summary>
    public Length SquareRoot() => Length.From(this);

    /// <inheritdoc/>
    public int CompareTo(Area other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Area"/> in the default unit
    /// <see cref="UnitOfArea.SquareMetre"/>, followed by the symbol [m²].</summary>
    public override string ToString() => $"{SquareMetres} [m²]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Area"/>,
    /// expressed in <paramref name="unitOfArea"/>.</summary>
    /// <param name="unitOfArea">The <see cref="UnitOfArea"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfArea unitOfArea) => InUnit(this, unitOfArea);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Area"/>,
    /// expressed in <paramref name="unitOfArea"/>.</summary>
    /// <param name="area">The <see cref="Area"/> to be expressed in <paramref name="unitOfArea"/>.</param>
    /// <param name="unitOfArea">The <see cref="UnitOfArea"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Area area, UnitOfArea unitOfArea) => new(area.Magnitude / unitOfArea.Area.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Area"/>.</summary>
    public Area Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Area"/> with negated magnitude.</summary>
    public Area Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Area"/>.</param>
    public static Area operator +(Area x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Area"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Area"/>.</param>
    public static Area operator -(Area x) => x.Negate();

    /// <summary>Multiplicates the <see cref="Area"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Area"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Area"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Area"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Area"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Area"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Area"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Area x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Area"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Area"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Area"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Area y) => y.Multiply(x);
    /// <summary>Division of the <see cref="Area"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Area"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Area"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Area x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="Area"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="Area"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Area"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, Area y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="Area"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Area Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Area"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Area"/> is scaled.</param>
    public Area Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Area"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Area"/> is divided.</param>
    public Area Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Area"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Area"/> <paramref name="x"/> by this value.</param>
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

    /// <summary>Computes the remainder from division of the <see cref="Area"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Area Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Area"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Area"/> is scaled.</param>
    public Area Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Area"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Area"/> is divided.</param>
    public Area Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Area"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Area"/> <paramref name="x"/> by this value.</param>
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

    /// <summary>Multiplication of the <see cref="Area"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Area"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Area"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Area x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="Area"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Area"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Area"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Area x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Area"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Area"/>.</param>
    public static bool operator <(Area x, Area y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Area"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Area"/>.</param>
    public static bool operator >(Area x, Area y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Area"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Area"/>.</param>
    public static bool operator <=(Area x, Area y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Area"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Area"/>.</param>
    public static bool operator >=(Area x, Area y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Area"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(Area x) => x.ToDouble();

    /// <summary>Converts the <see cref="Area"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Area x) => x.ToScalar();

    /// <summary>Constructs the <see cref="Area"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Area FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Area"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Area(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="Area"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static Area FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="Area"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator Area(Scalar x) => FromScalar(x);
}
