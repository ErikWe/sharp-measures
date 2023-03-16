namespace SharpMeasures;

using System;
using System.Globalization;

/// <summary>A scalar value, representing a <see cref="double"/> magnitude.</summary>
public readonly record struct Scalar : IScalarQuantity<Scalar>, IComparable<Scalar>, IFormattable
{
    /// <summary>The <see cref="Scalar"/> representing { 0 }.</summary>
    public static Scalar Zero { get; } = 0;

    /// <summary>The <see cref="Scalar"/> representing { 1 }.</summary>
    public static Scalar One { get; } = 1;

    /// <summary>The <see cref="Scalar"/> representing { <see cref="double.NaN"/> }.</summary>
    public static Scalar NaN { get; } = double.NaN;

    /// <summary>The <see cref="Scalar"/> representing { ∞ }.</summary>
    public static Scalar PositiveInfinity { get; } = double.PositiveInfinity;

    /// <summary>The <see cref="Scalar"/> representing { -∞ }.</summary>
    public static Scalar NegativeInfinity { get; } = double.NegativeInfinity;

    /// <summary>The magnitude represented by the <see cref="Scalar"/>.</summary>
    public double Value { get; }
    Scalar IScalarQuantity.Magnitude => this;

    /// <summary>Instantiates a <see cref="Scalar"/>, representing a <see cref="double"/> magnitude.</summary>
    /// <param name="value">The magnitude represented by the constructed <see cref="Scalar"/>.</param>
    public Scalar(double value)
    {
        Value = value;
    }

    static Scalar IScalarQuantity<Scalar>.WithMagnitude(Scalar magnitude) => magnitude;

    /// <summary>Indicates whether the <see cref="Scalar"/> represents { <see cref="double.NaN"/> }.</summary>
    public bool IsNaN => double.IsNaN(Value);

    /// <summary>Indicates whether the <see cref="Scalar"/> represents { 0 }.</summary>
    public bool IsZero => Value is 0;

    /// <summary>Indicates whether the <see cref="Scalar"/> represents a value greater than or equal to { 0 }.</summary>
    public bool IsPositive => double.IsPositive(Value);

    /// <summary>Indicates whether the <see cref="Scalar"/> represents a value smaller than { 0 }.</summary>
    public bool IsNegative => double.IsNegative(Value);

    /// <summary>Indicates whether the <see cref="Scalar"/> represents a finite value.</summary>
    public bool IsFinite => double.IsFinite(Value);

    /// <summary>Indicates whether the <see cref="Scalar"/> represents an infinite value.</summary>
    public bool IsInfinite => double.IsInfinity(Value);

    /// <summary>Indicates whether the <see cref="Scalar"/> represents { <see cref="double.PositiveInfinity"/> }.</summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Value);

    /// <summary>Indicates whether the <see cref="Scalar"/> represents { <see cref="double.NegativeInfinity"/> }.</summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Value);

    /// <summary>Computes the absolute value of the <see cref="Scalar"/> - resulting in a <see cref="Scalar"/> representing a value of the same magnitude, but always positive.</summary>
    /// <returns>The absolute value of the <see cref="Scalar"/>.</returns>
    public Scalar Absolute() => Math.Abs(Value);

    /// <summary>Computes the floor of the <see cref="Scalar"/> - rounding the <see cref="Scalar"/> towards { -∞ }.</summary>
    /// <returns>The floor of the <see cref="Scalar"/>.</returns>
    public Scalar Floor() => Math.Floor(Value);

    /// <summary>Computes the ceiling of the <see cref="Scalar"/> - rounding the <see cref="Scalar"/> towards { ∞ }.</summary>
    /// <returns>The ceiling of the <see cref="Scalar"/>.</returns>
    public Scalar Ceiling() => Math.Ceiling(Value);

    /// <summary>Rounds the <see cref="Scalar"/> to the nearest integer value.</summary>
    /// <remarks>Midpoint values are rounded to the nearest even integer value - consistent with <see cref="Math.Round(double)"/>.</remarks>
    /// <returns>The rounded <see cref="Scalar"/>.</returns>
    public Scalar Round() => Math.Round(Value);

    /// <summary>Truncates the <see cref="Scalar"/> - rounding the <see cref="Scalar"/> towards { 0 }.</summary>
    /// <returns>The truncated <see cref="Scalar"/>.</returns>
    public Scalar Truncate() => Math.Truncate(Value);

    /// <summary>Computes the sign of the <see cref="Scalar"/>, resulting in:
    /// <list type="bullet">
    /// <item><term>1</term><description> The magnitude is positive, but not { 0 }.</description></item>
    /// <item><term>0</term><description> The magnitude is { 0 }.</description></item>
    /// <item><term>-1</term><description> The magnitude is negative.</description></item>
    /// </list></summary>
    /// <returns>One of the <see cref="int"/> values { 1, 0, -1 }, as detailed above.</returns>
    /// <remarks>An <see cref="ArithmeticException"/> will be thrown if the <see cref="Scalar"/> represents { <see cref="double.NaN"/> }.</remarks>
    /// <exception cref="ArithmeticException"/>
    public int Sign() => Math.Sign(Value);

    /// <summary>Computes the <see cref="Scalar"/> raised to the provided power.</summary>
    /// <param name="exponent">The <see cref="Scalar"/> representing the exponent.</param>
    /// <returns>The <see cref="Scalar"/> raised to the provided power, { <see langword="this"/> ^ <paramref name="exponent"/> }.</returns>
    public Scalar Power(Scalar exponent) => Math.Pow(Value, exponent.Value);

    /// <summary>Computes the square of the <see cref="Scalar"/>.</summary>
    /// <returns>The square of the <see cref="Scalar"/>, { <see langword="this"/> ² }.</returns>
    public Scalar Square() => Value * Value;

    /// <summary>Computes the cube of the <see cref="Scalar"/>.</summary>
    /// <returns>The cube of the <see cref="Scalar"/>, { <see langword="this"/> ³ }.</returns>
    public Scalar Cube() => Value * Value * Value;

    /// <summary>Computes the square root of the <see cref="Scalar"/>.</summary>
    /// <returns>The square root of the <see cref="Scalar"/>, { √ <see langword="this"/> }.</returns>
    public Scalar SquareRoot() => Math.Sqrt(Value);

    /// <summary>Computes the cube root of the <see cref="Scalar"/>.</summary>
    /// <returns>The cube root of the <see cref="Scalar"/>, { ³√ <see langword="this"/> }.</returns>
    public Scalar CubeRoot() => Math.Cbrt(Value);

    /// <summary>Compares the <see cref="Scalar"/> to another, provided, <see cref="Scalar"/>, resulting in:
    /// <list type="bullet">
    /// <item><term>1</term><description> The <see cref="Scalar"/> represents a larger magnitude than does the provided <see cref="Scalar"/>.</description></item>
    /// <item><term>0</term><description> The <see cref="Scalar"/> and the provided <see cref="Scalar"/> represent the same magnitude.</description></item>
    /// <item><term>-1</term><description> The <see cref="Scalar"/> represents a smaller magnitude than does the provided <see cref="Scalar"/>.</description></item>
    /// </list><para>The value { <see cref="NaN"/> } represents the smallest possible value.</para></summary>
    /// <param name="other">The <see cref="Scalar"/> to which the original <see cref="Scalar"/> is compared.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.CompareTo(double)"/>.</remarks>
    /// <returns>One of the <see cref="int"/> values { 1, 0, -1 }, as detailed above.</returns>
    public int CompareTo(Scalar other) => Value.CompareTo(other.Value);

    /// <summary>Determines whether the <see cref="Scalar"/> is equivalent to another, provided, <see cref="Scalar"/>.</summary>
    /// <param name="other">The <see cref="Scalar"/> to which the original <see cref="Scalar"/> is compared.</param>
    /// <remarks>The values { <see cref="NaN"/> }, { <see cref="PositiveInfinity"/> }, and {<see cref="NegativeInfinity"/> } are all considered equivalent to themselves.</remarks>
    /// <returns>A <see cref="bool"/> indicating whether the two <see cref="Scalar"/> are equivalent.</returns>
    public bool Equals(Scalar other) => Value.Equals(other.Value);

    /// <summary>Determines whether the provided <see cref="Scalar"/> are equivalent.</summary>
    /// <param name="lhs">The first of the two <see cref="Scalar"/> that are compared.</param>
    /// <param name="rhs">The second of the two <see cref="Scalar"/> that are compared.</param>
    /// <remarks>The values { <see cref="NaN"/> }, { <see cref="PositiveInfinity"/> }, and {<see cref="NegativeInfinity"/> } are all considered equivalent to themselves.</remarks>
    /// <returns>A <see cref="bool"/> indicating whether the provided <see cref="Scalar"/> are equivalent.</returns>
    public static bool Equals(Scalar lhs, Scalar rhs) => lhs.Equals(rhs);

    /// <summary>Computes the <see cref="int"/> hash code describing the <see cref="Scalar"/>.</summary>
    /// <returns>The <see cref="int"/> hash code describing the <see cref="Scalar"/>.</returns>
    public override int GetHashCode() => Value.GetHashCode();

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Scalar"/>, formatted according to the <see cref="CultureInfo.CurrentCulture"/>.</summary>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString()"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Scalar"/>.</returns>
    public override string ToString() => ToString(CultureInfo.CurrentCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Scalar"/>, formatted according to the provided <see cref="IFormatProvider"/>.</summary>
    /// <param name="formatProvider">The <see cref="IFormatProvider"/>, providing culture-specific formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString(IFormatProvider?)"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Scalar"/>.</returns>
    public string ToString(IFormatProvider? formatProvider) => ToString("G", formatProvider);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Scalar"/>, formatted according to the provided <see cref="string"/> and the <see cref="CultureInfo.CurrentCulture"/>.</summary>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString(string?)"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Scalar"/>.</returns>
    public string ToString(string? format) => ToString(format, CultureInfo.CurrentCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Scalar"/>, formatted according to the provided <see cref="string"/> and <see cref="IFormatProvider"/>.</summary>
    /// <param name="formatProvider">The <see cref="IFormatProvider"/>, providing culture-specific formatting information.</param>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString(string?, IFormatProvider?)"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Scalar"/>.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider) => Value.ToString(format, formatProvider);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Scalar"/>, formatted according to the <see cref="CultureInfo.InvariantCulture"/>.</summary>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString(IFormatProvider)"/>, with the <see cref="CultureInfo.InvariantCulture"/> as the provider.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Scalar"/>.</returns>
    public string ToStringInvariant() => ToString(CultureInfo.InvariantCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Scalar"/>, formatted according to the provided <see cref="string"/> and the <see cref="CultureInfo.InvariantCulture"/>.</summary>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString(string?, IFormatProvider)"/>, with the <see cref="CultureInfo.InvariantCulture"/> as the provider.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Scalar"/>.</returns>
    public string ToStringInvariant(string? format) => ToString(format, CultureInfo.InvariantCulture);

    /// <inheritdoc/>
    public Scalar Plus() => this;

    /// <inheritdoc/>
    public Scalar Negate() => -Value;

    /// <summary>Computes the sum of the <see cref="Scalar"/> and another, provided, <see cref="Scalar"/>.</summary>
    /// <param name="addend">The <see cref="Scalar"/> that is added to the original <see cref="Scalar"/>.</param>
    /// <returns>The sum of the <see cref="Scalar"/>, { <see langword="this"/> + <paramref name="addend"/> }.</returns>
    public Scalar Add(Scalar addend) => Value + addend.Value;

    /// <summary>Computes the difference between the <see cref="Scalar"/> and another, provided, <see cref="Scalar"/>.</summary>
    /// <param name="other">The <see cref="Scalar"/> for which the difference to the original <see cref="Scalar"/> is computed.</param>
    /// <returns>The difference between the two <see cref="Scalar"/>, the absolute value of { <see langword="this"/> - <paramref name="other"/> }.</returns>
    public Scalar Difference(Scalar other) => Math.Abs(Value - other.Value);

    /// <summary>Computes the signed difference between the <see cref="Scalar"/> and another, provided, <see cref="Scalar"/>.</summary>
    /// <param name="subtrahend">The <see cref="Scalar"/> that is subtracted from the original <see cref="Scalar"/>.</param>
    /// <remarks>The resulting <see cref="Scalar"/> may be negative. If this is not desired, use <see cref="Difference(Scalar)"/>.</remarks>
    /// <returns>The signed difference between the two <see cref="Scalar"/>, { <see langword="this"/> - <paramref name="subtrahend"/> }.</returns>
    public Scalar Subtract(Scalar subtrahend) => Value - subtrahend.Value;

    /// <summary>Computes the remainder from division of the <see cref="Scalar"/> by another, provided, <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Scalar"/> by which the original <see cref="Scalar"/> is divided.</param>
    /// <returns>The remainder from division of the <see cref="Scalar"/>, { <see langword="this"/> % <paramref name="divisor"/> }.</returns>
    public Scalar Remainder(Scalar divisor) => Value % divisor.Value;

    /// <summary>Computes the product of the <see cref="Scalar"/> and another, provided, <see cref="Scalar"/>.</summary>
    /// <param name="factor">The <see cref="Scalar"/> by which the original <see cref="Scalar"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Scalar"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Scalar Multiply(Scalar factor) => Value * factor.Value;

    /// <summary>Scales the provided scalar quantity, of type <typeparamref name="TScalar"/>, by the <see cref="Scalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity that is scaled by the <see cref="Scalar"/>.</typeparam>
    /// <param name="factor">The <typeparamref name="TScalar"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <typeparamref name="TScalar"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public TScalar Multiply<TScalar>(TScalar factor) where TScalar : IScalarQuantity<TScalar> => TScalar.WithMagnitude(this * factor.Magnitude);

    /// <summary>Computes the quotient of the <see cref="Scalar"/> and another, provided, <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Scalar"/> by which the original <see cref="Scalar"/> is divided.</param>
    /// <returns>The quotient of the two <see cref="Scalar"/>, { <see langword="this"/> / <paramref name="divisor"/> }.</returns>
    public Scalar Divide(Scalar divisor) => Value / divisor.Value;

    /// <summary>Determines whether a <see cref="Scalar"/>, <paramref name="x"/>, represents a smaller magnitude than another <see cref="Scalar"/>, <paramref name="y"/>.</summary>
    /// <param name="x">The first <see cref="Scalar"/>, assumed to represent a smaller magnitude than the other <see cref="Scalar"/>.</param>
    /// <param name="y">The second <see cref="Scalar"/>, assumed to represent a greater magntiude than the other <see cref="Scalar"/>.</param>
    /// <remarks>A <see cref="bool"/> representing the truthfulness of { <paramref name="x"/> &lt; <paramref name="y"/> }.</remarks>
    public static bool operator <(Scalar x, Scalar y) => x.Value < y.Value;

    /// <summary>Determines whether a <see cref="Scalar"/>, <paramref name="x"/>, represents a greater magnitude than another <see cref="Scalar"/>, <paramref name="y"/>.</summary>
    /// <param name="x">The first <see cref="Scalar"/>, assumed to represent a greater magnitude than the other <see cref="Scalar"/>.</param>
    /// <param name="y">The second <see cref="Scalar"/>, assumed to represent a smaller magntiude than the other <see cref="Scalar"/>.</param>
    /// <remarks>A <see cref="bool"/> representing the truthfulness of { <paramref name="x"/> &gt; <paramref name="y"/> }.</remarks>
    public static bool operator >(Scalar x, Scalar y) => x.Value > y.Value;

    /// <summary>Determines whether a <see cref="Scalar"/>, <paramref name="x"/>, represents a smaller or equivalent magnitude compared to another <see cref="Scalar"/>, <paramref name="y"/>.</summary>
    /// <param name="x">The first <see cref="Scalar"/>, assumed to represent a smaller or equivalent magnitude compared to the other <see cref="Scalar"/>.</param>
    /// <param name="y">The second <see cref="Scalar"/>, assumed to represent a greater or equivalent magnitude compared to the other <see cref="Scalar"/>.</param>
    /// <remarks>A <see cref="bool"/> representing the truthfulness of { <paramref name="x"/> ≤ <paramref name="y"/> }.</remarks>
    public static bool operator <=(Scalar x, Scalar y) => x.Value <= y.Value;

    /// <summary>Determines whether a <see cref="Scalar"/>, <paramref name="x"/>, represents a greater or equivalent magnitude compared to another <see cref="Scalar"/>, <paramref name="y"/>.</summary>
    /// <param name="x">The first <see cref="Scalar"/>, assumed to represent a greater or equivalent magnitude compared to the other <see cref="Scalar"/>.</param>
    /// <param name="y">The second <see cref="Scalar"/>, assumed to represent a smaller or equivalent magnitude compared to the other <see cref="Scalar"/>.</param>
    /// <remarks>A <see cref="bool"/> representing the truthfulness of { <paramref name="x"/> ≥ <paramref name="y"/> }.</remarks>
    public static bool operator >=(Scalar x, Scalar y) => x.Value >= y.Value;

    /// <inheritdoc/>
    public static Scalar operator +(Scalar x) => x.Plus();

    /// <inheritdoc/>
    public static Scalar operator -(Scalar x) => x.Negate();

    /// <summary>Computes the sum of the provided <see cref="Scalar"/>.</summary>
    /// <param name="x">The first <see cref="Scalar"/>, added to the second <see cref="Scalar"/>.</param>
    /// <param name="y">The second <see cref="Scalar"/>, added to the first <see cref="Scalar"/>.</param>
    /// <returns>The sum of the <see cref="Scalar"/>, { <paramref name="x"/> + <paramref name="y"/> }.</returns>
    public static Scalar operator +(Scalar x, Scalar y) => x.Add(y);

    /// <summary>Computes the signed difference between the provided <see cref="Scalar"/>.</summary>
    /// <param name="x">The first <see cref="Scalar"/>, from which the second <see cref="Scalar"/> is subtracted.</param>
    /// <param name="y">The second <see cref="Scalar"/>, which is subtracted from the first <see cref="Scalar"/>.</param>
    /// <remarks>The resulting <see cref="Scalar"/> may be negative. If this is not desired, use <see cref="Difference(Scalar)"/>.</remarks>
    /// <returns>The signed difference between the two <see cref="Scalar"/>, { <paramref name="x"/> - <paramref name="y"/> }.</returns>
    public static Scalar operator -(Scalar x, Scalar y) => x.Subtract(y);

    /// <summary>Computes the remainder from division of the provided <see cref="Scalar"/>.</summary>
    /// <param name="x">The first <see cref="Scalar"/>, which is divided by the second <see cref="Scalar"/>.</param>
    /// <param name="y">The second <see cref="Scalar"/>, by which the first <see cref="Scalar"/> is divided.</param>
    /// <returns>The remainder from division of the <see cref="Scalar"/>, { <paramref name="x"/> % <paramref name="y"/> }.</returns>
    public static Scalar operator %(Scalar x, Scalar y) => x.Remainder(y);

    /// <summary>Computes the product of the provided <see cref="Scalar"/>.</summary>
    /// <param name="x">The first <see cref="Scalar"/>, by which the second <see cref="Scalar"/> is multiplied.</param>
    /// <param name="y">The second <see cref="Scalar"/>, by which the first <see cref="Scalar"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Scalar"/>, { <paramref name="x"/> ∙ <paramref name="y"/> }.</returns>
    public static Scalar operator *(Scalar x, Scalar y) => x.Multiply(y);

    /// <summary>Computes the quotient of the provided <see cref="Scalar"/>.</summary>
    /// <param name="x">The first <see cref="Scalar"/>, which is divided by the second <see cref="Scalar"/>.</param>
    /// <param name="y">The second <see cref="Scalar"/>, by which the first <see cref="Scalar"/> is divided.</param>
    /// <returns>The quotient of the <see cref="Scalar"/>, { <paramref name="x"/> / <paramref name="y"/> }.</returns>
    public static Scalar operator /(Scalar x, Scalar y) => x.Divide(y);

    /// <summary>Computes the element-wise remainder from division of the provided <see cref="Vector2"/>-tuple by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector2"/>-tuple, which is divided by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, by which the <see cref="Vector2"/>-tuple is divided.</param>
    /// <returns>The element-wise remainder from division of the <see cref="Vector2"/>-tuple by the <see cref="Scalar"/>, { <paramref name="a"/> % <paramref name="b"/> }.</returns>
    public static Vector2 operator %((double X, double Y) a, Scalar b) => ((Vector2)a).Remainder(b);

    /// <summary>Scales the provided <see cref="Vector2"/>-tuple by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Scalar"/>, by which the <see cref="Vector2"/>-tuple is scaled.</param>
    /// <param name="b">The <see cref="Vector2"/>-tuple, which is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Vector2"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Vector2 operator *(Scalar a, (double X, double Y) b) => ((Vector2)b).Multiply(a);

    /// <summary>Scales the provided <see cref="Vector2"/>-tuple by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector2"/>-tuple, which is scaled by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, by which the <see cref="Vector2"/>-tuple is scaled.</param>
    /// <returns>The scaled <see cref="Vector2"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Vector2 operator *((double X, double Y) a, Scalar b) => ((Vector2)a).Multiply(b);

    /// <summary>Scales the provided <see cref="Vector2"/>-tuple through division by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector2"/>-tuple, which is scaled through division by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, scaling the <see cref="Vector2"/>-tuple through division.</param>
    /// <returns>The scaled <see cref="Vector2"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    public static Vector2 operator /((double X, double Y) a, Scalar b) => ((Vector2)a).Divide(b);

    /// <summary>Computes the element-wise remainder from division of the provided <see cref="Vector3"/>-tuple by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector3"/>-tuple, which is divided by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, by which the <see cref="Vector3"/>-tuple is divided.</param>
    /// <returns>The element-wise remainder from division of the <see cref="Vector3"/>-tuple by the <see cref="Scalar"/>, { <paramref name="a"/> % <paramref name="b"/> }.</returns>
    public static Vector3 operator %((double X, double Y, double Z) a, Scalar b) => ((Vector3)a).Remainder(b);

    /// <summary>Scales the provided <see cref="Vector3"/>-tuple by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Scalar"/>, by which the <see cref="Vector3"/>-tuple is scaled.</param>
    /// <param name="b">The <see cref="Vector3"/>-tuple, which is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Vector3"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Vector3 operator *(Scalar a, (double X, double Y, double Z) b) => ((Vector3)b).Multiply(a);

    /// <summary>Scales the provided <see cref="Vector3"/>-tuple by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector3"/>-tuple, which is scaled by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, by which the <see cref="Vector3"/>-tuple is scaled.</param>
    /// <returns>The scaled <see cref="Vector3"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Vector3 operator *((double X, double Y, double Z) a, Scalar b) => ((Vector3)a).Multiply(b);

    /// <summary>Scales the provided <see cref="Vector3"/>-tuple through division by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector3"/>-tuple, which is scaled through division by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, scaling the <see cref="Vector3"/>-tuple through division.</param>
    /// <returns>The scaled <see cref="Vector3"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    public static Vector3 operator /((double X, double Y, double Z) a, Scalar b) => ((Vector3)a).Divide(b);

    /// <summary>Computes the element-wise remainder from division of the provided <see cref="Vector4"/>-tuple by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector4"/>-tuple, which is divided by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, by which the <see cref="Vector4"/>-tuple is divided.</param>
    /// <returns>The element-wise remainder from division of the <see cref="Vector4"/>-tuple by the <see cref="Scalar"/>, { <paramref name="a"/> % <paramref name="b"/> }.</returns>
    public static Vector4 operator %((double X, double Y, double Z, double W) a, Scalar b) => ((Vector4)a).Remainder(b);

    /// <summary>Scales the provided <see cref="Vector4"/>-tuple by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Scalar"/>, by which the <see cref="Vector4"/>-tuple is scaled.</param>
    /// <param name="b">The <see cref="Vector4"/>-tuple, which is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Vector4"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Vector4 operator *(Scalar a, (double X, double Y, double Z, double W) b) => ((Vector4)b).Multiply(a);

    /// <summary>Scales the provided <see cref="Vector4"/>-tuple by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector4"/>-tuple, which is scaled by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, by which the <see cref="Vector4"/>-tuple is scaled.</param>
    /// <returns>The scaled <see cref="Vector4"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Vector4 operator *((double X, double Y, double Z, double W) a, Scalar b) => ((Vector4)a).Multiply(b);

    /// <summary>Scales the provided <see cref="Vector4"/>-tuple through division by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector4"/>-tuple, which is scaled through division by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, scaling the <see cref="Vector4"/>-tuple through division.</param>
    /// <returns>The scaled <see cref="Vector4"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    public static Vector4 operator /((double X, double Y, double Z, double W) a, Scalar b) => ((Vector4)a).Divide(b);

    /// <summary>Constructs a <see cref="Scalar"/>, representing the provided <see cref="double"/> magnitude.</summary>
    /// <param name="value">The magnitude represented by the constructed <see cref="Scalar"/>.</param>
    /// <returns>The constructed <see cref="Scalar"/>.</returns>
    public static Scalar FromDouble(double value) => new(value);

    /// <summary>Retrieves the <see cref="double"/> magnitude of the <see cref="Scalar"/>.</summary>
    /// <returns>The <see cref="double"/> magnitude of the <see cref="Scalar"/>.</returns>
    public double ToDouble() => Value;

    /// <summary>Constructs a <see cref="Scalar"/>, representing the provided <see cref="double"/> magnitude.</summary>
    /// <param name="value">The magnitude represented by the constructed <see cref="Scalar"/>.</param>
    /// <returns>The constructed <see cref="Scalar"/>.</returns>
    public static implicit operator Scalar(double value) => FromDouble(value);

    /// <summary>Retrieves the <see cref="double"/> magnitude of the provided <see cref="Scalar"/>.</summary>
    /// <param name="scalar">The <see cref="Scalar"/>, from which the <see cref="double"/> magnitude is retrieved.</param>
    /// <returns>The <see cref="double"/> magnitude of the <see cref="Scalar"/>.</returns>
    public static implicit operator double(Scalar scalar) => scalar.ToDouble();
}
