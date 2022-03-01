#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="GravitationalAcceleration"/>, describing <see cref="Acceleration"/> caused by gravity.
/// This is the magnitude of the vector quantity <see cref="GravitationalAcceleration3"/>, and is expressed in <see cref="UnitOfAcceleration"/>,
/// with the SI unit being [m∙s⁻²].
/// <para>
/// New instances of <see cref="GravitationalAcceleration"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfAcceleration"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="GravitationalAcceleration"/> a = 3 * <see cref="GravitationalAcceleration.OneMetrePerSecondSquared"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="GravitationalAcceleration"/> d = <see cref="GravitationalAcceleration.From(Weight, Mass)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="GravitationalAcceleration"/> can be retrieved in the desired <see cref="UnitOfAcceleration"/> using pre-defined properties,
/// such as <see cref="MetresPerSecondSquared"/>.
/// </para>
/// </summary>
/// <remarks>
/// <see cref="GravitationalAcceleration"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="Acceleration"/></term>
/// <description>A more general form of <see cref="GravitationalAcceleration"/>, describing any form of acceleration.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct GravitationalAcceleration :
    IComparable<GravitationalAcceleration>,
    IScalarQuantity,
    IScalableScalarQuantity<GravitationalAcceleration>,
    IMultiplicableScalarQuantity<GravitationalAcceleration, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<GravitationalAcceleration, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<GravitationalAcceleration3, Vector3>
{
    /// <summary>The zero-valued <see cref="GravitationalAcceleration"/>.</summary>
    public static GravitationalAcceleration Zero { get; } = new(0);

    /// <summary>The constant <see cref="GravitationalAcceleration"/> <see cref="UnitOfAcceleration.StandardGravity"/>.</summary>
    public static GravitationalAcceleration StandardGravity { get; } = new(1, UnitOfAcceleration.StandardGravity);

    /// <summary>The <see cref="GravitationalAcceleration"/> of magnitude 1, when expressed in <see cref="UnitOfAcceleration.MetrePerSecondSquared"/>.</summary>
    public static GravitationalAcceleration OneMetrePerSecondSquared { get; } = new(1, UnitOfAcceleration.MetrePerSecondSquared);
    /// <summary>The <see cref="GravitationalAcceleration"/> of magnitude 1, when expressed in <see cref="UnitOfAcceleration.FootPerSecondSquared"/>.</summary>
    public static GravitationalAcceleration OneFootPerSecondSquared { get; } = new(1, UnitOfAcceleration.FootPerSecondSquared);
    /// <summary>The <see cref="GravitationalAcceleration"/> of magnitude 1, when expressed in <see cref="UnitOfAcceleration.KilometrePerHourPerSecond"/>.</summary>
    public static GravitationalAcceleration OneKilometrePerHourPerSecond { get; } = new(1, UnitOfAcceleration.KilometrePerHourPerSecond);
    /// <summary>The <see cref="GravitationalAcceleration"/> of magnitude 1, when expressed in <see cref="UnitOfAcceleration.MilePerHourPerSecond"/>.</summary>
    public static GravitationalAcceleration OneMilePerHourPerSecond { get; } = new(1, UnitOfAcceleration.MilePerHourPerSecond);

    /// <summary>The magnitude of the <see cref="GravitationalAcceleration"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAcceleration)"/> or a pre-defined property
    /// - such as <see cref="MetresPerSecondSquared"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="GravitationalAcceleration"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAcceleration"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="GravitationalAcceleration"/>, expressed in <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="unitOfAcceleration">The <see cref="UnitOfAcceleration"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="GravitationalAcceleration"/> a = 3 * <see cref="GravitationalAcceleration.OneMetrePerSecondSquared"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public GravitationalAcceleration(Scalar magnitude, UnitOfAcceleration unitOfAcceleration) : this(magnitude.Magnitude, unitOfAcceleration) { }
    /// <summary>Constructs a new <see cref="GravitationalAcceleration"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAcceleration"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="GravitationalAcceleration"/>, expressed in <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="unitOfAcceleration">The <see cref="UnitOfAcceleration"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="GravitationalAcceleration"/> a = 3 * <see cref="GravitationalAcceleration.OneMetrePerSecondSquared"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public GravitationalAcceleration(double magnitude, UnitOfAcceleration unitOfAcceleration) : this(magnitude * unitOfAcceleration.Acceleration.Magnitude) { }
    /// <summary>Constructs a new <see cref="GravitationalAcceleration"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="GravitationalAcceleration"/>.</param>
    /// <remarks>Consider preferring <see cref="GravitationalAcceleration(Scalar, UnitOfAcceleration)"/>.</remarks>
    public GravitationalAcceleration(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="GravitationalAcceleration"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="GravitationalAcceleration"/>.</param>
    /// <remarks>Consider preferring <see cref="GravitationalAcceleration(double, UnitOfAcceleration)"/>.</remarks>
    public GravitationalAcceleration(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="GravitationalAcceleration"/> to an instance of the associated quantity <see cref="Acceleration"/>, of equal magnitude.</summary>
    public Acceleration AsAcceleration => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="GravitationalAcceleration"/>, expressed in <see cref="UnitOfAcceleration.MetrePerSecondSquared"/>.</summary>
    public Scalar MetresPerSecondSquared => InUnit(UnitOfAcceleration.MetrePerSecondSquared);
    /// <summary>Retrieves the magnitude of the <see cref="GravitationalAcceleration"/>, expressed in <see cref="UnitOfAcceleration.FootPerSecondSquared"/>.</summary>
    public Scalar FootsPerSecondSquared => InUnit(UnitOfAcceleration.FootPerSecondSquared);
    /// <summary>Retrieves the magnitude of the <see cref="GravitationalAcceleration"/>, expressed in <see cref="UnitOfAcceleration.KilometrePerHourPerSecond"/>.</summary>
    public Scalar KilometresPerHourPerSecond => InUnit(UnitOfAcceleration.KilometrePerHourPerSecond);
    /// <summary>Retrieves the magnitude of the <see cref="GravitationalAcceleration"/>, expressed in <see cref="UnitOfAcceleration.MilePerHourPerSecond"/>.</summary>
    public Scalar MilesPerHourPerSecond => InUnit(UnitOfAcceleration.MilePerHourPerSecond);

    /// <summary>The number of multiples of the constant <see cref="UnitOfAcceleration.StandardGravity"/> that the <see cref="GravitationalAcceleration"/> corresponds to.</summary>
    public Scalar StandardGravityMultiples => InUnit(UnitOfAcceleration.StandardGravity);

    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalAcceleration"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalAcceleration"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalAcceleration"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalAcceleration"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalAcceleration"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalAcceleration"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalAcceleration"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="GravitationalAcceleration"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="GravitationalAcceleration"/>.</summary>
    public GravitationalAcceleration Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="GravitationalAcceleration"/>.</summary>
    public GravitationalAcceleration Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="GravitationalAcceleration"/>.</summary>
    public GravitationalAcceleration Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="GravitationalAcceleration"/> to the nearest integer value.</summary>
    public GravitationalAcceleration Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(GravitationalAcceleration other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="GravitationalAcceleration"/> in the default unit
    /// <see cref="UnitOfAcceleration.StandardGravity"/>, followed by the symbol [g].</summary>
    public override string ToString() => $"{StandardGravityMultiples} [g]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="GravitationalAcceleration"/>,
    /// expressed in <paramref name="unitOfAcceleration"/>.</summary>
    /// <param name="unitOfAcceleration">The <see cref="UnitOfAcceleration"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfAcceleration unitOfAcceleration) => InUnit(this, unitOfAcceleration);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="GravitationalAcceleration"/>,
    /// expressed in <paramref name="unitOfAcceleration"/>.</summary>
    /// <param name="gravitationalAcceleration">The <see cref="GravitationalAcceleration"/> to be expressed in <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="unitOfAcceleration">The <see cref="UnitOfAcceleration"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(GravitationalAcceleration gravitationalAcceleration, UnitOfAcceleration unitOfAcceleration) 
    	=> new(gravitationalAcceleration.Magnitude / unitOfAcceleration.Acceleration.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="GravitationalAcceleration"/>.</summary>
    public GravitationalAcceleration Plus() => this;
    /// <summary>Negation, resulting in a <see cref="GravitationalAcceleration"/> with negated magnitude.</summary>
    public GravitationalAcceleration Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="GravitationalAcceleration"/>.</param>
    public static GravitationalAcceleration operator +(GravitationalAcceleration x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="GravitationalAcceleration"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="GravitationalAcceleration"/>.</param>
    public static GravitationalAcceleration operator -(GravitationalAcceleration x) => x.Negate();

    /// <summary>Multiplicates the <see cref="GravitationalAcceleration"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="GravitationalAcceleration"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="GravitationalAcceleration"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="GravitationalAcceleration"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="GravitationalAcceleration"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="GravitationalAcceleration"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(GravitationalAcceleration x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="GravitationalAcceleration"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="GravitationalAcceleration"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="GravitationalAcceleration"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, GravitationalAcceleration y) => y.Multiply(x);
    /// <summary>Division of the <see cref="GravitationalAcceleration"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="GravitationalAcceleration"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(GravitationalAcceleration x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="GravitationalAcceleration"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="GravitationalAcceleration"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="GravitationalAcceleration"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, GravitationalAcceleration y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="GravitationalAcceleration"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public GravitationalAcceleration Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="GravitationalAcceleration"/> is scaled.</param>
    public GravitationalAcceleration Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="GravitationalAcceleration"/> is divided.</param>
    public GravitationalAcceleration Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="GravitationalAcceleration"/> <paramref name="x"/> by this value.</param>
    public static GravitationalAcceleration operator %(GravitationalAcceleration x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="GravitationalAcceleration"/> <paramref name="x"/>.</param>
    public static GravitationalAcceleration operator *(GravitationalAcceleration x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="GravitationalAcceleration"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="GravitationalAcceleration"/>, which is scaled by <paramref name="x"/>.</param>
    public static GravitationalAcceleration operator *(double x, GravitationalAcceleration y) => y.Multiply(x);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="GravitationalAcceleration"/> <paramref name="x"/>.</param>
    public static GravitationalAcceleration operator /(GravitationalAcceleration x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="GravitationalAcceleration"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public GravitationalAcceleration Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="GravitationalAcceleration"/> is scaled.</param>
    public GravitationalAcceleration Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="GravitationalAcceleration"/> is divided.</param>
    public GravitationalAcceleration Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="GravitationalAcceleration"/> <paramref name="x"/> by this value.</param>
    public static GravitationalAcceleration operator %(GravitationalAcceleration x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="GravitationalAcceleration"/> <paramref name="x"/>.</param>
    public static GravitationalAcceleration operator *(GravitationalAcceleration x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="GravitationalAcceleration"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="GravitationalAcceleration"/>, which is scaled by <paramref name="x"/>.</param>
    public static GravitationalAcceleration operator *(Scalar x, GravitationalAcceleration y) => y.Multiply(x);
    /// <summary>Scales the <see cref="GravitationalAcceleration"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="GravitationalAcceleration"/> <paramref name="x"/>.</param>
    public static GravitationalAcceleration operator /(GravitationalAcceleration x, Scalar y) => x.Divide(y);

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

    /// <summary>Multiplication of the <see cref="GravitationalAcceleration"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="GravitationalAcceleration"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(GravitationalAcceleration x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="GravitationalAcceleration"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="GravitationalAcceleration"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="GravitationalAcceleration"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(GravitationalAcceleration x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Multiplicates the <see cref="GravitationalAcceleration"/> with the <see cref="Vector3"/> <paramref name="factor"/> to produce
    /// a <see cref="GravitationalAcceleration3"/>.</summary>
    /// <param name="factor">This <see cref="Vector3"/> is multiplied by the <see cref="GravitationalAcceleration"/>.</param>
    public GravitationalAcceleration3 Multiply(Vector3 factor) => new(factor * Magnitude);
    /// <summary>Multiplicates the <see cref="GravitationalAcceleration"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="GravitationalAcceleration3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="GravitationalAcceleration"/>.</param>
    public GravitationalAcceleration3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplicates the <see cref="GravitationalAcceleration"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="GravitationalAcceleration3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="GravitationalAcceleration"/>.</param>
    public GravitationalAcceleration3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplication of the <see cref="GravitationalAcceleration"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/>
    /// to produce a <see cref="GravitationalAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="GravitationalAcceleration"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="GravitationalAcceleration"/> <paramref name="a"/>.</param>
    public static GravitationalAcceleration3 operator *(GravitationalAcceleration a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="GravitationalAcceleration"/> <paramref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/>
    /// to produce a <see cref="GravitationalAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="GravitationalAcceleration"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="GravitationalAcceleration"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static GravitationalAcceleration3 operator *(Vector3 a, GravitationalAcceleration b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="GravitationalAcceleration"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="GravitationalAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="GravitationalAcceleration"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="GravitationalAcceleration"/> <paramref name="a"/>.</param>
    public static GravitationalAcceleration3 operator *(GravitationalAcceleration a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="GravitationalAcceleration"/> <paramref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="GravitationalAcceleration3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="GravitationalAcceleration"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="GravitationalAcceleration"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static GravitationalAcceleration3 operator *((double x, double y, double z) a, GravitationalAcceleration b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="GravitationalAcceleration"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="GravitationalAcceleration3"/>.</summary>
    /// <param name="a">This <see cref="GravitationalAcceleration"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="GravitationalAcceleration"/> <paramref name="a"/>.</param>
    public static GravitationalAcceleration3 operator *(GravitationalAcceleration a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="GravitationalAcceleration"/> <paramref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="GravitationalAcceleration3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="GravitationalAcceleration"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="GravitationalAcceleration"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static GravitationalAcceleration3 operator *((Scalar x, Scalar y, Scalar z) a, GravitationalAcceleration b) => b.Multiply(a);

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="GravitationalAcceleration"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="GravitationalAcceleration"/>.</param>
    public static bool operator <(GravitationalAcceleration x, GravitationalAcceleration y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="GravitationalAcceleration"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="GravitationalAcceleration"/>.</param>
    public static bool operator >(GravitationalAcceleration x, GravitationalAcceleration y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="GravitationalAcceleration"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="GravitationalAcceleration"/>.</param>
    public static bool operator <=(GravitationalAcceleration x, GravitationalAcceleration y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="GravitationalAcceleration"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="GravitationalAcceleration"/>.</param>
    public static bool operator >=(GravitationalAcceleration x, GravitationalAcceleration y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="GravitationalAcceleration"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(GravitationalAcceleration x) => x.ToDouble();

    /// <summary>Converts the <see cref="GravitationalAcceleration"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(GravitationalAcceleration x) => x.ToScalar();

    /// <summary>Constructs the <see cref="GravitationalAcceleration"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static GravitationalAcceleration FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="GravitationalAcceleration"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator GravitationalAcceleration(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="GravitationalAcceleration"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static GravitationalAcceleration FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="GravitationalAcceleration"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator GravitationalAcceleration(Scalar x) => FromScalar(x);
}
