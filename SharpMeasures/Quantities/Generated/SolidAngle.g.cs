#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="SolidAngle"/>, describing an <see cref="Area"/> on a sphere - the square of <see cref="Angle"/>.
/// The quantity is expressed in <see cref="UnitOfSolidAngle"/>, with the SI unit being [sr].
/// <para>
/// New instances of <see cref="SolidAngle"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfSolidAngle"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="SolidAngle"/> a = 3 * <see cref="SolidAngle.OneSteradian"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="SolidAngle"/> d = <see cref="SolidAngle.From(Angle, Angle)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="SolidAngle"/> can be retrieved in the desired <see cref="UnitOfSolidAngle"/> using pre-defined properties,
/// such as <see cref="Steradians"/>.
/// </para>
/// </summary>
public readonly partial record struct SolidAngle :
    IComparable<SolidAngle>,
    IScalarQuantity,
    IScalableScalarQuantity<SolidAngle>,
    ISquareRootableScalarQuantity<Angle>,
    IMultiplicableScalarQuantity<SolidAngle, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<SolidAngle, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="SolidAngle"/>.</summary>
    public static SolidAngle Zero { get; } = new(0);

    /// <summary>The <see cref="SolidAngle"/> of magnitude 1, when expressed in <see cref="UnitOfSolidAngle.Steradian"/>.</summary>
    public static SolidAngle OneSteradian { get; } = UnitOfSolidAngle.Steradian.SolidAngle;
    /// <summary>The <see cref="SolidAngle"/> of magnitude 1, when expressed in <see cref="UnitOfSolidAngle.SquareRadian"/>.</summary>
    public static SolidAngle OneSquareRadian { get; } = UnitOfSolidAngle.SquareRadian.SolidAngle;
    /// <summary>The <see cref="SolidAngle"/> of magnitude 1, when expressed in <see cref="UnitOfSolidAngle.SquareDegree"/>.</summary>
    public static SolidAngle OneSquareDegree { get; } = UnitOfSolidAngle.SquareDegree.SolidAngle;
    /// <summary>The <see cref="SolidAngle"/> of magnitude 1, when expressed in <see cref="UnitOfSolidAngle.SquareArcminute"/>.</summary>
    public static SolidAngle OneSquareArcminute { get; } = UnitOfSolidAngle.SquareArcminute.SolidAngle;
    /// <summary>The <see cref="SolidAngle"/> of magnitude 1, when expressed in <see cref="UnitOfSolidAngle.SquareArcsecond"/>.</summary>
    public static SolidAngle OneSquareArcsecond { get; } = UnitOfSolidAngle.SquareArcsecond.SolidAngle;

    /// <summary>Computes <see cref="SolidAngle"/> according to { <paramref name="angle"/>² }.</summary>
    /// <param name="angle">This <see cref="Angle"/> is squared to produce a <see cref="SolidAngle"/>.</param>
    public static SolidAngle From(Angle angle) => new(Math.Pow(angle.Magnitude, 2));
    /// <summary>Computes <see cref="SolidAngle"/> according to { <paramref name="angle1"/> ∙ <paramref name="angle2"/> }.</summary>
    /// <param name="angle1">This <see cref="Angle"/> is multiplied by <paramref name="angle2"/> to
    /// produce a <see cref="SolidAngle"/>.</param>
    /// <param name="angle2">This <see cref="Angle"/> is multiplied by <paramref name="angle1"/> to
    /// produce a <see cref="SolidAngle"/>.</param>
    public static SolidAngle From(Angle angle1, Angle angle2) => new(angle1.Magnitude * angle2.Magnitude);

    /// <summary>The magnitude of the <see cref="SolidAngle"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfSolidAngle)"/> or a pre-defined property
    /// - such as <see cref="Steradians"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="SolidAngle"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfSolidAngle"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SolidAngle"/>, expressed in <paramref name="unitOfSolidAngle"/>.</param>
    /// <param name="unitOfSolidAngle">The <see cref="UnitOfSolidAngle"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SolidAngle"/> a = 3 * <see cref="SolidAngle.OneSteradian"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SolidAngle(Scalar magnitude, UnitOfSolidAngle unitOfSolidAngle) : this(magnitude.Magnitude, unitOfSolidAngle) { }
    /// <summary>Constructs a new <see cref="SolidAngle"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfSolidAngle"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SolidAngle"/>, expressed in <paramref name="unitOfSolidAngle"/>.</param>
    /// <param name="unitOfSolidAngle">The <see cref="UnitOfSolidAngle"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SolidAngle"/> a = 3 * <see cref="SolidAngle.OneSteradian"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SolidAngle(double magnitude, UnitOfSolidAngle unitOfSolidAngle) : this(magnitude * unitOfSolidAngle.SolidAngle.Magnitude) { }
    /// <summary>Constructs a new <see cref="SolidAngle"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SolidAngle"/>.</param>
    /// <remarks>Consider preferring <see cref="SolidAngle(Scalar, UnitOfSolidAngle)"/>.</remarks>
    public SolidAngle(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="SolidAngle"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SolidAngle"/>.</param>
    /// <remarks>Consider preferring <see cref="SolidAngle(double, UnitOfSolidAngle)"/>.</remarks>
    public SolidAngle(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="SolidAngle"/>, expressed in <see cref="UnitOfSolidAngle.Steradian"/>.</summary>
    public Scalar Steradians => InUnit(UnitOfSolidAngle.Steradian);
    /// <summary>Retrieves the magnitude of the <see cref="SolidAngle"/>, expressed in <see cref="UnitOfSolidAngle.SquareRadian"/>.</summary>
    public Scalar SquareRadians => InUnit(UnitOfSolidAngle.SquareRadian);
    /// <summary>Retrieves the magnitude of the <see cref="SolidAngle"/>, expressed in <see cref="UnitOfSolidAngle.SquareDegree"/>.</summary>
    public Scalar SquareDegrees => InUnit(UnitOfSolidAngle.SquareDegree);
    /// <summary>Retrieves the magnitude of the <see cref="SolidAngle"/>, expressed in <see cref="UnitOfSolidAngle.SquareArcminute"/>.</summary>
    public Scalar SquareArcminutes => InUnit(UnitOfSolidAngle.SquareArcminute);
    /// <summary>Retrieves the magnitude of the <see cref="SolidAngle"/>, expressed in <see cref="UnitOfSolidAngle.SquareArcsecond"/>.</summary>
    public Scalar SquareArcseconds => InUnit(UnitOfSolidAngle.SquareArcsecond);

    /// <summary>Indicates whether the magnitude of the <see cref="SolidAngle"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SolidAngle"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="SolidAngle"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="SolidAngle"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SolidAngle"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SolidAngle"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="SolidAngle"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="SolidAngle"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="SolidAngle"/>.</summary>
    public SolidAngle Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="SolidAngle"/>.</summary>
    public SolidAngle Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="SolidAngle"/>.</summary>
    public SolidAngle Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="SolidAngle"/> to the nearest integer value.</summary>
    public SolidAngle Round() => new(Math.Round(Magnitude));

    /// <summary>Computes the square root of the <see cref="SolidAngle"/>, producing a <see cref="Angle"/>.</summary>
    public Angle SquareRoot() => Angle.From(this);

    /// <inheritdoc/>
    public int CompareTo(SolidAngle other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="SolidAngle"/> in the default unit
    /// <see cref="UnitOfSolidAngle.Steradian"/>, followed by the symbol [sr].</summary>
    public override string ToString() => $"{Steradians} [sr]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="SolidAngle"/>,
    /// expressed in <paramref name="unitOfSolidAngle"/>.</summary>
    /// <param name="unitOfSolidAngle">The <see cref="UnitOfSolidAngle"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfSolidAngle unitOfSolidAngle) => InUnit(this, unitOfSolidAngle);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="SolidAngle"/>,
    /// expressed in <paramref name="unitOfSolidAngle"/>.</summary>
    /// <param name="solidAngle">The <see cref="SolidAngle"/> to be expressed in <paramref name="unitOfSolidAngle"/>.</param>
    /// <param name="unitOfSolidAngle">The <see cref="UnitOfSolidAngle"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(SolidAngle solidAngle, UnitOfSolidAngle unitOfSolidAngle) => new(solidAngle.Magnitude / unitOfSolidAngle.SolidAngle.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="SolidAngle"/>.</summary>
    public SolidAngle Plus() => this;
    /// <summary>Negation, resulting in a <see cref="SolidAngle"/> with negated magnitude.</summary>
    public SolidAngle Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="SolidAngle"/>.</param>
    public static SolidAngle operator +(SolidAngle x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="SolidAngle"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="SolidAngle"/>.</param>
    public static SolidAngle operator -(SolidAngle x) => x.Negate();

    /// <summary>Multiplicates the <see cref="SolidAngle"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="SolidAngle"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="SolidAngle"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="SolidAngle"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="SolidAngle"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SolidAngle"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(SolidAngle x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="SolidAngle"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="SolidAngle"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="SolidAngle"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, SolidAngle y) => y.Multiply(x);
    /// <summary>Division of the <see cref="SolidAngle"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SolidAngle"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(SolidAngle x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="SolidAngle"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="SolidAngle"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SolidAngle"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, SolidAngle y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="SolidAngle"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public SolidAngle Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="SolidAngle"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SolidAngle"/> is scaled.</param>
    public SolidAngle Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="SolidAngle"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SolidAngle"/> is divided.</param>
    public SolidAngle Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="SolidAngle"/> <paramref name="x"/> by this value.</param>
    public static SolidAngle operator %(SolidAngle x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SolidAngle"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SolidAngle"/> <paramref name="x"/>.</param>
    public static SolidAngle operator *(SolidAngle x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SolidAngle"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SolidAngle"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SolidAngle"/>, which is scaled by <paramref name="x"/>.</param>
    public static SolidAngle operator *(double x, SolidAngle y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SolidAngle"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SolidAngle"/> <paramref name="x"/>.</param>
    public static SolidAngle operator /(SolidAngle x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="SolidAngle"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public SolidAngle Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="SolidAngle"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SolidAngle"/> is scaled.</param>
    public SolidAngle Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="SolidAngle"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SolidAngle"/> is divided.</param>
    public SolidAngle Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="SolidAngle"/> <paramref name="x"/> by this value.</param>
    public static SolidAngle operator %(SolidAngle x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SolidAngle"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SolidAngle"/> <paramref name="x"/>.</param>
    public static SolidAngle operator *(SolidAngle x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SolidAngle"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SolidAngle"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SolidAngle"/>, which is scaled by <paramref name="x"/>.</param>
    public static SolidAngle operator *(Scalar x, SolidAngle y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SolidAngle"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SolidAngle"/> <paramref name="x"/>.</param>
    public static SolidAngle operator /(SolidAngle x, Scalar y) => x.Divide(y);

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

    /// <summary>Multiplication of the <see cref="SolidAngle"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="SolidAngle"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(SolidAngle x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="SolidAngle"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="SolidAngle"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(SolidAngle x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SolidAngle"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="SolidAngle"/>.</param>
    public static bool operator <(SolidAngle x, SolidAngle y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SolidAngle"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="SolidAngle"/>.</param>
    public static bool operator >(SolidAngle x, SolidAngle y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SolidAngle"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="SolidAngle"/>.</param>
    public static bool operator <=(SolidAngle x, SolidAngle y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="SolidAngle"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="SolidAngle"/>.</param>
    public static bool operator >=(SolidAngle x, SolidAngle y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="SolidAngle"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(SolidAngle x) => x.ToDouble();

    /// <summary>Converts the <see cref="SolidAngle"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(SolidAngle x) => x.ToScalar();

    /// <summary>Constructs the <see cref="SolidAngle"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static SolidAngle FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="SolidAngle"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator SolidAngle(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="SolidAngle"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static SolidAngle FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="SolidAngle"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator SolidAngle(Scalar x) => FromScalar(x);
}
