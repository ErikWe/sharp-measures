#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Weight"/>, describing <see cref="GravitationalAcceleration"/> of an object with
/// <see cref="Mass"/> - a <see cref="Force"/> caused by gravity. This is the magnitude of the vector quantity <see cref="Weight3"/>, and is expressed
/// in <see cref="UnitOfForce"/>, with the SI unit being [N].
/// <para>
/// New instances of <see cref="Weight"/> can be constructed using the pre-defined propertiies, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfForce"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. Lastly, instances can be constructed from quantities sharing the same unit, using
/// instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Weight"/> a = 3 * <see cref="Weight.OneNewton"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Weight"/> d = <see cref="Weight.From(Mass, GravitationalAcceleration)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Weight"/> e = <see cref="Force.AsWeight"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="Weight"/> can be retrieved in the desired <see cref="UnitOfForce"/> using pre-defined properties,
/// such as <see cref="Newtons"/>
/// </para>
/// </summary>
/// <remarks>
/// <see cref="Weight"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="Force"/></term>
/// <description>Describes any type of force.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct Weight :
    IComparable<Weight>,
    IScalarQuantity,
    IScalableScalarQuantity<Weight>,
    IMultiplicableScalarQuantity<Weight, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Weight, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<Weight3, Vector3>
{
    /// <summary>The zero-valued <see cref="Weight"/>.</summary>
    public static Weight Zero { get; } = new(0);

    /// <summary>The <see cref="Weight"/> with magnitude 1, when expressed in unit <see cref="UnitOfForce.Newton"/>.</summary>
    public static Weight OneNewton { get; } = UnitOfForce.Newton.Force.AsWeight;
    /// <summary>The <see cref="Weight"/> with magnitude 1, when expressed in unit <see cref="UnitOfForce.PoundForce"/>.</summary>
    public static Weight OnePoundForce { get; } = UnitOfForce.PoundForce.Force.AsWeight;

    /// <summary>The magnitude of the <see cref="Weight"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfForce)"/> or a pre-defined property
    /// - such as <see cref="Newtons"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Weight"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfForce"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Weight"/>, expressed in <paramref name="unitOfForce"/>.</param>
    /// <param name="unitOfForce">The <see cref="UnitOfForce"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Weight"/> a = 3 * <see cref="Weight.OneNewton"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Weight(Scalar magnitude, UnitOfForce unitOfForce) : this(magnitude.Magnitude, unitOfForce) { }
    /// <summary>Constructs a new <see cref="Weight"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfForce"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Weight"/>, expressed in <paramref name="unitOfForce"/>.</param>
    /// <param name="unitOfForce">The <see cref="UnitOfForce"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Weight"/> a = 3 * <see cref="Weight.OneNewton"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Weight(double magnitude, UnitOfForce unitOfForce) : this(magnitude * unitOfForce.Force.Magnitude) { }
    /// <summary>Constructs a new <see cref="Weight"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Weight"/>.</param>
    /// <remarks>Consider preferring <see cref="Weight(Scalar, UnitOfForce)"/>.</remarks>
    public Weight(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Weight"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Weight"/>.</param>
    /// <remarks>Consider preferring <see cref="Weight(double, UnitOfForce)"/>.</remarks>
    public Weight(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts the <see cref="Weight"/> to an instance of the associated quantity <see cref="Force"/>, of equal magnitude.</summary>
    public Force AsForce => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="Weight"/>, expressed in <see cref="UnitOfForce.Newton"/>.</summary>
    public Scalar Newtons => InUnit(UnitOfForce.Newton);
    /// <summary>Retrieves the magnitude of the <see cref="Weight"/>, expressed in <see cref="UnitOfForce.PoundForce"/>.</summary>
    public Scalar PoundsForce => InUnit(UnitOfForce.PoundForce);

    /// <summary>Indicates whether the magnitude of the <see cref="Weight"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Weight"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Weight"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Weight"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Weight"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Weight"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Weight"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Weight"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="Weight"/>.</summary>
    public Weight Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="Weight"/>.</summary>
    public Weight Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="Weight"/>.</summary>
    public Weight Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="Weight"/> to the nearest integer value.</summary>
    public Weight Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Weight other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Weight"/> in the default unit
    /// <see cref="UnitOfForce.Newton"/>, followed by the symbol [N].</summary>
    public override string ToString() => $"{Newtons} [N]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Weight"/>,
    /// expressed in <paramref name="unitOfForce"/>.</summary>
    /// <param name="unitOfForce">The <see cref="UnitOfForce"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfForce unitOfForce) => InUnit(this, unitOfForce);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Weight"/>,
    /// expressed in <paramref name="unitOfForce"/>.</summary>
    /// <param name="weight">The <see cref="Weight"/> to be expressed in <paramref name="unitOfForce"/>.</param>
    /// <param name="unitOfForce">The <see cref="UnitOfForce"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Weight weight, UnitOfForce unitOfForce) => new(weight.Magnitude / unitOfForce.Force.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Weight"/>.</summary>
    public Weight Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Weight"/> with negated magnitude.</summary>
    public Weight Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Weight"/>.</param>
    public static Weight operator +(Weight x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Weight"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Weight"/>.</param>
    public static Weight operator -(Weight x) => x.Negate();

    /// <summary>Multiplicates the <see cref="Weight"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Weight"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Weight"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Weight"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Weight"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Weight"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Weight x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Weight"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Weight"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Weight"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Weight y) => y.Multiply(x);
    /// <summary>Division of the <see cref="Weight"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Weight"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Weight x, Unhandled y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="Weight"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Weight Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Weight"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Weight"/> is scaled.</param>
    public Weight Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Weight"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Weight"/> is divided.</param>
    public Weight Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Weight"/> <paramref name="x"/> by this value.</param>
    public static Weight operator %(Weight x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Weight"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Weight"/> <paramref name="x"/>.</param>
    public static Weight operator *(Weight x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Weight"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Weight"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Weight"/>, which is scaled by <paramref name="x"/>.</param>
    public static Weight operator *(double x, Weight y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Weight"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Weight"/> <paramref name="x"/>.</param>
    public static Weight operator /(Weight x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="Weight"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Weight Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Weight"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Weight"/> is scaled.</param>
    public Weight Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Weight"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Weight"/> is divided.</param>
    public Weight Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Weight"/> <paramref name="x"/> by this value.</param>
    public static Weight operator %(Weight x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Weight"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Weight"/> <paramref name="x"/>.</param>
    public static Weight operator *(Weight x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Weight"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Weight"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Weight"/>, which is scaled by <paramref name="x"/>.</param>
    public static Weight operator *(Scalar x, Weight y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Weight"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Weight"/> <paramref name="x"/>.</param>
    public static Weight operator /(Weight x, Scalar y) => x.Divide(y);

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

    /// <summary>Multiplication of the <see cref="Weight"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Weight"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Weight x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="Weight"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Weight"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Weight"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Weight x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Multiplicates the <see cref="Weight"/> with the <see cref="Vector3"/> <paramref name="factor"/> to produce
    /// a <see cref="Weight3"/>.</summary>
    /// <param name="factor">This <see cref="Vector3"/> is multiplied by the <see cref="Weight"/>.</param>
    public Weight3 Multiply(Vector3 factor) => new(factor * Magnitude);
    /// <summary>Multiplicates the <see cref="Weight"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="Weight3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Weight"/>.</param>
    public Weight3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplicates the <see cref="Weight"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="Weight3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Weight"/>.</param>
    public Weight3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplication of the <see cref="Weight"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/>
    /// to produce a <see cref="Weight3"/>.</summary>
    /// <param name="a">This <see cref="Weight"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Weight"/> <paramref name="a"/>.</param>
    public static Weight3 operator *(Weight a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Weight"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/>
    /// to produce a <see cref="Weight3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Weight"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Weight"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Weight3 operator *(Vector3 a, Weight b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="Weight"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="Weight3"/>.</summary>
    /// <param name="a">This <see cref="Weight"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Weight"/> <paramref name="a"/>.</param>
    public static Weight3 operator *(Weight a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Weight"/> <parmref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="Weight3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Weight"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Weight"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Weight3 operator *((double x, double y, double z) a, Weight b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="Weight"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="Weight3"/>.</summary>
    /// <param name="a">This <see cref="Weight"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Weight"/> <paramref name="a"/>.</param>
    public static Weight3 operator *(Weight a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Weight"/> <parmref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="Weight3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Weight"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Weight"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Weight3 operator *((Scalar x, Scalar y, Scalar z) a, Weight b) => b.Multiply(a);

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Weight"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Weight"/>.</param>
    public static bool operator <(Weight x, Weight y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Weight"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Weight"/>.</param>
    public static bool operator >(Weight x, Weight y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Weight"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Weight"/>.</param>
    public static bool operator <=(Weight x, Weight y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Weight"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Weight"/>.</param>
    public static bool operator >=(Weight x, Weight y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Weight"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(Weight x) => x.ToDouble();

    /// <summary>Converts the <see cref="Weight"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Weight x) => x.ToScalar();

    /// <summary>Constructs the <see cref="Weight"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Weight FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Weight"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Weight(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="Weight"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static Weight FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="Weight"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator Weight(Scalar x) => FromScalar(x);
}
