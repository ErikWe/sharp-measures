namespace ErikWe.SharpMeasures.Quantities;

using System.Numerics;

public readonly record struct Vector3(double X, double Y, double Z) :
    IVector3Quantity,
    IScalableVector3Quantity<Vector3>,
    IAddableVector3Quantity<Vector3, Vector3>,
    ISubtractableVector3Quantity<Vector3, Vector3>,
    IMultiplicableVector3Quantity<Vector3, Scalar>,
    IDivisibleVector3Quantity<Vector3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity
{
    public static Vector3 Zero { get; } = new(0, 0, 0);
    public static Vector3 Ones { get; } = new(1, 1, 1);

    public Vector3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    public Vector3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    public Vector3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }

    public Scalar Magnitude() => SquaredMagnitude().SquareRoot();
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    public Vector3 Normalize() => this / Magnitude();
    public Vector3 Transform(Matrix4x4 transform) => Maths.Vectors.Transform(this, transform);

    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));

    public Unhandled Dot<TQuantity>(TQuantity factor) where TQuantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross<TQuantity>(TQuantity factor) where TQuantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    public override string ToString() => $"({X}, {Y}, {Z})";

    public Vector3 Plus() => this;
    public Vector3 Negate() => new(-X, -Y, -Z);
    public static Vector3 operator +(Vector3 a) => a;
    public static Vector3 operator -(Vector3 a) => new(-a.X, -a.Y, -a.Z);

    public Vector3 Add(Vector3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
    public Vector3 Subtract(Vector3 term) => new(X - term.X, Y - term.Y, Z - term.Z);
    public static Vector3 operator +(Vector3 a, Vector3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Vector3 operator -(Vector3 a, Vector3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(Vector3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Unhandled a, Vector3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(Vector3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    public Vector3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    public Vector3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Vector3 operator *(Vector3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Vector3 operator *(double a, Vector3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Vector3 operator /(Vector3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public Vector3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    public Vector3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Vector3 operator *(Vector3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Vector3 operator *(Scalar a, Vector3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Vector3 operator /(Vector3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply<TQuantity>(TQuantity factor) where TQuantity : IScalarQuantity => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    public Unhandled3 Divide<TQuantity>(TQuantity divisor) where TQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    public static Unhandled3 operator *(Vector3 a, IScalarQuantity b) => new(a.X * (b?.Magnitude ?? double.NaN), a.Y * (b?.Magnitude ?? double.NaN), a.Z * (b?.Magnitude ?? double.NaN));
    public static Unhandled3 operator *(IScalarQuantity a, Vector3 b) => new((a?.Magnitude ?? double.NaN) * b.X, (a?.Magnitude ?? double.NaN) * b.Y, (a?.Magnitude ?? double.NaN) * b.Z);
    public static Unhandled3 operator /(Vector3 a, IScalarQuantity b) => new(a.X / (b?.Magnitude ?? double.NaN), a.Y / (b?.Magnitude ?? double.NaN), a.Z / (b?.Magnitude ?? double.NaN));

    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    public static implicit operator (double x, double y, double z)(Vector3 vector) => (vector.X, vector.Y, vector.Z);

    public static Vector3 FromValueTuple((double x, double y, double z) components) => new(components.x, components.y, components.z);
    public static explicit operator Vector3((double x, double y, double z) components) => new(components.x, components.y, components.z);
}