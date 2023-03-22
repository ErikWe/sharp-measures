﻿namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

/// <summary>A measure of some four-dimensional vector quantity not covered by a designated type.</summary>
public readonly record struct Unhandled4 : IVector4Quantity<Unhandled4>, IFormattable
{
    /// <summary>The <see cref="Unhandled4"/> representing { 0, 0, 0 }.</summary>
    public static Unhandled4 Zero { get; } = new(0, 0, 0, 0);

    /// <summary>The X-component of the <see cref="Unhandled4"/>.</summary>
    public Unhandled X { get; }

    /// <summary>The Y-component of the <see cref="Unhandled4"/>.</summary>
    public Unhandled Y { get; }

    /// <summary>The Z-component of the <see cref="Unhandled4"/>.</summary>
    public Unhandled Z { get; }

    /// <summary>The W-component of the <see cref="Unhandled4"/>.</summary>
    public Unhandled W { get; }

    Scalar IVector4Quantity.X => X.Magnitude;
    Scalar IVector4Quantity.Y => Y.Magnitude;
    Scalar IVector4Quantity.Z => Z.Magnitude;
    Scalar IVector4Quantity.W => W.Magnitude;

    /// <summary>The magnitudes of the X, Y, Z, and W components of the <see cref="Unhandled4"/>.</summary>
    public Vector4 Components => (X.Magnitude, Y.Magnitude, Z.Magnitude, W.Magnitude);

    /// <summary>Instantiates an <see cref="Unhandled4"/>, representing a measure of some four-dimensional vector quantity not covered by a designated type.</summary>
    /// <param name="x">The X-component of the constructed <see cref="Unhandled4"/>.</param>
    /// <param name="y">The Y-component of the constructed <see cref="Unhandled4"/>.</param>
    /// <param name="z">The Z-component of the constructed <see cref="Unhandled4"/>.</param>
    /// <param name="w">The Z-component of the constructed <see cref="Unhandled4"/>.</param>
    public Unhandled4(Unhandled x, Unhandled y, Unhandled z, Unhandled w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>Instantiates an <see cref="Unhandled4"/>, representing a measure of some four-dimensional vector quantity not covered by a designated type.</summary>
    /// <param name="x">The magnitude of the X-component of the constructed <see cref="Unhandled4"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the constructed <see cref="Unhandled4"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the constructed <see cref="Unhandled4"/>.</param>
    /// <param name="w">The magnitude of the W-component of the constructed <see cref="Unhandled4"/>.</param>
    public Unhandled4(Scalar x, Scalar y, Scalar z, Scalar w)
    {
        X = new(x);
        Y = new(y);
        Z = new(z);
        W = new(w);
    }

    /// <summary>Instantiates an <see cref="Unhandled4"/>, representing a measure of some four-dimensional vector quantity not covered by a designated type.</summary>
    /// <param name="components">The magnitudes of the components of the constructed <see cref="Unhandled4"/>.</param>
    public Unhandled4(Vector4 components)
    {
        X = new(components.X);
        Y = new(components.Y);
        Z = new(components.Z);
        W = new(components.W);
    }

    static Unhandled4 IVector4Quantity<Unhandled4>.WithComponents(Vector4 components) => new(components);
    static Unhandled4 IVector4Quantity<Unhandled4>.WithComponents(Scalar x, Scalar y, Scalar z, Scalar w) => new(x, y, z, w);

    /// <summary>Indicates whether any of the X, Y, Z, and W components of the <see cref="Unhandled4"/> represent { <see cref="Scalar.NaN"/> }.</summary>
    public bool IsNaN => X.IsNaN || Y.IsNaN || Z.IsNaN || W.IsNaN;

    /// <summary>Indicates whether the <see cref="Unhandled4"/> represents { 0, 0, 0 }.</summary>
    public bool IsZero => X.IsZero && Y.IsZero && Z.IsZero && W.IsZero;

    /// <summary>Indicates whether the X, Y, Z, and W components of the <see cref="Unhandled4"/> all represent finite values.</summary>
    public bool IsFinite => X.IsFinite && Y.IsFinite && Z.IsFinite && W.IsFinite;

    /// <summary>Indicates whether any of the X, Y, Z, and W components of the <see cref="Unhandled4"/> represent an infinite value.</summary>
    public bool IsInfinite => X.IsInfinite || Y.IsInfinite || Z.IsInfinite || W.IsInfinite;

    Scalar IVectorQuantity.Magnitude() => PureMagnitude();
    Scalar IVectorQuantity.SquaredMagnitude() => PureSquaredMagnitude();

    /// <summary>Computes the magnitude, or length, of the <see cref="Unhandled4"/> - represented as a <see cref="Scalar"/>.</summary>
    /// <remarks>For improved performance, prefer <see cref="PureSquaredMagnitude"/> when applicable.</remarks>
    /// <returns>The magnitude of the <see cref="Unhandled4"/>, as a <see cref="Scalar"/>.</returns>
    public Scalar PureMagnitude() => PureSquaredMagnitude().SquareRoot();

    /// <summary>Computes the square of the magnitude, or length, of the <see cref="Unhandled4"/> - represented as a <see cref="Scalar"/>.</summary>
    /// <returns>The squared magnitude of the <see cref="Unhandled4"/>, as a <see cref="Scalar"/>.</returns>
    public Scalar PureSquaredMagnitude() => SquaredMagnitude().Magnitude;

    /// <summary>Computes the magnitude, or length, of the <see cref="Unhandled4"/>.</summary>
    /// <remarks>For improved performance, prefer <see cref="SquaredMagnitude"/> when applicable.</remarks>
    /// <returns>The magnitude of the <see cref="Unhandled4"/>.</returns>
    public Unhandled Magnitude() => SquaredMagnitude().SquareRoot();

    /// <summary>Computes the square of the magnitude, or length, of the <see cref="Unhandled4"/>.</summary>
    /// <returns>The squared magnitude of the <see cref="Unhandled4"/>.</returns>
    public Unhandled SquaredMagnitude() => Dot(this);

    /// <summary>Computes the normalized <see cref="Unhandled4"/> - the <see cref="Unhandled4"/> with the same direction, but magnitude { 1 }.</summary>
    /// <returns>The normalized <see cref="Unhandled4"/>.</returns>
    public Unhandled4 Normalize() => this / PureMagnitude();

    /// <summary>Determines whether the <see cref="Unhandled4"/> is equivalent to another, provided, <see cref="Unhandled4"/>.</summary>
    /// <param name="other">The <see cref="Unhandled4"/> to which the original <see cref="Unhandled4"/> is compared.</param>
    /// <returns>A <see cref="bool"/> indicating whether the two <see cref="Unhandled4"/> are equivalent.</returns>
    public bool Equals(Unhandled4 other) => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);

    /// <summary>Determines whether the provided <see cref="Unhandled4"/> are equivalent.</summary>
    /// <param name="lhs">The first of the two <see cref="Unhandled4"/> that are compared.</param>
    /// <param name="rhs">The second of the two <see cref="Unhandled4"/> that are compared.</param>
    /// <returns>A <see cref="bool"/> indicating whether the provided <see cref="Unhandled4"/> are equivalent.</returns>
    public static bool Equals(Unhandled4 lhs, Unhandled4 rhs) => lhs.Equals(rhs);

    /// <summary>Computes the <see cref="int"/> hash code describing the <see cref="Unhandled4"/>.</summary>
    /// <returns>The <see cref="int"/> hash code describing the <see cref="Unhandled4"/>.</returns>
    public override int GetHashCode() => (X, Y, Z, W).GetHashCode();

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Unhandled4"/>, formatted according to the <see cref="CultureInfo.CurrentCulture"/>.</summary>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Unhandled4"/>.</returns>
    public override string ToString() => ToString(CultureInfo.CurrentCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Unhandled4"/>, formatted according to the provided <see cref="IFormatProvider"/>.</summary>
    /// <param name="formatProvider">The <see cref="IFormatProvider"/>, providing culture-specific formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Unhandled4"/>.</returns>
    public string ToString(IFormatProvider? formatProvider) => ToString("G", formatProvider);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Unhandled4"/>, formatted according to the provided <see cref="string"/> and the <see cref="CultureInfo.CurrentCulture"/>.</summary>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Unhandled4"/>.</returns>
    public string ToString(string? format) => ToString(format, CultureInfo.CurrentCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Unhandled4"/>, formatted according to the provided <see cref="string"/> and <see cref="IFormatProvider"/>.</summary>
    /// <param name="formatProvider">The <see cref="IFormatProvider"/>, providing culture-specific formatting information.</param>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Unhandled4"/>.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (format is "G" or "g" or null)
        {
            format = "({0}, {1}, {2}, {3})";
        }

        return string.Format(formatProvider, format, X, Y, Z, W);
    }

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Unhandled4"/>, formatted according to the <see cref="CultureInfo.InvariantCulture"/>.</summary>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Unhandled4"/>.</returns>
    public string ToStringInvariant() => ToString(CultureInfo.InvariantCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Unhandled4"/>, formatted according to the provided <see cref="string"/> and the <see cref="CultureInfo.InvariantCulture"/>.</summary>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Unhandled4"/>.</returns>
    public string ToStringInvariant(string? format) => ToString(format, CultureInfo.InvariantCulture);

    /// <summary>Deconstructs the <see cref="Unhandled4"/> into the X and Y components.</summary>
    /// <param name="x">Assigned the X-component of the <see cref="Unhandled4"/>.</param>
    /// <param name="y">Assigned the Y-component of the <see cref="Unhandled4"/>.</param>
    /// <param name="z">Assigned the Z-component of the <see cref="Unhandled4"/>.</param>
    /// <param name="w">Assigned the W-component of the <see cref="Unhandled4"/>.</param>
    public void Deconstruct(out Unhandled x, out Unhandled y, out Unhandled z, out Unhandled w)
    {
        x = X;
        y = Y;
        z = Z;
        w = W;
    }

    /// <summary>Applies the unary plus to the <see cref="Unhandled4"/>.</summary>
    /// <returns>The same <see cref="Unhandled4"/>, { <see langword="this"/> }.</returns>
    public Unhandled4 Plus() => this;

    /// <summary>Negates the <see cref="Unhandled4"/>.</summary>
    /// <returns>The negated <see cref="Unhandled4"/>, { -<see langword="this"/> }.</returns>
    public Unhandled4 Negate() => (-X, -Y, -Z, -W);

    /// <summary>Computes the sum of the <see cref="Unhandled4"/> and the provided <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is added to the <see cref="Unhandled4"/>.</typeparam>
    /// <param name="addend">The <typeparamref name="TVector"/> that is added to the <see cref="Unhandled4"/>.</param>
    /// <returns>The sum of the <see cref="Unhandled4"/> and <typeparamref name="TVector"/>, { <see langword="this"/> + <paramref name="addend"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled4 Add<TVector>(TVector addend) where TVector : IVector4Quantity
    {
        ArgumentNullException.ThrowIfNull(addend);

        return (X + addend.X, Y + addend.Y, Z + addend.Z, W + addend.W);
    }

    /// <summary>Computes the difference between the <see cref="Unhandled4"/> and the provided <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is subtracted from the <see cref="Unhandled4"/>.</typeparam>
    /// <param name="subtrahend">The <typeparamref name="TVector"/> that is subtracted to the <see cref="Unhandled4"/>.</param>
    /// <returns>The difference between the <see cref="Unhandled4"/> and <typeparamref name="TVector"/>, { <see langword="this"/> - <paramref name="subtrahend"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled4 Subtract<TVector>(TVector subtrahend) where TVector : IVector4Quantity
    {
        ArgumentNullException.ThrowIfNull(subtrahend);

        return (X - subtrahend.X, Y - subtrahend.Y, Z - subtrahend.Z, W - subtrahend.W);
    }

    /// <summary>Scales the <see cref="Unhandled4"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="factor">The <see cref="Scalar"/> by which the <see cref="Unhandled4"/> is scaled.</param>
    /// <returns>The scaled <see cref="Unhandled4"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Unhandled4 Multiply(Scalar factor) => Multiply<Scalar>(factor);

    /// <summary>Computes the product of the <see cref="Unhandled4"/> and the provided <typeparamref name="TScalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity by which the <see cref="Unhandled4"/> is multiplied.</typeparam>
    /// <param name="factor">The <typeparamref name="TScalar"/> by which the <see cref="Unhandled4"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled4"/> and <typeparamref name="TScalar"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled4 Multiply<TScalar>(TScalar factor) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(factor);

        return (X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude, W * factor.Magnitude);
    }

    /// <summary>Scales the <see cref="Unhandled4"/> by the reciprocal of the provided <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Scalar"/>, the reciprocal of which scales the <see cref="Unhandled4"/></param>
    /// <returns>The scaled <see cref="Unhandled4"/>, { <see langword="this"/> / <paramref name="divisor"/> }.</returns>
    public Unhandled4 DivideBy(Scalar divisor) => DivideBy<Scalar>(divisor);

    /// <summary>Computes the quotient of the <see cref="Unhandled4"/> and the provided <typeparamref name="TScalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity by which the <see cref="Unhandled4"/> is divided.</typeparam>
    /// <param name="divisor">The <typeparamref name="TScalar"/> by which the <see cref="Unhandled4"/> is divided.</param>
    /// <returns>The quotient of the <see cref="Unhandled4"/> and <typeparamref name="TScalar"/>, { <see langword="this"/> / <paramref name="divisor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled4 DivideBy<TScalar>(TScalar divisor) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(divisor);

        return (X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude, W / divisor.Magnitude);
    }

    /// <summary>Computes the dot product of the <see cref="Unhandled4"/> and the provided <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity by which the <see cref="Unhandled4"/> is dot multiplied.</typeparam>
    /// <param name="factor">The <typeparamref name="TVector"/> by which the <see cref="Unhandled4"/> is dot multiplied.</param>
    /// <returns>The dot product of the <see cref="Unhandled4"/> and <typeparamref name="TVector"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled Dot<TVector>(TVector factor) where TVector : IVector4Quantity
    {
        ArgumentNullException.ThrowIfNull(factor);

        return (X * factor.X) + (Y * factor.Y) + (Z * factor.Z) + (W * factor.W);
    }

    /// <summary>Computes the sum of the provided <see cref="Unhandled4"/> and <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is added to the <see cref="Unhandled4"/>.</typeparam>
    /// <param name="a">The <see cref="Unhandled4"/> that is added to the <typeparamref name="TVector"/>.</param>
    /// <param name="b">The <typeparamref name="TVector"/> that is added to the <see cref="Unhandled4"/>.</param>
    /// <returns>The sum of the <see cref="Unhandled4"/> and <typeparamref name="TVector"/>, { <paramref name="a"/> + <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 Add<TVector>(Unhandled4 a, TVector b) where TVector : IVector4Quantity
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Add(b);
    }

    /// <summary>Computes the difference between the provided <see cref="Unhandled4"/> and <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is subtracted from the <see cref="Unhandled4"/>.</typeparam>
    /// <param name="a">The <see cref="Unhandled4"/>, from which the <typeparamref name="TVector"/> is subtracted.</param>
    /// <param name="b">The <typeparamref name="TVector"/>, that is subtracted from the <see cref="Unhandled4"/>.</param>
    /// <returns>The difference between the <see cref="Unhandled4"/> and <typeparamref name="TVector"/>, { <paramref name="a"/> - <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 Subtract<TVector>(Unhandled4 a, TVector b) where TVector : IVector4Quantity
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Subtract(b);
    }

    /// <summary>Scales the provided <see cref="Unhandled4"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Unhandled4"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/> by which the <see cref="Unhandled4"/> is scaled.</param>
    /// <returns>The scaled <see cref="Unhandled4"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Unhandled4 Multiply(Unhandled4 a, Scalar b) => a.Multiply(b);

    /// <summary>Computes the product of the provided <see cref="Unhandled4"/> and <typeparamref name="TScalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity by which the <see cref="Unhandled4"/> is multiplied.</typeparam>
    /// <param name="a">The <see cref="Unhandled4"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/> by which the <see cref="Unhandled4"/> is scaled.</param>
    /// <returns>The scaled <see cref="Unhandled4"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 Multiply<TScalar>(Unhandled4 a, TScalar b) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Multiply(b);
    }

    /// <summary>Scales the provided <see cref="Unhandled4"/> by the reciprocal of the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Unhandled4"/> that is scaled by the reciprocal of the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, the reciprocal of which scales the <see cref="Unhandled4"/></param>
    /// <returns>The scaled <see cref="Unhandled4"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    public static Unhandled4 Divide(Unhandled4 a, Scalar b) => a.DivideBy(b);

    /// <summary>Computes the quotient of the provided <see cref="Unhandled4"/> and <typeparamref name="TScalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity by which the <see cref="Unhandled4"/> is divided.</typeparam>
    /// <param name="a">The <see cref="Unhandled"/> that is divided by the <typeparamref name="TScalar"/>.</param>
    /// <param name="b">The <typeparamref name="TScalar"/> by which the <see cref="Unhandled4"/> is divided.</param>
    /// <returns>The quotient of the <see cref="Unhandled4"/> and <typeparamref name="TScalar"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 Divide<TScalar>(Unhandled4 a, TScalar b) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.DivideBy(b);
    }

    /// <summary>Computes the dot-product of the provided <see cref="Unhandled4"/> and <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity by which the <see cref="Unhandled4"/> is dot-multiplied.</typeparam>
    /// <param name="a">The <see cref="Unhandled4"/> by which the <typeparamref name="TVector"/> is dot-multiplied.</param>
    /// <param name="b">The <typeparamref name="TVector"/> by which the <see cref="Unhandled4"/> is dot-multiplied.</param>
    /// <returns>The dot-product of the <see cref="Unhandled4"/> and <typeparamref name="TVector"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled Dot<TVector>(Unhandled4 a, TVector b) where TVector : IVector4Quantity
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Dot(b);
    }

    /// <summary>Applies the unary plus to the provided <see cref="Unhandled4"/>.</summary>
    /// <param name="a">The <see cref="Unhandled4"/> to which the unary plus is applied.</param>
    /// <returns>The same <see cref="Unhandled4"/>, { <paramref name="a"/> }.</returns>
    public static Unhandled4 operator +(Unhandled4 a) => a.Plus();

    /// <summary>Negates the provided <see cref="Unhandled4"/>.</summary>
    /// <param name="a">The <see cref="Unhandled4"/> that is negated.</param>
    /// <returns>The negated <see cref="Unhandled4"/>, { -<paramref name="a"/> }.</returns>
    public static Unhandled4 operator -(Unhandled4 a) => a.Negate();

    /// <summary>Computes the sum of the provided <see cref="Unhandled4"/>.</summary>
    /// <param name="a">The first <see cref="Unhandled4"/>, added to the second <see cref="Unhandled4"/>.</param>
    /// <param name="b">The second <see cref="Unhandled4"/>, added to the first <see cref="Unhandled4"/>.</param>
    /// <returns>The sum of the <see cref="Unhandled4"/>, { <paramref name="a"/> + <paramref name="b"/> }.</returns>
    public static Unhandled4 operator +(Unhandled4 a, Unhandled4 b) => Add(a, b);

    /// <summary>Computes the difference between the provided <see cref="Unhandled4"/>.</summary>
    /// <param name="a">The first <see cref="Unhandled4"/>, from which the second <see cref="Unhandled4"/> is subtracted.</param>
    /// <param name="b">The second <see cref="Unhandled4"/>, which is subtracted from the first <see cref="Unhandled4"/>.</param>
    /// <returns>The difference between the two <see cref="Unhandled4"/>, { <paramref name="a"/> - <paramref name="b"/> }.</returns>
    public static Unhandled4 operator -(Unhandled4 a, Unhandled4 b) => Subtract(a, b);

    /// <summary>Computes the product of the provided <see cref="Unhandled4"/> and <see cref="Unhandled"/>.</summary>
    /// <param name="a">The <see cref="Unhandled4"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <param name="b">The <see cref="Unhandled"/> by which the <see cref="Unhandled4"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled4"/> and <see cref="Unhandled"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Unhandled4 operator *(Unhandled4 a, Unhandled b) => Multiply(a, b);

    /// <summary>Computes the product of the provided <see cref="Unhandled"/> and <see cref="Unhandled4"/>.</summary>
    /// <param name="a">The <see cref="Unhandled"/> by which the <see cref="Unhandled4"/> is multiplied.</param>
    /// <param name="b">The <see cref="Unhandled4"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled"/> and <see cref="Unhandled4"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Unhandled4 operator *(Unhandled a, Unhandled4 b) => Multiply(b, a);

    /// <summary>Computes the quotient of the provided <see cref="Unhandled4"/> and <see cref="Unhandled"/>.</summary>
    /// <param name="a">The <see cref="Unhandled4"/> that is divided by the <see cref="Unhandled"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> by which the <see cref="Unhandled4"/> is divided.</param>
    /// <returns>The quotient of the <see cref="Unhandled4"/> and <see cref="Unhandled"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    public static Unhandled4 operator /(Unhandled4 a, Unhandled b) => Divide(a, b);

    /// <inheritdoc cref="Multiply(Unhandled4, Scalar)"/>
    public static Unhandled4 operator *(Unhandled4 a, Scalar b) => Multiply(a, b);

    /// <summary>Scales the provided <see cref="Unhandled4"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Scalar"/> by which the <see cref="Unhandled4"/> is scaled.</param>
    /// <param name="b">The <see cref="Unhandled4"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Unhandled4"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Unhandled4 operator *(Scalar a, Unhandled4 b) => Multiply(b, a);

    /// <inheritdoc cref="Divide(Unhandled4, Scalar)"/>
    public static Unhandled4 operator /(Unhandled4 a, Scalar b) => Divide(a, b);

    /// <summary>Computes the sum of the provided <see cref="Unhandled4"/> and <see cref="IVector4Quantity"/>.</summary>
    /// <param name="a">The <see cref="Unhandled4"/> that is added to the <see cref="IVector4Quantity"/>.</param>
    /// <param name="b">The <see cref="IVector4Quantity"/> that is added to the <see cref="Unhandled4"/>.</param>
    /// <remarks>For improved performance, prefer <see cref="Add{TVector}(Unhandled4, TVector)"/> when the vector quantity is a <see langword="struct"/>.</remarks>
    /// <returns>The sum of the <see cref="Unhandled4"/> and <see cref="IVector4Quantity"/>, { <paramref name="a"/> + <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 operator +(Unhandled4 a, IVector4Quantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return Add(a, b);
    }

    /// <summary>Computes the sum of the provided <see cref="IVector4Quantity"/> and <see cref="Unhandled4"/>.</summary>
    /// <param name="a">The <see cref="IVector4Quantity"/> that is added to the <see cref="Unhandled4"/>.</param>
    /// <param name="b">The <see cref="Unhandled4"/> that is added to the <see cref="IVector4Quantity"/>.</param>
    /// <remarks>For improved performance, prefer <see cref="Add{TVector}(Unhandled4, TVector)"/> when the vector quantity is a <see langword="struct"/>.</remarks>
    /// <returns>The sum of the <see cref="IVector4Quantity"/> and <see cref="Unhandled4"/>, { <paramref name="a"/> + <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    [SuppressMessage("Major Code Smell", "S2234: Parameters should be passed in the correct order", Justification = "Addition is commutative.")]
    public static Unhandled4 operator +(IVector4Quantity a, Unhandled4 b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return Add(b, a);
    }

    /// <summary>Computes the difference between the provided <see cref="Unhandled4"/> and <see cref="IVector4Quantity"/>.</summary>
    /// <param name="a">The <see cref="Unhandled4"/> from which the <see cref="IVector4Quantity"/> is subtracted.</param>
    /// <param name="b">The <see cref="IVector4Quantity"/> that is subtracted from the <see cref="Unhandled4"/>.</param>
    /// <remarks>For improved performance, prefer <see cref="Subtract{TVector}(Unhandled4, TVector)"/> when the vector quantity is a <see langword="struct"/>.</remarks>
    /// <returns>The difference between the <see cref="Unhandled4"/> and <see cref="IVector4Quantity"/>, { <paramref name="a"/> - <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 operator -(Unhandled4 a, IVector4Quantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return Subtract(a, b);
    }

    /// <summary>Computes the difference between the provided <see cref="IVector4Quantity"/> and <see cref="Unhandled4"/>.</summary>
    /// <param name="a">The <see cref="IVector4Quantity"/> from which the <see cref="Unhandled4"/> is subtracted.</param>
    /// <param name="b">The <see cref="Unhandled4"/> that is subtracted from the <see cref="IVector4Quantity"/>.</param>
    /// <remarks>For improved performance, prefer -<see cref="Subtract{TVector}(Unhandled4, TVector)"/> when the vector quantity is a <see langword="struct"/>.</remarks>
    /// <returns>The difference between the <see cref="IVector4Quantity"/> and <see cref="Unhandled4"/>, { <paramref name="a"/> - <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    [SuppressMessage("Major Code Smell", "S2234: Parameters should be passed in the correct order", Justification = "Subtraction is anti-commutative.")]
    public static Unhandled4 operator -(IVector4Quantity a, Unhandled4 b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return -Subtract(b, a);
    }

    /// <summary>Computes the product of the provided <see cref="Unhandled4"/> and <see cref="IScalarQuantity"/>.</summary>
    /// <param name="a">The <see cref="Unhandled4"/> by which the <see cref="IScalarQuantity"/> is multiplied.</param>
    /// <param name="b">The <see cref="IScalarQuantity"/> by which the <see cref="Unhandled4"/> is multiplied.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply{TVector}(Unhandled4, TVector)"/> when the vector quantity is a <see langword="struct"/>.</remarks>
    /// <returns>The product of the <see cref="Unhandled4"/> and <see cref="IScalarQuantity"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 operator *(Unhandled4 a, IScalarQuantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return Multiply(a, b);
    }

    /// <summary>Computes the product of the provided <see cref="IScalarQuantity"/> and <see cref="Unhandled4"/>.</summary>
    /// <param name="a">The <see cref="IScalarQuantity"/> by which the <see cref="Unhandled4"/> is multiplied.</param>
    /// <param name="b">The <see cref="Unhandled4"/> by which the <see cref="IScalarQuantity"/> is multiplied.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply{TVector}(Unhandled4, TVector)"/> when the scalar quantity is a <see langword="struct"/>.</remarks>
    /// <returns>The product of the <see cref="IScalarQuantity"/> and <see cref="Unhandled4"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 operator *(IScalarQuantity a, Unhandled4 b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return Multiply(b, a);
    }

    /// <summary>Computes the element-wise quotient of the provided <see cref="Unhandled4"/> and <see cref="IScalarQuantity"/>.</summary>
    /// <param name="a">The <see cref="Unhandled4"/> that is divided by the <see cref="IScalarQuantity"/>.</param>
    /// <param name="b">The <see cref="IScalarQuantity"/> by which the <see cref="Unhandled4"/> is divided.</param>
    /// <remarks>For improved performance, prefer <see cref="DivideBy{TScalar}(TScalar)"/> when the scalar quantity is a <see langword="struct"/>.</remarks>
    /// <returns>The quotient of the <see cref="Unhandled4"/> and <see cref="IScalarQuantity"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled4 operator /(Unhandled4 a, IScalarQuantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return Divide(a, b);
    }

    /// <summary>Constructs an <see cref="Unhandled4"/>, representing the components of the provided <see cref="Unhandled"/>-tuple.</summary>
    /// <param name="components">The <see cref="Unhandled"/>-tuple describing the components of the constructed <see cref="Unhandled4"/>.</param>
    /// <returns>The constructed <see cref="Unhandled4"/>, representing the components of the <see cref="Unhandled"/>-tuple.</returns>
    public static Unhandled4 FromValueTuple((Unhandled X, Unhandled Y, Unhandled Z, Unhandled W) components) => new(components.X, components.Y, components.Z, components.W);

    /// <summary>Retrieves the <see cref="Unhandled"/>-tuple representing the components of the <see cref="Unhandled4"/>.</summary>
    /// <returns>The <see cref="Unhandled"/>-tuple representing the components of the <see cref="Unhandled4"/>.</returns>
    public (Unhandled X, Unhandled Y, Unhandled Z, Unhandled W) ToValueTuple() => (X, Y, Z, W);

    /// <summary>Constructs an <see cref="Unhandled3"/>, representing the components of the provided <see cref="Unhandled"/>-tuple.</summary>
    /// <param name="components">The <see cref="Unhandled"/>-tuple describing the components of the constructed <see cref="Unhandled4"/>.</param>
    /// <returns>The constructed <see cref="Unhandled3"/>, representing the components of the <see cref="Unhandled"/>-tuple.</returns>
    public static implicit operator Unhandled4((Unhandled X, Unhandled Y, Unhandled Z, Unhandled W) components) => FromValueTuple(components);

    /// <summary>Retrieves the <see cref="Unhandled"/>-tuple representing the components of the provided <see cref="Unhandled3"/>.</summary>
    /// <param name="vector">The <see cref="Unhandled3"/>, from which the <see cref="Unhandled"/>-tuple is retrieved.</param>
    /// <returns>The <see cref="Unhandled"/>-tuple representing the components of the <see cref="Unhandled3"/>.</returns>
    public static implicit operator (Unhandled X, Unhandled Y, Unhandled Z, Unhandled W)(Unhandled4 vector) => vector.ToValueTuple();
}
