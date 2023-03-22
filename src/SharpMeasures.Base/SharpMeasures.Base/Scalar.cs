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
    private double Value { get; }
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

    /// <summary>Computes the reciprocal, or multiplicative inverse, of the <see cref="Scalar"/>.</summary>
    /// <returns>The reciprocal of the <see cref="Scalar"/>, { 1 / <see langword="this"/> }.</returns>
    public Scalar Reciprocal() => 1 / Value;

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

    /// <summary>Applies the unary plus to the <see cref="Scalar"/>.</summary>
    /// <returns>The same <see cref="Scalar"/>, { <see langword="this"/> }.</returns>
    public Scalar Plus() => this;

    /// <summary>Negates the <see cref="Scalar"/>.</summary>
    /// <returns>The negated <see cref="Scalar"/>, { -<see langword="this"/> }.</returns>
    public Scalar Negate() => -Value;

    /// <summary>Computes the sum of the <see cref="Scalar"/> and another, provided, <see cref="Scalar"/>.</summary>
    /// <param name="addend">The <see cref="Scalar"/> that is added to the original <see cref="Scalar"/>.</param>
    /// <returns>The sum of the <see cref="Scalar"/>, { <see langword="this"/> + <paramref name="addend"/> }.</returns>
    public Scalar Add(Scalar addend) => Value + addend.Value;

    /// <summary>Computes the difference between the <see cref="Scalar"/> and another, provided, <see cref="Scalar"/>.</summary>
    /// <param name="other">The <see cref="Scalar"/> for which the difference to the original <see cref="Scalar"/> is computed.</param>
    /// <returns>The difference between the <see cref="Scalar"/>, { |<see langword="this"/> - <paramref name="other"/>| }.</returns>
    public Scalar Difference(Scalar other) => Math.Abs(Value - other.Value);

    /// <summary>Computes the signed difference between the <see cref="Scalar"/> and another, provided, <see cref="Scalar"/>.</summary>
    /// <param name="subtrahend">The <see cref="Scalar"/> that is subtracted from the original <see cref="Scalar"/>.</param>
    /// <remarks>The resulting <see cref="Scalar"/> may be negative. If this is not desired, use <see cref="Difference(Scalar)"/>.</remarks>
    /// <returns>The signed difference between the <see cref="Scalar"/>, { <see langword="this"/> - <paramref name="subtrahend"/> }.</returns>
    public Scalar Subtract(Scalar subtrahend) => Value - subtrahend.Value;

    /// <summary>Computes the remainder from division of the <see cref="Scalar"/> by another, provided, <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Scalar"/> by which the original <see cref="Scalar"/> is divided.</param>
    /// <returns>The remainder from division of the <see cref="Scalar"/>, { <see langword="this"/> % <paramref name="divisor"/> }.</returns>
    public Scalar Remainder(Scalar divisor) => Value % divisor.Value;

    /// <summary>Computes the product of the <see cref="Scalar"/> and another, provided, <see cref="Scalar"/>.</summary>
    /// <param name="factor">The <see cref="Scalar"/> by which the original <see cref="Scalar"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Scalar"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Scalar Multiply(Scalar factor) => Multiply<Scalar>(factor);

    /// <summary>Scales the provided <see cref="Vector2"/> by the <see cref="Scalar"/>.</summary>
    /// <param name="factor">The <see cref="Vector2"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Vector2"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Vector2 Multiply(Vector2 factor) => Multiply2(factor);

    /// <summary>Scales the provided <see cref="Vector3"/> by the <see cref="Scalar"/>.</summary>
    /// <param name="factor">The <see cref="Vector3"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Vector3"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Vector3 Multiply(Vector3 factor) => Multiply3(factor);

    /// <summary>Scales the provided <see cref="Vector4"/> by the <see cref="Scalar"/>.</summary>
    /// <param name="factor">The <see cref="Vector4"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Vector4"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Vector4 Multiply(Vector4 factor) => Multiply4(factor);

    /// <summary>Scales the provided <see cref="Unhandled2"/> by the <see cref="Scalar"/>.</summary>
    /// <param name="factor">The <see cref="Unhandled2"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Unhandled2"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Unhandled2 Multiply(Unhandled2 factor) => Multiply2(factor);

    /// <summary>Scales the provided <see cref="Unhandled3"/> by the <see cref="Scalar"/>.</summary>
    /// <param name="factor">The <see cref="Unhandled3"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Unhandled3"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Unhandled3 Multiply(Unhandled3 factor) => Multiply3(factor);

    /// <summary>Scales the provided <see cref="Unhandled4"/> by the <see cref="Scalar"/>.</summary>
    /// <param name="factor">The <see cref="Unhandled4"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Unhandled4"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Unhandled4 Multiply(Unhandled4 factor) => Multiply4(factor);

    /// <summary>Scales the provided <typeparamref name="TScalar"/> by the <see cref="Scalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity that is scaled by the <see cref="Scalar"/>.</typeparam>
    /// <param name="factor">The <typeparamref name="TScalar"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <typeparamref name="TScalar"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public TScalar Multiply<TScalar>(TScalar factor) where TScalar : IScalarQuantity<TScalar>
    {
        ArgumentNullException.ThrowIfNull(factor);

        return TScalar.WithMagnitude(Value * factor.Magnitude.Value);
    }

    /// <summary>Scales the provided <typeparamref name="TVector"/> by the <see cref="Scalar"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is scaled by the <see cref="Scalar"/>.</typeparam>
    /// <param name="factor">The <typeparamref name="TVector"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply2{TVector}(TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The scaled <typeparamref name="TVector"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public TVector Multiply<TVector>(IVector2Quantity<TVector> factor) where TVector : IVector2Quantity<TVector>
    {
        ArgumentNullException.ThrowIfNull(factor);

        return TVector.WithComponents(Multiply(factor.Components));
    }

    /// <summary>Scales the provided <typeparamref name="TVector"/> by the <see cref="Scalar"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is scaled by the <see cref="Scalar"/>.</typeparam>
    /// <param name="factor">The <typeparamref name="TVector"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply3{TVector}(TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The scaled <typeparamref name="TVector"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public TVector Multiply<TVector>(IVector3Quantity<TVector> factor) where TVector : IVector3Quantity<TVector>
    {
        ArgumentNullException.ThrowIfNull(factor);

        return TVector.WithComponents(Multiply(factor.Components));
    }

    /// <summary>Scales the provided <typeparamref name="TVector"/> by the <see cref="Scalar"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is scaled by the <see cref="Scalar"/>.</typeparam>
    /// <param name="factor">The <typeparamref name="TVector"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply4{TVector}(TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The scaled <typeparamref name="TVector"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public TVector Multiply<TVector>(IVector4Quantity<TVector> factor) where TVector : IVector4Quantity<TVector>
    {
        ArgumentNullException.ThrowIfNull(factor);

        return TVector.WithComponents(Multiply(factor.Components));
    }

    /// <summary>Scales the provided <typeparamref name="TVector"/> by the <see cref="Scalar"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is scaled by the <see cref="Scalar"/>.</typeparam>
    /// <param name="factor">The <typeparamref name="TVector"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <typeparamref name="TVector"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public TVector Multiply2<TVector>(TVector factor) where TVector : IVector2Quantity<TVector>
    {
        ArgumentNullException.ThrowIfNull(factor);

        return TVector.WithComponents(Value * factor.X.Value, Value * factor.Y.Value);
    }

    /// <summary>Scales the provided <typeparamref name="TVector"/> by the <see cref="Scalar"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is scaled by the <see cref="Scalar"/>.</typeparam>
    /// <param name="factor">The <typeparamref name="TVector"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <typeparamref name="TVector"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public TVector Multiply3<TVector>(TVector factor) where TVector : IVector3Quantity<TVector>
    {
        ArgumentNullException.ThrowIfNull(factor);

        return TVector.WithComponents(Value * factor.X.Value, Value * factor.Y.Value, Value * factor.Z.Value);
    }

    /// <summary>Scales the provided <typeparamref name="TVector"/> by the <see cref="Scalar"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is scaled by the <see cref="Scalar"/>.</typeparam>
    /// <param name="factor">The <typeparamref name="TVector"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <typeparamref name="TVector"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public TVector Multiply4<TVector>(TVector factor) where TVector : IVector4Quantity<TVector>
    {
        ArgumentNullException.ThrowIfNull(factor);

        return TVector.WithComponents(Value * factor.X.Value, Value * factor.Y.Value, Value * factor.Z.Value, Value * factor.W.Value);
    }

    /// <summary>Computes the quotient of the <see cref="Scalar"/> and another, provided, <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Scalar"/> by which the original <see cref="Scalar"/> is divided.</param>
    /// <returns>The quotient of the <see cref="Scalar"/>, { <see langword="this"/> / <paramref name="divisor"/> }.</returns>
    public Scalar DivideBy(Scalar divisor) => Value / divisor.Value;

    /// <summary>Scales the reciprocal of the provided <see cref="Unhandled"/> by the <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Unhandled"/>, the reciprocal of which is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Unhandled"/>, { <see langword="this"/> / <paramref name="divisor"/> }.</returns>
    public Unhandled DivideBy(Unhandled divisor) => new(Value / divisor.Magnitude);

    /// <summary>Computes the sum of the provided <see cref="Scalar"/>.</summary>
    /// <param name="x">The first <see cref="Scalar"/>, which is added to the second <see cref="Scalar"/>.</param>
    /// <param name="y">The second <see cref="Scalar"/>, which is added to the first <see cref="Scalar"/>.</param>
    /// <returns>The sum of the <see cref="Scalar"/>, { <paramref name="x"/> + <paramref name="y"/> }.</returns>
    public static Scalar Add(Scalar x, Scalar y) => x.Add(y);

    /// <summary>Computes the difference between the provided <see cref="Scalar"/>.</summary>
    /// <param name="x">The first <see cref="Scalar"/>, for which the difference to the second <see cref="Scalar"/> is computed.</param>
    /// <param name="y">The second <see cref="Scalar"/>, for which the difference to the first <see cref="Scalar"/> is computed.</param>
    /// <returns>The difference between the <see cref="Scalar"/>, { |<paramref name="x"/> - <paramref name="y"/>| }.</returns>
    public static Scalar Difference(Scalar x, Scalar y) => x.Difference(y);

    /// <summary>Computes the signed difference between the provided <see cref="Scalar"/>.</summary>
    /// <param name="x">The first <see cref="Scalar"/>, from which the second <see cref="Scalar"/> is subtracted.</param>
    /// <param name="y">The second <see cref="Scalar"/>, which is subtracted from the first <see cref="Scalar"/>.</param>
    /// <remarks>The resulting <see cref="Scalar"/> may be negative. If this is not desired, use <see cref="Difference(Scalar, Scalar)"/>.</remarks>
    /// <returns>The signed difference between the <see cref="Scalar"/>, { <paramref name="x"/> - <paramref name="y"/> }.</returns>
    public static Scalar Subtract(Scalar x, Scalar y) => x.Subtract(y);

    /// <summary>Computes the remainder from division of the provided <see cref="Scalar"/>.</summary>
    /// <param name="x">The first <see cref="Scalar"/>, which is divided by the second <see cref="Scalar"/>.</param>
    /// <param name="y">The second <see cref="Scalar"/>, by which the first <see cref="Scalar"/> is divided.</param>
    /// <returns>The remainder from division of the <see cref="Scalar"/>, { <paramref name="x"/> % <paramref name="y"/> }.</returns>
    public static Scalar Remainder(Scalar x, Scalar y) => x.Remainder(y);

    /// <summary>Computes the product of the provided <see cref="Scalar"/>.</summary>
    /// <param name="x">The first <see cref="Scalar"/>, by which the second <see cref="Scalar"/> is multiplied.</param>
    /// <param name="y">The second <see cref="Scalar"/>, by which the first <see cref="Scalar"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Scalar"/>, { <paramref name="x"/> ∙ <paramref name="y"/> }.</returns>
    public static Scalar Multiply(Scalar x, Scalar y) => x.Multiply(y);

    /// <summary>Scales the provided <see cref="Vector2"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Scalar"/> by which the <see cref="Vector2"/> is scaled.</param>
    /// <param name="b">The <see cref="Vector2"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Vector2"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Vector2 Multiply(Scalar a, Vector2 b) => a.Multiply(b);

    /// <summary>Scales the provided <see cref="Vector3"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Scalar"/> by which the <see cref="Vector3"/> is scaled.</param>
    /// <param name="b">The <see cref="Vector3"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Vector3"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Vector3 Multiply(Scalar a, Vector3 b) => a.Multiply(b);

    /// <summary>Scales the provided <see cref="Vector4"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Scalar"/> by which the <see cref="Vector4"/> is scaled.</param>
    /// <param name="b">The <see cref="Vector4"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Vector4"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Vector4 Multiply(Scalar a, Vector4 b) => a.Multiply(b);

    /// <summary>Scales the provided <see cref="Unhandled2"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Scalar"/> by which the <see cref="Unhandled2"/> is scaled.</param>
    /// <param name="b">The <see cref="Unhandled2"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Unhandled2"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Unhandled2 Multiply(Scalar a, Unhandled2 b) => a.Multiply(b);

    /// <summary>Scales the provided <see cref="Unhandled3"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Scalar"/> by which the <see cref="Unhandled3"/> is scaled.</param>
    /// <param name="b">The <see cref="Unhandled3"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Unhandled3"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Unhandled3 Multiply(Scalar a, Unhandled3 b) => a.Multiply(b);

    /// <summary>Scales the provided <see cref="Unhandled4"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Scalar"/> by which the <see cref="Unhandled4"/> is scaled.</param>
    /// <param name="b">The <see cref="Unhandled4"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Unhandled4"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Unhandled4 Multiply(Scalar a, Unhandled4 b) => a.Multiply(b);

    /// <summary>Scales the provided <typeparamref name="TScalar"/> by the provided <see cref="Scalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity that is scaled by the <see cref="Scalar"/>.</typeparam>
    /// <param name="x">The <see cref="Scalar"/> by which the <typeparamref name="TScalar"/> is scaled.</param>
    /// <param name="y">The <typeparamref name="TScalar"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <typeparamref name="TScalar"/>, { <paramref name="x"/> ∙ <paramref name="y"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static TScalar Multiply<TScalar>(Scalar x, TScalar y) where TScalar : IScalarQuantity<TScalar>
    {
        ArgumentNullException.ThrowIfNull(y);

        return x.Multiply(y);
    }

    /// <summary>Scales the provided <typeparamref name="TVector"/> by the provided <see cref="Scalar"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is scaled by the <see cref="Scalar"/>.</typeparam>
    /// <param name="a">The <see cref="Scalar"/> by which the <typeparamref name="TVector"/> is scaled.</param>
    /// <param name="b">The <typeparamref name="TVector"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply2{TVector}(Scalar, TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The scaled <typeparamref name="TVector"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static TVector Multiply<TVector>(Scalar a, IVector2Quantity<TVector> b) where TVector : IVector2Quantity<TVector>
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Multiply(b);
    }

    /// <summary>Scales the provided <typeparamref name="TVector"/> by the provided <see cref="Scalar"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is scaled by the <see cref="Scalar"/>.</typeparam>
    /// <param name="a">The <see cref="Scalar"/> by which the <typeparamref name="TVector"/> is scaled.</param>
    /// <param name="b">The <typeparamref name="TVector"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply3{TVector}(Scalar, TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The scaled <typeparamref name="TVector"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static TVector Multiply<TVector>(Scalar a, IVector3Quantity<TVector> b) where TVector : IVector3Quantity<TVector>
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Multiply(b);
    }

    /// <summary>Scales the provided <typeparamref name="TVector"/> by the provided <see cref="Scalar"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is scaled by the <see cref="Scalar"/>.</typeparam>
    /// <param name="a">The <see cref="Scalar"/> by which the <typeparamref name="TVector"/> is scaled.</param>
    /// <param name="b">The <typeparamref name="TVector"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply4{TVector}(Scalar, TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The scaled <typeparamref name="TVector"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static TVector Multiply<TVector>(Scalar a, IVector4Quantity<TVector> b) where TVector : IVector4Quantity<TVector>
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Multiply(b);
    }

    /// <summary>Scales the provided <typeparamref name="TVector"/> by the provided <see cref="Scalar"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is scaled by the <see cref="Scalar"/>.</typeparam>
    /// <param name="a">The <see cref="Scalar"/> by which the <typeparamref name="TVector"/> is scaled.</param>
    /// <param name="b">The <typeparamref name="TVector"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <typeparamref name="TVector"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static TVector Multiply2<TVector>(Scalar a, TVector b) where TVector : IVector2Quantity<TVector>
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Multiply2(b);
    }

    /// <summary>Scales the provided <typeparamref name="TVector"/> by the provided <see cref="Scalar"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is scaled by the <see cref="Scalar"/>.</typeparam>
    /// <param name="a">The <see cref="Scalar"/> by which the <typeparamref name="TVector"/> is scaled.</param>
    /// <param name="b">The <typeparamref name="TVector"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <typeparamref name="TVector"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static TVector Multiply3<TVector>(Scalar a, TVector b) where TVector : IVector3Quantity<TVector>
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Multiply3(b);
    }

    /// <summary>Scales the provided <typeparamref name="TVector"/> by the provided <see cref="Scalar"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is scaled by the <see cref="Scalar"/>.</typeparam>
    /// <param name="a">The <see cref="Scalar"/> by which the <typeparamref name="TVector"/> is scaled.</param>
    /// <param name="b">The <typeparamref name="TVector"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <typeparamref name="TVector"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static TVector Multiply4<TVector>(Scalar a, TVector b) where TVector : IVector4Quantity<TVector>
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Multiply4(b);
    }

    /// <summary>Computes the quotient of the provided <see cref="Scalar"/>.</summary>
    /// <param name="x">The first <see cref="Scalar"/>, which is divided by the second <see cref="Scalar"/>.</param>
    /// <param name="y">The second <see cref="Scalar"/>, by which the first <see cref="Scalar"/> is divided.</param>
    /// <returns>The quotient of the <see cref="Scalar"/>, { <paramref name="x"/> / <paramref name="y"/> }.</returns>
    public static Scalar Divide(Scalar x, Scalar y) => x.DivideBy(y);

    /// <summary>Scales the reciprocal of the provided <see cref="Unhandled"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="x">The <see cref="Scalar"/> by which the reciprocal of the <see cref="Unhandled"/> is scaled.</param>
    /// <param name="y">The <see cref="Unhandled"/>, the reciprocal of which is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Unhandled"/>, { <paramref name="x"/> / <paramref name="y"/> }.</returns>
    public static Unhandled Divide(Scalar x, Unhandled y) => x.DivideBy(y);

    /// <summary>Determines whether a <see cref="Scalar"/>, <paramref name="lhs"/>, represents a smaller magnitude than another <see cref="Scalar"/>, <paramref name="rhs"/>.</summary>
    /// <param name="lhs">The first <see cref="Scalar"/>, assumed to represent a smaller magnitude than the other <see cref="Scalar"/>.</param>
    /// <param name="rhs">The second <see cref="Scalar"/>, assumed to represent a greater magntiude than the other <see cref="Scalar"/>.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.operator &lt;(double, double)"/>.</remarks>
    /// <returns>A <see cref="bool"/> representing the truthfulness of { <paramref name="lhs"/> &lt; <paramref name="rhs"/> }.</returns>
    public static bool operator <(Scalar lhs, Scalar rhs) => lhs.Value < rhs.Value;

    /// <summary>Determines whether a <see cref="Scalar"/>, <paramref name="lhs"/>, represents a greater magnitude than another <see cref="Scalar"/>, <paramref name="rhs"/>.</summary>
    /// <param name="lhs">The first <see cref="Scalar"/>, assumed to represent a greater magnitude than the other <see cref="Scalar"/>.</param>
    /// <param name="rhs">The second <see cref="Scalar"/>, assumed to represent a smaller magntiude than the other <see cref="Scalar"/>.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.operator &gt;(double, double)"/>.</remarks>
    /// <returns>A <see cref="bool"/> representing the truthfulness of { <paramref name="lhs"/> &gt; <paramref name="rhs"/> }.</returns>
    public static bool operator >(Scalar lhs, Scalar rhs) => lhs.Value > rhs.Value;

    /// <summary>Determines whether a <see cref="Scalar"/>, <paramref name="lhs"/>, represents a smaller or equivalent magnitude compared to another <see cref="Scalar"/>, <paramref name="rhs"/>.</summary>
    /// <param name="lhs">The first <see cref="Scalar"/>, assumed to represent a smaller or equivalent magnitude compared to the other <see cref="Scalar"/>.</param>
    /// <param name="rhs">The second <see cref="Scalar"/>, assumed to represent a greater or equivalent magnitude compared to the other <see cref="Scalar"/>.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.operator &lt;=(double, double)"/>.</remarks>
    /// <returns>A <see cref="bool"/> representing the truthfulness of { <paramref name="lhs"/> ≤ <paramref name="rhs"/> }.</returns>
    public static bool operator <=(Scalar lhs, Scalar rhs) => lhs.Value <= rhs.Value;

    /// <summary>Determines whether a <see cref="Scalar"/>, <paramref name="lhs"/>, represents a greater or equivalent magnitude compared to another <see cref="Scalar"/>, <paramref name="rhs"/>.</summary>
    /// <param name="lhs">The first <see cref="Scalar"/>, assumed to represent a greater or equivalent magnitude compared to the other <see cref="Scalar"/>.</param>
    /// <param name="rhs">The second <see cref="Scalar"/>, assumed to represent a smaller or equivalent magnitude compared to the other <see cref="Scalar"/>.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.operator &gt;=(double, double)"/>.</remarks>
    /// <returns>A <see cref="bool"/> representing the truthfulness of { <paramref name="lhs"/> ≥ <paramref name="rhs"/> }.</returns>
    public static bool operator >=(Scalar lhs, Scalar rhs) => lhs.Value >= rhs.Value;

    /// <summary>Applies the unary plus to the provided <see cref="Scalar"/>.</summary>
    /// <param name="x">The <see cref="Scalar"/> to which the unary plus is applied.</param>
    /// <returns>The same <see cref="Scalar"/>, { <paramref name="x"/> }.</returns>
    public static Scalar operator +(Scalar x) => x.Plus();

    /// <summary>Negates the provided <see cref="Scalar"/>.</summary>
    /// <param name="x">The <see cref="Scalar"/> that is negated.</param>
    /// <returns>The negated <see cref="Scalar"/>, { -<paramref name="x"/> }.</returns>
    public static Scalar operator -(Scalar x) => x.Negate();

    /// <inheritdoc cref="Add(Scalar, Scalar)"/>
    public static Scalar operator +(Scalar x, Scalar y) => Add(x, y);

    /// <inheritdoc cref="Subtract(Scalar, Scalar)"/>
    public static Scalar operator -(Scalar x, Scalar y) => Subtract(x, y);

    /// <inheritdoc cref="Remainder(Scalar, Scalar)"/>
    public static Scalar operator %(Scalar x, Scalar y) => Remainder(x, y);

    /// <inheritdoc cref="Multiply(Scalar, Scalar)"/>
    public static Scalar operator *(Scalar x, Scalar y) => Multiply(x, y);

    /// <inheritdoc cref="Divide(Scalar, Scalar)"/>
    public static Scalar operator /(Scalar x, Scalar y) => Divide(x, y);

    /// <summary>Computes the element-wise remainder from division of the provided <see cref="Vector2"/>-tuple by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector2"/>-tuple that is divided by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, by which the <see cref="Vector2"/>-tuple is divided.</param>
    /// <returns>The element-wise remainder from division of the <see cref="Vector2"/> by the <see cref="Scalar"/>, { <paramref name="a"/> % <paramref name="b"/> }.</returns>
    public static Vector2 operator %((Scalar, Scalar) a, Scalar b) => Vector2.Remainder(a, b);

    /// <summary>Scales the provided <see cref="Vector2"/>-tuple by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Scalar"/>, by which the <see cref="Vector2"/>-tuple is scaled.</param>
    /// <param name="b">The <see cref="Vector2"/>-tuple that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Vector2"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Vector2 operator *(Scalar a, (Scalar, Scalar) b) => Multiply(a, b);

    /// <summary>Scales the provided <see cref="Vector2"/>-tuple by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector2"/>-tuple that is scaled by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, by which the <see cref="Vector2"/>-tuple is scaled.</param>
    /// <returns>The scaled <see cref="Vector2"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Vector2 operator *((Scalar, Scalar) a, Scalar b) => Multiply(b, a);

    /// <summary>Scales the provided <see cref="Vector2"/>-tuple by the reciprocal of the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector2"/>-tuple that is scaled by the reciprocal of the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, the reciprocal of which scales the <see cref="Vector2"/>-tuple.</param>
    /// <returns>The scaled <see cref="Vector2"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    public static Vector2 operator /((Scalar, Scalar) a, Scalar b) => Vector2.Divide(a, b);

    /// <summary>Computes the element-wise remainder from division of the provided <see cref="Vector3"/>-tuple by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector3"/>-tuple that is divided by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, by which the <see cref="Vector3"/>-tuple is divided.</param>
    /// <returns>The element-wise remainder from division of the <see cref="Vector3"/> by the <see cref="Scalar"/>, { <paramref name="a"/> % <paramref name="b"/> }.</returns>
    public static Vector3 operator %((Scalar, Scalar, Scalar) a, Scalar b) => Vector3.Remainder(a, b);

    /// <summary>Scales the provided <see cref="Vector3"/>-tuple by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Scalar"/>, by which the <see cref="Vector3"/>-tuple is scaled.</param>
    /// <param name="b">The <see cref="Vector3"/>-tuple that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Vector3"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Vector3 operator *(Scalar a, (Scalar, Scalar, Scalar) b) => Multiply(a, b);

    /// <summary>Scales the provided <see cref="Vector3"/>-tuple by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector3"/>-tuple that is scaled by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, by which the <see cref="Vector3"/>-tuple is scaled.</param>
    /// <returns>The scaled <see cref="Vector3"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Vector3 operator *((Scalar, Scalar, Scalar) a, Scalar b) => Multiply(b, a);

    /// <summary>Scales the provided <see cref="Vector3"/>-tuple by the reciprocal of the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector3"/>-tuple that is scaled by the reciprocal of the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, the reciprocal of which scales the <see cref="Vector3"/>-tuple.</param>
    /// <returns>The scaled <see cref="Vector3"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    public static Vector3 operator /((Scalar, Scalar, Scalar) a, Scalar b) => Vector3.Divide(a, b);

    /// <summary>Computes the element-wise remainder from division of the provided <see cref="Vector4"/>-tuple by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector4"/>-tuple that is divided by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, by which the <see cref="Vector4"/>-tuple is divided.</param>
    /// <returns>The element-wise remainder from division of the <see cref="Vector4"/> by the <see cref="Scalar"/>, { <paramref name="a"/> % <paramref name="b"/> }.</returns>
    public static Vector4 operator %((Scalar, Scalar, Scalar, Scalar) a, Scalar b) => Vector4.Remainder(a, b);

    /// <summary>Scales the provided <see cref="Vector4"/>-tuple by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Scalar"/>, by which the <see cref="Vector4"/>-tuple is scaled.</param>
    /// <param name="b">The <see cref="Vector4"/>-tuple that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Vector4"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Vector4 operator *(Scalar a, (Scalar, Scalar, Scalar, Scalar) b) => Multiply(a, b);

    /// <summary>Scales the provided <see cref="Vector4"/>-tuple by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector4"/>-tuple that is scaled by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, by which the <see cref="Vector4"/>-tuple is scaled.</param>
    /// <returns>The scaled <see cref="Vector4"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Vector4 operator *((Scalar, Scalar, Scalar, Scalar) a, Scalar b) => Multiply(b, a);

    /// <summary>Scales the provided <see cref="Vector4"/>-tuple by the reciprocal of the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector4"/>-tuple that is scaled by the reciprocal of the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, the reciprocal of which scales the <see cref="Vector4"/>-tuple.</param>
    /// <returns>The scaled <see cref="Vector4"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    public static Vector4 operator /((Scalar, Scalar, Scalar, Scalar) a, Scalar b) => Vector4.Divide(a, b);

    /// <summary>Constructs a <see cref="Scalar"/>, representing the provided <see cref="double"/> magnitude.</summary>
    /// <param name="value">The magnitude represented by the constructed <see cref="Scalar"/>.</param>
    /// <returns>The constructed <see cref="Scalar"/>.</returns>
    public static Scalar FromDouble(double value) => new(value);

    /// <summary>Converts the <see cref="Scalar"/> to the <see cref="double"/> representing the same magnitude.</summary>
    /// <returns>The <see cref="double"/>, equivalent to the magnitude of the <see cref="Scalar"/>.</returns>
    public double ToDouble() => Value;

    /// <summary>Constructs a <see cref="Scalar"/>, representing the provided <see cref="double"/> magnitude.</summary>
    /// <param name="value">The magnitude represented by the constructed <see cref="Scalar"/>.</param>
    /// <returns>The constructed <see cref="Scalar"/>.</returns>
    public static implicit operator Scalar(double value) => FromDouble(value);

    /// <summary>Converts the provided <see cref="Scalar"/> to the <see cref="double"/> representing the same magnitude.</summary>
    /// <param name="scalar">The <see cref="Scalar"/> that is converted to a <see cref="double"/>.</param>
    /// <returns>The <see cref="double"/>, equivalent to the magnitude of the provided <see cref="Scalar"/>.</returns>
    public static implicit operator double(Scalar scalar) => scalar.ToDouble();
}
