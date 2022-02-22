#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="LinearDensity"/>, describing the amount of <see cref="Mass"/> over a <see cref="Length"/>.
/// The quantity is expressed in <see cref="UnitOfLinearDensity"/>, with the SI unit being [kg∙m⁻¹].
/// <para>
/// New instances of <see cref="LinearDensity"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfLinearDensity"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="LinearDensity"/> a = 3 * <see cref="LinearDensity.OneKilogramPerMetre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="LinearDensity"/> d = <see cref="LinearDensity.From(Mass, Length)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="LinearDensity"/> can be retrieved in the desired <see cref="UnitOfLinearDensity"/> using pre-defined properties,
/// such as <see cref="KilogramsPerMetre"/>.
/// </para>
/// </summary>
public readonly partial record struct LinearDensity :
    IComparable<LinearDensity>,
    IScalarQuantity,
    IScalableScalarQuantity<LinearDensity>,
    IMultiplicableScalarQuantity<LinearDensity, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<LinearDensity, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="LinearDensity"/>.</summary>
    public static LinearDensity Zero { get; } = new(0);

    /// <summary>The <see cref="LinearDensity"/> with magnitude 1, when expressed in unit <see cref="UnitOfLinearDensity.KilogramPerMetre"/>.</summary>
    public static LinearDensity OneKilogramPerMetre { get; } = UnitOfLinearDensity.KilogramPerMetre.LinearDensity;

    /// <summary>The magnitude of the <see cref="LinearDensity"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfLinearDensity)"/> or a pre-defined property
    /// - such as <see cref="KilogramsPerMetre"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="LinearDensity"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfLinearDensity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="LinearDensity"/>, expressed in <paramref name="unitOfLinearDensity"/>.</param>
    /// <param name="unitOfLinearDensity">The <see cref="UnitOfLinearDensity"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="LinearDensity"/> a = 3 * <see cref="LinearDensity.OneKilogramPerMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public LinearDensity(Scalar magnitude, UnitOfLinearDensity unitOfLinearDensity) : this(magnitude.Magnitude, unitOfLinearDensity) { }
    /// <summary>Constructs a new <see cref="LinearDensity"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfLinearDensity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="LinearDensity"/>, expressed in <paramref name="unitOfLinearDensity"/>.</param>
    /// <param name="unitOfLinearDensity">The <see cref="UnitOfLinearDensity"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="LinearDensity"/> a = 3 * <see cref="LinearDensity.OneKilogramPerMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public LinearDensity(double magnitude, UnitOfLinearDensity unitOfLinearDensity) : this(magnitude * unitOfLinearDensity.LinearDensity.Magnitude) { }
    /// <summary>Constructs a new <see cref="LinearDensity"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="LinearDensity"/>.</param>
    /// <remarks>Consider preferring <see cref="LinearDensity(Scalar, UnitOfLinearDensity)"/>.</remarks>
    public LinearDensity(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="LinearDensity"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="LinearDensity"/>.</param>
    /// <remarks>Consider preferring <see cref="LinearDensity(double, UnitOfLinearDensity)"/>.</remarks>
    public LinearDensity(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="LinearDensity"/>, expressed in <see cref="UnitOfLinearDensity.KilogramPerMetre"/>.</summary>
    public Scalar KilogramsPerMetre => InUnit(UnitOfLinearDensity.KilogramPerMetre);

    /// <summary>Indicates whether the magnitude of the <see cref="LinearDensity"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="LinearDensity"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="LinearDensity"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="LinearDensity"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="LinearDensity"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="LinearDensity"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="LinearDensity"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="LinearDensity"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="LinearDensity"/>.</summary>
    public LinearDensity Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="LinearDensity"/>.</summary>
    public LinearDensity Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="LinearDensity"/>.</summary>
    public LinearDensity Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="LinearDensity"/> to the nearest integer value.</summary>
    public LinearDensity Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(LinearDensity other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="LinearDensity"/> in the default unit
    /// <see cref="UnitOfLinearDensity.KilogramPerMetre"/>, followed by the symbol [kg∙m⁻¹].</summary>
    public override string ToString() => $"{KilogramsPerMetre} [kg∙m⁻¹]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="LinearDensity"/>,
    /// expressed in <paramref name="unitOfLinearDensity"/>.</summary>
    /// <param name="unitOfLinearDensity">The <see cref="UnitOfLinearDensity"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfLinearDensity unitOfLinearDensity) => InUnit(this, unitOfLinearDensity);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="LinearDensity"/>,
    /// expressed in <paramref name="unitOfLinearDensity"/>.</summary>
    /// <param name="linearDensity">The <see cref="LinearDensity"/> to be expressed in <paramref name="unitOfLinearDensity"/>.</param>
    /// <param name="unitOfLinearDensity">The <see cref="UnitOfLinearDensity"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(LinearDensity linearDensity, UnitOfLinearDensity unitOfLinearDensity) 
    	=> new(linearDensity.Magnitude / unitOfLinearDensity.LinearDensity.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="LinearDensity"/>.</summary>
    public LinearDensity Plus() => this;
    /// <summary>Negation, resulting in a <see cref="LinearDensity"/> with negated magnitude.</summary>
    public LinearDensity Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="LinearDensity"/>.</param>
    public static LinearDensity operator +(LinearDensity x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="LinearDensity"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="LinearDensity"/>.</param>
    public static LinearDensity operator -(LinearDensity x) => x.Negate();

    /// <summary>Multiplicates the <see cref="LinearDensity"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="LinearDensity"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="LinearDensity"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="LinearDensity"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="LinearDensity"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="LinearDensity"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="LinearDensity"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(LinearDensity x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="LinearDensity"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="LinearDensity"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="LinearDensity"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, LinearDensity y) => y.Multiply(x);
    /// <summary>Division of the <see cref="LinearDensity"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="LinearDensity"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="LinearDensity"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(LinearDensity x, Unhandled y) => x.Divide(y);
    /// <summary>Division of the <see cref="Unhandled"/> quantity <paramref name="x"/> by the <see cref="LinearDensity"/> <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity, which is divided by the <see cref="LinearDensity"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="LinearDensity"/> by which the <see cref="Unhandled"/> quantity <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Unhandled x, LinearDensity y) => new(x.Magnitude / y.Magnitude);

    /// <summary>Computes the remainder from division of the <see cref="LinearDensity"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public LinearDensity Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="LinearDensity"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="LinearDensity"/> is scaled.</param>
    public LinearDensity Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="LinearDensity"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="LinearDensity"/> is divided.</param>
    public LinearDensity Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="LinearDensity"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="LinearDensity"/> <paramref name="x"/> by this value.</param>
    public static LinearDensity operator %(LinearDensity x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="LinearDensity"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="LinearDensity"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="LinearDensity"/> <paramref name="x"/>.</param>
    public static LinearDensity operator *(LinearDensity x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="LinearDensity"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="LinearDensity"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="LinearDensity"/>, which is scaled by <paramref name="x"/>.</param>
    public static LinearDensity operator *(double x, LinearDensity y) => y.Multiply(x);
    /// <summary>Scales the <see cref="LinearDensity"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="LinearDensity"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="LinearDensity"/> <paramref name="x"/>.</param>
    public static LinearDensity operator /(LinearDensity x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="LinearDensity"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public LinearDensity Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="LinearDensity"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="LinearDensity"/> is scaled.</param>
    public LinearDensity Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="LinearDensity"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="LinearDensity"/> is divided.</param>
    public LinearDensity Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="LinearDensity"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="LinearDensity"/> <paramref name="x"/> by this value.</param>
    public static LinearDensity operator %(LinearDensity x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="LinearDensity"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="LinearDensity"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="LinearDensity"/> <paramref name="x"/>.</param>
    public static LinearDensity operator *(LinearDensity x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="LinearDensity"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="LinearDensity"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="LinearDensity"/>, which is scaled by <paramref name="x"/>.</param>
    public static LinearDensity operator *(Scalar x, LinearDensity y) => y.Multiply(x);
    /// <summary>Scales the <see cref="LinearDensity"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="LinearDensity"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="LinearDensity"/> <paramref name="x"/>.</param>
    public static LinearDensity operator /(LinearDensity x, Scalar y) => x.Divide(y);

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

    /// <summary>Multiplication of the <see cref="LinearDensity"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="LinearDensity"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="LinearDensity"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(LinearDensity x, IScalarQuantity y) => x.Multiply(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="LinearDensity"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="LinearDensity"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="LinearDensity"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(LinearDensity x, IScalarQuantity y) => x.Divide(y, (m) => new Unhandled(m));

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="LinearDensity"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="LinearDensity"/>.</param>
    public static bool operator <(LinearDensity x, LinearDensity y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="LinearDensity"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="LinearDensity"/>.</param>
    public static bool operator >(LinearDensity x, LinearDensity y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="LinearDensity"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="LinearDensity"/>.</param>
    public static bool operator <=(LinearDensity x, LinearDensity y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="LinearDensity"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="LinearDensity"/>.</param>
    public static bool operator >=(LinearDensity x, LinearDensity y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="LinearDensity"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(LinearDensity x) => x.ToDouble();

    /// <summary>Converts the <see cref="LinearDensity"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(LinearDensity x) => x.ToScalar();

    /// <summary>Constructs the <see cref="LinearDensity"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static LinearDensity FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="LinearDensity"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator LinearDensity(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="LinearDensity"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static LinearDensity FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="LinearDensity"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator LinearDensity(Scalar x) => FromScalar(x);
}
