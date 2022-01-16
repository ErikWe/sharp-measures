namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

public readonly partial record struct Acceleration3(double X, double Y, double Z) :
    IVector3<Acceleration3>,
	IAddableVector3<Acceleration3, Acceleration3>,
	ISubtractableVector3<Acceleration3, Acceleration3>
{
    public static Acceleration3 Zero { get; } = new(0, 0, 0);
    public static Acceleration3 Ones { get; } = new(Vector3.Ones);

    public Vector3 AsVector3() => new(X, Y, Z);

    public Acceleration3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    public Acceleration3((double x, double y, double z) components, UnitOfAcceleration unitOfAcceleration) :
        this(components.x, components.y, components.z, unitOfAcceleration) { }
    public Acceleration3(double x, double y, double z, UnitOfAcceleration unitOfAcceleration) :
        this(x * unitOfAcceleration.Factor, y * unitOfAcceleration.Factor, z * unitOfAcceleration.Factor) { }

	public Vector3 MetresPerSecondSquared => InUnit(UnitOfAcceleration.MetrePerSecondSquared);

    Scalar IVector3.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    public Acceleration Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    public Acceleration3 Normalize() => this / Magnitude().Magnitude;
    public Acceleration3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));

    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    public Unhandled Dot<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    public Unhandled3 Cross<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Cross(this, factor));

    public override string ToString() => $"({X}, {Y}, {Z}) [m * s^-2]";

    public Vector3 InUnit(UnitOfAcceleration unitOfAcceleration) => InUnit(AsVector3(), unitOfAcceleration);
    private static Vector3 InUnit(Vector3 vector, UnitOfAcceleration unitOfAcceleration) => vector / unitOfAcceleration.Factor;

    public Acceleration3 Plus() => this;
    public Acceleration3 Negate() => new(-X, -Y, -Z);
    public static Acceleration3 operator +(Acceleration3 a) => a;
    public static Acceleration3 operator -(Acceleration3 a) => new(-a.X, -a.Y, -a.Z);

	public Acceleration3 Add(Acceleration3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
	public Acceleration3 Subtract(Acceleration3 term) => new(X - term.X, Y - term.Y, Z - term.Z);
	public static Acceleration3 operator +(Acceleration3 a, Acceleration3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
	public static Acceleration3 operator -(Acceleration3 a, Acceleration3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(Acceleration3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Unhandled a, Acceleration3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(Acceleration3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    public Acceleration3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Acceleration3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    public Acceleration3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Acceleration3 operator %(Acceleration3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Acceleration3 operator *(Acceleration3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Acceleration3 operator *(double a, Acceleration3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Acceleration3 operator /(Acceleration3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public Acceleration3 Remainder(Scalar divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Acceleration3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    public Acceleration3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Acceleration3 operator %(Acceleration3 a, Scalar b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Acceleration3 operator *(Acceleration3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Acceleration3 operator *(Scalar a, Acceleration3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Acceleration3 operator /(Acceleration3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    public static Unhandled3 operator *(Acceleration3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    public static Unhandled3 operator *(IScalarQuantity a, Acceleration3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    public static Unhandled3 operator /(Acceleration3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    public static implicit operator (double x, double y, double z)(Acceleration3 a) => (a.X, a.Y, a.Z);

    public Vector3 ToVector3() => new(X, Y, Z);
    public static explicit operator Vector3(Acceleration3 a) => new(a.X, a.Y, a.Z);

    public static Acceleration3 FromValueTuple((double x, double y, double z) components) => new(components);
    public static explicit operator Acceleration3((double x, double y, double z) components) => new(components);

    public static Acceleration3 FromVector3(Vector3 a) => new(a);
    public static explicit operator Acceleration3(Vector3 a) => new(a);
}