namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

public readonly partial record struct SpecificAngularMomentum3(double X, double Y, double Z) :
    IVector3<SpecificAngularMomentum3>,
	IAddableVector3<SpecificAngularMomentum3, SpecificAngularMomentum3>,
	ISubtractableVector3<SpecificAngularMomentum3, SpecificAngularMomentum3>
{
    public static SpecificAngularMomentum3 Zero { get; } = new(0, 0, 0);
    public static SpecificAngularMomentum3 Ones { get; } = new(Vector3.Ones);

    public Vector3 AsVector3() => new(X, Y, Z);

    public SpecificAngularMomentum3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    public SpecificAngularMomentum3((double x, double y, double z) components, UnitOfSpecificAngularMomentum unitOfSpecificAngularMomentum) :
        this(components.x, components.y, components.z, unitOfSpecificAngularMomentum) { }
    public SpecificAngularMomentum3(double x, double y, double z, UnitOfSpecificAngularMomentum unitOfSpecificAngularMomentum) :
        this(x * unitOfSpecificAngularMomentum.Factor, y * unitOfSpecificAngularMomentum.Factor, z * unitOfSpecificAngularMomentum.Factor) { }

	public Vector3 SquareMetresPerSecond => InUnit(UnitOfSpecificAngularMomentum.SquareMetrePerSecond);

    Scalar IVector3.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    public SpecificAngularMomentum Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    public SpecificAngularMomentum3 Normalize() => this / Magnitude().Magnitude;
    public SpecificAngularMomentum3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));

    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    public Unhandled Dot<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    public Unhandled3 Cross<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Cross(this, factor));

    public override string ToString() => $"({X}, {Y}, {Z}) [kg * m^2]";

    public Vector3 InUnit(UnitOfSpecificAngularMomentum unitOfSpecificAngularMomentum) => InUnit(AsVector3(), unitOfSpecificAngularMomentum);
    private static Vector3 InUnit(Vector3 vector, UnitOfSpecificAngularMomentum unitOfSpecificAngularMomentum) => vector / unitOfSpecificAngularMomentum.Factor;

    public SpecificAngularMomentum3 Plus() => this;
    public SpecificAngularMomentum3 Negate() => new(-X, -Y, -Z);
    public static SpecificAngularMomentum3 operator +(SpecificAngularMomentum3 a) => a;
    public static SpecificAngularMomentum3 operator -(SpecificAngularMomentum3 a) => new(-a.X, -a.Y, -a.Z);

	public SpecificAngularMomentum3 Add(SpecificAngularMomentum3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
	public SpecificAngularMomentum3 Subtract(SpecificAngularMomentum3 term) => new(X - term.X, Y - term.Y, Z - term.Z);
	public static SpecificAngularMomentum3 operator +(SpecificAngularMomentum3 a, SpecificAngularMomentum3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
	public static SpecificAngularMomentum3 operator -(SpecificAngularMomentum3 a, SpecificAngularMomentum3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(SpecificAngularMomentum3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Unhandled a, SpecificAngularMomentum3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(SpecificAngularMomentum3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    public SpecificAngularMomentum3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public SpecificAngularMomentum3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    public SpecificAngularMomentum3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static SpecificAngularMomentum3 operator %(SpecificAngularMomentum3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    public static SpecificAngularMomentum3 operator *(SpecificAngularMomentum3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static SpecificAngularMomentum3 operator *(double a, SpecificAngularMomentum3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static SpecificAngularMomentum3 operator /(SpecificAngularMomentum3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public SpecificAngularMomentum3 Remainder(Scalar divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public SpecificAngularMomentum3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    public SpecificAngularMomentum3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static SpecificAngularMomentum3 operator %(SpecificAngularMomentum3 a, Scalar b) => new(a.X % b, a.Y % b, a.Z % b);
    public static SpecificAngularMomentum3 operator *(SpecificAngularMomentum3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
    public static SpecificAngularMomentum3 operator *(Scalar a, SpecificAngularMomentum3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static SpecificAngularMomentum3 operator /(SpecificAngularMomentum3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    public static Unhandled3 operator *(SpecificAngularMomentum3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    public static Unhandled3 operator *(IScalarQuantity a, SpecificAngularMomentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    public static Unhandled3 operator /(SpecificAngularMomentum3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    public static implicit operator (double x, double y, double z)(SpecificAngularMomentum3 a) => (a.X, a.Y, a.Z);

    public Vector3 ToVector3() => new(X, Y, Z);
    public static explicit operator Vector3(SpecificAngularMomentum3 a) => new(a.X, a.Y, a.Z);

    public static SpecificAngularMomentum3 FromValueTuple((double x, double y, double z) components) => new(components);
    public static explicit operator SpecificAngularMomentum3((double x, double y, double z) components) => new(components);

    public static SpecificAngularMomentum3 FromVector3(Vector3 a) => new(a);
    public static explicit operator SpecificAngularMomentum3(Vector3 a) => new(a);
}