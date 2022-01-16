namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct AngularVelocity :
    IComparable<AngularVelocity>,
    IScalarQuantity<AngularVelocity>,
    IAddableScalarQuantity<AngularVelocity, AngularVelocity>,
    ISubtractableScalarQuantity<AngularVelocity, AngularVelocity>,
    IDivisibleScalarQuantity<Scalar, AngularVelocity>,
    IVector3izableScalarQuantity<RotationalVelocity3>
{
    /// <summary>The zero-valued <see cref="AngularVelocity"/>.</summary>
    public static AngularVelocity Zero { get; } = new(0);

    /// <summary>The <see cref="AngularVelocity"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngularVelocity.RadianPerSecond"/>.</summary>
    public static AngularVelocity OneRadianPerSecond { get; } = new(1, UnitOfAngularVelocity.RadianPerSecond);
    /// <summary>The <see cref="AngularVelocity"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngularVelocity.DegreePerSecond"/>.</summary>
    public static AngularVelocity OneDegreePerSecond { get; } = new(1, UnitOfAngularVelocity.DegreePerSecond);
    /// <summary>The <see cref="AngularVelocity"/> with magnitude 1, when expressed in unit <see cref="UnitOfAngularVelocity.TurnPerSecond"/>.</summary>
    public static AngularVelocity OneTurnPerSecond { get; } = new(1, UnitOfAngularVelocity.TurnPerSecond);

    /// <summary>The magnitude of the <see cref="AngularVelocity"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="AngularVelocity.InDegreesPerSecond"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="AngularVelocity"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularVelocity"/>, in unit <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="unitOfAngularVelocity">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="AngularVelocity"/> a = 2.6 * <see cref="AngularVelocity.OneTurnPerSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public AngularVelocity(double magnitude, UnitOfAngularVelocity unitOfAngularVelocity) : this(magnitude * unitOfAngularVelocity.Factor) { }
    /// <summary>Constructs a new <see cref="AngularVelocity"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="AngularVelocity"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfAngularVelocity"/> to be specified.</remarks>
    public AngularVelocity(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="AngularVelocity"/>, expressed in unit <see cref="UnitOfAngularVelocity.RadianPerSecond"/>.</summary>
    public Scalar InRadiansPerSecond => InUnit(UnitOfAngularVelocity.RadianPerSecond);
    /// <summary>Retrieves the magnitude of the <see cref="AngularVelocity"/>, expressed in unit <see cref="UnitOfAngularVelocity.DegreePerSecond"/>.</summary>
    public Scalar InDegreesPerSecond => InUnit(UnitOfAngularVelocity.DegreePerSecond);
    /// <summary>Retrieves the magnitude of the <see cref="AngularVelocity"/>, expressed in unit <see cref="UnitOfAngularVelocity.TurnPerSecond"/>.</summary>
    public Scalar InTurnsPerSecond => InUnit(UnitOfAngularVelocity.TurnPerSecond);

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
    public AngularVelocity Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public AngularVelocity Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public AngularVelocity Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public AngularVelocity Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(AngularVelocity other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="AngularVelocity"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [rad * s^-1]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="AngularVelocity"/>, expressed in unit <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="unitOfAngularVelocity">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfAngularVelocity unitOfAngularVelocity) => InUnit(Magnitude, unitOfAngularVelocity);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="AngularVelocity"/>, expressed in unit <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="AngularVelocity"/>.</param>
    /// <param name="unitOfAngularVelocity">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfAngularVelocity unitOfAngularVelocity) => new(magnitude / unitOfAngularVelocity.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="AngularVelocity"/>.</summary>
    public AngularVelocity Plus() => this;
    /// <summary>Negation, resulting in a <see cref="AngularVelocity"/> with negated magnitude.</summary>
    public AngularVelocity Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="AngularVelocity"/>.</param>
    public static AngularVelocity operator +(AngularVelocity x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="AngularVelocity"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="AngularVelocity"/>.</param>
    public static AngularVelocity operator -(AngularVelocity x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="AngularVelocity"/> <paramref name="term"/>, producing another <see cref="AngularVelocity"/>.</summary>
    /// <param name="term">This <see cref="AngularVelocity"/> is added to this instance.</param>
    public AngularVelocity Add(AngularVelocity term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="AngularVelocity"/> <paramref name="term"/> from this instance, producing another <see cref="AngularVelocity"/>.</summary>
    /// <param name="term">This <see cref="AngularVelocity"/> is subtracted from this instance.</param>
    public AngularVelocity Subtract(AngularVelocity term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="AngularVelocity"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="AngularVelocity"/>.</summary>
    /// <param name="x">This <see cref="AngularVelocity"/> is added to the <see cref="AngularVelocity"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="AngularVelocity"/> is added to the <see cref="AngularVelocity"/> <paramref name="x"/>.</param>
    public static AngularVelocity operator +(AngularVelocity x, AngularVelocity y) => x.Add(y);
    /// <summary>Subtract the <see cref="AngularVelocity"/> <paramref name="y"/> from the <see cref="AngularVelocity"/> <paramref name="x"/>, producing another <see cref="AngularVelocity"/>.</summary>
    /// <param name="x">The <see cref="AngularVelocity"/> <paramref name="y"/> is subtracted from this <see cref="AngularVelocity"/>.</param>
    /// <param name="y">This <see cref="AngularVelocity"/> is subtracted from the <see cref="AngularVelocity"/> <paramref name="x"/>.</param>
    public static AngularVelocity operator -(AngularVelocity x, AngularVelocity y) => x.Subtract(y);

    /// <summary>Divides this <see cref="AngularVelocity"/> by the <see cref="AngularVelocity"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="AngularVelocity"/> is divided by this <see cref="AngularVelocity"/>.</param>
    public Scalar Divide(AngularVelocity divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="AngularVelocity"/> <paramref name="x"/> by the <see cref="AngularVelocity"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="AngularVelocity"/> is divided by the <see cref="AngularVelocity"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="AngularVelocity"/> <paramref name="x"/> is divided by this <see cref="AngularVelocity"/>.</param>
    public static Scalar operator /(AngularVelocity x, AngularVelocity y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="AngularVelocity"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="AngularVelocity"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="AngularVelocity"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="AngularVelocity"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="AngularVelocity"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularVelocity"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="AngularVelocity"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(AngularVelocity x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="AngularVelocity"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularVelocity"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="AngularVelocity"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(AngularVelocity x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="AngularVelocity"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public AngularVelocity Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="AngularVelocity"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="AngularVelocity"/> is scaled.</param>
    public AngularVelocity Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="AngularVelocity"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="AngularVelocity"/> is divided.</param>
    public AngularVelocity Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="AngularVelocity"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularVelocity"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static AngularVelocity operator %(AngularVelocity x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="AngularVelocity"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularVelocity"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="AngularVelocity"/> <paramref name="x"/>.</param>
    public static AngularVelocity operator *(AngularVelocity x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="AngularVelocity"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="AngularVelocity"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="AngularVelocity"/>, which is scaled by <paramref name="x"/>.</param>
    public static AngularVelocity operator *(double x, AngularVelocity y) => y.Multiply(x);
    /// <summary>Scales the <see cref="AngularVelocity"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularVelocity"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="AngularVelocity"/> <paramref name="x"/>.</param>
    public static AngularVelocity operator /(AngularVelocity x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="AngularVelocity"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public AngularVelocity Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="AngularVelocity"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="AngularVelocity"/> is scaled.</param>
    public AngularVelocity Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="AngularVelocity"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="AngularVelocity"/> is divided.</param>
    public AngularVelocity Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="AngularVelocity"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularVelocity"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static AngularVelocity operator %(AngularVelocity x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="AngularVelocity"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularVelocity"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="AngularVelocity"/> <paramref name="x"/>.</param>
    public static AngularVelocity operator *(AngularVelocity x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="AngularVelocity"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="AngularVelocity"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="AngularVelocity"/>, which is scaled by <paramref name="x"/>.</param>
    public static AngularVelocity operator *(Scalar x, AngularVelocity y) => y.Multiply(x);
    /// <summary>Scales the <see cref="AngularVelocity"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="AngularVelocity"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="AngularVelocity"/> <paramref name="x"/>.</param>
    public static AngularVelocity operator /(AngularVelocity x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="AngularVelocity"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="AngularVelocity"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="AngularVelocity"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="AngularVelocity"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="AngularVelocity"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularVelocity"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="AngularVelocity"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="AngularVelocity.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(AngularVelocity x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="AngularVelocity"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="AngularVelocity"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="AngularVelocity"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="AngularVelocity.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(AngularVelocity x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Multiplies the <see cref="AngularVelocity"/> with the <see cref="Vector3"/> <paramref name="vector"/> to produce a <see cref="RotationalVelocity3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="AngularVelocity"/>.</param>
    public RotationalVelocity3 Multiply(Vector3 vector) => new(vector * Magnitude);
    /// <summary>Multiplies the <see cref="AngularVelocity"/> with the <see cref="ValueTuple"/> <paramref name="vector"/> to produce a <see cref="RotationalVelocity3"/>.</summary>
    /// <param name="vector">This <see cref="Vector3"/> is multiplied by the <see cref="AngularVelocity"/>.</param>
    public RotationalVelocity3 Multiply((double x, double y, double z) vector) => new(Magnitude * vector.x, Magnitude * vector.y, Magnitude * vector.z);
    /// <summary>Multiplies the <see cref="AngularVelocity"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/> to produce a <see cref="RotationalVelocity3"/>.</summary>
    /// <param name="a">This <see cref="AngularVelocity"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="AngularVelocity"/> <paramref name="a"/>.</param>
    public static RotationalVelocity3 operator *(AngularVelocity a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="AngularVelocity"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/> to produce a <see cref="RotationalVelocity3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="AngularVelocity"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="AngularVelocity"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static RotationalVelocity3 operator *(Vector3 a, AngularVelocity b) => b.Multiply(a);
    /// <summary>Multiplies the <see cref="AngularVelocity"/> <paramref name="a"/> with the <see cref="ValueTuple"/> <paramref name="b"/> to produce a <see cref="RotationalVelocity3"/>.</summary>
    /// <param name="a">This <see cref="AngularVelocity"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="ValueTuple"/> is multiplied by the <see cref="AngularVelocity"/> <paramref name="a"/>.</param>
    public static RotationalVelocity3 operator *(AngularVelocity a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplies the <see cref="AngularVelocity"/> <paramref name="b"/> with the <see cref="ValueTuple"/> <paramref name="a"/> to produce a <see cref="RotationalVelocity3"/>.</summary>
    /// <param name="a">This <see cref="ValueTuple"/> is multiplied by the <see cref="AngularVelocity"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="AngularVelocity"/> is multiplied by the <see cref="ValueTuple"/> <paramref name="a"/>.</param>
    public static RotationalVelocity3 operator *((double x, double y, double z) a, AngularVelocity b) => b.Multiply(a);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(AngularVelocity x, AngularVelocity y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(AngularVelocity x, AngularVelocity y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(AngularVelocity x, AngularVelocity y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(AngularVelocity x, AngularVelocity y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="AngularVelocity"/> <paramref name="x"/>.</summary>
    public static implicit operator double(AngularVelocity x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(AngularVelocity x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularVelocity"/> of magnitude <paramref name="x"/>.</summary>
    public static AngularVelocity FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularVelocity"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator AngularVelocity(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularVelocity"/> of equivalent magnitude.</summary>
    public static AngularVelocity FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="AngularVelocity"/> of equivalent magnitude.</summary>
    public static explicit operator AngularVelocity(Scalar x) => FromScalar(x);
}