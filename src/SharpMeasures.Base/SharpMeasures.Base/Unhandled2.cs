namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

/// <summary>A measure of some two-dimensional vector quantity not covered by a designated type.</summary>
public readonly record struct Unhandled2 : IVector2Quantity<Unhandled2>, IFormattable
{
    /// <summary>The <see cref="Unhandled2"/> representing { 0, 0 }.</summary>
    public static Unhandled2 Zero { get; } = new(0, 0);

    /// <summary>The X-component of the <see cref="Unhandled2"/>.</summary>
    public Unhandled X { get; }

    /// <summary>The Y-component of the <see cref="Unhandled2"/>.</summary>
    public Unhandled Y { get; }

    Scalar IVector2Quantity.X => X.Magnitude;
    Scalar IVector2Quantity.Y => Y.Magnitude;

    /// <summary>The magnitudes of the X and Y components of the <see cref="Unhandled2"/>.</summary>
    public Vector2 Components => (X.Magnitude, Y.Magnitude);

    /// <summary>Instantiates an <see cref="Unhandled2"/>, representing a measure of some two-dimensional vector quantity not covered by a designated type.</summary>
    /// <param name="x">The X-component of the constructed <see cref="Unhandled2"/>.</param>
    /// <param name="y">The Y-component of the constructed <see cref="Unhandled2"/>.</param>
    public Unhandled2(Unhandled x, Unhandled y)
    {
        X = x;
        Y = y;
    }

    /// <summary>Instantiates an <see cref="Unhandled2"/>, representing a measure of some two-dimensional vector quantity not covered by a designated type.</summary>
    /// <param name="x">The magnitude of the X-component of the constructed <see cref="Unhandled2"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the constructed <see cref="Unhandled2"/>.</param>
    public Unhandled2(Scalar x, Scalar y)
    {
        X = new(x);
        Y = new(y);
    }

    /// <summary>Instantiates an <see cref="Unhandled2"/>, representing a measure of some two-dimensional vector quantity not covered by a designated type.</summary>
    /// <param name="components">The magnitudes of the components of the constructed <see cref="Unhandled2"/>.</param>
    public Unhandled2(Vector2 components)
    {
        X = new(components.X);
        Y = new(components.Y);
    }

    static Unhandled2 IVector2Quantity<Unhandled2>.WithComponents(Vector2 components) => new(components);
    static Unhandled2 IVector2Quantity<Unhandled2>.WithComponents(Scalar x, Scalar y) => new(x, y);

    /// <summary>Indicates whether either of the X and Y components of the <see cref="Unhandled2"/> represent { <see cref="Scalar.NaN"/> }.</summary>
    public bool IsNaN => X.IsNaN || Y.IsNaN;

    /// <summary>Indicates whether the <see cref="Unhandled2"/> represents { 0, 0 }.</summary>
    public bool IsZero => X.IsZero && Y.IsZero;

    /// <summary>Indicates whether both the X and Y components of the <see cref="Unhandled2"/> represent finite values.</summary>
    public bool IsFinite => X.IsFinite && Y.IsFinite;

    /// <summary>Indicates whether either of the X and Y components of the <see cref="Unhandled2"/> represent an infinite value.</summary>
    public bool IsInfinite => X.IsInfinite || Y.IsInfinite;

    Scalar IVectorQuantity.Magnitude() => PureMagnitude();
    Scalar IVectorQuantity.SquaredMagnitude() => PureSquaredMagnitude();

    /// <summary>Computes the magnitude, or length, of the <see cref="Unhandled2"/> - represented as a <see cref="Scalar"/>.</summary>
    /// <remarks>For improved performance, prefer <see cref="PureSquaredMagnitude"/> when applicable.</remarks>
    /// <returns>The magnitude of the <see cref="Unhandled2"/>, as a <see cref="Scalar"/>.</returns>
    public Scalar PureMagnitude() => PureSquaredMagnitude().SquareRoot();

    /// <summary>Computes the square of the magnitude, or length, of the <see cref="Unhandled2"/> - represented as a <see cref="Scalar"/>.</summary>
    /// <returns>The squared magnitude of the <see cref="Unhandled2"/>, as a <see cref="Scalar"/>.</returns>
    public Scalar PureSquaredMagnitude() => SquaredMagnitude().Magnitude;

    /// <summary>Computes the magnitude, or length, of the <see cref="Unhandled2"/>.</summary>
    /// <remarks>For improved performance, prefer <see cref="SquaredMagnitude"/> when applicable.</remarks>
    /// <returns>The magnitude of the <see cref="Unhandled2"/>.</returns>
    public Unhandled Magnitude() => SquaredMagnitude().SquareRoot();

    /// <summary>Computes the square of the magnitude, or length, of the <see cref="Unhandled2"/>.</summary>
    /// <returns>The squared magnitude of the <see cref="Unhandled2"/>.</returns>
    public Unhandled SquaredMagnitude() => Dot(this);

    /// <summary>Computes the normalized <see cref="Unhandled2"/> - the <see cref="Unhandled2"/> with the same direction, but magnitude { 1 }.</summary>
    /// <returns>The normalized <see cref="Unhandled2"/>.</returns>
    public Unhandled2 Normalize() => this / PureMagnitude();

    /// <summary>Determines whether the <see cref="Unhandled2"/> is equivalent to another, provided, <see cref="Unhandled2"/>.</summary>
    /// <param name="other">The <see cref="Unhandled2"/> to which the original <see cref="Unhandled2"/> is compared.</param>
    /// <returns>A <see cref="bool"/> indicating whether the two <see cref="Unhandled2"/> are equivalent.</returns>
    public bool Equals(Unhandled2 other) => X.Equals(other.X) && Y.Equals(other.Y);

    /// <summary>Determines whether the provided <see cref="Unhandled2"/> are equivalent.</summary>
    /// <param name="lhs">The first of the two <see cref="Unhandled2"/> that are compared.</param>
    /// <param name="rhs">The second of the two <see cref="Unhandled2"/> that are compared.</param>
    /// <returns>A <see cref="bool"/> indicating whether the provided <see cref="Unhandled2"/> are equivalent.</returns>
    public static bool Equals(Unhandled2 lhs, Unhandled2 rhs) => lhs.Equals(rhs);

    /// <summary>Computes the <see cref="int"/> hash code describing the <see cref="Unhandled2"/>.</summary>
    /// <returns>The <see cref="int"/> hash code describing the <see cref="Unhandled2"/>.</returns>
    public override int GetHashCode() => (X, Y).GetHashCode();

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Unhandled2"/>, formatted according to the <see cref="CultureInfo.CurrentCulture"/>.</summary>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Unhandled2"/>.</returns>
    public override string ToString() => ToString(CultureInfo.CurrentCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Unhandled2"/>, formatted according to the provided <see cref="IFormatProvider"/>.</summary>
    /// <param name="formatProvider">The <see cref="IFormatProvider"/>, providing culture-specific formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Unhandled2"/>.</returns>
    public string ToString(IFormatProvider? formatProvider) => ToString("G", formatProvider);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Unhandled2"/>, formatted according to the provided <see cref="string"/> and the <see cref="CultureInfo.CurrentCulture"/>.</summary>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Unhandled2"/>.</returns>
    public string ToString(string? format) => ToString(format, CultureInfo.CurrentCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Unhandled2"/>, formatted according to the provided <see cref="string"/> and <see cref="IFormatProvider"/>.</summary>
    /// <param name="formatProvider">The <see cref="IFormatProvider"/>, providing culture-specific formatting information.</param>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Unhandled2"/>.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (format is "G" or "g" or null)
        {
            format = "({0}, {1})";
        }

        return string.Format(formatProvider, format, X, Y);
    }

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Unhandled2"/>, formatted according to the <see cref="CultureInfo.InvariantCulture"/>.</summary>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Unhandled2"/>.</returns>
    public string ToStringInvariant() => ToString(CultureInfo.InvariantCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="Unhandled2"/>, formatted according to the provided <see cref="string"/> and the <see cref="CultureInfo.InvariantCulture"/>.</summary>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <returns>A <see cref="string"/>-representation of the <see cref="Unhandled2"/>.</returns>
    public string ToStringInvariant(string? format) => ToString(format, CultureInfo.InvariantCulture);

    /// <summary>Deconstructs the <see cref="Unhandled2"/> into the X and Y components.</summary>
    /// <param name="x">Assigned the X-component of the <see cref="Unhandled2"/>.</param>
    /// <param name="y">Assigned the Y-component of the <see cref="Unhandled2"/>.</param>
    public void Deconstruct(out Unhandled x, out Unhandled y)
    {
        x = X;
        y = Y;
    }

    /// <summary>Applies the unary plus to the <see cref="Unhandled2"/>.</summary>
    /// <returns>The same <see cref="Unhandled2"/>, { <see langword="this"/> }.</returns>
    public Unhandled2 Plus() => this;

    /// <summary>Negates the <see cref="Unhandled2"/>.</summary>
    /// <returns>The negated <see cref="Unhandled2"/>, { -<see langword="this"/> }.</returns>
    public Unhandled2 Negate() => (-X, -Y);

    /// <summary>Computes the sum of the <see cref="Unhandled2"/> and the provided <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is added to the <see cref="Unhandled2"/>.</typeparam>
    /// <param name="addend">The <typeparamref name="TVector"/> that is added to the <see cref="Unhandled2"/>.</param>
    /// <returns>The sum of the <see cref="Unhandled2"/> and <typeparamref name="TVector"/>, { <see langword="this"/> + <paramref name="addend"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled2 Add<TVector>(TVector addend) where TVector : IVector2Quantity
    {
        ArgumentNullException.ThrowIfNull(addend);

        return (X + addend.X, Y + addend.Y);
    }

    /// <summary>Computes the difference between the <see cref="Unhandled2"/> and the provided <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is subtracted from the <see cref="Unhandled2"/>.</typeparam>
    /// <param name="subtrahend">The <typeparamref name="TVector"/> that is subtracted to the <see cref="Unhandled2"/>.</param>
    /// <returns>The difference between the <see cref="Unhandled2"/> and <typeparamref name="TVector"/>, { <see langword="this"/> - <paramref name="subtrahend"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled2 Subtract<TVector>(TVector subtrahend) where TVector : IVector2Quantity
    {
        ArgumentNullException.ThrowIfNull(subtrahend);

        return (X - subtrahend.X, Y - subtrahend.Y);
    }

    /// <summary>Scales the <see cref="Unhandled2"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="factor">The <see cref="Scalar"/> by which the <see cref="Unhandled2"/> is scaled.</param>
    /// <returns>The scaled <see cref="Unhandled2"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    public Unhandled2 Multiply(Scalar factor) => Multiply<Scalar>(factor);

    /// <summary>Computes the product of the <see cref="Unhandled2"/> and the provided <typeparamref name="TScalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity by which the <see cref="Unhandled2"/> is multiplied.</typeparam>
    /// <param name="factor">The <typeparamref name="TScalar"/> by which the <see cref="Unhandled2"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled2"/> and <typeparamref name="TScalar"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled2 Multiply<TScalar>(TScalar factor) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(factor);

        return (X * factor.Magnitude, Y * factor.Magnitude);
    }

    /// <summary>Scales the <see cref="Unhandled2"/> by the reciprocal of the provided <see cref="Scalar"/>.</summary>
    /// <param name="divisor">The <see cref="Scalar"/>, the reciprocal of which scales the <see cref="Unhandled2"/></param>
    /// <returns>The scaled <see cref="Unhandled2"/>, { <see langword="this"/> / <paramref name="divisor"/> }.</returns>
    public Unhandled2 DivideBy(Scalar divisor) => DivideBy<Scalar>(divisor);

    /// <summary>Computes the quotient of the <see cref="Unhandled2"/> and the provided <typeparamref name="TScalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity by which the <see cref="Unhandled2"/> is divided.</typeparam>
    /// <param name="divisor">The <typeparamref name="TScalar"/> by which the <see cref="Unhandled2"/> is divided.</param>
    /// <returns>The quotient of the <see cref="Unhandled2"/> and <typeparamref name="TScalar"/>, { <see langword="this"/> / <paramref name="divisor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled2 DivideBy<TScalar>(TScalar divisor) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(divisor);

        return (X / divisor.Magnitude, Y / divisor.Magnitude);
    }

    /// <summary>Computes the dot-product of the <see cref="Unhandled2"/> and the provided <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity by which the <see cref="Unhandled2"/> is dot-multiplied.</typeparam>
    /// <param name="factor">The <typeparamref name="TVector"/> by which the <see cref="Unhandled2"/> is dot-multiplied.</param>
    /// <returns>The dot-product of the <see cref="Unhandled2"/> and <typeparamref name="TVector"/>, { <see langword="this"/> ∙ <paramref name="factor"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled Dot<TVector>(TVector factor) where TVector : IVector2Quantity
    {
        ArgumentNullException.ThrowIfNull(factor);

        return (X * factor.X) + (Y * factor.Y);
    }

    /// <summary>Computes the sum of the provided <see cref="Unhandled2"/> and <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is added to the <see cref="Unhandled2"/>.</typeparam>
    /// <param name="a">The <see cref="Unhandled2"/> that is added to the <typeparamref name="TVector"/>.</param>
    /// <param name="b">The <typeparamref name="TVector"/> that is added to the <see cref="Unhandled2"/>.</param>
    /// <returns>The sum of the <see cref="Unhandled2"/> and <typeparamref name="TVector"/>, { <paramref name="a"/> + <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 Add<TVector>(Unhandled2 a, TVector b) where TVector : IVector2Quantity
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Add(b);
    }

    /// <summary>Computes the difference between the provided <see cref="Unhandled2"/> and <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity that is subtracted from the <see cref="Unhandled2"/>.</typeparam>
    /// <param name="a">The <see cref="Unhandled2"/>, from which the <typeparamref name="TVector"/> is subtracted.</param>
    /// <param name="b">The <typeparamref name="TVector"/>, that is subtracted from the <see cref="Unhandled2"/>.</param>
    /// <returns>The difference between the <see cref="Unhandled2"/> and <typeparamref name="TVector"/>, { <paramref name="a"/> - <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 Subtract<TVector>(Unhandled2 a, TVector b) where TVector : IVector2Quantity
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Subtract(b);
    }

    /// <summary>Scales the provided <see cref="Unhandled2"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Unhandled2"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/> by which the <see cref="Unhandled2"/> is scaled.</param>
    /// <returns>The scaled <see cref="Unhandled2"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Unhandled2 Multiply(Unhandled2 a, Scalar b) => a.Multiply(b);

    /// <summary>Computes the product of the provided <see cref="Unhandled2"/> and <typeparamref name="TScalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity by which the <see cref="Unhandled2"/> is multiplied.</typeparam>
    /// <param name="a">The <see cref="Unhandled2"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/> by which the <see cref="Unhandled2"/> is scaled.</param>
    /// <returns>The scaled <see cref="Unhandled2"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 Multiply<TScalar>(Unhandled2 a, TScalar b) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Multiply(b);
    }

    /// <summary>Scales the provided <see cref="Unhandled2"/> by the reciprocal of the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Unhandled2"/> that is scaled by the reciprocal of the <see cref="Scalar"/>.</param>
    /// <param name="b">The <see cref="Scalar"/>, the reciprocal of which scales the <see cref="Unhandled2"/></param>
    /// <returns>The scaled <see cref="Unhandled2"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    public static Unhandled2 Divide(Unhandled2 a, Scalar b) => a.DivideBy(b);

    /// <summary>Computes the quotient of the provided <see cref="Unhandled2"/> and <typeparamref name="TScalar"/>.</summary>
    /// <typeparam name="TScalar">The type of the scalar quantity by which the <see cref="Unhandled2"/> is divided.</typeparam>
    /// <param name="a">The <see cref="Unhandled"/> that is divided by the <typeparamref name="TScalar"/>.</param>
    /// <param name="b">The <typeparamref name="TScalar"/> by which the <see cref="Unhandled2"/> is divided.</param>
    /// <returns>The quotient of the <see cref="Unhandled2"/> and <typeparamref name="TScalar"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 Divide<TScalar>(Unhandled2 a, TScalar b) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.DivideBy(b);
    }

    /// <summary>Computes the dot-product of the provided <see cref="Unhandled2"/> and <typeparamref name="TVector"/>.</summary>
    /// <typeparam name="TVector">The type of the vector quantity by which the <see cref="Unhandled2"/> is dot-multiplied.</typeparam>
    /// <param name="a">The <see cref="Unhandled2"/> by which the <typeparamref name="TVector"/> is dot-multiplied.</param>
    /// <param name="b">The <typeparamref name="TVector"/> by which the <see cref="Unhandled2"/> is dot-multiplied.</param>
    /// <returns>The dot-product of the <see cref="Unhandled2"/> and <typeparamref name="TVector"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled Dot<TVector>(Unhandled2 a, TVector b) where TVector : IVector2Quantity
    {
        ArgumentNullException.ThrowIfNull(b);

        return a.Dot(b);
    }

    /// <summary>Applies the unary plus to the provided <see cref="Unhandled2"/>.</summary>
    /// <param name="a">The <see cref="Unhandled2"/> to which the unary plus is applied.</param>
    /// <returns>The same <see cref="Unhandled2"/>, { <paramref name="a"/> }.</returns>
    public static Unhandled2 operator +(Unhandled2 a) => a.Plus();

    /// <summary>Negates the provided <see cref="Unhandled2"/>.</summary>
    /// <param name="a">The <see cref="Unhandled2"/> that is negated.</param>
    /// <returns>The negated <see cref="Unhandled2"/>, { -<paramref name="a"/> }.</returns>
    public static Unhandled2 operator -(Unhandled2 a) => a.Negate();

    /// <summary>Computes the sum of the provided <see cref="Unhandled2"/>.</summary>
    /// <param name="a">The first <see cref="Unhandled2"/>, which is added to the second <see cref="Unhandled2"/>.</param>
    /// <param name="b">The second <see cref="Unhandled2"/>, which is added to the first <see cref="Unhandled2"/>.</param>
    /// <returns>The sum of the <see cref="Unhandled2"/>, { <paramref name="a"/> + <paramref name="b"/> }.</returns>
    public static Unhandled2 operator +(Unhandled2 a, Unhandled2 b) => Add(a, b);

    /// <summary>Computes the difference between the provided <see cref="Unhandled2"/>.</summary>
    /// <param name="a">The first <see cref="Unhandled2"/>, from which the second <see cref="Unhandled2"/> is subtracted.</param>
    /// <param name="b">The second <see cref="Unhandled2"/>, which is subtracted from the first <see cref="Unhandled2"/>.</param>
    /// <returns>The difference between the <see cref="Unhandled2"/>, { <paramref name="a"/> - <paramref name="b"/> }.</returns>
    public static Unhandled2 operator -(Unhandled2 a, Unhandled2 b) => Subtract(a, b);

    /// <summary>Computes the product of the provided <see cref="Unhandled2"/> and <see cref="Unhandled"/>.</summary>
    /// <param name="a">The <see cref="Unhandled2"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <param name="b">The <see cref="Unhandled"/> by which the <see cref="Unhandled2"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled2"/> and <see cref="Unhandled"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Unhandled2 operator *(Unhandled2 a, Unhandled b) => Multiply(a, b);

    /// <summary>Computes the product of the provided <see cref="Unhandled"/> and <see cref="Unhandled2"/>.</summary>
    /// <param name="a">The <see cref="Unhandled"/> by which the <see cref="Unhandled2"/> is multiplied.</param>
    /// <param name="b">The <see cref="Unhandled2"/> by which the <see cref="Unhandled"/> is multiplied.</param>
    /// <returns>The product of the <see cref="Unhandled"/> and <see cref="Unhandled2"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Unhandled2 operator *(Unhandled a, Unhandled2 b) => Multiply(b, a);

    /// <summary>Computes the quotient of the provided <see cref="Unhandled2"/> and <see cref="Unhandled"/>.</summary>
    /// <param name="a">The <see cref="Unhandled2"/> that is divided by the <see cref="Unhandled"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> by which the <see cref="Unhandled2"/> is divided.</param>
    /// <returns>The quotient of the <see cref="Unhandled2"/> and <see cref="Unhandled"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    public static Unhandled2 operator /(Unhandled2 a, Unhandled b) => Divide(a, b);

    /// <inheritdoc cref="Multiply(Unhandled2, Scalar)"/>
    public static Unhandled2 operator *(Unhandled2 a, Scalar b) => Multiply(a, b);

    /// <summary>Scales the provided <see cref="Unhandled2"/> by the provided <see cref="Scalar"/>.</summary>
    /// <param name="a">The <see cref="Scalar"/> by which the <see cref="Unhandled2"/> is scaled.</param>
    /// <param name="b">The <see cref="Unhandled2"/> that is scaled by the <see cref="Scalar"/>.</param>
    /// <returns>The scaled <see cref="Unhandled2"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    public static Unhandled2 operator *(Scalar a, Unhandled2 b) => Multiply(b, a);

    /// <inheritdoc cref="Divide(Unhandled2, Scalar)"/>
    public static Unhandled2 operator /(Unhandled2 a, Scalar b) => Divide(a, b);

    /// <summary>Computes the sum of the provided <see cref="Unhandled2"/> and <see cref="IVector2Quantity"/>.</summary>
    /// <param name="a">The <see cref="Unhandled2"/> that is added to the <see cref="IVector2Quantity"/>.</param>
    /// <param name="b">The <see cref="IVector2Quantity"/> that is added to the <see cref="Unhandled2"/>.</param>
    /// <remarks>For improved performance, prefer <see cref="Add{TVector}(Unhandled2, TVector)"/> when the vector quantity is a <see langword="struct"/>.</remarks>
    /// <returns>The sum of the <see cref="Unhandled2"/> and <see cref="IVector2Quantity"/>, { <paramref name="a"/> + <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 operator +(Unhandled2 a, IVector2Quantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return Add(a, b);
    }

    /// <summary>Computes the sum of the provided <see cref="IVector2Quantity"/> and <see cref="Unhandled2"/>.</summary>
    /// <param name="a">The <see cref="IVector2Quantity"/> that is added to the <see cref="Unhandled2"/>.</param>
    /// <param name="b">The <see cref="Unhandled2"/> that is added to the <see cref="IVector2Quantity"/>.</param>
    /// <remarks>For improved performance, prefer <see cref="Add{TVector}(Unhandled2, TVector)"/> when the vector quantity is a <see langword="struct"/>.</remarks>
    /// <returns>The sum of the <see cref="IVector2Quantity"/> and <see cref="Unhandled2"/>, { <paramref name="a"/> + <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    [SuppressMessage("Major Code Smell", "S2234: Parameters should be passed in the correct order", Justification = "Addition is commutative.")]
    public static Unhandled2 operator +(IVector2Quantity a, Unhandled2 b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return Add(b, a);
    }

    /// <summary>Computes the difference between the provided <see cref="Unhandled2"/> and <see cref="IVector2Quantity"/>.</summary>
    /// <param name="a">The <see cref="Unhandled2"/> from which the <see cref="IVector2Quantity"/> is subtracted.</param>
    /// <param name="b">The <see cref="IVector2Quantity"/> that is subtracted from the <see cref="Unhandled2"/>.</param>
    /// <remarks>For improved performance, prefer <see cref="Subtract{TVector}(Unhandled2, TVector)"/> when the vector quantity is a <see langword="struct"/>.</remarks>
    /// <returns>The difference between the <see cref="Unhandled2"/> and <see cref="IVector2Quantity"/>, { <paramref name="a"/> - <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 operator -(Unhandled2 a, IVector2Quantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return Subtract(a, b);
    }

    /// <summary>Computes the difference between the provided <see cref="IVector2Quantity"/> and <see cref="Unhandled2"/>.</summary>
    /// <param name="a">The <see cref="IVector2Quantity"/> from which the <see cref="Unhandled2"/> is subtracted.</param>
    /// <param name="b">The <see cref="Unhandled2"/> that is subtracted from the <see cref="IVector2Quantity"/>.</param>
    /// <remarks>For improved performance, prefer -<see cref="Subtract{TVector}(Unhandled2, TVector)"/> when the vector quantity is a <see langword="struct"/>.</remarks>
    /// <returns>The difference between the <see cref="IVector2Quantity"/> and <see cref="Unhandled2"/>, { <paramref name="a"/> - <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    [SuppressMessage("Major Code Smell", "S2234: Parameters should be passed in the correct order", Justification = "Subtraction is anti-commutative.")]
    public static Unhandled2 operator -(IVector2Quantity a, Unhandled2 b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return -Subtract(b, a);
    }

    /// <summary>Computes the product of the provided <see cref="Unhandled2"/> and <see cref="IScalarQuantity"/>.</summary>
    /// <param name="a">The <see cref="Unhandled2"/> by which the <see cref="IScalarQuantity"/> is multiplied.</param>
    /// <param name="b">The <see cref="IScalarQuantity"/> by which the <see cref="Unhandled2"/> is multiplied.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply{TScalar}(Unhandled2, TScalar)"/> when the scalar quantity is a <see langword="struct"/>.</remarks>
    /// <returns>The product of the <see cref="Unhandled2"/> and <see cref="IScalarQuantity"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 operator *(Unhandled2 a, IScalarQuantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return Multiply(a, b);
    }

    /// <summary>Computes the product of the provided <see cref="IScalarQuantity"/> and <see cref="Unhandled2"/>.</summary>
    /// <param name="a">The <see cref="IScalarQuantity"/> by which the <see cref="Unhandled2"/> is multiplied.</param>
    /// <param name="b">The <see cref="Unhandled2"/> by which the <see cref="IScalarQuantity"/> is multiplied.</param>
    /// <remarks>For improved performance, prefer <see cref="Multiply{TScalar}(Unhandled2, TScalar)"/> when the scalar quantity is a <see langword="struct"/>.</remarks>
    /// <returns>The product of the <see cref="IScalarQuantity"/> and <see cref="Unhandled2"/>, { <paramref name="a"/> ∙ <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 operator *(IScalarQuantity a, Unhandled2 b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return Multiply(b, a);
    }

    /// <summary>Computes the element-wise quotient of the provided <see cref="Unhandled2"/> and <see cref="IScalarQuantity"/>.</summary>
    /// <param name="a">The <see cref="Unhandled2"/> that is divided by the <see cref="IScalarQuantity"/>.</param>
    /// <param name="b">The <see cref="IScalarQuantity"/> by which the <see cref="Unhandled2"/> is divided.</param>
    /// <remarks>For improved performance, prefer <see cref="Divide{TScalar}(Unhandled2, TScalar)"/> when the scalar quantity is a <see langword="struct"/>.</remarks>
    /// <returns>The quotient of the <see cref="Unhandled2"/> and <see cref="IScalarQuantity"/>, { <paramref name="a"/> / <paramref name="b"/> }.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 operator /(Unhandled2 a, IScalarQuantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return Divide(a, b);
    }

    /// <summary>Constructs an <see cref="Unhandled2"/>, representing the components of the provided <see cref="Unhandled"/>-tuple.</summary>
    /// <param name="components">The <see cref="Unhandled"/>-tuple describing the components of the constructed <see cref="Unhandled2"/>.</param>
    /// <returns>The constructed <see cref="Unhandled2"/>, representing the components of the <see cref="Unhandled"/>-tuple.</returns>
    public static Unhandled2 FromValueTuple((Unhandled X, Unhandled Y) components) => new(components.X, components.Y);

    /// <summary>Retrieves the <see cref="Unhandled"/>-tuple representing the components of the <see cref="Unhandled2"/>.</summary>
    /// <returns>The <see cref="Unhandled"/>-tuple representing the components of the <see cref="Unhandled2"/>.</returns>
    public (Unhandled X, Unhandled Y) ToValueTuple() => (X, Y);

    /// <summary>Constructs an <see cref="Unhandled2"/>, representing the components of the provided <see cref="Unhandled"/>-tuple.</summary>
    /// <param name="components">The <see cref="Unhandled"/>-tuple describing the components of the constructed <see cref="Unhandled2"/>.</param>
    /// <returns>The constructed <see cref="Unhandled2"/>, representing the components of the <see cref="Unhandled"/>-tuple.</returns>
    public static implicit operator Unhandled2((Unhandled X, Unhandled Y) components) => FromValueTuple(components);

    /// <summary>Retrieves the <see cref="Unhandled"/>-tuple representing the components of the provided <see cref="Unhandled2"/>.</summary>
    /// <param name="vector">The <see cref="Unhandled2"/>, from which the <see cref="Unhandled"/>-tuple is retrieved.</param>
    /// <returns>The <see cref="Unhandled"/>-tuple representing the components of the <see cref="Unhandled2"/>.</returns>
    public static implicit operator (Unhandled X, Unhandled Y)(Unhandled2 vector) => vector.ToValueTuple();
}
