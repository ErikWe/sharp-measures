namespace SharpMeasures;

using System;
using System.Globalization;
using System.Numerics;

/// <summary>A three-dimensional vector, representing the <see cref="Scalar"/> components { X, Y, Z }.</summary>
public readonly record struct Vector3 : IVector3Quantity<Vector3>, IFormattable
{
    /// <summary>The <see cref="Vector3"/> representing { 0, 0, 0 }.</summary>
    public static Vector3 Zero { get; } = (0, 0, 0);

    /// <summary>The <see cref="Vector3"/> representing { 1, 1, 1 }.</summary>
    public static Vector3 Ones { get; } = (1, 1, 1);

    /// <summary>The X-component of the <see cref="Vector3"/>.</summary>
    public Scalar X { get; }

    /// <summary>The Y-component of the <see cref="Vector3"/>.</summary>
    public Scalar Y { get; }

    /// <summary>The Z-component of the <see cref="Vector3"/>.</summary>
    public Scalar Z { get; }

    Vector3 IVector3Quantity.Components => this;

    /// <summary>Instantiates a <see cref="Vector3"/>, representing the <see cref="Scalar"/> components { X, Y, Z }.</summary>
    /// <param name="x">The X-component of the constructed <see cref="Vector3"/>.</param>
    /// <param name="y">The Y-component of the constructed <see cref="Vector3"/>.</param>
    /// <param name="z">The Z-component of the constructed <see cref="Vector3"/>.</param>
    public Vector3(Scalar x, Scalar y, Scalar z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    static Vector3 IVector3Quantity<Vector3>.WithComponents(Vector3 components) => components;
    static Vector3 IVector3Quantity<Vector3>.WithComponents(Scalar x, Scalar y, Scalar z) => (x, y, z);

    /// <summary>Indicates whether any of the X, Y, and Z components of the <see cref="Vector3"/> represent { <see cref="Scalar.NaN"/> }.</summary>
    public bool IsNaN => X.IsNaN || Y.IsNaN || Z.IsNaN;

    /// <summary>Indicates whether the <see cref="Vector3"/> represents { 0, 0, 0 }.</summary>
    public bool IsZero => X.IsZero && Y.IsZero && Z.IsZero;

    /// <summary>Indicates whether the X, Y, and Z components of the <see cref="Vector3"/> all represent finite values.</summary>
    public bool IsFinite => X.IsFinite && Y.IsFinite && Z.IsFinite;

    /// <summary>Indicates whether any of the X, Y, and Z components of the <see cref="Vector3"/> represent an infinite value.</summary>
    public bool IsInfinite => X.IsInfinite || Y.IsInfinite || Z.IsInfinite;

    /// <inheritdoc/>
    public Scalar Magnitude() => SquaredMagnitude().SquareRoot();

    /// <inheritdoc/>
    public Scalar SquaredMagnitude() => Dot(this);

    /// <summary>Computes the normalized <see cref="Vector3"/> - the <see cref="Vector3"/> with the same direction, but magnitude { 1 }.</summary>
    /// <returns>The normalized <see cref="Vector3"/>.</returns>
    public Vector3 Normalize() => this / Magnitude();

    /// <summary>Transforms the <see cref="Vector3"/> by the provided <see cref="Matrix4x4"/>.</summary>
    /// <param name="transform">The <see cref="Matrix4x4"/> describing the transformation.</param>
    /// <returns>The transformed <see cref="Vector3"/>.</returns>
    public Vector3 Transform(Matrix4x4 transform) =>
    (
        (X * transform.M11) + (Y * transform.M21) + (Z * transform.M31) + transform.M41,
        (X * transform.M12) + (Y * transform.M22) + (Z * transform.M32) + transform.M42,
        (X * transform.M13) + (Y * transform.M23) + (Z * transform.M33) + transform.M43
    );

    /// <summary>Determines whether the <see cref="Vector3"/> is equivalent to another, provided, <see cref="Vector3"/>.</summary>
    /// <param name="other">The <see cref="Vector3"/> to which the original <see cref="Vector3"/> is compared.</param>
    /// <returns>A <see cref="bool"/> indicating whether the two <see cref="Vector3"/> are equivalent.</returns>
    public bool Equals(Vector3 other) => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);

    /// <summary>Determines whether the provided <see cref="Vector3"/> are equivalent.</summary>
    /// <param name="lhs">The first of the two <see cref="Vector3"/> that are compared.</param>
    /// <param name="rhs">The second of the two <see cref="Vector3"/> that are compared.</param>
    /// <returns>A <see cref="bool"/> indicating whether the two <see cref="Vector3"/> are equivalent.</returns>
    public static bool Equals(Vector3 lhs, Vector3 rhs) => lhs.Equals(rhs);

    /// <summary>Computes the <see cref="int"/> hash code describing the <see cref="Vector3"/>.</summary>
    /// <returns>The <see cref="int"/> hash code describing the <see cref="Vector3"/>.</returns>
    public override int GetHashCode() => (X, Y, Z).GetHashCode();

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Vector3"/>, formatted according to the <see cref="CultureInfo.CurrentCulture"/>.</summary>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Vector3"/>.</returns>
    public override string ToString() => ToString(CultureInfo.CurrentCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Vector3"/>, formatted according to the provided <see cref="IFormatProvider"/>.</summary>
    /// <param name="formatProvider">The <see cref="IFormatProvider"/>, providing culture-specific formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Vector3"/>.</returns>
    public string ToString(IFormatProvider? formatProvider) => ToString("G", formatProvider);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Vector3"/>, formatted according to the provided <see cref="string"/> and the <see cref="CultureInfo.CurrentCulture"/>.</summary>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Vector3"/>.</returns>
    public string ToString(string? format) => ToString(format, CultureInfo.CurrentCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Vector3"/>, formatted according to the provided <see cref="string"/> and <see cref="IFormatProvider"/>.</summary>
    /// <param name="formatProvider">The <see cref="IFormatProvider"/>, providing culture-specific formatting information.</param>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Vector3"/>.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (format is "G" or "g" or null)
        {
            format = "({0}, {1}, {2})";
        }

        return string.Format(formatProvider, format, X, Y, Z);
    }

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Vector3"/>, formatted according to the <see cref="CultureInfo.InvariantCulture"/>.</summary>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Vector3"/>.</returns>
    public string ToStringInvariant() => ToString(CultureInfo.InvariantCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Vector3"/>, formatted according to the provided <see cref="string"/> and the <see cref="CultureInfo.InvariantCulture"/>.</summary>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Vector3"/>.</returns>
    public string ToStringInvariant(string? format) => ToString(format, CultureInfo.InvariantCulture);

    /// <summary>Deconstructs the <see cref="Vector3"/> into the X, Y, and Z components.</summary>
    /// <param name="x">Assigned the X-component of the <see cref="Vector3"/>.</param>
    /// <param name="y">Assigned the Y-component of the <see cref="Vector3"/>.</param>
    /// <param name="z">Assigned the Z-component of the <see cref="Vector3"/>.</param>
    public void Deconstruct(out Scalar x, out Scalar y, out Scalar z)
    {
        x = X;
        y = Y;
        z = Z;
    }

    /// <inheritdoc/>
    public Vector3 Plus() => this;

    /// <inheritdoc/>
    public Vector3 Negate() => (-X, -Y, -Z);

    /// <summary>Computes the sum of the <see cref="Vector3"/> and another, provided, <see cref="Vector3"/>.</summary>
    /// <param name="addend">The <see cref="Vector3"/> that is added to the original <see cref="Vector3"/>.</param>
    /// <returns>The sum of the <see cref="Vector3"/>, { <see langword="this"/> + <paramref name="addend"/> }.</returns>
    public Vector3 Add(Vector3 addend) => (X + addend.X, Y + addend.Y, Z + addend.Z);

    /// <summary>Computes the difference between the <see cref="Vector3"/> and another, provided, <see cref="Vector3"/>.</summary>
    /// <param name="subtrahend">The <see cref="Vector3"/> that is subtracted from the original <see cref="Vector3"/>.</param>
    /// <returns>The difference between the <see cref="Vector3"/>, { <see langword="this"/> - <paramref name="subtrahend"/> }.</returns>
    public Vector3 Subtract(Vector3 subtrahend) => (X - subtrahend.X, Y - subtrahend.Y, Z - subtrahend.Z);

    /// <summary>Computes the element-wise remainder from division of the <see cref="Vector3"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Scalar"/> by which the <see cref="Vector3"/> is divided.</param>
    /// <returns>The element-wise remainder from division of the <see cref="Vector3"/> by the <see cref="Scalar"/>, { <see langword="this"/> % <paramref name="divisor"/> }.</returns>
    public Vector3 Remainder(Scalar divisor) => (X % divisor, Y % divisor, Z % divisor);

    /// <summary>Scales the <see cref="Vector3"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="factor">The <see cref="Scalar"/> by which the <see cref="Vector3"/> is scaled.</param>
    /// <returns>The scaled <see cref="Vector3"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Vector3 Multiply(Scalar factor) => (X * factor, Y * factor, Z * factor);

    /// <summary>Scales the provided <see cref="Unhandled"/> by the <see cref="Vector3"/>.</summary>
    /// <param name="factor">The <see cref="Unhandled"/> that is scaled by the <see cref="Vector3"/> is scaled.</param>
    /// <returns>The scaled <see cref="Unhandled3"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Unhandled3 Multiply(Unhandled factor) => (X * factor, Y * factor, Z * factor);

    /// <summary>Scales the provided <typeparamref name="TScalar"/> by the <see cref="Vector3"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity that is scaled by the <see cref="Vector3"/>.</typeparam>
    /// <param name="factor">The <typeparamref name="TScalar"/> that is scaled by the <see cref="Vector3"/>.</param>
    /// <returns>The scaled <typeparamref name="TScalar"/>-tuple, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public (TScalar, TScalar, TScalar) Multiply<TScalar>(TScalar factor) where TScalar : IScalarQuantity<TScalar>
    {
        ArgumentNullException.ThrowIfNull(factor);

        return (X.Multiply(factor), Y.Multiply(factor), Z.Multiply(factor));
    }

    /// <summary>Scales the <see cref="Vector3"/> by the reciprocal of the provided <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Scalar"/>, the reciprocal of which scales the <see cref="Vector3"/></param>
    /// <returns>The scaled <see cref="Vector3"/>, { <see langword="this"/> / <paramref name="divisor"/> }.</returns>
    public Vector3 DivideBy(Scalar divisor) => (X / divisor, Y / divisor, Z / divisor);

    /// <summary>Scales the reciprocal of the provided <see cref="Unhandled"/> by the <see cref="Vector3"/>.</summary>
    /// <param name="divisor">The <see cref="Unhandled"/>, the reciprocal of which is scaled by the <see cref="Vector3"/></param>
    /// <returns>The scaled <see cref="Unhandled3"/>, { <see langword="this"/> / <paramref name="divisor"/> }.</returns>
    public Unhandled3 DivideBy(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);

    /// <summary>Computes the dot-product of the <see cref="Vector3"/> and another, provided, <see cref="Vector3"/>.</summary>
    /// <param name="factor">The <see cref="Vector3"/> by which the original <see cref="Vector3"/> is dot-multiplied.</param>
    /// <returns>The dot-product of the <see cref="Vector3"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Scalar Dot(Vector3 factor) => (X * factor.X) + (Y * factor.Y) + (Z * factor.Z);

    /// <summary>Computes the cross-product of the <see cref="Vector3"/> and another, provided, <see cref="Vector3"/>.</summary>
    /// <param name="factor">The provided <see cref="Vector3"/>, the second factor in the cross-multiplication with the original <see cref="Vector3"/>.</param>
    /// <returns>The cross-product of the <see cref="Vector3"/>, { <see langword="this"/> ⨯ <paramref name="factor"/> }.</returns>
    public Vector3 Cross(Vector3 factor) =>
    (
        (Y * factor.Z) - (Z * factor.Y),
        (Z * factor.X) - (X * factor.Z),
        (X * factor.Y) - (Y * factor.X)
    );

    /// <summary>Computes the sum of the provided <see cref="Vector3"/>.</summary>
    /// <param name="a">The first <see cref="Vector3"/>, which is added to the second <see cref="Vector3"/>.</param>
    /// <param name="b">The second <see cref="Vector3"/>, which is added to the first <see cref="Vector3"/>.</param>
    /// <returns>The sum of the <see cref="Vector3"/>, { <paramref name="a"/> + <paramref name="b"/> }.</returns>
    public static Vector3 Add(Vector3 a, Vector3 b) => a.Add(b);

    /// <summary>Computes the difference between the provided <see cref="Vector3"/>.</summary>
    /// <param name="a">The first <see cref="Vector3"/>, from which the second <see cref="Vector3"/> is subtracted.</param>
    /// <param name="b">The second <see cref="Vector3"/>, which is subtracted from the first <see cref="Vector3"/>.</param>
    /// <returns>The difference between the <see cref="Scalar"/>, { <paramref name="a"/> - <paramref name="b"/> }.</returns>
    public static Vector3 Subtract(Vector3 a, Vector3 b) => a.Subtract(b);

    /// <summary>Computes the element-wise remainder from division of the provided <see cref="Vector3"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector3"/> that is divided by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/> by which the <see cref="Vector3"/> is divided.</param>
    /// <returns>The element-wise remainder from division of the <see cref="Vector3"/> by the <see cref="Scalar"/>, { <paramref name="a"/> % <paramref name="b"/> }.</returns>
    public static Vector3 Remainder(Vector3 a, Scalar b) => a.Remainder(b);

    /// <summary>Scales the provided <see cref="Vector3"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector3"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/> by which the <see cref="Vector3"/> is scaled.</param>
    /// <returns>The scaled <see cref="Vector3"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Vector3 Multiply(Vector3 a, Scalar b) => a.Multiply(b);

    /// <summary>Scales the provided <see cref="Unhandled"/> by the provided <see cref="Vector3"/>.</summary>
    /// <param name="a">The <see cref="Vector3"/> by which the <see cref="Unhandled"/> is scaled.</param>
    /// <param name="b">The <see cref="Unhandled"/> that is scaled by the <see cref="Vector3"/>.</param>
    /// <returns>The scaled <see cref="Unhandled3"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Unhandled3 Multiply(Vector3 a, Unhandled b) => a.Multiply(b);

    /// <summary>Scales the provided <typeparamref name="TScalar"/> by the provided <see cref="Vector3"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity that is scaled by the <see cref="Vector3"/>.</typeparam>
    /// <param name="a">The <see cref="Vector3"/> by which the <typeparamref name="TScalar"/> is scaled.</param>
    /// <param name="b">The <typeparamref name="TScalar"/> that is scaled by the <see cref="Vector3"/>.</param>
    /// <returns>The scaled <typeparamref name="TScalar"/>-tuple, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static (TScalar, TScalar, TScalar) Multiply<TScalar>(Vector3 a, TScalar b) where TScalar : IScalarQuantity<TScalar>
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Multiply(b);
    }

    /// <summary>Scales the provided <see cref="Vector3"/> by the reciprocal of the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Vector3"/> that is scaled by the reciprocal of the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, the reciprocal of which scales the <see cref="Vector3"/>.</param>
    /// <returns>The quotient of the <see cref="Vector3"/> and <see cref="Scalar"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    public static Vector3 Divide(Vector3 a, Scalar b) => a.DivideBy(b);

    /// <summary>Scales the reciprocal of the provided <see cref="Unhandled"/> by the provided <see cref="Vector3"/>.</summary>
    /// <param name="a">The <see cref="Vector3"/> by which the reciprocal of the <see cref="Unhandled"/> is scaled.</param>
    /// <param name="b">The <see cref="Unhandled"/>, the reciprocal of which is scaled by the <see cref="Vector3"/></param>
    /// <returns>The scaled <see cref="Unhandled3"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    public static Unhandled3 Divide(Vector3 a, Unhandled b) => a.DivideBy(b);

    /// <summary>Computes the dot-product of the provided <see cref="Vector3"/>.</summary>
    /// <param name="a">The first <see cref="Vector3"/>, by which the second <see cref="Vector3"/> is dot-multiplied.</param>
    /// <param name="b">The second <see cref="Vector3"/>, by which the first <see cref="Vector3"/> is dot-multiplied.</param>
    /// <returns>The dot-product of the <see cref="Vector3"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Scalar Dot(Vector3 a, Vector3 b) => a.Dot(b);

    /// <summary>Computes the cross-product of the provided <see cref="Vector3"/>.</summary>
    /// <param name="a">The first <see cref="Vector3"/>, by which the second <see cref="Vector3"/> is cross-multiplied.</param>
    /// <param name="b">The second <see cref="Vector3"/>, by which the first <see cref="Vector3"/> is cross-multiplied.</param>
    /// <returns>The cross-product of the <see cref="Vector3"/>, { <paramref name="a"/> ⨯ <paramref name="b"/> }.</returns>
    public static Vector3 Cross(Vector3 a, Vector3 b) => a.Cross(b);

    /// <summary>Applies the unary plus to the provided <see cref="Vector3"/>.</summary>
    /// <param name="a">The <see cref="Vector3"/> to which the unary plus is applied.</param>
    /// <returns>The same <see cref="Vector3"/>, { <paramref name="a"/> }.</returns>
    public static Vector3 operator +(Vector3 a) => a.Plus();

    /// <summary>Negates the provided <see cref="Vector3"/>.</summary>
    /// <param name="a">The <see cref="Vector3"/> that is negated.</param>
    /// <returns>The negated <see cref="Vector3"/>, { -<paramref name="a"/> }.</returns>
    public static Vector3 operator -(Vector3 a) => a.Negate();

    /// <inheritdoc cref="Add(Vector3, Vector3)"/>
    public static Vector3 operator +(Vector3 a, Vector3 b) => Add(a, b);

    /// <inheritdoc cref="Subtract(Vector3, Vector3)"/>
    public static Vector3 operator -(Vector3 a, Vector3 b) => Subtract(a, b);

    /// <inheritdoc cref="Remainder(Vector3, Scalar)"/>
    public static Vector3 operator %(Vector3 a, Scalar b) => Remainder(a, b);

    /// <inheritdoc cref="Multiply(Vector3, Scalar)"/>
    public static Vector3 operator *(Vector3 a, Scalar b) => Multiply(a, b);

    /// <summary>Scales the provided <see cref="Vector3"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Scalar"/> by which the <see cref="Vector3"/> is scaled.</param>
    /// <param name="b">The <see cref="Vector3"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Vector3"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Vector3 operator *(Scalar a, Vector3 b) => Multiply(b, a);

    /// <inheritdoc/>
    public static Vector3 operator /(Vector3 a, Scalar b) => Divide(a, b);

    /// <inheritdoc cref="Multiply(Vector3, Unhandled)"/>
    public static Unhandled3 operator *(Vector3 a, Unhandled b) => Multiply(a, b);

    /// <inheritdoc cref="Divide(Vector3, Unhandled)"/>
    public static Unhandled3 operator /(Vector3 a, Unhandled b) => Divide(a, b);

    /// <summary>Constructs a <see cref="Vector3"/>, representing the components of the provided <see cref="Scalar"/>-tuple.</summary>
    /// <param name="components">The <see cref="Scalar"/>-tuple describing the components of the constructed <see cref="Vector3"/>.</param>
    /// <returns>The constructed <see cref="Vector3"/>, representing the components of the <see cref="Scalar"/>-tuple.</returns>
    public static Vector3 FromValueTuple((Scalar X, Scalar Y, Scalar Z) components) => new(components.X, components.Y, components.Z);

    /// <summary>Retrieves the <see cref="Scalar"/>-tuple representing the components of the <see cref="Vector3"/>.</summary>
    /// <returns>The <see cref="Scalar"/>-tuple representing the components of the <see cref="Vector3"/>.</returns>
    public (Scalar X, Scalar Y, Scalar Z) ToValueTuple() => (X, Y, Z);

    /// <summary>Constructs a <see cref="Vector3"/>, representing the components of the provided <see cref="Scalar"/>-tuple.</summary>
    /// <param name="components">The <see cref="Scalar"/>-tuple describing the components of the constructed <see cref="Vector3"/>.</param>
    /// <returns>The constructed <see cref="Vector3"/>, representing the components of the <see cref="Scalar"/>-tuple.</returns>
    public static implicit operator Vector3((Scalar X, Scalar Y, Scalar Z) components) => FromValueTuple(components);

    /// <summary>Retrieves the <see cref="Scalar"/>-tuple representing the components of the provided <see cref="Vector3"/>.</summary>
    /// <param name="vector">The <see cref="Vector3"/>, from which the <see cref="Scalar"/>-tuple is retrieved.</param>
    /// <returns>The <see cref="Scalar"/>-tuple representing the components of the <see cref="Vector3"/>.</returns>
    public static implicit operator (Scalar X, Scalar Y, Scalar Z)(Vector3 vector) => vector.ToValueTuple();
}
