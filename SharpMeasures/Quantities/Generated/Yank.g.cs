#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Yank"/>, describing change in <see cref="Force"/> over <see cref="Time"/>.
/// This is the magnitude of the vector quantity <see cref="Yank3"/>, and is expressed in <see cref="UnitOfYank"/>, with the SI unit being [N∙s⁻¹].
/// <para>
/// New instances of <see cref="Yank"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfYank"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Yank"/> a = 3 * <see cref="Yank.OneNewtonPerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Yank"/> d = <see cref="Yank.From(Force, Time)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="Yank"/> can be retrieved in the desired <see cref="UnitOfYank"/> using pre-defined properties,
/// such as <see cref="NewtonsPerSecond"/>.
/// </para>
/// </summary>
public readonly partial record struct Yank :
    IComparable<Yank>,
    IScalarQuantity,
    IScalableScalarQuantity<Yank>,
    IMultiplicableScalarQuantity<Yank, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Yank, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<Yank3, Vector3>
{
    /// <summary>The zero-valued <see cref="Yank"/>.</summary>
    public static Yank Zero { get; } = new(0);

    /// <summary>The <see cref="Yank"/> with magnitude 1, when expressed in unit <see cref="UnitOfYank.NewtonPerSecond"/>.</summary>
    public static Yank OneNewtonPerSecond { get; } = UnitOfYank.NewtonPerSecond.Yank;

    /// <summary>The magnitude of the <see cref="Yank"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfYank)"/> or a pre-defined property
    /// - such as <see cref="NewtonsPerSecond"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Yank"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfYank"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Yank"/>, expressed in <paramref name="unitOfYank"/>.</param>
    /// <param name="unitOfYank">The <see cref="UnitOfYank"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Yank"/> a = 3 * <see cref="Yank.OneNewtonPerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Yank(Scalar magnitude, UnitOfYank unitOfYank) : this(magnitude.Magnitude, unitOfYank) { }
    /// <summary>Constructs a new <see cref="Yank"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfYank"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Yank"/>, expressed in <paramref name="unitOfYank"/>.</param>
    /// <param name="unitOfYank">The <see cref="UnitOfYank"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Yank"/> a = 3 * <see cref="Yank.OneNewtonPerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Yank(double magnitude, UnitOfYank unitOfYank) : this(magnitude * unitOfYank.Yank.Magnitude) { }
    /// <summary>Constructs a new <see cref="Yank"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Yank"/>.</param>
    /// <remarks>Consider preferring <see cref="Yank(Scalar, UnitOfYank)"/>.</remarks>
    public Yank(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Yank"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Yank"/>.</param>
    /// <remarks>Consider preferring <see cref="Yank(double, UnitOfYank)"/>.</remarks>
    public Yank(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Yank"/>, expressed in <see cref="UnitOfYank.NewtonPerSecond"/>.</summary>
    public Scalar NewtonsPerSecond => InUnit(UnitOfYank.NewtonPerSecond);

    /// <summary>Indicates whether the magnitude of the <see cref="Yank"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Yank"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Yank"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Yank"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Yank"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Yank"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Yank"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Yank"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="Yank"/>.</summary>
    public Yank Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="Yank"/>.</summary>
    public Yank Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="Yank"/>.</summary>
    public Yank Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="Yank"/> to the nearest integer value.</summary>
    public Yank Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Yank other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Yank"/> in the default unit
    /// <see cref="UnitOfYank.NewtonPerSecond"/>, followed by the symbol [N∙s⁻¹].</summary>
    public override string ToString() => $"{NewtonsPerSecond} [N∙s⁻¹]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Yank"/>,
    /// expressed in <paramref name="unitOfYank"/>.</summary>
    /// <param name="unitOfYank">The <see cref="UnitOfYank"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfYank unitOfYank) => InUnit(this, unitOfYank);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Yank"/>,
    /// expressed in <paramref name="unitOfYank"/>.</summary>
    /// <param name="yank">The <see cref="Yank"/> to be expressed in <paramref name="unitOfYank"/>.</param>
    /// <param name="unitOfYank">The <see cref="UnitOfYank"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Yank yank, UnitOfYank unitOfYank) => new(yank.Magnitude / unitOfYank.Yank.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Yank"/>.</summary>
    public Yank Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Yank"/> with negated magnitude.</summary>
    public Yank Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Yank"/>.</param>
    public static Yank operator +(Yank x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Yank"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Yank"/>.</param>
    public static Yank operator -(Yank x) => x.Negate();

    /// <summary>Multiplicates the <see cref="Yank"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Yank"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Yank"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Yank"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Yank"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Yank"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Yank"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Yank x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Yank"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Yank"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Yank"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Yank y) => y.Multiply(x);
    /// <summary>Division of the <see cref="Yank"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Yank"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Yank"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Yank x, Unhandled y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="Yank"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Yank Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Yank"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Yank"/> is scaled.</param>
    public Yank Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Yank"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Yank"/> is divided.</param>
    public Yank Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Yank"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Yank"/> <paramref name="x"/> by this value.</param>
    public static Yank operator %(Yank x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Yank"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Yank"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Yank"/> <paramref name="x"/>.</param>
    public static Yank operator *(Yank x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Yank"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Yank"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Yank"/>, which is scaled by <paramref name="x"/>.</param>
    public static Yank operator *(double x, Yank y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Yank"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Yank"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Yank"/> <paramref name="x"/>.</param>
    public static Yank operator /(Yank x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="Yank"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Yank Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Yank"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Yank"/> is scaled.</param>
    public Yank Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Yank"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Yank"/> is divided.</param>
    public Yank Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Yank"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Yank"/> <paramref name="x"/> by this value.</param>
    public static Yank operator %(Yank x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Yank"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Yank"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Yank"/> <paramref name="x"/>.</param>
    public static Yank operator *(Yank x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Yank"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Yank"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Yank"/>, which is scaled by <paramref name="x"/>.</param>
    public static Yank operator *(Scalar x, Yank y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Yank"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Yank"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Yank"/> <paramref name="x"/>.</param>
    public static Yank operator /(Yank x, Scalar y) => x.Divide(y);

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

    /// <summary>Multiplication of the <see cref="Yank"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Yank"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Yank"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Yank x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="Yank"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Yank"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Yank"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Yank x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Multiplicates the <see cref="Yank"/> with the <see cref="Vector3"/> <paramref name="factor"/> to produce
    /// a <see cref="Yank3"/>.</summary>
    /// <param name="factor">This <see cref="Vector3"/> is multiplied by the <see cref="Yank"/>.</param>
    public Yank3 Multiply(Vector3 factor) => new(factor * Magnitude);
    /// <summary>Multiplicates the <see cref="Yank"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="Yank3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Yank"/>.</param>
    public Yank3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplicates the <see cref="Yank"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="Yank3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Yank"/>.</param>
    public Yank3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplication of the <see cref="Yank"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/>
    /// to produce a <see cref="Yank3"/>.</summary>
    /// <param name="a">This <see cref="Yank"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Yank"/> <paramref name="a"/>.</param>
    public static Yank3 operator *(Yank a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Yank"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/>
    /// to produce a <see cref="Yank3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Yank"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Yank"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Yank3 operator *(Vector3 a, Yank b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="Yank"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="Yank3"/>.</summary>
    /// <param name="a">This <see cref="Yank"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Yank"/> <paramref name="a"/>.</param>
    public static Yank3 operator *(Yank a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Yank"/> <parmref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="Yank3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Yank"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Yank"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Yank3 operator *((double x, double y, double z) a, Yank b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="Yank"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="Yank3"/>.</summary>
    /// <param name="a">This <see cref="Yank"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Yank"/> <paramref name="a"/>.</param>
    public static Yank3 operator *(Yank a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Yank"/> <parmref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="Yank3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Yank"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Yank"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Yank3 operator *((Scalar x, Scalar y, Scalar z) a, Yank b) => b.Multiply(a);

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Yank"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Yank"/>.</param>
    public static bool operator <(Yank x, Yank y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Yank"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Yank"/>.</param>
    public static bool operator >(Yank x, Yank y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Yank"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Yank"/>.</param>
    public static bool operator <=(Yank x, Yank y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Yank"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Yank"/>.</param>
    public static bool operator >=(Yank x, Yank y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Yank"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(Yank x) => x.ToDouble();

    /// <summary>Converts the <see cref="Yank"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Yank x) => x.ToScalar();

    /// <summary>Constructs the <see cref="Yank"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Yank FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Yank"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Yank(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="Yank"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static Yank FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="Yank"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator Yank(Scalar x) => FromScalar(x);
}
