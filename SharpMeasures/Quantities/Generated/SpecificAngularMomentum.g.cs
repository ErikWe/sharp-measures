#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="SpecificAngularMomentum"/>, describing the <see cref="AngularMomentum"/> per <see cref="Mass"/> of an object.
/// This is the magnitude of the vector quantity <see cref="SpecificAngularMomentum3"/>, and is expressed in <see cref="UnitOfSpecificAngularMomentum"/>, with the SI unit being [m²∙s⁻¹].
/// <para>
/// New instances of <see cref="SpecificAngularMomentum"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfSpecificAngularMomentum"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="SpecificAngularMomentum"/> a = 3 * <see cref="SpecificAngularMomentum.OneSquareMetrePerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="SpecificAngularMomentum"/> d = <see cref="SpecificAngularMomentum.From(AngularMomentum, Mass)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="SpecificAngularMomentum"/> can be retrieved in the desired <see cref="UnitOfSpecificAngularMomentum"/> using pre-defined properties,
/// such as <see cref="SquareMetresPerSecond"/>.
/// </para>
/// </summary>
public readonly partial record struct SpecificAngularMomentum :
    IComparable<SpecificAngularMomentum>,
    IScalarQuantity,
    IScalableScalarQuantity<SpecificAngularMomentum>,
    IMultiplicableScalarQuantity<SpecificAngularMomentum, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<SpecificAngularMomentum, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<SpecificAngularMomentum3, Vector3>
{
    /// <summary>The zero-valued <see cref="SpecificAngularMomentum"/>.</summary>
    public static SpecificAngularMomentum Zero { get; } = new(0);

    /// <summary>The <see cref="SpecificAngularMomentum"/> of magnitude 1, when expressed in <see cref="UnitOfSpecificAngularMomentum.SquareMetrePerSecond"/>.</summary>
    public static SpecificAngularMomentum OneSquareMetrePerSecond { get; } = UnitOfSpecificAngularMomentum.SquareMetrePerSecond.SpecificAngularMomentum;

    /// <summary>The magnitude of the <see cref="SpecificAngularMomentum"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfSpecificAngularMomentum)"/> or a pre-defined property
    /// - such as <see cref="SquareMetresPerSecond"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="SpecificAngularMomentum"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfSpecificAngularMomentum"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpecificAngularMomentum"/>, expressed in <paramref name="unitOfSpecificAngularMomentum"/>.</param>
    /// <param name="unitOfSpecificAngularMomentum">The <see cref="UnitOfSpecificAngularMomentum"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SpecificAngularMomentum"/> a = 3 * <see cref="SpecificAngularMomentum.OneSquareMetrePerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SpecificAngularMomentum(Scalar magnitude, UnitOfSpecificAngularMomentum unitOfSpecificAngularMomentum) : this(magnitude.Magnitude, unitOfSpecificAngularMomentum) { }
    /// <summary>Constructs a new <see cref="SpecificAngularMomentum"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfSpecificAngularMomentum"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpecificAngularMomentum"/>, expressed in <paramref name="unitOfSpecificAngularMomentum"/>.</param>
    /// <param name="unitOfSpecificAngularMomentum">The <see cref="UnitOfSpecificAngularMomentum"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SpecificAngularMomentum"/> a = 3 * <see cref="SpecificAngularMomentum.OneSquareMetrePerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SpecificAngularMomentum(double magnitude, UnitOfSpecificAngularMomentum unitOfSpecificAngularMomentum) : 
    	this(magnitude * unitOfSpecificAngularMomentum.SpecificAngularMomentum.Magnitude) { }
    /// <summary>Constructs a new <see cref="SpecificAngularMomentum"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpecificAngularMomentum"/>.</param>
    /// <remarks>Consider preferring <see cref="SpecificAngularMomentum(Scalar, UnitOfSpecificAngularMomentum)"/>.</remarks>
    public SpecificAngularMomentum(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="SpecificAngularMomentum"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SpecificAngularMomentum"/>.</param>
    /// <remarks>Consider preferring <see cref="SpecificAngularMomentum(double, UnitOfSpecificAngularMomentum)"/>.</remarks>
    public SpecificAngularMomentum(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="SpecificAngularMomentum"/>, expressed in <see cref="UnitOfSpecificAngularMomentum.SquareMetrePerSecond"/>.</summary>
    public Scalar SquareMetresPerSecond => InUnit(UnitOfSpecificAngularMomentum.SquareMetrePerSecond);

    /// <summary>Indicates whether the magnitude of the <see cref="SpecificAngularMomentum"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpecificAngularMomentum"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="SpecificAngularMomentum"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="SpecificAngularMomentum"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpecificAngularMomentum"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpecificAngularMomentum"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="SpecificAngularMomentum"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SpecificAngularMomentum"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="SpecificAngularMomentum"/>.</summary>
    public SpecificAngularMomentum Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="SpecificAngularMomentum"/>.</summary>
    public SpecificAngularMomentum Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="SpecificAngularMomentum"/>.</summary>
    public SpecificAngularMomentum Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="SpecificAngularMomentum"/> to the nearest integer value.</summary>
    public SpecificAngularMomentum Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(SpecificAngularMomentum other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="SpecificAngularMomentum"/> in the default unit
    /// <see cref="UnitOfSpecificAngularMomentum.SquareMetrePerSecond"/>, followed by the symbol [m²∙s⁻¹].</summary>
    public override string ToString() => $"{SquareMetresPerSecond} [m²∙s⁻¹]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="SpecificAngularMomentum"/>,
    /// expressed in <paramref name="unitOfSpecificAngularMomentum"/>.</summary>
    /// <param name="unitOfSpecificAngularMomentum">The <see cref="UnitOfSpecificAngularMomentum"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfSpecificAngularMomentum unitOfSpecificAngularMomentum) => InUnit(this, unitOfSpecificAngularMomentum);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="SpecificAngularMomentum"/>,
    /// expressed in <paramref name="unitOfSpecificAngularMomentum"/>.</summary>
    /// <param name="specificAngularMomentum">The <see cref="SpecificAngularMomentum"/> to be expressed in <paramref name="unitOfSpecificAngularMomentum"/>.</param>
    /// <param name="unitOfSpecificAngularMomentum">The <see cref="UnitOfSpecificAngularMomentum"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(SpecificAngularMomentum specificAngularMomentum, UnitOfSpecificAngularMomentum unitOfSpecificAngularMomentum) 
    	=> new(specificAngularMomentum.Magnitude / unitOfSpecificAngularMomentum.SpecificAngularMomentum.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="SpecificAngularMomentum"/>.</summary>
    public SpecificAngularMomentum Plus() => this;
    /// <summary>Negation, resulting in a <see cref="SpecificAngularMomentum"/> with negated magnitude.</summary>
    public SpecificAngularMomentum Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="SpecificAngularMomentum"/>.</param>
    public static SpecificAngularMomentum operator +(SpecificAngularMomentum x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="SpecificAngularMomentum"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="SpecificAngularMomentum"/>.</param>
    public static SpecificAngularMomentum operator -(SpecificAngularMomentum x) => x.Negate();

    /// <summary>Multiplicates the <see cref="SpecificAngularMomentum"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="SpecificAngularMomentum"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="SpecificAngularMomentum"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="SpecificAngularMomentum"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="SpecificAngularMomentum"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpecificAngularMomentum"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SpecificAngularMomentum"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(SpecificAngularMomentum x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="SpecificAngularMomentum"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="SpecificAngularMomentum"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="SpecificAngularMomentum"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, SpecificAngularMomentum y) => y.Multiply(x);
    /// <summary>Division of the <see cref="SpecificAngularMomentum"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpecificAngularMomentum"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SpecificAngularMomentum"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(SpecificAngularMomentum x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="SpecificAngularMomentum"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="SpecificAngularMomentum"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpecificAngularMomentum"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, SpecificAngularMomentum y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="SpecificAngularMomentum"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public SpecificAngularMomentum Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="SpecificAngularMomentum"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SpecificAngularMomentum"/> is scaled.</param>
    public SpecificAngularMomentum Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="SpecificAngularMomentum"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SpecificAngularMomentum"/> is divided.</param>
    public SpecificAngularMomentum Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpecificAngularMomentum"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="SpecificAngularMomentum"/> <paramref name="x"/> by this value.</param>
    public static SpecificAngularMomentum operator %(SpecificAngularMomentum x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SpecificAngularMomentum"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpecificAngularMomentum"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SpecificAngularMomentum"/> <paramref name="x"/>.</param>
    public static SpecificAngularMomentum operator *(SpecificAngularMomentum x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SpecificAngularMomentum"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SpecificAngularMomentum"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpecificAngularMomentum"/>, which is scaled by <paramref name="x"/>.</param>
    public static SpecificAngularMomentum operator *(double x, SpecificAngularMomentum y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SpecificAngularMomentum"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpecificAngularMomentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SpecificAngularMomentum"/> <paramref name="x"/>.</param>
    public static SpecificAngularMomentum operator /(SpecificAngularMomentum x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="SpecificAngularMomentum"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public SpecificAngularMomentum Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="SpecificAngularMomentum"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SpecificAngularMomentum"/> is scaled.</param>
    public SpecificAngularMomentum Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="SpecificAngularMomentum"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SpecificAngularMomentum"/> is divided.</param>
    public SpecificAngularMomentum Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpecificAngularMomentum"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="SpecificAngularMomentum"/> <paramref name="x"/> by this value.</param>
    public static SpecificAngularMomentum operator %(SpecificAngularMomentum x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SpecificAngularMomentum"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpecificAngularMomentum"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SpecificAngularMomentum"/> <paramref name="x"/>.</param>
    public static SpecificAngularMomentum operator *(SpecificAngularMomentum x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SpecificAngularMomentum"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SpecificAngularMomentum"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SpecificAngularMomentum"/>, which is scaled by <paramref name="x"/>.</param>
    public static SpecificAngularMomentum operator *(Scalar x, SpecificAngularMomentum y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SpecificAngularMomentum"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SpecificAngularMomentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SpecificAngularMomentum"/> <paramref name="x"/>.</param>
    public static SpecificAngularMomentum operator /(SpecificAngularMomentum x, Scalar y) => x.Divide(y);

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

    /// <summary>Multiplication of the <see cref="SpecificAngularMomentum"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpecificAngularMomentum"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="SpecificAngularMomentum"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(SpecificAngularMomentum x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="SpecificAngularMomentum"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SpecificAngularMomentum"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="SpecificAngularMomentum"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(SpecificAngularMomentum x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Multiplicates the <see cref="SpecificAngularMomentum"/> with the <see cref="Vector3"/> <paramref name="factor"/> to produce
    /// a <see cref="SpecificAngularMomentum3"/>.</summary>
    /// <param name="factor">This <see cref="Vector3"/> is multiplied by the <see cref="SpecificAngularMomentum"/>.</param>
    public SpecificAngularMomentum3 Multiply(Vector3 factor) => new(factor * Magnitude);
    /// <summary>Multiplicates the <see cref="SpecificAngularMomentum"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="SpecificAngularMomentum3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="SpecificAngularMomentum"/>.</param>
    public SpecificAngularMomentum3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplicates the <see cref="SpecificAngularMomentum"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="SpecificAngularMomentum3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="SpecificAngularMomentum"/>.</param>
    public SpecificAngularMomentum3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplication of the <see cref="SpecificAngularMomentum"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/>
    /// to produce a <see cref="SpecificAngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="SpecificAngularMomentum"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="SpecificAngularMomentum"/> <paramref name="a"/>.</param>
    public static SpecificAngularMomentum3 operator *(SpecificAngularMomentum a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="SpecificAngularMomentum"/> <paramref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/>
    /// to produce a <see cref="SpecificAngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="SpecificAngularMomentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="SpecificAngularMomentum"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static SpecificAngularMomentum3 operator *(Vector3 a, SpecificAngularMomentum b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="SpecificAngularMomentum"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="SpecificAngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="SpecificAngularMomentum"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="SpecificAngularMomentum"/> <paramref name="a"/>.</param>
    public static SpecificAngularMomentum3 operator *(SpecificAngularMomentum a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="SpecificAngularMomentum"/> <paramref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="SpecificAngularMomentum3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="SpecificAngularMomentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="SpecificAngularMomentum"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static SpecificAngularMomentum3 operator *((double x, double y, double z) a, SpecificAngularMomentum b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="SpecificAngularMomentum"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="SpecificAngularMomentum3"/>.</summary>
    /// <param name="a">This <see cref="SpecificAngularMomentum"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="SpecificAngularMomentum"/> <paramref name="a"/>.</param>
    public static SpecificAngularMomentum3 operator *(SpecificAngularMomentum a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="SpecificAngularMomentum"/> <paramref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="SpecificAngularMomentum3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="SpecificAngularMomentum"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="SpecificAngularMomentum"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static SpecificAngularMomentum3 operator *((Scalar x, Scalar y, Scalar z) a, SpecificAngularMomentum b) => b.Multiply(a);

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SpecificAngularMomentum"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="SpecificAngularMomentum"/>.</param>
    public static bool operator <(SpecificAngularMomentum x, SpecificAngularMomentum y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SpecificAngularMomentum"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="SpecificAngularMomentum"/>.</param>
    public static bool operator >(SpecificAngularMomentum x, SpecificAngularMomentum y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SpecificAngularMomentum"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="SpecificAngularMomentum"/>.</param>
    public static bool operator <=(SpecificAngularMomentum x, SpecificAngularMomentum y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SpecificAngularMomentum"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="SpecificAngularMomentum"/>.</param>
    public static bool operator >=(SpecificAngularMomentum x, SpecificAngularMomentum y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="SpecificAngularMomentum"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(SpecificAngularMomentum x) => x.ToDouble();

    /// <summary>Converts the <see cref="SpecificAngularMomentum"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(SpecificAngularMomentum x) => x.ToScalar();

    /// <summary>Constructs the <see cref="SpecificAngularMomentum"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static SpecificAngularMomentum FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="SpecificAngularMomentum"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator SpecificAngularMomentum(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="SpecificAngularMomentum"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static SpecificAngularMomentum FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="SpecificAngularMomentum"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator SpecificAngularMomentum(Scalar x) => FromScalar(x);
}
