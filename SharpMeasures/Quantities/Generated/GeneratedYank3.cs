namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

public readonly partial record struct Yank3(double X, double Y, double Z) :
    IVector3<Yank3>,
	IAddableVector3<Yank3, Yank3>,
	ISubtractableVector3<Yank3, Yank3>
{
    public static Yank3 Zero { get; } = new(0, 0, 0);
    public static Yank3 Ones { get; } = new(Vector3.Ones);

    public Vector3 AsVector3() => new(X, Y, Z);

    public Yank3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    public Yank3((double x, double y, double z) components, UnitOfYank unitOfYank) :
        this(components.x, components.y, components.z, unitOfYank) { }
    public Yank3(double x, double y, double z, UnitOfYank unitOfYank) :
        this(x * unitOfYank.Factor, y * unitOfYank.Factor, z * unitOfYank.Factor) { }

	public Vector3 NewtonsPerSecond => InUnit(UnitOfYank.NewtonPerSecond);

    Scalar IVector3.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    public Yank Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    public Yank3 Normalize() => this / Magnitude().Magnitude;
    public Yank3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));

    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    public Unhandled Dot<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    public Unhandled3 Cross<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Cross(this, factor));

    public override string ToString() => $"({X}, {Y}, {Z}) [N * s^-1]";

    public Vector3 InUnit(UnitOfYank unitOfYank) => InUnit(AsVector3(), unitOfYank);
    private static Vector3 InUnit(Vector3 vector, UnitOfYank unitOfYank) => vector / unitOfYank.Factor;

    public Yank3 Plus() => this;
    public Yank3 Negate() => new(-X, -Y, -Z);
    public static Yank3 operator +(Yank3 a) => a;
    public static Yank3 operator -(Yank3 a) => new(-a.X, -a.Y, -a.Z);

	public Yank3 Add(Yank3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
	public Yank3 Subtract(Yank3 term) => new(X - term.X, Y - term.Y, Z - term.Z);
	public static Yank3 operator +(Yank3 a, Yank3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
	public static Yank3 operator -(Yank3 a, Yank3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(Yank3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Unhandled a, Yank3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(Yank3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    public Yank3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Yank3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    public Yank3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Yank3 operator %(Yank3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Yank3 operator *(Yank3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Yank3 operator *(double a, Yank3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Yank3 operator /(Yank3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public Yank3 Remainder(Scalar divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Yank3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    public Yank3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Yank3 operator %(Yank3 a, Scalar b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Yank3 operator *(Yank3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Yank3 operator *(Scalar a, Yank3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Yank3 operator /(Yank3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    public static Unhandled3 operator *(Yank3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    public static Unhandled3 operator *(IScalarQuantity a, Yank3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    public static Unhandled3 operator /(Yank3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    public static implicit operator (double x, double y, double z)(Yank3 a) => (a.X, a.Y, a.Z);

    public Vector3 ToVector3() => new(X, Y, Z);
    public static explicit operator Vector3(Yank3 a) => new(a.X, a.Y, a.Z);

    public static Yank3 FromValueTuple((double x, double y, double z) components) => new(components);
    public static explicit operator Yank3((double x, double y, double z) components) => new(components);

    public static Yank3 FromVector3(Vector3 a) => new(a);
    public static explicit operator Yank3(Vector3 a) => new(a);
}