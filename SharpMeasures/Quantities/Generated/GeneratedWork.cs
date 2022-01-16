namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct Work :
    IComparable<Work>,
    IScalarQuantity<Work>,
    IAddableScalarQuantity<Work, Work>,
    ISubtractableScalarQuantity<Work, Work>,
    IDivisibleScalarQuantity<Scalar, Work>
{
    /// <summary>The zero-valued <see cref="Work"/>.</summary>
    public static Work Zero { get; } = new(0);

    /// <summary>The <see cref="Work"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Joule"/>.</summary>
    public static Work OneJoule { get; } = new(1, UnitOfEnergy.Joule);
    /// <summary>The <see cref="Work"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Kilojoule"/>.</summary>
    public static Work OneKilojoule { get; } = new(1, UnitOfEnergy.Kilojoule);
    /// <summary>The <see cref="Work"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Megajoule"/>.</summary>
    public static Work OneMegajoule { get; } = new(1, UnitOfEnergy.Megajoule);
    /// <summary>The <see cref="Work"/> with magnitude 1, when expressed in unit <see cref="UnitOfEnergy.Gigajoule"/>.</summary>
    public static Work OneGigajoule { get; } = new(1, UnitOfEnergy.Gigajoule);

    /// <summary>The magnitude of the <see cref="Work"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Work.InJoules"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Work"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Work"/>, in unit <paramref name="unitOfEnergy"/>.</param>
    /// <param name="unitOfEnergy">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Work"/> a = 2.6 * <see cref="Work.OneKilojoule"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Work(double magnitude, UnitOfEnergy unitOfEnergy) : this(magnitude * unitOfEnergy.Factor) { }
    /// <summary>Constructs a new <see cref="Work"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Work"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfEnergy"/> to be specified.</remarks>
    public Work(double magnitude)
    {
        Magnitude = magnitude;
    }

    public PotentialEnergy AsPotentialEnergy => new(Magnitude);
    public KineticEnergy AsKineticEnergy => new(Magnitude);
    public Torque AsTorque => new(Magnitude);

    /// <summary>Retrieves the magnitude of the <see cref="Work"/>, expressed in unit <see cref="UnitOfEnergy.Joule"/>.</summary>
    public Scalar InJoules => InUnit(UnitOfEnergy.Joule);
    /// <summary>Retrieves the magnitude of the <see cref="Work"/>, expressed in unit <see cref="UnitOfEnergy.Kilojoule"/>.</summary>
    public Scalar InKilojoules => InUnit(UnitOfEnergy.Kilojoule);
    /// <summary>Retrieves the magnitude of the <see cref="Work"/>, expressed in unit <see cref="UnitOfEnergy.Megajoule"/>.</summary>
    public Scalar InMegajoules => InUnit(UnitOfEnergy.Megajoule);
    /// <summary>Retrieves the magnitude of the <see cref="Work"/>, expressed in unit <see cref="UnitOfEnergy.Gigajoule"/>.</summary>
    public Scalar InGigajoules => InUnit(UnitOfEnergy.Gigajoule);

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
    public Work Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public Work Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public Work Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public Work Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Work other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Work"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [J]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Work"/>, expressed in unit <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="unitOfEnergy">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfEnergy unitOfEnergy) => InUnit(Magnitude, unitOfEnergy);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Work"/>, expressed in unit <paramref name="unitOfEnergy"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="Work"/>.</param>
    /// <param name="unitOfEnergy">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfEnergy unitOfEnergy) => new(magnitude / unitOfEnergy.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Work"/>.</summary>
    public Work Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Work"/> with negated magnitude.</summary>
    public Work Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Work"/>.</param>
    public static Work operator +(Work x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Work"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Work"/>.</param>
    public static Work operator -(Work x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="Work"/> <paramref name="term"/>, producing another <see cref="Work"/>.</summary>
    /// <param name="term">This <see cref="Work"/> is added to this instance.</param>
    public Work Add(Work term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="Work"/> <paramref name="term"/> from this instance, producing another <see cref="Work"/>.</summary>
    /// <param name="term">This <see cref="Work"/> is subtracted from this instance.</param>
    public Work Subtract(Work term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="Work"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="Work"/>.</summary>
    /// <param name="x">This <see cref="Work"/> is added to the <see cref="Work"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="Work"/> is added to the <see cref="Work"/> <paramref name="x"/>.</param>
    public static Work operator +(Work x, Work y) => x.Add(y);
    /// <summary>Subtract the <see cref="Work"/> <paramref name="y"/> from the <see cref="Work"/> <paramref name="x"/>, producing another <see cref="Work"/>.</summary>
    /// <param name="x">The <see cref="Work"/> <paramref name="y"/> is subtracted from this <see cref="Work"/>.</param>
    /// <param name="y">This <see cref="Work"/> is subtracted from the <see cref="Work"/> <paramref name="x"/>.</param>
    public static Work operator -(Work x, Work y) => x.Subtract(y);

    /// <summary>Divides this <see cref="Work"/> by the <see cref="Work"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Work"/> is divided by this <see cref="Work"/>.</param>
    public Scalar Divide(Work divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="Work"/> <paramref name="x"/> by the <see cref="Work"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="Work"/> is divided by the <see cref="Work"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Work"/> <paramref name="x"/> is divided by this <see cref="Work"/>.</param>
    public static Scalar operator /(Work x, Work y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="Work"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Work"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Work"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Work"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Work"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Work"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Work"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Work x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Work"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Work"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Work"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Work x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Work"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Work Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Work"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Work"/> is scaled.</param>
    public Work Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Work"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Work"/> is divided.</param>
    public Work Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Work"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Work"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Work operator %(Work x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Work"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Work"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Work"/> <paramref name="x"/>.</param>
    public static Work operator *(Work x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Work"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Work"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Work"/>, which is scaled by <paramref name="x"/>.</param>
    public static Work operator *(double x, Work y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Work"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Work"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Work"/> <paramref name="x"/>.</param>
    public static Work operator /(Work x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="Work"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Work Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Work"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Work"/> is scaled.</param>
    public Work Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Work"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Work"/> is divided.</param>
    public Work Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Work"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Work"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Work operator %(Work x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Work"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Work"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Work"/> <paramref name="x"/>.</param>
    public static Work operator *(Work x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Work"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Work"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Work"/>, which is scaled by <paramref name="x"/>.</param>
    public static Work operator *(Scalar x, Work y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Work"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Work"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Work"/> <paramref name="x"/>.</param>
    public static Work operator /(Work x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="Work"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Work"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Work"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Work"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Work"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Work"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Work"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Work.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Work x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Work"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Work"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Work"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Work.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Work x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Work x, Work y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Work x, Work y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Work x, Work y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Work x, Work y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="Work"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Work x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Work x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Work"/> of magnitude <paramref name="x"/>.</summary>
    public static Work FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Work"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Work(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Work"/> of equivalent magnitude.</summary>
    public static Work FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Work"/> of equivalent magnitude.</summary>
    public static explicit operator Work(Scalar x) => FromScalar(x);
}