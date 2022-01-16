namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct TemperatureDifference :
    IComparable<TemperatureDifference>,
    IScalarQuantity<TemperatureDifference>,
    IAddableScalarQuantity<TemperatureDifference, TemperatureDifference>,
    ISubtractableScalarQuantity<TemperatureDifference, TemperatureDifference>,
    IDivisibleScalarQuantity<Scalar, TemperatureDifference>
{
    /// <summary>The zero-valued <see cref="TemperatureDifference"/>.</summary>
    public static TemperatureDifference Zero { get; } = new(0);

    /// <summary>The <see cref="TemperatureDifference"/> with magnitude 1, when expressed in unit <see cref="UnitOfTemperatureDifference.Kelvin"/>.</summary>
    public static TemperatureDifference OneKelvin { get; } = new(1, UnitOfTemperatureDifference.Kelvin);
    /// <summary>The <see cref="TemperatureDifference"/> with magnitude 1, when expressed in unit <see cref="UnitOfTemperatureDifference.Celsius"/>.</summary>
    public static TemperatureDifference OneCelsius { get; } = new(1, UnitOfTemperatureDifference.Celsius);
    /// <summary>The <see cref="TemperatureDifference"/> with magnitude 1, when expressed in unit <see cref="UnitOfTemperatureDifference.Rankine"/>.</summary>
    public static TemperatureDifference OneRankine { get; } = new(1, UnitOfTemperatureDifference.Rankine);
    /// <summary>The <see cref="TemperatureDifference"/> with magnitude 1, when expressed in unit <see cref="UnitOfTemperatureDifference.Fahrenheit"/>.</summary>
    public static TemperatureDifference OneFahrenheit { get; } = new(1, UnitOfTemperatureDifference.Fahrenheit);

    /// <summary>The magnitude of the <see cref="TemperatureDifference"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="TemperatureDifference.InKelvin"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="TemperatureDifference"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfTemperatureDifference"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TemperatureDifference"/>, in unit <paramref name="unitOfTemperatureDifference"/>.</param>
    /// <param name="unitOfTemperatureDifference">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="TemperatureDifference"/> a = 2.6 * <see cref="TemperatureDifference.OneCelsius"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public TemperatureDifference(double magnitude, UnitOfTemperatureDifference unitOfTemperatureDifference) : this(magnitude * unitOfTemperatureDifference.Factor) { }
    /// <summary>Constructs a new <see cref="TemperatureDifference"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="TemperatureDifference"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfTemperatureDifference"/> to be specified.</remarks>
    public TemperatureDifference(double magnitude)
    {
        Magnitude = magnitude;
    }

    public Temperature AsTemperature => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="TemperatureDifference"/>, expressed in unit <see cref="UnitOfTemperatureDifference.Kelvin"/>.</summary>
    public Scalar InKelvin => InUnit(UnitOfTemperatureDifference.Kelvin);
    /// <summary>Retrieves the magnitude of the <see cref="TemperatureDifference"/>, expressed in unit <see cref="UnitOfTemperatureDifference.Celsius"/>.</summary>
    public Scalar InCelsius => InUnit(UnitOfTemperatureDifference.Celsius);
    /// <summary>Retrieves the magnitude of the <see cref="TemperatureDifference"/>, expressed in unit <see cref="UnitOfTemperatureDifference.Rankine"/>.</summary>
    public Scalar InRankine => InUnit(UnitOfTemperatureDifference.Rankine);
    /// <summary>Retrieves the magnitude of the <see cref="TemperatureDifference"/>, expressed in unit <see cref="UnitOfTemperatureDifference.Fahrenheit"/>.</summary>
    public Scalar InFahrenheit => InUnit(UnitOfTemperatureDifference.Fahrenheit);

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
    public TemperatureDifference Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public TemperatureDifference Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public TemperatureDifference Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public TemperatureDifference Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(TemperatureDifference other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="TemperatureDifference"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [K]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="TemperatureDifference"/>, expressed in unit <paramref name="unitOfTemperatureDifference"/>.</summary>
    /// <param name="unitOfTemperatureDifference">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfTemperatureDifference unitOfTemperatureDifference) => InUnit(Magnitude, unitOfTemperatureDifference);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="TemperatureDifference"/>, expressed in unit <paramref name="unitOfTemperatureDifference"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="TemperatureDifference"/>.</param>
    /// <param name="unitOfTemperatureDifference">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfTemperatureDifference unitOfTemperatureDifference) => new(magnitude / unitOfTemperatureDifference.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="TemperatureDifference"/>.</summary>
    public TemperatureDifference Plus() => this;
    /// <summary>Negation, resulting in a <see cref="TemperatureDifference"/> with negated magnitude.</summary>
    public TemperatureDifference Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="TemperatureDifference"/>.</param>
    public static TemperatureDifference operator +(TemperatureDifference x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="TemperatureDifference"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="TemperatureDifference"/>.</param>
    public static TemperatureDifference operator -(TemperatureDifference x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="TemperatureDifference"/> <paramref name="term"/>, producing another <see cref="TemperatureDifference"/>.</summary>
    /// <param name="term">This <see cref="TemperatureDifference"/> is added to this instance.</param>
    public TemperatureDifference Add(TemperatureDifference term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="TemperatureDifference"/> <paramref name="term"/> from this instance, producing another <see cref="TemperatureDifference"/>.</summary>
    /// <param name="term">This <see cref="TemperatureDifference"/> is subtracted from this instance.</param>
    public TemperatureDifference Subtract(TemperatureDifference term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="TemperatureDifference"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="TemperatureDifference"/>.</summary>
    /// <param name="x">This <see cref="TemperatureDifference"/> is added to the <see cref="TemperatureDifference"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="TemperatureDifference"/> is added to the <see cref="TemperatureDifference"/> <paramref name="x"/>.</param>
    public static TemperatureDifference operator +(TemperatureDifference x, TemperatureDifference y) => x.Add(y);
    /// <summary>Subtract the <see cref="TemperatureDifference"/> <paramref name="y"/> from the <see cref="TemperatureDifference"/> <paramref name="x"/>, producing another <see cref="TemperatureDifference"/>.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/> <paramref name="y"/> is subtracted from this <see cref="TemperatureDifference"/>.</param>
    /// <param name="y">This <see cref="TemperatureDifference"/> is subtracted from the <see cref="TemperatureDifference"/> <paramref name="x"/>.</param>
    public static TemperatureDifference operator -(TemperatureDifference x, TemperatureDifference y) => x.Subtract(y);

    /// <summary>Divides this <see cref="TemperatureDifference"/> by the <see cref="TemperatureDifference"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="TemperatureDifference"/> is divided by this <see cref="TemperatureDifference"/>.</param>
    public Scalar Divide(TemperatureDifference divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="TemperatureDifference"/> <paramref name="x"/> by the <see cref="TemperatureDifference"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="TemperatureDifference"/> is divided by the <see cref="TemperatureDifference"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TemperatureDifference"/> <paramref name="x"/> is divided by this <see cref="TemperatureDifference"/>.</param>
    public static Scalar operator /(TemperatureDifference x, TemperatureDifference y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="TemperatureDifference"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="TemperatureDifference"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="TemperatureDifference"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="TemperatureDifference"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="TemperatureDifference"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="TemperatureDifference"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(TemperatureDifference x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="TemperatureDifference"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="TemperatureDifference"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(TemperatureDifference x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="TemperatureDifference"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public TemperatureDifference Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="TemperatureDifference"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="TemperatureDifference"/> is scaled.</param>
    public TemperatureDifference Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="TemperatureDifference"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="TemperatureDifference"/> is divided.</param>
    public TemperatureDifference Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="TemperatureDifference"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static TemperatureDifference operator %(TemperatureDifference x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="TemperatureDifference"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="TemperatureDifference"/> <paramref name="x"/>.</param>
    public static TemperatureDifference operator *(TemperatureDifference x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="TemperatureDifference"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="TemperatureDifference"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TemperatureDifference"/>, which is scaled by <paramref name="x"/>.</param>
    public static TemperatureDifference operator *(double x, TemperatureDifference y) => y.Multiply(x);
    /// <summary>Scales the <see cref="TemperatureDifference"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="TemperatureDifference"/> <paramref name="x"/>.</param>
    public static TemperatureDifference operator /(TemperatureDifference x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="TemperatureDifference"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public TemperatureDifference Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="TemperatureDifference"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="TemperatureDifference"/> is scaled.</param>
    public TemperatureDifference Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="TemperatureDifference"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="TemperatureDifference"/> is divided.</param>
    public TemperatureDifference Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="TemperatureDifference"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static TemperatureDifference operator %(TemperatureDifference x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="TemperatureDifference"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="TemperatureDifference"/> <paramref name="x"/>.</param>
    public static TemperatureDifference operator *(TemperatureDifference x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="TemperatureDifference"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="TemperatureDifference"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="TemperatureDifference"/>, which is scaled by <paramref name="x"/>.</param>
    public static TemperatureDifference operator *(Scalar x, TemperatureDifference y) => y.Multiply(x);
    /// <summary>Scales the <see cref="TemperatureDifference"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="TemperatureDifference"/> <paramref name="x"/>.</param>
    public static TemperatureDifference operator /(TemperatureDifference x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="TemperatureDifference"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="TemperatureDifference"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="TemperatureDifference"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="TemperatureDifference"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="TemperatureDifference"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="TemperatureDifference"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="TemperatureDifference.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(TemperatureDifference x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="TemperatureDifference"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="TemperatureDifference"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="TemperatureDifference"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="TemperatureDifference.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(TemperatureDifference x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(TemperatureDifference x, TemperatureDifference y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(TemperatureDifference x, TemperatureDifference y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(TemperatureDifference x, TemperatureDifference y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(TemperatureDifference x, TemperatureDifference y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="TemperatureDifference"/> <paramref name="x"/>.</summary>
    public static implicit operator double(TemperatureDifference x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(TemperatureDifference x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="TemperatureDifference"/> of magnitude <paramref name="x"/>.</summary>
    public static TemperatureDifference FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="TemperatureDifference"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator TemperatureDifference(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="TemperatureDifference"/> of equivalent magnitude.</summary>
    public static TemperatureDifference FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="TemperatureDifference"/> of equivalent magnitude.</summary>
    public static explicit operator TemperatureDifference(Scalar x) => FromScalar(x);
}