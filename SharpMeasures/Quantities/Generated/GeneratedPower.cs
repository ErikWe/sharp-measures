namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct Power :
    IComparable<Power>,
    IScalarQuantity<Power>,
    IAddableScalarQuantity<Power, Power>,
    ISubtractableScalarQuantity<Power, Power>,
    IDivisibleScalarQuantity<Scalar, Power>
{
    /// <summary>The zero-valued <see cref="Power"/>.</summary>
    public static Power Zero { get; } = new(0);

    /// <summary>The <see cref="Power"/> with magnitude 1, when expressed in unit <see cref="UnitOfPower.Watt"/>.</summary>
    public static Power OneWatt { get; } = new(1, UnitOfPower.Watt);
    /// <summary>The <see cref="Power"/> with magnitude 1, when expressed in unit <see cref="UnitOfPower.Kilowatt"/>.</summary>
    public static Power OneKilowatt { get; } = new(1, UnitOfPower.Kilowatt);
    /// <summary>The <see cref="Power"/> with magnitude 1, when expressed in unit <see cref="UnitOfPower.Megawatt"/>.</summary>
    public static Power OneMegawatt { get; } = new(1, UnitOfPower.Megawatt);
    /// <summary>The <see cref="Power"/> with magnitude 1, when expressed in unit <see cref="UnitOfPower.Gigawatt"/>.</summary>
    public static Power OneGigawatt { get; } = new(1, UnitOfPower.Gigawatt);
    /// <summary>The <see cref="Power"/> with magnitude 1, when expressed in unit <see cref="UnitOfPower.Terawatt"/>.</summary>
    public static Power OneTerawatt { get; } = new(1, UnitOfPower.Terawatt);

    /// <summary>The magnitude of the <see cref="Power"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Power.InTerawatts"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Power"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfPower"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Power"/>, in unit <paramref name="unitOfPower"/>.</param>
    /// <param name="unitOfPower">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Power"/> a = 2.6 * <see cref="Power.OneWatt"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Power(double magnitude, UnitOfPower unitOfPower) : this(magnitude * unitOfPower.Factor) { }
    /// <summary>Constructs a new <see cref="Power"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Power"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfPower"/> to be specified.</remarks>
    public Power(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Power"/>, expressed in unit <see cref="UnitOfPower.Watt"/>.</summary>
    public Scalar InWatts => InUnit(UnitOfPower.Watt);
    /// <summary>Retrieves the magnitude of the <see cref="Power"/>, expressed in unit <see cref="UnitOfPower.Kilowatt"/>.</summary>
    public Scalar InKilowatts => InUnit(UnitOfPower.Kilowatt);
    /// <summary>Retrieves the magnitude of the <see cref="Power"/>, expressed in unit <see cref="UnitOfPower.Megawatt"/>.</summary>
    public Scalar InMegawatts => InUnit(UnitOfPower.Megawatt);
    /// <summary>Retrieves the magnitude of the <see cref="Power"/>, expressed in unit <see cref="UnitOfPower.Gigawatt"/>.</summary>
    public Scalar InGigawatts => InUnit(UnitOfPower.Gigawatt);
    /// <summary>Retrieves the magnitude of the <see cref="Power"/>, expressed in unit <see cref="UnitOfPower.Terawatt"/>.</summary>
    public Scalar InTerawatts => InUnit(UnitOfPower.Terawatt);

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
    public Power Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public Power Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public Power Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public Power Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Power other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Power"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [W]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Power"/>, expressed in unit <paramref name="unitOfPower"/>.</summary>
    /// <param name="unitOfPower">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfPower unitOfPower) => InUnit(Magnitude, unitOfPower);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Power"/>, expressed in unit <paramref name="unitOfPower"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="Power"/>.</param>
    /// <param name="unitOfPower">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfPower unitOfPower) => new(magnitude / unitOfPower.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Power"/>.</summary>
    public Power Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Power"/> with negated magnitude.</summary>
    public Power Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Power"/>.</param>
    public static Power operator +(Power x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Power"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Power"/>.</param>
    public static Power operator -(Power x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="Power"/> <paramref name="term"/>, producing another <see cref="Power"/>.</summary>
    /// <param name="term">This <see cref="Power"/> is added to this instance.</param>
    public Power Add(Power term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="Power"/> <paramref name="term"/> from this instance, producing another <see cref="Power"/>.</summary>
    /// <param name="term">This <see cref="Power"/> is subtracted from this instance.</param>
    public Power Subtract(Power term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="Power"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="Power"/>.</summary>
    /// <param name="x">This <see cref="Power"/> is added to the <see cref="Power"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="Power"/> is added to the <see cref="Power"/> <paramref name="x"/>.</param>
    public static Power operator +(Power x, Power y) => x.Add(y);
    /// <summary>Subtract the <see cref="Power"/> <paramref name="y"/> from the <see cref="Power"/> <paramref name="x"/>, producing another <see cref="Power"/>.</summary>
    /// <param name="x">The <see cref="Power"/> <paramref name="y"/> is subtracted from this <see cref="Power"/>.</param>
    /// <param name="y">This <see cref="Power"/> is subtracted from the <see cref="Power"/> <paramref name="x"/>.</param>
    public static Power operator -(Power x, Power y) => x.Subtract(y);

    /// <summary>Divides this <see cref="Power"/> by the <see cref="Power"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Power"/> is divided by this <see cref="Power"/>.</param>
    public Scalar Divide(Power divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="Power"/> <paramref name="x"/> by the <see cref="Power"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="Power"/> is divided by the <see cref="Power"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Power"/> <paramref name="x"/> is divided by this <see cref="Power"/>.</param>
    public static Scalar operator /(Power x, Power y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="Power"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Power"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Power"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Power"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Power"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Power"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Power"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Power x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Power"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Power"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Power"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Power x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Power"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Power Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Power"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Power"/> is scaled.</param>
    public Power Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Power"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Power"/> is divided.</param>
    public Power Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Power"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Power"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Power operator %(Power x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Power"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Power"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Power"/> <paramref name="x"/>.</param>
    public static Power operator *(Power x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Power"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Power"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Power"/>, which is scaled by <paramref name="x"/>.</param>
    public static Power operator *(double x, Power y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Power"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Power"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Power"/> <paramref name="x"/>.</param>
    public static Power operator /(Power x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="Power"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Power Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Power"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Power"/> is scaled.</param>
    public Power Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Power"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Power"/> is divided.</param>
    public Power Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Power"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Power"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Power operator %(Power x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Power"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Power"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Power"/> <paramref name="x"/>.</param>
    public static Power operator *(Power x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Power"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Power"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Power"/>, which is scaled by <paramref name="x"/>.</param>
    public static Power operator *(Scalar x, Power y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Power"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Power"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Power"/> <paramref name="x"/>.</param>
    public static Power operator /(Power x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="Power"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Power"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Power"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Power"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Power"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Power"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Power"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Power.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Power x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Power"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Power"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Power"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Power.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Power x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Power x, Power y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Power x, Power y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Power x, Power y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Power x, Power y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="Power"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Power x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Power x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Power"/> of magnitude <paramref name="x"/>.</summary>
    public static Power FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Power"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Power(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Power"/> of equivalent magnitude.</summary>
    public static Power FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Power"/> of equivalent magnitude.</summary>
    public static explicit operator Power(Scalar x) => FromScalar(x);
}