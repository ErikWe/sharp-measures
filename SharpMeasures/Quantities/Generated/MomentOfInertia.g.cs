namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="MomentOfInertia"/>, describes the resistance to rotation of an object. This is similar to
/// how <see cref="Mass"/> can be seen as the resistance to translation of an object. The quantity is expressed in <see cref="UnitOfMomentOfInertia"/>,
/// with the SI unit being [kg * m²].
/// <para>
/// New instances of <see cref="MomentOfInertia"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfMomentOfInertia"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="MomentOfInertia"/> a = 3 * <see cref="MomentOfInertia.OneKilogramMetreSquared"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="#Param:quantity"/> d = <see cref="MomentOfInertia.From(AngularMomentum, AngularSpeed)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="MomentOfInertia"/> can be retrieved in the desired <see cref="UnitOfMomentOfInertia"/> using pre-defined properties,
/// such as <see cref="KilogramMetresSquared"/>.
/// </para>
/// </summary>
public readonly partial record struct MomentOfInertia :
    IComparable<MomentOfInertia>,
    IScalarQuantity,
    IScalableScalarQuantity<MomentOfInertia>,
    IMultiplicableScalarQuantity<MomentOfInertia, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<MomentOfInertia, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="MomentOfInertia"/>.</summary>
    public static MomentOfInertia Zero { get; } = new(0);

    /// <summary>The <see cref="MomentOfInertia"/> with magnitude 1, when expressed in unit <see cref="UnitOfMomentOfInertia.KilogramMetreSquared"/>.</summary>
    public static MomentOfInertia OneKilogramMetreSquared { get; } = new(1, UnitOfMomentOfInertia.KilogramMetreSquared);

    /// <summary>The magnitude of the <see cref="MomentOfInertia"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfMomentOfInertia)"/> or a pre-defined property
    /// - such as <see cref="KilogramMetresSquared"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="MomentOfInertia"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfMomentOfInertia"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="MomentOfInertia"/>, expressed in <paramref name="unitOfMomentOfInertia"/>.</param>
    /// <param name="unitOfMomentOfInertia">The <see cref="UnitOfMomentOfInertia"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="MomentOfInertia"/> a = 3 * <see cref="MomentOfInertia.OneKilogramMetreSquared"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public MomentOfInertia(Scalar magnitude, UnitOfMomentOfInertia unitOfMomentOfInertia) : this(magnitude.Magnitude, unitOfMomentOfInertia) { }
    /// <summary>Constructs a new <see cref="MomentOfInertia"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfMomentOfInertia"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="MomentOfInertia"/>, expressed in <paramref name="unitOfMomentOfInertia"/>.</param>
    /// <param name="unitOfMomentOfInertia">The <see cref="UnitOfMomentOfInertia"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="MomentOfInertia"/> a = 3 * <see cref="MomentOfInertia.OneKilogramMetreSquared"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public MomentOfInertia(double magnitude, UnitOfMomentOfInertia unitOfMomentOfInertia) : this(magnitude * unitOfMomentOfInertia.Factor) { }
    /// <summary>Constructs a new <see cref="MomentOfInertia"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="MomentOfInertia"/>.</param>
    /// <remarks>Consider preferring <see cref="MomentOfInertia(Scalar, UnitOfMomentOfInertia)"/>.</remarks>
    public MomentOfInertia(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="MomentOfInertia"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="MomentOfInertia"/>.</param>
    /// <remarks>Consider preferring <see cref="MomentOfInertia(double, UnitOfMomentOfInertia)"/>.</remarks>
    public MomentOfInertia(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="MomentOfInertia"/>, expressed in <see cref="UnitOfMomentOfInertia.KilogramMetreSquared"/>.</summary>
    public Scalar KilogramMetresSquared => InUnit(UnitOfMomentOfInertia.KilogramMetreSquared);

    /// <summary>Indicates whether the magnitude of the <see cref="MomentOfInertia"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="MomentOfInertia"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="MomentOfInertia"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="MomentOfInertia"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="MomentOfInertia"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="MomentOfInertia"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="MomentOfInertia"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="MomentOfInertia"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="MomentOfInertia"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public MomentOfInertia Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="MomentOfInertia"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public MomentOfInertia Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="MomentOfInertia"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public MomentOfInertia Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="MomentOfInertia"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public MomentOfInertia Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(MomentOfInertia other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="MomentOfInertia"/> (in SI units), and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [kg * m^2]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="MomentOfInertia"/>,
    /// expressed in <paramref name="unitOfMomentOfInertia"/>.</summary>
    /// <param name="unitOfMomentOfInertia">The <see cref="UnitOfMomentOfInertia"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfMomentOfInertia unitOfMomentOfInertia) => InUnit(this, unitOfMomentOfInertia);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="MomentOfInertia"/>,
    /// expressed in <paramref name="unitOfMomentOfInertia"/>.</summary>
    /// <param name="momentOfInertia">The <see cref="MomentOfInertia"/> to be expressed in <paramref name="unitOfMomentOfInertia"/>.</param>
    /// <param name="unitOfMomentOfInertia">The <see cref="UnitOfMomentOfInertia"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(MomentOfInertia momentOfInertia, UnitOfMomentOfInertia unitOfMomentOfInertia) => new(momentOfInertia.Magnitude / unitOfMomentOfInertia.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="MomentOfInertia"/>.</summary>
    public MomentOfInertia Plus() => this;
    /// <summary>Negation, resulting in a <see cref="MomentOfInertia"/> with negated magnitude.</summary>
    public MomentOfInertia Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="MomentOfInertia"/>.</param>
    public static MomentOfInertia operator +(MomentOfInertia x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="MomentOfInertia"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="MomentOfInertia"/>.</param>
    public static MomentOfInertia operator -(MomentOfInertia x) => x.Negate();

    /// <summary>Multiplies the <see cref="MomentOfInertia"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="MomentOfInertia"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="MomentOfInertia"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="MomentOfInertia"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="MomentOfInertia"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="MomentOfInertia"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="MomentOfInertia"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(MomentOfInertia x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="MomentOfInertia"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="MomentOfInertia"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="MomentOfInertia"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, MomentOfInertia y) => y.Multiply(x);
    /// <summary>Divides the <see cref="MomentOfInertia"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="MomentOfInertia"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="MomentOfInertia"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(MomentOfInertia x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="MomentOfInertia"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public MomentOfInertia Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="MomentOfInertia"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="MomentOfInertia"/> is scaled.</param>
    public MomentOfInertia Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="MomentOfInertia"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="MomentOfInertia"/> is divided.</param>
    public MomentOfInertia Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="MomentOfInertia"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="MomentOfInertia"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="MomentOfInertia"/> <paramref name="x"/> by this value.</param>
    public static MomentOfInertia operator %(MomentOfInertia x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="MomentOfInertia"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="MomentOfInertia"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="MomentOfInertia"/> <paramref name="x"/>.</param>
    public static MomentOfInertia operator *(MomentOfInertia x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="MomentOfInertia"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="MomentOfInertia"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="MomentOfInertia"/>, which is scaled by <paramref name="x"/>.</param>
    public static MomentOfInertia operator *(double x, MomentOfInertia y) => y.Multiply(x);
    /// <summary>Scales the <see cref="MomentOfInertia"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="MomentOfInertia"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="MomentOfInertia"/> <paramref name="x"/>.</param>
    public static MomentOfInertia operator /(MomentOfInertia x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="MomentOfInertia"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public MomentOfInertia Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="MomentOfInertia"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="MomentOfInertia"/> is scaled.</param>
    public MomentOfInertia Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="MomentOfInertia"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="MomentOfInertia"/> is divided.</param>
    public MomentOfInertia Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="MomentOfInertia"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="MomentOfInertia"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="MomentOfInertia"/> <paramref name="x"/> by this value.</param>
    public static MomentOfInertia operator %(MomentOfInertia x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="MomentOfInertia"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="MomentOfInertia"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="MomentOfInertia"/> <paramref name="x"/>.</param>
    public static MomentOfInertia operator *(MomentOfInertia x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="MomentOfInertia"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="MomentOfInertia"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="MomentOfInertia"/>, which is scaled by <paramref name="x"/>.</param>
    public static MomentOfInertia operator *(Scalar x, MomentOfInertia y) => y.Multiply(x);
    /// <summary>Scales the <see cref="MomentOfInertia"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="MomentOfInertia"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="MomentOfInertia"/> <paramref name="x"/>.</param>
    public static MomentOfInertia operator /(MomentOfInertia x, Scalar y) => x.Divide(y);

    /// <inheritdoc/>
    public TProductScalarQuantity Multiply<TProductScalarQuantity, TFactorScalarQuantity>(TFactorScalarQuantity factor, Func<double, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorScalarQuantity : IScalarQuantity
        => factory(Magnitude * factor.Magnitude);
    /// <inheritdoc/>
    public TQuotientScalarQuantity Divide<TQuotientScalarQuantity, TDivisorScalarQuantity>(TDivisorScalarQuantity divisor, Func<double, TQuotientScalarQuantity> factory)
        where TQuotientScalarQuantity : IScalarQuantity
        where TDivisorScalarQuantity : IScalarQuantity
        => factory(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="MomentOfInertia"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="MomentOfInertia"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="MomentOfInertia"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity, Func{double, TProductScalarQuantity})"/>.</remarks>
    public static Unhandled operator *(MomentOfInertia x, IScalarQuantity y) => x.Multiply<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));
    /// <summary>Divides the <see cref="MomentOfInertia"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="MomentOfInertia"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="MomentOfInertia"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity, Func{double, TQuotientScalarQuantity})"/>.</remarks>
    public static Unhandled operator /(MomentOfInertia x, IScalarQuantity y) => x.Divide<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(MomentOfInertia x, MomentOfInertia y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(MomentOfInertia x, MomentOfInertia y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(MomentOfInertia x, MomentOfInertia y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(MomentOfInertia x, MomentOfInertia y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="MomentOfInertia"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static implicit operator double(MomentOfInertia x) => x.ToDouble();

    /// <summary>Converts the <see cref="MomentOfInertia"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(MomentOfInertia x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="MomentOfInertia"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static MomentOfInertia FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="MomentOfInertia"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator MomentOfInertia(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="MomentOfInertia"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static MomentOfInertia FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="MomentOfInertia"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator MomentOfInertia(Scalar x) => FromScalar(x);
}
