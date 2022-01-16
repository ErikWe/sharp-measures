namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

public readonly partial record struct TemperatureGradient3(double X, double Y, double Z) :
    IVector3<TemperatureGradient3>,
	IAddableVector3<TemperatureGradient3, TemperatureGradient3>,
	ISubtractableVector3<TemperatureGradient3, TemperatureGradient3>
{
    public static TemperatureGradient3 Zero { get; } = new(0, 0, 0);
    public static TemperatureGradient3 Ones { get; } = new(Vector3.Ones);

    public Vector3 AsVector3() => new(X, Y, Z);

    public TemperatureGradient3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    public TemperatureGradient3((double x, double y, double z) components, UnitOfTemperatureGradient unitOfTemperatureGradient) :
        this(components.x, components.y, components.z, unitOfTemperatureGradient) { }
    public TemperatureGradient3(double x, double y, double z, UnitOfTemperatureGradient unitOfTemperatureGradient) :
        this(x * unitOfTemperatureGradient.Factor, y * unitOfTemperatureGradient.Factor, z * unitOfTemperatureGradient.Factor) { }

	public Vector3 KelvinPerMetre => InUnit(UnitOfTemperatureGradient.KelvinPerMetre);
	public Vector3 CelsiusPerMetre => InUnit(UnitOfTemperatureGradient.CelsiusPerMetre);
	public Vector3 RankinePerMetre => InUnit(UnitOfTemperatureGradient.RankinePerMetre);
	public Vector3 FahrenheitPerMetre => InUnit(UnitOfTemperatureGradient.FahrenheitPerMetre);

    Scalar IVector3.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    public TemperatureGradient Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    public TemperatureGradient3 Normalize() => this / Magnitude().Magnitude;
    public TemperatureGradient3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));

    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    public Unhandled Dot<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    public Unhandled3 Cross<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Cross(this, factor));

    public override string ToString() => $"({X}, {Y}, {Z}) [K * m^-1]";

    public Vector3 InUnit(UnitOfTemperatureGradient unitOfTemperatureGradient) => InUnit(AsVector3(), unitOfTemperatureGradient);
    private static Vector3 InUnit(Vector3 vector, UnitOfTemperatureGradient unitOfTemperatureGradient) => vector / unitOfTemperatureGradient.Factor;

    public TemperatureGradient3 Plus() => this;
    public TemperatureGradient3 Negate() => new(-X, -Y, -Z);
    public static TemperatureGradient3 operator +(TemperatureGradient3 a) => a;
    public static TemperatureGradient3 operator -(TemperatureGradient3 a) => new(-a.X, -a.Y, -a.Z);

	public TemperatureGradient3 Add(TemperatureGradient3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
	public TemperatureGradient3 Subtract(TemperatureGradient3 term) => new(X - term.X, Y - term.Y, Z - term.Z);
	public static TemperatureGradient3 operator +(TemperatureGradient3 a, TemperatureGradient3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
	public static TemperatureGradient3 operator -(TemperatureGradient3 a, TemperatureGradient3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(TemperatureGradient3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Unhandled a, TemperatureGradient3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(TemperatureGradient3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    public TemperatureGradient3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public TemperatureGradient3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    public TemperatureGradient3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static TemperatureGradient3 operator %(TemperatureGradient3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    public static TemperatureGradient3 operator *(TemperatureGradient3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static TemperatureGradient3 operator *(double a, TemperatureGradient3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static TemperatureGradient3 operator /(TemperatureGradient3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public TemperatureGradient3 Remainder(Scalar divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public TemperatureGradient3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    public TemperatureGradient3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static TemperatureGradient3 operator %(TemperatureGradient3 a, Scalar b) => new(a.X % b, a.Y % b, a.Z % b);
    public static TemperatureGradient3 operator *(TemperatureGradient3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
    public static TemperatureGradient3 operator *(Scalar a, TemperatureGradient3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static TemperatureGradient3 operator /(TemperatureGradient3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    public static Unhandled3 operator *(TemperatureGradient3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    public static Unhandled3 operator *(IScalarQuantity a, TemperatureGradient3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    public static Unhandled3 operator /(TemperatureGradient3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    public static implicit operator (double x, double y, double z)(TemperatureGradient3 a) => (a.X, a.Y, a.Z);

    public Vector3 ToVector3() => new(X, Y, Z);
    public static explicit operator Vector3(TemperatureGradient3 a) => new(a.X, a.Y, a.Z);

    public static TemperatureGradient3 FromValueTuple((double x, double y, double z) components) => new(components);
    public static explicit operator TemperatureGradient3((double x, double y, double z) components) => new(components);

    public static TemperatureGradient3 FromVector3(Vector3 a) => new(a);
    public static explicit operator TemperatureGradient3(Vector3 a) => new(a);
}