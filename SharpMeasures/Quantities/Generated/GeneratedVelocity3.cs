namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

public readonly partial record struct Velocity3(double X, double Y, double Z) :
    IVector3<Velocity3>,
	IAddableVector3<Velocity3, Velocity3>,
	ISubtractableVector3<Velocity3, Velocity3>
{
    public static Velocity3 Zero { get; } = new(0, 0, 0);
    public static Velocity3 Ones { get; } = new(Vector3.Ones);

    public Vector3 AsVector3() => new(X, Y, Z);

    public Velocity3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    public Velocity3((double x, double y, double z) components, UnitOfVelocity unitOfVelocity) :
        this(components.x, components.y, components.z, unitOfVelocity) { }
    public Velocity3(double x, double y, double z, UnitOfVelocity unitOfVelocity) :
        this(x * unitOfVelocity.Factor, y * unitOfVelocity.Factor, z * unitOfVelocity.Factor) { }

	public Vector3 MetresPerSecond => InUnit(UnitOfVelocity.MetrePerSecond);
	public Vector3 KilometresPerHour => InUnit(UnitOfVelocity.KilometrePerHour);

	public Vector3 MilesPerHour => InUnit(UnitOfVelocity.MilePerHour);

    Scalar IVector3.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    public Velocity Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    Scalar IVector3.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    public VelocitySquared SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    public Velocity3 Normalize() => this / Magnitude().Magnitude;
    public Velocity3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));

    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    public Unhandled Dot<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    public Unhandled3 Cross<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Cross(this, factor));

    public override string ToString() => $"({X}, {Y}, {Z}) [m * s^-1]";

    public Vector3 InUnit(UnitOfVelocity unitOfVelocity) => InUnit(AsVector3(), unitOfVelocity);
    private static Vector3 InUnit(Vector3 vector, UnitOfVelocity unitOfVelocity) => vector / unitOfVelocity.Factor;

    public Velocity3 Plus() => this;
    public Velocity3 Negate() => new(-X, -Y, -Z);
    public static Velocity3 operator +(Velocity3 a) => a;
    public static Velocity3 operator -(Velocity3 a) => new(-a.X, -a.Y, -a.Z);

	public Velocity3 Add(Velocity3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
	public Velocity3 Subtract(Velocity3 term) => new(X - term.X, Y - term.Y, Z - term.Z);
	public static Velocity3 operator +(Velocity3 a, Velocity3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
	public static Velocity3 operator -(Velocity3 a, Velocity3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(Velocity3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Unhandled a, Velocity3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(Velocity3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    public Velocity3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Velocity3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    public Velocity3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Velocity3 operator %(Velocity3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Velocity3 operator *(Velocity3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Velocity3 operator *(double a, Velocity3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Velocity3 operator /(Velocity3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public Velocity3 Remainder(Scalar divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Velocity3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    public Velocity3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Velocity3 operator %(Velocity3 a, Scalar b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Velocity3 operator *(Velocity3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Velocity3 operator *(Scalar a, Velocity3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Velocity3 operator /(Velocity3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    public static Unhandled3 operator *(Velocity3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    public static Unhandled3 operator *(IScalarQuantity a, Velocity3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    public static Unhandled3 operator /(Velocity3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    public static implicit operator (double x, double y, double z)(Velocity3 a) => (a.X, a.Y, a.Z);

    public Vector3 ToVector3() => new(X, Y, Z);
    public static explicit operator Vector3(Velocity3 a) => new(a.X, a.Y, a.Z);

    public static Velocity3 FromValueTuple((double x, double y, double z) components) => new(components);
    public static explicit operator Velocity3((double x, double y, double z) components) => new(components);

    public static Velocity3 FromVector3(Vector3 a) => new(a);
    public static explicit operator Velocity3(Vector3 a) => new(a);
}