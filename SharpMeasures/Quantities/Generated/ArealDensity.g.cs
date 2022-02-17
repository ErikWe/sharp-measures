namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="ArealDensity"/>, describing the amount of <see cref="Mass"/> over an <see cref="Area"/>.
/// The quantity is expressed in <see cref="UnitOfArealDensity"/>, with the SI unit being [kg∙m⁻²].
/// <para>
/// New instances of <see cref="ArealDensity"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfArealDensity"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="ArealDensity"/> a = 3 * <see cref="ArealDensity.OneKilogramPerSquareMetre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="ArealDensity"/> d = <see cref="ArealDensity.From(Mass, Area)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="ArealDensity"/> can be retrieved in the desired <see cref="UnitOfArealDensity"/> using pre-defined properties,
/// such as <see cref="KilogramsPerSquareMetre"/>.
/// </para>
/// </summary>
public readonly partial record struct ArealDensity :
    IComparable<ArealDensity>,
    IScalarQuantity,
    IScalableScalarQuantity<ArealDensity>,
    IMultiplicableScalarQuantity<ArealDensity, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<ArealDensity, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="ArealDensity"/>.</summary>
    public static ArealDensity Zero { get; } = new(0);

    /// <summary>The <see cref="ArealDensity"/> with magnitude 1, when expressed in unit <see cref="UnitOfArealDensity.KilogramPerSquareMetre"/>.</summary>
    public static ArealDensity OneKilogramPerSquareMetre { get; } = new(1, UnitOfArealDensity.KilogramPerSquareMetre);

    /// <summary>The magnitude of the <see cref="ArealDensity"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfArealDensity)"/> or a pre-defined property
    /// - such as <see cref="KilogramsPerSquareMetre"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="ArealDensity"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfArealDensity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="ArealDensity"/>, expressed in <paramref name="unitOfArealDensity"/>.</param>
    /// <param name="unitOfArealDensity">The <see cref="UnitOfArealDensity"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="ArealDensity"/> a = 3 * <see cref="ArealDensity.OneKilogramPerSquareMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public ArealDensity(Scalar magnitude, UnitOfArealDensity unitOfArealDensity) : this(magnitude.Magnitude, unitOfArealDensity) { }
    /// <summary>Constructs a new <see cref="ArealDensity"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfArealDensity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="ArealDensity"/>, expressed in <paramref name="unitOfArealDensity"/>.</param>
    /// <param name="unitOfArealDensity">The <see cref="UnitOfArealDensity"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="ArealDensity"/> a = 3 * <see cref="ArealDensity.OneKilogramPerSquareMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public ArealDensity(double magnitude, UnitOfArealDensity unitOfArealDensity) : this(magnitude * unitOfArealDensity.ArealDensity.Magnitude) { }
    /// <summary>Constructs a new <see cref="ArealDensity"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="ArealDensity"/>.</param>
    /// <remarks>Consider preferring <see cref="ArealDensity(Scalar, UnitOfArealDensity)"/>.</remarks>
    public ArealDensity(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="ArealDensity"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="ArealDensity"/>.</param>
    /// <remarks>Consider preferring <see cref="ArealDensity(double, UnitOfArealDensity)"/>.</remarks>
    public ArealDensity(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="ArealDensity"/>, expressed in <see cref="UnitOfArealDensity.KilogramPerSquareMetre"/>.</summary>
    public Scalar KilogramsPerSquareMetre => InUnit(UnitOfArealDensity.KilogramPerSquareMetre);

    /// <summary>Indicates whether the magnitude of the <see cref="ArealDensity"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="ArealDensity"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="ArealDensity"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="ArealDensity"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="ArealDensity"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="ArealDensity"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="ArealDensity"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="ArealDensity"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="ArealDensity"/>.</summary>
    public ArealDensity Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="ArealDensity"/>.</summary>
    public ArealDensity Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="ArealDensity"/>.</summary>
    public ArealDensity Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="ArealDensity"/> to the nearest integer value.</summary>
    public ArealDensity Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(ArealDensity other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="ArealDensity"/> in the default unit
    /// <see cref="UnitOfArealDensity.KilogramPerSquareMetre"/>, followed by the symbol [kg∙m⁻²].</summary>
    public override string ToString() => $"{KilogramsPerSquareMetre} [kg∙m⁻²]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="ArealDensity"/>,
    /// expressed in <paramref name="unitOfArealDensity"/>.</summary>
    /// <param name="unitOfArealDensity">The <see cref="UnitOfArealDensity"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfArealDensity unitOfArealDensity) => InUnit(this, unitOfArealDensity);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="ArealDensity"/>,
    /// expressed in <paramref name="unitOfArealDensity"/>.</summary>
    /// <param name="arealDensity">The <see cref="ArealDensity"/> to be expressed in <paramref name="unitOfArealDensity"/>.</param>
    /// <param name="unitOfArealDensity">The <see cref="UnitOfArealDensity"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(ArealDensity arealDensity, UnitOfArealDensity unitOfArealDensity) => new(arealDensity.Magnitude / unitOfArealDensity.ArealDensity.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="ArealDensity"/>.</summary>
    public ArealDensity Plus() => this;
    /// <summary>Negation, resulting in a <see cref="ArealDensity"/> with negated magnitude.</summary>
    public ArealDensity Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="ArealDensity"/>.</param>
    public static ArealDensity operator +(ArealDensity x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="ArealDensity"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="ArealDensity"/>.</param>
    public static ArealDensity operator -(ArealDensity x) => x.Negate();

    /// <summary>Multiplicates the <see cref="ArealDensity"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="ArealDensity"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="ArealDensity"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="ArealDensity"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="ArealDensity"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="ArealDensity"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="ArealDensity"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(ArealDensity x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="ArealDensity"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="ArealDensity"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="ArealDensity"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, ArealDensity y) => y.Multiply(x);
    /// <summary>Division of the <see cref="ArealDensity"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="ArealDensity"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="ArealDensity"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(ArealDensity x, Unhandled y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="ArealDensity"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public ArealDensity Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="ArealDensity"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="ArealDensity"/> is scaled.</param>
    public ArealDensity Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="ArealDensity"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="ArealDensity"/> is divided.</param>
    public ArealDensity Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="ArealDensity"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="ArealDensity"/> <paramref name="x"/> by this value.</param>
    public static ArealDensity operator %(ArealDensity x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="ArealDensity"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="ArealDensity"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="ArealDensity"/> <paramref name="x"/>.</param>
    public static ArealDensity operator *(ArealDensity x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="ArealDensity"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="ArealDensity"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="ArealDensity"/>, which is scaled by <paramref name="x"/>.</param>
    public static ArealDensity operator *(double x, ArealDensity y) => y.Multiply(x);
    /// <summary>Scales the <see cref="ArealDensity"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="ArealDensity"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="ArealDensity"/> <paramref name="x"/>.</param>
    public static ArealDensity operator /(ArealDensity x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="ArealDensity"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public ArealDensity Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="ArealDensity"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="ArealDensity"/> is scaled.</param>
    public ArealDensity Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="ArealDensity"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="ArealDensity"/> is divided.</param>
    public ArealDensity Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="ArealDensity"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="ArealDensity"/> <paramref name="x"/> by this value.</param>
    public static ArealDensity operator %(ArealDensity x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="ArealDensity"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="ArealDensity"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="ArealDensity"/> <paramref name="x"/>.</param>
    public static ArealDensity operator *(ArealDensity x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="ArealDensity"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="ArealDensity"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="ArealDensity"/>, which is scaled by <paramref name="x"/>.</param>
    public static ArealDensity operator *(Scalar x, ArealDensity y) => y.Multiply(x);
    /// <summary>Scales the <see cref="ArealDensity"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="ArealDensity"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="ArealDensity"/> <paramref name="x"/>.</param>
    public static ArealDensity operator /(ArealDensity x, Scalar y) => x.Divide(y);

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
        else
        {
            return factory(Magnitude / divisor.Magnitude);
        }
    }

    /// <summary>Multiplication of the <see cref="ArealDensity"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="ArealDensity"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="ArealDensity"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(ArealDensity x, IScalarQuantity y) => x.Multiply<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="ArealDensity"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="ArealDensity"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="ArealDensity"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(ArealDensity x, IScalarQuantity y) => x.Divide<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="ArealDensity"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="ArealDensity"/>.</param>
    public static bool operator <(ArealDensity x, ArealDensity y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="ArealDensity"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="ArealDensity"/>.</param>
    public static bool operator >(ArealDensity x, ArealDensity y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="ArealDensity"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="ArealDensity"/>.</param>
    public static bool operator <=(ArealDensity x, ArealDensity y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="ArealDensity"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="ArealDensity"/>.</param>
    public static bool operator >=(ArealDensity x, ArealDensity y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="ArealDensity"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(ArealDensity x) => x.ToDouble();

    /// <summary>Converts the <see cref="ArealDensity"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(ArealDensity x) => x.ToScalar();

    /// <summary>Constructs the <see cref="ArealDensity"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static ArealDensity FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="ArealDensity"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator ArealDensity(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="ArealDensity"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static ArealDensity FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="ArealDensity"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator ArealDensity(Scalar x) => FromScalar(x);
}
