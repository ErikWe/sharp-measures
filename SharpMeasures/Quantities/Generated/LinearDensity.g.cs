namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="LinearDensity"/>, describing the amount of <see cref="Mass"/> over a <see cref="Length"/>.
/// The quantity is expressed in <see cref="UnitOfLinearDensity"/>, with the SI unit being [kg / m].
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
/// The magnitude of the measure can be retrieved using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfLinearDensity"/>.
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
    public static LinearDensity OneKilogramPerMetre { get; } = new(1, UnitOfLinearDensity.KilogramPerMetre);

    /// <summary>The magnitude of the <see cref="LinearDensity"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="LinearDensity.InKilogramsPerMetre"/>.
    /// <para>This value should only be used (to maximize efficiency) when implementing mathematical operations with other quantities.</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="LinearDensity"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfLinearDensity"/> <paramref name="unitOfLinearDensity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="LinearDensity"/>, in <see cref="UnitOfLinearDensity"/> <paramref name="unitOfLinearDensity"/>.</param>
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
    /// <summary>Constructs a new <see cref="LinearDensity"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfLinearDensity"/> <paramref name="unitOfLinearDensity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="LinearDensity"/>, in <see cref="UnitOfLinearDensity"/> <paramref name="unitOfLinearDensity"/>.</param>
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
    public LinearDensity(double magnitude, UnitOfLinearDensity unitOfLinearDensity) : this(magnitude * unitOfLinearDensity.Factor) { }
    /// <summary>Constructs a new <see cref="LinearDensity"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="LinearDensity"/>.</param>
    /// <remarks>Consider preffering a constructor that requires a <see cref="UnitOfLinearDensity"/> to be specified.</remarks>
    public LinearDensity(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="LinearDensity"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="LinearDensity"/>.</param>
    /// <remarks>Consider preferring a constructor that requires a <see cref="UnitOfLinearDensity"/> to be specified.</remarks>
    public LinearDensity(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="LinearDensity"/>, expressed in unit <see cref="UnitOfLinearDensity.KilogramPerMetre"/>.</summary>
    public Scalar InKilogramsPerMetre => InUnit(UnitOfLinearDensity.KilogramPerMetre);

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

    /// <summary>Produces a <see cref="LinearDensity"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public LinearDensity Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="LinearDensity"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public LinearDensity Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="LinearDensity"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public LinearDensity Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="LinearDensity"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public LinearDensity Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(LinearDensity other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="LinearDensity"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [kg / m]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="LinearDensity"/>, expressed in <see cref="UnitOfLinearDensity"/>
    /// <paramref name="unitOfLinearDensity"/>.</summary>
    /// <param name="unitOfLinearDensity">The <see cref="UnitOfLinearDensity"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfLinearDensity unitOfLinearDensity) => InUnit(this, unitOfLinearDensity);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="LinearDensity"/>, expressed in <see cref="UnitOfLinearDensity"/>
    /// <paramref name="unitOfLinearDensity"/>.</summary>
    /// <param name="linearDensity">The <see cref="LinearDensity"/> to be expressed in <see cref="UnitOfLinearDensity"/> <paramref name="unitOfLinearDensity"/>.</param>
    /// <param name="unitOfLinearDensity">The <see cref="UnitOfLinearDensity"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(LinearDensity linearDensity, UnitOfLinearDensity unitOfLinearDensity) => new(linearDensity.Magnitude / unitOfLinearDensity.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="LinearDensity"/>.</summary>
    public LinearDensity Plus() => this;
    /// <summary>Negation, resulting in a <see cref="LinearDensity"/> with negated magnitude.</summary>
    public LinearDensity Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="LinearDensity"/>.</param>
    public static LinearDensity operator +(LinearDensity x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="LinearDensity"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="LinearDensity"/>.</param>
    public static LinearDensity operator -(LinearDensity x) => x.Negate();

    /// <summary>Multiplies the <see cref="LinearDensity"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="LinearDensity"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="LinearDensity"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="LinearDensity"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="LinearDensity"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="LinearDensity"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="LinearDensity"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(LinearDensity x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="LinearDensity"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="LinearDensity"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="LinearDensity"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, LinearDensity y) => y.Multiply(x);
    /// <summary>Divides the <see cref="LinearDensity"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="LinearDensity"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="LinearDensity"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(LinearDensity x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="LinearDensity"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public LinearDensity Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="LinearDensity"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="LinearDensity"/> is scaled.</param>
    public LinearDensity Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="LinearDensity"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="LinearDensity"/> is divided.</param>
    public LinearDensity Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="LinearDensity"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="LinearDensity"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="LinearDensity"/> <paramref name="x"/> by this value.</param>
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

    /// <summary>Produces a <see cref="LinearDensity"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public LinearDensity Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="LinearDensity"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="LinearDensity"/> is scaled.</param>
    public LinearDensity Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="LinearDensity"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="LinearDensity"/> is divided.</param>
    public LinearDensity Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="LinearDensity"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="LinearDensity"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="LinearDensity"/> <paramref name="x"/> by this value.</param>
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

    /// <summary>Multiplies the <see cref="LinearDensity"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="LinearDensity"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="LinearDensity"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="LinearDensity"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="LinearDensity"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="LinearDensity"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="LinearDensity"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="LinearDensity.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(LinearDensity x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="LinearDensity"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="LinearDensity"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="LinearDensity"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="LinearDensity.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(LinearDensity x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(LinearDensity x, LinearDensity y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(LinearDensity x, LinearDensity y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(LinearDensity x, LinearDensity y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(LinearDensity x, LinearDensity y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="LinearDensity"/> to a <see cref="double"/> with value <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts the <see cref="LinearDensity"/> to a <see cref="double"/> based on the magnitude of the <see cref="LinearDensity"/> <paramref name="x"/>.</summary>
    public static implicit operator double(LinearDensity x) => x.ToDouble();

    /// <summary>Converts the <see cref="LinearDensity"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts the <see cref="LinearDensity"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(LinearDensity x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="LinearDensity"/> of magnitude <paramref name="x"/>.</summary>
    public static LinearDensity FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="LinearDensity"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator LinearDensity(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="LinearDensity"/> of equivalent magnitude.</summary>
    public static LinearDensity FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="LinearDensity"/> of equivalent magnitude.</summary>
    public static explicit operator LinearDensity(Scalar x) => FromScalar(x);
}
