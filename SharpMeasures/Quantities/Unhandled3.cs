namespace ErikWe.SharpMeasures.Quantities;

using System.Numerics;

public readonly record struct Unhandled3(double X, double Y, double Z) :
    IVector3Quantity,
    IScalableVector3Quantity<Unhandled3>,
    IAddableVector3Quantity<Unhandled3, Unhandled3>,
    ISubtractableVector3Quantity<Unhandled3, Unhandled3>,
    IMultiplicableVector3Quantity<Unhandled3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity
{
    public static readonly Unhandled3 Zero = new(0, 0, 0);
    public static readonly Unhandled3 Ones = new(1, 1, 1);

    public Unhandled3((Unhandled x, Unhandled y, Unhandled z) components) : this(components.x, components.y, components.z) { }
    public Unhandled3(Unhandled x, Unhandled y, Unhandled z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    public Unhandled3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    public Unhandled3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    public Unhandled3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    public Unhandled3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }

    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    public Unhandled Magnitude() => SquaredMagnitude().SquareRoot();
    public Unhandled SquaredMagnitude() => Dot(this);

    public Unhandled3 Normalize() => this / Magnitude();
    public Unhandled3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));

    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));

    public Unhandled Dot<TQuantity>(TQuantity factor) where TQuantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross<TQuantity>(TQuantity factor) where TQuantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    public override string ToString() => $"({X}, {Y}, {Z}) [undef]";

    public Unhandled3 Plus() => this;
    public Unhandled3 Negate() => new(-X, -Y, -Z);
    public static Unhandled3 operator +(Unhandled3 a) => a;
    public static Unhandled3 operator -(Unhandled3 a) => new(-a.X, -a.Y, -a.Z);

    public Unhandled3 Add(Unhandled3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
    public Unhandled3 Subtract(Unhandled3 term) => new(X - term.X, Y - term.Y, Z - term.Z);
    public static Unhandled3 operator +(Unhandled3 a, Unhandled3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Unhandled3 operator -(Unhandled3 a, Unhandled3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(Unhandled3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Unhandled a, Unhandled3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(Unhandled3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(Unhandled3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(double a, Unhandled3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(Unhandled3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(Unhandled3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Scalar a, Unhandled3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(Unhandled3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply<TQuantity>(TQuantity factor) where TQuantity : IScalarQuantity => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    public Unhandled3 Divide<TQuantity>(TQuantity divisor) where TQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    public static Unhandled3 operator *(Unhandled3 a, IScalarQuantity b) => new(a.X * (b?.Magnitude ?? double.NaN), a.Y * (b?.Magnitude ?? double.NaN), a.Z * (b?.Magnitude ?? double.NaN));
    public static Unhandled3 operator *(IScalarQuantity a, Unhandled3 b) => new((a?.Magnitude ?? double.NaN) * b.X, (a?.Magnitude ?? double.NaN) * b.Y, (a?.Magnitude ?? double.NaN) * b.Z);
    public static Unhandled3 operator /(Unhandled3 a, IScalarQuantity b) => new(a.X / (b?.Magnitude ?? double.NaN), a.Y / (b?.Magnitude ?? double.NaN), a.Z / (b?.Magnitude ?? double.NaN));

    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    public static implicit operator (double x, double y, double z)(Unhandled3 a) => (a.X, a.Y, a.Z);

    public Vector3 ToVector3() => new(X, Y, Z);
    public static explicit operator Vector3(Unhandled3 a) => new(a.X, a.Y, a.Z);

    public static Unhandled3 FromValueTuple((double x, double y, double z) components) => new(components);
    public static explicit operator Unhandled3((double x, double y, double z) components) => new(components);

    public static Unhandled3 FromVector3(Vector3 a) => new(a);
    public static explicit operator Unhandled3(Vector3 a) => new(a);
}