namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct Force :
    IComparable<Force>,
    IScalarQuantity<Force>,
    IAddableScalarQuantity<Force, Force>,
    ISubtractableScalarQuantity<Force, Force>,
    IDivisibleScalarQuantity<Scalar, Force>,
    IVector3izableScalarQuantity<Force3>
{
    /// <summary>The zero-valued <see cref="Force"/>.</summary>
    public static Force Zero { get; } = new(0);

    /// <summary>The <see cref="Force"/> with magnitude 1, when expressed in unit <see cref="UnitOfForce.Newton"/>.</summary>
    public static Force OneNewton { get; } = new(1, UnitOfForce.Newton);
    /// <summary>The <see cref="Force"/> with magnitude 1, when expressed in unit <see cref="UnitOfForce.PoundForce"/>.</summary>
    public static Force OnePoundForce { get; } = new(1, UnitOfForce.PoundForce);

    /// <summary>The magnitude of the <see cref="Force"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Force.InNewtons"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Force"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfForce"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Force"/>, in unit <paramref name="unitOfForce"/>.</param>
    /// <param name="unitOfForce">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Force"/> a = 2.6 * <see cref="Force.OnePoundForce"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Force(double magnitude, UnitOfForce unitOfForce) : this(magnitude * unitOfForce.Factor) { }
    /// <summary>Constructs a new <see cref="Force"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Force"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfForce"/> to be specified.</remarks>
    public Force(double magnitude)
    {
        Magnitude = magnitude;
    }

    public Weight AsWeight => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="Force"/>, expressed in unit <see cref="UnitOfForce.Newton"/>.</summary>
    public Scalar InNewtons => InUnit(UnitOfForce.Newton);
    /// <summary>Retrieves the magnitude of the <see cref="Force"/>, expressed in unit <see cref="UnitOfForce.PoundForce"/>.</summary>
    public Scalar InPoundsForce => InUnit(UnitOfForce.PoundForce);

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
    public Force Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public Force Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public Force Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public Force Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Force other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Force"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [N]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Force"/>, expressed in unit <paramref name="unitOfForce"/>.</summary>
    /// <param name="unitOfForce">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfForce unitOfForce) => InUnit(Magnitude, unitOfForce);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Force"/>, expressed in unit <paramref name="unitOfForce"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="Force"/>.</param>
    /// <param name="unitOfForce">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfForce unitOfForce) => new(magnitude / unitOfForce.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Force"/>.</summary>
    public Force Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Force"/> with negated magnitude.</summary>
    public Force Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Force"/>.</param>
    public static Force operator +(Force x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Force"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Force"/>.</param>
    public static Force operator -(Force x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="Force"/> <paramref name="term"/>, producing another <see cref="Force"/>.</summary>
    /// <param name="term">This <see cref="Force"/> is added to this instance.</param>
    public Force Add(Force term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="Force"/> <paramref name="term"/> from this instance, producing another <see cref="Force"/>.</summary>
    /// <param name="term">This <see cref="Force"/> is subtracted from this instance.</param>
    public Force Subtract(Force term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="Force"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="Force"/>.</summary>
    /// <param name="x">This <see cref="Force"/> is added to the <see cref="Force"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="Force"/> is added to the <see cref="Force"/> <paramref name="x"/>.</param>
    public static Force operator +(Force x, Force y) => x.Add(y);
    /// <summary>Subtract the <see cref="Force"/> <paramref name="y"/> from the <see cref="Force"/> <paramref name="x"/>, producing another <see cref="Force"/>.</summary>
    /// <param name="x">The <see cref="Force"/> <paramref name="y"/> is subtracted from this <see cref="Force"/>.</param>
    /// <param name="y">This <see cref="Force"/> is subtracted from the <see cref="Force"/> <paramref name="x"/>.</param>
    public static Force operator -(Force x, Force y) => x.Subtract(y);

    /// <summary>Divides this <see cref="Force"/> by the <see cref="Force"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Force"/> is divided by this <see cref="Force"/>.</param>
    public Scalar Divide(Force divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="Force"/> <paramref name="x"/> by the <see cref="Force"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="Force"/> is divided by the <see cref="Force"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Force"/> <paramref name="x"/> is divided by this <see cref="Force"/>.</param>
    public static Scalar operator /(Force x, Force y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="Force"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Force"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Force"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Force"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Force"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Force"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Force"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Force x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Force"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Force"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Force"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Force x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Force"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Force Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Force"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Force"/> is scaled.</param>
    public Force Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Force"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Force"/> is divided.</param>
    public Force Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Force"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Force"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Force operator %(Force x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Force"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Force"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Force"/> <paramref name="x"/>.</param>
    public static Force operator *(Force x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Force"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Force"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Force"/>, which is scaled by <paramref name="x"/>.</param>
    public static Force operator *(double x, Force y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Force"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Force"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Force"/> <paramref name="x"/>.</param>
    public static Force operator /(Force x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="Force"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Force Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Force"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Force"/> is scaled.</param>
    public Force Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Force"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Force"/> is divided.</param>
    public Force Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Force"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Force"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Force operator %(Force x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Force"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Force"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Force"/> <paramref name="x"/>.</param>
    public static Force operator *(Force x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Force"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Force"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Force"/>, which is scaled by <paramref name="x"/>.</param>
    public static Force operator *(Scalar x, Force y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Force"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Force"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Force"/> <paramref name="x"/>.</param>
    public static Force operator /(Force x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="Force"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Force"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Force"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Force"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Force"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Force"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Force"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Force.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Force x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Force"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Force"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Force"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Force.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Force x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Multiplies the <see cref="Force"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="Force3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Force"/>.</param>
    public Force3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="Force"/> with the <see cref="ValueTuple"/> <paramref name="vector"/> to produce a <see cref="Force3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Force"/>.</param>
    public Force3 Multiply((double x, double y, double z) vector) => new(Magnitude * vector.x, Magnitude * vector.y, Magnitude * vector.z);
    /// <summary>Multiplies the <see cref="Force"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="Force3"/>.</summary>
    /// <param name="a">This <see cref="Force"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Force"/> <paramref name="a"/>.</param>
    public static Force3 operator *(Force a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Force"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="Force3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Force"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Force"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Force3 operator *(Vector3 a, Force b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="Force"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="Force3"/>.</summary>
    /// <param name="a">This <see cref="Force"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="Force"/> <paramref name="a"/>.</param>
    public static Force3 operator *(Force a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Force"/> <paramref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="Force3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="Force"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Force"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static Force3 operator *((double x, double y, double z) a, Force b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Force x, Force y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Force x, Force y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Force x, Force y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Force x, Force y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="Force"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Force x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Force x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Force"/> of magnitude <paramref name="x"/>.</summary>
    public static Force FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Force"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Force(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Force"/> of equivalent magnitude.</summary>
    public static Force FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Force"/> of equivalent magnitude.</summary>
    public static explicit operator Force(Scalar x) => FromScalar(x);
}