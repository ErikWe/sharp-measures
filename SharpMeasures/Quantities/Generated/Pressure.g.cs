namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Pressure"/>, describing <see cref="Force"/> distributed over some <see cref="Area"/>.
/// The quantity is expressed in <see cref="UnitOfPressure"/>, with the SI unit being [Pa].
/// <para>
/// New instances of <see cref="Pressure"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfPressure"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Pressure"/> a = 3 * <see cref="Pressure.OnePascal"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Pressure"/> d = <see cref="Pressure.From(Force, Area)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the measure can be retrieved using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfPressure"/>.
/// </para>
/// </summary>
public readonly partial record struct Pressure :
    IComparable<Pressure>,
    IScalarQuantity,
    IScalableScalarQuantity<Pressure>,
    IMultiplicableScalarQuantity<Pressure, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Pressure, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    /// <summary>The zero-valued <see cref="Pressure"/>.</summary>
    public static Pressure Zero { get; } = new(0);

    /// <summary>The <see cref="Pressure"/> with magnitude 1, when expressed in unit <see cref="UnitOfPressure.Pascal"/>.</summary>
    public static Pressure OnePascal { get; } = new(1, UnitOfPressure.Pascal);
    /// <summary>The <see cref="Pressure"/> with magnitude 1, when expressed in unit <see cref="UnitOfPressure.Kilopascal"/>.</summary>
    public static Pressure OneKilopascal { get; } = new(1, UnitOfPressure.Kilopascal);
    /// <summary>The <see cref="Pressure"/> with magnitude 1, when expressed in unit <see cref="UnitOfPressure.Bar"/>.</summary>
    public static Pressure OneBar { get; } = new(1, UnitOfPressure.Bar);
    /// <summary>The <see cref="Pressure"/> with magnitude 1, when expressed in unit <see cref="UnitOfPressure.PoundForcePerSquareInch"/>.</summary>
    public static Pressure OnePoundForcePerSquareInch { get; } = new(1, UnitOfPressure.PoundForcePerSquareInch);

    /// <summary>The magnitude of the <see cref="Pressure"/> measure, in SI units.</summary>
    /// <remarks>When the magnitude of the measure is desired, prefer retrieving this through methods prefixed with 'In', such as <see cref="Pressure.InPascals"/>.
    /// <para>This value should only be used (to maximize efficiency) when implementing mathematical operations with other quantities.</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Pressure"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfPressure"/> <paramref name="unitOfPressure"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Pressure"/>, in <see cref="UnitOfPressure"/> <paramref name="unitOfPressure"/>.</param>
    /// <param name="unitOfPressure">The <see cref="UnitOfPressure"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Pressure"/> a = 3 * <see cref="Pressure.OnePascal"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Pressure(Scalar magnitude, UnitOfPressure unitOfPressure) : this(magnitude.Magnitude, unitOfPressure) { }
    /// <summary>Constructs a new <see cref="Pressure"/>, with magnitude <paramref name="magnitude"/> in <see cref="UnitOfPressure"/> <paramref name="unitOfPressure"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Pressure"/>, in <see cref="UnitOfPressure"/> <paramref name="unitOfPressure"/>.</param>
    /// <param name="unitOfPressure">The <see cref="UnitOfPressure"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Pressure"/> a = 3 * <see cref="Pressure.OnePascal"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Pressure(double magnitude, UnitOfPressure unitOfPressure) : this(magnitude * unitOfPressure.Factor) { }
    /// <summary>Constructs a new <see cref="Pressure"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Pressure"/>.</param>
    /// <remarks>Consider preffering a constructor that requires a <see cref="UnitOfPressure"/> to be specified.</remarks>
    public Pressure(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Pressure"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Pressure"/>.</param>
    /// <remarks>Consider preferring a constructor that requires a <see cref="UnitOfPressure"/> to be specified.</remarks>
    public Pressure(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Pressure"/>, expressed in unit <see cref="UnitOfPressure.Pascal"/>.</summary>
    public Scalar InPascals => InUnit(UnitOfPressure.Pascal);
    /// <summary>Retrieves the magnitude of the <see cref="Pressure"/>, expressed in unit <see cref="UnitOfPressure.Kilopascal"/>.</summary>
    public Scalar InKilopascals => InUnit(UnitOfPressure.Kilopascal);

    /// <summary>Retrieves the magnitude of the <see cref="Pressure"/>, expressed in unit <see cref="UnitOfPressure.Bar"/>.</summary>
    public Scalar InBars => InUnit(UnitOfPressure.Bar);
    /// <summary>Retrieves the magnitude of the <see cref="Pressure"/>, expressed in unit <see cref="UnitOfPressure.PoundForcePerSquareInch"/>.</summary>
    public Scalar InPoundsForcePerSquareInch => InUnit(UnitOfPressure.PoundForcePerSquareInch);

    /// <summary>Indicates whether the magnitude of the <see cref="Pressure"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Pressure"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Pressure"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Pressure"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Pressure"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Pressure"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Pressure"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Pressure"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Produces a <see cref="Pressure"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public Pressure Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Produces a <see cref="Pressure"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public Pressure Floor() => new(Math.Floor(Magnitude));
    /// <summary>Produces a <see cref="Pressure"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public Pressure Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Produces a <see cref="Pressure"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public Pressure Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Pressure other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Pressure"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [Pa]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Pressure"/>, expressed in <see cref="UnitOfPressure"/>
    /// <paramref name="unitOfPressure"/>.</summary>
    /// <param name="unitOfPressure">The <see cref="UnitOfPressure"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfPressure unitOfPressure) => InUnit(this, unitOfPressure);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Pressure"/>, expressed in <see cref="UnitOfPressure"/>
    /// <paramref name="unitOfPressure"/>.</summary>
    /// <param name="pressure">The <see cref="Pressure"/> to be expressed in <see cref="UnitOfPressure"/> <paramref name="unitOfPressure"/>.</param>
    /// <param name="unitOfPressure">The <see cref="UnitOfPressure"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Pressure pressure, UnitOfPressure unitOfPressure) => new(pressure.Magnitude / unitOfPressure.Factor);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Pressure"/>.</summary>
    public Pressure Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Pressure"/> with negated magnitude.</summary>
    public Pressure Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this instance of <see cref="Pressure"/>.</param>
    public static Pressure operator +(Pressure x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Pressure"/> with magnitude negated from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this instance of <see cref="Pressure"/>.</param>
    public static Pressure operator -(Pressure x) => x.Negate();

    /// <summary>Multiplies the <see cref="Pressure"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Pressure"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Pressure"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Pressure"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Pressure"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Pressure"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Pressure"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Pressure x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Pressure"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Pressure"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Pressure"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Pressure y) => y.Multiply(x);
    /// <summary>Divides the <see cref="Pressure"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Pressure"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Pressure"/> <paramref name="x"/> is divded.</param>
    public static Unhandled operator /(Pressure x, Unhandled y) => x.Divide(y);

    /// <summary>Produces a <see cref="Pressure"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Pressure Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Pressure"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Pressure"/> is scaled.</param>
    public Pressure Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Pressure"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Pressure"/> is divided.</param>
    public Pressure Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Produces a <see cref="Pressure"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Pressure"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of <see cref="Pressure"/> <paramref name="x"/> by this value.</param>
    public static Pressure operator %(Pressure x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Pressure"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Pressure"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Pressure"/> <paramref name="x"/>.</param>
    public static Pressure operator *(Pressure x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Pressure"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Pressure"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Pressure"/>, which is scaled by <paramref name="x"/>.</param>
    public static Pressure operator *(double x, Pressure y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Pressure"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Pressure"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Pressure"/> <paramref name="x"/>.</param>
    public static Pressure operator /(Pressure x, double y) => x.Divide(y);

    /// <summary>Produces a <see cref="Pressure"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Pressure Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Pressure"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Pressure"/> is scaled.</param>
    public Pressure Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Pressure"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Pressure"/> is divided.</param>
    public Pressure Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Produces a <see cref="Pressure"/>, with magnitude equal to the remainder from division of the magnitude of <paramref name="x"/>
    /// by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Pressure"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is retrieved from division of the <see cref="Pressure"/> <paramref name="x"/> by this value.</param>
    public static Pressure operator %(Pressure x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Pressure"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Pressure"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Pressure"/> <paramref name="x"/>.</param>
    public static Pressure operator *(Pressure x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Pressure"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Pressure"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Pressure"/>, which is scaled by <paramref name="x"/>.</param>
    public static Pressure operator *(Scalar x, Pressure y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Pressure"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Pressure"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Pressure"/> <paramref name="x"/>.</param>
    public static Pressure operator /(Pressure x, Scalar y) => x.Divide(y);

    /// <summary>Multiplies the <see cref="Pressure"/> by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the <see cref="Pressure"/> is multiplied.</param>
    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Pressure"/> by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the <see cref="Pressure"/> is divided.</param>
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Pressure"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Pressure"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Pressure"/> <paramref name="x"/>.</param>
    /// <remarks>To maximize performance, prefer <see cref="Pressure.Multiply{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator *(Pressure x, IScalarQuantity y) => x.Multiply(y);
    /// <summary>Divides the <see cref="Pressure"/> <paramref name="x"/> by the quantity <paramref name="y"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Pressure"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Pressure"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To maximize performance, prefer <see cref="Pressure.Divide{TScalarQuantity}(TScalarQuantity)"/> - where boxing is avoided.</remarks>
    public static Unhandled operator /(Pressure x, IScalarQuantity y) => x.Multiply(y);

    /// <summary>Determines whether <paramref name="x"/> is less than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <(Pressure x, Pressure y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >(Pressure x, Pressure y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is less than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator <=(Pressure x, Pressure y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Pressure x, Pressure y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Pressure"/> to a <see cref="double"/> with value <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts the <see cref="Pressure"/> to a <see cref="double"/> based on the magnitude of the <see cref="Pressure"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Pressure x) => x.ToDouble();

    /// <summary>Converts the <see cref="Pressure"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts the <see cref="Pressure"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public static explicit operator Scalar(Pressure x) => x.ToScalar();

    /// <summary>Converts <paramref name="x"/> to the <see cref="Pressure"/> of magnitude <paramref name="x"/>.</summary>
    public static Pressure FromDouble(double x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Pressure"/> of magnitude <paramref name="x"/>.</summary>
    public static explicit operator Pressure(double x) => FromDouble(x);

    /// <summary>Converts <paramref name="x"/> to the <see cref="Pressure"/> of equivalent magnitude.</summary>
    public static Pressure FromScalar(Scalar x) => new(x);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Pressure"/> of equivalent magnitude.</summary>
    public static explicit operator Pressure(Scalar x) => FromScalar(x);
}
