#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="TimeSquared"/>, describing the square of <see cref="Time"/>.
/// The quantity is expressed in <see cref="UnitOfTimeSquared"/>, with the SI unit being [s²].
/// <para>
/// New instances of <see cref="TimeSquared"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfTimeSquared"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="TimeSquared"/> a = 3 * <see cref="TimeSquared.OneSquareSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="TimeSquared"/> d = <see cref="TimeSquared.From(Distance, Acceleration)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="TimeSquared"/> can be retrieved in the desired <see cref="UnitOfTimeSquared"/> using pre-defined properties,
/// such as <see cref="SquareSeconds"/>.
/// </para>
/// </summary>
public readonly partial record struct TimeSquared :
    IComparable<TimeSquared>,
    IScalarQuantity,
    IScalableScalarQuantity<TimeSquared>,
    IInvertibleScalarQuantity<FrequencyDrift>,
    ISquareRootableScalarQuantity<Time>,
    IMultiplicableScalarQuantity<TimeSquared, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<TimeSquared, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="TimeSquared"/>.</summary>
    public static TimeSquared Zero { get; } = new(0);

    /// <summary>The <see cref="TimeSquared"/> of magnitude 1, when expressed in <see cref="UnitOfTimeSquared.SquareSecond"/>.</summary>
    public static TimeSquared OneSquareSecond { get; } = UnitOfTimeSquared.SquareSecond.TimeSquared;

    /// <summary>Computes <see cref="TimeSquared"/> according to { 1 / <paramref name="frequencyDrift"/> }.</summary>
    /// <summary>Constructs a <see cref="TimeSquared"/> by inverting the <see cref="FrequencyDrift"/> <paramref name="frequencyDrift"/>.</summary>
    public static TimeSquared From(FrequencyDrift frequencyDrift) => new(1 / frequencyDrift.Magnitude);
    /// <summary>Computes <see cref="TimeSquared"/> according to { <paramref name="time"/>² }.</summary>
    /// <param name="time">This <see cref="Time"/> is squared to produce a <see cref="TimeSquared"/>.</param>
    public static TimeSquared From(Time time) => new(Math.Pow(time.Magnitude, 2));
    /// <summary>Computes <see cref="TimeSquared"/> according to { <paramref name="time1"/> ∙ <paramref name="time2"/> }.</summary>
    /// <param name="time1">This <see cref="Time"/> is multiplied by <paramref name="time2"/> to
    /// produce a <see cref="TimeSquared"/>.</param>
    /// <param name="time2">This <see cref="Time"/> is multiplied by <paramref name="time1"/> to
    /// produce a <see cref="TimeSquared"/>.</param>
    public static TimeSquared From(Time time1, Time time2) => new(time1.Magnitude * time2.Magnitude);

    /// <summary>The magnitude of the <see cref="TimeSquared"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfTimeSquared)"/> or a pre-defined property
    /// - such as <see cref="SquareSeconds"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="TimeSquared"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfTimeSquared"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TimeSquared"/>, expressed in <paramref name="unitOfTimeSquared"/>.</param>
    /// <param name="unitOfTimeSquared">The <see cref="UnitOfTimeSquared"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="TimeSquared"/> a = 3 * <see cref="TimeSquared.OneSquareSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public TimeSquared(Scalar magnitude, UnitOfTimeSquared unitOfTimeSquared) : this(magnitude.Magnitude, unitOfTimeSquared) { }
    /// <summary>Constructs a new <see cref="TimeSquared"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfTimeSquared"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TimeSquared"/>, expressed in <paramref name="unitOfTimeSquared"/>.</param>
    /// <param name="unitOfTimeSquared">The <see cref="UnitOfTimeSquared"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="TimeSquared"/> a = 3 * <see cref="TimeSquared.OneSquareSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public TimeSquared(double magnitude, UnitOfTimeSquared unitOfTimeSquared) : this(magnitude * unitOfTimeSquared.TimeSquared.Magnitude) { }
    /// <summary>Constructs a new <see cref="TimeSquared"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TimeSquared"/>.</param>
    /// <remarks>Consider preferring <see cref="TimeSquared(Scalar, UnitOfTimeSquared)"/>.</remarks>
    public TimeSquared(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="TimeSquared"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TimeSquared"/>.</param>
    /// <remarks>Consider preferring <see cref="TimeSquared(double, UnitOfTimeSquared)"/>.</remarks>
    public TimeSquared(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="TimeSquared"/>, expressed in <see cref="UnitOfTimeSquared.SquareSecond"/>.</summary>
    public Scalar SquareSeconds => InUnit(UnitOfTimeSquared.SquareSecond);

    /// <summary>Indicates whether the magnitude of the <see cref="TimeSquared"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="TimeSquared"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="TimeSquared"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="TimeSquared"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="TimeSquared"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="TimeSquared"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="TimeSquared"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="TimeSquared"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="TimeSquared"/>.</summary>
    public TimeSquared Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="TimeSquared"/>.</summary>
    public TimeSquared Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="TimeSquared"/>.</summary>
    public TimeSquared Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="TimeSquared"/> to the nearest integer value.</summary>
    public TimeSquared Round() => new(Math.Round(Magnitude));

    /// <summary>Computes the inverse of the <see cref="TimeSquared"/>, producing a <see cref="FrequencyDrift"/>.</summary>
    public FrequencyDrift Invert() => FrequencyDrift.From(this);
    /// <summary>Computes the square root of the <see cref="TimeSquared"/>, producing a <see cref="Time"/>.</summary>
    public Time SquareRoot() => Time.From(this);

    /// <inheritdoc/>
    public int CompareTo(TimeSquared other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="TimeSquared"/> in the default unit
    /// <see cref="UnitOfTimeSquared.SquareSecond"/>, followed by the symbol [s²].</summary>
    public override string ToString() => $"{SquareSeconds} [s²]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="TimeSquared"/>,
    /// expressed in <paramref name="unitOfTimeSquared"/>.</summary>
    /// <param name="unitOfTimeSquared">The <see cref="UnitOfTimeSquared"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfTimeSquared unitOfTimeSquared) => InUnit(this, unitOfTimeSquared);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="TimeSquared"/>,
    /// expressed in <paramref name="unitOfTimeSquared"/>.</summary>
    /// <param name="timeSquared">The <see cref="TimeSquared"/> to be expressed in <paramref name="unitOfTimeSquared"/>.</param>
    /// <param name="unitOfTimeSquared">The <see cref="UnitOfTimeSquared"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(TimeSquared timeSquared, UnitOfTimeSquared unitOfTimeSquared) => new(timeSquared.Magnitude / unitOfTimeSquared.TimeSquared.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="TimeSquared"/>.</summary>
    public TimeSquared Plus() => this;
    /// <summary>Negation, resulting in a <see cref="TimeSquared"/> with negated magnitude.</summary>
    public TimeSquared Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="TimeSquared"/>.</param>
    public static TimeSquared operator +(TimeSquared x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="TimeSquared"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="TimeSquared"/>.</param>
    public static TimeSquared operator -(TimeSquared x) => x.Negate();

    /// <summary>Multiplicates the <see cref="TimeSquared"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="TimeSquared"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="TimeSquared"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="TimeSquared"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="TimeSquared"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="TimeSquared"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(TimeSquared x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="TimeSquared"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="TimeSquared"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="TimeSquared"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, TimeSquared y) => y.Multiply(x);
    /// <summary>Division of the <see cref="TimeSquared"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="TimeSquared"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(TimeSquared x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="TimeSquared"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="TimeSquared"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TimeSquared"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, TimeSquared y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="TimeSquared"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public TimeSquared Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="TimeSquared"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="TimeSquared"/> is scaled.</param>
    public TimeSquared Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="TimeSquared"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="TimeSquared"/> is divided.</param>
    public TimeSquared Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="TimeSquared"/> <paramref name="x"/> by this value.</param>
    public static TimeSquared operator %(TimeSquared x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="TimeSquared"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="TimeSquared"/> <paramref name="x"/>.</param>
    public static TimeSquared operator *(TimeSquared x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="TimeSquared"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="TimeSquared"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TimeSquared"/>, which is scaled by <paramref name="x"/>.</param>
    public static TimeSquared operator *(double x, TimeSquared y) => y.Multiply(x);
    /// <summary>Scales the <see cref="TimeSquared"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="TimeSquared"/> <paramref name="x"/>.</param>
    public static TimeSquared operator /(TimeSquared x, double y) => x.Divide(y);
    /// <summary>Inverts the <see cref="TimeSquared"/> <paramref name="y"/> to produce a <see cref="FrequencyDrift"/>, which is then scaled by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="TimeSquared"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TimeSquared"/>, which is inverted to a <see cref="FrequencyDrift"/> and scaled by <paramref name="x"/>.</param>
    public static FrequencyDrift operator /(double x, TimeSquared y) => new(x / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="TimeSquared"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public TimeSquared Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="TimeSquared"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="TimeSquared"/> is scaled.</param>
    public TimeSquared Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="TimeSquared"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="TimeSquared"/> is divided.</param>
    public TimeSquared Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="TimeSquared"/> <paramref name="x"/> by this value.</param>
    public static TimeSquared operator %(TimeSquared x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="TimeSquared"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="TimeSquared"/> <paramref name="x"/>.</param>
    public static TimeSquared operator *(TimeSquared x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="TimeSquared"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="TimeSquared"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TimeSquared"/>, which is scaled by <paramref name="x"/>.</param>
    public static TimeSquared operator *(Scalar x, TimeSquared y) => y.Multiply(x);
    /// <summary>Scales the <see cref="TimeSquared"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="TimeSquared"/> <paramref name="x"/>.</param>
    public static TimeSquared operator /(TimeSquared x, Scalar y) => x.Divide(y);
    /// <summary>Inverts the <see cref="TimeSquared"/> <paramref name="y"/> to produce a <see cref="FrequencyDrift"/>,
    /// which is then scaled by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="TimeSquared"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TimeSquared"/>, which is inverted to a <see cref="FrequencyDrift"/> and scaled by <paramref name="x"/>.</param>
    public static FrequencyDrift operator /(Scalar x, TimeSquared y) => new(x / y.Magnitude);

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

    /// <summary>Multiplication of the <see cref="TimeSquared"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="TimeSquared"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(TimeSquared x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="TimeSquared"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TimeSquared"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="TimeSquared"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(TimeSquared x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="TimeSquared"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="TimeSquared"/>.</param>
    public static bool operator <(TimeSquared x, TimeSquared y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="TimeSquared"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="TimeSquared"/>.</param>
    public static bool operator >(TimeSquared x, TimeSquared y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="TimeSquared"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="TimeSquared"/>.</param>
    public static bool operator <=(TimeSquared x, TimeSquared y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="TimeSquared"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="TimeSquared"/>.</param>
    public static bool operator >=(TimeSquared x, TimeSquared y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="TimeSquared"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(TimeSquared x) => x.ToDouble();

    /// <summary>Converts the <see cref="TimeSquared"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(TimeSquared x) => x.ToScalar();

    /// <summary>Constructs the <see cref="TimeSquared"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static TimeSquared FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="TimeSquared"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator TimeSquared(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="TimeSquared"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static TimeSquared FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="TimeSquared"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator TimeSquared(Scalar x) => FromScalar(x);
}
