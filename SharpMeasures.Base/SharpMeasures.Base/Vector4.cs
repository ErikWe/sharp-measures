namespace SharpMeasures;

using SharpMeasures.Maths;

using System.Diagnostics.CodeAnalysis;

/// <summary>A pure three-dimensional vector, representing { X, Y, Z, W }.</summary>
public readonly record struct Vector4 : IVector4Quantity<Vector4>
{
    /// <summary>The <see cref="Vector4"/> representing { 0, 0, 0, 0 }.</summary>
    public static Vector4 Zero { get; } = (0, 0, 0, 0);
    /// <summary>The <see cref="Vector4"/> representing { 1, 1, 1, 1 }.</summary>
    public static Vector4 Ones { get; } = (1, 1, 1, 1);

    /// <inheritdoc/>
    static Vector4 IVector4Quantity<Vector4>.WithComponents(Vector4 components) => components;
    /// <inheritdoc/>
    static Vector4 IVector4Quantity<Vector4>.WithComponents(Scalar x, Scalar y, Scalar z, Scalar w) => (x, y, z, w);

    /// <summary>The X-component of <see langword="this"/>.</summary>
    public Scalar X { get; }
    /// <summary>The Y-component of <see langword="this"/>.</summary>
    public Scalar Y { get; }
    /// <summary>The Z-component of <see langword="this"/>.</summary>
    public Scalar Z { get; }
    /// <summary>The W-component of <see langword="this"/>.</summary>
    public Scalar W { get; }

    /// <inheritdoc/>
    Vector4 IVector4Quantity.Components => this;

    /// <summary>Constructs a new <see cref="Vector4"/> representing { <paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>, <paramref name="w"/> }.</summary>
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

    /// <summary>Indicates whether any of the X, Y, Z, or W components represented by <see langword="this"/> represents { <see cref="double.NaN"/> }.</summary>
    public bool IsNaN => X.IsNaN || Y.IsNaN || Z.IsNaN || W.IsNaN;
    /// <summary>Indicates whether <see langword="this"/> represents { 0, 0, 0 }.</summary>
    public bool IsZero => (X.Value, Y.Value, Z.Value, W.Value) is (0, 0, 0, 0);
    /// <summary>Indicates whether the X, Y, Z, and W components represented by <see langword="this"/> represent finite values.</summary>
    public bool IsFinite => X.IsFinite && Y.IsFinite && Z.IsFinite && W.IsFinite;
    /// <summary>Indicates whether any of the X, Y, Z or W components represented by <see langword="this"/> represents an infinite value.</summary>
    public bool IsInfinite => X.IsInfinite || Y.IsInfinite || Z.IsInfinite || W.IsInfinite;

    /// <inheritdoc/>
    public Scalar Magnitude() => ScalarMaths.Magnitude4(this);
    /// <inheritdoc/>
    public Scalar SquaredMagnitude() => ScalarMaths.SquaredMagnitude4(this);

    /// <inheritdoc/>
    public Vector4 Normalize() => VectorMaths.Normalize(this);

    /// <summary>Produces a description of <see langword="this"/> containing the represented components.</summary>
    public override string ToString() => $"({X.Value}, {Y.Value}, {Z.Value}, {W.Value})";

    /// <summary>Deconstructs <see langword="this"/> into the components X, Y, Z, and W.</summary>
    /// <param name="x">The X-component of <see langword="this"/>.</param>
    /// <param name="y">The Y-component of <see langword="this"/>.</param>
    /// <param name="z">The Z-component of <see langword="this"/>.</param>
    /// <param name="w">The W-component of <see langword="this"/>.</param>
    public void Deconstruct(out Scalar x, out Scalar y, out Scalar z, out  Scalar w)
    {
        x = X;
        y = Y;
        z = Z;
        w = W;
    }

    /// <inheritdoc/>
    public Vector4 Plus() => this;
    /// <inheritdoc/>
    public Vector4 Negate() => -this;

    /// <summary>Computes { <see langword="this"/> + <paramref name="addend"/> }.</summary>
    /// <param name="addend">The second term of { <see langword="this"/> + <paramref name="addend"/> }.</param>
    public Vector4 Add(Vector4 addend) => this + addend;
    /// <summary>Computes { <see langword="this"/> - <paramref name="subtrahend"/> }.</summary>
    /// <param name="subtrahend">The second term of { <see langword="this"/> - <paramref name="subtrahend"/> }.</param>
    public Vector4 Subtract(Vector4 subtrahend) => this - subtrahend;
    /// <inheritdoc/>
    public Vector4 Multiply(Scalar factor) => this * factor;
    /// <inheritdoc/>
    public Vector4 Divide(Scalar divisor) => this / divisor;
    /// <inheritdoc cref="Scalar.Remainder(Scalar)"/>
    public Vector4 Remainder(Scalar divisor) => this % divisor;
    /// <summary>Computes { <see langword="this"/> ∙ <paramref name="factor"/> }.</summary>
    /// <param name="factor">The second factor of { <see langword="this"/> ∙ <paramref name="factor"/> }.</param>
    public Scalar Dot(Vector4 factor) => ScalarMaths.Dot4(this, factor);

    /// <inheritdoc/>
    public static Vector4 operator +(Vector4 a) => a;
    /// <inheritdoc/>
    public static Vector4 operator -(Vector4 a) => (-a.X, -a.Y, -a.Z, -a.W);
    /// <summary>Computes { <paramref name="a"/> + <paramref name="b"/> }.</summary>
    /// <param name="a">The first term of { <paramref name="a"/> + <paramref name="b"/> }.</param>
    /// <param name="b">The second term of { <paramref name="a"/> + <paramref name="b"/> }.</param>
    public static Vector4 operator +(Vector4 a, Vector4 b) => (a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
    /// <summary>Computes { <paramref name="a"/> - <paramref name="b"/> }.</summary>
    /// <param name="a">The first term of { <paramref name="a"/> - <paramref name="b"/> }.</param>
    /// <param name="b">The second term of { <paramref name="a"/> - <paramref name="b"/> }.</param>
    public static Vector4 operator -(Vector4 a, Vector4 b) => (a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);

    /// <inheritdoc/>
    public static Vector4 operator *(Vector4 a, Scalar b) => (a.X * b, a.Y * b, a.Z * b, a.W * b);
    /// <inheritdoc/>
    public static Vector4 operator *(Scalar a, Vector4 b) => (a * b.X, a * b.Y, a * b.Z, a * b.W);
    /// <inheritdoc/>
    public static Vector4 operator /(Vector4 a, Scalar b) => (a.X / b, a.Y / b, a.Z / b, a.W / b);
    /// <summary>Computes { <paramref name="a"/> % <paramref name="b"/> }.</summary>
    /// <param name="a">The dividend of { <paramref name="a"/> % <paramref name="b"/> }.</param>
    /// <param name="b">The divisor of { <paramref name="a"/> % <paramref name="b"/> }.</param>
    public static Vector4 operator %(Vector4 a, Scalar b) => (a.X % b, a.Y % b, a.Z % b, a.W % b);

    /// <summary>Constructs the <see cref="Vector4"/> with the elements of <paramref name="components"/> as components.</summary>
    [SuppressMessage("Usage", "CA2225", Justification = "Behaviour can be achieved through a constructor")]
    public static implicit operator Vector4((Scalar X, Scalar Y, Scalar Z, Scalar W) components) => new(components.X, components.Y, components.Z, components.W);

    /// <summary>Constructs the tuple (<see cref="Scalar"/>, <see cref="Scalar"/>, <see cref="Scalar"/>, <see cref="Scalar"/>) with the elements of <paramref name="vector"/>.</summary>
    [SuppressMessage("Usage", "CA2225", Justification = "Behaviour can be achieved through the deconstructor")]
    public static implicit operator (Scalar X, Scalar Y, Scalar Z, Scalar W)(Vector4 vector) => new(vector.X, vector.Y, vector.Z, vector.W);

    /// <summary>Describes mathematical operations that result in a pure <see cref="Scalar"/>.</summary>
    private static IScalarResultingMaths<Scalar> ScalarMaths { get; } = MathFactory.ScalarResult();
    /// <summary>Describes mathematical operations that result in a pure <see cref="Vector4"/>.</summary>
    private static IVector4ResultingMaths<Vector4> VectorMaths { get; } = MathFactory.Vector4Result();
}
