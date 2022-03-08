#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Energy"/>, the capability to perform <see cref="Work"/>. The quantity is expressed
/// in <see cref="UnitOfEnergy"/>, with the SI unit being [J].
/// <para>
/// New instances of <see cref="Energy"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfEnergy"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code><see cref="Energy"/> a = 3 * <see cref="Energy.OneJoule"/>;</code>
/// </item>
/// <item>
/// <code><see cref="Energy"/> d = <see cref="Energy.From(PotentialEnergy, KineticEnergy)"/>;</code>
/// </item>
/// <item>
/// <code><see cref="Energy"/> e = <see cref="Work.AsEnergy"/>;</code>
/// </item>
/// </list>
/// The magnitude of the <see cref="Energy"/> can be retrieved in the desired <see cref="UnitOfEnergy"/> using pre-defined properties,
/// such as <see cref="Joules"/>
/// </para>
/// </summary>
/// <remarks>
/// <see cref="Energy"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="KineticEnergy"/></term>
/// <description>Describes the <see cref="Energy"/> of an object tied to the motion of the object.</description>
/// </item>
/// <item>
/// <term><see cref="PotentialEnergy"/></term>
/// <description>Describes the <see cref="Energy"/> of an object tied to the position of the object, and internal state of the object.</description>
/// </item>
/// <item>
/// <term><see cref="Work"/></term>
/// <description>Describes the effect of a <see cref="Force"/> on an object, which transfers <see cref="Energy"/>.</description>
/// </item>
/// <item>
/// <term><see cref="Torque"/></term>
/// <description>Describes <see cref="AngularAcceleration"/> of an object.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct Energy :
    IComparable<Energy>,
    IScalarQuantity,
    IScalableScalarQuantity<Energy>,
    IMultiplicableScalarQuantity<Energy, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Energy, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="Energy"/>.</summary>
    public static Energy Zero { get; } = new(0);

    /// <summary>The <see cref="Energy"/> of magnitude 1, when expressed in <see cref="UnitOfEnergy.Joule"/>.</summary>
    public static Energy OneJoule { get; } = UnitOfEnergy.Joule.Energy;
    /// <summary>The <see cref="Energy"/> of magnitude 1, when expressed in <see cref="UnitOfEnergy.Kilojoule"/>.</summary>
    public static Energy OneKilojoule { get; } = UnitOfEnergy.Kilojoule.Energy;
    /// <summary>The <see cref="Energy"/> of magnitude 1, when expressed in <see cref="UnitOfEnergy.Megajoule"/>.</summary>
    public static Energy OneMegajoule { get; } = UnitOfEnergy.Megajoule.Energy;
    /// <summary>The <see cref="Energy"/> of magnitude 1, when expressed in <see cref="UnitOfEnergy.Gigajoule"/>.</summary>
    public static Energy OneGigajoule { get; } = UnitOfEnergy.Gigajoule.Energy;
    /// <summary>The <see cref="Energy"/> of magnitude 1, when expressed in <see cref="UnitOfEnergy.KilowattHour"/>.</summary>
    public static Energy OneKilowattHour { get; } = UnitOfEnergy.KilowattHour.Energy;
    /// <summary>The <see cref="Energy"/> of magnitude 1, when expressed in <see cref="UnitOfEnergy.Calorie"/>.</summary>
    public static Energy OneCalorie { get; } = UnitOfEnergy.Calorie.Energy;
    /// <summary>The <see cref="Energy"/> of magnitude 1, when expressed in <see cref="UnitOfEnergy.Kilocalorie"/>.</summary>
    public static Energy OneKilocalorie { get; } = UnitOfEnergy.Kilocalorie.Energy;

    /// <summary>The magnitude of the <see cref="Energy"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfEnergy)"/> or a pre-defined property
    /// - such as <see cref="Joules"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Energy"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Energy"/>, expressed in <paramref name="unitOfEnergy"/>.</param>
    /// <param name="unitOfEnergy">The <see cref="UnitOfEnergy"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Energy"/> a = 3 * <see cref="Energy.OneJoule"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Energy(Scalar magnitude, UnitOfEnergy unitOfEnergy) : this(magnitude.Magnitude, unitOfEnergy) { }
    /// <summary>Constructs a new <see cref="Energy"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Energy"/>, expressed in <paramref name="unitOfEnergy"/>.</param>
    /// <param name="unitOfEnergy">The <see cref="UnitOfEnergy"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Energy"/> a = 3 * <see cref="Energy.OneJoule"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Energy(double magnitude, UnitOfEnergy unitOfEnergy) : this(magnitude * unitOfEnergy.Energy.Magnitude) { }
    /// <summary>Constructs a new <see cref="Energy"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Energy"/>.</param>
    /// <remarks>Consider preferring <see cref="Energy(Scalar, UnitOfEnergy)"/>.</remarks>
    public Energy(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Energy"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Energy"/>.</param>
    /// <remarks>Consider preferring <see cref="Energy(double, UnitOfEnergy)"/>.</remarks>
    public Energy(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="Energy"/> to an instance of the associated quantity <see cref="KineticEnergy"/>, of equal magnitude.</summary>
    public KineticEnergy AsKineticEnergy => new(Magnitude);
    /// <summary>Converts the <see cref="Energy"/> to an instance of the associated quantity <see cref="PotentialEnergy"/>, of equal magnitude.</summary>
    public PotentialEnergy AsPotentialEnergy => new(Magnitude);
    /// <summary>Converts the <see cref="Energy"/> to an instance of the associated quantity <see cref="Work"/>, of equal magnitude.</summary>
    public Work AsWork => new(Magnitude);
    /// <summary>Converts the <see cref="Energy"/> to an instance of the associated quantity <see cref="Torque"/>, of equal magnitude.</summary>
    public Torque AsTorque => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="Energy"/>, expressed in <see cref="UnitOfEnergy.Joule"/>.</summary>
    public Scalar Joules => InUnit(UnitOfEnergy.Joule);
    /// <summary>Retrieves the magnitude of the <see cref="Energy"/>, expressed in <see cref="UnitOfEnergy.Kilojoule"/>.</summary>
    public Scalar Kilojoules => InUnit(UnitOfEnergy.Kilojoule);
    /// <summary>Retrieves the magnitude of the <see cref="Energy"/>, expressed in <see cref="UnitOfEnergy.Megajoule"/>.</summary>
    public Scalar Megajoules => InUnit(UnitOfEnergy.Megajoule);
    /// <summary>Retrieves the magnitude of the <see cref="Energy"/>, expressed in <see cref="UnitOfEnergy.Gigajoule"/>.</summary>
    public Scalar Gigajoules => InUnit(UnitOfEnergy.Gigajoule);
    /// <summary>Retrieves the magnitude of the <see cref="Energy"/>, expressed in <see cref="UnitOfEnergy.KilowattHour"/>.</summary>
    public Scalar KilowattHours => InUnit(UnitOfEnergy.KilowattHour);
    /// <summary>Retrieves the magnitude of the <see cref="Energy"/>, expressed in <see cref="UnitOfEnergy.Calorie"/>.</summary>
    public Scalar Calories => InUnit(UnitOfEnergy.Calorie);
    /// <summary>Retrieves the magnitude of the <see cref="Energy"/>, expressed in <see cref="UnitOfEnergy.Kilocalorie"/>.</summary>
    public Scalar Kilocalories => InUnit(UnitOfEnergy.Kilocalorie);

    /// <summary>Indicates whether the magnitude of the <see cref="Energy"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Energy"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Energy"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Energy"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Energy"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Energy"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Energy"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Energy"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="Energy"/>.</summary>
    public Energy Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="Energy"/>.</summary>
    public Energy Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="Energy"/>.</summary>
    public Energy Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="Energy"/> to the nearest integer value.</summary>
    public Energy Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Energy other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Energy"/> in the default unit
    /// <see cref="UnitOfEnergy.Joule"/>, followed by the symbol [J].</summary>
    public override string ToString() => $"{Joules} [J]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Energy"/>,
    /// expressed in <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="unitOfEnergy">The <see cref="UnitOfEnergy"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfEnergy unitOfEnergy) => InUnit(this, unitOfEnergy);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Energy"/>,
    /// expressed in <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="energy">The <see cref="Energy"/> to be expressed in <paramref name="unitOfEnergy"/>.</param>
    /// <param name="unitOfEnergy">The <see cref="UnitOfEnergy"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Energy energy, UnitOfEnergy unitOfEnergy) => new(energy.Magnitude / unitOfEnergy.Energy.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Energy"/>.</summary>
    public Energy Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Energy"/> with negated magnitude.</summary>
    public Energy Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Energy"/>.</param>
    public static Energy operator +(Energy x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Energy"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Energy"/>.</param>
    public static Energy operator -(Energy x) => x.Negate();

    /// <summary>Multiplicates the <see cref="Energy"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Energy"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Energy"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Energy"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Energy"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Energy"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Energy"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Energy x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Energy"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Energy"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Energy"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Energy y) => y.Multiply(x);
    /// <summary>Division of the <see cref="Energy"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Energy"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Energy"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Energy x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="Energy"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="Energy"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Energy"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, Energy y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="Energy"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Energy Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Energy"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Energy"/> is scaled.</param>
    public Energy Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Energy"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Energy"/> is divided.</param>
    public Energy Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Energy"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Energy"/> <paramref name="x"/> by this value.</param>
    public static Energy operator %(Energy x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Energy"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Energy"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Energy"/> <paramref name="x"/>.</param>
    public static Energy operator *(Energy x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Energy"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Energy"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Energy"/>, which is scaled by <paramref name="x"/>.</param>
    public static Energy operator *(double x, Energy y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Energy"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Energy"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Energy"/> <paramref name="x"/>.</param>
    public static Energy operator /(Energy x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="Energy"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Energy Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Energy"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Energy"/> is scaled.</param>
    public Energy Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Energy"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Energy"/> is divided.</param>
    public Energy Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Energy"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Energy"/> <paramref name="x"/> by this value.</param>
    public static Energy operator %(Energy x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Energy"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Energy"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Energy"/> <paramref name="x"/>.</param>
    public static Energy operator *(Energy x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Energy"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Energy"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Energy"/>, which is scaled by <paramref name="x"/>.</param>
    public static Energy operator *(Scalar x, Energy y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Energy"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Energy"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Energy"/> <paramref name="x"/>.</param>
    public static Energy operator /(Energy x, Scalar y) => x.Divide(y);

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

    /// <summary>Multiplication of the <see cref="Energy"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Energy"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Energy"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Energy x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="Energy"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Energy"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Energy"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Energy x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Energy"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Energy"/>.</param>
    public static bool operator <(Energy x, Energy y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Energy"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Energy"/>.</param>
    public static bool operator >(Energy x, Energy y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Energy"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Energy"/>.</param>
    public static bool operator <=(Energy x, Energy y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Energy"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Energy"/>.</param>
    public static bool operator >=(Energy x, Energy y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Energy"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(Energy x) => x.ToDouble();

    /// <summary>Converts the <see cref="Energy"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Energy x) => x.ToScalar();

    /// <summary>Constructs the <see cref="Energy"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Energy FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Energy"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Energy(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="Energy"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static Energy FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="Energy"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator Energy(Scalar x) => FromScalar(x);
}
