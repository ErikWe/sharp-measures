namespace SharpMeasures;

using System;
using System.Globalization;

/// <summary>A two-dimensional vector, representing the <see cref="Scalar"/> components { X, Y }.</summary>
public readonly record struct Vector2 : IVector2Quantity<Vector2>, IFormattable
{
    /// <summary>The <see cref="Vector2"/> representing { 0, 0 }.</summary>
    public static Vector2 Zero { get; } = (0, 0);

    /// <summary>The <see cref="Vector2"/> representing { 1, 1 }.</summary>
    public static Vector2 Ones { get; } = (1, 1);

    /// <summary>The X-component of the <see cref="Vector2"/>.</summary>
    public Scalar X { get; }

    /// <summary>The Y-component of the <see cref="Vector2"/>.</summary>
    public Scalar Y { get; }

    Vector2 IVector2Quantity.Components => this;

    /// <summary>Instantiates a <see cref="Vector2"/>, representing the <see cref="Scalar"/> components { X, Y }.</summary>
    /// <param name="x">The X-component of the constructed <see cref="Vector2"/>.</param>
    /// <param name="y">The Y-component of the constructed <see cref="Vector2"/>.</param>
    public Vector2(Scalar x, Scalar y)
    {
        X = x;
        Y = y;
    }

    static Vector2 IVector2Quantity<Vector2>.WithComponents(Vector2 components) => components;
    static Vector2 IVector2Quantity<Vector2>.WithComponents(Scalar x, Scalar y) => (x, y);

    /// <summary>Indicates whether either of the X and Y components of the <see cref="Vector2"/> represent { <see cref="Scalar.NaN"/> }.</summary>
    public bool IsNaN => X.IsNaN || Y.IsNaN;

    /// <summary>Indicates whether the <see cref="Vector2"/> represents { 0, 0 }.</summary>
    public bool IsZero => (X.Value, Y.Value) is (0, 0);

    /// <summary>Indicates whether both the X and Y components of the <see cref="Vector2"/> represent finite values.</summary>
    public bool IsFinite => X.IsFinite && Y.IsFinite;

    /// <summary>Indicates whether either of the X and Y components of the <see cref="Vector2"/> represent an infinite value.</summary>
    public bool IsInfinite => X.IsInfinite || Y.IsInfinite;

    /// <inheritdoc/>
    public Scalar Magnitude() => SquaredMagnitude().SquareRoot();

    /// <inheritdoc/>
    public Scalar SquaredMagnitude() => Dot(this);

    /// <inheritdoc/>
    public Vector2 Normalize() => this / Magnitude();

    /// <summary>Determines whether the <see cref="Vector2"/> is equivalent to another, provided, <see cref="Vector2"/>.</summary>
    /// <param name="other">The <see cref="Vector2"/> to which the original <see cref="Vector2"/> is compared.</param>
    /// <returns>A <see cref="bool"/> indicating whether the two <see cref="Vector2"/> are equivalent.</returns>
    public bool Equals(Vector2 other) => X.Equals(other.X) && Y.Equals(other.Y);

    /// <summary>Determines whether the provided <see cref="Vector2"/> are equivalent.</summary>
    /// <param name="lhs">The first of the two <see cref="Vector2"/> that are compared.</param>
    /// <param name="rhs">The second of the two <see cref="Vector2"/> that are compared.</param>
    /// <returns>A <see cref="bool"/> indicating whether the provided <see cref="Vector2"/> are equivalent.</returns>
    public static bool Equals(Vector2 lhs, Vector2 rhs) => lhs.Equals(rhs);

    /// <summary>Computes the <see cref="int"/> hash code describing the <see cref="Vector2"/>.</summary>
    /// <returns>The <see cref="int"/> hash code describing the <see cref="Vector2"/>.</returns>
    public override int GetHashCode() => (X, Y).GetHashCode();

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Vector2"/>, formatted according to the <see cref="CultureInfo.CurrentCulture"/>.</summary>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Vector2"/>.</returns>
    public override string ToString() => ToString(CultureInfo.CurrentCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Vector2"/>, formatted according to the provided <see cref="IFormatProvider"/>.</summary>
    /// <param name="formatProvider">The <see cref="IFormatProvider"/>, providing culture-specific formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Vector2"/>.</returns>
    public string ToString(IFormatProvider? formatProvider) => ToString("G", formatProvider);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Vector2"/>, formatted according to the provided <see cref="string"/> and the <see cref="CultureInfo.CurrentCulture"/>.</summary>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Vector2"/>.</returns>
    public string ToString(string? format) => ToString(format, CultureInfo.CurrentCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Vector2"/>, formatted according to the provided <see cref="string"/> and <see cref="IFormatProvider"/>.</summary>
    /// <param name="formatProvider">The <see cref="IFormatProvider"/>, providing culture-specific formatting information.</param>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Vector2"/>.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (format is "G" or "g" or null)
        {
            format = "({0}, {1})";
        }

        return string.Format(formatProvider, format, X, Y);
    }

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Vector2"/>, formatted according to the <see cref="CultureInfo.InvariantCulture"/>.</summary>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Vector2"/>.</returns>
    public string ToStringInvariant() => ToString(CultureInfo.InvariantCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Vector2"/>, formatted according to the provided <see cref="string"/> and the <see cref="CultureInfo.InvariantCulture"/>.</summary>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Vector2"/>.</returns>
    public string ToStringInvariant(string? format) => ToString(format, CultureInfo.InvariantCulture);

    /// <summary>Deconstructs the <see cref="Vector2"/> into the X and Y components.</summary>
    /// <param name="x">Assigned the X-component of the <see cref="Vector2"/>.</param>
    /// <param name="y">Assigned the Y-component of the <see cref="Vector2"/>.</param>
    public void Deconstruct(out Scalar x, out Scalar y)
    {
        x = X;
        y = Y;
    }

    /// <inheritdoc/>
    public Vector2 Plus() => this;

    /// <inheritdoc/>
    public Vector2 Negate() => (-X, -Y);

    /// <summary>Computes the sum of the <see cref="Vector2"/> and another, provided, <see cref="Vector2"/>.</summary>
    /// <param name="addend">The <see cref="Vector2"/> that is added to the original <see cref="Vector2"/>.</param>
    /// <returns>The sum of the <see cref="Vector2"/>, { <see langword="this"/> + <paramref name="addend"/> }.</returns>
    public Vector2 Add(Vector2 addend) => (X + addend.X, Y + addend.Y);

    /// <summary>Computes the difference between the <see cref="Vector2"/> and another, provided, <see cref="Vector2"/>.</summary>
    /// <param name="subtrahend">The <see cref="Vector2"/> that is subtracted from the original <see cref="Vector2"/>.</param>
    /// <returns>The difference between the two <see cref="Vector2"/>, { <see langword="this"/> - <paramref name="subtrahend"/> }.</returns>
    public Vector2 Subtract(Vector2 subtrahend) => (X - subtrahend.X, Y - subtrahend.Y);

    /// <summary>Computes the element-wise remainder from division of the <see cref="Vector2"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Scalar"/> by which the <see cref="Vector2"/> is divided.</param>
    /// <returns>The element-wise remainder from division of the <see cref="Vector2"/> by the <see cref="Scalar"/>, { <see langword="this"/> % <paramref name="divisor"/> }.</returns>
    public Vector2 Remainder(Scalar divisor) => (X % divisor, Y % divisor);

    /// <summary>Scales the <see cref="Vector2"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="factor">The <see cref="Scalar"/> by which the <see cref="Vector2"/> is scaled.</param>
    /// <returns>The scaled <see cref="Vector2"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Vector2 Multiply(Scalar factor) => (X * factor, Y * factor);

    /// <summary>Scales the <see cref="Vector2"/> through division by the provided <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Scalar"/>, scaling the <see cref="Vector2"/> through division.</param>
    /// <returns>The scaled <see cref="Vector2"/>, { <see langword="this"/> / <paramref name="divisor"/> }.</returns>
    public Vector2 Divide(Scalar divisor) => (X / divisor, Y / divisor);

    /// <summary>Computes the dot product of the <see cref="Vector2"/> and another, provided, <see cref="Vector2"/>.</summary>
    /// <param name="factor">The <see cref="Vector2"/> by which the original <see cref="Vector2"/> is dot multiplied.</param>
    /// <returns>The dot product of the <see cref="Vector2"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Scalar Dot(Vector2 factor) => (X * factor.X) + (Y * factor.Y);

    /// <inheritdoc/>
    public static Vector2 operator +(Vector2 a) => a.Plus();

    /// <inheritdoc/>
    public static Vector2 operator -(Vector2 a) => a.Negate();

    /// <summary>Computes the sum of the provided <see cref="Vector2"/>.</summary>
    /// <param name="a">The first <see cref="Vector2"/>, added to the second <see cref="Vector2"/>.</param>
    /// <param name="b">The second <see cref="Vector2"/>, added to the first <see cref="Vector2"/>.</param>
    /// <returns>The sum of the <see cref="Vector2"/>, { <paramref name="a"/> + <paramref name="b"/> }.</returns>
    public static Vector2 operator +(Vector2 a, Vector2 b) => a.Add(b);

    /// <summary>Computes the difference between the provided <see cref="Vector2"/>.</summary>
    /// <param name="a">The first <see cref="Vector2"/>, from which the second <see cref="Vector2"/> is subtracted.</param>
    /// <param name="b">The second <see cref="Vector2"/>, which is subtracted from the first <see cref="Vector2"/>.</param>
    /// <returns>The difference between the two <see cref="Vector2"/>, { <paramref name="a"/> - <paramref name="b"/> }.</returns>
    public static Vector2 operator -(Vector2 a, Vector2 b) => a.Subtract(b);

    /// <summary>Computes the element-wise remainder from division of the provided <see cref="Vector2"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector2"/>, which is divided by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, by which the <see cref="Vector2"/> is divided.</param>
    /// <returns>The element-wise remainder from division of the <see cref="Vector2"/> by the <see cref="Scalar"/>, { <paramref name="a"/> % <paramref name="b"/> }.</returns>
    public static Vector2 operator %(Vector2 a, Scalar b) => a.Remainder(b);

    /// <inheritdoc/>
    public static Vector2 operator *(Vector2 a, Scalar b) => a.Multiply(b);

    /// <inheritdoc/>
    public static Vector2 operator *(Scalar a, Vector2 b) => b.Multiply(a);

    /// <inheritdoc/>
    public static Vector2 operator /(Vector2 a, Scalar b) => a.Divide(b);

    /// <summary>Constructs a <see cref="Vector2"/>, representing the components of the provided <see cref="Scalar"/>-tuple.</summary>
    /// <param name="components">The <see cref="Scalar"/>-tuple describing the components of the constructed <see cref="Vector2"/>.</param>
    /// <returns>The constructed <see cref="Vector2"/>, representing the components of the <see cref="Scalar"/>-tuple.</returns>
    public static Vector2 FromValueTuple((Scalar X, Scalar Y) components) => new(components.X, components.Y);

    /// <summary>Retrieves the <see cref="Scalar"/>-tuple representing the components of the <see cref="Vector2"/>.</summary>
    /// <returns>The <see cref="Scalar"/>-tuple representing the components of the <see cref="Vector2"/>.</returns>
    public (Scalar X, Scalar Y) ToValueTuple() => (X, Y);

    /// <summary>Constructs a <see cref="Vector2"/>, representing the components of the provided <see cref="Scalar"/>-tuple.</summary>
    /// <param name="components">The <see cref="Scalar"/>-tuple describing the components of the constructed <see cref="Vector2"/>.</param>
    /// <returns>The constructed <see cref="Vector2"/>, representing the components of the <see cref="Scalar"/>-tuple.</returns>
    public static implicit operator Vector2((Scalar X, Scalar Y) components) => FromValueTuple(components);

    /// <summary>Retrieves the <see cref="Scalar"/>-tuple representing the components of the provided <see cref="Vector2"/>.</summary>
    /// <param name="vector">The <see cref="Vector2"/>, from which the <see cref="Scalar"/>-tuple is retrieved.</param>
    /// <returns>The <see cref="Scalar"/>-tuple representing the components of the <see cref="Vector2"/>.</returns>
    public static implicit operator (Scalar X, Scalar Y)(Vector2 vector) => vector.ToValueTuple();
}
