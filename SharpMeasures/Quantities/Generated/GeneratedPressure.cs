namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct Pressure :
    IComparable<Pressure>,
    IScalarQuantity<Pressure>,
    IAddableScalarQuantity<Pressure, Pressure>,
    ISubtractableScalarQuantity<Pressure, Pressure>,
    IDivisibleScalarQuantity<Scalar, Pressure>
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
    /// <para>This value should only be used when implementing mathematical operations with other quantities, to maximize effiency</para></remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Pressure"/>, with magnitude <paramref name="magnitude"/> in unit <paramref name="unitOfPressure"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Pressure"/>, in unit <paramref name="unitOfPressure"/>.</param>
    /// <param name="unitOfPressure">The unit in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Instances may also be constructed according to:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Pressure"/> a = 2.6 * <see cref="Pressure.OneKilopascal"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Pressure(double magnitude, UnitOfPressure unitOfPressure) : this(magnitude * unitOfPressure.Factor) { }
    /// <summary>Constructs a new <see cref="Pressure"/>, with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Pressure"/>.</param>
    /// <remarks>Prefer using a constructor that requires a <see cref="UnitOfPressure"/> to be specified.</remarks>
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
    public Pressure Absolute() => new(Math.Abs(Magnitude));
    /// <inheritdoc/>
    public Pressure Floor() => new(Math.Floor(Magnitude));
    /// <inheritdoc/>
    public Pressure Ceiling() => new(Math.Ceiling(Magnitude));
    /// <inheritdoc/>
    public Pressure Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Pressure other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Pressure"/>, and the SI base unit of the quantity.</summary>
    public override string ToString() => $"{Magnitude} [Pa]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Pressure"/>, expressed in unit <paramref name="unitOfPressure"/>.</summary>
    /// <param name="unitOfPressure">The unit in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfPressure unitOfPressure) => InUnit(Magnitude, unitOfPressure);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Pressure"/>, expressed in unit <paramref name="unitOfPressure"/>.</summary>
    /// <param name="magnitude">The magnitude of the instance of <see cref="Pressure"/>.</param>
    /// <param name="unitOfPressure">The unit in which the magnitude is expressed.</param>
    private static Scalar InUnit(double magnitude, UnitOfPressure unitOfPressure) => new(magnitude / unitOfPressure.Factor);

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

    /// <summary>Adds this instance and the <see cref="Pressure"/> <paramref name="term"/>, producing another <see cref="Pressure"/>.</summary>
    /// <param name="term">This <see cref="Pressure"/> is added to this instance.</param>
    public Pressure Add(Pressure term) => new(Magnitude + term.Magnitude);
    /// <summary>Subtracts the <see cref="Pressure"/> <paramref name="term"/> from this instance, producing another <see cref="Pressure"/>.</summary>
    /// <param name="term">This <see cref="Pressure"/> is subtracted from this instance.</param>
    public Pressure Subtract(Pressure term) => new(Magnitude - term.Magnitude);
    /// <summary>Adds the instances of <see cref="Pressure"/>, <paramref name="x"/> and <paramref name="y"/> - producing another <see cref="Pressure"/>.</summary>
    /// <param name="x">This <see cref="Pressure"/> is added to the <see cref="Pressure"/> <paramref name="y"/>.</param>
    /// <param name="y">This <see cref="Pressure"/> is added to the <see cref="Pressure"/> <paramref name="x"/>.</param>
    public static Pressure operator +(Pressure x, Pressure y) => x.Add(y);
    /// <summary>Subtract the <see cref="Pressure"/> <paramref name="y"/> from the <see cref="Pressure"/> <paramref name="x"/>, producing another <see cref="Pressure"/>.</summary>
    /// <param name="x">The <see cref="Pressure"/> <paramref name="y"/> is subtracted from this <see cref="Pressure"/>.</param>
    /// <param name="y">This <see cref="Pressure"/> is subtracted from the <see cref="Pressure"/> <paramref name="x"/>.</param>
    public static Pressure operator -(Pressure x, Pressure y) => x.Subtract(y);

    /// <summary>Divides this <see cref="Pressure"/> by the <see cref="Pressure"/> <paramref name="divisor"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Pressure"/> is divided by this <see cref="Pressure"/>.</param>
    public Scalar Divide(Pressure divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Divides the <see cref="Pressure"/> <paramref name="x"/> by the <see cref="Pressure"/> <paramref name="y"/> - producing a <see cref="Scalar"/>.</summary>
    /// <param name="x">This <see cref="Pressure"/> is divided by the <see cref="Pressure"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Pressure"/> <paramref name="x"/> is divided by this <see cref="Pressure"/>.</param>
    public static Scalar operator /(Pressure x, Pressure y) => x.Divide(y)
;

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
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
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
    /// <param name="y">The remainder is retrieved from division of <paramref name="x"/> by this value.</param>
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
    public static bool operator <=(Pressure x, Pressure y) => !(x > y);
    /// <summary>Determines whether <paramref name="x"/> is greater than or equal to <paramref name="y"/>.</summary>
    /// <param name="x"><paramref name="y"/> is compared against this value.</param>
    /// <param name="y"><paramref name="x"/> is compared against this value.</param>
    public static bool operator >=(Pressure x, Pressure y) => !(x < y);

    /// <summary>Converts <see langword="this"/> to a <see cref="double"/> based on <see cref="Magnitude"/>.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> based on the magnitude of the <see cref="Pressure"/> <paramref name="x"/>.</summary>
    public static implicit operator double(Pressure x) => x.ToDouble();

    /// <summary>Converts <see langword="this"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude.</summary>
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