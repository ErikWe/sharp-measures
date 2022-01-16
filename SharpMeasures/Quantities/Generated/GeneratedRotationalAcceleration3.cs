namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

public readonly partial record struct RotationalAcceleration3(double X, double Y, double Z) :
    IVector3<RotationalAcceleration3>,
	IAddableVector3<RotationalAcceleration3, RotationalAcceleration3>,
	ISubtractableVector3<RotationalAcceleration3, RotationalAcceleration3>
{
    public static RotationalAcceleration3 Zero { get; } = new(0, 0, 0);
    public static RotationalAcceleration3 Ones { get; } = new(Vector3.Ones);

    public Vector3 AsVector3() => new(X, Y, Z);

    public RotationalAcceleration3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    public RotationalAcceleration3((double x, double y, double z) components, UnitOfAngularAcceleration unitOfAngularAcceleration) :
        this(components.x, components.y, components.z, unitOfAngularAcceleration) { }
    public RotationalAcceleration3(double x, double y, double z, UnitOfAngularAcceleration unitOfAngularAcceleration) :
        this(x * unitOfAngularAcceleration.Factor, y * unitOfAngularAcceleration.Factor, z * unitOfAngularAcceleration.Factor) { }

	public Vector3 RadiansPerSecondSquared => InUnit(UnitOfAngularAcceleration.RadianPerSecondSquared);

    Scalar IVector3.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    public AngularAcceleration Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    public RotationalAcceleration3 Normalize() => this / Magnitude().Magnitude;
    public RotationalAcceleration3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));

    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    public Unhandled Dot<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    public Unhandled3 Cross<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Cross(this, factor));

    public override string ToString() => $"({X}, {Y}, {Z}) [rad * s^-2]";

    public Vector3 InUnit(UnitOfAngularAcceleration unitOfAngularAcceleration) => InUnit(AsVector3(), unitOfAngularAcceleration);
    private static Vector3 InUnit(Vector3 vector, UnitOfAngularAcceleration unitOfAngularAcceleration) => vector / unitOfAngularAcceleration.Factor;

    public RotationalAcceleration3 Plus() => this;
    public RotationalAcceleration3 Negate() => new(-X, -Y, -Z);
    public static RotationalAcceleration3 operator +(RotationalAcceleration3 a) => a;
    public static RotationalAcceleration3 operator -(RotationalAcceleration3 a) => new(-a.X, -a.Y, -a.Z);

	public RotationalAcceleration3 Add(RotationalAcceleration3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
	public RotationalAcceleration3 Subtract(RotationalAcceleration3 term) => new(X - term.X, Y - term.Y, Z - term.Z);
	public static RotationalAcceleration3 operator +(RotationalAcceleration3 a, RotationalAcceleration3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
	public static RotationalAcceleration3 operator -(RotationalAcceleration3 a, RotationalAcceleration3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(RotationalAcceleration3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Unhandled a, RotationalAcceleration3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(RotationalAcceleration3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    public RotationalAcceleration3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public RotationalAcceleration3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    public RotationalAcceleration3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static RotationalAcceleration3 operator %(RotationalAcceleration3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    public static RotationalAcceleration3 operator *(RotationalAcceleration3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static RotationalAcceleration3 operator *(double a, RotationalAcceleration3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static RotationalAcceleration3 operator /(RotationalAcceleration3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public RotationalAcceleration3 Remainder(Scalar divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public RotationalAcceleration3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    public RotationalAcceleration3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static RotationalAcceleration3 operator %(RotationalAcceleration3 a, Scalar b) => new(a.X % b, a.Y % b, a.Z % b);
    public static RotationalAcceleration3 operator *(RotationalAcceleration3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
    public static RotationalAcceleration3 operator *(Scalar a, RotationalAcceleration3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static RotationalAcceleration3 operator /(RotationalAcceleration3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    public static Unhandled3 operator *(RotationalAcceleration3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    public static Unhandled3 operator *(IScalarQuantity a, RotationalAcceleration3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    public static Unhandled3 operator /(RotationalAcceleration3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    public static implicit operator (double x, double y, double z)(RotationalAcceleration3 a) => (a.X, a.Y, a.Z);

    public Vector3 ToVector3() => new(X, Y, Z);
    public static explicit operator Vector3(RotationalAcceleration3 a) => new(a.X, a.Y, a.Z);

    public static RotationalAcceleration3 FromValueTuple((double x, double y, double z) components) => new(components);
    public static explicit operator RotationalAcceleration3((double x, double y, double z) components) => new(components);

    public static RotationalAcceleration3 FromVector3(Vector3 a) => new(a);
    public static explicit operator RotationalAcceleration3(Vector3 a) => new(a);
}