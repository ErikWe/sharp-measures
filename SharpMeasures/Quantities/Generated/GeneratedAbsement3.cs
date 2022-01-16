namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

public readonly partial record struct Absement3(double X, double Y, double Z) :
    IVector3<Absement3>,
	IAddableVector3<Absement3, Absement3>,
	ISubtractableVector3<Absement3, Absement3>
{
    public static Absement3 Zero { get; } = new(0, 0, 0);
    public static Absement3 Ones { get; } = new(Vector3.Ones);

    public Vector3 AsVector3() => new(X, Y, Z);

    public Absement3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    public Absement3((double x, double y, double z) components, UnitOfAbsement unitOfAbsement) :
        this(components.x, components.y, components.z, unitOfAbsement) { }
    public Absement3(double x, double y, double z, UnitOfAbsement unitOfAbsement) :
        this(x * unitOfAbsement.Factor, y * unitOfAbsement.Factor, z * unitOfAbsement.Factor) { }

	public Vector3 MetreSeconds => InUnit(UnitOfAbsement.MetreSecond);

    Scalar IVector3.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    public Absement Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    public Absement3 Normalize() => this / Magnitude().Magnitude;
    public Absement3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));

    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    public Unhandled Dot<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    public Unhandled3 Cross<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Cross(this, factor));

    public override string ToString() => $"({X}, {Y}, {Z}) [m * s]";

    public Vector3 InUnit(UnitOfAbsement unitOfAbsement) => InUnit(AsVector3(), unitOfAbsement);
    private static Vector3 InUnit(Vector3 vector, UnitOfAbsement unitOfAbsement) => vector / unitOfAbsement.Factor;

    public Absement3 Plus() => this;
    public Absement3 Negate() => new(-X, -Y, -Z);
    public static Absement3 operator +(Absement3 a) => a;
    public static Absement3 operator -(Absement3 a) => new(-a.X, -a.Y, -a.Z);

	public Absement3 Add(Absement3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
	public Absement3 Subtract(Absement3 term) => new(X - term.X, Y - term.Y, Z - term.Z);
	public static Absement3 operator +(Absement3 a, Absement3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
	public static Absement3 operator -(Absement3 a, Absement3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(Absement3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Unhandled a, Absement3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(Absement3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    public Absement3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Absement3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    public Absement3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Absement3 operator %(Absement3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Absement3 operator *(Absement3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Absement3 operator *(double a, Absement3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Absement3 operator /(Absement3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public Absement3 Remainder(Scalar divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Absement3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    public Absement3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Absement3 operator %(Absement3 a, Scalar b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Absement3 operator *(Absement3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Absement3 operator *(Scalar a, Absement3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Absement3 operator /(Absement3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    public static Unhandled3 operator *(Absement3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    public static Unhandled3 operator *(IScalarQuantity a, Absement3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    public static Unhandled3 operator /(Absement3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    public static implicit operator (double x, double y, double z)(Absement3 a) => (a.X, a.Y, a.Z);

    public Vector3 ToVector3() => new(X, Y, Z);
    public static explicit operator Vector3(Absement3 a) => new(a.X, a.Y, a.Z);

    public static Absement3 FromValueTuple((double x, double y, double z) components) => new(components);
    public static explicit operator Absement3((double x, double y, double z) components) => new(components);

    public static Absement3 FromVector3(Vector3 a) => new(a);
    public static explicit operator Absement3(Vector3 a) => new(a);
}