namespace SharpMeasures;

using System;
using System.Globalization;

/// <summary>A three-dimensional vector, representing the <see cref="Scalar"/> components { X, Y, Z, W }.</summary>
public readonly record struct Vector4 : IVector4Quantity<Vector4>, IFormattable
{
    /// <summary>The <see cref="Vector4"/> representing { 0, 0, 0, 0 }.</summary>
    public static Vector4 Zero { get; } = (0, 0, 0, 0);

    /// <summary>The <see cref="Vector4"/> representing { 1, 1, 1, 1 }.</summary>
    public static Vector4 Ones { get; } = (1, 1, 1, 1);

    /// <summary>The X-component of the <see cref="Vector4"/>.</summary>
    public Scalar X { get; }

    /// <summary>The Y-component of the <see cref="Vector4"/>.</summary>
    public Scalar Y { get; }

    /// <summary>The Z-component of the <see cref="Vector4"/>.</summary>
    public Scalar Z { get; }

    /// <summary>The W-component of the <see cref="Vector4"/>.</summary>
    public Scalar W { get; }

    Vector4 IVector4Quantity.Components => this;

    /// <summary>Instantiates a <see cref="Vector4"/> representing the <see cref="Scalar"/> components { X, Y, Z, W }.</summary>
    /// <param name="x">The X-component of the constructed <see cref="Vector4"/>.</param>
    /// <param name="y">The Y-component of the constructed <see cref="Vector4"/>.</param>
    /// <param name="z">The Z-component of the constructed <see cref="Vector4"/>.</param>
    /// <param name="w">The W-component of the constructed <see cref="Vector4"/>.</param>
    public Vector4(Scalar x, Scalar y, Scalar z, Scalar w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    static Vector4 IVector4Quantity<Vector4>.WithComponents(Vector4 components) => components;
    static Vector4 IVector4Quantity<Vector4>.WithComponents(Scalar x, Scalar y, Scalar z, Scalar w) => (x, y, z, w);

    /// <summary>Indicates whether any of the X, Y, Z, and W components of the <see cref="Vector4"/> represent { <see cref="Scalar.NaN"/> }.</summary>
    public bool IsNaN => X.IsNaN || Y.IsNaN || Z.IsNaN || W.IsNaN;

    /// <summary>Indicates whether the <see cref="Vector4"/> represents { 0, 0, 0 }.</summary>
    public bool IsZero => (X.Value, Y.Value, Z.Value, W.Value) is (0, 0, 0, 0);

    /// <summary>Indicates whether the X, Y, Z, and W components of the <see cref="Vector4"/> all represent finite values.</summary>
    public bool IsFinite => X.IsFinite && Y.IsFinite && Z.IsFinite && W.IsFinite;

    /// <summary>Indicates whether any of the X, Y, Z, and W components of the <see cref="Vector4"/> represent an infinite value.</summary>
    public bool IsInfinite => X.IsInfinite || Y.IsInfinite || Z.IsInfinite || W.IsInfinite;

    /// <inheritdoc/>
    public Scalar Magnitude() => SquaredMagnitude().SquareRoot();

    /// <inheritdoc/>
    public Scalar SquaredMagnitude() => Dot(this);

    /// <inheritdoc/>
    public Vector4 Normalize() => this / Magnitude();

    /// <summary>Determines whether the <see cref="Vector4"/> is equivalent to another, provided, <see cref="Vector4"/>.</summary>
    /// <param name="other">The <see cref="Vector4"/> to which the original <see cref="Vector4"/> is compared.</param>
    /// <returns>A <see cref="bool"/> indicating whether the two <see cref="Vector4"/> are equivalent.</returns>
    public bool Equals(Vector4 other) => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);

    /// <summary>Determines whether the provided <see cref="Vector4"/> are equivalent.</summary>
    /// <param name="lhs">The first of the two <see cref="Vector4"/> that are compared.</param>
    /// <param name="rhs">The second of the two <see cref="Vector4"/> that are compared.</param>
    /// <returns>A <see cref="bool"/> indicating whether the two <see cref="Vector4"/> are equivalent.</returns>
    public static bool Equals(Vector4 lhs, Vector4 rhs) => lhs.Equals(rhs);

    /// <summary>Computes the <see cref="int"/> hash code describing the <see cref="Vector4"/>.</summary>
    /// <returns>The <see cref="int"/> hash code describing the <see cref="Vector4"/>.</returns>
    public override int GetHashCode() => (X, Y, Z, W).GetHashCode();

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Vector4"/>, formatted according to the <see cref="CultureInfo.CurrentCulture"/>.</summary>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Vector4"/>.</returns>
    public override string ToString() => ToString(CultureInfo.CurrentCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Vector4"/>, formatted according to the provided <see cref="IFormatProvider"/>.</summary>
    /// <param name="formatProvider">The <see cref="IFormatProvider"/>, providing culture-specific formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Vector4"/>.</returns>
    public string ToString(IFormatProvider? formatProvider) => ToString("G", formatProvider);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Vector4"/>, formatted according to the provided <see cref="string"/> and the <see cref="CultureInfo.CurrentCulture"/>.</summary>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Vector4"/>.</returns>
    public string ToString(string? format) => ToString(format, CultureInfo.CurrentCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Vector4"/>, formatted according to the provided <see cref="string"/> and <see cref="IFormatProvider"/>.</summary>
    /// <param name="formatProvider">The <see cref="IFormatProvider"/>, providing culture-specific formatting information.</param>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Vector4"/>.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (format is "G" or "g" or null)
        {
            format = "({0}, {1}, {2}, {3})";
        }

        return string.Format(formatProvider, format, X, Y, Z, W);
    }

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Vector4"/>, formatted according to the <see cref="CultureInfo.InvariantCulture"/>.</summary>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Vector4"/>.</returns>
    public string ToStringInvariant() => ToString(CultureInfo.InvariantCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Vector4"/>, formatted according to the provided <see cref="string"/> and the <see cref="CultureInfo.InvariantCulture"/>.</summary>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Vector4"/>.</returns>
    public string ToStringInvariant(string? format) => ToString(format, CultureInfo.InvariantCulture);

    /// <summary>Deconstructs the <see cref="Vector4"/> into the X, Y, Z, and W components.</summary>
    /// <param name="x">Assigned the X-component of the <see cref="Vector4"/>.</param>
    /// <param name="y">Assigned the Y-component of the <see cref="Vector4"/>.</param>
    /// <param name="z">Assigned the Z-component of the <see cref="Vector4"/>.</param>
    /// <param name="w">Assigned the W-component of the <see cref="Vector4"/>.</param>
    public void Deconstruct(out Scalar x, out Scalar y, out Scalar z, out Scalar w)
    {
        x = X;
        y = Y;
        z = Z;
        w = W;
    }

    /// <inheritdoc/>
    public Vector4 Plus() => this;

    /// <inheritdoc/>
    public Vector4 Negate() => (-X, -Y, -Z, -W);

    /// <summary>Computes the sum of the <see cref="Vector4"/> and another, provided, <see cref="Vector4"/>.</summary>
    /// <param name="addend">The <see cref="Vector4"/> that is added to the original <see cref="Vector4"/>.</param>
    /// <returns>The sum of the <see cref="Vector4"/>, { <see langword="this"/> + <paramref name="addend"/> }.</returns>
    public Vector4 Add(Vector4 addend) => (X + addend.X, Y + addend.Y, Z + addend.Z, W + addend.W);

    /// <summary>Computes the difference between the <see cref="Vector4"/> and another, provided, <see cref="Vector4"/>.</summary>
    /// <param name="subtrahend">The <see cref="Vector4"/> that is subtracted from the original <see cref="Vector4"/>.</param>
    /// <returns>The difference between the two <see cref="Vector4"/>, { <see langword="this"/> - <paramref name="subtrahend"/> }.</returns>
    public Vector4 Subtract(Vector4 subtrahend) => (X - subtrahend.X, Y - subtrahend.Y, Z - subtrahend.Z, W - subtrahend.W);

    /// <summary>Computes the element-wise remainder from division of the <see cref="Vector4"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Scalar"/> by which the <see cref="Vector4"/> is divided.</param>
    /// <returns>The element-wise remainder from division of the <see cref="Vector4"/> by the <see cref="Scalar"/>, { <see langword="this"/> % <paramref name="divisor"/> }.</returns>
    public Vector4 Remainder(Scalar divisor) => (X % divisor, Y % divisor, Z % divisor, W % divisor);

    /// <summary>Scales the <see cref="Vector4"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="factor">The <see cref="Scalar"/> by which the <see cref="Vector4"/> is scaled.</param>
    /// <returns>The scaled <see cref="Vector4"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Vector4 Multiply(Scalar factor) => this * factor;

    /// <summary>Scales the <see cref="Vector4"/> through division by the provided <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Scalar"/>, scaling the <see cref="Vector4"/> through division.</param>
    /// <returns>The scaled <see cref="Vector4"/>, { <see langword="this"/> / <paramref name="divisor"/> }.</returns>
    public Vector4 Divide(Scalar divisor) => this / divisor;

    /// <summary>Computes the dot product of the <see cref="Vector4"/> and another, provided, <see cref="Vector4"/>.</summary>
    /// <param name="factor">The <see cref="Vector4"/> by which the original <see cref="Vector4"/> is dot multiplied.</param>
    /// <returns>The dot product of the <see cref="Vector4"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Scalar Dot(Vector4 factor) => (X * factor.X) + (Y * factor.Y) + (Z * factor.Z) + (W * factor.W);

    /// <inheritdoc/>
    public static Vector4 operator +(Vector4 a) => a.Plus();

    /// <inheritdoc/>
    public static Vector4 operator -(Vector4 a) => a.Negate();

    /// <summary>Computes the sum of the provided <see cref="Vector4"/>.</summary>
    /// <param name="a">The first <see cref="Vector4"/>, added to the second <see cref="Vector4"/>.</param>
    /// <param name="b">The second <see cref="Vector4"/>, added to the first <see cref="Vector4"/>.</param>
    /// <returns>The sum of the <see cref="Vector4"/>, { <paramref name="a"/> + <paramref name="b"/> }.</returns>
    public static Vector4 operator +(Vector4 a, Vector4 b) => a.Add(b);

    /// <summary>Computes the difference between the provided <see cref="Vector4"/>.</summary>
    /// <param name="a">The first <see cref="Vector4"/>, from which the second <see cref="Vector4"/> is subtracted.</param>
    /// <param name="b">The second <see cref="Vector4"/>, which is subtracted from the first <see cref="Vector4"/>.</param>
    /// <returns>The difference between the two <see cref="Vector4"/>, { <paramref name="a"/> - <paramref name="b"/> }.</returns>
    public static Vector4 operator -(Vector4 a, Vector4 b) => a.Subtract(b);

    /// <summary>Computes the element-wise remainder from division of the provided <see cref="Vector4"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector4"/>, which is divided by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, by which the <see cref="Vector4"/> is divided.</param>
    /// <returns>The element-wise remainder from division of the <see cref="Vector4"/> by the <see cref="Scalar"/>, { <paramref name="a"/> % <paramref name="b"/> }.</returns>
    public static Vector4 operator %(Vector4 a, Scalar b) => a.Remainder(b);

    /// <inheritdoc/>
    public static Vector4 operator *(Vector4 a, Scalar b) => a.Multiply(b);

    /// <inheritdoc/>
    public static Vector4 operator *(Scalar a, Vector4 b) => b.Multiply(a);

    /// <inheritdoc/>
    public static Vector4 operator /(Vector4 a, Scalar b) => a.Divide(b);

    /// <summary>Constructs a <see cref="Vector4"/>, representing the components of the provided <see cref="Scalar"/>-tuple.</summary>
    /// <param name="components">The <see cref="Scalar"/>-tuple describing the components of the constructed <see cref="Vector4"/>.</param>
    /// <returns>The constructed <see cref="Vector4"/>, representing the components of the <see cref="Scalar"/>-tuple.</returns>
    public static Vector4 FromValueTuple((Scalar X, Scalar Y, Scalar Z, Scalar W) components) => new(components.X, components.Y, components.Z, components.W);

    /// <summary>Retrieves the <see cref="Scalar"/>-tuple representing the components of the <see cref="Vector4"/>.</summary>
    /// <returns>The <see cref="Scalar"/>-tuple representing the components of the <see cref="Vector4"/>.</returns>
    public (Scalar X, Scalar Y, Scalar Z, Scalar W) ToValueTuple() => (X, Y, Z, W);

    /// <summary>Constructs a <see cref="Vector4"/>, representing the components of the provided <see cref="Scalar"/>-tuple.</summary>
    /// <param name="components">The <see cref="Scalar"/>-tuple describing the components of the constructed <see cref="Vector4"/>.</param>
    /// <returns>The constructed <see cref="Vector4"/>, representing the components of the <see cref="Scalar"/>-tuple.</returns>
    public static implicit operator Vector4((Scalar X, Scalar Y, Scalar Z, Scalar W) components) => FromValueTuple(components);

    /// <summary>Retrieves the <see cref="Scalar"/>-tuple representing the components of the provided <see cref="Vector4"/>.</summary>
    /// <param name="vector">The <see cref="Vector4"/>, from which the <see cref="Scalar"/>-tuple is retrieved.</param>
    /// <returns>The <see cref="Scalar"/>-tuple representing the components of the <see cref="Vector4"/>.</returns>
    public static implicit operator (Scalar X, Scalar Y, Scalar Z, Scalar W)(Vector4 vector) => vector.ToValueTuple();
}
