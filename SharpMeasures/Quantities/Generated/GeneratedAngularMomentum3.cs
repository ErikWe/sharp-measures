namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

public readonly partial record struct AngularMomentum3(double X, double Y, double Z) :
    IVector3<AngularMomentum3>,
	IAddableVector3<AngularMomentum3, AngularMomentum3>,
	ISubtractableVector3<AngularMomentum3, AngularMomentum3>
{
    public static AngularMomentum3 Zero { get; } = new(0, 0, 0);
    public static AngularMomentum3 Ones { get; } = new(Vector3.Ones);

    public Vector3 AsVector3() => new(X, Y, Z);

    public AngularMomentum3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    public AngularMomentum3((double x, double y, double z) components, UnitOfAngularMomentum unitOfAngularMomentum) :
        this(components.x, components.y, components.z, unitOfAngularMomentum) { }
    public AngularMomentum3(double x, double y, double z, UnitOfAngularMomentum unitOfAngularMomentum) :
        this(x * unitOfAngularMomentum.Factor, y * unitOfAngularMomentum.Factor, z * unitOfAngularMomentum.Factor) { }

	public Vector3 KilogramMetresSquaredPerSecond => InUnit(UnitOfAngularMomentum.KilogramMetreSquaredPerSecond);

    Scalar IVector3.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    public AngularMomentum Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    public AngularMomentum3 Normalize() => this / Magnitude().Magnitude;
    public AngularMomentum3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));

    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    public Unhandled Dot<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    public Unhandled3 Cross<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Cross(this, factor));

    public override string ToString() => $"({X}, {Y}, {Z}) [kg * m^2 * s^-1]";

    public Vector3 InUnit(UnitOfAngularMomentum unitOfAngularMomentum) => InUnit(AsVector3(), unitOfAngularMomentum);
    private static Vector3 InUnit(Vector3 vector, UnitOfAngularMomentum unitOfAngularMomentum) => vector / unitOfAngularMomentum.Factor;

    public AngularMomentum3 Plus() => this;
    public AngularMomentum3 Negate() => new(-X, -Y, -Z);
    public static AngularMomentum3 operator +(AngularMomentum3 a) => a;
    public static AngularMomentum3 operator -(AngularMomentum3 a) => new(-a.X, -a.Y, -a.Z);

	public AngularMomentum3 Add(AngularMomentum3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
	public AngularMomentum3 Subtract(AngularMomentum3 term) => new(X - term.X, Y - term.Y, Z - term.Z);
	public static AngularMomentum3 operator +(AngularMomentum3 a, AngularMomentum3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
	public static AngularMomentum3 operator -(AngularMomentum3 a, AngularMomentum3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(AngularMomentum3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Unhandled a, AngularMomentum3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(AngularMomentum3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    public AngularMomentum3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public AngularMomentum3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    public AngularMomentum3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static AngularMomentum3 operator %(AngularMomentum3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    public static AngularMomentum3 operator *(AngularMomentum3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static AngularMomentum3 operator *(double a, AngularMomentum3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static AngularMomentum3 operator /(AngularMomentum3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public AngularMomentum3 Remainder(Scalar divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public AngularMomentum3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    public AngularMomentum3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static AngularMomentum3 operator %(AngularMomentum3 a, Scalar b) => new(a.X % b, a.Y % b, a.Z % b);
    public static AngularMomentum3 operator *(AngularMomentum3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
    public static AngularMomentum3 operator *(Scalar a, AngularMomentum3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static AngularMomentum3 operator /(AngularMomentum3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    public static Unhandled3 operator *(AngularMomentum3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    public static Unhandled3 operator *(IScalarQuantity a, AngularMomentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    public static Unhandled3 operator /(AngularMomentum3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    public static implicit operator (double x, double y, double z)(AngularMomentum3 a) => (a.X, a.Y, a.Z);

    public Vector3 ToVector3() => new(X, Y, Z);
    public static explicit operator Vector3(AngularMomentum3 a) => new(a.X, a.Y, a.Z);

    public static AngularMomentum3 FromValueTuple((double x, double y, double z) components) => new(components);
    public static explicit operator AngularMomentum3((double x, double y, double z) components) => new(components);

    public static AngularMomentum3 FromVector3(Vector3 a) => new(a);
    public static explicit operator AngularMomentum3(Vector3 a) => new(a);
}