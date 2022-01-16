namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Mass"/>, used for describing masses of objects.
/// <para>
/// New instances of <see cref="Mass"/> can be constructed according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Mass"/> a = 5 * <see cref="Mass.OneKilogram"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Mass"/> b = new(7, <see cref="UnitOfMass.Ounce"/>);
/// </code>
/// </item>
/// </list>
/// Instances of <see cref="Mass"/> may be applied according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Density"/> c = 3 * <see cref="Mass.OnePound"/> / <see cref="Volume.OneLitre"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved in a desired unit according to:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Mass.InPounds"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Mass.InUnit(UnitOfMass)"/>;
/// </code>
/// </item>
/// </list>
/// </para>
/// </summary>
public readonly partial record struct Mass :
    IComparable<Mass>,
    IScalarQuantity<Mass>,
    IAddableScalarQuantity<Mass, Mass>,
    ISubtractableScalarQuantity<Mass, Mass>,
    IDivisibleScalarQuantity<Scalar, Mass>
{
    /// <summary>The zero-valued <see cref="Mass"/>.</summary>
    public static Mass Zero { get; } = new(0);

    /// <summary>The <see cref="Mass"/> with magnitude 1, when expressed in unit <see cref="UnitOfMass.Milligram"/>.</summary>
    public static Mass OneMilligram { get; } = new(1, UnitOfMass.Milligram);
    /// <summary>The <see cref="Mass"/> with magnitude 1, when expressed in unit <see cref="UnitOfMass.Gram"/>.</summary>
    public static Mass OneGram { get; } = new(1, UnitOfMass.Gram);
    /// <summary>The <see cref="Mass"/> with magnitude 1, when expressed in unit <see cref="UnitOfMass.Hectogram"/>.</summary>
    public static Mass OneHectogram { get; } = new(1, UnitOfMass.Hectogram);
    /// <summary>The <see cref="Mass"/> with magnitude 1, when expressed in unit <see cref="UnitOfMass.Kilogram"/>.</summary>
    public static Mass OneKilogram { get; } = new(1, UnitOfMass.Kilogram);
    /// <summary>The <see cref="Mass"/> with magnitude 1, when expressed in unit <see cref="UnitOfMass.Tonne"/>.</summary>
    public static Mass OneTonne { get; } = new(1, UnitOfMass.Tonne);

    /// <summary>The <see cref="Mass"/> with magnitude 1, when expressed in unit <see cref="UnitOfMass.Ounce"/>.</summary>
    public static Mass OneOunce { get; } = new(1, UnitOfMass.Ounce);
    /// <summary>The <see cref="Mass"/> with magnitude 1, when expressed in unit <see cref="UnitOfMass.Pound"/>.</summary>
    public static Mass OnePound { get; } = new(1, UnitOfMass.Pound);

    /// <summary>The magnitude of the <see cref="Mass"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Mass.InTonnes"/>.
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Mass"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfMass"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Mass"/>, in unit <paramref name="unitOfMass"/>.</param>
    /// <param name="unitOfMass">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Mass"/> a = 2.6 * <see cref="Mass.OneKilogram"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Mass(double magnitude, UnitOfMass unitOfMass) : this(magnitude * unitOfMass.Factor) { }
    /// <summary>Constructs a new <see cref="Mass"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Mass"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfMass"/> to be specified.</remarks>
    public Mass(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Mass"/>, expressed in unit <see cref="UnitOfMass.Milligram"/>.</summary>
    public Scalar InMilligrams => InUnit(UnitOfMass.Milligram);
    /// <summary>Retrieves the magnitude of the <see cref="Mass"/>, expressed in unit <see cref="UnitOfMass.Gram"/>.</summary>
    public Scalar InGrams => InUnit(UnitOfMass.Gram);
    /// <summary>Retrieves the magnitude of the <see cref="Mass"/>, expressed in unit <see cref="UnitOfMass.Hectogram"/>.</summary>
    public Scalar InHectograms => InUnit(UnitOfMass.Hectogram);
    /// <summary>Retrieves the magnitude of the <see cref="Mass"/>, expressed in unit <see cref="UnitOfMass.Kilogram"/>.</summary>
    public Scalar InKilograms => InUnit(UnitOfMass.Kilogram);
    /// <summary>Retrieves the magnitude of the <see cref="Mass"/>, expressed in unit <see cref="UnitOfMass.Tonne"/>.</summary>
    public Scalar InTonnes => InUnit(UnitOfMass.Tonne);

    /// <summary>Retrieves the magnitude of the <see cref="Mass"/>, expressed in unit <see cref="UnitOfMass.Ounce"/>.</summary>
    public Scalar InOunces => InUnit(UnitOfMass.Ounce);
    /// <summary>Retrieves the magnitude of the <see cref="Mass"/>, expressed in unit <see cref="UnitOfMass.Pound"/>.</summary>
    public Scalar InPounds => InUnit(UnitOfMass.Pound);

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
    public Mass Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public Mass Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public Mass Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public Mass Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Mass other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Mass"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [kg]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Mass"/>, expressed in unit <paramref name="unitOfMass"/>.</summary>
    /// <param name="unitOfMass">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfMass unitOfMass) => InUnit(Magnitude, unitOfMass);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Mass"/>, expressed in unit <paramref name="unitOfMass"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="Mass"/>.</param>
    /// <param name="unitOfMass">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfMass unitOfMass) => new(magnitude / unitOfMass.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Mass"/>.</summary>
    public Mass Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Mass"/> with negated magnitude.</summary>
    public Mass Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Mass"/>.</param>
    public static Mass operator +(Mass x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Mass"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Mass"/>.</param>
    public static Mass operator -(Mass x) => x.Negate();

    /// <summary>Adds this instance and the <see cref="Mass"/> <paramref name="term"/>, producing another <see cref="Mass"/>.</summary>
    /// <param name="term">This <see cref="Mass"/> is added to this instance.</param>
    public Mass Add(Mass term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="Mass"/> <paramref name="term"/> from this instance, producing another <see cref="Mass"/>.</summary>
    /// <param name="term">This <see cref="Mass"/> is subtracted from this instance.</param>
    public Mass Subtract(Mass term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="Mass"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="Mass"/>.</summary>
    /// <param name="x">This <see cref="Mass"/> is added to the <see cref="Mass"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="Mass"/> is added to the <see cref="Mass"/> <paramref name="x"/>.</param>
    public static Mass operator +(Mass x, Mass y) => x.Add(y);
    /// <summary>Subtract the <see cref="Mass"/> <paramref name="y"/> from the <see cref="Mass"/> <paramref name="x"/>, producing another <see cref="Mass"/>.</summary>
    /// <param name="x">The <see cref="Mass"/> <paramref name="y"/> is subtracted from this <see cref="Mass"/>.</param>
    /// <param name="y">This <see cref="Mass"/> is subtracted from the <see cref="Mass"/> <paramref name="x"/>.</param>
    public static Mass operator -(Mass x, Mass y) => x.Subtract(y);

    /// <summary>Divides this <see cref="Mass"/> by the <see cref="Mass"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Mass"/> is divided by this <see cref="Mass"/>.</param>
    public Scalar Divide(Mass divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="Mass"/> <paramref name="x"/> by the <see cref="Mass"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="Mass"/> is divided by the <see cref="Mass"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Mass"/> <paramref name="x"/> is divided by this <see cref="Mass"/>.</param>
    public static Scalar operator /(Mass x, Mass y) => x.Divide(y)
;

    /// <summary>Multiplies the <see cref="Mass"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Mass"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Mass"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Mass"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Mass"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Mass"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Mass x, Unhandled y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Mass"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Mass"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Mass x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Mass"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Mass Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Mass"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Mass"/> is scaled.</param>
    public Mass Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Mass"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Mass"/> is divided.</param>
    public Mass Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Mass"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Mass operator %(Mass x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Mass"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Mass"/> <paramref name="x"/>.</param>
    public static Mass operator *(Mass x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Mass"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Mass"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Mass"/>, which is scaled by <paramref name="x"/>.</param>
    public static Mass operator *(double x, Mass y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Mass"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Mass"/> <paramref name="x"/>.</param>
    public static Mass operator /(Mass x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="Mass"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Mass Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Mass"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Mass"/> is scaled.</param>
    public Mass Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Mass"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Mass"/> is divided.</param>
    public Mass Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Mass"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
    public static Mass operator %(Mass x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Mass"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Mass"/> <paramref name="x"/>.</param>
    public static Mass operator *(Mass x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Mass"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Mass"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Mass"/>, which is scaled by <paramref name="x"/>.</param>
    public static Mass operator *(Scalar x, Mass y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Mass"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Mass"/> <paramref name="x"/>.</param>
    public static Mass operator /(Mass x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="Mass"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Mass"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Mass"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Mass"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Mass"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Mass"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Mass.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Mass x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Mass"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Mass"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Mass"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Mass.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Mass x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Mass x, Mass y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Mass x, Mass y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Mass x, Mass y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Mass x, Mass y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="Mass"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Mass x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Mass x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Mass"/> of magnitude <paramref name="x"/>.</summary>
    public static Mass FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Mass"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Mass(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Mass"/> of equivalent magnitude.</summary>
    public static Mass FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Mass"/> of equivalent magnitude.</summary>
    public static explicit operator Mass(Scalar x) => FromScalar(x);
}