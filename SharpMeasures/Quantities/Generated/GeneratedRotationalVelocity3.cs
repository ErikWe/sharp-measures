namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

public readonly partial record struct RotationalVelocity3(double X, double Y, double Z) :
    IVector3<RotationalVelocity3>,
	IAddableVector3<RotationalVelocity3, RotationalVelocity3>,
	ISubtractableVector3<RotationalVelocity3, RotationalVelocity3>
{
    public static RotationalVelocity3 Zero { get; } = new(0, 0, 0);
    public static RotationalVelocity3 Ones { get; } = new(Vector3.Ones);

    public Vector3 AsVector3() => new(X, Y, Z);

    public RotationalVelocity3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    public RotationalVelocity3((double x, double y, double z) components, UnitOfAngularVelocity unitOfAngularVelocity) :
        this(components.x, components.y, components.z, unitOfAngularVelocity) { }
    public RotationalVelocity3(double x, double y, double z, UnitOfAngularVelocity unitOfAngularVelocity) :
        this(x * unitOfAngularVelocity.Factor, y * unitOfAngularVelocity.Factor, z * unitOfAngularVelocity.Factor) { }

	public Vector3 RadiansPerSecond => InUnit(UnitOfAngularVelocity.RadianPerSecond);
	public Vector3 DegreesPerSecond => InUnit(UnitOfAngularVelocity.DegreePerSecond);
	public Vector3 TurnsPerSecond => InUnit(UnitOfAngularVelocity.TurnPerSecond);

    Scalar IVector3.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    public AngularVelocity Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    public RotationalVelocity3 Normalize() => this / Magnitude().Magnitude;
    public RotationalVelocity3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));

    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    public Unhandled Dot<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    public Unhandled3 Cross<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Cross(this, factor));

    public override string ToString() => $"({X}, {Y}, {Z}) [rad * s^-1]";

    public Vector3 InUnit(UnitOfAngularVelocity unitOfAngularVelocity) => InUnit(AsVector3(), unitOfAngularVelocity);
    private static Vector3 InUnit(Vector3 vector, UnitOfAngularVelocity unitOfAngularVelocity) => vector / unitOfAngularVelocity.Factor;

    public RotationalVelocity3 Plus() => this;
    public RotationalVelocity3 Negate() => new(-X, -Y, -Z);
    public static RotationalVelocity3 operator +(RotationalVelocity3 a) => a;
    public static RotationalVelocity3 operator -(RotationalVelocity3 a) => new(-a.X, -a.Y, -a.Z);

	public RotationalVelocity3 Add(RotationalVelocity3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
	public RotationalVelocity3 Subtract(RotationalVelocity3 term) => new(X - term.X, Y - term.Y, Z - term.Z);
	public static RotationalVelocity3 operator +(RotationalVelocity3 a, RotationalVelocity3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
	public static RotationalVelocity3 operator -(RotationalVelocity3 a, RotationalVelocity3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(RotationalVelocity3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Unhandled a, RotationalVelocity3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(RotationalVelocity3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    public RotationalVelocity3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public RotationalVelocity3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    public RotationalVelocity3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static RotationalVelocity3 operator %(RotationalVelocity3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    public static RotationalVelocity3 operator *(RotationalVelocity3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static RotationalVelocity3 operator *(double a, RotationalVelocity3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static RotationalVelocity3 operator /(RotationalVelocity3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public RotationalVelocity3 Remainder(Scalar divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public RotationalVelocity3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    public RotationalVelocity3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static RotationalVelocity3 operator %(RotationalVelocity3 a, Scalar b) => new(a.X % b, a.Y % b, a.Z % b);
    public static RotationalVelocity3 operator *(RotationalVelocity3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
    public static RotationalVelocity3 operator *(Scalar a, RotationalVelocity3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static RotationalVelocity3 operator /(RotationalVelocity3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    public static Unhandled3 operator *(RotationalVelocity3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    public static Unhandled3 operator *(IScalarQuantity a, RotationalVelocity3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    public static Unhandled3 operator /(RotationalVelocity3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    public static implicit operator (double x, double y, double z)(RotationalVelocity3 a) => (a.X, a.Y, a.Z);

    public Vector3 ToVector3() => new(X, Y, Z);
    public static explicit operator Vector3(RotationalVelocity3 a) => new(a.X, a.Y, a.Z);

    public static RotationalVelocity3 FromValueTuple((double x, double y, double z) components) => new(components);
    public static explicit operator RotationalVelocity3((double x, double y, double z) components) => new(components);

    public static RotationalVelocity3 FromVector3(Vector3 a) => new(a);
    public static explicit operator RotationalVelocity3(Vector3 a) => new(a);
}