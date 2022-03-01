#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Speed"/>, describing change in <see cref="Distance"/> over <see cref="Time"/>.
/// This is the magnitude of the vector quantity <see cref="Velocity3"/>, and is expressed in <see cref="UnitOfVelocity"/>, with the SI unit being [m∙s⁻¹].
/// <para>
/// New instances of <see cref="Speed"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfVelocity"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Speed"/> a = 3 * <see cref="Speed.OneMetrePerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Speed"/> d = <see cref="Speed.From(Distance, Time)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="Speed"/> can be retrieved in the desired <see cref="UnitOfVelocity"/> using pre-defined properties,
/// such as <see cref="MetresPerSecond"/>.
/// </para>
/// </summary>
public readonly partial record struct Speed :
    IComparable<Speed>,
    IScalarQuantity,
    IScalableScalarQuantity<Speed>,
    ISquarableScalarQuantity<SpeedSquared>,
    IMultiplicableScalarQuantity<Speed, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Speed, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<Velocity3, Vector3>
{
    /// <summary>The zero-valued <see cref="Speed"/>.</summary>
    public static Speed Zero { get; } = new(0);

    /// <summary>The <see cref="Speed"/> of magnitude 1, when expressed in <see cref="UnitOfVelocity.MetrePerSecond"/>.</summary>
    public static Speed OneMetrePerSecond { get; } = UnitOfVelocity.MetrePerSecond.Speed;
    /// <summary>The <see cref="Speed"/> of magnitude 1, when expressed in <see cref="UnitOfVelocity.KilometrePerSecond"/>.</summary>
    public static Speed OneKilometrePerSecond { get; } = UnitOfVelocity.KilometrePerSecond.Speed;
    /// <summary>The <see cref="Speed"/> of magnitude 1, when expressed in <see cref="UnitOfVelocity.KilometrePerHour"/>.</summary>
    public static Speed OneKilometrePerHour { get; } = UnitOfVelocity.KilometrePerHour.Speed;
    /// <summary>The <see cref="Speed"/> of magnitude 1, when expressed in <see cref="UnitOfVelocity.FootPerSecond"/>.</summary>
    public static Speed OneFootPerSecond { get; } = UnitOfVelocity.FootPerSecond.Speed;
    /// <summary>The <see cref="Speed"/> of magnitude 1, when expressed in <see cref="UnitOfVelocity.YardPerSecond"/>.</summary>
    public static Speed OneYardPerSecond { get; } = UnitOfVelocity.YardPerSecond.Speed;
    /// <summary>The <see cref="Speed"/> of magnitude 1, when expressed in <see cref="UnitOfVelocity.MilePerHour"/>.</summary>
    public static Speed OneMilePerHour { get; } = UnitOfVelocity.MilePerHour.Speed;

    /// <summary>Computes <see cref="Speed"/> according to { √<paramref name="speedSquared"/> }.</summary>
    /// <param name="speedSquared">The square root of this <see cref="SpeedSquared"/> is taken to produce a <see cref="Speed"/>.</param>
    public static Speed From(SpeedSquared speedSquared) => new(Math.Sqrt(speedSquared.Magnitude));

    /// <summary>The magnitude of the <see cref="Speed"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfVelocity)"/> or a pre-defined property
    /// - such as <see cref="MetresPerSecond"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Speed"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfVelocity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Speed"/>, expressed in <paramref name="unitOfVelocity"/>.</param>
    /// <param name="unitOfVelocity">The <see cref="UnitOfVelocity"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Speed"/> a = 3 * <see cref="Speed.OneMetrePerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Speed(Scalar magnitude, UnitOfVelocity unitOfVelocity) : this(magnitude.Magnitude, unitOfVelocity) { }
    /// <summary>Constructs a new <see cref="Speed"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfVelocity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Speed"/>, expressed in <paramref name="unitOfVelocity"/>.</param>
    /// <param name="unitOfVelocity">The <see cref="UnitOfVelocity"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Speed"/> a = 3 * <see cref="Speed.OneMetrePerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Speed(double magnitude, UnitOfVelocity unitOfVelocity) : this(magnitude * unitOfVelocity.Speed.Magnitude) { }
    /// <summary>Constructs a new <see cref="Speed"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Speed"/>.</param>
    /// <remarks>Consider preferring <see cref="Speed(Scalar, UnitOfVelocity)"/>.</remarks>
    public Speed(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Speed"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Speed"/>.</param>
    /// <remarks>Consider preferring <see cref="Speed(double, UnitOfVelocity)"/>.</remarks>
    public Speed(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Speed"/>, expressed in <see cref="UnitOfVelocity.MetrePerSecond"/>.</summary>
    public Scalar MetresPerSecond => InUnit(UnitOfVelocity.MetrePerSecond);
    /// <summary>Retrieves the magnitude of the <see cref="Speed"/>, expressed in <see cref="UnitOfVelocity.KilometrePerSecond"/>.</summary>
    public Scalar KilometresPerSecond => InUnit(UnitOfVelocity.KilometrePerSecond);
    /// <summary>Retrieves the magnitude of the <see cref="Speed"/>, expressed in <see cref="UnitOfVelocity.KilometrePerHour"/>.</summary>
    public Scalar KilometresPerHour => InUnit(UnitOfVelocity.KilometrePerHour);
    /// <summary>Retrieves the magnitude of the <see cref="Speed"/>, expressed in <see cref="UnitOfVelocity.FootPerSecond"/>.</summary>
    public Scalar FeetPerSecond => InUnit(UnitOfVelocity.FootPerSecond);
    /// <summary>Retrieves the magnitude of the <see cref="Speed"/>, expressed in <see cref="UnitOfVelocity.YardPerSecond"/>.</summary>
    public Scalar YardsPerSecond => InUnit(UnitOfVelocity.YardPerSecond);
    /// <summary>Retrieves the magnitude of the <see cref="Speed"/>, expressed in <see cref="UnitOfVelocity.MilePerHour"/>.</summary>
    public Scalar MilesPerHour => InUnit(UnitOfVelocity.MilePerHour);

    /// <summary>Indicates whether the magnitude of the <see cref="Speed"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Speed"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Speed"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Speed"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Speed"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Speed"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Speed"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Speed"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="Speed"/>.</summary>
    public Speed Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="Speed"/>.</summary>
    public Speed Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="Speed"/>.</summary>
    public Speed Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="Speed"/> to the nearest integer value.</summary>
    public Speed Round() => new(Math.Round(Magnitude));

    /// <summary>Computes the square of the <see cref="Speed"/>, producing a <see cref="SpeedSquared"/>.</summary>
    public SpeedSquared Square() => SpeedSquared.From(this);

    /// <inheritdoc/>
    public int CompareTo(Speed other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Speed"/> in the default unit
    /// <see cref="UnitOfVelocity.MetrePerSecond"/>, followed by the symbol [m∙s⁻¹].</summary>
    public override string ToString() => $"{MetresPerSecond} [m∙s⁻¹]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Speed"/>,
    /// expressed in <paramref name="unitOfVelocity"/>.</summary>
    /// <param name="unitOfVelocity">The <see cref="UnitOfVelocity"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfVelocity unitOfVelocity) => InUnit(this, unitOfVelocity);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Speed"/>,
    /// expressed in <paramref name="unitOfVelocity"/>.</summary>
    /// <param name="speed">The <see cref="Speed"/> to be expressed in <paramref name="unitOfVelocity"/>.</param>
    /// <param name="unitOfVelocity">The <see cref="UnitOfVelocity"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Speed speed, UnitOfVelocity unitOfVelocity) => new(speed.Magnitude / unitOfVelocity.Speed.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Speed"/>.</summary>
    public Speed Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Speed"/> with negated magnitude.</summary>
    public Speed Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Speed"/>.</param>
    public static Speed operator +(Speed x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Speed"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Speed"/>.</param>
    public static Speed operator -(Speed x) => x.Negate();

    /// <summary>Multiplicates the <see cref="Speed"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Speed"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Speed"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Speed"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Speed"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Speed"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Speed"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Speed x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Speed"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Speed"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Speed"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Speed y) => y.Multiply(x);
    /// <summary>Division of the <see cref="Speed"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Speed"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Speed"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Speed x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="Speed"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="Speed"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Speed"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, Speed y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="Speed"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Speed Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Speed"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Speed"/> is scaled.</param>
    public Speed Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Speed"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Speed"/> is divided.</param>
    public Speed Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Speed"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Speed"/> <paramref name="x"/> by this value.</param>
    public static Speed operator %(Speed x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Speed"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Speed"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Speed"/> <paramref name="x"/>.</param>
    public static Speed operator *(Speed x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Speed"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Speed"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Speed"/>, which is scaled by <paramref name="x"/>.</param>
    public static Speed operator *(double x, Speed y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Speed"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Speed"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Speed"/> <paramref name="x"/>.</param>
    public static Speed operator /(Speed x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="Speed"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Speed Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Speed"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Speed"/> is scaled.</param>
    public Speed Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Speed"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Speed"/> is divided.</param>
    public Speed Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Speed"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Speed"/> <paramref name="x"/> by this value.</param>
    public static Speed operator %(Speed x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Speed"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Speed"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Speed"/> <paramref name="x"/>.</param>
    public static Speed operator *(Speed x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Speed"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Speed"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Speed"/>, which is scaled by <paramref name="x"/>.</param>
    public static Speed operator *(Scalar x, Speed y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Speed"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Speed"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Speed"/> <paramref name="x"/>.</param>
    public static Speed operator /(Speed x, Scalar y) => x.Divide(y);

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

    /// <summary>Multiplication of the <see cref="Speed"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Speed"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Speed"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Speed x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="Speed"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Speed"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Speed"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Speed x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Multiplicates the <see cref="Speed"/> with the <see cref="Vector3"/> <paramref name="factor"/> to produce
    /// a <see cref="Velocity3"/>.</summary>
    /// <param name="factor">This <see cref="Vector3"/> is multiplied by the <see cref="Speed"/>.</param>
    public Velocity3 Multiply(Vector3 factor) => new(factor * Magnitude);
    /// <summary>Multiplicates the <see cref="Speed"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="Velocity3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Speed"/>.</param>
    public Velocity3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplicates the <see cref="Speed"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="Velocity3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Speed"/>.</param>
    public Velocity3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplication of the <see cref="Speed"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/>
    /// to produce a <see cref="Velocity3"/>.</summary>
    /// <param name="a">This <see cref="Speed"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Speed"/> <paramref name="a"/>.</param>
    public static Velocity3 operator *(Speed a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Speed"/> <paramref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/>
    /// to produce a <see cref="Velocity3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Speed"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Speed"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Velocity3 operator *(Vector3 a, Speed b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="Speed"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="Velocity3"/>.</summary>
    /// <param name="a">This <see cref="Speed"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Speed"/> <paramref name="a"/>.</param>
    public static Velocity3 operator *(Speed a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Speed"/> <paramref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="Velocity3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Speed"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Speed"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Velocity3 operator *((double x, double y, double z) a, Speed b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="Speed"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="Velocity3"/>.</summary>
    /// <param name="a">This <see cref="Speed"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Speed"/> <paramref name="a"/>.</param>
    public static Velocity3 operator *(Speed a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Speed"/> <paramref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="Velocity3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Speed"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Speed"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Velocity3 operator *((Scalar x, Scalar y, Scalar z) a, Speed b) => b.Multiply(a);

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Speed"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Speed"/>.</param>
    public static bool operator <(Speed x, Speed y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Speed"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Speed"/>.</param>
    public static bool operator >(Speed x, Speed y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Speed"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Speed"/>.</param>
    public static bool operator <=(Speed x, Speed y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Speed"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Speed"/>.</param>
    public static bool operator >=(Speed x, Speed y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Speed"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(Speed x) => x.ToDouble();

    /// <summary>Converts the <see cref="Speed"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Speed x) => x.ToScalar();

    /// <summary>Constructs the <see cref="Speed"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Speed FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Speed"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Speed(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="Speed"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static Speed FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="Speed"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator Speed(Scalar x) => FromScalar(x);
}
