namespace SharpMeasures;

using SharpMeasures.Maths;

using System;
using System.Globalization;

/// <summary>A pure scalar.</summary>
public readonly record struct Scalar : IScalarQuantity<Scalar>, IComparable<Scalar>, IFormattable
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
    /// <summary>Indicates whether <see langword="this"/> represents a value greater than or equal to { 0 }.</summary>
    public bool IsPositive => double.IsPositive(Value);
    /// <summary>Indicates whether <see langword="this"/> represents a value smaller than { 0 }.</summary>
    public bool IsNegative => double.IsNegative(Value);
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
    /// <remarks>Midpoint values are rounded to the nearest even integer value. The behaviour is consistent with <see cref="Math.Round(double)"/>.</remarks>
    public Scalar Round() => Math.Round(Value);
    /// <summary>Computes the sign of <see langword="this"/>. { 1 } is returned if <see langword="this"/> is positive, { -1 } is returned if
    /// <see langword="this"/> is negative, and { 0 } is returned if <see langword="this"/> is zero.</summary>
    public int Sign() => Math.Sign(Value);

    /// <summary>Computes { <see langword="this"/> ^ <paramref name="exponent"/> }.</summary>
    /// <param name="exponent">The exponent of { <see langword="this"/> ^ <paramref name="exponent"/> }.</param>
    public Scalar Power(Scalar exponent) => Math.Pow(Value, exponent.Value);
    /// <summary>Computes { <see langword="this"/> ² }.</summary>
    public Scalar Square() => ScalarMaths.Square(this);
    /// <summary>Computes { <see langword="this"/> ³ }.</summary>
    public Scalar Cube() => ScalarMaths.Cube(this);
    /// <summary>Computes { √ <see langword="this"/> }.</summary>
    public Scalar SquareRoot() => ScalarMaths.SquareRoot(this);
    /// <summary>Computes { ³√ <see langword="this"/> }.</summary>
    public Scalar CubeRoot() => ScalarMaths.CubeRoot(this);

    /// <summary>Compares <see langword="this"/> to <paramref name="other"/>. { 1 } is returned if <see langword="this"/> is greater than <paramref name="other"/>,
    /// { -1 } is returned if <paramref name="other"/> is greater than <see langword="this"/>, and { 0 } is returned if <see langword="this"/> and
    /// <paramref name="other"/> are equal.
    /// <para>A representation of { <see cref="double.NaN"/> }, { <see cref="double.NegativeInfinity"/> }, or { <see cref="double.PositiveInfinity"/> } is
    /// considered equal to another representation of the same value. { <see cref="double.NaN"/> } is treated as the smallest value.</para></summary>
    /// <param name="other"><see langword="this"/> is compared to this value.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.CompareTo(double)"/>.</remarks>
    public int CompareTo(Scalar other) => Value.CompareTo(other.Value);

    /// <summary>Formats the represented <see cref="Value"/> using the current culture.</summary>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString()"/>.</remarks>
    public override string ToString() => ToString(CultureInfo.CurrentCulture);
    /// <summary>Formats the represented <see cref="Value"/> according to <paramref name="format"/>, using the current culture.</summary>
    /// <param name="format">The format.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString(string?)"/>.</remarks>
    public string ToString(string? format) => ToString(format, CultureInfo.CurrentCulture);
    /// <summary>Formats the represented represented <see cref="Value"/> using culture-specific formatting information provided by <paramref name="formatProvider"/>.</summary>
    /// <param name="formatProvider">Provides culture-specific formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString(IFormatProvider?)"/>.</remarks>
    public string ToString(IFormatProvider? formatProvider) => ToString("G", formatProvider);
    /// <summary>Formats the represented <see cref="Value"/> according to <paramref name="format"/>, using culture-specific formatting information provided by <paramref name="formatProvider"/>.</summary>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">Provides culture-specific formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString(string?, IFormatProvider?)"/>.</remarks>
    public string ToString(string? format, IFormatProvider? formatProvider) => Value.ToString(format, formatProvider);

    /// <inheritdoc/>
    public Scalar Plus() => this;
    /// <inheritdoc/>s
    public Scalar Negate() => -this;

    /// <summary>Computes { <see langword="this"/> + <paramref name="addend"/> }.</summary>
    /// <param name="addend">The second term of { <see langword="this"/> + <paramref name="addend"/> }.</param>
    public Scalar Add(Scalar addend) => this + addend;
    /// <summary>Computes { <see langword="this"/> - <paramref name="subtrahend"/> }.</summary>
    /// <param name="subtrahend">The second term of { <see langword="this"/> - <paramref name="subtrahend"/> }.</param>
    public Scalar Subtract(Scalar subtrahend) => this - subtrahend;

    /// <summary>Computes { <see langword="this"/> % <paramref name="divisor"/> }.</summary>
    /// <param name="divisor">The divisor of { <see langword="this"/> % <paramref name="divisor"/> }.</param>
    public Scalar Remainder(Scalar divisor) => this % divisor;
    /// <inheritdoc/>
    public Scalar Multiply(Scalar factor) => this * factor;
    /// <inheritdoc/>
    public Scalar Divide(Scalar divisor) => this / divisor;

    /// <inheritdoc cref="Multiply(Scalar)"/>
    /// <typeparam name="TScalar">The type of <paramref name="factor"/>.</typeparam>
    public TScalar Multiply<TScalar>(TScalar factor) where TScalar : IScalarQuantity<TScalar> => TScalar.WithMagnitude(this * factor.Magnitude);

    /// <inheritdoc cref="Divide(Scalar)"/>
    /// <typeparam name="TScalar">The type of <paramref name="divisor"/>.</typeparam>
    public Unhandled Divide<TScalar>(TScalar divisor) where TScalar : IScalarQuantity => this / divisor;

    /// <summary>Computes { <paramref name="dividend"/> / <see langword="this"/> }.</summary>
    /// <typeparam name="TScalar">The type of <paramref name="dividend"/>.</typeparam>
    /// <param name="dividend">The dividend of { <paramref name="dividend"/> / <see langword="this"/> }.</param>
    public TScalar DivideInto<TScalar>(TScalar dividend) where TScalar : IScalarQuantity<TScalar> => TScalar.WithMagnitude(dividend.Magnitude / this);

    /// <summary>Computes { <see langword="this"/> ∙ <paramref name="factor"/> }.</summary>
    /// <param name="factor">The second factor of { <see langword="this"/> ∙ <paramref name="factor"/> }.</param>
    public Vector2 Multiply(Vector2 factor) => this * factor;
    /// <summary>Computes { <paramref name="dividend"/> / <see langword="this"/> }.</summary>
    /// <param name="dividend">The dividend of { <paramref name="dividend"/> / <see langword="this"/> }.</param>
    public Vector2 DivideInto(Vector2 dividend) => dividend / this;
    /// <summary>Computes { <paramref name="dividend"/> % <see langword="this"/> }.</summary>
    /// <param name="dividend">The dividend of { <paramref name="dividend"/> % <see langword="this"/> }.</param>
    public Vector2 Remainder(Vector2 dividend) => dividend % this;

    /// <inheritdoc cref="Multiply(Vector2)"/>
    public Vector3 Multiply(Vector3 factor) => this * factor;
    /// <inheritdoc cref="DivideInto(Vector2)"/>
    public Vector3 DivideInto(Vector3 dividend) => dividend / this;
    /// <inheritdoc cref="Remainder(Vector2)"/>
    public Vector3 Remainder(Vector3 dividend) => dividend % this;

    /// <inheritdoc cref="Multiply(Vector2)"/>
    public Vector4 Multiply(Vector4 factor) => this * factor;
    /// <inheritdoc cref="DivideInto(Vector2)"/>
    public Vector4 DivideInto(Vector4 dividend) => dividend / this;
    /// <inheritdoc cref="Remainder(Vector2)"/>
    public Vector4 Remainder(Vector4 dividend) => dividend % this;

    /// <inheritdoc/>
    public static Scalar operator +(Scalar x) => x;
    /// <inheritdoc/>
    public static Scalar operator -(Scalar x) => -x.Value;

    /// <summary>Increments <paramref name="value"/>, { <paramref name="value"/> + 1 }.</summary>
    /// <param name="value">The incremented value.</param>
    public static Scalar operator ++(Scalar value) => value + 1;
    /// <summary>Decrements <paramref name="value"/>, { <paramref name="value"/> - 1 }.</summary>
    /// <param name="value">The decremented value.</param>
    public static Scalar operator --(Scalar value) => value - 1;

    /// <summary>Computes { <paramref name="x"/> + <paramref name="y"/> }.</summary>
    /// <param name="x">The first term of { <paramref name="x"/> + <paramref name="y"/> }.</param>
    /// <param name="y">The second term of { <paramref name="x"/> + <paramref name="y"/> }.</param>
    public static Scalar operator +(Scalar x, Scalar y) => x.Value + y.Value;
    /// <summary>Computes { <paramref name="x"/> - <paramref name="y"/> }.</summary>
    /// <param name="x">The first term of { <paramref name="x"/> - <paramref name="y"/> }.</param>
    /// <param name="y">The second term of { <paramref name="x"/> - <paramref name="y"/> }.</param>
    public static Scalar operator -(Scalar x, Scalar y) => x.Value - y.Value;

    /// <summary>Computes { <paramref name="x"/> % <paramref name="y"/> }.</summary>
    /// <param name="x">The dividend of { <paramref name="x"/> % <paramref name="y"/> }.</param>
    /// <param name="y">The divisor of { <paramref name="x"/> % <paramref name="y"/> }.</param>
    public static Scalar operator %(Scalar x, Scalar y) => x.Value % y.Value;
    /// <inheritdoc/>
    public static Scalar operator *(Scalar x, Scalar y) => x.Value * y.Value;
    /// <inheritdoc/>
    public static Scalar operator /(Scalar x, Scalar y) => x.Value / y.Value;

    /// <inheritdoc cref="operator /(Scalar, Scalar)"/>
    public static Unhandled operator /(Scalar x, IScalarQuantity y)
    {
        ArgumentNullException.ThrowIfNull(y);

        return new(x.Value / y.Magnitude.Value);
    }

    /// <summary>Computes { <paramref name="a"/> ∙ <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    public static Vector2 operator *(Scalar a, (double X, double Y) b) => (a * b.X, a * b.Y);
    /// <summary>Computes { <paramref name="a"/> ∙ <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    public static Vector2 operator *((double X, double Y) a, Scalar b) => (a.X * b, a.Y * b);
    /// <summary>Computes { <paramref name="a"/> / <paramref name="b"/> }.</summary>
    /// <param name="a">The dividend of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    /// <param name="b">The divisor of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    public static Vector2 operator /((double X, double Y) a, Scalar b) => (a.X / b, a.Y / b);
    /// <summary>Computes { <paramref name="a"/> % <paramref name="b"/> }.</summary>
    /// <param name="a">The dividend of { <paramref name="a"/> % <paramref name="b"/> }.</param>
    /// <param name="b">The divisor of { <paramref name="a"/> % <paramref name="b"/> }.</param>
    public static Vector2 operator %((double X, double Y) a, Scalar b) => (a.X % b, a.Y % b);

    /// <summary>Computes { <paramref name="a"/> ∙ <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    public static Vector3 operator *(Scalar a, (double X, double Y, double Z) b) => (a * b.X, a * b.Y, a * b.Z);
    /// <summary>Computes { <paramref name="a"/> ∙ <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    public static Vector3 operator *((double X, double Y, double Z) a, Scalar b) => (a.X * b, a.Y * b, a.Z * b);
    /// <summary>Computes { <paramref name="a"/> / <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    public static Vector3 operator /((double X, double Y, double Z) a, Scalar b) => (a.X / b, a.Y / b, a.Z / b);
    /// <summary>Computes { <paramref name="a"/> % <paramref name="b"/> }.</summary>
    /// <param name="a">The dividend of { <paramref name="a"/> % <paramref name="b"/> }.</param>
    /// <param name="b">The divisor of { <paramref name="a"/> % <paramref name="b"/> }.</param>
    public static Vector3 operator %((double X, double Y, double Z) a, Scalar b) => (a.X % b, a.Y % b, a.Z % b);

    /// <summary>Computes { <paramref name="a"/> ∙ <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    public static Vector4 operator *(Scalar a, (double X, double Y, double Z, double W) b) => (a * b.X, a * b.Y, a * b.Z, a * b.W);
    /// <summary>Computes { <paramref name="a"/> ∙ <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    public static Vector4 operator *((double X, double Y, double Z, double W) a, Scalar b) => (a.X * b, a.Y * b, a.Z * b, a.W * b);
    /// <summary>Computes { <paramref name="a"/> / <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    public static Vector4 operator /((double X, double Y, double Z, double W) a, Scalar b) => (a.X / b, a.Y / b, a.Z / b, a.W / b);
    /// <summary>Computes { <paramref name="a"/> % <paramref name="b"/> }.</summary>
    /// <param name="a">The dividend of { <paramref name="a"/> % <paramref name="b"/> }.</param>
    /// <param name="b">The divisor of { <paramref name="a"/> % <paramref name="b"/> }.</param>
    public static Vector4 operator %((double X, double Y, double Z, double W) a, Scalar b) => (a.X % b, a.Y % b, a.Z % b, a.W % b);

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

    /// <summary>Produces the <see cref="Scalar"/> representing <paramref name="x"/>.</summary>
    public static implicit operator Scalar(double x) => new(x);

    /// <summary>Produces the <see cref="double"/> equivalent to <paramref name="x"/>.</summary>
    public static implicit operator double(Scalar x) => x.Value;

    /// <summary>Describes mathematical operations that result in a pure <see cref="Scalar"/>.</summary>
    private static IScalarResultingMaths<Scalar> ScalarMaths { get; } = MathFactory.ScalarResult();
}
