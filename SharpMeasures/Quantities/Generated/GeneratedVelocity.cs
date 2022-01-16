namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct Velocity :
    IComparable<Velocity>,
    IScalarQuantity<Velocity>,
    ISquarableScalarQuantity<VelocitySquared>,
    IAddableScalarQuantity<Velocity, Velocity>,
    ISubtractableScalarQuantity<Velocity, Velocity>,
    IDivisibleScalarQuantity<Scalar, Velocity>,
    IVector3izableScalarQuantity<Velocity3>
{
    /// <summary>The zero-valued <see cref="Velocity"/>.</summary>
    public static Velocity Zero { get; } = new(0);

    /// <summary>The <see cref="Velocity"/> with magnitude 1, when expressed in unit <see cref="UnitOfVelocity.MetrePerSecond"/>.</summary>
    public static Velocity OneMetrePerSecond { get; } = new(1, UnitOfVelocity.MetrePerSecond);
    /// <summary>The <see cref="Velocity"/> with magnitude 1, when expressed in unit <see cref="UnitOfVelocity.KilometrePerHour"/>.</summary>
    public static Velocity OneKilometrePerHour { get; } = new(1, UnitOfVelocity.KilometrePerHour);

    /// <summary>The <see cref="Velocity"/> with magnitude 1, when expressed in unit <see cref="UnitOfVelocity.MilePerHour"/>.</summary>
    public static Velocity OneMilePerHour { get; } = new(1, UnitOfVelocity.MilePerHour);

    /// <summary>Constructs a <see cref="Velocity"/> by taking the square root of the <see cref="VelocitySquared"/> <paramref name="velocitySquared"/>.</summary>
    /// <param name="velocitySquared">The square root of this <see cref="VelocitySquared"/> is taken to produce a <see cref="Velocity"/>.</param>
    public static Velocity From(VelocitySquared velocitySquared) => new(Math.Sqrt(velocitySquared.InSquareMetresPerSecondSquared));

    /// <summary>The magnitude of the <see cref="Velocity"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Velocity.InKilometresPerHour"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Velocity"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfVelocity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Velocity"/>, in unit <paramref name="unitOfVelocity"/>.</param>
    /// <param name="unitOfVelocity">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Velocity"/> a = 2.6 * <see cref="Velocity.OneMilePerHour"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Velocity(double magnitude, UnitOfVelocity unitOfVelocity) : this(magnitude * unitOfVelocity.Factor) { }
    /// <summary>Constructs a new <see cref="Velocity"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Velocity"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfVelocity"/> to be specified.</remarks>
    public Velocity(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Velocity"/>, expressed in unit <see cref="UnitOfVelocity.MetrePerSecond"/>.</summary>
    public Scalar InMetresPerSecond => InUnit(UnitOfVelocity.MetrePerSecond);
    /// <summary>Retrieves the magnitude of the <see cref="Velocity"/>, expressed in unit <see cref="UnitOfVelocity.KilometrePerHour"/>.</summary>
    public Scalar InKilometresPerHour => InUnit(UnitOfVelocity.KilometrePerHour);

    /// <summary>Retrieves the magnitude of the <see cref="Velocity"/>, expressed in unit <see cref="UnitOfVelocity.MilePerHour"/>.</summary>
    public Scalar InMilesPerHour => InUnit(UnitOfVelocity.MilePerHour);

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
    public Velocity Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public Velocity Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public Velocity Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public Velocity Round() => new(Math.Round(Magnitude));

    /// <summary>Squares the <see cref="Velocity"/>, producing a <see cref="VelocitySquared"/>.</summary>
    public VelocitySquared Square() => VelocitySquared.From(this);

    /// <inheritdoc/>
    public int CompareTo(Velocity other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Velocity"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [m * s^-1]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Velocity"/>, expressed in unit <paramref name="unitOfVelocity"/>.</summary>
    /// <param name="unitOfVelocity">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfVelocity unitOfVelocity) => InUnit(Magnitude, unitOfVelocity);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Velocity"/>, expressed in unit <paramref name="unitOfVelocity"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="Velocity"/>.</param>
    /// <param name="unitOfVelocity">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfVelocity unitOfVelocity) => new(magnitude / unitOfVelocity.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Velocity"/>.</summary>
    public Velocity Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Velocity"/> with negated magnitude.</summary>
    public Velocity Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Velocity"/>.</param>
    public static Velocity operator +(Velocity x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Velocity"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Velocity"/>.</param>
    public static Velocity operator -(Velocity x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="Velocity"/> <paramref name="term"/>, producing another <see cref="Velocity"/>.</summary>
    /// <param name="term">This <see cref="Velocity"/> is added to this instance.</param>
    public Velocity Add(Velocity term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="Velocity"/> <paramref name="term"/> from this instance, producing another <see cref="Velocity"/>.</summary>
    /// <param name="term">This <see cref="Velocity"/> is subtracted from this instance.</param>
    public Velocity Subtract(Velocity term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="Velocity"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="Velocity"/>.</summary>
    /// <param name="x">This <see cref="Velocity"/> is added to the <see cref="Velocity"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="Velocity"/> is added to the <see cref="Velocity"/> <paramref name="x"/>.</param>
    public static Velocity operator +(Velocity x, Velocity y) => x.Add(y);
    /// <summary>Subtract the <see cref="Velocity"/> <paramref name="y"/> from the <see cref="Velocity"/> <paramref name="x"/>, producing another <see cref="Velocity"/>.</summary>
    /// <param name="x">The <see cref="Velocity"/> <paramref name="y"/> is subtracted from this <see cref="Velocity"/>.</param>
    /// <param name="y">This <see cref="Velocity"/> is subtracted from the <see cref="Velocity"/> <paramref name="x"/>.</param>
    public static Velocity operator -(Velocity x, Velocity y) => x.Subtract(y);

    /// <summary>Divides this <see cref="Velocity"/> by the <see cref="Velocity"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Velocity"/> is divided by this <see cref="Velocity"/>.</param>
    public Scalar Divide(Velocity divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="Velocity"/> <paramref name="x"/> by the <see cref="Velocity"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="Velocity"/> is divided by the <see cref="Velocity"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Velocity"/> <paramref name="x"/> is divided by this <see cref="Velocity"/>.</param>
    public static Scalar operator /(Velocity x, Velocity y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="Velocity"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Velocity"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Velocity"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Velocity"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Velocity"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Velocity"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Velocity"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Velocity x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Velocity"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Velocity"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Velocity"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Velocity x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Velocity"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Velocity Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Velocity"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Velocity"/> is scaled.</param>
    public Velocity Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Velocity"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Velocity"/> is divided.</param>
    public Velocity Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Velocity"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Velocity"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Velocity operator %(Velocity x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Velocity"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Velocity"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Velocity"/> <paramref name="x"/>.</param>
    public static Velocity operator *(Velocity x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Velocity"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Velocity"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Velocity"/>, which is scaled by <paramref name="x"/>.</param>
    public static Velocity operator *(double x, Velocity y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Velocity"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Velocity"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Velocity"/> <paramref name="x"/>.</param>
    public static Velocity operator /(Velocity x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="Velocity"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Velocity Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Velocity"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Velocity"/> is scaled.</param>
    public Velocity Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Velocity"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Velocity"/> is divided.</param>
    public Velocity Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Velocity"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Velocity"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Velocity operator %(Velocity x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Velocity"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Velocity"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Velocity"/> <paramref name="x"/>.</param>
    public static Velocity operator *(Velocity x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Velocity"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Velocity"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Velocity"/>, which is scaled by <paramref name="x"/>.</param>
    public static Velocity operator *(Scalar x, Velocity y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Velocity"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Velocity"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Velocity"/> <paramref name="x"/>.</param>
    public static Velocity operator /(Velocity x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="Velocity"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Velocity"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Velocity"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Velocity"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Velocity"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Velocity"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Velocity"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Velocity.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Velocity x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Velocity"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Velocity"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Velocity"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Velocity.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Velocity x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Multiplies the <see cref="Velocity"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="Velocity3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Velocity"/>.</param>
    public Velocity3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="Velocity"/> with the <see cref="ValueTuple"/> <paramref name="vector"/> to produce a <see cref="Velocity3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="Velocity"/>.</param>
    public Velocity3 Multiply((double x, double y, double z) vector) => new(Magnitude * vector.x, Magnitude * vector.y, Magnitude * vector.z);
    /// <summary>Multiplies the <see cref="Velocity"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="Velocity3"/>.</summary>
    /// <param name="a">This <see cref="Velocity"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Velocity"/> <paramref name="a"/>.</param>
    public static Velocity3 operator *(Velocity a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Velocity"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="Velocity3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Velocity"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Velocity"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Velocity3 operator *(Vector3 a, Velocity b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="Velocity"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="Velocity3"/>.</summary>
    /// <param name="a">This <see cref="Velocity"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="Velocity"/> <paramref name="a"/>.</param>
    public static Velocity3 operator *(Velocity a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="Velocity"/> <paramref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="Velocity3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="Velocity"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Velocity"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static Velocity3 operator *((double x, double y, double z) a, Velocity b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Velocity x, Velocity y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Velocity x, Velocity y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Velocity x, Velocity y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Velocity x, Velocity y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="Velocity"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Velocity x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Velocity x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Velocity"/> of magnitude <paramref name="x"/>.</summary>
    public static Velocity FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Velocity"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Velocity(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Velocity"/> of equivalent magnitude.</summary>
    public static Velocity FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Velocity"/> of equivalent magnitude.</summary>
    public static explicit operator Velocity(Scalar x) => FromScalar(x);
}