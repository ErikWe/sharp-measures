namespace SharpMeasures;

using SharpMeasures.Maths;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>A measure of a scalar quantity that is not covered by a designated type.</summary>
public readonly record struct Unhandled : IScalarQuantity<Unhandled>, IComparable<Unhandled>
{
    /// <summary>The <see cref="Unhandled"/> representing { 0 }.</summary>
    public static Unhandled Zero { get; } = new(0);
    /// <summary>The <see cref="Unhandled"/> representing { 1 }.</summary>
    public static Unhandled One { get; } = new(1);

    /// <inheritdoc/>
    static Unhandled IScalarQuantity<Unhandled>.WithMagnitude(Scalar magnitude) => new(magnitude);

    /// <summary>The magnitude of <see langword="this"/>.</summary>
    public Scalar Magnitude { get; }

    /// <summary>Constructs a new <see cref="Unhandled"/> representing { <paramref name="magnitude"/> }.</summary>
    /// <param name="magnitude">The magnitude of the constructed <see cref="Unhandled"/>.</param>
    public Unhandled(Scalar magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Converts <see langword="this"/> to a scalar quantity <typeparamref name="TScalar"/> representing the same magnitude.</summary>
    /// <typeparam name="TScalar"><see langword="this"/> is converted to a scalar quantity of this type.</typeparam>
    public TScalar As<TScalar>() where TScalar : IScalarQuantity<TScalar> => TScalar.WithMagnitude(Magnitude);

    /// <inheritdoc cref="Scalar.IsNaN"/>
    public bool IsNaN => Magnitude.IsNaN;
    /// <inheritdoc cref="Scalar.IsZero"/>
    public bool IsZero => Magnitude.IsZero;
    /// <inheritdoc cref="Scalar.IsPositive"/>
    public bool IsPositive => Magnitude.IsPositive;
    /// <inheritdoc cref="Scalar.IsNegative"/>
    public bool IsNegative => Magnitude.IsNegative;
    /// <inheritdoc cref="Scalar.IsFinite"/>
    public bool IsFinite => Magnitude.IsFinite;
    /// <inheritdoc cref="Scalar.IsInfinite"/>
    public bool IsInfinite => Magnitude.IsInfinite;
    /// <inheritdoc cref="Scalar.IsPositiveInfinity"/>
    public bool IsPositiveInfinity => Magnitude.IsPositiveInfinity;
    /// <inheritdoc cref="Scalar.IsNegativeInfinity"/>v
    public bool IsNegativeInfinity => Magnitude.IsNegativeInfinity;

    /// <inheritdoc cref="Scalar.Absolute"/>
    public Unhandled Absolute() => ScalarMaths.Absolute(this);
    /// <inheritdoc cref="Scalar.Sign"/>
    public int Sign() => Math.Sign(Magnitude);

    /// <inheritdoc cref="Scalar.Power(Scalar)"/>
    public Unhandled Power(Scalar exponent) => new(Math.Pow(Magnitude, exponent));
    /// <inheritdoc cref="Scalar.Square"/>
    public Unhandled Square() => ScalarMaths.Square(this);
    /// <inheritdoc cref="Scalar.Cube"/>
    public Unhandled Cube() => ScalarMaths.Cube(this);
    /// <inheritdoc cref="Scalar.SquareRoot"/>
    public Unhandled SquareRoot() => ScalarMaths.SquareRoot(this);
    /// <inheritdoc cref="Scalar.CubeRoot"/>
    public Unhandled CubeRoot() => ScalarMaths.CubeRoot(this);

    /// <inheritdoc cref="Scalar.CompareTo(Scalar)"/>
    public int CompareTo(Unhandled other) => Magnitude.CompareTo(other.Magnitude);
    /// <summary>Produces a description of <see langword="this"/> containing the represented <see cref="Magnitude"/>.</summary>
    public override string ToString() => Magnitude.ToString();

    /// <inheritdoc/>
    public Unhandled Plus() => this;
    /// <inheritdoc/>
    public Unhandled Negate() => -this;

    /// <inheritdoc cref="Scalar.Add(Scalar)"/>
    public Unhandled Add(Unhandled addend) => this + addend;
    /// <inheritdoc cref="Scalar.Subtract(Scalar)"/>
    public Unhandled Subtract(Unhandled subtrahend) => this - subtrahend;
    /// <inheritdoc cref="Scalar.Multiply(Scalar)"/>
    public Unhandled Multiply(Unhandled factor) => this * factor;
    /// <inheritdoc cref="Scalar.Divide(Scalar)"/>
    public Unhandled Divide(Unhandled divisor) => this / divisor;

    /// <inheritdoc/>
    public Unhandled Multiply(Scalar factor) => this * factor;
    /// <inheritdoc/>
    public Unhandled Divide(Scalar divisor) => this / divisor;
    /// <summary>Computes { <paramref name="dividend"/> / <see langword="this"/> }.</summary>
    /// <param name="dividend">The dividend of { <paramref name="dividend"/> / <see langword="this"/> }.</param>
    public Unhandled DivideInto(Scalar dividend) => dividend / this;

    /// <summary>Computes { <see langword="this"/> ∙ <paramref name="factor"/> }.</summary>
    /// <param name="factor">The second factor of { <see langword="this"/> ∙ <paramref name="factor"/> }.</param>
    public Unhandled2 Multiply(Vector2 factor) => this * factor;
    /// <summary>Computes { <paramref name="dividend"/> / <see langword="this"/> }.</summary>
    /// <param name="dividend">The dividend of { <paramref name="dividend"/> / <see langword="this"/> }.</param>
    public Unhandled2 DivideInto(Vector2 dividend) => dividend / this;

    /// <inheritdoc cref="Multiply(Vector2)"/>
    public Unhandled3 Multiply(Vector3 factor) => this * factor;
    /// <inheritdoc cref="DivideInto(Vector2)"/>
    public Unhandled3 DivideInto(Vector3 dividend) => dividend / this;

    /// <inheritdoc cref="Multiply(Vector2)"/>
    public Unhandled4 Multiply(Vector4 factor) => this * factor;
    /// <inheritdoc cref="DivideInto(Vector2)"/>
    public Unhandled4 DivideInto(Vector4 dividend) => dividend / this;

    /// <summary>Computes { <see langword="this"/> + <paramref name="addend"/> }.</summary>
    /// <param name="addend">The second term of { <see langword="this"/> + <paramref name="addend"/> }.</param>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled Add<TScalar>(TScalar addend) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(addend);

        return new(Magnitude + addend.Magnitude);
    }

    /// <summary>Computes { <see langword="this"/> - <paramref name="subtrahend"/> }.</summary>
    /// <param name="subtrahend">The second term of { <see langword="this"/> - <paramref name="subtrahend"/> }.</param>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled Subtract<TScalar>(TScalar subtrahend) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(subtrahend);

        return new(Magnitude - subtrahend.Magnitude);
    }

    /// <summary>Computes { <paramref name="minuend"/> - <see langword="this"/> }.</summary>
    /// <param name="minuend">The first term of { <paramref name="minuend"/> - <see langword="this"/> }.</param>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled SubtractFrom<TScalar>(TScalar minuend) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(minuend);

        return new(minuend.Magnitude - Magnitude);
    }

    /// <summary>Computes { <see langword="this"/> ∙ <paramref name="factor"/> }.</summary>
    /// <param name="factor">The second factor of { <see langword="this"/> ∙ <paramref name="factor"/> }.</param>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled Multiply<TScalar>(TScalar factor) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(factor);

        return new(Magnitude * factor.Magnitude);
    }

    /// <summary>Computes { <see langword="this"/> / <paramref name="divisor"/> }.</summary>
    /// <param name="divisor">The divisor of { <see langword="this"/> / <paramref name="divisor"/> }.</param>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled Divide<TScalar>(TScalar divisor) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(divisor);

        return new(Magnitude / divisor.Magnitude);
    }

    /// <summary>Computes { <paramref name="dividend"/> / <see langword="this"/> }.</summary>
    /// <param name="dividend">The divisor of { <paramref name="dividend"/> / <see langword="this"/> }.</param>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled DivideInto<TScalar>(TScalar dividend) where TScalar : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(dividend);

        return new(dividend.Magnitude / Magnitude);
    }

    /// <inheritdoc/>
    public static Unhandled operator +(Unhandled x) => x;
    /// <inheritdoc/>
    public static Unhandled operator -(Unhandled x) => new(-x.Magnitude);

    /// <inheritdoc cref="Scalar.operator +(Scalar, Scalar)"/>
    public static Unhandled operator +(Unhandled x, Unhandled y) => new(x.Magnitude + y.Magnitude);
    /// <inheritdoc cref="Scalar.operator -(Scalar, Scalar)"/>
    public static Unhandled operator -(Unhandled x, Unhandled y) => new(x.Magnitude - y.Magnitude);
    /// <inheritdoc cref="Scalar.operator *(Scalar, Scalar)"/>
    public static Unhandled operator *(Unhandled x, Unhandled y) => new(x.Magnitude * y.Magnitude);
    /// <inheritdoc cref="Scalar.operator /(Scalar, Scalar)"/>
    public static Unhandled operator /(Unhandled x, Unhandled y) => new(x.Magnitude / y.Magnitude);

    /// <inheritdoc/>
    public static Unhandled operator *(Unhandled x, Scalar y) => new(x.Magnitude * y);
    /// <inheritdoc/>
    public static Unhandled operator *(Scalar x, Unhandled y) => new(x * y.Magnitude);
    /// <inheritdoc/>
    public static Unhandled operator /(Unhandled x, Scalar y) => new(x.Magnitude / y);
    /// <summary>Computes { <paramref name="x"/> / <paramref name="y"/> }.</summary>
    /// <param name="x">The dividend of { <paramref name="x"/> / <paramref name="y"/> }.</param>
    /// <param name="y">The divisor of { <paramref name="x"/> / <paramref name="y"/> }.</param>
    public static Unhandled operator /(Scalar x, Unhandled y) => new(x / y.Magnitude);

    /// <summary>Computes { <paramref name="a"/> ∙ <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    public static Unhandled2 operator *(Unhandled a, Vector2 b) => (a * b.X, a * b.Y);
    /// <summary>Computes { <paramref name="a"/> ∙ <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> ∙ <paramref name="b"/> }.</param>
    public static Unhandled2 operator *(Vector2 a, Unhandled b) => (a.X * b, a.Y * b);
    /// <summary>Computes { <paramref name="a"/> / <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    public static Unhandled2 operator /(Vector2 a, Unhandled b) => (a.X / b, a.Y / b);

    /// <inheritdoc cref="operator *(Unhandled, Vector2)"/>
    public static Unhandled3 operator *(Unhandled a, Vector3 b) => (a * b.X, a * b.Y, a * b.Z);
    /// <inheritdoc cref="operator *(Vector2, Unhandled)"/>
    public static Unhandled3 operator *(Vector3 a, Unhandled b) => (a.X * b, a.Y * b, a.Z * b);
    /// <inheritdoc cref="operator /(Vector2, Unhandled)"/>
    public static Unhandled3 operator /(Vector3 a, Unhandled b) => (a.X / b, a.Y / b, a.Z / b);

    /// <inheritdoc cref="operator *(Unhandled, Vector2)"/>
    public static Unhandled4 operator *(Unhandled a, Vector4 b) => (a * b.X, a * b.Y, a * b.Z, a * b.W);
    /// <inheritdoc cref="operator *(Vector2, Unhandled)"/>
    public static Unhandled4 operator *(Vector4 a, Unhandled b) => (a.X * b, a.Y * b, a.Z * b, a.W * b);
    /// <inheritdoc cref="operator /(Vector2, Unhandled)"/>
    public static Unhandled4 operator /(Vector4 a, Unhandled b) => (a.X / b, a.Y / b, a.Z / b, a.W / b);

    /// <inheritdoc cref="operator +(Unhandled, Unhandled)"/>
    /// <remarks>Consider preferring <see cref="Add{TScalar}(TScalar)"/>, where boxing is avoided.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator +(Unhandled x, IScalarQuantity y)
    {
        ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude + y.Magnitude);
    }

    /// <inheritdoc cref="operator +(Unhandled, Unhandled)"/>
    /// <remarks>Consider preferring <see cref="Add{TScalar}(TScalar)"/>, where boxing is avoided.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator +(IScalarQuantity x, Unhandled y)
    {
        ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude + y.Magnitude);
    }

    /// <inheritdoc cref="operator -(Unhandled, Unhandled)"/>
    /// <remarks>Consider preferring <see cref="Subtract{TScalar}(TScalar)"/>, where boxing is avoided.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator -(Unhandled x, IScalarQuantity y)
    {
        ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude - y.Magnitude);
    }

    /// <inheritdoc cref="operator -(Unhandled, Unhandled)"/>
    /// <remarks>Consider preferring <see cref="SubtractFrom{TScalar}(TScalar)"/>, where boxing is avoided.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator -(IScalarQuantity x, Unhandled y)
    {
        ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude - y.Magnitude);
    }

    /// <inheritdoc cref="operator *(Unhandled, Unhandled)"/>
    /// <remarks>Consider preferring <see cref="Multiply{TScalar}(TScalar)"/>, where boxing is avoided.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(Unhandled x, IScalarQuantity y)
    {
        ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude * y.Magnitude);
    }

    /// <inheritdoc cref="operator *(Unhandled, IScalarQuantity)"/>
    /// <remarks>Consider preferring <see cref="Multiply{TScalar}(TScalar)"/>, where boxing is avoided.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator *(IScalarQuantity x, Unhandled y)
    {
        ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude * y.Magnitude);
    }

    /// <inheritdoc cref="operator /(Unhandled, Unhandled)"/>
    /// <remarks>Consider preferring <see cref="Divide{TScalar}(TScalar)"/>, where boxing is avoided.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(Unhandled x, IScalarQuantity y)
    {
        ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude / y.Magnitude);
    }

    /// <inheritdoc cref="operator /(Unhandled, Unhandled)"/>
    /// <remarks>Consider preferring <see cref="DivideInto{TScalar}(TScalar)"/>, where boxing is avoided.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled operator /(IScalarQuantity x, Unhandled y)
    {
        ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude / y.Magnitude);
    }

    /// <summary>Computes { <paramref name="a"/> * <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> * <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> * <paramref name="b"/> }.</param>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 operator *(Unhandled a, IVector2Quantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return new(a * b.X, a * b.Y);
    }

    /// <inheritdoc cref="operator *(Unhandled, IVector2Quantity)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 operator *(IVector2Quantity a, Unhandled b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return new(a.X * b, a.Y * b);
    }

    /// <summary>Computes { <paramref name="a"/> / <paramref name="b"/> }.</summary>
    /// <param name="a">The dividend of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    /// <param name="b">The divisor of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled2 operator /(IVector2Quantity a, Unhandled b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return new(a.X / b, a.Y / b);
    }

    /// <summary>Computes { <paramref name="a"/> * <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> * <paramref name="b"/> }.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> * <paramref name="b"/> }.</param>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(Unhandled a, IVector3Quantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return new(a * b.X, a * b.Y, a * b.Z);
    }

    /// <inheritdoc cref="operator *(Unhandled, IVector3Quantity)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(IVector3Quantity a, Unhandled b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return new(a.X * b, a.Y * b, a.Z * b);
    }

    /// <summary>Computes { <paramref name="a"/> / <paramref name="b"/> }.</summary>
    /// <param name="a">The dividend of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    /// <param name="b">The divisor of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator /(IVector3Quantity a, Unhandled b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return new(a.X / b, a.Y / b, a.Z / b);
    }

    /// <summary>Produces the <see cref="Scalar"/> representing <paramref name="x"/>.</summary>
    [SuppressMessage("Usage", "CA2225", Justification = "Available as constructor")]
    public static explicit operator Unhandled(Scalar x) => new(x);

    /// <summary>Produces the <see cref="double"/> equivalent to <paramref name="x"/>.</summary>
    [SuppressMessage("Usage", "CA2225", Justification = "Available as 'Magnitude'")]
    public static explicit operator Scalar(Unhandled x) => x.Magnitude;

    /// <inheritdoc cref="Scalar.operator &lt;(Scalar, Scalar)"/>
    public static bool operator <(Unhandled x, Unhandled y) => x.Magnitude.Value < y.Magnitude.Value;
    /// <inheritdoc cref="Scalar.operator &gt;(Scalar, Scalar)"/>
    public static bool operator >(Unhandled x, Unhandled y) => x.Magnitude.Value > y.Magnitude.Value;
    /// <inheritdoc cref="Scalar.operator &lt;=(Scalar, Scalar)"/>
    public static bool operator <=(Unhandled x, Unhandled y) => x.Magnitude.Value <= y.Magnitude.Value;
    /// <inheritdoc cref="Scalar.operator &gt;=(Scalar, Scalar)"/>
    public static bool operator >=(Unhandled x, Unhandled y) => x.Magnitude.Value >= y.Magnitude.Value;

    /// <summary>Describes mathematical operations that result in <see cref="Unhandled"/>.</summary>
    private static IScalarResultingMaths<Unhandled> ScalarMaths { get; } = MathFactory.ScalarResult<Unhandled>();
}
