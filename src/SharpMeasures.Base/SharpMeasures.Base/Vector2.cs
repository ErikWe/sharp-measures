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
    public bool IsZero => X.IsZero && Y.IsZero;

    /// <summary>Indicates whether both the X and Y components of the <see cref="Vector2"/> represent finite values.</summary>
    public bool IsFinite => X.IsFinite && Y.IsFinite;

    /// <summary>Indicates whether either of the X and Y components of the <see cref="Vector2"/> represent an infinite value.</summary>
    public bool IsInfinite => X.IsInfinite || Y.IsInfinite;

    /// <inheritdoc/>
    public Scalar Magnitude() => SquaredMagnitude().SquareRoot();

    /// <inheritdoc/>
    public Scalar SquaredMagnitude() => Dot(this);

    /// <summary>Computes the normalized <see cref="Vector2"/> - the <see cref="Vector2"/> with the same direction, but magnitude { 1 }.</summary>
    /// <returns>The normalized <see cref="Vector2"/>.</returns>
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

    /// <summary>Applies the unary plus to the <see cref="Vector2"/>.</summary>
    /// <returns>The same <see cref="Vector2"/>, { <see langword="this"/> }.</returns>
    public Vector2 Plus() => this;

    /// <summary>Negates the <see cref="Vector2"/>.</summary>
    /// <returns>The negated <see cref="Vector2"/>, { -<see langword="this"/> }.</returns>
    public Vector2 Negate() => (-X, -Y);

    /// <summary>Computes the sum of the <see cref="Vector2"/> and another, provided, <see cref="Vector2"/>.</summary>
    /// <param name="addend">The <see cref="Vector2"/> that is added to the original <see cref="Vector2"/>.</param>
    /// <returns>The sum of the <see cref="Vector2"/>, { <see langword="this"/> + <paramref name="addend"/> }.</returns>
    public Vector2 Add(Vector2 addend) => (X + addend.X, Y + addend.Y);

    /// <summary>Computes the difference between the <see cref="Vector2"/> and another, provided, <see cref="Vector2"/>.</summary>
    /// <param name="subtrahend">The <see cref="Vector2"/> that is subtracted from the original <see cref="Vector2"/>.</param>
    /// <returns>The difference between the <see cref="Vector2"/>, { <see langword="this"/> - <paramref name="subtrahend"/> }.</returns>
    public Vector2 Subtract(Vector2 subtrahend) => (X - subtrahend.X, Y - subtrahend.Y);

    /// <summary>Computes the element-wise remainder from division of the <see cref="Vector2"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Scalar"/> by which the <see cref="Vector2"/> is divided.</param>
    /// <returns>The element-wise remainder from division of the <see cref="Vector2"/> by the <see cref="Scalar"/>, { <see langword="this"/> % <paramref name="divisor"/> }.</returns>
    public Vector2 Remainder(Scalar divisor) => (X % divisor, Y % divisor);

    /// <summary>Scales the <see cref="Vector2"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="factor">The <see cref="Scalar"/> by which the <see cref="Vector2"/> is scaled.</param>
    /// <returns>The scaled <see cref="Vector2"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Vector2 Multiply(Scalar factor) => (X * factor, Y * factor);

    /// <summary>Scales the provided <see cref="Unhandled"/> by the <see cref="Vector2"/>.</summary>
    /// <param name="factor">The <see cref="Unhandled"/> that is scaled by the <see cref="Vector2"/> is scaled.</param>
    /// <returns>The scaled <see cref="Unhandled2"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Unhandled2 Multiply(Unhandled factor) => (X * factor, Y * factor);

    /// <summary>Scales the provided <typeparamref name="TScalar"/> by the <see cref="Vector2"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity that is scaled by the <see cref="Vector2"/>.</typeparam>
    /// <param name="factor">The <typeparamref name="TScalar"/> that is scaled by the <see cref="Vector2"/>.</param>
    /// <returns>The scaled <typeparamref name="TScalar"/>-tuple, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public (TScalar, TScalar) Multiply<TScalar>(TScalar factor) where TScalar : IScalarQuantity<TScalar>
    {
        ArgumentNullException.ThrowIfNull(factor);

        return (X.Multiply(factor), Y.Multiply(factor));
    }

    /// <summary>Scales the <see cref="Vector2"/> by the reciprocal of the provided <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Scalar"/>, the reciprocal of which scales the <see cref="Vector2"/></param>
    /// <returns>The scaled <see cref="Vector2"/>, { <see langword="this"/> / <paramref name="divisor"/> }.</returns>
    public Vector2 DivideBy(Scalar divisor) => (X / divisor, Y / divisor);

    /// <summary>Scales the reciprocal of the provided <see cref="Unhandled"/> by the <see cref="Vector2"/>.</summary>
    /// <param name="divisor">The <see cref="Unhandled"/>, the reciprocal of which is scaled by the <see cref="Vector2"/></param>
    /// <returns>The scaled <see cref="Unhandled2"/>, { <see langword="this"/> / <paramref name="divisor"/> }.</returns>
    public Unhandled2 DivideBy(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude);

    /// <summary>Computes the dot-product of the <see cref="Vector2"/> and another, provided, <see cref="Vector2"/>.</summary>
    /// <param name="factor">The <see cref="Vector2"/> by which the original <see cref="Vector2"/> is dot-multiplied.</param>
    /// <returns>The dot-product of the <see cref="Vector2"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Scalar Dot(Vector2 factor) => (X * factor.X) + (Y * factor.Y);

    /// <summary>Computes the sum of the provided <see cref="Vector2"/>.</summary>
    /// <param name="a">The first <see cref="Vector2"/>, which is added to the second <see cref="Vector2"/>.</param>
    /// <param name="b">The second <see cref="Vector2"/>, which is added to the first <see cref="Vector2"/>.</param>
    /// <returns>The sum of the <see cref="Vector2"/>, { <paramref name="a"/> + <paramref name="b"/> }.</returns>
    public static Vector2 Add(Vector2 a, Vector2 b) => a.Add(b);

    /// <summary>Computes the difference between the provided <see cref="Vector2"/>.</summary>
    /// <param name="a">The first <see cref="Vector2"/>, from which the second <see cref="Vector2"/> is subtracted.</param>
    /// <param name="b">The second <see cref="Vector2"/>, which is subtracted from the first <see cref="Vector2"/>.</param>
    /// <returns>The difference between the <see cref="Scalar"/>, { <paramref name="a"/> - <paramref name="b"/> }.</returns>
    public static Vector2 Subtract(Vector2 a, Vector2 b) => a.Subtract(b);

    /// <summary>Computes the element-wise remainder from division of the provided <see cref="Vector2"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector2"/> that is divided by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/> by which the <see cref="Vector2"/> is divided.</param>
    /// <returns>The element-wise remainder from division of the <see cref="Vector2"/> by the <see cref="Scalar"/>, { <paramref name="a"/> % <paramref name="b"/> }.</returns>
    public static Vector2 Remainder(Vector2 a, Scalar b) => a.Remainder(b);

    /// <summary>Scales the provided <see cref="Vector2"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector2"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/> by which the <see cref="Vector2"/> is scaled.</param>
    /// <returns>The scaled <see cref="Vector2"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Vector2 Multiply(Vector2 a, Scalar b) => a.Multiply(b);

    /// <summary>Scales the provided <see cref="Unhandled"/> by the provided <see cref="Vector2"/>.</summary>
    /// <param name="a">The <see cref="Vector2"/> by which the <see cref="Unhandled"/> is scaled.</param>
    /// <param name="b">The <see cref="Unhandled"/> that is scaled by the <see cref="Vector2"/>.</param>
    /// <returns>The scaled <see cref="Unhandled2"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Unhandled2 Multiply(Vector2 a, Unhandled b) => a.Multiply(b);

    /// <summary>Scales the provided <typeparamref name="TScalar"/> by the provided <see cref="Vector2"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity that is scaled by the <see cref="Vector2"/>.</typeparam>
    /// <param name="a">The <see cref="Vector2"/> by which the <typeparamref name="TScalar"/> is scaled.</param>
    /// <param name="b">The <typeparamref name="TScalar"/> that is scaled by the <see cref="Vector2"/>.</param>
    /// <returns>The scaled <typeparamref name="TScalar"/>-tuple, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static (TScalar, TScalar) Multiply<TScalar>(Vector2 a, TScalar b) where TScalar : IScalarQuantity<TScalar>
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Multiply(b);
    }

    /// <summary>Scales the provided <see cref="Vector2"/> by the reciprocal of the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector2"/> that is scaled by the reciprocal of the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, the reciprocal of which scales the <see cref="Vector2"/>.</param>
    /// <returns>The quotient of the <see cref="Vector2"/> and <see cref="Scalar"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    public static Vector2 Divide(Vector2 a, Scalar b) => a.DivideBy(b);

    /// <summary>Scales the reciprocal of the provided <see cref="Unhandled"/> by the provided <see cref="Vector2"/>.</summary>
    /// <param name="a">The <see cref="Vector2"/> by which the reciprocal of the <see cref="Unhandled"/> is scaled.</param>
    /// <param name="b">The <see cref="Unhandled"/>, the reciprocal of which is scaled by the <see cref="Vector2"/></param>
    /// <returns>The scaled <see cref="Unhandled2"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    public static Unhandled2 Divide(Vector2 a, Unhandled b) => a.DivideBy(b);

    /// <summary>Computes the dot-product of the provided <see cref="Vector2"/>.</summary>
    /// <param name="a">The first <see cref="Vector2"/>, by which the second <see cref="Vector2"/> is dot-multiplied.</param>
    /// <param name="b">The second <see cref="Vector2"/>, by which the first <see cref="Vector2"/> is dot-multiplied.</param>
    /// <returns>The dot-product of the <see cref="Vector2"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Scalar Dot(Vector2 a, Vector2 b) => a.Dot(b);

    /// <summary>Applies the unary plus to the provided <see cref="Vector2"/>.</summary>
    /// <param name="a">The <see cref="Vector2"/> to which the unary plus is applied.</param>
    /// <returns>The same <see cref="Vector2"/>, { <paramref name="a"/> }.</returns>
    public static Vector2 operator +(Vector2 a) => a.Plus();

    /// <summary>Negates the provided <see cref="Vector2"/>.</summary>
    /// <param name="a">The <see cref="Vector2"/> that is negated.</param>
    /// <returns>The negated <see cref="Vector2"/>, { -<paramref name="a"/> }.</returns>
    public static Vector2 operator -(Vector2 a) => a.Negate();

    /// <inheritdoc cref="Add(Vector2, Vector2)"/>
    public static Vector2 operator +(Vector2 a, Vector2 b) => Add(a, b);

    /// <inheritdoc cref="Subtract(Vector2, Vector2)"/>
    public static Vector2 operator -(Vector2 a, Vector2 b) => Subtract(a, b);

    /// <inheritdoc cref="Remainder(Vector2, Scalar)"/>
    public static Vector2 operator %(Vector2 a, Scalar b) => Remainder(a, b);

    /// <inheritdoc cref="Multiply(Vector2, Scalar)"/>
    public static Vector2 operator *(Vector2 a, Scalar b) => Multiply(a, b);

    /// <summary>Scales the provided <see cref="Vector2"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Scalar"/> by which the <see cref="Vector2"/> is scaled.</param>
    /// <param name="b">The <see cref="Vector2"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Vector2"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Vector2 operator *(Scalar a, Vector2 b) => Multiply(b, a);

    /// <inheritdoc cref="Divide(Vector2, Scalar)"/>
    public static Vector2 operator /(Vector2 a, Scalar b) => Divide(a, b);

    /// <inheritdoc cref="Multiply(Vector2, Unhandled)"/>
    public static Unhandled2 operator *(Vector2 a, Unhandled b) => Multiply(a, b);

    /// <inheritdoc cref="Divide(Vector2, Unhandled)"/>
    public static Unhandled2 operator /(Vector2 a, Unhandled b) => Divide(a, b);

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
