namespace SharpMeasures;

using SharpMeasures.Maths;

using System.Diagnostics.CodeAnalysis;

/// <summary>A pure two-dimensional vector, representing { X, Y }.</summary>
public readonly record struct Vector2 : IVector2Quantity<Vector2>
{
    /// <summary>The <see cref="Vector2"/> representing { 0, 0 }.</summary>
    public static Vector2 Zero { get; } = (0, 0);
    /// <summary>The <see cref="Vector2"/> representing { 1, 1 }.</summary>
    public static Vector2 Ones { get; } = (1, 1);

    /// <inheritdoc/>
    static Vector2 IVector2Quantity<Vector2>.WithComponents(Vector2 components) => components;
    /// <inheritdoc/>
    static Vector2 IVector2Quantity<Vector2>.WithComponents(Scalar x, Scalar y) => (x, y);

    /// <summary>The X-component of <see langword="this"/>.</summary>
    public Scalar X { get; }
    /// <summary>The Y-component of <see langword="this"/>.</summary>
    public Scalar Y { get; }

    /// <inheritdoc/>
    Vector2 IVector2Quantity.Components => this;

    /// <summary>Constructs a new <see cref="Vector2"/> representing { <paramref name="x"/>, <paramref name="y"/> }.</summary>
    /// <param name="x">The X-component of the constructed <see cref="Vector2"/>.</param>
    /// <param name="y">The Y-component of the constructed <see cref="Vector2"/>.</param>
    public Vector2(Scalar x, Scalar y)
    {
        X = x;
        Y = y;
    }

    /// <summary>Indicates whether either of the X and Y components represented by <see langword="this"/> represents { <see cref="double.NaN"/> }.</summary>
    public bool IsNaN => X.IsNaN || Y.IsNaN;
    /// <summary>Indicates whether <see langword="this"/> represents { 0, 0 }.</summary>
    public bool IsZero => (X.Value, Y.Value) is (0, 0);
    /// <summary>Indicates whether the X and Y components represented by <see langword="this"/> represent finite values.</summary>
    public bool IsFinite => X.IsFinite && Y.IsFinite;
    /// <summary>Indicates whether either of the X and Y components represented by <see langword="this"/> represents an infinite value.</summary>
    public bool IsInfinite => X.IsInfinite || Y.IsInfinite;

    /// <inheritdoc/>
    public Scalar Magnitude() => ScalarMaths.Magnitude2(this);
    /// <inheritdoc/>
    public Scalar SquaredMagnitude() => ScalarMaths.SquaredMagnitude2(this);

    /// <inheritdoc/>
    public Vector2 Normalize() => VectorMaths.Normalize(this);

    /// <summary>Produces a description of <see langword="this"/> containing the represented (<see cref="X"/>, <see cref="Y"/>).</summary>
    public override string ToString() => $"({X.Value}, {Y.Value})";

    /// <summary>Deconstructs <see langword="this"/> into the components (<see cref="X"/>, <see cref="Y"/>).</summary>
    /// <param name="x">The X-component of <see langword="this"/>.</param>
    /// <param name="y">The Y-component of <see langword="this"/>.</param>
    public void Deconstruct(out Scalar x, out Scalar y)
    {
        x = X;
        y = Y;
    }

    /// <inheritdoc/>
    public Vector2 Plus() => this;
    /// <inheritdoc/>
    public Vector2 Negate() => -this;

    /// <summary>Computes { <see langword="this"/> + <paramref name="addend"/> }.</summary>
    /// <param name="addend">The second term of { <see langword="this"/> + <paramref name="addend"/> }.</param>
    public Vector2 Add(Vector2 addend) => this + addend;
    /// <summary>Computes { <see langword="this"/> - <paramref name="subtrahend"/> }.</summary>
    /// <param name="subtrahend">The second term of { <see langword="this"/> - <paramref name="subtrahend"/> }.</param>
    public Vector2 Subtract(Vector2 subtrahend) => this - subtrahend;
    /// <inheritdoc/>
    public Vector2 Multiply(Scalar factor) => this * factor;
    /// <inheritdoc/>
    public Vector2 Divide(Scalar divisor) => this / divisor;
    /// <inheritdoc cref="Scalar.Remainder(Scalar)"/>
    public Vector2 Remainder(Scalar divisor) => this % divisor;
    /// <summary>Computes { <see langword="this"/> ∙ <paramref name="factor"/> }.</summary>
    /// <param name="factor">The second factor of { <see langword="this"/> ∙ <paramref name="factor"/> }.</param>
    public Scalar Dot(Vector2 factor) => ScalarMaths.Dot2(this, factor);

    /// <inheritdoc/>
    public static Vector2 operator +(Vector2 a) => a;
    /// <inheritdoc/>
    public static Vector2 operator -(Vector2 a) => (-a.X, -a.Y);
    /// <summary>Computes { <paramref name="a"/> + <paramref name="b"/> }.</summary>
    /// <param name="a">The first term of { <paramref name="a"/> + <paramref name="b"/> }.</param>
    /// <param name="b">The second term of { <paramref name="a"/> + <paramref name="b"/> }.</param>
    public static Vector2 operator +(Vector2 a, Vector2 b) => (a.X + b.X, a.Y + b.Y);
    /// <summary>Computes { <paramref name="a"/> - <paramref name="b"/> }.</summary>
    /// <param name="a">The first term of { <paramref name="a"/> - <paramref name="b"/> }.</param>
    /// <param name="b">The second term of { <paramref name="a"/> - <paramref name="b"/> }.</param>
    public static Vector2 operator -(Vector2 a, Vector2 b) => (a.X - b.X, a.Y - b.Y);

    /// <inheritdoc/>
    public static Vector2 operator *(Vector2 a, Scalar b) => (a.X * b, a.Y * b);
    /// <inheritdoc/>
    public static Vector2 operator *(Scalar a, Vector2 b) => (a * b.X, a * b.Y);
    /// <inheritdoc/>
    public static Vector2 operator /(Vector2 a, Scalar b) => (a.X / b, a.Y / b);
    /// <summary>Computes { <paramref name="a"/> % <paramref name="b"/> }.</summary>
    /// <param name="a">The dividend of { <paramref name="a"/> % <paramref name="b"/> }.</param>
    /// <param name="b">The divisor of { <paramref name="a"/> % <paramref name="b"/> }.</param>
    public static Vector2 operator %(Vector2 a, Scalar b) => (a.X % b, a.Y % b);

    /// <summary>Constructs the <see cref="Vector2"/> with the elements of <paramref name="components"/> as components.</summary>
    [SuppressMessage("Usage", "CA2225", Justification = "Behaviour can be achieved through a constructor")]
    public static implicit operator Vector2((Scalar X, Scalar Y) components) => new(components.X, components.Y);

    /// <summary>Constructs the tuple (<see cref="Scalar"/>, <see cref="Scalar"/>) with the elements of <paramref name="vector"/>.</summary>
    [SuppressMessage("Usage", "CA2225", Justification = "Behaviour can be achieved through the deconstructor")]
    public static implicit operator (Scalar X, Scalar Y)(Vector2 vector) => new(vector.X, vector.Y);

    /// <summary>Describes mathematical operations that result in a pure <see cref="Scalar"/>.</summary>
    private static IScalarResultingMaths<Scalar> ScalarMaths { get; } = MathFactory.ScalarResult();
    /// <summary>Describes mathematical operations that result in a pure <see cref="Vector2"/>.</summary>
    private static IVector2ResultingMaths<Vector2> VectorMaths { get; } = MathFactory.Vector2Result();
}
