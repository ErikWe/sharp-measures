namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

public readonly partial record struct Rotation3(double X, double Y, double Z) :
    IVector3<Rotation3>,
	IAddableVector3<Rotation3, Rotation3>,
	ISubtractableVector3<Rotation3, Rotation3>
{
    public static Rotation3 Zero { get; } = new(0, 0, 0);
    public static Rotation3 Ones { get; } = new(Vector3.Ones);

    public Vector3 AsVector3() => new(X, Y, Z);

    public Rotation3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    public Rotation3((double x, double y, double z) components, UnitOfAngle unitOfAngle) :
        this(components.x, components.y, components.z, unitOfAngle) { }
    public Rotation3(double x, double y, double z, UnitOfAngle unitOfAngle) :
        this(x * unitOfAngle.Factor, y * unitOfAngle.Factor, z * unitOfAngle.Factor) { }

	public Vector3 Radians => InUnit(UnitOfAngle.Radian);
	public Vector3 Degrees => InUnit(UnitOfAngle.Degree);
	public Vector3 ArcMinutes => InUnit(UnitOfAngle.ArcMinute);
	public Vector3 ArcSeconds => InUnit(UnitOfAngle.ArcSecond);
	public Vector3 Turns => InUnit(UnitOfAngle.Turn);

    Scalar IVector3.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    public Angle Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    public Rotation3 Normalize() => this / Magnitude().Magnitude;
    public Rotation3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));

    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    public Unhandled Dot<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    public Unhandled3 Cross<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Cross(this, factor));

    public override string ToString() => $"({X}, {Y}, {Z}) [rad]";

    public Vector3 InUnit(UnitOfAngle unitOfAngle) => InUnit(AsVector3(), unitOfAngle);
    private static Vector3 InUnit(Vector3 vector, UnitOfAngle unitOfAngle) => vector / unitOfAngle.Factor;

    public Rotation3 Plus() => this;
    public Rotation3 Negate() => new(-X, -Y, -Z);
    public static Rotation3 operator +(Rotation3 a) => a;
    public static Rotation3 operator -(Rotation3 a) => new(-a.X, -a.Y, -a.Z);

	public Rotation3 Add(Rotation3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
	public Rotation3 Subtract(Rotation3 term) => new(X - term.X, Y - term.Y, Z - term.Z);
	public static Rotation3 operator +(Rotation3 a, Rotation3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
	public static Rotation3 operator -(Rotation3 a, Rotation3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(Rotation3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Unhandled a, Rotation3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(Rotation3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    public Rotation3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Rotation3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    public Rotation3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Rotation3 operator %(Rotation3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Rotation3 operator *(Rotation3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Rotation3 operator *(double a, Rotation3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Rotation3 operator /(Rotation3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public Rotation3 Remainder(Scalar divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Rotation3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    public Rotation3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Rotation3 operator %(Rotation3 a, Scalar b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Rotation3 operator *(Rotation3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Rotation3 operator *(Scalar a, Rotation3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Rotation3 operator /(Rotation3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    public static Unhandled3 operator *(Rotation3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    public static Unhandled3 operator *(IScalarQuantity a, Rotation3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    public static Unhandled3 operator /(Rotation3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    public static implicit operator (double x, double y, double z)(Rotation3 a) => (a.X, a.Y, a.Z);

    public Vector3 ToVector3() => new(X, Y, Z);
    public static explicit operator Vector3(Rotation3 a) => new(a.X, a.Y, a.Z);

    public static Rotation3 FromValueTuple((double x, double y, double z) components) => new(components);
    public static explicit operator Rotation3((double x, double y, double z) components) => new(components);

    public static Rotation3 FromVector3(Vector3 a) => new(a);
    public static explicit operator Rotation3(Vector3 a) => new(a);
}