namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

/// <summary>A measure of the scalar quantity <see cref="Absement"/>, describing sustained displacement - <see cref="Distance"/> for some <see cref="Time"/>.
/// This is the magnitude of the vector quantity <see cref="Absement3"/>, and is expressed in <see cref="UnitOfAbsement"/>, with the SI unit being [m∙s].
/// <para>
/// New instances of <see cref="Absement"/> can be constructed using pre-defined properties, prefixed with 'One', having magnitude 1 expressed
/// in the desired <see cref="UnitOfAbsement"/>. Instances can also be produced by combining other quantities, either through mathematical operators
/// or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Absement"/> a = 3 * <see cref="Absement.OneMetreSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Absement"/> d = <see cref="Absement.From(Distance, Time)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the <see cref="Absement"/> can be retrieved in the desired <see cref="UnitOfAbsement"/> using pre-defined properties,
/// such as <see cref="MetreSeconds"/>.
/// </para>
/// </summary>
public readonly partial record struct Absement :
    IComparable<Absement>,
    IScalarQuantity,
    IScalableScalarQuantity<Absement>,
    IMultiplicableScalarQuantity<Absement, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Absement, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity,
    IVector3MultiplicableScalarQuantity<Absement3, Vector3>
{
    /// <summary>The zero-valued <see cref="Absement"/>.</summary>
    public static Absement Zero { get; } = new(0);

    /// <summary>The <see cref="Absement"/> with magnitude 1, when expressed in unit <see cref="UnitOfAbsement.MetreSecond"/>.</summary>
    public static Absement OneMetreSecond { get; } = new(1, UnitOfAbsement.MetreSecond);

    /// <summary>The magnitude of the <see cref="Absement"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAbsement)"/> or a pre-defined property
    /// - such as <see cref="MetreSeconds"/>.</remarks>
    public double Magnitude { get; init; }

    /// <summary>Constructs a new <see cref="Absement"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAbsement"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Absement"/>, expressed in <paramref name="unitOfAbsement"/>.</param>
    /// <param name="unitOfAbsement">The <see cref="UnitOfAbsement"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring constructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Absement"/> a = 3 * <see cref="Absement.OneMetreSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Absement(Scalar magnitude, UnitOfAbsement unitOfAbsement) : this(magnitude.Magnitude, unitOfAbsement) { }
    /// <summary>Constructs a new <see cref="Absement"/> with magnitude <paramref name="magnitude"/>, expressed in <paramref name="unitOfAbsement"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Absement"/>, expressed in <paramref name="unitOfAbsement"/>.</param>
    /// <param name="unitOfAbsement">The <see cref="UnitOfAbsement"/> in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
    /// <remarks>Consider preferring cosntructing instances according to the following:
    /// <list type="bullet">
    /// <item>
    /// <code>
    /// <see cref="Absement"/> a = 3 * <see cref="Absement.OneMetreSecond"/>;
    /// </code>
    /// </item>
    /// </list>
    /// </remarks>
    public Absement(double magnitude, UnitOfAbsement unitOfAbsement) : this(magnitude * unitOfAbsement.Absement.Magnitude) { }
    /// <summary>Constructs a new <see cref="Absement"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Absement"/>.</param>
    /// <remarks>Consider preferring <see cref="Absement(Scalar, UnitOfAbsement)"/>.</remarks>
    public Absement(Scalar magnitude) : this(magnitude.Magnitude) { }
    /// <summary>Constructs a new <see cref="Absement"/> with magnitude <paramref name="magnitude"/>.</summary>
    /// <param name="magnitude">The magnitude of the <see cref="Absement"/>.</param>
    /// <remarks>Consider preferring <see cref="Absement(double, UnitOfAbsement)"/>.</remarks>
    public Absement(double magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Retrieves the magnitude of the <see cref="Absement"/>, expressed in <see cref="UnitOfAbsement.MetreSecond"/>.</summary>
    public Scalar MetreSeconds => InUnit(UnitOfAbsement.MetreSecond);

    /// <summary>Indicates whether the magnitude of the <see cref="Absement"/> is NaN.</summary>
    public bool IsNaN => double.IsNaN(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Absement"/> is zero.</summary>
    public bool IsZero => Magnitude == 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Absement"/> is positive.</summary>
    public bool IsPositive => Magnitude > 0;
    /// <summary>Indicates whether the magnitude of the <see cref="Absement"/> is negative.</summary>
    public bool IsNegative => double.IsNegative(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Absement"/> is finite.</summary>
    public bool IsFinite => double.IsFinite(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Absement"/> is infinite.</summary>
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    /// <summary>Indicates whether the magnitude of the <see cref="Absement"/> is infinite, and positive.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    /// <summary>Indicates whether the magnitude of the <see cref="Absement"/> is infinite, and negative.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    /// <summary>Computes the absolute of the <see cref="Absement"/>.</summary>
    public Absement Absolute() => new(Math.Abs(Magnitude));
    /// <summary>Computes the floor of the <see cref="Absement"/>.</summary>
    public Absement Floor() => new(Math.Floor(Magnitude));
    /// <summary>Computes the ceiling of the <see cref="Absement"/>.</summary>
    public Absement Ceiling() => new(Math.Ceiling(Magnitude));
    /// <summary>Rounds the <see cref="Absement"/> to the nearest integer value.</summary>
    public Absement Round() => new(Math.Round(Magnitude));

    /// <inheritdoc/>
    public int CompareTo(Absement other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a formatted string from the magnitude of the <see cref="Absement"/> in the default unit
    /// <see cref="UnitOfAbsement.MetreSecond"/>, followed by the symbol [m∙s].</summary>
    public override string ToString() => $"{MetreSeconds} [m∙s]";

    /// <summary>Produces a <see cref="Scalar"/> with magnitude equal to that of the <see cref="Absement"/>,
    /// expressed in <paramref name="unitOfAbsement"/>.</summary>
    /// <param name="unitOfAbsement">The <see cref="UnitOfAbsement"/> in which the magnitude is expressed.</param>
    public Scalar InUnit(UnitOfAbsement unitOfAbsement) => InUnit(this, unitOfAbsement);
    /// <summary>Produces a <see cref="Scalar"/> from the magnitude of a <see cref="Absement"/>,
    /// expressed in <paramref name="unitOfAbsement"/>.</summary>
    /// <param name="absement">The <see cref="Absement"/> to be expressed in <paramref name="unitOfAbsement"/>.</param>
    /// <param name="unitOfAbsement">The <see cref="UnitOfAbsement"/> in which the magnitude is expressed.</param>
    private static Scalar InUnit(Absement absement, UnitOfAbsement unitOfAbsement) => new(absement.Magnitude / unitOfAbsement.Absement.Magnitude);

    /// <summary>Unary plus, resulting in the unmodified <see cref="Absement"/>.</summary>
    public Absement Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Absement"/> with negated magnitude.</summary>
    public Absement Negate() => new(-Magnitude);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="x"/>.</summary>
    /// <param name="x">Unary plus is applied to this <see cref="Absement"/>.</param>
    public static Absement operator +(Absement x) => x.Plus();
    /// <summary>Negation, resulting in a <see cref="Absement"/> with negated magnitude from that of <paramref name="x"/>.</summary>
    /// <param name="x">Negation is applied to this <see cref="Absement"/>.</param>
    public static Absement operator -(Absement x) => x.Negate();

    /// <summary>Multiplicates the <see cref="Absement"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Absement"/> is multiplied.</param>
    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    /// <summary>Divides the <see cref="Absement"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Absement"/> is divided.</param>
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Absement"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Absement"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Absement"/> <paramref name="x"/> is multiplied.</param>
    public static Unhandled operator *(Absement x, Unhandled y) => x.Multiply(y);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="y"/> by the <see cref="Absement"/> <paramref name="x"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Unhandled"/> quantity by which the <see cref="Absement"/> <paramref name="y"/> is multiplied.</param>
    /// <param name="y">The <see cref="Absement"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="x"/>.</param>
    public static Unhandled operator *(Unhandled x, Absement y) => y.Multiply(x);
    /// <summary>Division of the <see cref="Absement"/> <paramref name="x"/> by the <see cref="Unhandled"/> quantity <paramref name="y"/> -
    /// resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Absement"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> quantity by which the <see cref="Absement"/> <paramref name="x"/> is divided.</param>
    public static Unhandled operator /(Absement x, Unhandled y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="Absement"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Absement Remainder(double divisor) => new(Magnitude % divisor);
    /// <summary>Scales the <see cref="Absement"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Absement"/> is scaled.</param>
    public Absement Multiply(double factor) => new(Magnitude * factor);
    /// <summary>Scales the <see cref="Absement"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Absement"/> is divided.</param>
    public Absement Divide(double divisor) => new(Magnitude / divisor);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Absement"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Absement"/> <paramref name="x"/> by this value.</param>
    public static Absement operator %(Absement x, double y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Absement"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Absement"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Absement"/> <paramref name="x"/>.</param>
    public static Absement operator *(Absement x, double y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Absement"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Absement"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Absement"/>, which is scaled by <paramref name="x"/>.</param>
    public static Absement operator *(double x, Absement y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Absement"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Absement"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Absement"/> <paramref name="x"/>.</param>
    public static Absement operator /(Absement x, double y) => x.Divide(y);

    /// <summary>Computes the remainder from division of the <see cref="Absement"/> by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Absement Remainder(Scalar divisor) => Remainder(divisor.Magnitude);
    /// <summary>Scales the <see cref="Absement"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Absement"/> is scaled.</param>
    public Absement Multiply(Scalar factor) => Multiply(factor.Magnitude);
    /// <summary>Scales the <see cref="Absement"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Absement"/> is divided.</param>
    public Absement Divide(Scalar divisor) => Divide(divisor.Magnitude);
    /// <summary>Computes the remainder from division of <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Absement"/>, which is divided by <paramref name="y"/> to produce a remainder.</param>
    /// <param name="y">The remainder is produced from division of the <see cref="Absement"/> <paramref name="x"/> by this value.</param>
    public static Absement operator %(Absement x, Scalar y) => x.Remainder(y);
    /// <summary>Scales the <see cref="Absement"/> <paramref name="x"/> by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Absement"/>, which is scaled by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to scale the <see cref="Absement"/> <paramref name="x"/>.</param>
    public static Absement operator *(Absement x, Scalar y) => x.Multiply(y);
    /// <summary>Scales the <see cref="Absement"/> <paramref name="y"/> by <paramref name="x"/>.</summary>
    /// <param name="x">This value is used to scale the <see cref="Absement"/> <paramref name="y"/>.</param>
    /// <param name="y">The <see cref="Absement"/>, which is scaled by <paramref name="x"/>.</param>
    public static Absement operator *(Scalar x, Absement y) => y.Multiply(x);
    /// <summary>Scales the <see cref="Absement"/> <paramref name="x"/> through division by <paramref name="y"/>.</summary>
    /// <param name="x">The <see cref="Absement"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">This value is used to divide the <see cref="Absement"/> <paramref name="x"/>.</param>
    public static Absement operator /(Absement x, Scalar y) => x.Divide(y);

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TProductScalarQuantity Multiply<TProductScalarQuantity, TFactorScalarQuantity>(TFactorScalarQuantity factor, Func<double, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorScalarQuantity : IScalarQuantity
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }
        else
        {
            return factory(Magnitude * factor.Magnitude);
        }
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TQuotientScalarQuantity Divide<TQuotientScalarQuantity, TDivisorScalarQuantity>(TDivisorScalarQuantity divisor, Func<double, TQuotientScalarQuantity> factory)
        where TQuotientScalarQuantity : IScalarQuantity
        where TDivisorScalarQuantity : IScalarQuantity
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }
        else
        {
            return factory(Magnitude / divisor.Magnitude);
        }
    }

    /// <summary>Multiplication of the <see cref="Absement"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Absement"/>, which is multiplied by <paramref name="y"/>.</param>
    /// <param name="y">This quantity is multiplied by the <see cref="Absement"/> <paramref name="x"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductScalarQuantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, TProductScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Absement x, IScalarQuantity y) => x.Multiply<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));
    /// <summary>Division of the <see cref="Absement"/> <paramref name="x"/> by the quantity <paramref name="y"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="x">The <see cref="Absement"/>, which is divided by <paramref name="y"/>.</param>
    /// <param name="y">The<see cref="Absement"/> <paramref name="x"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientScalarQuantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, TQuotientScalarQuantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Absement x, IScalarQuantity y) => x.Divide<Unhandled, IScalarQuantity>(y, (m) => new Unhandled(m));

    /// <summary>Multiplicates the <see cref="Absement"/> with the <see cref="Vector3"/> <paramref name="factor"/> to produce
    /// a <see cref="Absement3"/>.</summary>
    /// <param name="factor">This <see cref="Vector3"/> is multiplied by the <see cref="Absement"/>.</param>
    public Absement3 Multiply(Vector3 factor) => new(factor * Magnitude);
    /// <summary>Multiplicates the <see cref="Absement"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="Absement3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Absement"/>.</param>
    public Absement3 Multiply((double x, double y, double z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplicates the <see cref="Absement"/> with the values of <paramref name="components"/> to produce
    /// a <see cref="Absement3"/>.</summary>
    /// <param name="components">These values are multiplied by the <see cref="Absement"/>.</param>
    public Absement3 Multiply((Scalar x, Scalar y, Scalar z) components) => Multiply(new Vector3(components));
    /// <summary>Multiplication of the <see cref="Absement"/> <paramref name="a"/> with the <see cref="Vector3"/> <paramref name="b"/>
    /// to produce a <see cref="Absement3"/>.</summary>
    /// <param name="a">This <see cref="Absement"/> is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Vector3"/> is multiplied by the <see cref="Absement"/> <paramref name="a"/>.</param>
    public static Absement3 operator *(Absement a, Vector3 b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Absement"/> <parmref name="b"/> with the <see cref="Vector3"/> <paramref name="a"/>
    /// to produce a <see cref="Absement3"/>.</summary>
    /// <param name="a">This <see cref="Vector3"/> is multiplied by the <see cref="Absement"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Absement"/> is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Absement3 operator *(Vector3 a, Absement b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="Absement"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="Absement3"/>.</summary>
    /// <param name="a">This <see cref="Absement"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Absement"/> <paramref name="a"/>.</param>
    public static Absement3 operator *(Absement a, (double x, double y, double z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Absement"/> <parmref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="Absement3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Absement"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Absement"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Absement3 operator *((double x, double y, double z) a, Absement b) => b.Multiply(a);
    /// <summary>Multiplication of the <see cref="Absement"/> <paramref name="a"/> with the values of <paramref name="b"/>
    /// to produce a <see cref="Absement3"/>.</summary>
    /// <param name="a">This <see cref="Absement"/> is multiplied by the values of <paramref name="b"/>.</param>
    /// <param name="b">These values are multiplied by the <see cref="Absement"/> <paramref name="a"/>.</param>
    public static Absement3 operator *(Absement a, (Scalar x, Scalar y, Scalar z) b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Absement"/> <parmref name="b"/> with the values of <paramref name="a"/>
    /// to produce a <see cref="Absement3"/>.</summary>
    /// <param name="a">These values are multiplied by the <see cref="Absement"/> <paramref name="b"/>.</param>
    /// <param name="b">This <see cref="Absement"/> is multiplied by the values of <paramref name="a"/>.</param>
    public static Absement3 operator *((Scalar x, Scalar y, Scalar z) a, Absement b) => b.Multiply(a);

    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Absement"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than that of this <see cref="Absement"/>.</param>
    public static bool operator <(Absement x, Absement y) => x.Magnitude < y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Absement"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than that of this <see cref="Absement"/>.</param>
    public static bool operator >(Absement x, Absement y) => x.Magnitude > y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Absement"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is less than or equal to that of this <see cref="Absement"/>.</param>
    public static bool operator <=(Absement x, Absement y) => x.Magnitude <= y.Magnitude;
    /// <summary>Determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the magnitude of this <see cref="Absement"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the magnitude of <paramref name="x"/> is greater than or equal to that of this <see cref="Absement"/>.</param>
    public static bool operator >=(Absement x, Absement y) => x.Magnitude >= y.Magnitude;

    /// <summary>Converts the <see cref="Absement"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public double ToDouble() => Magnitude;
    /// <summary>Converts <paramref name="x"/> to a <see cref="double"/> with value <see cref="Magnitude"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator double(Absement x) => x.ToDouble();

    /// <summary>Converts the <see cref="Absement"/> to the <see cref="Scalar"/> of equivalent magnitude, when
    /// expressed in SI units.</summary>
    public Scalar ToScalar() => new(Magnitude);
    /// <summary>Converts <paramref name="x"/> to the <see cref="Scalar"/> of equivalent magnitude, when expressed in SI units.</summary>
    public static explicit operator Scalar(Absement x) => x.ToScalar();

    /// <summary>Constructs the <see cref="Absement"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static Absement FromDouble(double x) => new(x);
    /// <summary>Constructs the <see cref="Absement"/> of magnitude <paramref name="x"/>, when expressed
    /// in SI units.</summary>
    public static explicit operator Absement(double x) => FromDouble(x);

    /// <summary>Constructs the <see cref="Absement"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static Absement FromScalar(Scalar x) => new(x);
    /// <summary>Constructs the <see cref="Absement"/> of magnitude <paramref name="x"/>, when expressed in SI units.</summary>
    public static explicit operator Absement(Scalar x) => FromScalar(x);
}
