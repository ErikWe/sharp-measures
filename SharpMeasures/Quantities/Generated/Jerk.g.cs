#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Jerk"/>, describing change in <see cref="Acceleration"/> over <see cref="Time"/>.
/// This is the magnitude of the vector quantity <see cref="Jerk3"/>, and is expressed in <see cref="UnitOfJerk"/>, with the SI unit being [m∙s⁻³].
/// <para>
/// New instances of <see cref="Jerk"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfJerk"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Jerk"/> a = 3 * <see cref="Jerk.OneMetrePerSecondCubed"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Jerk"/> d = <see cref="Jerk.From(Acceleration, Time)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="Jerk"/> can be retrieved in the desired <see cref="UnitOfJerk"/> using pre-defined properties,
/// such as <see cref="MetresPerSecondCubed"/>.
/// </para>
/// </summary>
public readonly partial record struct Jerk :
    IComparable<Jerk>,
    IScalarQuantity,
    IScalableScalarQuantity<Jerk>,
    IMultiplicableScalarQuantity<Jerk, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Jerk, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<Jerk3, Vector3>
{
    /// <summary>The zero-valued <see cref="Jerk"/>.</summary>
    public static Jerk Zero { get; } = new(0);

    /// <summary>The <see cref="Jerk"/> with magnitude 1, when expressed in unit <see cref="UnitOfJerk.MetrePerSecondCubed"/>.</summary>
    public static Jerk OneMetrePerSecondCubed { get; } = UnitOfJerk.MetrePerSecondCubed.Jerk;
    /// <summary>The <see cref="Jerk"/> with magnitude 1, when expressed in unit <see cref="UnitOfJerk.FootPerSecondCubed"/>.</summary>
    public static Jerk OneFootPerSecondCubed { get; } = UnitOfJerk.FootPerSecondCubed.Jerk;

    /// <summary>The magnitude of the <see cref="Jerk"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfJerk)"/> or a pre-defined property
    /// - such as <see cref="MetresPerSecondCubed"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Jerk"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfJerk"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Jerk"/>, expressed in <paramref name="unitOfJerk"/>.</param>
    /// <param name="unitOfJerk">The <see cref="UnitOfJerk"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Jerk"/> a = 3 * <see cref="Jerk.OneMetrePerSecondCubed"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Jerk(Scalar magnitude, UnitOfJerk unitOfJerk) : this(magnitude.Magnitude, unitOfJerk) { }
    /// <summary>Constructs a new <see cref="Jerk"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfJerk"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Jerk"/>, expressed in <paramref name="unitOfJerk"/>.</param>
    /// <param name="unitOfJerk">The <see cref="UnitOfJerk"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Jerk"/> a = 3 * <see cref="Jerk.OneMetrePerSecondCubed"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Jerk(double magnitude, UnitOfJerk unitOfJerk) : this(magnitude * unitOfJerk.Jerk.Magnitude) { }
    /// <summary>Constructs a new <see cref="Jerk"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Jerk"/>.</param>
    /// <remarks>Consider preferring <see cref="Jerk(Scalar, UnitOfJerk)"/>.</remarks>
    public Jerk(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Jerk"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Jerk"/>.</param>
    /// <remarks>Consider preferring <see cref="Jerk(double, UnitOfJerk)"/>.</remarks>
    public Jerk(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Jerk"/>, expressed in <see cref="UnitOfJerk.MetrePerSecondCubed"/>.</summary>
    public Scalar MetresPerSecondCubed => InUnit(UnitOfJerk.MetrePerSecondCubed);
    /// <summary>Retrieves the magnitude of the <see cref="Jerk"/>, expressed in <see cref="UnitOfJerk.FootPerSecondCubed"/>.</summary>
    public Scalar FootsPerSecondCubed => InUnit(UnitOfJerk.FootPerSecondCubed);

    /// <summary>Indicates whether the magnitude of the <see cref="Jerk"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Jerk"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Jerk"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Jerk"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Jerk"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Jerk"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Jerk"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Jerk"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="Jerk"/>.</summary>
    public Jerk Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="Jerk"/>.</summary>
    public Jerk Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="Jerk"/>.</summary>
    public Jerk Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="Jerk"/> to the nearest integer value.</summary>
    public Jerk Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Jerk other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Jerk"/> in the default unit
    /// <see cref="UnitOfJerk.MetrePerSecondCubed"/>, followed by the symbol [m∙s⁻³].</summary>
    public override string ToString() => $"{MetresPerSecondCubed} [m∙s⁻³]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Jerk"/>,
    /// expressed in <paramref name="unitOfJerk"/>.</summary>
    /// <param name="unitOfJerk">The <see cref="UnitOfJerk"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfJerk unitOfJerk) => InUnit(this, unitOfJerk);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Jerk"/>,
    /// expressed in <paramref name="unitOfJerk"/>.</summary>
    /// <param name="jerk">The <see cref="Jerk"/> to be expressed in <paramref name="unitOfJerk"/>.</param>
    /// <param name="unitOfJerk">The <see cref="UnitOfJerk"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Jerk jerk, UnitOfJerk unitOfJerk) => new(jerk.Magnitude / unitOfJerk.Jerk.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Jerk"/>.</summary>
    public Jerk Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Jerk"/> with negated magnitude.</summary>
    public Jerk Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Jerk"/>.</param>
    public static Jerk operator +(Jerk x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Jerk"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Jerk"/>.</param>
    public static Jerk operator -(Jerk x) => x.Negate();

    /// <summary>Multiplicates the <see cref="Jerk"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Jerk"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Jerk"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Jerk"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Jerk"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Jerk"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Jerk x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Jerk"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Jerk"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Jerk"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Jerk y) => y.Multiply(x);
    /// <summary>Division of the <see cref="Jerk"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Jerk"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Jerk x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="Jerk"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="Jerk"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Jerk"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, Jerk y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="Jerk"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Jerk Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Jerk"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Jerk"/> is scaled.</param>
    public Jerk Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Jerk"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Jerk"/> is divided.</param>
    public Jerk Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Jerk"/> <paramref name="x"/> by this value.</param>
    public static Jerk operator %(Jerk x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Jerk"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Jerk"/> <paramref name="x"/>.</param>
    public static Jerk operator *(Jerk x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Jerk"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Jerk"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Jerk"/>, which is scaled by <paramref name="x"/>.</param>
    public static Jerk operator *(double x, Jerk y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Jerk"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Jerk"/> <paramref name="x"/>.</param>
    public static Jerk operator /(Jerk x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="Jerk"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Jerk Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Jerk"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Jerk"/> is scaled.</param>
    public Jerk Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Jerk"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Jerk"/> is divided.</param>
    public Jerk Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Jerk"/> <paramref name="x"/> by this value.</param>
    public static Jerk operator %(Jerk x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Jerk"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Jerk"/> <paramref name="x"/>.</param>
    public static Jerk operator *(Jerk x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Jerk"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Jerk"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Jerk"/>, which is scaled by <paramref name="x"/>.</param>
    public static Jerk operator *(Scalar x, Jerk y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Jerk"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Jerk"/> <paramref name="x"/>.</param>
    public static Jerk operator /(Jerk x, Scalar y) => x.Divide(y);

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

    /// <summary>Multiplication of the <see cref="Jerk"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Jerk"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Jerk x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="Jerk"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Jerk"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Jerk x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Multiplicates the <see cref="Jerk"/> with the <see cref="Vector3"/> <paramref name="factor"/> to produce
    /// a <see cref="Jerk3"/>.</summary>
    /// <param name="factor">This <see cref="Vector3"/> is multiplied by the <see cref="Jerk"/>.</param>
    public Jerk3 Multiply(Vector3 factor) => new(factor * Magnitude);
    /// <summary>Multiplicates the <see cref="Jerk"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="Jerk3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Jerk"/>.</param>
    public Jerk3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplicates the <see cref="Jerk"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="Jerk3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Jerk"/>.</param>
    public Jerk3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplication of the <see cref="Jerk"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/>
    /// to produce a <see cref="Jerk3"/>.</summary>
    /// <param name="a">This <see cref="Jerk"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Jerk"/> <paramref name="a"/>.</param>
    public static Jerk3 operator *(Jerk a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Jerk"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/>
    /// to produce a <see cref="Jerk3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Jerk"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Jerk"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Jerk3 operator *(Vector3 a, Jerk b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="Jerk"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="Jerk3"/>.</summary>
    /// <param name="a">This <see cref="Jerk"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Jerk"/> <paramref name="a"/>.</param>
    public static Jerk3 operator *(Jerk a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Jerk"/> <parmref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="Jerk3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Jerk"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Jerk"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Jerk3 operator *((double x, double y, double z) a, Jerk b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="Jerk"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="Jerk3"/>.</summary>
    /// <param name="a">This <see cref="Jerk"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Jerk"/> <paramref name="a"/>.</param>
    public static Jerk3 operator *(Jerk a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Jerk"/> <parmref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="Jerk3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Jerk"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Jerk"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Jerk3 operator *((Scalar x, Scalar y, Scalar z) a, Jerk b) => b.Multiply(a);

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Jerk"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Jerk"/>.</param>
    public static bool operator <(Jerk x, Jerk y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Jerk"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Jerk"/>.</param>
    public static bool operator >(Jerk x, Jerk y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Jerk"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Jerk"/>.</param>
    public static bool operator <=(Jerk x, Jerk y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Jerk"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Jerk"/>.</param>
    public static bool operator >=(Jerk x, Jerk y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Jerk"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(Jerk x) => x.ToDouble();

    /// <summary>Converts the <see cref="Jerk"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Jerk x) => x.ToScalar();

    /// <summary>Constructs the <see cref="Jerk"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Jerk FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Jerk"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Jerk(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="Jerk"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static Jerk FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="Jerk"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator Jerk(Scalar x) => FromScalar(x);
}
