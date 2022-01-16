namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="LinearDensity"/>, used for describing the amount of <see cref="Mass"/> over a <see cref="Length"/>.
/// <para>
/// New instances of <see cref="LinearDensity"/> can be constructed according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="LinearDensity"/> a = 5 * <see cref="LinearDensity.OneKilogramPerMetre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="LinearDensity"/> b = new(7, <see cref="UnitOfLinearDensity.KilogramPerMetre"/>);
/// </code>
/// </item>
/// </list>
/// Instances of <see cref="LinearDensity"/> may be applied according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Mass"/> c = 3 * <see cref="LinearDensity.OneKilogramPerMetre"/> * <see cref="Length.OneMillimetre"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved in a desired unit according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="LinearDensity.InKilogramsPerMetre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="LinearDensity.InUnit(UnitOfLinearDensity)"/>;
/// </code>
/// </item>
/// </list>
/// </para>
/// </summary>
public readonly partial record struct LinearDensity :
    IComparable<LinearDensity>,
    IScalarQuantity<LinearDensity>,
    IAddableScalarQuantity<LinearDensity, LinearDensity>,
    ISubtractableScalarQuantity<LinearDensity, LinearDensity>,
    IDivisibleScalarQuantity<Scalar, LinearDensity>
{
    /// <summary>The zero-valued <see cref="LinearDensity"/>.</summary>
    public static LinearDensity Zero { get; } = new(0);

    /// <summary>The <see cref="LinearDensity"/> with magnitude 1, when expressed in unit <see cref="UnitOfLinearDensity.KilogramPerMetre"/>.</summary>
    public static LinearDensity OneKilogramPerMetre { get; } = new(1, UnitOfLinearDensity.KilogramPerMetre);

    /// <summary>The magnitude of the <see cref="LinearDensity"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="LinearDensity.InKilogramsPerMetre"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="LinearDensity"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfLinearDensity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="LinearDensity"/>, in unit <paramref name="unitOfLinearDensity"/>.</param>
    /// <param name="unitOfLinearDensity">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="LinearDensity"/> a = 2.6 * <see cref="LinearDensity.OneKilogramPerMetre"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public LinearDensity(double magnitude, UnitOfLinearDensity unitOfLinearDensity) : this(magnitude * unitOfLinearDensity.Factor) { }
    /// <summary>Constructs a new <see cref="LinearDensity"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="LinearDensity"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfLinearDensity"/> to be specified.</remarks>
    public LinearDensity(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="LinearDensity"/>, expressed in unit <see cref="UnitOfLinearDensity.KilogramPerMetre"/>.</summary>
    public Scalar InKilogramsPerMetre => InUnit(UnitOfLinearDensity.KilogramPerMetre);

    /// <inheritdoc/>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <inheritdoc/>
    public bool IsZero => Magnitude == 0;
    /// <inheritdoc/>
    public bool IsPositive => Magnitude > 0;
    /// <inheritdoc/>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <inheritdoc/>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <inheritdoc/>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <inheritdoc/>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <inheritdoc/>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <inheritdoc/>
    public LinearDensity Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public LinearDensity Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public LinearDensity Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public LinearDensity Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(LinearDensity other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="LinearDensity"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [kg * m^-1]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="LinearDensity"/>, expressed in unit <paramref name="unitOfLinearDensity"/>.</summary>
    /// <param name="unitOfLinearDensity">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfLinearDensity unitOfLinearDensity) => InUnit(Magnitude, unitOfLinearDensity);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="LinearDensity"/>, expressed in unit <paramref name="unitOfLinearDensity"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="LinearDensity"/>.</param>
    /// <param name="unitOfLinearDensity">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfLinearDensity unitOfLinearDensity) => new(magnitude / unitOfLinearDensity.Factor);

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

    /// <summary>Adds this instance and the <see cref="LinearDensity"/> <paramref name="term"/>, producing another <see cref="LinearDensity"/>.</summary>
    /// <param name="term">This <see cref="LinearDensity"/> is added to this instance.</param>
    public LinearDensity Add(LinearDensity term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="LinearDensity"/> <paramref name="term"/> from this instance, producing another <see cref="LinearDensity"/>.</summary>
    /// <param name="term">This <see cref="LinearDensity"/> is subtracted from this instance.</param>
    public LinearDensity Subtract(LinearDensity term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="LinearDensity"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="LinearDensity"/>.</summary>
    /// <param name="x">This <see cref="LinearDensity"/> is added to the <see cref="LinearDensity"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="LinearDensity"/> is added to the <see cref="LinearDensity"/> <paramref name="x"/>.</param>
    public static LinearDensity operator +(LinearDensity x, LinearDensity y) => x.Add(y);
    /// <summary>Subtract the <see cref="LinearDensity"/> <paramref name="y"/> from the <see cref="LinearDensity"/> <paramref name="x"/>, producing another <see cref="LinearDensity"/>.</summary>
    /// <param name="x">The <see cref="LinearDensity"/> <paramref name="y"/> is subtracted from this <see cref="LinearDensity"/>.</param>
    /// <param name="y">This <see cref="LinearDensity"/> is subtracted from the <see cref="LinearDensity"/> <paramref name="x"/>.</param>
    public static LinearDensity operator -(LinearDensity x, LinearDensity y) => x.Subtract(y);

    /// <summary>Divides this <see cref="LinearDensity"/> by the <see cref="LinearDensity"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="LinearDensity"/> is divided by this <see cref="LinearDensity"/>.</param>
    public Scalar Divide(LinearDensity divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="LinearDensity"/> <paramref name="x"/> by the <see cref="LinearDensity"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="LinearDensity"/> is divided by the <see cref="LinearDensity"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="LinearDensity"/> <paramref name="x"/> is divided by this <see cref="LinearDensity"/>.</param>
    public static Scalar operator /(LinearDensity x, LinearDensity y) => x.Divide(y)
;

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
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
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
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
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
    public static bool operator <=(LinearDensity x, LinearDensity y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(LinearDensity x, LinearDensity y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="LinearDensity"/> <paramref name="x"/>.</summary>
    public static implicit operator double(LinearDensity x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
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