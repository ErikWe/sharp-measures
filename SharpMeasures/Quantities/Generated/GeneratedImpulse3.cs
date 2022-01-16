namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

public readonly partial record struct Impulse3(double X, double Y, double Z) :
    IVector3<Impulse3>,
	IAddableVector3<Impulse3, Impulse3>,
	ISubtractableVector3<Impulse3, Impulse3>
{
    public static Impulse3 Zero { get; } = new(0, 0, 0);
    public static Impulse3 Ones { get; } = new(Vector3.Ones);

    public Vector3 AsVector3() => new(X, Y, Z);
	public Momentum3 AsMomentum3() => new(X, Y, Z);

    public Impulse3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    public Impulse3((double x, double y, double z) components, UnitOfImpulse unitOfImpulse) :
        this(components.x, components.y, components.z, unitOfImpulse) { }
    public Impulse3(double x, double y, double z, UnitOfImpulse unitOfImpulse) :
        this(x * unitOfImpulse.Factor, y * unitOfImpulse.Factor, z * unitOfImpulse.Factor) { }

	public Vector3 NewtonSeconds => InUnit(UnitOfImpulse.NewtonSecond);

    Scalar IVector3.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    public Impulse Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    public Impulse3 Normalize() => this / Magnitude().Magnitude;
    public Impulse3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));

    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    public Unhandled Dot<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    public Unhandled3 Cross<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Cross(this, factor));

    public override string ToString() => $"({X}, {Y}, {Z}) [N * s]";

    public Vector3 InUnit(UnitOfImpulse unitOfImpulse) => InUnit(AsVector3(), unitOfImpulse);
    private static Vector3 InUnit(Vector3 vector, UnitOfImpulse unitOfImpulse) => vector / unitOfImpulse.Factor;

    public Impulse3 Plus() => this;
    public Impulse3 Negate() => new(-X, -Y, -Z);
    public static Impulse3 operator +(Impulse3 a) => a;
    public static Impulse3 operator -(Impulse3 a) => new(-a.X, -a.Y, -a.Z);

	public Impulse3 Add(Impulse3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
	public Impulse3 Subtract(Impulse3 term) => new(X - term.X, Y - term.Y, Z - term.Z);
	public static Impulse3 operator +(Impulse3 a, Impulse3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
	public static Impulse3 operator -(Impulse3 a, Impulse3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(Impulse3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Unhandled a, Impulse3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(Impulse3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    public Impulse3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Impulse3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    public Impulse3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Impulse3 operator %(Impulse3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Impulse3 operator *(Impulse3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Impulse3 operator *(double a, Impulse3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Impulse3 operator /(Impulse3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public Impulse3 Remainder(Scalar divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Impulse3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    public Impulse3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Impulse3 operator %(Impulse3 a, Scalar b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Impulse3 operator *(Impulse3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Impulse3 operator *(Scalar a, Impulse3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Impulse3 operator /(Impulse3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    public static Unhandled3 operator *(Impulse3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    public static Unhandled3 operator *(IScalarQuantity a, Impulse3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    public static Unhandled3 operator /(Impulse3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    public static implicit operator (double x, double y, double z)(Impulse3 a) => (a.X, a.Y, a.Z);

    public Vector3 ToVector3() => new(X, Y, Z);
    public static explicit operator Vector3(Impulse3 a) => new(a.X, a.Y, a.Z);

    public static Impulse3 FromValueTuple((double x, double y, double z) components) => new(components);
    public static explicit operator Impulse3((double x, double y, double z) components) => new(components);

    public static Impulse3 FromVector3(Vector3 a) => new(a);
    public static explicit operator Impulse3(Vector3 a) => new(a);
}