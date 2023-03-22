namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

/// <summary>A measure of some scalar quantity not covered by a designated type.</summary>
public readonly record struct Unhandled : IScalarQuantity<Unhandled>, IComparable<Unhandled>, IFormattable
{
    /// <summary>The <see cref="Unhandled"/> representing { 0 }.</summary>
    public static Unhandled Zero { get; } = new(Scalar.Zero);

    /// <summary>The <see cref="Unhandled"/> representing { <see cref="double.NaN"/> }.</summary>
    public static Unhandled NaN { get; } = new(Scalar.NaN);

    /// <summary>The <see cref="Unhandled"/> representing { ∞ }.</summary>
    public static Unhandled PositiveInfinity { get; } = new(Scalar.PositiveInfinity);

    /// <summary>The <see cref="Unhandled"/> representing { -∞ }.</summary>
    public static Unhandled NegativeInfinity { get; } = new(Scalar.NegativeInfinity);

    /// <summary>The magnitude of the <see cref="Unhandled"/>.</summary>
    public Scalar Magnitude { get; }

    /// <summary>Instantiates an <see cref="Unhandled"/>, representing a measure of some scalar quantity not covered by a designated type.</summary>
    /// <param name="magnitude">The magnitude of the constructed <see cref="Unhandled"/>.</param>
    public Unhandled(Scalar magnitude)
    {
        Magnitude = magnitude;
    }

    static Unhandled IScalarQuantity<Unhandled>.WithMagnitude(Scalar magnitude) => new(magnitude);

    /// <summary>Converts the <see cref="Unhandled"/> to a scalar quantity of type <typeparamref name="TScalar"/>, representing the same magnitude.</summary>
    /// <typeparam name="TScalar">The scalar quantity that the <see cref="Unhandled"/> is converted to.</typeparam>
    /// <returns>The constructed <typeparamref name="TScalar"/>, representing the same magnitude as the <see cref="Unhandled"/>.</returns>
    public TScalar As<TScalar>() where TScalar : IScalarQuantity<TScalar> => TScalar.WithMagnitude(Magnitude);

    /// <summary>Indicates whether the <see cref="Unhandled"/> represents { <see cref="Scalar.NaN"/> }.</summary>
    public bool IsNaN => Magnitude.IsNaN;

    /// <summary>Indicates whether the <see cref="Unhandled"/> represents { 0 }.</summary>
    public bool IsZero => Magnitude.IsZero;

    /// <summary>Indicates whether the <see cref="Unhandled"/> represents a value greater than or equal to { 0 }.</summary>
    public bool IsPositive => Magnitude.IsPositive;

    /// <summary>Indicates whether the <see cref="Unhandled"/> represents a value smaller than { 0 }.</summary>
    public bool IsNegative => Magnitude.IsNegative;

    /// <summary>Indicates whether the <see cref="Unhandled"/> represents a finite value.</summary>
    public bool IsFinite => Magnitude.IsFinite;

    /// <summary>Indicates whether the <see cref="Unhandled"/> represents an infinite value.</summary>
    public bool IsInfinite => Magnitude.IsInfinite;

    /// <summary>Indicates whether the <see cref="Unhandled"/> represents { <see cref="Scalar.PositiveInfinity"/> }.</summary>
    public bool IsPositiveInfinity => Magnitude.IsPositiveInfinity;

    /// <summary>Indicates whether the <see cref="Unhandled"/> represents { <see cref="Scalar.NegativeInfinity"/> }.</summary>
    public bool IsNegativeInfinity => Magnitude.IsNegativeInfinity;

    /// <summary>Computes the absolute value of the <see cref="Unhandled"/> - resulting in an <see cref="Unhandled"/> representing a value of the same magnitude, but always positive.</summary>
    /// <returns>The absolute value of the <see cref="Unhandled"/>.</returns>
    public Unhandled Absolute() => new(Magnitude.Absolute());

    /// <summary>Computes the sign of the <see cref="Unhandled"/>, resulting in:
    /// <list type="bullet">
    /// <item><term>1</term><description> The magnitude is positive, but not { 0 }.</description></item>
    /// <item><term>0</term><description> The magnitude is { 0 }.</description></item>
    /// <item><term>-1</term><description> The magnitude is negative.</description></item>
    /// </list></summary>
    /// <returns>One of the <see cref="int"/> values { 1, 0, -1 }, as detailed above.</returns>
    /// <remarks>An <see cref="ArithmeticException"/> will be thrown if the <see cref="Unhandled"/> represents { <see cref="Scalar.NaN"/> }.</remarks>
    /// <exception cref="ArithmeticException"/>
    public int Sign() => Magnitude.Sign();

    /// <summary>Computes the <see cref="Unhandled"/> raised to the provided power.</summary>
    /// <param name="exponent">The <see cref="Scalar"/> representing the exponent.</param>
    /// <returns>The <see cref="Unhandled"/> raised to the provided power, { <see langword="this"/> ^ <paramref name="exponent"/> }.</returns>
    public Unhandled Power(Scalar exponent) => new(Magnitude.Power(exponent));

    /// <summary>Computes the reciprocal, or multiplicative inverse, of the <see cref="Unhandled"/>.</summary>
    /// <returns>The reciprocal of the <see cref="Unhandled"/>, { 1 / <see langword="this"/> }.</returns>
    public Unhandled Reciprocal() => new(Magnitude.Reciprocal());

    /// <summary>Computes the square of the <see cref="Unhandled"/>.</summary>
    /// <returns>The square of the <see cref="Unhandled"/>, { <see langword="this"/> ² }.</returns>
    public Unhandled Square() => new(Magnitude.Square());

    /// <summary>Computes the cube of the <see cref="Unhandled"/>.</summary>
    /// <returns>The cube of the <see cref="Unhandled"/>, { <see langword="this"/> ³ }.</returns>
    public Unhandled Cube() => new(Magnitude.Cube());

    /// <summary>Computes the square root of the <see cref="Unhandled"/>.</summary>
    /// <returns>The square root of the <see cref="Unhandled"/>, { √ <see langword="this"/> }.</returns>
    public Unhandled SquareRoot() => new(Magnitude.SquareRoot());

    /// <summary>Computes the cube root of the <see cref="Unhandled"/>.</summary>
    /// <returns>The cube root of the <see cref="Unhandled"/>, { √ <see langword="this"/> }.</returns>
    public Unhandled CubeRoot() => new(Magnitude.CubeRoot());

    /// <summary>Compares the <see cref="Unhandled"/> to another, provided, <see cref="Unhandled"/>, resulting in:
    /// <list type="bullet">
    /// <item><term>1</term><description> The <see cref="Unhandled"/> represents a larger magnitude than does the provided <see cref="Unhandled"/>.</description></item>
    /// <item><term>0</term><description> The <see cref="Unhandled"/> and the provided <see cref="Unhandled"/> represent the same magnitude.</description></item>
    /// <item><term>-1</term><description> The <see cref="Unhandled"/> represents a smaller magnitude than does the provided <see cref="Unhandled"/>.</description></item>
    /// </list><para>The value { <see cref="NaN"/> } represents the smallest possible value.</para></summary>
    /// <param name="other">The <see cref="Unhandled"/> to which the original <see cref="Unhandled"/> is compared.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.CompareTo(double)"/>.</remarks>
    /// <returns>One of the <see cref="int"/> values { 1, 0, -1 }, as detailed above.</returns>
    public int CompareTo(Unhandled other) => Magnitude.CompareTo(other.Magnitude);

    /// <summary>Determines whether the <see cref="Unhandled"/> is equivalent to another, provided, <see cref="Unhandled"/>.</summary>
    /// <param name="other">The <see cref="Unhandled"/> to which the original <see cref="Unhandled"/> is compared.</param>
    /// <remarks>The values { <see cref="NaN"/> }, { <see cref="PositiveInfinity"/> }, and {<see cref="NegativeInfinity"/> } are all considered equivalent to themselves.</remarks>
    /// <returns>A <see cref="bool"/> indicating whether the two <see cref="Unhandled"/> are equivalent.</returns>
    public bool Equals(Unhandled other) => Magnitude.Equals(other.Magnitude);

    /// <summary>Determines whether the provided <see cref="Unhandled"/> are equivalent.</summary>
    /// <param name="lhs">The first of the two <see cref="Unhandled"/> that are compared.</param>
    /// <param name="rhs">The second of the two <see cref="Unhandled"/> that are compared.</param>
    /// <remarks>The values { <see cref="NaN"/> }, { <see cref="PositiveInfinity"/> }, and {<see cref="NegativeInfinity"/> } are all considered equivalent to themselves.</remarks>
    /// <returns>A <see cref="bool"/> indicating whether the two <see cref="Unhandled"/> are equivalent.</returns>
    public static bool Equals(Unhandled lhs, Unhandled rhs) => lhs.Equals(rhs);

    /// <summary>Computes the <see cref="int"/> hash code describing the <see cref="Unhandled"/>.</summary>
    /// <returns>The <see cref="int"/> hash code describing the <see cref="Unhandled"/>.</returns>
    public override int GetHashCode() => Magnitude.GetHashCode();

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Unhandled"/>, formatted according to the <see cref="CultureInfo.CurrentCulture"/>.</summary>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString()"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Unhandled"/>.</returns>
    public override string ToString() => ToString(CultureInfo.CurrentCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Unhandled"/>, formatted according to the provided <see cref="IFormatProvider"/>.</summary>
    /// <param name="formatProvider">The <see cref="IFormatProvider"/>, providing culture-specific formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString(IFormatProvider?)"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Unhandled"/>.</returns>
    public string ToString(IFormatProvider? formatProvider) => ToString("G", formatProvider);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Unhandled"/>, formatted according to the provided <see cref="string"/> and the <see cref="CultureInfo.CurrentCulture"/>.</summary>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString(string?)"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Unhandled"/>.</returns>
    public string ToString(string? format) => ToString(format, CultureInfo.CurrentCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Unhandled"/>, formatted according to the provided <see cref="string"/> and <see cref="IFormatProvider"/>.</summary>
    /// <param name="formatProvider">The <see cref="IFormatProvider"/>, providing culture-specific formatting information.</param>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString(string?, IFormatProvider?)"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Unhandled"/>.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider) => Magnitude.ToString(format, formatProvider);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Unhandled"/>, formatted according to the <see cref="CultureInfo.InvariantCulture"/>.</summary>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString(IFormatProvider)"/>, with the <see cref="CultureInfo.InvariantCulture"/> as the provider.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Unhandled"/>.</returns>
    public string ToStringInvariant() => ToString(CultureInfo.InvariantCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Unhandled"/>, formatted according to the provided <see cref="string"/> and the <see cref="CultureInfo.InvariantCulture"/>.</summary>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString(string?, IFormatProvider)"/>, with the <see cref="CultureInfo.InvariantCulture"/> as the provider.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Unhandled"/>.</returns>
    public string ToStringInvariant(string? format) => ToString(format, CultureInfo.InvariantCulture);

    /// <summary>Applies the unary plus to the <see cref="Unhandled"/>.</summary>
    /// <returns>The same <see cref="Unhandled"/>, { <see langword="this"/> }.</returns>
    public Unhandled Plus() => this;

    /// <summary>Negates the <see cref="Unhandled"/>.</summary>
    /// <returns>The negated <see cref="Unhandled"/>, { -<see langword="this"/> }.</returns>
    public Unhandled Negate() => new(-Magnitude);

    /// <summary>Computes the sum of the <see cref="Unhandled"/> and the provided <typeparamref name="TScalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity that is added to the <see cref="Unhandled"/>.</typeparam>
    /// <param name="addend">The <typeparamref name="TScalar"/> that is added to the <see cref="Unhandled"/>.</param>
    /// <returns>The sum of the <see cref="Unhandled"/> and <typeparamref name="TScalar"/>, { <see langword="this"/> + <paramref name="addend"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled Add<TScalar>(TScalar addend) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(addend);

        return new(Magnitude + addend.Magnitude);
    }

    /// <summary>Computes the difference between the <see cref="Unhandled"/> and the provided <typeparamref name="TScalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity for which the difference to the <see cref="Unhandled"/> is computed.</typeparam>
    /// <param name="other">The <typeparamref name="TScalar"/>, for which the difference to the <see cref="Unhandled"/> is computed.</param>
    /// <returns>The difference between the <see cref="Unhandled"/> and the <typeparamref name="TScalar"/>, { |<see langword="this"/> - <paramref name="other"/>| }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled Difference<TScalar>(TScalar other) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(other);

        return new(Math.Abs(Magnitude - other.Magnitude));
    }

    /// <summary>Computes the signed difference between the <see cref="Unhandled"/> and the provided <typeparamref name="TScalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity that is subtracted from the <see cref="Unhandled"/>.</typeparam>
    /// <param name="subtrahend">The <typeparamref name="TScalar"/> that is subtracted from the <see cref="Unhandled"/>.</param>
    /// <remarks>The resulting <see cref="Unhandled"/> may be negative. If this is not desired, use <see cref="Difference{TScalar}(TScalar)"/>.</remarks>
    /// <returns>The signed difference between the <see cref="Unhandled"/> and the <typeparamref name="TScalar"/>, { <see langword="this"/> - <paramref name="subtrahend"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled Subtract<TScalar>(TScalar subtrahend) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(subtrahend);

        return new(Magnitude - subtrahend.Magnitude);
    }

    /// <summary>Scales the <see cref="Unhandled"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="factor">The <see cref="Scalar"/> by which the <see cref="Unhandled"/> is scaled.</param>
    /// <returns>The scaled <see cref="Unhandled"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Unhandled Multiply(Scalar factor) => Multiply<Scalar>(factor);

    /// <summary>Scales the <see cref="Unhandled"/> by the provided <see cref="Vector2"/>.</summary>
    /// <param name="factor">The <see cref="Vector2"/> by which the <see cref="Unhandled"/> is scaled.</param>
    /// <returns>The scaled <see cref="Unhandled"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Unhandled2 Multiply(Vector2 factor) => Multiply2(factor);

    /// <summary>Scales the <see cref="Unhandled"/> by the provided <see cref="Vector3"/>.</summary>
    /// <param name="factor">The <see cref="Vector3"/> by which the <see cref="Unhandled"/> is scaled.</param>
    /// <returns>The scaled <see cref="Unhandled"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Unhandled3 Multiply(Vector3 factor) => Multiply3(factor);

    /// <summary>Scales the <see cref="Unhandled"/> by the provided <see cref="Vector4"/>.</summary>
    /// <param name="factor">The <see cref="Vector4"/> by which the <see cref="Unhandled"/> is scaled.</param>
    /// <returns>The scaled <see cref="Unhandled"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Unhandled4 Multiply(Vector4 factor) => Multiply4(factor);

    /// <summary>Computes the product of the <see cref="Unhandled"/> and the provided <see cref="Unhandled2"/>.</summary>
    /// <param name="factor">The <see cref="Unhandled2"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled2"/> and <see cref="Unhandled"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Unhandled2 Multiply(Unhandled2 factor) => Multiply2(factor);

    /// <summary>Computes the product of the <see cref="Unhandled"/> and the provided <see cref="Unhandled3"/>.</summary>
    /// <param name="factor">The <see cref="Unhandled3"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled3"/> and <see cref="Unhandled"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Unhandled3 Multiply(Unhandled3 factor) => Multiply3(factor);

    /// <summary>Computes the product of the <see cref="Unhandled"/> and the provided <see cref="Unhandled4"/>.</summary>
    /// <param name="factor">The <see cref="Unhandled4"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled4"/> and <see cref="Unhandled"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Unhandled4 Multiply(Unhandled4 factor) => Multiply4(factor);

    /// <summary>Computes the product of the <see cref="Unhandled"/> and the provided <typeparamref name="TScalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity by which the <see cref="Unhandled"/> is multiplied.</typeparam>
    /// <param name="factor">The <typeparamref name="TScalar"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled"/> and <typeparamref name="TScalar"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled Multiply<TScalar>(TScalar factor) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(factor);

        return new(Magnitude * factor.Magnitude);
    }

    /// <summary>Computes the product of the <see cref="Unhandled"/> and the provided <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity by which the <see cref="Unhandled"/> is multiplied.</typeparam>
    /// <param name="factor">The <typeparamref name="TVector"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply2{TVector}(TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The product of the <see cref="Unhandled"/> and <typeparamref name="TVector"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled2 Multiply<TVector>(IVector2Quantity<TVector> factor) where TVector : IVector2Quantity<TVector>
    {
        ArgumentNullException.ThrowIfNull(factor);

        return new(Magnitude * factor.X, Magnitude * factor.Y);
    }

    /// <summary>Computes the product of the <see cref="Unhandled"/> and the provided <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity by which the <see cref="Unhandled"/> is multiplied.</typeparam>
    /// <param name="factor">The <typeparamref name="TVector"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply3{TVector}(TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The product of the <see cref="Unhandled"/> and <typeparamref name="TVector"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled3 Multiply<TVector>(IVector3Quantity<TVector> factor) where TVector : IVector3Quantity<TVector>
    {
        ArgumentNullException.ThrowIfNull(factor);

        return new(Magnitude * factor.X, Magnitude * factor.Y, Magnitude * factor.Z);
    }

    /// <summary>Computes the product of the <see cref="Unhandled"/> and the provided <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity by which the <see cref="Unhandled"/> is multiplied.</typeparam>
    /// <param name="factor">The <typeparamref name="TVector"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply4{TVector}(TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The product of the <see cref="Unhandled"/> and <typeparamref name="TVector"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled4 Multiply<TVector>(IVector4Quantity<TVector> factor) where TVector : IVector4Quantity<TVector>
    {
        ArgumentNullException.ThrowIfNull(factor);

        return new(Magnitude * factor.X, Magnitude * factor.Y, Magnitude * factor.Z, Magnitude * factor.W);
    }

    /// <summary>Computes the product of the <see cref="Unhandled"/> and the provided <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity by which the <see cref="Unhandled"/> is multiplied.</typeparam>
    /// <param name="factor">The <typeparamref name="TVector"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled"/> and <typeparamref name="TVector"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled2 Multiply2<TVector>(TVector factor) where TVector : IVector2Quantity
    {
        ArgumentNullException.ThrowIfNull(factor);

        return new(Magnitude * factor.X, Magnitude * factor.Y);
    }

    /// <summary>Computes the product of the <see cref="Unhandled"/> and the provided <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity by which the <see cref="Unhandled"/> is multiplied.</typeparam>
    /// <param name="factor">The <typeparamref name="TVector"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled"/> and <typeparamref name="TVector"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled3 Multiply3<TVector>(TVector factor) where TVector : IVector3Quantity
    {
        ArgumentNullException.ThrowIfNull(factor);

        return new(Magnitude * factor.X, Magnitude * factor.Y, Magnitude * factor.Z);
    }

    /// <summary>Computes the product of the <see cref="Unhandled"/> and the provided <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity by which the <see cref="Unhandled"/> is multiplied.</typeparam>
    /// <param name="factor">The <typeparamref name="TVector"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled"/> and <typeparamref name="TVector"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled4 Multiply4<TVector>(TVector factor) where TVector : IVector4Quantity
    {
        ArgumentNullException.ThrowIfNull(factor);

        return new(Magnitude * factor.X, Magnitude * factor.Y, Magnitude * factor.Z, Magnitude * factor.W);
    }

    /// <summary>Scales the <see cref="Unhandled"/> by the reciprocal of the provided <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Scalar"/>, the reciprocal of which scales the <see cref="Unhandled"/></param>
    /// <returns>The scaled <see cref="Unhandled"/>, { <see langword="this"/> / <paramref name="divisor"/> }.</returns>
    public Unhandled DivideBy(Scalar divisor) => DivideBy<Scalar>(divisor);

    /// <summary>Computes the quotient of the <see cref="Unhandled"/> and the provided <typeparamref name="TScalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity by which the <see cref="Unhandled"/> is divided.</typeparam>
    /// <param name="divisor">The <typeparamref name="TScalar"/> by which the <see cref="Unhandled"/> is divided.</param>
    /// <returns>The quotient of the <see cref="Unhandled"/> and the <typeparamref name="TScalar"/>, { <see langword="this"/> / <paramref name="divisor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled DivideBy<TScalar>(TScalar divisor) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(divisor);

        return new(Magnitude / divisor.Magnitude);
    }

    /// <summary>Computes the sum of the provided <see cref="Unhandled"/> and <typeparamref name="TScalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity that is added to the <see cref="Unhandled"/>.</typeparam>
    /// <param name="x">The <see cref="Unhandled"/> to which the <typeparamref name="TScalar"/> is added.</param>
    /// <param name="y">The <typeparamref name="TScalar"/> that is added to the <see cref="Unhandled"/>.</param>
    /// <returns>The sum of the <see cref="Unhandled"/> and <typeparamref name="TScalar"/>, { <paramref name="x"/> + <paramref name="y"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled Add<TScalar>(Unhandled x, TScalar y) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(y);

        return x.Add(y);
    }

    /// <summary>Computes the difference between the provided <see cref="Unhandled"/> and <typeparamref name="TScalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity for which the difference to the <see cref="Unhandled"/> is computed.</typeparam>
    /// <param name="x">The <see cref="Unhandled"/>, for which the difference to the <typeparamref name="TScalar"/> is computed.</param>
    /// <param name="y">The <typeparamref name="TScalar"/>, for which the difference to the <see cref="Unhandled"/> is computed.</param>
    /// <returns>The difference between the <see cref="Unhandled"/> and the <typeparamref name="TScalar"/>, { |<paramref name="x"/> - <paramref name="y"/>| }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled Difference<TScalar>(Unhandled x, TScalar y) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(y);

        return x.Difference(y);
    }

    /// <summary>Computes the signed difference between the provided <see cref="Unhandled"/> and <typeparamref name="TScalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity for which the difference to the <see cref="Unhandled"/> is computed.</typeparam>
    /// <param name="x">The <see cref="Unhandled"/>, for which the difference to the <typeparamref name="TScalar"/> is computed.</param>
    /// <param name="y">The <typeparamref name="TScalar"/>, for which the difference to the <see cref="Unhandled"/> is computed.</param>
    /// <remarks>The resulting <see cref="Unhandled"/> may be negative. If this is not desired, use <see cref="Difference{TScalar}(Unhandled, TScalar)"/>.</remarks>
    /// <returns>The signed difference between the <see cref="Unhandled"/> and the <typeparamref name="TScalar"/>, { |<paramref name="x"/> - <paramref name="y"/>| }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled Subtract<TScalar>(Unhandled x, TScalar y) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(y);

        return x.Subtract(y);
    }

    /// <summary>Scales the provided <see cref="Unhandled"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="x">The <see cref="Unhandled"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <param name="y">The <see cref="Scalar"/> by which the <see cref="Unhandled"/> is scaled.</param>
    /// <returns>The scaled <see cref="Unhandled"/>, { <paramref name="x"/> ∙ <paramref name="y"/> }.</returns>
    public static Unhandled Multiply(Unhandled x, Scalar y) => x.Multiply(y);

    /// <summary>Scales the provided <see cref="Unhandled"/> by the provided <see cref="Vector2"/>.</summary>
    /// <param name="a">The <see cref="Unhandled"/> that is scaled by the <see cref="Vector2"/>.</param>
    /// <param name="b">The <see cref="Vector2"/> by which the <see cref="Unhandled"/> is scaled.</param>
    /// <returns>The scaled <see cref="Unhandled2"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Unhandled2 Multiply(Unhandled a, Vector2 b) => a.Multiply(b);

    /// <summary>Scales the provided <see cref="Unhandled"/> by the provided <see cref="Vector3"/>.</summary>
    /// <param name="a">The <see cref="Unhandled"/> that is scaled by the <see cref="Vector3"/>.</param>
    /// <param name="b">The <see cref="Vector3"/> by which the <see cref="Unhandled"/> is scaled.</param>
    /// <returns>The scaled <see cref="Unhandled3"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Unhandled3 Multiply(Unhandled a, Vector3 b) => a.Multiply(b);

    /// <summary>Scales the provided <see cref="Unhandled"/> by the provided <see cref="Vector4"/>.</summary>
    /// <param name="a">The <see cref="Unhandled"/> that is scaled by the <see cref="Vector4"/>.</param>
    /// <param name="b">The <see cref="Vector4"/> by which the <see cref="Unhandled"/> is scaled.</param>
    /// <returns>The scaled <see cref="Unhandled3"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Unhandled4 Multiply(Unhandled a, Vector4 b) => a.Multiply(b);

    /// <summary>Computes the product of the provided <see cref="Unhandled"/> and <see cref="Unhandled2"/>.</summary>
    /// <param name="a">The <see cref="Unhandled"/> by which the <see cref="Unhandled2"/> is multiplied.</param>
    /// <param name="b">The <see cref="Unhandled2"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled"/> and <see cref="Unhandled2"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Unhandled2 Multiply(Unhandled a, Unhandled2 b) => a.Multiply(b);

    /// <summary>Computes the product of the provided <see cref="Unhandled"/> and <see cref="Unhandled3"/>.</summary>
    /// <param name="a">The <see cref="Unhandled"/> by which the <see cref="Unhandled3"/> is multiplied.</param>
    /// <param name="b">The <see cref="Unhandled3"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled"/> and <see cref="Unhandled3"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Unhandled3 Multiply(Unhandled a, Unhandled3 b) => a.Multiply(b);

    /// <summary>Computes the product of the provided <see cref="Unhandled"/> and <see cref="Unhandled4"/>.</summary>
    /// <param name="a">The <see cref="Unhandled"/> by which the <see cref="Unhandled4"/> is multiplied.</param>
    /// <param name="b">The <see cref="Unhandled4"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled"/> and <see cref="Unhandled4"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Unhandled4 Multiply(Unhandled a, Unhandled4 b) => a.Multiply(b);

    /// <summary>Computes the product of the provided <see cref="Unhandled"/> and <typeparamref name="TScalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity by which the <see cref="Unhandled"/> is multiplied.</typeparam>
    /// <param name="x">The <see cref="Unhandled"/> by which the <typeparamref name="TScalar"/> is multiplied.</param>
    /// <param name="y">The <typeparamref name="TScalar"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled"/> and <typeparamref name="TScalar"/>, { <paramref name="x"/> ∙ <paramref name="y"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled Multiply<TScalar>(Unhandled x, TScalar y) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(y);

        return x.Multiply(y);
    }

    /// <summary>Computes the product of the provided <see cref="Unhandled"/> and <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity by which the <see cref="Unhandled"/> is multiplied.</typeparam>
    /// <param name="a">The <see cref="Unhandled"/> by which the <typeparamref name="TVector"/> is multiplied.</param>
    /// <param name="b">The <typeparamref name="TVector"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply2{TVector}(Unhandled, TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The product of the <see cref="Unhandled"/> and <typeparamref name="TVector"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 Multiply<TVector>(Unhandled a, IVector2Quantity<TVector> b) where TVector : IVector2Quantity<TVector>
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Multiply(b);
    }

    /// <summary>Computes the product of the provided <see cref="Unhandled"/> and <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity by which the <see cref="Unhandled"/> is multiplied.</typeparam>
    /// <param name="a">The <see cref="Unhandled"/> by which the <typeparamref name="TVector"/> is multiplied.</param>
    /// <param name="b">The <typeparamref name="TVector"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply3{TVector}(Unhandled, TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The product of the <see cref="Unhandled"/> and <typeparamref name="TVector"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 Multiply<TVector>(Unhandled a, IVector3Quantity<TVector> b) where TVector : IVector3Quantity<TVector>
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Multiply(b);
    }

    /// <summary>Computes the product of the provided <see cref="Unhandled"/> and <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity by which the <see cref="Unhandled"/> is multiplied.</typeparam>
    /// <param name="a">The <see cref="Unhandled"/> by which the <typeparamref name="TVector"/> is multiplied.</param>
    /// <param name="b">The <typeparamref name="TVector"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply4{TVector}(Unhandled, TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The product of the <see cref="Unhandled"/> and <typeparamref name="TVector"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 Multiply<TVector>(Unhandled a, IVector4Quantity<TVector> b) where TVector : IVector4Quantity<TVector>
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Multiply(b);
    }

    /// <summary>Computes the product of the provided <see cref="Unhandled"/> and <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity by which the <see cref="Unhandled"/> is multiplied.</typeparam>
    /// <param name="a">The <see cref="Unhandled"/> by which the <typeparamref name="TVector"/> is multiplied.</param>
    /// <param name="b">The <typeparamref name="TVector"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled"/> and <typeparamref name="TVector"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 Multiply2<TVector>(Unhandled a, TVector b) where TVector : IVector2Quantity
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Multiply2(b);
    }

    /// <summary>Computes the product of the provided <see cref="Unhandled"/> and <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity by which the <see cref="Unhandled"/> is multiplied.</typeparam>
    /// <param name="a">The <see cref="Unhandled"/> by which the <typeparamref name="TVector"/> is multiplied.</param>
    /// <param name="b">The <typeparamref name="TVector"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled"/> and <typeparamref name="TVector"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 Multiply3<TVector>(Unhandled a, TVector b) where TVector : IVector3Quantity
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Multiply3(b);
    }

    /// <summary>Computes the product of the provided <see cref="Unhandled"/> and <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity by which the <see cref="Unhandled"/> is multiplied.</typeparam>
    /// <param name="a">The <see cref="Unhandled"/> by which the <typeparamref name="TVector"/> is multiplied.</param>
    /// <param name="b">The <typeparamref name="TVector"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled"/> and <typeparamref name="TVector"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 Multiply4<TVector>(Unhandled a, TVector b) where TVector : IVector4Quantity
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Multiply4(b);
    }

    /// <summary>Scales the provided <see cref="Unhandled"/> by the reciprocal of the provided <see cref="Scalar"/>.</summary>
    /// <param name="x">The <see cref="Unhandled"/> that is scaled by the reciprocal of the <see cref="Scalar"/>.</param>
    /// <param name="y">The <see cref="Scalar"/>, the reciprocal of which scales the <see cref="Unhandled"/></param>
    /// <returns>The scaled <see cref="Unhandled"/>, { <paramref name="x"/> / <paramref name="y"/> }.</returns>
    public static Unhandled Divide(Unhandled x, Scalar y) => x.DivideBy(y);

    /// <summary>Computes the quotient of the provided <see cref="Unhandled"/> and <typeparamref name="TScalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity by which the <see cref="Unhandled"/> is divided.</typeparam>
    /// <param name="x">The <see cref="Unhandled"/> that is divided by <typeparamref name="TScalar"/>.</param>
    /// <param name="y">The <typeparamref name="TScalar"/> by which the <see cref="Unhandled"/> is divided.</param>
    /// <returns>The quotient of the <see cref="Unhandled"/> and the <typeparamref name="TScalar"/>, { <paramref name="x"/> / <paramref name="y"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled Divide<TScalar>(Unhandled x, TScalar y) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(y);

        return x.DivideBy(y);
    }

    /// <summary>Determines whether an <see cref="Unhandled"/>, <paramref name="lhs"/>, represents a smaller magnitude than another <see cref="Unhandled"/>, <paramref name="rhs"/>.</summary>
    /// <param name="lhs">The first <see cref="Unhandled"/>, assumed to represent a smaller magnitude than the other <see cref="Unhandled"/>.</param>
    /// <param name="rhs">The second <see cref="Unhandled"/>, assumed to represent a greater magntiude than the other <see cref="Unhandled"/>.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.operator &lt;(double, double)"/>.</remarks>
    /// <returns>A <see cref="bool"/> representing the truthfulness of { <paramref name="lhs"/> &lt; <paramref name="rhs"/> }.</returns>
    public static bool operator <(Unhandled lhs, Unhandled rhs) => lhs.Magnitude < rhs.Magnitude;

    /// <summary>Determines whether an <see cref="Unhandled"/>, <paramref name="lhs"/>, represents a greater magnitude than another <see cref="Unhandled"/>, <paramref name="rhs"/>.</summary>
    /// <param name="lhs">The first <see cref="Unhandled"/>, assumed to represent a greater magnitude than the other <see cref="Unhandled"/>.</param>
    /// <param name="rhs">The second <see cref="Unhandled"/>, assumed to represent a smaller magntiude than the other <see cref="Unhandled"/>.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.operator &gt;(double, double)"/>.</remarks>
    /// <returns>A <see cref="bool"/> representing the truthfulness of { <paramref name="lhs"/> &gt; <paramref name="rhs"/> }.</returns>
    public static bool operator >(Unhandled lhs, Unhandled rhs) => lhs.Magnitude > rhs.Magnitude;

    /// <summary>Determines whether an <see cref="Unhandled"/>, <paramref name="lhs"/>, represents a smaller or equivalent magnitude compared to another <see cref="Unhandled"/>, <paramref name="rhs"/>.</summary>
    /// <param name="lhs">The first <see cref="Unhandled"/>, assumed to represent a smaller or equivalent magnitude compared to the other <see cref="Unhandled"/>.</param>
    /// <param name="rhs">The second <see cref="Unhandled"/>, assumed to represent a greater or equivalent magnitude compared to the other <see cref="Unhandled"/>.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.operator &lt;=(double, double)"/>.</remarks>
    /// <returns>A <see cref="bool"/> representing the truthfulness of { <paramref name="lhs"/> ≤ <paramref name="rhs"/> }.</returns>
    public static bool operator <=(Unhandled lhs, Unhandled rhs) => lhs.Magnitude <= rhs.Magnitude;

    /// <summary>Determines whether an <see cref="Unhandled"/>, <paramref name="lhs"/>, represents a greater or equivalent magnitude compared to another <see cref="Unhandled"/>, <paramref name="rhs"/>.</summary>
    /// <param name="lhs">The first <see cref="Unhandled"/>, assumed to represent a greater or equivalent magnitude compared to the other <see cref="Unhandled"/>.</param>
    /// <param name="rhs">The second <see cref="Unhandled"/>, assumed to represent a smaller or equivalent magnitude compared to the other <see cref="Unhandled"/>.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.operator &gt;=(double, double)"/>.</remarks>
    /// <returns>A <see cref="bool"/> representing the truthfulness of { <paramref name="lhs"/> ≥ <paramref name="rhs"/> }.</returns>
    public static bool operator >=(Unhandled lhs, Unhandled rhs) => lhs.Magnitude >= rhs.Magnitude;

    /// <summary>Applies the unary plus to the provided <see cref="Unhandled"/>.</summary>
    /// <param name="x">The <see cref="Unhandled"/> to which the unary plus is applied.</param>
    /// <returns>The same <see cref="Unhandled"/>, { <paramref name="x"/> }.</returns>
    public static Unhandled operator +(Unhandled x) => x.Plus();

    /// <summary>Negates the provided <see cref="Unhandled"/>.</summary>
    /// <param name="x">The <see cref="Unhandled"/> that is negated.</param>
    /// <returns>The negated <see cref="Unhandled"/>, { -<paramref name="x"/> }.</returns>
    public static Unhandled operator -(Unhandled x) => x.Negate();

    /// <summary>Computes the sum of the provided <see cref="Unhandled"/>.</summary>
    /// <param name="x">The first <see cref="Unhandled"/>, which is added to the second <see cref="Unhandled"/>.</param>
    /// <param name="y">The second <see cref="Unhandled"/>, which is added to the first <see cref="Unhandled"/>.</param>
    /// <returns>The sum of the <see cref="Unhandled"/>, { <paramref name="x"/> + <paramref name="y"/> }.</returns>
    public static Unhandled operator +(Unhandled x, Unhandled y) => Add(x, y);

    /// <summary>Computes the signed difference between the provided <see cref="Unhandled"/>.</summary>
    /// <param name="x">The first <see cref="Unhandled"/>, for which the difference to the second <see cref="Unhandled"/> is computed.</param>
    /// <param name="y">The second <see cref="Unhandled"/>, for which the difference to the first <see cref="Unhandled"/> is computed.</param>
    /// <remarks>The resulting <see cref="Unhandled"/> may be negative. If this is not desired, use <see cref="Difference{TScalar}(Unhandled, TScalar)"/>.</remarks>
    /// <returns>The signed difference between the <see cref="Unhandled"/>, { |<paramref name="x"/> - <paramref name="y"/>| }.</returns>
    public static Unhandled operator -(Unhandled x, Unhandled y) => Subtract(x, y);

    /// <summary>Computes the product of the provided <see cref="Unhandled"/>.</summary>
    /// <param name="x">The first <see cref="Unhandled"/>, by which the second <see cref="Unhandled"/> is multiplied.</param>
    /// <param name="y">The second <see cref="Unhandled"/>, by which the first <see cref="Unhandled"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled"/>, { <paramref name="x"/> ∙ <paramref name="y"/> }.</returns>
    public static Unhandled operator *(Unhandled x, Unhandled y) => Multiply(x, y);

    /// <summary>Computes the quotient of the provided <see cref="Unhandled"/>.</summary>
    /// <param name="x">The first <see cref="Unhandled"/>, which is divided by the second <see cref="Unhandled"/>.</param>
    /// <param name="y">The second <see cref="Unhandled"/>, by which the first <see cref="Unhandled"/> is divided.</param>
    /// <returns>The quotient of the <see cref="Unhandled"/>, { <paramref name="x"/> / <paramref name="y"/> }.</returns>
    public static Unhandled operator /(Unhandled x, Unhandled y) => Divide(x, y);

    /// <inheritdoc cref="Multiply(Unhandled, Scalar)"/>
    public static Unhandled operator *(Unhandled x, Scalar y) => Multiply(x, y);

    /// <summary>Scales the provided <see cref="Unhandled"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="x">The <see cref="Scalar"/> by which the <see cref="Unhandled"/> is scaled.</param>
    /// <param name="y">The <see cref="Unhandled"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Unhandled"/>, { <paramref name="x"/> ∙ <paramref name="y"/> }.</returns>
    public static Unhandled operator *(Scalar x, Unhandled y) => Multiply(y, x);

    /// <inheritdoc cref="Divide(Unhandled, Scalar)"/>
    public static Unhandled operator /(Unhandled x, Scalar y) => Divide(x, y);

    /// <summary>Scales the reciprocal of the provided <see cref="Unhandled"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="x">The <see cref="Scalar"/> by which the reciprocal of the <see cref="Unhandled"/> is scaled.</param>
    /// <param name="y">The <see cref="Unhandled"/>, the reciprocal of which is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled reciprocal of the <see cref="Unhandled"/>, { <paramref name="x"/> / <paramref name="y"/> }.</returns>
    public static Unhandled operator /(Scalar x, Unhandled y) => Scalar.Divide(x, y);

    /// <inheritdoc cref="Multiply(Unhandled, Vector2)"/>
    public static Unhandled2 operator *(Unhandled a, Vector2 b) => Multiply(a, b);

    /// <inheritdoc cref="Multiply(Unhandled, Vector3)"/>
    public static Unhandled3 operator *(Unhandled a, Vector3 b) => Multiply(a, b);

    /// <inheritdoc cref="Multiply(Unhandled, Vector4)"/>
    public static Unhandled4 operator *(Unhandled a, Vector4 b) => Multiply(a, b);

    /// <summary>Computes the sum of the provided <see cref="Unhandled"/> and <see cref="IScalarQuantity"/>.</summary>
    /// <param name="x">The <see cref="Unhandled"/> that is added to the <see cref="IScalarQuantity"/>.</param>
    /// <param name="y">The <see cref="IScalarQuantity"/> that is added to the <see cref="Unhandled"/>.</param>
    /// <remarks>For improved performance, prefer <see cref="Add{TScalar}(Unhandled, TScalar)"/> when the scalar quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The sum of the <see cref="Unhandled"/> and <see cref="IScalarQuantity"/>, { <paramref name="x"/> + <paramref name="y"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator +(Unhandled x, IScalarQuantity y)
    {
        ArgumentNullException.ThrowIfNull(y);

        return Add(x, y);
    }

    /// <summary>Computes the sum of the provided <see cref="Unhandled"/> and <see cref="IScalarQuantity"/>.</summary>
    /// <param name="x">The <see cref="IScalarQuantity"/> that is added to the <see cref="Unhandled"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> that is added to the <see cref="IScalarQuantity"/>.</param>
    /// <remarks>For improved performance, prefer <see cref="Add{TScalar}(Unhandled, TScalar)"/> when the scalar quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The sum of the <see cref="Unhandled"/> and <see cref="IScalarQuantity"/>, { <paramref name="x"/> + <paramref name="y"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    [SuppressMessage("Major Code Smell", "S2234: Parameters should be passed in the correct order", Justification = "Addition is commutative.")]
    public static Unhandled operator +(IScalarQuantity x, Unhandled y)
    {
        ArgumentNullException.ThrowIfNull(x);

        return Add(y, x);
    }

    /// <summary>Computes the signed difference between the provided <see cref="Unhandled"/> and <see cref="IScalarQuantity"/>.</summary>
    /// <param name="x">The <see cref="Unhandled"/> from which the <see cref="IScalarQuantity"/> is subtracted.</param>
    /// <param name="y">The <see cref="IScalarQuantity"/> that is subtracted from the <see cref="Unhandled"/>.</param>
    /// <remarks>The resulting <see cref="Unhandled"/> may be negative. If this is not desired, use <see cref="Difference{TScalar}(Unhandled, TScalar)"/>.
    /// <para>For improved performance, prefer <see cref="Subtract{TScalar}(Unhandled, TScalar)"/> when the scalar quantity is a <see langword="struct"/> (avoiding boxing).</para></remarks>
    /// <returns>The signed difference between the <see cref="Unhandled"/> and <see cref="IScalarQuantity"/>, { <paramref name="x"/> - <paramref name="y"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator -(Unhandled x, IScalarQuantity y)
    {
        ArgumentNullException.ThrowIfNull(y);

        return Subtract(x, y);
    }

    /// <summary>Computes the signed difference between the provided <see cref="IScalarQuantity"/> and <see cref="Unhandled"/>.</summary>
    /// <param name="x">The <see cref="IScalarQuantity"/> from which the <see cref="Unhandled"/> is subtracted.</param>
    /// <param name="y">The <see cref="Unhandled"/> that is subtracted from the <see cref="IScalarQuantity"/>.</param>
    /// <remarks>The resulting <see cref="Unhandled"/> may be negative. If this is not desired, use <see cref="Difference{TScalar}(Unhandled, TScalar)"/>.
    /// <para>For improved performance, prefer -<see cref="Subtract{TScalar}(Unhandled, TScalar)"/> when the scalar quantity is a <see langword="struct"/> (avoiding boxing).</para></remarks>
    /// <returns>The signed difference between the <see cref="IScalarQuantity"/> and <see cref="Unhandled"/>, { <paramref name="x"/> - <paramref name="y"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    [SuppressMessage("Major Code Smell", "S2234: Parameters should be passed in the correct order", Justification = "Subtraction is anti-commutative.")]
    public static Unhandled operator -(IScalarQuantity x, Unhandled y)
    {
        ArgumentNullException.ThrowIfNull(x);

        return -Subtract(y, x);
    }

    /// <summary>Computes the product of the provided <see cref="Unhandled"/> and <see cref="IScalarQuantity"/>.</summary>
    /// <param name="x">The <see cref="Unhandled"/> by which the <see cref="IScalarQuantity"/> is multiplied.</param>
    /// <param name="y">The <see cref="IScalarQuantity"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply{TScalar}(Unhandled, TScalar)"/> when the scalar quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The product of the <see cref="Unhandled"/> and <see cref="IScalarQuantity"/>, { <paramref name="x"/> ∙ <paramref name="y"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Unhandled x, IScalarQuantity y)
    {
        ArgumentNullException.ThrowIfNull(y);

        return Multiply(x, y);
    }

    /// <summary>Computes the product of the provided <see cref="IScalarQuantity"/> and <see cref="Unhandled"/>.</summary>
    /// <param name="x">The <see cref="IScalarQuantity"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <param name="y">The <see cref="Unhandled"/> by which the <see cref="IScalarQuantity"/> is multiplied.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply{TScalar}(TScalar)"/> when the scalar quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The product of the <see cref="IScalarQuantity"/> and <see cref="Unhandled"/>, { <paramref name="x"/> ∙ <paramref name="y"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    [SuppressMessage("Major Code Smell", "S2234: Parameters should be passed in the correct order", Justification = "Multiplication is commutative.")]
    public static Unhandled operator *(IScalarQuantity x, Unhandled y)
    {
        ArgumentNullException.ThrowIfNull(x);

        return Multiply(y, x);
    }

    /// <summary>Computes the quotient of the provided <see cref="Unhandled"/> and <see cref="IScalarQuantity"/>.</summary>
    /// <param name="x">The <see cref="Unhandled"/> that is divided by the <see cref="IScalarQuantity"/>.</param>
    /// <param name="y">The <see cref="IScalarQuantity"/> by which the <see cref="Unhandled"/> is divided.</param>
    /// <remarks>For improved performance, prefer <see cref="DivideBy{TScalar}(TScalar)"/> when the scalar quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The quotient of the <see cref="Unhandled"/> and <see cref="IScalarQuantity"/>, { <paramref name="x"/> / <paramref name="y"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Unhandled x, IScalarQuantity y)
    {
        ArgumentNullException.ThrowIfNull(y);

        return Divide(x, y);
    }

    /// <summary>Computes the quotient of the provided <see cref="IScalarQuantity"/> and <see cref="Unhandled"/>.</summary>
    /// <param name="x">The <see cref="IScalarQuantity"/>that is divided by the <see cref="Unhandled"/>.</param>
    /// <param name="y">The <see cref="Unhandled"/> by which the <see cref="IScalarQuantity"/> is divided.</param>
    /// <remarks>For improved performance, prefer <see cref="Reciprocal"/> and <see cref="Multiply{TScalar}(Unhandled, TScalar)"/> when the scalar quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The quotient of the <see cref="IScalarQuantity"/> and <see cref="Unhandled"/>, { <paramref name="x"/> / <paramref name="y"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(IScalarQuantity x, Unhandled y)
    {
        ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude / y.Magnitude);
    }

    /// <summary>Computes the product of the provided <see cref="Unhandled"/> and <see cref="IVector2Quantity"/>.</summary>
    /// <param name="a">The <see cref="Unhandled"/> by which the <see cref="IVector2Quantity"/> is multiplied.</param>
    /// <param name="b">The <see cref="IVector2Quantity"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply2{TVector}(TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The product of the <see cref="Unhandled"/> and <see cref="IVector2Quantity"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 operator *(Unhandled a, IVector2Quantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return Multiply2(a, b);
    }

    /// <summary>Computes the product of the provided <see cref="IVector2Quantity"/> and <see cref="Unhandled"/>.</summary>
    /// <param name="a">The <see cref="IVector2Quantity"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <param name="b">The <see cref="Unhandled"/> by which the <see cref="IVector2Quantity"/> is multiplied.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply2{TVector}(TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The product of the <see cref="IVector2Quantity"/> and <see cref="Unhandled"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 operator *(IVector2Quantity a, Unhandled b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return Multiply2(b, a);
    }

    /// <summary>Computes the quotient of the provided <see cref="IVector2Quantity"/> and <see cref="Unhandled"/>.</summary>
    /// <param name="a">The <see cref="IVector2Quantity"/>that is divided by the <see cref="Unhandled"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> by which the <see cref="IVector2Quantity"/> is divided.</param>
    /// <remarks>For improved performance, prefer <see cref="Reciprocal"/> and <see cref="Multiply2{TVector}(Unhandled, TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The quotient of the <see cref="IVector2Quantity"/> and <see cref="Unhandled"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 operator /(IVector2Quantity a, Unhandled b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return a.Components.DivideBy(b);
    }

    /// <summary>Computes the product of the provided <see cref="Unhandled"/> and <see cref="IVector3Quantity"/>.</summary>
    /// <param name="a">The <see cref="Unhandled"/> by which the <see cref="IVector3Quantity"/> is multiplied.</param>
    /// <param name="b">The <see cref="IVector3Quantity"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply3{TVector}(TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The product of the <see cref="Unhandled"/> and <see cref="IVector3Quantity"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(Unhandled a, IVector3Quantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return Multiply3(a, b);
    }

    /// <summary>Computes the product of the provided <see cref="IVector3Quantity"/> and <see cref="Unhandled"/>.</summary>
    /// <param name="a">The <see cref="IVector3Quantity"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <param name="b">The <see cref="Unhandled"/> by which the <see cref="IVector3Quantity"/> is multiplied.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply3{TVector}(TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The product of the <see cref="IVector3Quantity"/> and <see cref="Unhandled"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(IVector3Quantity a, Unhandled b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return Multiply3(b, a);
    }

    /// <summary>Computes the quotient of the provided <see cref="IVector3Quantity"/> and <see cref="Unhandled"/>.</summary>
    /// <param name="a">The <see cref="IVector3Quantity"/>that is divided by the <see cref="Unhandled"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> by which the <see cref="IVector3Quantity"/> is divided.</param>
    /// <remarks>For improved performance, prefer <see cref="Reciprocal"/> and <see cref="Multiply3{TVector}(Unhandled, TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The quotient of the <see cref="IVector3Quantity"/> and <see cref="Unhandled"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator /(IVector3Quantity a, Unhandled b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return a.Components.DivideBy(b);
    }

    /// <summary>Computes the product of the provided <see cref="Unhandled"/> and <see cref="IVector4Quantity"/>.</summary>
    /// <param name="a">The <see cref="Unhandled"/> by which the <see cref="IVector4Quantity"/> is multiplied.</param>
    /// <param name="b">The <see cref="IVector4Quantity"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply4{TVector}(TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The product of the <see cref="Unhandled"/> and <see cref="IVector4Quantity"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 operator *(Unhandled a, IVector4Quantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return Multiply4(a, b);
    }

    /// <summary>Computes the product of the provided <see cref="IVector4Quantity"/> and <see cref="Unhandled"/>.</summary>
    /// <param name="a">The <see cref="IVector4Quantity"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <param name="b">The <see cref="Unhandled"/> by which the <see cref="IVector4Quantity"/> is multiplied.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply4{TVector}(TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The product of the <see cref="IVector4Quantity"/> and <see cref="Unhandled"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 operator *(IVector4Quantity a, Unhandled b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return Multiply4(b, a);
    }

    /// <summary>Computes the quotient of the provided <see cref="IVector4Quantity"/> and <see cref="Unhandled"/>.</summary>
    /// <param name="a">The <see cref="IVector4Quantity"/>that is divided by the <see cref="Unhandled"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> by which the <see cref="IVector4Quantity"/> is divided.</param>
    /// <remarks>For improved performance, prefer <see cref="Reciprocal"/> and <see cref="Multiply4{TVector}(Unhandled, TVector)"/> when the vector quantity is a <see langword="struct"/> (avoiding boxing).</remarks>
    /// <returns>The quotient of the <see cref="IVector4Quantity"/> and <see cref="Unhandled"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 operator /(IVector4Quantity a, Unhandled b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return a.Components.DivideBy(b);
    }

    /// <summary>Constructs an <see cref="Unhandled"/>, representing the provided <see cref="Scalar"/> magnitude.</summary>
    /// <param name="value">The magnitude represented by the constructed <see cref="Unhandled"/>.</param>
    /// <returns>The constructed <see cref="Unhandled"/>.</returns>
    public static Unhandled FromScalar(Scalar value) => new(value);

    /// <summary>Retrieves the <see cref="Scalar"/> magnitude of the <see cref="Unhandled"/>.</summary>
    /// <returns>The <see cref="Scalar"/> magnitude of the <see cref="Unhandled"/>.</returns>
    public Scalar ToScalar() => Magnitude;

    /// <summary>Constructs an <see cref="Unhandled"/>, representing the provided <see cref="Scalar"/> magnitude.</summary>
    /// <param name="value">The magnitude represented by the constructed <see cref="Unhandled"/>.</param>
    /// <returns>The constructed <see cref="Unhandled"/>.</returns>
    public static explicit operator Unhandled(Scalar value) => FromScalar(value);

    /// <summary>Retrieves the <see cref="Scalar"/> magnitude of the provided <see cref="Unhandled"/>.</summary>
    /// <param name="unhandled">The <see cref="Unhandled"/>, from which the <see cref="Scalar"/> magnitude is retrieved.</param>
    /// <returns>The <see cref="Scalar"/> magnitude of the <see cref="Unhandled"/>.</returns>
    public static explicit operator Scalar(Unhandled unhandled) => unhandled.ToScalar();
}
