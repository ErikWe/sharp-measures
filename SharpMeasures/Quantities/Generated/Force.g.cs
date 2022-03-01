#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Force"/>, describing <see cref="Acceleration"/> of an object with <see cref="Mass"/>.
/// This is the magnitude of the vector quantity <see cref="Force3"/>, and is expressed in <see cref="UnitOfForce"/>, with the SI unit being [N].
/// <para>
/// New instances of <see cref="Force"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfForce"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Force"/> a = 3 * <see cref="Force.OneNewton"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Force"/> d = <see cref="Force.From(Mass, Acceleration)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Force"/> e = <see cref="Weight.AsForce"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="Force"/> can be retrieved in the desired <see cref="UnitOfForce"/> using pre-defined properties,
/// such as <see cref="Newtons"/>
/// </para>
/// </summary>
/// <remarks>
/// <see cref="Force"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="Weight"/></term>
/// <description>Describes <see cref="Force"/> caused specifically by <see cref="GravitationalAcceleration"/>.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct Force :
    IComparable<Force>,
    IScalarQuantity,
    IScalableScalarQuantity<Force>,
    IMultiplicableScalarQuantity<Force, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Force, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<Force3, Vector3>
{
    /// <summary>The zero-valued <see cref="Force"/>.</summary>
    public static Force Zero { get; } = new(0);

    /// <summary>The <see cref="Force"/> of magnitude 1, when expressed in <see cref="UnitOfForce.Newton"/>.</summary>
    public static Force OneNewton { get; } = UnitOfForce.Newton.Force;
    /// <summary>The <see cref="Force"/> of magnitude 1, when expressed in <see cref="UnitOfForce.PoundForce"/>.</summary>
    public static Force OnePoundForce { get; } = UnitOfForce.PoundForce.Force;

    /// <summary>The magnitude of the <see cref="Force"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfForce)"/> or a pre-defined property
    /// - such as <see cref="Newtons"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Force"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfForce"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Force"/>, expressed in <paramref name="unitOfForce"/>.</param>
    /// <param name="unitOfForce">The <see cref="UnitOfForce"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Force"/> a = 3 * <see cref="Force.OneNewton"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Force(Scalar magnitude, UnitOfForce unitOfForce) : this(magnitude.Magnitude, unitOfForce) { }
    /// <summary>Constructs a new <see cref="Force"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfForce"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Force"/>, expressed in <paramref name="unitOfForce"/>.</param>
    /// <param name="unitOfForce">The <see cref="UnitOfForce"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Force"/> a = 3 * <see cref="Force.OneNewton"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Force(double magnitude, UnitOfForce unitOfForce) : this(magnitude * unitOfForce.Force.Magnitude) { }
    /// <summary>Constructs a new <see cref="Force"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Force"/>.</param>
    /// <remarks>Consider preferring <see cref="Force(Scalar, UnitOfForce)"/>.</remarks>
    public Force(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Force"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Force"/>.</param>
    /// <remarks>Consider preferring <see cref="Force(double, UnitOfForce)"/>.</remarks>
    public Force(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="Force"/> to an instance of the associated quantity <see cref="Weight"/>, of equal magnitude.</summary>
    public Weight AsWeight => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="Force"/>, expressed in <see cref="UnitOfForce.Newton"/>.</summary>
    public Scalar Newtons => InUnit(UnitOfForce.Newton);
    /// <summary>Retrieves the magnitude of the <see cref="Force"/>, expressed in <see cref="UnitOfForce.PoundForce"/>.</summary>
    public Scalar PoundsForce => InUnit(UnitOfForce.PoundForce);

    /// <summary>Indicates whether the magnitude of the <see cref="Force"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Force"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Force"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Force"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Force"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Force"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Force"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Force"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="Force"/>.</summary>
    public Force Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="Force"/>.</summary>
    public Force Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="Force"/>.</summary>
    public Force Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="Force"/> to the nearest integer value.</summary>
    public Force Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Force other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Force"/> in the default unit
    /// <see cref="UnitOfForce.Newton"/>, followed by the symbol [N].</summary>
    public override string ToString() => $"{Newtons} [N]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Force"/>,
    /// expressed in <paramref name="unitOfForce"/>.</summary>
    /// <param name="unitOfForce">The <see cref="UnitOfForce"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfForce unitOfForce) => InUnit(this, unitOfForce);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Force"/>,
    /// expressed in <paramref name="unitOfForce"/>.</summary>
    /// <param name="force">The <see cref="Force"/> to be expressed in <paramref name="unitOfForce"/>.</param>
    /// <param name="unitOfForce">The <see cref="UnitOfForce"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Force force, UnitOfForce unitOfForce) => new(force.Magnitude / unitOfForce.Force.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Force"/>.</summary>
    public Force Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Force"/> with negated magnitude.</summary>
    public Force Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Force"/>.</param>
    public static Force operator +(Force x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Force"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Force"/>.</param>
    public static Force operator -(Force x) => x.Negate();

    /// <summary>Multiplicates the <see cref="Force"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Force"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Force"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Force"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Force"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Force"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Force"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Force x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Force"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Force"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Force"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Force y) => y.Multiply(x);
    /// <summary>Division of the <see cref="Force"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Force"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Force"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Force x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="Force"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="Force"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Force"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, Force y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="Force"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Force Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Force"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Force"/> is scaled.</param>
    public Force Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Force"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Force"/> is divided.</param>
    public Force Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Force"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Force"/> <paramref name="x"/> by this value.</param>
    public static Force operator %(Force x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Force"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Force"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Force"/> <paramref name="x"/>.</param>
    public static Force operator *(Force x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Force"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Force"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Force"/>, which is scaled by <paramref name="x"/>.</param>
    public static Force operator *(double x, Force y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Force"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Force"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Force"/> <paramref name="x"/>.</param>
    public static Force operator /(Force x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="Force"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Force Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Force"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Force"/> is scaled.</param>
    public Force Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Force"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Force"/> is divided.</param>
    public Force Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Force"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Force"/> <paramref name="x"/> by this value.</param>
    public static Force operator %(Force x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Force"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Force"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Force"/> <paramref name="x"/>.</param>
    public static Force operator *(Force x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Force"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Force"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Force"/>, which is scaled by <paramref name="x"/>.</param>
    public static Force operator *(Scalar x, Force y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Force"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Force"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Force"/> <paramref name="x"/>.</param>
    public static Force operator /(Force x, Scalar y) => x.Divide(y);

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

    /// <summary>Multiplication of the <see cref="Force"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Force"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Force"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Force x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="Force"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Force"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Force"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Force x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Multiplicates the <see cref="Force"/> with the <see cref="Vector3"/> <paramref name="factor"/> to produce
    /// a <see cref="Force3"/>.</summary>
    /// <param name="factor">This <see cref="Vector3"/> is multiplied by the <see cref="Force"/>.</param>
    public Force3 Multiply(Vector3 factor) => new(factor * Magnitude);
    /// <summary>Multiplicates the <see cref="Force"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="Force3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Force"/>.</param>
    public Force3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplicates the <see cref="Force"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="Force3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Force"/>.</param>
    public Force3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplication of the <see cref="Force"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/>
    /// to produce a <see cref="Force3"/>.</summary>
    /// <param name="a">This <see cref="Force"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Force"/> <paramref name="a"/>.</param>
    public static Force3 operator *(Force a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Force"/> <paramref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/>
    /// to produce a <see cref="Force3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Force"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Force"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Force3 operator *(Vector3 a, Force b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="Force"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="Force3"/>.</summary>
    /// <param name="a">This <see cref="Force"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Force"/> <paramref name="a"/>.</param>
    public static Force3 operator *(Force a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Force"/> <paramref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="Force3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Force"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Force"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Force3 operator *((double x, double y, double z) a, Force b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="Force"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="Force3"/>.</summary>
    /// <param name="a">This <see cref="Force"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Force"/> <paramref name="a"/>.</param>
    public static Force3 operator *(Force a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Force"/> <paramref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="Force3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Force"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Force"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Force3 operator *((Scalar x, Scalar y, Scalar z) a, Force b) => b.Multiply(a);

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Force"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Force"/>.</param>
    public static bool operator <(Force x, Force y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Force"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Force"/>.</param>
    public static bool operator >(Force x, Force y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Force"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Force"/>.</param>
    public static bool operator <=(Force x, Force y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Force"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Force"/>.</param>
    public static bool operator >=(Force x, Force y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Force"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(Force x) => x.ToDouble();

    /// <summary>Converts the <see cref="Force"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Force x) => x.ToScalar();

    /// <summary>Constructs the <see cref="Force"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Force FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Force"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Force(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="Force"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static Force FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="Force"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator Force(Scalar x) => FromScalar(x);
}
