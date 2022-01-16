namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="MassFlowRate"/>, used for describing change in <see cref="Mass"/> over <see cref="Time"/>,
/// or the flow of <see cref="Mass"/>.
/// <para>
/// New instances of <see cref="MassFlowRate"/> can be constructed according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="MassFlowRate"/> a = 5 * <see cref="MassFlowRate.OneKilogramPerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="MassFlowRate"/> b = new(7, <see cref="UnitOfMassFlowRate.KilogramPerSecond"/>);
/// </code>
/// </item>
/// <item>
/// <code>
/// </code>
/// </item>
/// </list>
/// Instances of <see cref="MassFlowRate"/> may be applied according to:
/// <list type="bullet">
/// <item>
/// <code>
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved in a desired unit according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="MassFlowRate.InKilogramsPerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="MassFlowRate.InUnit(UnitOfMassFlowRate)"/>;
/// </code>
/// </item>
/// </list>
/// </para>
/// </summary>
public readonly partial record struct MassFlowRate :
    IComparable<MassFlowRate>,
    IScalarQuantity<MassFlowRate>,
    IAddableScalarQuantity<MassFlowRate, MassFlowRate>,
    ISubtractableScalarQuantity<MassFlowRate, MassFlowRate>,
    IDivisibleScalarQuantity<Scalar, MassFlowRate>
{
    /// <summary>The zero-valued <see cref="MassFlowRate"/>.</summary>
    public static MassFlowRate Zero { get; } = new(0);

    /// <summary>The <see cref="MassFlowRate"/> with magnitude 1, when expressed in unit <see cref="UnitOfMassFlowRate.KilogramPerSecond"/>.</summary>
    public static MassFlowRate OneKilogramPerSecond { get; } = new(1, UnitOfMassFlowRate.KilogramPerSecond);

    /// <summary>The magnitude of the <see cref="MassFlowRate"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="MassFlowRate.InKilogramsPerSecond"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="MassFlowRate"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfMassFlowRate"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="MassFlowRate"/>, in unit <paramref name="unitOfMassFlowRate"/>.</param>
    /// <param name="unitOfMassFlowRate">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="MassFlowRate"/> a = 2.6 * <see cref="MassFlowRate.OneKilogramPerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public MassFlowRate(double magnitude, UnitOfMassFlowRate unitOfMassFlowRate) : this(magnitude * unitOfMassFlowRate.Factor) { }
    /// <summary>Constructs a new <see cref="MassFlowRate"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="MassFlowRate"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfMassFlowRate"/> to be specified.</remarks>
    public MassFlowRate(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="MassFlowRate"/>, expressed in unit <see cref="UnitOfMassFlowRate.KilogramPerSecond"/>.</summary>
    public Scalar InKilogramsPerSecond => InUnit(UnitOfMassFlowRate.KilogramPerSecond);

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
    public MassFlowRate Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public MassFlowRate Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public MassFlowRate Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public MassFlowRate Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(MassFlowRate other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="MassFlowRate"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [kg * s^-1]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="MassFlowRate"/>, expressed in unit <paramref name="unitOfMassFlowRate"/>.</summary>
    /// <param name="unitOfMassFlowRate">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfMassFlowRate unitOfMassFlowRate) => InUnit(Magnitude, unitOfMassFlowRate);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="MassFlowRate"/>, expressed in unit <paramref name="unitOfMassFlowRate"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="MassFlowRate"/>.</param>
    /// <param name="unitOfMassFlowRate">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfMassFlowRate unitOfMassFlowRate) => new(magnitude / unitOfMassFlowRate.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="MassFlowRate"/>.</summary>
    public MassFlowRate Plus() => this;
    /// <summary>Negation, resulting in a <see cref="MassFlowRate"/> with negated magnitude.</summary>
    public MassFlowRate Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="MassFlowRate"/>.</param>
    public static MassFlowRate operator +(MassFlowRate x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="MassFlowRate"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="MassFlowRate"/>.</param>
    public static MassFlowRate operator -(MassFlowRate x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="MassFlowRate"/> <paramref name="term"/>, producing another <see cref="MassFlowRate"/>.</summary>
    /// <param name="term">This <see cref="MassFlowRate"/> is added to this instance.</param>
    public MassFlowRate Add(MassFlowRate term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="MassFlowRate"/> <paramref name="term"/> from this instance, producing another <see cref="MassFlowRate"/>.</summary>
    /// <param name="term">This <see cref="MassFlowRate"/> is subtracted from this instance.</param>
    public MassFlowRate Subtract(MassFlowRate term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="MassFlowRate"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="MassFlowRate"/>.</summary>
    /// <param name="x">This <see cref="MassFlowRate"/> is added to the <see cref="MassFlowRate"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="MassFlowRate"/> is added to the <see cref="MassFlowRate"/> <paramref name="x"/>.</param>
    public static MassFlowRate operator +(MassFlowRate x, MassFlowRate y) => x.Add(y);
    /// <summary>Subtract the <see cref="MassFlowRate"/> <paramref name="y"/> from the <see cref="MassFlowRate"/> <paramref name="x"/>, producing another <see cref="MassFlowRate"/>.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/> <paramref name="y"/> is subtracted from this <see cref="MassFlowRate"/>.</param>
    /// <param name="y">This <see cref="MassFlowRate"/> is subtracted from the <see cref="MassFlowRate"/> <paramref name="x"/>.</param>
    public static MassFlowRate operator -(MassFlowRate x, MassFlowRate y) => x.Subtract(y);

    /// <summary>Divides this <see cref="MassFlowRate"/> by the <see cref="MassFlowRate"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="MassFlowRate"/> is divided by this <see cref="MassFlowRate"/>.</param>
    public Scalar Divide(MassFlowRate divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="MassFlowRate"/> <paramref name="x"/> by the <see cref="MassFlowRate"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="MassFlowRate"/> is divided by the <see cref="MassFlowRate"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="MassFlowRate"/> <paramref name="x"/> is divided by this <see cref="MassFlowRate"/>.</param>
    public static Scalar operator /(MassFlowRate x, MassFlowRate y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="MassFlowRate"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="MassFlowRate"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="MassFlowRate"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="MassFlowRate"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="MassFlowRate"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="MassFlowRate"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(MassFlowRate x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="MassFlowRate"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="MassFlowRate"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(MassFlowRate x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="MassFlowRate"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public MassFlowRate Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="MassFlowRate"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="MassFlowRate"/> is scaled.</param>
    public MassFlowRate Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="MassFlowRate"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="MassFlowRate"/> is divided.</param>
    public MassFlowRate Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="MassFlowRate"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static MassFlowRate operator %(MassFlowRate x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="MassFlowRate"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="MassFlowRate"/> <paramref name="x"/>.</param>
    public static MassFlowRate operator *(MassFlowRate x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="MassFlowRate"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="MassFlowRate"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="MassFlowRate"/>, which is scaled by <paramref name="x"/>.</param>
    public static MassFlowRate operator *(double x, MassFlowRate y) => y.Multiply(x);
    /// <summary>Scales the <see cref="MassFlowRate"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="MassFlowRate"/> <paramref name="x"/>.</param>
    public static MassFlowRate operator /(MassFlowRate x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="MassFlowRate"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public MassFlowRate Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="MassFlowRate"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="MassFlowRate"/> is scaled.</param>
    public MassFlowRate Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="MassFlowRate"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="MassFlowRate"/> is divided.</param>
    public MassFlowRate Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="MassFlowRate"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static MassFlowRate operator %(MassFlowRate x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="MassFlowRate"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="MassFlowRate"/> <paramref name="x"/>.</param>
    public static MassFlowRate operator *(MassFlowRate x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="MassFlowRate"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="MassFlowRate"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="MassFlowRate"/>, which is scaled by <paramref name="x"/>.</param>
    public static MassFlowRate operator *(Scalar x, MassFlowRate y) => y.Multiply(x);
    /// <summary>Scales the <see cref="MassFlowRate"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="MassFlowRate"/> <paramref name="x"/>.</param>
    public static MassFlowRate operator /(MassFlowRate x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="MassFlowRate"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="MassFlowRate"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="MassFlowRate"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="MassFlowRate"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="MassFlowRate"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="MassFlowRate"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="MassFlowRate.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(MassFlowRate x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="MassFlowRate"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="MassFlowRate"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="MassFlowRate"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="MassFlowRate.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(MassFlowRate x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(MassFlowRate x, MassFlowRate y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(MassFlowRate x, MassFlowRate y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(MassFlowRate x, MassFlowRate y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(MassFlowRate x, MassFlowRate y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="MassFlowRate"/> <paramref name="x"/>.</summary>
    public static implicit operator double(MassFlowRate x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(MassFlowRate x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="MassFlowRate"/> of magnitude <paramref name="x"/>.</summary>
    public static MassFlowRate FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="MassFlowRate"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator MassFlowRate(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="MassFlowRate"/> of equivalent magnitude.</summary>
    public static MassFlowRate FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="MassFlowRate"/> of equivalent magnitude.</summary>
    public static explicit operator MassFlowRate(Scalar x) => FromScalar(x);
}