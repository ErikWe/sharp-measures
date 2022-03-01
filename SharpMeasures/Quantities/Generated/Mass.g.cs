#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Mass"/>.
/// The quantity is expressed in <see cref="UnitOfMass"/>, with the SI unit being [kg].
/// <para>
/// New instances of <see cref="Mass"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfMass"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Mass"/> a = 3 * <see cref="Mass.OneKilogram"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Mass"/> d = <see cref="Mass.From(Density, Volume)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="Mass"/> can be retrieved in the desired <see cref="UnitOfMass"/> using pre-defined properties,
/// such as <see cref="Kilograms"/>.
/// </para>
/// </summary>
public readonly partial record struct Mass :
    IComparable<Mass>,
    IScalarQuantity,
    IScalableScalarQuantity<Mass>,
    IMultiplicableScalarQuantity<Mass, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Mass, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="Mass"/>.</summary>
    public static Mass Zero { get; } = new(0);

    /// <summary>The <see cref="Mass"/> of magnitude 1, when expressed in <see cref="UnitOfMass.Gram"/>.</summary>
    public static Mass OneGram { get; } = UnitOfMass.Gram.Mass;
    /// <summary>The <see cref="Mass"/> of magnitude 1, when expressed in <see cref="UnitOfMass.Milligram"/>.</summary>
    public static Mass OneMilligram { get; } = UnitOfMass.Milligram.Mass;
    /// <summary>The <see cref="Mass"/> of magnitude 1, when expressed in <see cref="UnitOfMass.Hectogram"/>.</summary>
    public static Mass OneHectogram { get; } = UnitOfMass.Hectogram.Mass;
    /// <summary>The <see cref="Mass"/> of magnitude 1, when expressed in <see cref="UnitOfMass.Kilogram"/>.</summary>
    public static Mass OneKilogram { get; } = UnitOfMass.Kilogram.Mass;
    /// <summary>The <see cref="Mass"/> of magnitude 1, when expressed in <see cref="UnitOfMass.Tonne"/>.</summary>
    public static Mass OneTonne { get; } = UnitOfMass.Tonne.Mass;
    /// <summary>The <see cref="Mass"/> of magnitude 1, when expressed in <see cref="UnitOfMass.Ounce"/>.</summary>
    public static Mass OneOunce { get; } = UnitOfMass.Ounce.Mass;
    /// <summary>The <see cref="Mass"/> of magnitude 1, when expressed in <see cref="UnitOfMass.Pound"/>.</summary>
    public static Mass OnePound { get; } = UnitOfMass.Pound.Mass;

    /// <summary>The magnitude of the <see cref="Mass"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfMass)"/> or a pre-defined property
    /// - such as <see cref="Grams"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Mass"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfMass"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Mass"/>, expressed in <paramref name="unitOfMass"/>.</param>
    /// <param name="unitOfMass">The <see cref="UnitOfMass"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Mass"/> a = 3 * <see cref="Mass.OneGram"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Mass(Scalar magnitude, UnitOfMass unitOfMass) : this(magnitude.Magnitude, unitOfMass) { }
    /// <summary>Constructs a new <see cref="Mass"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfMass"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Mass"/>, expressed in <paramref name="unitOfMass"/>.</param>
    /// <param name="unitOfMass">The <see cref="UnitOfMass"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Mass"/> a = 3 * <see cref="Mass.OneGram"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Mass(double magnitude, UnitOfMass unitOfMass) : this(magnitude * unitOfMass.Mass.Magnitude) { }
    /// <summary>Constructs a new <see cref="Mass"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Mass"/>.</param>
    /// <remarks>Consider preferring <see cref="Mass(Scalar, UnitOfMass)"/>.</remarks>
    public Mass(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Mass"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Mass"/>.</param>
    /// <remarks>Consider preferring <see cref="Mass(double, UnitOfMass)"/>.</remarks>
    public Mass(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Mass"/>, expressed in <see cref="UnitOfMass.Gram"/>.</summary>
    public Scalar Grams => InUnit(UnitOfMass.Gram);
    /// <summary>Retrieves the magnitude of the <see cref="Mass"/>, expressed in <see cref="UnitOfMass.Milligram"/>.</summary>
    public Scalar Milligrams => InUnit(UnitOfMass.Milligram);
    /// <summary>Retrieves the magnitude of the <see cref="Mass"/>, expressed in <see cref="UnitOfMass.Hectogram"/>.</summary>
    public Scalar Hectograms => InUnit(UnitOfMass.Hectogram);
    /// <summary>Retrieves the magnitude of the <see cref="Mass"/>, expressed in <see cref="UnitOfMass.Kilogram"/>.</summary>
    public Scalar Kilograms => InUnit(UnitOfMass.Kilogram);
    /// <summary>Retrieves the magnitude of the <see cref="Mass"/>, expressed in <see cref="UnitOfMass.Tonne"/>.</summary>
    public Scalar Tonnes => InUnit(UnitOfMass.Tonne);
    /// <summary>Retrieves the magnitude of the <see cref="Mass"/>, expressed in <see cref="UnitOfMass.Ounce"/>.</summary>
    public Scalar Ounces => InUnit(UnitOfMass.Ounce);
    /// <summary>Retrieves the magnitude of the <see cref="Mass"/>, expressed in <see cref="UnitOfMass.Pound"/>.</summary>
    public Scalar Pounds => InUnit(UnitOfMass.Pound);

    /// <summary>Indicates whether the magnitude of the <see cref="Mass"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Mass"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Mass"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Mass"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Mass"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Mass"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Mass"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Mass"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="Mass"/>.</summary>
    public Mass Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="Mass"/>.</summary>
    public Mass Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="Mass"/>.</summary>
    public Mass Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="Mass"/> to the nearest integer value.</summary>
    public Mass Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Mass other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Mass"/> in the default unit
    /// <see cref="UnitOfMass.Kilogram"/>, followed by the symbol [kg].</summary>
    public override string ToString() => $"{Kilograms} [kg]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Mass"/>,
    /// expressed in <paramref name="unitOfMass"/>.</summary>
    /// <param name="unitOfMass">The <see cref="UnitOfMass"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfMass unitOfMass) => InUnit(this, unitOfMass);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Mass"/>,
    /// expressed in <paramref name="unitOfMass"/>.</summary>
    /// <param name="mass">The <see cref="Mass"/> to be expressed in <paramref name="unitOfMass"/>.</param>
    /// <param name="unitOfMass">The <see cref="UnitOfMass"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Mass mass, UnitOfMass unitOfMass) => new(mass.Magnitude / unitOfMass.Mass.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Mass"/>.</summary>
    public Mass Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Mass"/> with negated magnitude.</summary>
    public Mass Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Mass"/>.</param>
    public static Mass operator +(Mass x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Mass"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Mass"/>.</param>
    public static Mass operator -(Mass x) => x.Negate();

    /// <summary>Multiplicates the <see cref="Mass"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Mass"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Mass"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Mass"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Mass"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Mass"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Mass x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Mass"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Mass"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Mass"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Mass y) => y.Multiply(x);
    /// <summary>Division of the <see cref="Mass"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Mass"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Mass x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="Mass"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="Mass"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Mass"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, Mass y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="Mass"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Mass Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Mass"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Mass"/> is scaled.</param>
    public Mass Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Mass"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Mass"/> is divided.</param>
    public Mass Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Mass"/> <paramref name="x"/> by this value.</param>
    public static Mass operator %(Mass x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Mass"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Mass"/> <paramref name="x"/>.</param>
    public static Mass operator *(Mass x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Mass"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Mass"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Mass"/>, which is scaled by <paramref name="x"/>.</param>
    public static Mass operator *(double x, Mass y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Mass"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Mass"/> <paramref name="x"/>.</param>
    public static Mass operator /(Mass x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="Mass"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Mass Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Mass"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Mass"/> is scaled.</param>
    public Mass Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Mass"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Mass"/> is divided.</param>
    public Mass Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Mass"/> <paramref name="x"/> by this value.</param>
    public static Mass operator %(Mass x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Mass"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Mass"/> <paramref name="x"/>.</param>
    public static Mass operator *(Mass x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Mass"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Mass"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Mass"/>, which is scaled by <paramref name="x"/>.</param>
    public static Mass operator *(Scalar x, Mass y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Mass"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Mass"/> <paramref name="x"/>.</param>
    public static Mass operator /(Mass x, Scalar y) => x.Divide(y);

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TProductScalarQuantity Multiply<TProductScalarQuantity, TFactorScalarQuantity>(TFactorScalarQuantity factor, Func<double, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorScalarQuantity : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        ArgumentNullException.ThrowIfNull(factor, nameof(factor));

        return factory(Magnitude * factor.Magnitude);

    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TQuotientScalarQuantity Divide<TQuotientScalarQuantity, TDivisorScalarQuantity>(TDivisorScalarQuantity divisor, Func<double, TQuotientScalarQuantity> factory)
        where TQuotientScalarQuantity : IScalarQuantity
        where TDivisorScalarQuantity : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        ArgumentNullException.ThrowIfNull(divisor, nameof(divisor));

        return factory(Magnitude / divisor.Magnitude);
    }

    /// <summary>Multiplication of the <see cref="Mass"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Mass"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Mass x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="Mass"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Mass"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Mass x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Mass"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Mass"/>.</param>
    public static bool operator <(Mass x, Mass y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Mass"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Mass"/>.</param>
    public static bool operator >(Mass x, Mass y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Mass"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Mass"/>.</param>
    public static bool operator <=(Mass x, Mass y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Mass"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Mass"/>.</param>
    public static bool operator >=(Mass x, Mass y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Mass"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(Mass x) => x.ToDouble();

    /// <summary>Converts the <see cref="Mass"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Mass x) => x.ToScalar();

    /// <summary>Constructs the <see cref="Mass"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Mass FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Mass"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Mass(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="Mass"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static Mass FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="Mass"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator Mass(Scalar x) => FromScalar(x);
}
