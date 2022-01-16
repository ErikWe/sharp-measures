namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

public readonly partial record struct VelocitySquared3(double X, double Y, double Z) :
    IVector3<VelocitySquared3>,
	IAddableVector3<VelocitySquared3, VelocitySquared3>,
	ISubtractableVector3<VelocitySquared3, VelocitySquared3>
{
    public static VelocitySquared3 Zero { get; } = new(0, 0, 0);
    public static VelocitySquared3 Ones { get; } = new(Vector3.Ones);

    public Vector3 AsVector3() => new(X, Y, Z);

    public VelocitySquared3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    public VelocitySquared3((double x, double y, double z) components, UnitOfVelocitySquared unitOfVelocitySquared) :
        this(components.x, components.y, components.z, unitOfVelocitySquared) { }
    public VelocitySquared3(double x, double y, double z, UnitOfVelocitySquared unitOfVelocitySquared) :
        this(x * unitOfVelocitySquared.Factor, y * unitOfVelocitySquared.Factor, z * unitOfVelocitySquared.Factor) { }

	public Vector3 SquareMetresPerSecondSquared => InUnit(UnitOfVelocitySquared.SquareMetrePerSecondSquared);

    Scalar IVector3.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    public VelocitySquared Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    public VelocitySquared3 Normalize() => this / Magnitude().Magnitude;
    public VelocitySquared3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));

    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    public Unhandled Dot<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    public Unhandled3 Cross<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Cross(this, factor));

    public override string ToString() => $"({X}, {Y}, {Z}) [m^2 * s^-2]";

    public Vector3 InUnit(UnitOfVelocitySquared unitOfVelocitySquared) => InUnit(AsVector3(), unitOfVelocitySquared);
    private static Vector3 InUnit(Vector3 vector, UnitOfVelocitySquared unitOfVelocitySquared) => vector / unitOfVelocitySquared.Factor;

    public VelocitySquared3 Plus() => this;
    public VelocitySquared3 Negate() => new(-X, -Y, -Z);
    public static VelocitySquared3 operator +(VelocitySquared3 a) => a;
    public static VelocitySquared3 operator -(VelocitySquared3 a) => new(-a.X, -a.Y, -a.Z);

	public VelocitySquared3 Add(VelocitySquared3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
	public VelocitySquared3 Subtract(VelocitySquared3 term) => new(X - term.X, Y - term.Y, Z - term.Z);
	public static VelocitySquared3 operator +(VelocitySquared3 a, VelocitySquared3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
	public static VelocitySquared3 operator -(VelocitySquared3 a, VelocitySquared3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(VelocitySquared3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Unhandled a, VelocitySquared3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(VelocitySquared3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    public VelocitySquared3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public VelocitySquared3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    public VelocitySquared3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static VelocitySquared3 operator %(VelocitySquared3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    public static VelocitySquared3 operator *(VelocitySquared3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static VelocitySquared3 operator *(double a, VelocitySquared3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static VelocitySquared3 operator /(VelocitySquared3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public VelocitySquared3 Remainder(Scalar divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public VelocitySquared3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    public VelocitySquared3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static VelocitySquared3 operator %(VelocitySquared3 a, Scalar b) => new(a.X % b, a.Y % b, a.Z % b);
    public static VelocitySquared3 operator *(VelocitySquared3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
    public static VelocitySquared3 operator *(Scalar a, VelocitySquared3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static VelocitySquared3 operator /(VelocitySquared3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    public static Unhandled3 operator *(VelocitySquared3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    public static Unhandled3 operator *(IScalarQuantity a, VelocitySquared3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    public static Unhandled3 operator /(VelocitySquared3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    public static implicit operator (double x, double y, double z)(VelocitySquared3 a) => (a.X, a.Y, a.Z);

    public Vector3 ToVector3() => new(X, Y, Z);
    public static explicit operator Vector3(VelocitySquared3 a) => new(a.X, a.Y, a.Z);

    public static VelocitySquared3 FromValueTuple((double x, double y, double z) components) => new(components);
    public static explicit operator VelocitySquared3((double x, double y, double z) components) => new(components);

    public static VelocitySquared3 FromVector3(Vector3 a) => new(a);
    public static explicit operator VelocitySquared3(Vector3 a) => new(a);
}