namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct KineticEnergy :
    IComparable<KineticEnergy>,
    IScalarQuantity<KineticEnergy>,
    IAddableScalarQuantity<KineticEnergy, KineticEnergy>,
    ISubtractableScalarQuantity<KineticEnergy, KineticEnergy>,
    IDivisibleScalarQuantity<Scalar, KineticEnergy>
{
    /// <summary>The zero-valued <see cref="KineticEnergy"/>.</summary>
    public static KineticEnergy Zero { get; } = new(0);

    /// <summary>The <see cref="KineticEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Joule"/>.</summary>
    public static KineticEnergy OneJoule { get; } = new(1, UnitOfEnergy.Joule);
    /// <summary>The <see cref="KineticEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Kilojoule"/>.</summary>
    public static KineticEnergy OneKilojoule { get; } = new(1, UnitOfEnergy.Kilojoule);
    /// <summary>The <see cref="KineticEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Megajoule"/>.</summary>
    public static KineticEnergy OneMegajoule { get; } = new(1, UnitOfEnergy.Megajoule);
    /// <summary>The <see cref="KineticEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Gigajoule"/>.</summary>
    public static KineticEnergy OneGigajoule { get; } = new(1, UnitOfEnergy.Gigajoule);

    /// <summary>The <see cref="KineticEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.KilowattHour"/>.</summary>
    public static KineticEnergy OneKilowattHour { get; } = new(1, UnitOfEnergy.KilowattHour);
    /// <summary>The <see cref="KineticEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Calorie"/>.</summary>
    public static KineticEnergy OneCalorie { get; } = new(1, UnitOfEnergy.Calorie);
    /// <summary>The <see cref="KineticEnergy"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Kilocalorie"/>.</summary>
    public static KineticEnergy OneKilocalorie { get; } = new(1, UnitOfEnergy.Kilocalorie);

    /// <summary>The magnitude of the <see cref="KineticEnergy"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="KineticEnergy.InKilowattHours"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="KineticEnergy"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="KineticEnergy"/>, in unit <paramref name="unitOfEnergy"/>.</param>
    /// <param name="unitOfEnergy">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="KineticEnergy"/> a = 2.6 * <see cref="KineticEnergy.OneCalorie"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public KineticEnergy(double magnitude, UnitOfEnergy unitOfEnergy) : this(magnitude * unitOfEnergy.Factor) { }
    /// <summary>Constructs a new <see cref="KineticEnergy"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="KineticEnergy"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfEnergy"/> to be specified.</remarks>
    public KineticEnergy(double magnitude)
    {
        Magnitude = magnitude;
    }

    public PotentialEnergy AsPotentialEnergy => new(Magnitude);
    public Work AsWork => new(Magnitude);
    public Torque AsTorque => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="KineticEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Joule"/>.</summary>
    public Scalar InJoules => InUnit(UnitOfEnergy.Joule);
    /// <summary>Retrieves the magnitude of the <see cref="KineticEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Kilojoule"/>.</summary>
    public Scalar InKilojoules => InUnit(UnitOfEnergy.Kilojoule);
    /// <summary>Retrieves the magnitude of the <see cref="KineticEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Megajoule"/>.</summary>
    public Scalar InMegajoules => InUnit(UnitOfEnergy.Megajoule);
    /// <summary>Retrieves the magnitude of the <see cref="KineticEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Gigajoule"/>.</summary>
    public Scalar InGigajoules => InUnit(UnitOfEnergy.Gigajoule);

    /// <summary>Retrieves the magnitude of the <see cref="KineticEnergy"/>, expressed in unit <see cref="UnitOfEnergy.KilowattHour"/>.</summary>
    public Scalar InKilowattHours => InUnit(UnitOfEnergy.KilowattHour);
    /// <summary>Retrieves the magnitude of the <see cref="KineticEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Calorie"/>.</summary>
    public Scalar InCalories => InUnit(UnitOfEnergy.Calorie);
    /// <summary>Retrieves the magnitude of the <see cref="KineticEnergy"/>, expressed in unit <see cref="UnitOfEnergy.Kilocalorie"/>.</summary>
    public Scalar InKilocalories => InUnit(UnitOfEnergy.Kilocalorie);

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
    public KineticEnergy Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public KineticEnergy Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public KineticEnergy Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public KineticEnergy Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(KineticEnergy other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="KineticEnergy"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [J]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="KineticEnergy"/>, expressed in unit <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="unitOfEnergy">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfEnergy unitOfEnergy) => InUnit(Magnitude, unitOfEnergy);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="KineticEnergy"/>, expressed in unit <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="KineticEnergy"/>.</param>
    /// <param name="unitOfEnergy">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfEnergy unitOfEnergy) => new(magnitude / unitOfEnergy.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="KineticEnergy"/>.</summary>
    public KineticEnergy Plus() => this;
    /// <summary>Negation, resulting in a <see cref="KineticEnergy"/> with negated magnitude.</summary>
    public KineticEnergy Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="KineticEnergy"/>.</param>
    public static KineticEnergy operator +(KineticEnergy x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="KineticEnergy"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="KineticEnergy"/>.</param>
    public static KineticEnergy operator -(KineticEnergy x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="KineticEnergy"/> <paramref name="term"/>, producing another <see cref="KineticEnergy"/>.</summary>
    /// <param name="term">This <see cref="KineticEnergy"/> is added to this instance.</param>
    public KineticEnergy Add(KineticEnergy term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="KineticEnergy"/> <paramref name="term"/> from this instance, producing another <see cref="KineticEnergy"/>.</summary>
    /// <param name="term">This <see cref="KineticEnergy"/> is subtracted from this instance.</param>
    public KineticEnergy Subtract(KineticEnergy term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="KineticEnergy"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="KineticEnergy"/>.</summary>
    /// <param name="x">This <see cref="KineticEnergy"/> is added to the <see cref="KineticEnergy"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="KineticEnergy"/> is added to the <see cref="KineticEnergy"/> <paramref name="x"/>.</param>
    public static KineticEnergy operator +(KineticEnergy x, KineticEnergy y) => x.Add(y);
    /// <summary>Subtract the <see cref="KineticEnergy"/> <paramref name="y"/> from the <see cref="KineticEnergy"/> <paramref name="x"/>, producing another <see cref="KineticEnergy"/>.</summary>
    /// <param name="x">The <see cref="KineticEnergy"/> <paramref name="y"/> is subtracted from this <see cref="KineticEnergy"/>.</param>
    /// <param name="y">This <see cref="KineticEnergy"/> is subtracted from the <see cref="KineticEnergy"/> <paramref name="x"/>.</param>
    public static KineticEnergy operator -(KineticEnergy x, KineticEnergy y) => x.Subtract(y);

    /// <summary>Divides this <see cref="KineticEnergy"/> by the <see cref="KineticEnergy"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="KineticEnergy"/> is divided by this <see cref="KineticEnergy"/>.</param>
    public Scalar Divide(KineticEnergy divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="KineticEnergy"/> <paramref name="x"/> by the <see cref="KineticEnergy"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="KineticEnergy"/> is divided by the <see cref="KineticEnergy"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="KineticEnergy"/> <paramref name="x"/> is divided by this <see cref="KineticEnergy"/>.</param>
    public static Scalar operator /(KineticEnergy x, KineticEnergy y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="KineticEnergy"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="KineticEnergy"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="KineticEnergy"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="KineticEnergy"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="KineticEnergy"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="KineticEnergy"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="KineticEnergy"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(KineticEnergy x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="KineticEnergy"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="KineticEnergy"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="KineticEnergy"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(KineticEnergy x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="KineticEnergy"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public KineticEnergy Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="KineticEnergy"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="KineticEnergy"/> is scaled.</param>
    public KineticEnergy Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="KineticEnergy"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="KineticEnergy"/> is divided.</param>
    public KineticEnergy Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="KineticEnergy"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="KineticEnergy"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static KineticEnergy operator %(KineticEnergy x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="KineticEnergy"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="KineticEnergy"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="KineticEnergy"/> <paramref name="x"/>.</param>
    public static KineticEnergy operator *(KineticEnergy x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="KineticEnergy"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="KineticEnergy"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="KineticEnergy"/>, which is scaled by <paramref name="x"/>.</param>
    public static KineticEnergy operator *(double x, KineticEnergy y) => y.Multiply(x);
    /// <summary>Scales the <see cref="KineticEnergy"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="KineticEnergy"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="KineticEnergy"/> <paramref name="x"/>.</param>
    public static KineticEnergy operator /(KineticEnergy x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="KineticEnergy"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public KineticEnergy Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="KineticEnergy"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="KineticEnergy"/> is scaled.</param>
    public KineticEnergy Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="KineticEnergy"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="KineticEnergy"/> is divided.</param>
    public KineticEnergy Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="KineticEnergy"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="KineticEnergy"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static KineticEnergy operator %(KineticEnergy x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="KineticEnergy"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="KineticEnergy"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="KineticEnergy"/> <paramref name="x"/>.</param>
    public static KineticEnergy operator *(KineticEnergy x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="KineticEnergy"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="KineticEnergy"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="KineticEnergy"/>, which is scaled by <paramref name="x"/>.</param>
    public static KineticEnergy operator *(Scalar x, KineticEnergy y) => y.Multiply(x);
    /// <summary>Scales the <see cref="KineticEnergy"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="KineticEnergy"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="KineticEnergy"/> <paramref name="x"/>.</param>
    public static KineticEnergy operator /(KineticEnergy x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="KineticEnergy"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="KineticEnergy"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="KineticEnergy"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="KineticEnergy"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="KineticEnergy"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="KineticEnergy"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="KineticEnergy"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="KineticEnergy.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(KineticEnergy x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="KineticEnergy"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="KineticEnergy"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="KineticEnergy"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="KineticEnergy.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(KineticEnergy x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(KineticEnergy x, KineticEnergy y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(KineticEnergy x, KineticEnergy y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(KineticEnergy x, KineticEnergy y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(KineticEnergy x, KineticEnergy y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="KineticEnergy"/> <paramref name="x"/>.</summary>
    public static implicit operator double(KineticEnergy x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(KineticEnergy x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="KineticEnergy"/> of magnitude <paramref name="x"/>.</summary>
    public static KineticEnergy FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="KineticEnergy"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator KineticEnergy(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="KineticEnergy"/> of equivalent magnitude.</summary>
    public static KineticEnergy FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="KineticEnergy"/> of equivalent magnitude.</summary>
    public static explicit operator KineticEnergy(Scalar x) => FromScalar(x);
}