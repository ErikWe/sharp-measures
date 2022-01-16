namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct SolidAngle :
    IComparable<SolidAngle>,
    IScalarQuantity<SolidAngle>,
    IAddableScalarQuantity<SolidAngle, SolidAngle>,
    ISubtractableScalarQuantity<SolidAngle, SolidAngle>,
    IDivisibleScalarQuantity<Scalar, SolidAngle>
{
    /// <summary>The zero-valued <see cref="SolidAngle"/>.</summary>
    public static SolidAngle Zero { get; } = new(0);

    /// <summary>The <see cref="SolidAngle"/> with magnitude 1, when expressed in unit <see cref="UnitOfSolidAngle.Steradian"/>.</summary>
    public static SolidAngle OneSteradian { get; } = new(1, UnitOfSolidAngle.Steradian);

    /// <summary>The magnitude of the <see cref="SolidAngle"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="SolidAngle.InSteradians"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="SolidAngle"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfSolidAngle"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SolidAngle"/>, in unit <paramref name="unitOfSolidAngle"/>.</param>
    /// <param name="unitOfSolidAngle">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="SolidAngle"/> a = 2.6 * <see cref="SolidAngle.OneSteradian"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public SolidAngle(double magnitude, UnitOfSolidAngle unitOfSolidAngle) : this(magnitude * unitOfSolidAngle.Factor) { }
    /// <summary>Constructs a new <see cref="SolidAngle"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="SolidAngle"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfSolidAngle"/> to be specified.</remarks>
    public SolidAngle(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="SolidAngle"/>, expressed in unit <see cref="UnitOfSolidAngle.Steradian"/>.</summary>
    public Scalar InSteradians => InUnit(UnitOfSolidAngle.Steradian);

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
    public SolidAngle Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public SolidAngle Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public SolidAngle Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public SolidAngle Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(SolidAngle other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="SolidAngle"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [sr]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="SolidAngle"/>, expressed in unit <paramref name="unitOfSolidAngle"/>.</summary>
    /// <param name="unitOfSolidAngle">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfSolidAngle unitOfSolidAngle) => InUnit(Magnitude, unitOfSolidAngle);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="SolidAngle"/>, expressed in unit <paramref name="unitOfSolidAngle"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="SolidAngle"/>.</param>
    /// <param name="unitOfSolidAngle">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfSolidAngle unitOfSolidAngle) => new(magnitude / unitOfSolidAngle.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="SolidAngle"/>.</summary>
    public SolidAngle Plus() => this;
    /// <summary>Negation, resulting in a <see cref="SolidAngle"/> with negated magnitude.</summary>
    public SolidAngle Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="SolidAngle"/>.</param>
    public static SolidAngle operator +(SolidAngle x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="SolidAngle"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="SolidAngle"/>.</param>
    public static SolidAngle operator -(SolidAngle x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="SolidAngle"/> <paramref name="term"/>, producing another <see cref="SolidAngle"/>.</summary>
    /// <param name="term">This <see cref="SolidAngle"/> is added to this instance.</param>
    public SolidAngle Add(SolidAngle term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="SolidAngle"/> <paramref name="term"/> from this instance, producing another <see cref="SolidAngle"/>.</summary>
    /// <param name="term">This <see cref="SolidAngle"/> is subtracted from this instance.</param>
    public SolidAngle Subtract(SolidAngle term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="SolidAngle"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="SolidAngle"/>.</summary>
    /// <param name="x">This <see cref="SolidAngle"/> is added to the <see cref="SolidAngle"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="SolidAngle"/> is added to the <see cref="SolidAngle"/> <paramref name="x"/>.</param>
    public static SolidAngle operator +(SolidAngle x, SolidAngle y) => x.Add(y);
    /// <summary>Subtract the <see cref="SolidAngle"/> <paramref name="y"/> from the <see cref="SolidAngle"/> <paramref name="x"/>, producing another <see cref="SolidAngle"/>.</summary>
    /// <param name="x">The <see cref="SolidAngle"/> <paramref name="y"/> is subtracted from this <see cref="SolidAngle"/>.</param>
    /// <param name="y">This <see cref="SolidAngle"/> is subtracted from the <see cref="SolidAngle"/> <paramref name="x"/>.</param>
    public static SolidAngle operator -(SolidAngle x, SolidAngle y) => x.Subtract(y);

    /// <summary>Divides this <see cref="SolidAngle"/> by the <see cref="SolidAngle"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="SolidAngle"/> is divided by this <see cref="SolidAngle"/>.</param>
    public Scalar Divide(SolidAngle divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="SolidAngle"/> <paramref name="x"/> by the <see cref="SolidAngle"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="SolidAngle"/> is divided by the <see cref="SolidAngle"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SolidAngle"/> <paramref name="x"/> is divided by this <see cref="SolidAngle"/>.</param>
    public static Scalar operator /(SolidAngle x, SolidAngle y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="SolidAngle"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="SolidAngle"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="SolidAngle"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="SolidAngle"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="SolidAngle"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SolidAngle"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(SolidAngle x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="SolidAngle"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="SolidAngle"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(SolidAngle x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="SolidAngle"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public SolidAngle Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="SolidAngle"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SolidAngle"/> is scaled.</param>
    public SolidAngle Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="SolidAngle"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SolidAngle"/> is divided.</param>
    public SolidAngle Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="SolidAngle"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static SolidAngle operator %(SolidAngle x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SolidAngle"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SolidAngle"/> <paramref name="x"/>.</param>
    public static SolidAngle operator *(SolidAngle x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SolidAngle"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SolidAngle"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SolidAngle"/>, which is scaled by <paramref name="x"/>.</param>
    public static SolidAngle operator *(double x, SolidAngle y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SolidAngle"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SolidAngle"/> <paramref name="x"/>.</param>
    public static SolidAngle operator /(SolidAngle x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="SolidAngle"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public SolidAngle Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="SolidAngle"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="SolidAngle"/> is scaled.</param>
    public SolidAngle Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="SolidAngle"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="SolidAngle"/> is divided.</param>
    public SolidAngle Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="SolidAngle"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static SolidAngle operator %(SolidAngle x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="SolidAngle"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="SolidAngle"/> <paramref name="x"/>.</param>
    public static SolidAngle operator *(SolidAngle x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="SolidAngle"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="SolidAngle"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="SolidAngle"/>, which is scaled by <paramref name="x"/>.</param>
    public static SolidAngle operator *(Scalar x, SolidAngle y) => y.Multiply(x);
    /// <summary>Scales the <see cref="SolidAngle"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="SolidAngle"/> <paramref name="x"/>.</param>
    public static SolidAngle operator /(SolidAngle x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="SolidAngle"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="SolidAngle"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="SolidAngle"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="SolidAngle"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="SolidAngle"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="SolidAngle"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="SolidAngle.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(SolidAngle x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="SolidAngle"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="SolidAngle"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="SolidAngle"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="SolidAngle.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(SolidAngle x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(SolidAngle x, SolidAngle y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(SolidAngle x, SolidAngle y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(SolidAngle x, SolidAngle y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(SolidAngle x, SolidAngle y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="SolidAngle"/> <paramref name="x"/>.</summary>
    public static implicit operator double(SolidAngle x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(SolidAngle x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="SolidAngle"/> of magnitude <paramref name="x"/>.</summary>
    public static SolidAngle FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="SolidAngle"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator SolidAngle(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="SolidAngle"/> of equivalent magnitude.</summary>
    public static SolidAngle FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="SolidAngle"/> of equivalent magnitude.</summary>
    public static explicit operator SolidAngle(Scalar x) => FromScalar(x);
}