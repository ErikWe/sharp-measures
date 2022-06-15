namespace SharpMeasures;

using SharpMeasures.Maths;
using SharpMeasures.ScalarAbstractions;
using SharpMeasures.Vector3Abstractions;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>A pure scalar.</summary>
public readonly partial record struct Scalar :
    IComparable<Scalar>,
    IScalarQuantity<Scalar>,
    IReciprocalScalarQuantity<Scalar>,
    ISquareScalarQuantity<Scalar>,
    ICubeScalarQuantity<Scalar>,
    ISquareRootScalarQuantity<Scalar>,
    ICubeRootScalarQuantity<Scalar>,
    IAddendScalarQuantity<Scalar>,
    IMinuendScalarQuantity<Scalar>,
    ISubtrahendScalarQuantity<Scalar>,
    IFactorScalarQuantity<Scalar, Scalar, Scalar>,
    IDividendScalarQuantity<Scalar, Scalar, Scalar>,
    IDivisorScalarQuantity<Scalar, Scalar, Scalar>,
    IFactor3ScalarQuantity<Vector3, Vector3>
{
    /// <summary>The <see cref="Scalar"/> representing { 0 }.</summary>
    public static Scalar Zero { get; } = 0;
    /// <summary>The <see cref="Scalar"/> representing { 1 }.</summary>
    public static Scalar One { get; } = 1;

    /// <inheritdoc/>
    static Scalar IScalarQuantity<Scalar>.WithMagnitude(Scalar magnitude) => magnitude;

    /// <summary>The value represented by <see langword="this"/>.</summary>
    public double Value { get; }

    /// <inheritdoc/>
    Scalar IScalarQuantity.Magnitude => this;

    /// <summary>Constructs a new <see cref="Scalar"/> representing { <paramref name="value"/> }.</summary>
    /// <param name="value">The value represented by the constructed <see cref="Scalar"/>.</param>
    public Scalar(double value)
    {
        Value = value;
    }

    /// <summary>Indicates whether <see langword="this"/> represents { <see cref="double.NaN"/> }.</summary>
    public bool IsNaN => double.IsNaN(Value);
    /// <summary>Indicates whether <see langword="this"/> represents { 0 }.</summary>
    public bool IsZero => Value is 0;
    /// <summary>Indicates whether <see langword="this"/> represents a value greater than { 0 }.</summary>
    public bool IsPositive => Value > 0 || IsPositiveInfinity;
    /// <summary>Indicates whether <see langword="this"/> represents a value smaller than { 0 }.</summary>
    public bool IsNegative => Value < 0 || IsNegativeInfinity;
    /// <summary>Indicates whether <see langword="this"/> represents a finite value.</summary>
    public bool IsFinite => double.IsFinite(Value);
    /// <summary>Indicates whether <see langword="this"/> represents an infinite value.</summary>
    public bool IsInfinite => double.IsInfinity(Value);
    /// <summary>Indicates whether <see langword="this"/> represents { <see cref="double.PositiveInfinity"/> }.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Value);
    /// <summary>Indicates whether <see langword="this"/> represents { <see cref="double.NegativeInfinity"/> }.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Value);

    /// <summary>Computes the absolute value of <see langword="this"/>.</summary>
    public Scalar Absolute() => ScalarMaths.Absolute(this);
    /// <summary>Computes the floor of <see langword="this"/>.</summary>
    public Scalar Floor() => Math.Floor(Value);
    /// <summary>Computes the ceiling of <see langword="this"/>.</summary>
    public Scalar Ceiling() => Math.Ceiling(Value);
    /// <summary>Rounds <see langword="this"/> to the nearest integer value.</summary>
    /// <remarks>Midpoint values are rounded to the nearest even integer value, consistent with <see cref="Math.Round(double)"/>.</remarks>
    public Scalar Round() => Math.Round(Value);

    /// <summary>Computes { <see langword="this"/> ^ <paramref name="exponent"/> }.</summary>
    /// <param name="exponent">The exponent of { <see langword="this"/> ^ <paramref name="exponent"/> }.</param>
    public Scalar Power(Scalar exponent) => Math.Pow(Value, exponent.Value);
    /// <inheritdoc/>
    public Scalar Reciprocal() => ScalarMaths.Reciprocal(this);
    /// <inheritdoc/>
    public Scalar Square() => ScalarMaths.Square(this);
    /// <inheritdoc/>
    public Scalar Cube() => ScalarMaths.Cube(this);
    /// <inheritdoc/>
    public Scalar SquareRoot() => ScalarMaths.SquareRoot(this);
    /// <inheritdoc/>
    public Scalar CubeRoot() => ScalarMaths.CubeRoot(this);

    /// <summary>Compares <see langword="this"/> to <paramref name="other"/>. 1 is returned if <see langword="this"/> is greater than <paramref name="other"/>,
    /// -1 is returned if <paramref name="other"/> is greater than <see langword="this"/>, and 0 is returned if <see langword="this"/> and <paramref name="other"/>
    /// are equal.
    /// <para>A representation of { <see cref="double.NaN"/> }, { <see cref="double.NegativeInfinity"/> }, or { <see cref="double.PositiveInfinity"/> } is
    /// considered equal to another representation of the same value. { <see cref="double.NaN"/> } is treated as the smallest value.</para></summary>
    /// <param name="other"><see langword="this"/> is compared to this value.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.CompareTo(double)"/>.</remarks>
    public int CompareTo(Scalar other) => Value.CompareTo(other.Value);
    /// <summary>Produces a description of <see langword="this"/> containing the type and the represented value.</summary>
    public override string ToString() => $"{nameof(Scalar)}: {Value}";

    /// <inheritdoc/>
    public Scalar Plus() => this;
    /// <inheritdoc/>s
    public Scalar Negate() => -this;

    /// <inheritdoc/>
    public Scalar Add(Scalar addend) => this + addend;
    /// <inheritdoc/>
    public Scalar Subtract(Scalar subtrahend) => this - subtrahend;
    /// <inheritdoc/>
    Scalar ISubtrahendScalarQuantity<Scalar, Scalar>.SubtractFrom(Scalar minuend) => minuend - this;

    /// <summary>Computes { <see langword="this"/> % <paramref name="divisor"/> }.</summary>
    /// <param name="divisor">The divisor of { <see langword="this"/> % <paramref name="divisor"/> }.</param>
    public Scalar Remainder(Scalar divisor) => this % divisor;
    /// <inheritdoc/>
    public Scalar Multiply(Scalar factor) => this * factor;
    /// <inheritdoc/>
    public Scalar Divide(Scalar divisor) => this / divisor;
    /// <inheritdoc/>
    Scalar IDivisorScalarQuantity<Scalar, Scalar>.DivideInto(Scalar dividend) => dividend / this;

    /// <inheritdoc cref="IScalarQuantity.Multiply{TFactor}(TFactor)"/>
    public TFactor Multiply<TFactor>(TFactor factor) where TFactor : IScalarQuantity<TFactor> => TFactor.WithMagnitude(this * factor.Magnitude);

    /// <inheritdoc/>
    Unhandled IScalarQuantity.Multiply<TFactor>(TFactor factor) => new(this * factor.Magnitude);

    /// <inheritdoc/>
    TProduct IScalarQuantity.Multiply<TProduct, TFactor>(TFactor factor) => TProduct.WithMagnitude(this * factor.Magnitude);

    /// <inheritdoc/>
    TQuotient IScalarQuantity.DivideInto<TQuotient, TDividend>(TDividend dividend) => TQuotient.WithMagnitude(dividend.Magnitude / this);

    /// <inheritdoc cref="Divide(Scalar)"/>
    /// <typeparam name="TQuotient">The scalar quantity that represents the result of { <see langword="this"/> / <paramref name="divisor"/> }.</typeparam>
    /// <typeparam name="TDivisor">The scalar quantity that represents the divisor of { <see langword="this"/> / <paramref name="divisor"/> }.</typeparam>
    public TQuotient Divide<TQuotient, TDivisor>(TDivisor divisor)
        where TQuotient : IScalarQuantity<TQuotient>
        where TDivisor : IScalarQuantity
        => TQuotient.WithMagnitude(this / divisor.Magnitude);

    /// <inheritdoc/>
    Unhandled IScalarQuantity.Divide<TDivisor>(TDivisor divisor) => new(this / divisor.Magnitude);
    /// <inheritdoc/>
    Unhandled IScalarQuantity.DivideInto<TDividend>(TDividend dividend) => new(dividend.Magnitude / this);

    /// <inheritdoc/>
    public Vector3 Multiply(Vector3 factor) => this * factor;

    /// <inheritdoc/>
    public static Scalar operator +(Scalar x) => x;
    /// <inheritdoc/>
    public static Scalar operator -(Scalar x) => -x.Value;

    /// <inheritdoc/>
    public static Scalar operator +(Scalar x, Scalar y) => x.Value + y.Value;
    /// <inheritdoc/>
    public static Scalar operator -(Scalar x, Scalar y) => x.Value - y.Value;

    /// <summary>Computes { <paramref name="x"/> % <paramref name="y"/> }.</summary>
    /// <param name="x">The dividend of { <paramref name="x"/> % <paramref name="y"/> }.</param>
    /// <param name="y">The divisor of { <paramref name="x"/> % <paramref name="y"/> }.</param>
    public static Scalar operator %(Scalar x, Scalar y) => x.Value % y.Value;
    /// <inheritdoc/>
    public static Scalar operator *(Scalar x, Scalar y) => x.Value * y.Value;
    /// <inheritdoc/>
    public static Scalar operator /(Scalar x, Scalar y) => x.Value / y.Value;

    /// <summary>Determines the truthfulness of { <paramref name="x"/> &lt; <paramref name="y"/> }.</summary>
    /// <param name="x">Assumed lesser than <paramref name="y"/>.</param>
    /// <param name="y">Assumed greater than <paramref name="x"/>.</param>
    public static bool operator <(Scalar x, Scalar y) => x.Value < y.Value;
    /// <summary>Determines the truthfulness of { <paramref name="x"/> &gt; <paramref name="y"/> }.</summary>
    /// <param name="x">Assumed greater than <paramref name="y"/>.</param>
    /// <param name="y">Assumed lesser than <paramref name="x"/>.</param>
    public static bool operator >(Scalar x, Scalar y) => x.Value > y.Value;
    /// <summary>Determines the truthfulness of { <paramref name="x"/> ≤ <paramref name="y"/> }.</summary>
    /// <param name="x">Assumed lesser than or equal to <paramref name="y"/>.</param>
    /// <param name="y">Assumed greater than or equal to <paramref name="x"/>.</param>
    public static bool operator <=(Scalar x, Scalar y) => x.Value <= y.Value;
    /// <summary>Determines the truthfulness of { <paramref name="x"/> ≥ <paramref name="y"/> }.</summary>
    /// <param name="x">Assumed greater than or equal to <paramref name="y"/>.</param>
    /// <param name="y">Assumed lesser than or equal to <paramref name="x"/>.</param>
    public static bool operator >=(Scalar x, Scalar y) => x.Value >= y.Value;

    /// <summary>Produces the <see cref="double"/> equivalent to <paramref name="x"/>.</summary>
    /// <param name="x">The <see cref="double"/> equivalent to this value is produced.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "Available as 'Value'")]
    public static implicit operator double(Scalar x) => x.Value;

    /// <summary>Produces the <see cref="Scalar"/> representing <paramref name="x"/>.</summary>
    /// <param name="x">The <see cref="Scalar"/> equivalent to this value is produced.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "Available as constructor")]
    public static implicit operator Scalar(double x) => new(x);

    /// <summary>Describes mathematical operations that result in a pure <see cref="Scalar"/>.</summary>
    private static IScalarResultingMaths<Scalar> ScalarMaths { get; } = MathFactory.ScalarResult();
}
