namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

public readonly partial record struct Momentum3(double X, double Y, double Z) :
    IVector3<Momentum3>,
	IAddableVector3<Momentum3, Momentum3>
{
    public static Momentum3 Zero { get; } = new(0, 0, 0);
    public static Momentum3 Ones { get; } = new(Vector3.Ones);

    public Vector3 AsVector3() => new(X, Y, Z);
	public Impulse3 AsImpulse3() => new(X, Y, Z);

    public Momentum3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    public Momentum3((double x, double y, double z) components, UnitOfMomentum unitOfMomentum) :
        this(components.x, components.y, components.z, unitOfMomentum) { }
    public Momentum3(double x, double y, double z, UnitOfMomentum unitOfMomentum) :
        this(x * unitOfMomentum.Factor, y * unitOfMomentum.Factor, z * unitOfMomentum.Factor) { }

	public Vector3 KilogramMetresPerSecond => InUnit(UnitOfMomentum.KilogramMetrePerSecond);

    Scalar IVector3.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    public Momentum Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    public Momentum3 Normalize() => this / Magnitude().Magnitude;
    public Momentum3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));

    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    public Unhandled Dot<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    public Unhandled3 Cross<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Cross(this, factor));

    public override string ToString() => $"({X}, {Y}, {Z}) [kg * m * s^-1]";

    public Vector3 InUnit(UnitOfMomentum unitOfMomentum) => InUnit(AsVector3(), unitOfMomentum);
    private static Vector3 InUnit(Vector3 vector, UnitOfMomentum unitOfMomentum) => vector / unitOfMomentum.Factor;

    public Momentum3 Plus() => this;
    public Momentum3 Negate() => new(-X, -Y, -Z);
    public static Momentum3 operator +(Momentum3 a) => a;
    public static Momentum3 operator -(Momentum3 a) => new(-a.X, -a.Y, -a.Z);

	public Momentum3 Add(Momentum3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
	public static Momentum3 operator +(Momentum3 a, Momentum3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(Momentum3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Unhandled a, Momentum3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(Momentum3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    public Momentum3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Momentum3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    public Momentum3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Momentum3 operator %(Momentum3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Momentum3 operator *(Momentum3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Momentum3 operator *(double a, Momentum3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Momentum3 operator /(Momentum3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public Momentum3 Remainder(Scalar divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Momentum3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    public Momentum3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Momentum3 operator %(Momentum3 a, Scalar b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Momentum3 operator *(Momentum3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Momentum3 operator *(Scalar a, Momentum3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Momentum3 operator /(Momentum3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    public static Unhandled3 operator *(Momentum3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    public static Unhandled3 operator *(IScalarQuantity a, Momentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    public static Unhandled3 operator /(Momentum3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    public static implicit operator (double x, double y, double z)(Momentum3 a) => (a.X, a.Y, a.Z);

    public Vector3 ToVector3() => new(X, Y, Z);
    public static explicit operator Vector3(Momentum3 a) => new(a.X, a.Y, a.Z);

    public static Momentum3 FromValueTuple((double x, double y, double z) components) => new(components);
    public static explicit operator Momentum3((double x, double y, double z) components) => new(components);

    public static Momentum3 FromVector3(Vector3 a) => new(a);
    public static explicit operator Momentum3(Vector3 a) => new(a);
}