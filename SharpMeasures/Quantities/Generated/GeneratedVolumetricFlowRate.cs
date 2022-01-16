namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct VolumetricFlowRate :
    IComparable<VolumetricFlowRate>,
    IScalarQuantity<VolumetricFlowRate>,
    IAddableScalarQuantity<VolumetricFlowRate, VolumetricFlowRate>,
    ISubtractableScalarQuantity<VolumetricFlowRate, VolumetricFlowRate>,
    IDivisibleScalarQuantity<Scalar, VolumetricFlowRate>
{
    /// <summary>The zero-valued <see cref="VolumetricFlowRate"/>.</summary>
    public static VolumetricFlowRate Zero { get; } = new(0);

    /// <summary>The <see cref="VolumetricFlowRate"/> with magnitude 1, when expressed in unit <see cref="UnitOfVolumetricFlowRate.CubicMetrePerSecond"/>.</summary>
    public static VolumetricFlowRate OneCubicMetrePerSecond { get; } = new(1, UnitOfVolumetricFlowRate.CubicMetrePerSecond);
    /// <summary>The <see cref="VolumetricFlowRate"/> with magnitude 1, when expressed in unit <see cref="UnitOfVolumetricFlowRate.LitrePerSecond"/>.</summary>
    public static VolumetricFlowRate OneLitrePerSecond { get; } = new(1, UnitOfVolumetricFlowRate.LitrePerSecond);

    /// <summary>The magnitude of the <see cref="VolumetricFlowRate"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="VolumetricFlowRate.InCubicMetresPerSecond"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="VolumetricFlowRate"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfVolumetricFlowRate"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="VolumetricFlowRate"/>, in unit <paramref name="unitOfVolumetricFlowRate"/>.</param>
    /// <param name="unitOfVolumetricFlowRate">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="VolumetricFlowRate"/> a = 2.6 * <see cref="VolumetricFlowRate.OneLitrePerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public VolumetricFlowRate(double magnitude, UnitOfVolumetricFlowRate unitOfVolumetricFlowRate) : this(magnitude * unitOfVolumetricFlowRate.Factor) { }
    /// <summary>Constructs a new <see cref="VolumetricFlowRate"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="VolumetricFlowRate"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfVolumetricFlowRate"/> to be specified.</remarks>
    public VolumetricFlowRate(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="VolumetricFlowRate"/>, expressed in unit <see cref="UnitOfVolumetricFlowRate.CubicMetrePerSecond"/>.</summary>
    public Scalar InCubicMetresPerSecond => InUnit(UnitOfVolumetricFlowRate.CubicMetrePerSecond);
    /// <summary>Retrieves the magnitude of the <see cref="VolumetricFlowRate"/>, expressed in unit <see cref="UnitOfVolumetricFlowRate.LitrePerSecond"/>.</summary>
    public Scalar InLitresPerSecond => InUnit(UnitOfVolumetricFlowRate.LitrePerSecond);

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
    public VolumetricFlowRate Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public VolumetricFlowRate Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public VolumetricFlowRate Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public VolumetricFlowRate Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(VolumetricFlowRate other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="VolumetricFlowRate"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [m^3 * s^-1]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="VolumetricFlowRate"/>, expressed in unit <paramref name="unitOfVolumetricFlowRate"/>.</summary>
    /// <param name="unitOfVolumetricFlowRate">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfVolumetricFlowRate unitOfVolumetricFlowRate) => InUnit(Magnitude, unitOfVolumetricFlowRate);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="VolumetricFlowRate"/>, expressed in unit <paramref name="unitOfVolumetricFlowRate"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="VolumetricFlowRate"/>.</param>
    /// <param name="unitOfVolumetricFlowRate">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfVolumetricFlowRate unitOfVolumetricFlowRate) => new(magnitude / unitOfVolumetricFlowRate.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="VolumetricFlowRate"/>.</summary>
    public VolumetricFlowRate Plus() => this;
    /// <summary>Negation, resulting in a <see cref="VolumetricFlowRate"/> with negated magnitude.</summary>
    public VolumetricFlowRate Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="VolumetricFlowRate"/>.</param>
    public static VolumetricFlowRate operator +(VolumetricFlowRate x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="VolumetricFlowRate"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="VolumetricFlowRate"/>.</param>
    public static VolumetricFlowRate operator -(VolumetricFlowRate x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="VolumetricFlowRate"/> <paramref name="term"/>, producing another <see cref="VolumetricFlowRate"/>.</summary>
    /// <param name="term">This <see cref="VolumetricFlowRate"/> is added to this instance.</param>
    public VolumetricFlowRate Add(VolumetricFlowRate term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="VolumetricFlowRate"/> <paramref name="term"/> from this instance, producing another <see cref="VolumetricFlowRate"/>.</summary>
    /// <param name="term">This <see cref="VolumetricFlowRate"/> is subtracted from this instance.</param>
    public VolumetricFlowRate Subtract(VolumetricFlowRate term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="VolumetricFlowRate"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="VolumetricFlowRate"/>.</summary>
    /// <param name="x">This <see cref="VolumetricFlowRate"/> is added to the <see cref="VolumetricFlowRate"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="VolumetricFlowRate"/> is added to the <see cref="VolumetricFlowRate"/> <paramref name="x"/>.</param>
    public static VolumetricFlowRate operator +(VolumetricFlowRate x, VolumetricFlowRate y) => x.Add(y);
    /// <summary>Subtract the <see cref="VolumetricFlowRate"/> <paramref name="y"/> from the <see cref="VolumetricFlowRate"/> <paramref name="x"/>, producing another <see cref="VolumetricFlowRate"/>.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/> <paramref name="y"/> is subtracted from this <see cref="VolumetricFlowRate"/>.</param>
    /// <param name="y">This <see cref="VolumetricFlowRate"/> is subtracted from the <see cref="VolumetricFlowRate"/> <paramref name="x"/>.</param>
    public static VolumetricFlowRate operator -(VolumetricFlowRate x, VolumetricFlowRate y) => x.Subtract(y);

    /// <summary>Divides this <see cref="VolumetricFlowRate"/> by the <see cref="VolumetricFlowRate"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="VolumetricFlowRate"/> is divided by this <see cref="VolumetricFlowRate"/>.</param>
    public Scalar Divide(VolumetricFlowRate divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="VolumetricFlowRate"/> <paramref name="x"/> by the <see cref="VolumetricFlowRate"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="VolumetricFlowRate"/> is divided by the <see cref="VolumetricFlowRate"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="VolumetricFlowRate"/> <paramref name="x"/> is divided by this <see cref="VolumetricFlowRate"/>.</param>
    public static Scalar operator /(VolumetricFlowRate x, VolumetricFlowRate y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="VolumetricFlowRate"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="VolumetricFlowRate"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="VolumetricFlowRate"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="VolumetricFlowRate"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="VolumetricFlowRate"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="VolumetricFlowRate"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(VolumetricFlowRate x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="VolumetricFlowRate"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="VolumetricFlowRate"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(VolumetricFlowRate x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="VolumetricFlowRate"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public VolumetricFlowRate Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="VolumetricFlowRate"/> is scaled.</param>
    public VolumetricFlowRate Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="VolumetricFlowRate"/> is divided.</param>
    public VolumetricFlowRate Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="VolumetricFlowRate"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static VolumetricFlowRate operator %(VolumetricFlowRate x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="VolumetricFlowRate"/> <paramref name="x"/>.</param>
    public static VolumetricFlowRate operator *(VolumetricFlowRate x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="VolumetricFlowRate"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="VolumetricFlowRate"/>, which is scaled by <paramref name="x"/>.</param>
    public static VolumetricFlowRate operator *(double x, VolumetricFlowRate y) => y.Multiply(x);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="VolumetricFlowRate"/> <paramref name="x"/>.</param>
    public static VolumetricFlowRate operator /(VolumetricFlowRate x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="VolumetricFlowRate"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public VolumetricFlowRate Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="VolumetricFlowRate"/> is scaled.</param>
    public VolumetricFlowRate Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="VolumetricFlowRate"/> is divided.</param>
    public VolumetricFlowRate Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="VolumetricFlowRate"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static VolumetricFlowRate operator %(VolumetricFlowRate x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="VolumetricFlowRate"/> <paramref name="x"/>.</param>
    public static VolumetricFlowRate operator *(VolumetricFlowRate x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="VolumetricFlowRate"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="VolumetricFlowRate"/>, which is scaled by <paramref name="x"/>.</param>
    public static VolumetricFlowRate operator *(Scalar x, VolumetricFlowRate y) => y.Multiply(x);
    /// <summary>Scales the <see cref="VolumetricFlowRate"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="VolumetricFlowRate"/> <paramref name="x"/>.</param>
    public static VolumetricFlowRate operator /(VolumetricFlowRate x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="VolumetricFlowRate"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="VolumetricFlowRate"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="VolumetricFlowRate"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="VolumetricFlowRate"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="VolumetricFlowRate"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="VolumetricFlowRate"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="VolumetricFlowRate.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(VolumetricFlowRate x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="VolumetricFlowRate"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="VolumetricFlowRate"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="VolumetricFlowRate"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="VolumetricFlowRate.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(VolumetricFlowRate x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(VolumetricFlowRate x, VolumetricFlowRate y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(VolumetricFlowRate x, VolumetricFlowRate y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(VolumetricFlowRate x, VolumetricFlowRate y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(VolumetricFlowRate x, VolumetricFlowRate y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="VolumetricFlowRate"/> <paramref name="x"/>.</summary>
    public static implicit operator double(VolumetricFlowRate x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(VolumetricFlowRate x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="VolumetricFlowRate"/> of magnitude <paramref name="x"/>.</summary>
    public static VolumetricFlowRate FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="VolumetricFlowRate"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator VolumetricFlowRate(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="VolumetricFlowRate"/> of equivalent magnitude.</summary>
    public static VolumetricFlowRate FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="VolumetricFlowRate"/> of equivalent magnitude.</summary>
    public static explicit operator VolumetricFlowRate(Scalar x) => FromScalar(x);
}