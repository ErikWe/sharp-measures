#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Impulse"/>, describing a change in the <see cref="Momentum"/> of an object.
/// This is the magnitude of the vector quantity <see cref="Momentum3"/>, and is expressed in <see cref="UnitOfImpulse"/>, with the SI unit being [N∙s].
/// <para>
/// New instances of <see cref="Impulse"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfImpulse"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code><see cref="Impulse"/> a = 3 * <see cref="Impulse.OneNewtonSecond"/>;</code>
/// </item>
/// <item>
/// <code><see cref="Impulse"/> d = <see cref="Impulse.From(Force, Time)"/>;</code>
/// </item>
/// <item>
/// <code><see cref="Impulse"/> e = <see cref="Momentum.AsImpulse"/>;</code>
/// </item>
/// </list>
/// The magnitude of the <see cref="Impulse"/> can be retrieved in the desired <see cref="UnitOfImpulse"/> using pre-defined properties,
/// such as <see cref="NewtonSeconds"/>
/// </para>
/// </summary>
/// <remarks>
/// <see cref="Impulse"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="Momentum"/></term>
/// <description>A property of an object with <see cref="Mass"/> in motion.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct Impulse :
    IComparable<Impulse>,
    IScalarQuantity,
    IScalableScalarQuantity<Impulse>,
    IMultiplicableScalarQuantity<Impulse, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Impulse, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<Impulse3, Vector3>
{
    /// <summary>The zero-valued <see cref="Impulse"/>.</summary>
    public static Impulse Zero { get; } = new(0);

    /// <summary>The <see cref="Impulse"/> of magnitude 1, when expressed in <see cref="UnitOfImpulse.NewtonSecond"/>.</summary>
    public static Impulse OneNewtonSecond { get; } = UnitOfImpulse.NewtonSecond.Impulse;

    /// <summary>The magnitude of the <see cref="Impulse"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfImpulse)"/> or a pre-defined property
    /// - such as <see cref="NewtonSeconds"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Impulse"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfImpulse"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Impulse"/>, expressed in <paramref name="unitOfImpulse"/>.</param>
    /// <param name="unitOfImpulse">The <see cref="UnitOfImpulse"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Impulse"/> a = 3 * <see cref="Impulse.OneNewtonSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Impulse(Scalar magnitude, UnitOfImpulse unitOfImpulse) : this(magnitude.Magnitude, unitOfImpulse) { }
    /// <summary>Constructs a new <see cref="Impulse"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfImpulse"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Impulse"/>, expressed in <paramref name="unitOfImpulse"/>.</param>
    /// <param name="unitOfImpulse">The <see cref="UnitOfImpulse"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Impulse"/> a = 3 * <see cref="Impulse.OneNewtonSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Impulse(double magnitude, UnitOfImpulse unitOfImpulse) : this(magnitude * unitOfImpulse.Impulse.Magnitude) { }
    /// <summary>Constructs a new <see cref="Impulse"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Impulse"/>.</param>
    /// <remarks>Consider preferring <see cref="Impulse(Scalar, UnitOfImpulse)"/>.</remarks>
    public Impulse(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Impulse"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Impulse"/>.</param>
    /// <remarks>Consider preferring <see cref="Impulse(double, UnitOfImpulse)"/>.</remarks>
    public Impulse(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="Impulse"/> to an instance of the associated quantity <see cref="Momentum"/>, of equal magnitude.</summary>
    public Momentum AsMomentum => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="Impulse"/>, expressed in <see cref="UnitOfImpulse.NewtonSecond"/>.</summary>
    public Scalar NewtonSeconds => InUnit(UnitOfImpulse.NewtonSecond);

    /// <summary>Indicates whether the magnitude of the <see cref="Impulse"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Impulse"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Impulse"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Impulse"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Impulse"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Impulse"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Impulse"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Impulse"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="Impulse"/>.</summary>
    public Impulse Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="Impulse"/>.</summary>
    public Impulse Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="Impulse"/>.</summary>
    public Impulse Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="Impulse"/> to the nearest integer value.</summary>
    public Impulse Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Impulse other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Impulse"/> in the default unit
    /// <see cref="UnitOfImpulse.NewtonSecond"/>, followed by the symbol [N∙s].</summary>
    public override string ToString() => $"{NewtonSeconds} [N∙s]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Impulse"/>,
    /// expressed in <paramref name="unitOfImpulse"/>.</summary>
    /// <param name="unitOfImpulse">The <see cref="UnitOfImpulse"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfImpulse unitOfImpulse) => InUnit(this, unitOfImpulse);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Impulse"/>,
    /// expressed in <paramref name="unitOfImpulse"/>.</summary>
    /// <param name="impulse">The <see cref="Impulse"/> to be expressed in <paramref name="unitOfImpulse"/>.</param>
    /// <param name="unitOfImpulse">The <see cref="UnitOfImpulse"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Impulse impulse, UnitOfImpulse unitOfImpulse) => new(impulse.Magnitude / unitOfImpulse.Impulse.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Impulse"/>.</summary>
    public Impulse Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Impulse"/> with negated magnitude.</summary>
    public Impulse Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Impulse"/>.</param>
    public static Impulse operator +(Impulse x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Impulse"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Impulse"/>.</param>
    public static Impulse operator -(Impulse x) => x.Negate();

    /// <summary>Multiplicates the <see cref="Impulse"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Impulse"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Impulse"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Impulse"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Impulse"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Impulse"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Impulse x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Impulse"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Impulse"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Impulse"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Impulse y) => y.Multiply(x);
    /// <summary>Division of the <see cref="Impulse"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Impulse"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Impulse x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="Impulse"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="Impulse"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Impulse"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, Impulse y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="Impulse"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Impulse Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Impulse"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Impulse"/> is scaled.</param>
    public Impulse Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Impulse"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Impulse"/> is divided.</param>
    public Impulse Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Impulse"/> <paramref name="x"/> by this value.</param>
    public static Impulse operator %(Impulse x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Impulse"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Impulse"/> <paramref name="x"/>.</param>
    public static Impulse operator *(Impulse x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Impulse"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Impulse"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Impulse"/>, which is scaled by <paramref name="x"/>.</param>
    public static Impulse operator *(double x, Impulse y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Impulse"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Impulse"/> <paramref name="x"/>.</param>
    public static Impulse operator /(Impulse x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="Impulse"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Impulse Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Impulse"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Impulse"/> is scaled.</param>
    public Impulse Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Impulse"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Impulse"/> is divided.</param>
    public Impulse Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Impulse"/> <paramref name="x"/> by this value.</param>
    public static Impulse operator %(Impulse x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Impulse"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Impulse"/> <paramref name="x"/>.</param>
    public static Impulse operator *(Impulse x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Impulse"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Impulse"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Impulse"/>, which is scaled by <paramref name="x"/>.</param>
    public static Impulse operator *(Scalar x, Impulse y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Impulse"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Impulse"/> <paramref name="x"/>.</param>
    public static Impulse operator /(Impulse x, Scalar y) => x.Divide(y);

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

    /// <summary>Multiplication of the <see cref="Impulse"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Impulse"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Impulse x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="Impulse"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Impulse"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Impulse"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Impulse x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Multiplicates the <see cref="Impulse"/> with the <see cref="Vector3"/> <paramref name="factor"/> to produce
    /// a <see cref="Impulse3"/>.</summary>
    /// <param name="factor">This <see cref="Vector3"/> is multiplied by the <see cref="Impulse"/>.</param>
    public Impulse3 Multiply(Vector3 factor) => new(factor * Magnitude);
    /// <summary>Multiplicates the <see cref="Impulse"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="Impulse3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Impulse"/>.</param>
    public Impulse3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplicates the <see cref="Impulse"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="Impulse3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Impulse"/>.</param>
    public Impulse3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplication of the <see cref="Impulse"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/>
    /// to produce a <see cref="Impulse3"/>.</summary>
    /// <param name="a">This <see cref="Impulse"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Impulse"/> <paramref name="a"/>.</param>
    public static Impulse3 operator *(Impulse a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Impulse"/> <paramref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/>
    /// to produce a <see cref="Impulse3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Impulse"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Impulse"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Impulse3 operator *(Vector3 a, Impulse b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="Impulse"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="Impulse3"/>.</summary>
    /// <param name="a">This <see cref="Impulse"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Impulse"/> <paramref name="a"/>.</param>
    public static Impulse3 operator *(Impulse a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Impulse"/> <paramref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="Impulse3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Impulse"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Impulse"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Impulse3 operator *((double x, double y, double z) a, Impulse b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="Impulse"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="Impulse3"/>.</summary>
    /// <param name="a">This <see cref="Impulse"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Impulse"/> <paramref name="a"/>.</param>
    public static Impulse3 operator *(Impulse a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Impulse"/> <paramref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="Impulse3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Impulse"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Impulse"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Impulse3 operator *((Scalar x, Scalar y, Scalar z) a, Impulse b) => b.Multiply(a);

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Impulse"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Impulse"/>.</param>
    public static bool operator <(Impulse x, Impulse y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Impulse"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Impulse"/>.</param>
    public static bool operator >(Impulse x, Impulse y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Impulse"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Impulse"/>.</param>
    public static bool operator <=(Impulse x, Impulse y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Impulse"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Impulse"/>.</param>
    public static bool operator >=(Impulse x, Impulse y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Impulse"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(Impulse x) => x.ToDouble();

    /// <summary>Converts the <see cref="Impulse"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Impulse x) => x.ToScalar();

    /// <summary>Constructs the <see cref="Impulse"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Impulse FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Impulse"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Impulse(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="Impulse"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static Impulse FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="Impulse"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator Impulse(Scalar x) => FromScalar(x);
}
