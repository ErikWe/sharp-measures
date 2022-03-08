#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="MassFlowRate"/>, describing the amount of <see cref="Mass"/> that flows
/// through some region of space over some <see cref="Time"/>. The quantity is expressed in <see cref="UnitOfMassFlowRate"/>, with the SI unit being [kg∙s⁻¹].
/// <para>
/// New instances of <see cref="MassFlowRate"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfMassFlowRate"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code><see cref="MassFlowRate"/> a = 3 * <see cref="MassFlowRate.OneKilogramPerSecond"/>;</code>
/// </item>
/// <item>
/// <code><see cref="MassFlowRate"/> d = <see cref="MassFlowRate.From(Mass, Time)"/>;</code>
/// </item>
/// </list>
/// The magnitude of the <see cref="MassFlowRate"/> can be retrieved in the desired <see cref="UnitOfMassFlowRate"/> using pre-defined properties,
/// such as <see cref="KilogramsPerSecond"/>.
/// </para>
/// </summary>
public readonly partial record struct MassFlowRate :
    IComparable<MassFlowRate>,
    IScalarQuantity,
    IScalableScalarQuantity<MassFlowRate>,
    IMultiplicableScalarQuantity<MassFlowRate, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<MassFlowRate, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="MassFlowRate"/>.</summary>
    public static MassFlowRate Zero { get; } = new(0);

    /// <summary>The <see cref="MassFlowRate"/> of magnitude 1, when expressed in <see cref="UnitOfMassFlowRate.KilogramPerSecond"/>.</summary>
    public static MassFlowRate OneKilogramPerSecond { get; } = UnitOfMassFlowRate.KilogramPerSecond.MassFlowRate;
    /// <summary>The <see cref="MassFlowRate"/> of magnitude 1, when expressed in <see cref="UnitOfMassFlowRate.PoundPerSecond"/>.</summary>
    public static MassFlowRate OnePoundPerSecond { get; } = UnitOfMassFlowRate.PoundPerSecond.MassFlowRate;

    /// <summary>The magnitude of the <see cref="MassFlowRate"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfMassFlowRate)"/> or a pre-defined property
    /// - such as <see cref="KilogramsPerSecond"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="MassFlowRate"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfMassFlowRate"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="MassFlowRate"/>, expressed in <paramref name="unitOfMassFlowRate"/>.</param>
    /// <param name="unitOfMassFlowRate">The <see cref="UnitOfMassFlowRate"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="MassFlowRate"/> a = 3 * <see cref="MassFlowRate.OneKilogramPerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public MassFlowRate(Scalar magnitude, UnitOfMassFlowRate unitOfMassFlowRate) : this(magnitude.Magnitude, unitOfMassFlowRate) { }
    /// <summary>Constructs a new <see cref="MassFlowRate"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfMassFlowRate"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="MassFlowRate"/>, expressed in <paramref name="unitOfMassFlowRate"/>.</param>
    /// <param name="unitOfMassFlowRate">The <see cref="UnitOfMassFlowRate"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="MassFlowRate"/> a = 3 * <see cref="MassFlowRate.OneKilogramPerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public MassFlowRate(double magnitude, UnitOfMassFlowRate unitOfMassFlowRate) : this(magnitude * unitOfMassFlowRate.MassFlowRate.Magnitude) { }
    /// <summary>Constructs a new <see cref="MassFlowRate"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="MassFlowRate"/>.</param>
    /// <remarks>Consider preferring <see cref="MassFlowRate(Scalar, UnitOfMassFlowRate)"/>.</remarks>
    public MassFlowRate(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="MassFlowRate"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="MassFlowRate"/>.</param>
    /// <remarks>Consider preferring <see cref="MassFlowRate(double, UnitOfMassFlowRate)"/>.</remarks>
    public MassFlowRate(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="MassFlowRate"/>, expressed in <see cref="UnitOfMassFlowRate.KilogramPerSecond"/>.</summary>
    public Scalar KilogramsPerSecond => InUnit(UnitOfMassFlowRate.KilogramPerSecond);
    /// <summary>Retrieves the magnitude of the <see cref="MassFlowRate"/>, expressed in <see cref="UnitOfMassFlowRate.PoundPerSecond"/>.</summary>
    public Scalar PoundsPerSecond => InUnit(UnitOfMassFlowRate.PoundPerSecond);

    /// <summary>Indicates whether the magnitude of the <see cref="MassFlowRate"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="MassFlowRate"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="MassFlowRate"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="MassFlowRate"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="MassFlowRate"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="MassFlowRate"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="MassFlowRate"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="MassFlowRate"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="MassFlowRate"/>.</summary>
    public MassFlowRate Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="MassFlowRate"/>.</summary>
    public MassFlowRate Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="MassFlowRate"/>.</summary>
    public MassFlowRate Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="MassFlowRate"/> to the nearest integer value.</summary>
    public MassFlowRate Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(MassFlowRate other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="MassFlowRate"/> in the default unit
    /// <see cref="UnitOfMassFlowRate.KilogramPerSecond"/>, followed by the symbol [kg∙s⁻¹].</summary>
    public override string ToString() => $"{KilogramsPerSecond} [kg∙s⁻¹]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="MassFlowRate"/>,
    /// expressed in <paramref name="unitOfMassFlowRate"/>.</summary>
    /// <param name="unitOfMassFlowRate">The <see cref="UnitOfMassFlowRate"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfMassFlowRate unitOfMassFlowRate) => InUnit(this, unitOfMassFlowRate);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="MassFlowRate"/>,
    /// expressed in <paramref name="unitOfMassFlowRate"/>.</summary>
    /// <param name="massFlowRate">The <see cref="MassFlowRate"/> to be expressed in <paramref name="unitOfMassFlowRate"/>.</param>
    /// <param name="unitOfMassFlowRate">The <see cref="UnitOfMassFlowRate"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(MassFlowRate massFlowRate, UnitOfMassFlowRate unitOfMassFlowRate) => new(massFlowRate.Magnitude / unitOfMassFlowRate.MassFlowRate.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="MassFlowRate"/>.</summary>
    public MassFlowRate Plus() => this;
    /// <summary>Negation, resulting in a <see cref="MassFlowRate"/> with negated magnitude.</summary>
    public MassFlowRate Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="MassFlowRate"/>.</param>
    public static MassFlowRate operator +(MassFlowRate x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="MassFlowRate"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="MassFlowRate"/>.</param>
    public static MassFlowRate operator -(MassFlowRate x) => x.Negate();

    /// <summary>Multiplicates the <see cref="MassFlowRate"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="MassFlowRate"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="MassFlowRate"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="MassFlowRate"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="MassFlowRate"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="MassFlowRate"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(MassFlowRate x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="MassFlowRate"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="MassFlowRate"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="MassFlowRate"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, MassFlowRate y) => y.Multiply(x);
    /// <summary>Division of the <see cref="MassFlowRate"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="MassFlowRate"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(MassFlowRate x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="MassFlowRate"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="MassFlowRate"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="MassFlowRate"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, MassFlowRate y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="MassFlowRate"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public MassFlowRate Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="MassFlowRate"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="MassFlowRate"/> is scaled.</param>
    public MassFlowRate Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="MassFlowRate"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="MassFlowRate"/> is divided.</param>
    public MassFlowRate Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="MassFlowRate"/> <paramref name="x"/> by this value.</param>
    public static MassFlowRate operator %(MassFlowRate x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="MassFlowRate"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="MassFlowRate"/> <paramref name="x"/>.</param>
    public static MassFlowRate operator *(MassFlowRate x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="MassFlowRate"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="MassFlowRate"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="MassFlowRate"/>, which is scaled by <paramref name="x"/>.</param>
    public static MassFlowRate operator *(double x, MassFlowRate y) => y.Multiply(x);
    /// <summary>Scales the <see cref="MassFlowRate"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="MassFlowRate"/> <paramref name="x"/>.</param>
    public static MassFlowRate operator /(MassFlowRate x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="MassFlowRate"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public MassFlowRate Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="MassFlowRate"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="MassFlowRate"/> is scaled.</param>
    public MassFlowRate Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="MassFlowRate"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="MassFlowRate"/> is divided.</param>
    public MassFlowRate Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="MassFlowRate"/> <paramref name="x"/> by this value.</param>
    public static MassFlowRate operator %(MassFlowRate x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="MassFlowRate"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="MassFlowRate"/> <paramref name="x"/>.</param>
    public static MassFlowRate operator *(MassFlowRate x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="MassFlowRate"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="MassFlowRate"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="MassFlowRate"/>, which is scaled by <paramref name="x"/>.</param>
    public static MassFlowRate operator *(Scalar x, MassFlowRate y) => y.Multiply(x);
    /// <summary>Scales the <see cref="MassFlowRate"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="MassFlowRate"/> <paramref name="x"/>.</param>
    public static MassFlowRate operator /(MassFlowRate x, Scalar y) => x.Divide(y);

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

    /// <summary>Multiplication of the <see cref="MassFlowRate"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="MassFlowRate"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(MassFlowRate x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="MassFlowRate"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="MassFlowRate"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(MassFlowRate x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="MassFlowRate"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="MassFlowRate"/>.</param>
    public static bool operator <(MassFlowRate x, MassFlowRate y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="MassFlowRate"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="MassFlowRate"/>.</param>
    public static bool operator >(MassFlowRate x, MassFlowRate y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="MassFlowRate"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="MassFlowRate"/>.</param>
    public static bool operator <=(MassFlowRate x, MassFlowRate y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="MassFlowRate"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="MassFlowRate"/>.</param>
    public static bool operator >=(MassFlowRate x, MassFlowRate y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="MassFlowRate"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(MassFlowRate x) => x.ToDouble();

    /// <summary>Converts the <see cref="MassFlowRate"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(MassFlowRate x) => x.ToScalar();

    /// <summary>Constructs the <see cref="MassFlowRate"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static MassFlowRate FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="MassFlowRate"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator MassFlowRate(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="MassFlowRate"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static MassFlowRate FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="MassFlowRate"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator MassFlowRate(Scalar x) => FromScalar(x);
}
