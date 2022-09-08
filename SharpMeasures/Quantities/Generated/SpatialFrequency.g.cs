﻿#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="SpatialFrequency"/>, describing spatial periodicity.
/// <see cref="SpatialFrequency"/> is related to <see cref="Distance"/> similarly to how <see cref="Frequency"/> is related to <see cref="Time"/>.
/// The quantity is expressed in <see cref="UnitOfSpatialFrequency"/>, with the SI unit being [m⁻¹].
/// <para>
/// New instances of <see cref="SpatialFrequency"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfSpatialFrequency"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code><see cref="SpatialFrequency"/> a = 3 * <see cref="SpatialFrequency.OnePerMetre"/>;</code>
/// </item>
/// <item>
/// <code><see cref="SpatialFrequency"/> d = <see cref="SpatialFrequency.From(LinearDensity, Mass)"/>;</code>
/// </item>
/// </list>
/// The magnitude of the <see cref="SpatialFrequency"/> can be retrieved in the desired <see cref="UnitOfSpatialFrequency"/> using pre-defined properties,
/// such as <see cref="PerMetre"/>.
/// </para>
/// </summary>
public readonly partial record struct SpatialFrequency :
    IComparable<SpatialFrequency>,
    IScalarQuantity,
    IScalableScalarQuantity<SpatialFrequency>,
    IInvertibleScalarQuantity<Distance>,
    IMultiplicableScalarQuantity<SpatialFrequency, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<SpatialFrequency, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="SpatialFrequency"/>.</summary>
    public static SpatialFrequency Zero { get; } = new(0);

    /// <summary>The <see cref="SpatialFrequency"/> of magnitude 1, when expressed in <see cref="UnitOfSpatialFrequency.PerMetre"/>.</summary>
    public static SpatialFrequency OnePerMetre { get; } = UnitOfSpatialFrequency.PerMetre.SpatialFrequency;

    /// <summary>Computes <see cref="SpatialFrequency"/> according to { 1 / <paramref name="distance"/> }.</summary>
    /// <summary>Constructs a <see cref="SpatialFrequency"/> by inverting the <see cref="Distance"/> <paramref name="distance"/>.</summary>
    public static SpatialFrequency From(Distance distance) => new(1 / distance.Magnitude);
    /// <summary>Computes <see cref="SpatialFrequency"/> according to { 1 / <paramref name="length"/> }.</summary>
    /// <summary>Constructs a <see cref="SpatialFrequency"/> by inverting the <see cref="Length"/> <paramref name="length"/>.</summary>
    public static SpatialFrequency From(Length length) => new(1 / length.Magnitude);

    /// <summary>The magnitude of the <see cref="SpatialFrequency"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfSpatialFrequency)"/> or a pre-defined property
    /// - such as <see cref="PerMetre"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="SpatialFrequency"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfSpatialFrequency"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpatialFrequency"/>, expressed in <paramref name="unitOfSpatialFrequency"/>.</param>
    /// <param name="unitOfSpatialFrequency">The <see cref="UnitOfSpatialFrequency"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SpatialFrequency"/> a = 3 * <see cref="SpatialFrequency.OnePerMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SpatialFrequency(Scalar magnitude, UnitOfSpatialFrequency unitOfSpatialFrequency) : this(magnitude.Magnitude, unitOfSpatialFrequency) { }
    /// <summary>Constructs a new <see cref="SpatialFrequency"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfSpatialFrequency"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpatialFrequency"/>, expressed in <paramref name="unitOfSpatialFrequency"/>.</param>
    /// <param name="unitOfSpatialFrequency">The <see cref="UnitOfSpatialFrequency"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SpatialFrequency"/> a = 3 * <see cref="SpatialFrequency.OnePerMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SpatialFrequency(double magnitude, UnitOfSpatialFrequency unitOfSpatialFrequency) : this(magnitude * unitOfSpatialFrequency.SpatialFrequency.Magnitude) { }
    /// <summary>Constructs a new <see cref="SpatialFrequency"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpatialFrequency"/>.</param>
    /// <remarks>Consider preferring <see cref="SpatialFrequency(Scalar, UnitOfSpatialFrequency)"/>.</remarks>
    public SpatialFrequency(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="SpatialFrequency"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpatialFrequency"/>.</param>
    /// <remarks>Consider preferring <see cref="SpatialFrequency(double, UnitOfSpatialFrequency)"/>.</remarks>
    public SpatialFrequency(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="SpatialFrequency"/>, expressed in <see cref="UnitOfSpatialFrequency.PerMetre"/>.</summary>
    public Scalar PerMetre => InUnit(UnitOfSpatialFrequency.PerMetre);

    /// <summary>Indicates whether the magnitude of the <see cref="SpatialFrequency"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpatialFrequency"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="SpatialFrequency"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="SpatialFrequency"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpatialFrequency"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpatialFrequency"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="SpatialFrequency"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpatialFrequency"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="SpatialFrequency"/>.</summary>
    public SpatialFrequency Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="SpatialFrequency"/>.</summary>
    public SpatialFrequency Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="SpatialFrequency"/>.</summary>
    public SpatialFrequency Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="SpatialFrequency"/> to the nearest integer value.</summary>
    public SpatialFrequency Round() => new(Math.Round(Magnitude));

    /// <summary>Computes the inverse of the <see cref="SpatialFrequency"/>, producing a <see cref="Distance"/>.</summary>
    public Distance Invert() => Distance.From(this);

    /// <inheritdoc/>
    public int CompareTo(SpatialFrequency other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="SpatialFrequency"/> in the default unit
    /// <see cref="UnitOfSpatialFrequency.PerMetre"/>, followed by the symbol [m⁻¹].</summary>
    public override string ToString() => $"{PerMetre} [m⁻¹]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="SpatialFrequency"/>,
    /// expressed in <paramref name="unitOfSpatialFrequency"/>.</summary>
    /// <param name="unitOfSpatialFrequency">The <see cref="UnitOfSpatialFrequency"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfSpatialFrequency unitOfSpatialFrequency) => InUnit(this, unitOfSpatialFrequency);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="SpatialFrequency"/>,
    /// expressed in <paramref name="unitOfSpatialFrequency"/>.</summary>
    /// <param name="spatialFrequency">The <see cref="SpatialFrequency"/> to be expressed in <paramref name="unitOfSpatialFrequency"/>.</param>
    /// <param name="unitOfSpatialFrequency">The <see cref="UnitOfSpatialFrequency"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(SpatialFrequency spatialFrequency, UnitOfSpatialFrequency unitOfSpatialFrequency) 
    	=> new(spatialFrequency.Magnitude / unitOfSpatialFrequency.SpatialFrequency.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="SpatialFrequency"/>.</summary>
    public SpatialFrequency Plus() => this;
    /// <summary>Negation, resulting in a <see cref="SpatialFrequency"/> with negated magnitude.</summary>
    public SpatialFrequency Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="SpatialFrequency"/>.</param>
    public static SpatialFrequency operator +(SpatialFrequency x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="SpatialFrequency"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="SpatialFrequency"/>.</param>
    public static SpatialFrequency operator -(SpatialFrequency x) => x.Negate();

    /// <summary>Multiplicates the <see cref="SpatialFrequency"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="SpatialFrequency"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="SpatialFrequency"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="SpatialFrequency"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="SpatialFrequency"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SpatialFrequency"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(SpatialFrequency x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="SpatialFrequency"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="SpatialFrequency"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="SpatialFrequency"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, SpatialFrequency y) => y.Multiply(x);
    /// <summary>Division of the <see cref="SpatialFrequency"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SpatialFrequency"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(SpatialFrequency x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="SpatialFrequency"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="SpatialFrequency"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpatialFrequency"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, SpatialFrequency y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="SpatialFrequency"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public SpatialFrequency Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="SpatialFrequency"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SpatialFrequency"/> is scaled.</param>
    public SpatialFrequency Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="SpatialFrequency"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SpatialFrequency"/> is divided.</param>
    public SpatialFrequency Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="SpatialFrequency"/> <paramref name="x"/> by this value.</param>
    public static SpatialFrequency operator %(SpatialFrequency x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SpatialFrequency"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SpatialFrequency"/> <paramref name="x"/>.</param>
    public static SpatialFrequency operator *(SpatialFrequency x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SpatialFrequency"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SpatialFrequency"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpatialFrequency"/>, which is scaled by <paramref name="x"/>.</param>
    public static SpatialFrequency operator *(double x, SpatialFrequency y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SpatialFrequency"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SpatialFrequency"/> <paramref name="x"/>.</param>
    public static SpatialFrequency operator /(SpatialFrequency x, double y) => x.Divide(y);
    /// <summary>Inverts the <see cref="SpatialFrequency"/> <paramref name="y"/> to produce a <see cref="Distance"/>, which is then scaled by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="SpatialFrequency"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpatialFrequency"/>, which is inverted to a <see cref="Distance"/> and scaled by <paramref name="x"/>.</param>
    public static Distance operator /(double x, SpatialFrequency y) => new(x / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="SpatialFrequency"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public SpatialFrequency Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="SpatialFrequency"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SpatialFrequency"/> is scaled.</param>
    public SpatialFrequency Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="SpatialFrequency"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SpatialFrequency"/> is divided.</param>
    public SpatialFrequency Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="SpatialFrequency"/> <paramref name="x"/> by this value.</param>
    public static SpatialFrequency operator %(SpatialFrequency x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SpatialFrequency"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SpatialFrequency"/> <paramref name="x"/>.</param>
    public static SpatialFrequency operator *(SpatialFrequency x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SpatialFrequency"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SpatialFrequency"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpatialFrequency"/>, which is scaled by <paramref name="x"/>.</param>
    public static SpatialFrequency operator *(Scalar x, SpatialFrequency y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SpatialFrequency"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SpatialFrequency"/> <paramref name="x"/>.</param>
    public static SpatialFrequency operator /(SpatialFrequency x, Scalar y) => x.Divide(y);
    /// <summary>Inverts the <see cref="SpatialFrequency"/> <paramref name="y"/> to produce a <see cref="Distance"/>,
    /// which is then scaled by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the inverted <see cref="SpatialFrequency"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpatialFrequency"/>, which is inverted to a <see cref="Distance"/> and scaled by <paramref name="x"/>.</param>
    public static Distance operator /(Scalar x, SpatialFrequency y) => new(x / y.Magnitude);

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

    /// <summary>Multiplication of the <see cref="SpatialFrequency"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="SpatialFrequency"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(SpatialFrequency x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="SpatialFrequency"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpatialFrequency"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="SpatialFrequency"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(SpatialFrequency x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SpatialFrequency"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="SpatialFrequency"/>.</param>
    public static bool operator <(SpatialFrequency x, SpatialFrequency y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SpatialFrequency"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="SpatialFrequency"/>.</param>
    public static bool operator >(SpatialFrequency x, SpatialFrequency y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SpatialFrequency"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="SpatialFrequency"/>.</param>
    public static bool operator <=(SpatialFrequency x, SpatialFrequency y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SpatialFrequency"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="SpatialFrequency"/>.</param>
    public static bool operator >=(SpatialFrequency x, SpatialFrequency y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="SpatialFrequency"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(SpatialFrequency x) => x.ToDouble();

    /// <summary>Converts the <see cref="SpatialFrequency"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(SpatialFrequency x) => x.ToScalar();

    /// <summary>Constructs the <see cref="SpatialFrequency"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static SpatialFrequency FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="SpatialFrequency"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator SpatialFrequency(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="SpatialFrequency"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static SpatialFrequency FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="SpatialFrequency"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator SpatialFrequency(Scalar x) => FromScalar(x);
}