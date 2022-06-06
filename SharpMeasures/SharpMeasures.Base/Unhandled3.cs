namespace SharpMeasures;

using SharpMeasures.Maths;
using SharpMeasures.Vector3Abstractions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

/// <summary>A measure of a three-dimensional vector quantity that is not covered by a designated type.</summary>
public readonly record struct Unhandled3 :
    IVector3Quantity<Unhandled3>,
    IAddendVector3Quantity<Unhandled3>,
    IMinuendVector3Quantity<Unhandled3>,
    ISubtrahendVector3Quantity<Unhandled3>,
    IFactorVector3Quantity<Unhandled3, Unhandled3, Unhandled>,
    IDividendVector3Quantity<Unhandled3, Unhandled3, Unhandled>,
    IDotFactorVector3Quantity<Unhandled, Unhandled3>,
    ICrossFactorVector3Quantity<Unhandled3, Unhandled3>,
    IAddendVector3Quantity<Unhandled3, Unhandled3, IVector3Quantity>,
    IMinuendVector3Quantity<Unhandled3, Unhandled3, IVector3Quantity>,
    ISubtrahendVector3Quantity<Unhandled3, Unhandled3, IVector3Quantity>,
    IFactorVector3Quantity<Unhandled3, Unhandled3, IScalarQuantity>,
    IDividendVector3Quantity<Unhandled3, Unhandled3, IScalarQuantity>,
    IDotFactorVector3Quantity<Unhandled, IVector3Quantity>,
    ICrossFactorVector3Quantity<Unhandled3, IVector3Quantity>
{
    /// <summary>The <see cref="Unhandled3"/> representing { 0, 0, 0 }.</summary>
    public static readonly Unhandled3 Zero = new(0, 0, 0);
    /// <summary>The <see cref="Unhandled3"/> representing { 1, 1, 1 }.</summary>
    public static readonly Unhandled3 Ones = new(1, 1, 1);

    /// <inheritdoc/>
    static Unhandled3 IVector3Quantity<Unhandled3>.WithComponents(Vector3 components) => new(components);
    /// <inheritdoc/>
    static Unhandled3 IVector3Quantity<Unhandled3>.WithComponents(Scalar x, Scalar y, Scalar z) => new(x, y, z);

    /// <summary>The X-component of <see langword="this"/>.</summary>
    public Unhandled X { get; }
    /// <summary>The Y-component of <see langword="this"/>.</summary>
    public Unhandled Y { get; }
    /// <summary>The Z-component of <see langword="this"/>.</summary>
    public Unhandled Z { get; }

    /// <inheritdoc/>
    Scalar IVector3Quantity.X => X.Magnitude;
    /// <inheritdoc/>
    Scalar IVector3Quantity.Y => Y.Magnitude;
    /// <inheritdoc/>
    Scalar IVector3Quantity.Z => Z.Magnitude;

    /// <inheritdoc/>
    public Vector3 Components => (X.Magnitude, Y.Magnitude, Z.Magnitude);

    /// <summary>Constructs a new <see cref="Unhandled3"/> representing { <paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/> }.</summary>
    /// <param name="x">The X-component by the constructed <see cref="Unhandled3"/>.</param>
    /// <param name="y">The Y-component of the constructed <see cref="Unhandled3"/>.</param>
    /// <param name="z">The Z-component of the constructed <see cref="Unhandled3"/>.</param>
    public Unhandled3(Unhandled x, Unhandled y, Unhandled z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>Constructs a new <see cref="Unhandled3"/> with components of magnitudes { <paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/> }.</summary>
    /// <param name="x">The magnitude of the X-component of the constructed <see cref="Unhandled3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the constructed <see cref="Unhandled3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the constructed <see cref="Unhandled3"/>.</param>
    public Unhandled3(Scalar x, Scalar y, Scalar z) : this(new Unhandled(x), new Unhandled(y), new Unhandled(z)) { }

    /// <summary>Constructs a new <see cref="Unhandled3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the constructed <see cref="Unhandled3"/>.</param>
    public Unhandled3(Vector3 components) : this(components.X, components.Y, components.Z) { }

    /// <inheritdoc/>
    Scalar IVector3Quantity.Magnitude() => PureScalarMaths.Magnitude3(this);
    /// <inheritdoc/>
    Scalar IVector3Quantity.SquaredMagnitude() => PureScalarMaths.SquaredMagnitude3(this);

    /// <summary>Computes the magnitude / norm / length of <see langword="this"/>.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when applicable.</remarks>
    public Unhandled Magnitude() => ScalarMaths.Magnitude3(this);
    /// <summary>Computes the square of the magnitude / norm / length of <see langword="this"/>.</summary>
    public Unhandled SquaredMagnitude() => ScalarMaths.SquaredMagnitude3(this);

    /// <inheritdoc/>
    public Unhandled3 Normalize() => VectorMaths.Normalize(this);
    /// <inheritdoc/>
    public Unhandled3 Transform(Matrix4x4 transform) => VectorMaths.Transform(this, transform);

    /// <summary>Produces a description of <see langword="this"/> containing the type and the represented components.</summary>
    public override string ToString() => $"{nameof(Unhandled3)}: ({X.Magnitude.Value}, {Y.Magnitude.Value}, {Z.Magnitude.Value})";

    /// <summary>Deconstructs <see langword="this"/> into the components X, Y, and Z.</summary>
    /// <param name="x">The X-component of <see langword="this"/>.</param>
    /// <param name="y">The Y-component of <see langword="this"/>.</param>
    /// <param name="z">The Z-component of <see langword="this"/>.</param>
    public void Deconstruct(out Unhandled x, out Unhandled y, out Unhandled z)
    {
        x = X;
        y = Y;
        z = Z;
    }

    /// <inheritdoc/>
    public Unhandled3 Plus() => this;
    /// <inheritdoc/>
    public Unhandled3 Negate() => -this;

    /// <inheritdoc/>
    public Unhandled3 Add(Unhandled3 addend) => this + addend;
    /// <inheritdoc/>
    public Unhandled3 Subtract(Unhandled3 subtrahend) => this - subtrahend;
    /// <inheritdoc/>
    Unhandled3 ISubtrahendVector3Quantity<Unhandled3, Unhandled3>.SubtractFrom(Unhandled3 minuend) => minuend - this;
    /// <inheritdoc/>
    public Unhandled3 Multiply(Unhandled factor) => this * factor;
    /// <inheritdoc/>
    public Unhandled3 Divide(Unhandled divisor) => this / divisor;
    /// <inheritdoc/>
    public Unhandled Dot(Unhandled3 factor) => ScalarMaths.Dot3(this, factor);
    /// <inheritdoc/>
    public Unhandled3 Cross(Unhandled3 factor) => VectorMaths.Cross(this, factor);
    /// <inheritdoc/>
    Unhandled3 ICrossFactorVector3Quantity<Unhandled3, Unhandled3>.CrossInto(Unhandled3 factor) => VectorMaths.Cross(factor, this);

    /// <inheritdoc/>
    public Unhandled3 Multiply(Scalar factor) => this * factor;
    /// <inheritdoc/>
    public Unhandled3 Divide(Scalar divisor) => this / divisor;
    /// <inheritdoc/>
    public Unhandled3 Remainder(Scalar divisor) => this % divisor;

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    Unhandled3 IAddendVector3Quantity<Unhandled3, IVector3Quantity>.Add(IVector3Quantity addend) => this + addend;
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    Unhandled3 IMinuendVector3Quantity<Unhandled3, IVector3Quantity>.Subtract(IVector3Quantity subtrahend) => this - subtrahend;
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    Unhandled3 ISubtrahendVector3Quantity<Unhandled3, IVector3Quantity>.SubtractFrom(IVector3Quantity minuend) => minuend - this;
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    Unhandled3 IFactorVector3Quantity<Unhandled3, IScalarQuantity>.Multiply(IScalarQuantity factor) => this * factor;
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    Unhandled3 IDividendVector3Quantity<Unhandled3, IScalarQuantity>.Divide(IScalarQuantity divisor) => this / divisor;
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    Unhandled IDotFactorVector3Quantity<Unhandled, IVector3Quantity>.Dot(IVector3Quantity factor) => ScalarMaths.Dot3(this, factor);
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    Unhandled3 ICrossFactorVector3Quantity<Unhandled3, IVector3Quantity>.Cross(IVector3Quantity factor) => VectorMaths.Cross(this, factor);
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    Unhandled3 ICrossFactorVector3Quantity<Unhandled3, IVector3Quantity>.CrossInto(IVector3Quantity factor) => VectorMaths.Cross(factor, this);

    /// <inheritdoc cref="IAddendVector3Quantity{TSum, TAddend}.Add(TAddend)"/>
    /// <typeparam name="TAddend">The three-dimensional vector quantity that represents the second addend of { <see langword="this"/> + <typeparamref name="TAddend"/> }.</typeparam>
    public Unhandled3 Add<TAddend>(TAddend addend) where TAddend : IVector3Quantity => new(Components + addend.Components);

    /// <inheritdoc cref="IMinuendVector3Quantity{TSum, TSubtrahend}.Subtract(TSubtrahend)"/>
    /// <typeparam name="TSubtrahend">The three-dimensional vector quantity that represents the subtrahend of { <see langword="this"/> -
    /// <typeparamref name="TSubtrahend"/> }.</typeparam>
    public Unhandled3 Subtract<TSubtrahend>(TSubtrahend subtrahend) where TSubtrahend : IVector3Quantity => new(Components - subtrahend.Components);

    /// <inheritdoc cref="ISubtrahendVector3Quantity{TSum, TMinuend}.SubtractFrom(TMinuend)"/>
    /// <typeparam name="TMinuend">The three-dimensional vector quantity that represents the minuend of { <typeparamref name="TMinuend"/> - <see langword="this"/> }.</typeparam>
    public Unhandled3 SubtractFrom<TMinuend>(TMinuend minuend) where TMinuend : IVector3Quantity => new(minuend.Components - Components);

    /// <inheritdoc cref="IFactorVector3Quantity{TSum, TFactor}.Multiply(TFactor)"/>
    /// <typeparam name="TFactor">The scalar quantity that represents the second factor of { <see langword="this"/> ∙ <typeparamref name="TFactor"/> }.</typeparam>
    public Unhandled3 Multiply<TFactor>(TFactor factor) where TFactor : IScalarQuantity => this * factor.Magnitude;

    /// <inheritdoc cref="IDividendVector3Quantity{TSum, TDivisor}.Divide(TDivisor)"/>
    /// <typeparam name="TDivisor">The scalar quantity that represents the divisor of { <see langword="this"/> / <typeparamref name="TDivisor"/> }.</typeparam>
    public Unhandled3 Divide<TDivisor>(TDivisor divisor) where TDivisor : IScalarQuantity => this / divisor.Magnitude;

    /// <inheritdoc cref="IDotFactorVector3Quantity{TProduct, TFactor}.Dot(TFactor)"/>
    /// <typeparam name="TFactor">The three-dimensional vector quantity that represents the second factor of { <see langword="this"/> ∙ <typeparamref name="TFactor"/> }.</typeparam>
    public Unhandled Dot<TFactor>(TFactor factor) where TFactor : IVector3Quantity => ScalarMaths.Dot3(this, factor);

    /// <inheritdoc cref="ICrossFactorVector3Quantity{TProduct, TFactor}.Cross(TFactor)"/>
    /// <typeparam name="TFactor">The three-dimensional vector quantity that represents the second factor of { <see langword="this"/> ⨯ <typeparamref name="TFactor"/> }.</typeparam>
    public Unhandled3 Cross<TFactor>(TFactor factor) where TFactor : IVector3Quantity => VectorMaths.Cross(this, factor);

    /// <inheritdoc cref="ICrossFactorVector3Quantity{TProduct, TFactor}.CrossInto(TFactor)"/>
    /// <typeparam name="TFactor">The three-dimensional vector quantity that represents the first factor of { <typeparamref name="TFactor"/> ⨯ <see langword="this"/> }.</typeparam>
    public Unhandled3 CrossInto<TFactor>(TFactor factor) where TFactor : IVector3Quantity => VectorMaths.Cross(this, factor);

    /// <inheritdoc/>
    public static Unhandled3 operator +(Unhandled3 a) => a;
    /// <inheritdoc/>
    public static Unhandled3 operator -(Unhandled3 a) => (-a.X, -a.Y, -a.Z);

    /// <inheritdoc/>
    public static Unhandled3 operator +(Unhandled3 a, Unhandled3 b) => (a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    /// <inheritdoc/>
    public static Unhandled3 operator -(Unhandled3 a, Unhandled3 b) => (a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    /// <inheritdoc/>
    public static Unhandled3 operator *(Unhandled3 a, Unhandled b) => (a.X * b, a.Y * b, a.Z * b);
    static Unhandled3 IFactorVector3Quantity<Unhandled3, Unhandled3, Unhandled>.operator *(Unhandled a, Unhandled3 b) => (a * b.X, a * b.Y, a * b.Z);
    /// <inheritdoc/>
    public static Unhandled3 operator /(Unhandled3 a, Unhandled b) => (a.X / b, a.Y / b, a.Z / b);

    /// <inheritdoc/>
    public static Unhandled3 operator *(Unhandled3 a, Scalar b) => (a.X * b, a.Y * b, a.Z * b);
    /// <inheritdoc/>
    public static Unhandled3 operator *(Scalar a, Unhandled3 b) => (a * b.X, a * b.Y, a * b.Z);
    /// <inheritdoc/>
    public static Unhandled3 operator /(Unhandled3 a, Scalar b) => (a.X / b, a.Y / b, a.Z / b);
    /// <inheritdoc/>
    public static Unhandled3 operator %(Unhandled3 a, Scalar b) => (a.X % b, a.Y % b, a.Z % b);

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator +(Unhandled3 a, IVector3Quantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return (a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator +(IVector3Quantity a, Unhandled3 b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return (a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator -(Unhandled3 a, IVector3Quantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return (a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator -(IVector3Quantity a, Unhandled3 b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return (a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(Unhandled3 a, IScalarQuantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return (a.X * b, a.Y * b, a.Z * b);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(IScalarQuantity a, Unhandled3 b)
    {
        ArgumentNullException.ThrowIfNull(a);

        return (a * b.X, a * b.Y, a * b.Z);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator /(Unhandled3 a, IScalarQuantity b)
    {
        ArgumentNullException.ThrowIfNull(b);

        return (a.X / b, a.Y / b, a.Z / b);
    }

    /// <summary>Constructs the <see cref="Vector3"/> with components equal to the values of <paramref name="components"/>.</summary>
    [SuppressMessage("Usage", "CA2225", Justification = "Behaviour can be achieved through a constructor")]
    public static implicit operator Unhandled3((Unhandled X, Unhandled Y, Unhandled Z) components) => new(components.X, components.Y, components.Z);

    /// <summary>Describes mathematical operations that result in a pure <see cref="Scalar"/>.</summary>
    private static IScalarResultingMaths<Scalar> PureScalarMaths { get; } = MathFactory.ScalarResult();
    /// <summary>Describes mathematical operations that result in <see cref="Unhandled"/>.</summary>
    private static IScalarResultingMaths<Unhandled> ScalarMaths { get; } = MathFactory.ScalarResult<Unhandled>();
    /// <summary>Describes mathematical operations that result in <see cref="Unhandled3"/>.</summary>
    private static IVector3ResultingMaths<Unhandled3> VectorMaths { get; } = MathFactory.Vector3Result<Unhandled3>();
}
