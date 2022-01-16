namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

public readonly partial record struct Jerk3(double X, double Y, double Z) :
    IVector3<Jerk3>,
	IAddableVector3<Jerk3, Jerk3>,
	ISubtractableVector3<Jerk3, Jerk3>
{
    public static Jerk3 Zero { get; } = new(0, 0, 0);
    public static Jerk3 Ones { get; } = new(Vector3.Ones);

    public Vector3 AsVector3() => new(X, Y, Z);

    public Jerk3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    public Jerk3((double x, double y, double z) components, UnitOfJerk unitOfJerk) :
        this(components.x, components.y, components.z, unitOfJerk) { }
    public Jerk3(double x, double y, double z, UnitOfJerk unitOfJerk) :
        this(x * unitOfJerk.Factor, y * unitOfJerk.Factor, z * unitOfJerk.Factor) { }

	public Vector3 MetresPerSecondCubed => InUnit(UnitOfJerk.MetrePerSecondCubed);

    Scalar IVector3.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    public Jerk Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    public Jerk3 Normalize() => this / Magnitude().Magnitude;
    public Jerk3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));

    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    public Unhandled Dot<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    public Unhandled3 Cross<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Cross(this, factor));

    public override string ToString() => $"({X}, {Y}, {Z}) [m * s^-3]";

    public Vector3 InUnit(UnitOfJerk unitOfJerk) => InUnit(AsVector3(), unitOfJerk);
    private static Vector3 InUnit(Vector3 vector, UnitOfJerk unitOfJerk) => vector / unitOfJerk.Factor;

    public Jerk3 Plus() => this;
    public Jerk3 Negate() => new(-X, -Y, -Z);
    public static Jerk3 operator +(Jerk3 a) => a;
    public static Jerk3 operator -(Jerk3 a) => new(-a.X, -a.Y, -a.Z);

	public Jerk3 Add(Jerk3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
	public Jerk3 Subtract(Jerk3 term) => new(X - term.X, Y - term.Y, Z - term.Z);
	public static Jerk3 operator +(Jerk3 a, Jerk3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
	public static Jerk3 operator -(Jerk3 a, Jerk3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(Jerk3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Unhandled a, Jerk3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(Jerk3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    public Jerk3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Jerk3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    public Jerk3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Jerk3 operator %(Jerk3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Jerk3 operator *(Jerk3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Jerk3 operator *(double a, Jerk3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Jerk3 operator /(Jerk3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public Jerk3 Remainder(Scalar divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Jerk3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    public Jerk3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Jerk3 operator %(Jerk3 a, Scalar b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Jerk3 operator *(Jerk3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Jerk3 operator *(Scalar a, Jerk3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Jerk3 operator /(Jerk3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    public static Unhandled3 operator *(Jerk3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    public static Unhandled3 operator *(IScalarQuantity a, Jerk3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    public static Unhandled3 operator /(Jerk3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    public static implicit operator (double x, double y, double z)(Jerk3 a) => (a.X, a.Y, a.Z);

    public Vector3 ToVector3() => new(X, Y, Z);
    public static explicit operator Vector3(Jerk3 a) => new(a.X, a.Y, a.Z);

    public static Jerk3 FromValueTuple((double x, double y, double z) components) => new(components);
    public static explicit operator Jerk3((double x, double y, double z) components) => new(components);

    public static Jerk3 FromVector3(Vector3 a) => new(a);
    public static explicit operator Jerk3(Vector3 a) => new(a);
}