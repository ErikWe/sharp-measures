namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct Jerk :
    IComparable<Jerk>,
    IScalarQuantity<Jerk>,
    IAddableScalarQuantity<Jerk, Jerk>,
    ISubtractableScalarQuantity<Jerk, Jerk>,
    IDivisibleScalarQuantity<Scalar, Jerk>,
    IVector3izableScalarQuantity<Jerk3>
{
    /// <summary>The zero-valued <see cref="Jerk"/>.</summary>
    public static Jerk Zero { get; } = new(0);

    /// <summary>The <see cref="Jerk"/> with magnitude 1, when expressed in unit <see cref="UnitOfJerk.MetrePerSecondCubed"/>.</summary>
    public static Jerk OneMetrePerSecondCubed { get; } = new(1, UnitOfJerk.MetrePerSecondCubed);

    /// <summary>The magnitude of the <see cref="Jerk"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Jerk.InMetresPerSecondCubed"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Jerk"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfJerk"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Jerk"/>, in unit <paramref name="unitOfJerk"/>.</param>
    /// <param name="unitOfJerk">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Jerk"/> a = 2.6 * <see cref="Jerk.OneMetrePerSecondCubed"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Jerk(double magnitude, UnitOfJerk unitOfJerk) : this(magnitude * unitOfJerk.Factor) { }
    /// <summary>Constructs a new <see cref="Jerk"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Jerk"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfJerk"/> to be specified.</remarks>
    public Jerk(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Jerk"/>, expressed in unit <see cref="UnitOfJerk.MetrePerSecondCubed"/>.</summary>
    public Scalar InMetresPerSecondCubed => InUnit(UnitOfJerk.MetrePerSecondCubed);

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
    public Jerk Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public Jerk Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public Jerk Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public Jerk Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Jerk other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Jerk"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [m * s^-3]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Jerk"/>, expressed in unit <paramref name="unitOfJerk"/>.</summary>
    /// <param name="unitOfJerk">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfJerk unitOfJerk) => InUnit(Magnitude, unitOfJerk);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Jerk"/>, expressed in unit <paramref name="unitOfJerk"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="Jerk"/>.</param>
    /// <param name="unitOfJerk">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfJerk unitOfJerk) => new(magnitude / unitOfJerk.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Jerk"/>.</summary>
    public Jerk Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Jerk"/> with negated magnitude.</summary>
    public Jerk Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Jerk"/>.</param>
    public static Jerk operator +(Jerk x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Jerk"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Jerk"/>.</param>
    public static Jerk operator -(Jerk x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="Jerk"/> <paramref name="term"/>, producing another <see cref="Jerk"/>.</summary>
    /// <param name="term">This <see cref="Jerk"/> is added to this instance.</param>
    public Jerk Add(Jerk term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="Jerk"/> <paramref name="term"/> from this instance, producing another <see cref="Jerk"/>.</summary>
    /// <param name="term">This <see cref="Jerk"/> is subtracted from this instance.</param>
    public Jerk Subtract(Jerk term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="Jerk"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="Jerk"/>.</summary>
    /// <param name="x">This <see cref="Jerk"/> is added to the <see cref="Jerk"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="Jerk"/> is added to the <see cref="Jerk"/> <paramref name="x"/>.</param>
    public static Jerk operator +(Jerk x, Jerk y) => x.Add(y);
    /// <summary>Subtract the <see cref="Jerk"/> <paramref name="y"/> from the <see cref="Jerk"/> <paramref name="x"/>, producing another <see cref="Jerk"/>.</summary>
    /// <param name="x">The <see cref="Jerk"/> <paramref name="y"/> is subtracted from this <see cref="Jerk"/>.</param>
    /// <param name="y">This <see cref="Jerk"/> is subtracted from the <see cref="Jerk"/> <paramref name="x"/>.</param>
    public static Jerk operator -(Jerk x, Jerk y) => x.Subtract(y);

    /// <summary>Divides this <see cref="Jerk"/> by the <see cref="Jerk"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Jerk"/> is divided by this <see cref="Jerk"/>.</param>
    public Scalar Divide(Jerk divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="Jerk"/> <paramref name="x"/> by the <see cref="Jerk"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="Jerk"/> is divided by the <see cref="Jerk"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Jerk"/> <paramref name="x"/> is divided by this <see cref="Jerk"/>.</param>
    public static Scalar operator /(Jerk x, Jerk y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="Jerk"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Jerk"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Jerk"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Jerk"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Jerk"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Jerk"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Jerk x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Jerk"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Jerk"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Jerk x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Jerk"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Jerk Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Jerk"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Jerk"/> is scaled.</param>
    public Jerk Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Jerk"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Jerk"/> is divided.</param>
    public Jerk Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Jerk"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Jerk operator %(Jerk x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Jerk"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Jerk"/> <paramref name="x"/>.</param>
    public static Jerk operator *(Jerk x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Jerk"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Jerk"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Jerk"/>, which is scaled by <paramref name="x"/>.</param>
    public static Jerk operator *(double x, Jerk y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Jerk"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Jerk"/> <paramref name="x"/>.</param>
    public static Jerk operator /(Jerk x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="Jerk"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Jerk Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Jerk"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Jerk"/> is scaled.</param>
    public Jerk Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Jerk"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Jerk"/> is divided.</param>
    public Jerk Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Jerk"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Jerk operator %(Jerk x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Jerk"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Jerk"/> <paramref name="x"/>.</param>
    public static Jerk operator *(Jerk x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Jerk"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Jerk"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Jerk"/>, which is scaled by <paramref name="x"/>.</param>
    public static Jerk operator *(Scalar x, Jerk y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Jerk"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Jerk"/> <paramref name="x"/>.</param>
    public static Jerk operator /(Jerk x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="Jerk"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Jerk"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Jerk"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Jerk"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Jerk"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Jerk"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Jerk.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Jerk x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Jerk"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Jerk"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Jerk"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Jerk.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Jerk x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Multiplies the <see cref="Jerk"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="Jerk3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Jerk"/>.</param>
    public Jerk3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="Jerk"/> with the <see cref="ValueTuple"/> <paramref name="vector"/> to produce a <see cref="Jerk3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Jerk"/>.</param>
    public Jerk3 Multiply((double x, double y, double z) vector) => new(Magnitude * vector.x, Magnitude * vector.y, Magnitude * vector.z);
    /// <summary>Multiplies the <see cref="Jerk"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="Jerk3"/>.</summary>
    /// <param name="a">This <see cref="Jerk"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Jerk"/> <paramref name="a"/>.</param>
    public static Jerk3 operator *(Jerk a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Jerk"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="Jerk3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Jerk"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Jerk"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Jerk3 operator *(Vector3 a, Jerk b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="Jerk"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="Jerk3"/>.</summary>
    /// <param name="a">This <see cref="Jerk"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="Jerk"/> <paramref name="a"/>.</param>
    public static Jerk3 operator *(Jerk a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Jerk"/> <paramref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="Jerk3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="Jerk"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Jerk"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static Jerk3 operator *((double x, double y, double z) a, Jerk b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Jerk x, Jerk y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Jerk x, Jerk y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Jerk x, Jerk y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Jerk x, Jerk y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="Jerk"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Jerk x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Jerk x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Jerk"/> of magnitude <paramref name="x"/>.</summary>
    public static Jerk FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Jerk"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Jerk(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Jerk"/> of equivalent magnitude.</summary>
    public static Jerk FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Jerk"/> of equivalent magnitude.</summary>
    public static explicit operator Jerk(Scalar x) => FromScalar(x);
}