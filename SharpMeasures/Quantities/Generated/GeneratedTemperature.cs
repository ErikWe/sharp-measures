namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct Temperature :
    IComparable<Temperature>,
    IScalarQuantity<Temperature>,
    IDivisibleScalarQuantity<Scalar, Temperature>
{
    /// <summary>The zero-valued <see cref="Temperature"/>.</summary>
    public static Temperature Zero { get; } = new(0);

    /// <summary>The <see cref="Temperature"/> with magnitude 1, when expressed in unit <see cref="UnitOfTemperature.Kelvin"/>.</summary>
    public static Temperature OneKelvin { get; } = new(1, UnitOfTemperature.Kelvin);
    /// <summary>The <see cref="Temperature"/> with magnitude 1, when expressed in unit <see cref="UnitOfTemperature.Rankine"/>.</summary>
    public static Temperature OneRankine { get; } = new(1, UnitOfTemperature.Rankine);

    /// <summary>The magnitude of the <see cref="Temperature"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Temperature.InKelvin"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Temperature"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfTemperature"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Temperature"/>, in unit <paramref name="unitOfTemperature"/>.</param>
    /// <param name="unitOfTemperature">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Temperature"/> a = 2.6 * <see cref="Temperature.OneRankine"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Temperature(double magnitude, UnitOfTemperature unitOfTemperature) : 
    	this((magnitude * unitOfTemperature.Prefix.Scale + unitOfTemperature.Bias) * unitOfTemperature.BaseScale) { }
    /// <summary>Constructs a new <see cref="Temperature"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Temperature"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfTemperature"/> to be specified.</remarks>
    public Temperature(double magnitude)
    {
        Magnitude = magnitude;
    }

    public TemperatureDifference AsTemperatureDifference => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="Temperature"/>, expressed in unit <see cref="UnitOfTemperature.Kelvin"/>.</summary>
    public Scalar InKelvin => InUnit(UnitOfTemperature.Kelvin);
    /// <summary>Retrieves the magnitude of the <see cref="Temperature"/>, expressed in unit <see cref="UnitOfTemperature.Celsius"/>.</summary>
    public Scalar InCelsius => InUnit(UnitOfTemperature.Celsius);
    /// <summary>Retrieves the magnitude of the <see cref="Temperature"/>, expressed in unit <see cref="UnitOfTemperature.Rankine"/>.</summary>
    public Scalar InRankine => InUnit(UnitOfTemperature.Rankine);
    /// <summary>Retrieves the magnitude of the <see cref="Temperature"/>, expressed in unit <see cref="UnitOfTemperature.Fahrenheit"/>.</summary>
    public Scalar InFahrenheit => InUnit(UnitOfTemperature.Fahrenheit);

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
    public Temperature Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public Temperature Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public Temperature Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public Temperature Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Temperature other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Temperature"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [K]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Temperature"/>, expressed in unit <paramref name="unitOfTemperature"/>.</summary>
    /// <param name="unitOfTemperature">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfTemperature unitOfTemperature) => InUnit(Magnitude, unitOfTemperature);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Temperature"/>, expressed in unit <paramref name="unitOfTemperature"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="Temperature"/>.</param>
    /// <param name="unitOfTemperature">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfTemperature unitOfTemperature) => new((magnitude / unitOfTemperature.BaseScale - unitOfTemperature.Bias) / unitOfTemperature.Prefix.Scale);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Temperature"/>.</summary>
    public Temperature Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Temperature"/> with negated magnitude.</summary>
    public Temperature Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Temperature"/>.</param>
    public static Temperature operator +(Temperature x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Temperature"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Temperature"/>.</param>
    public static Temperature operator -(Temperature x) => x.Negate();

    /// <summary>Divides this <see cref="Temperature"/> by the <see cref="Temperature"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Temperature"/> is divided by this <see cref="Temperature"/>.</param>
    public Scalar Divide(Temperature divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="Temperature"/> <paramref name="x"/> by the <see cref="Temperature"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="Temperature"/> is divided by the <see cref="Temperature"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Temperature"/> <paramref name="x"/> is divided by this <see cref="Temperature"/>.</param>
    public static Scalar operator /(Temperature x, Temperature y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="Temperature"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Temperature"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Temperature"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Temperature"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Temperature"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Temperature"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Temperature x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Temperature"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Temperature"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Temperature x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Temperature"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Temperature Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Temperature"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Temperature"/> is scaled.</param>
    public Temperature Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Temperature"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Temperature"/> is divided.</param>
    public Temperature Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Temperature"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Temperature operator %(Temperature x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Temperature"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Temperature"/> <paramref name="x"/>.</param>
    public static Temperature operator *(Temperature x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Temperature"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Temperature"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Temperature"/>, which is scaled by <paramref name="x"/>.</param>
    public static Temperature operator *(double x, Temperature y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Temperature"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Temperature"/> <paramref name="x"/>.</param>
    public static Temperature operator /(Temperature x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="Temperature"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Temperature Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Temperature"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Temperature"/> is scaled.</param>
    public Temperature Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Temperature"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Temperature"/> is divided.</param>
    public Temperature Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Temperature"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Temperature operator %(Temperature x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Temperature"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Temperature"/> <paramref name="x"/>.</param>
    public static Temperature operator *(Temperature x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Temperature"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Temperature"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Temperature"/>, which is scaled by <paramref name="x"/>.</param>
    public static Temperature operator *(Scalar x, Temperature y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Temperature"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Temperature"/> <paramref name="x"/>.</param>
    public static Temperature operator /(Temperature x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="Temperature"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Temperature"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Temperature"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Temperature"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Temperature"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Temperature"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Temperature.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Temperature x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Temperature"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Temperature"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Temperature"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Temperature.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Temperature x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Temperature x, Temperature y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Temperature x, Temperature y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Temperature x, Temperature y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Temperature x, Temperature y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="Temperature"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Temperature x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Temperature x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Temperature"/> of magnitude <paramref name="x"/>.</summary>
    public static Temperature FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Temperature"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Temperature(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Temperature"/> of equivalent magnitude.</summary>
    public static Temperature FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Temperature"/> of equivalent magnitude.</summary>
    public static explicit operator Temperature(Scalar x) => FromScalar(x);
}