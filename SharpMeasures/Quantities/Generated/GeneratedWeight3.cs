namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

public readonly partial record struct Weight3(double X, double Y, double Z) :
    IVector3<Weight3>,
	IAddableVector3<Weight3, Weight3>,
	ISubtractableVector3<Weight3, Weight3>
{
    public static Weight3 Zero { get; } = new(0, 0, 0);
    public static Weight3 Ones { get; } = new(Vector3.Ones);

    public Vector3 AsVector3() => new(X, Y, Z);
	public Force3 AsForce3() => new(X, Y, Z);

    public Weight3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    public Weight3((double x, double y, double z) components, UnitOfForce unitOfForce) :
        this(components.x, components.y, components.z, unitOfForce) { }
    public Weight3(double x, double y, double z, UnitOfForce unitOfForce) :
        this(x * unitOfForce.Factor, y * unitOfForce.Factor, z * unitOfForce.Factor) { }

	public Vector3 Newtons => InUnit(UnitOfForce.Newton);
	public Vector3 PoundsForce => InUnit(UnitOfForce.PoundForce);

    Scalar IVector3.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    public Weight Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    public Weight3 Normalize() => this / Magnitude().Magnitude;
    public Weight3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));

    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    public Unhandled Dot<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    public Unhandled3 Cross<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Cross(this, factor));

    public override string ToString() => $"({X}, {Y}, {Z}) [N]";

    public Vector3 InUnit(UnitOfForce unitOfForce) => InUnit(AsVector3(), unitOfForce);
    private static Vector3 InUnit(Vector3 vector, UnitOfForce unitOfForce) => vector / unitOfForce.Factor;

    public Weight3 Plus() => this;
    public Weight3 Negate() => new(-X, -Y, -Z);
    public static Weight3 operator +(Weight3 a) => a;
    public static Weight3 operator -(Weight3 a) => new(-a.X, -a.Y, -a.Z);

	public Weight3 Add(Weight3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
	public Weight3 Subtract(Weight3 term) => new(X - term.X, Y - term.Y, Z - term.Z);
	public static Weight3 operator +(Weight3 a, Weight3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
	public static Weight3 operator -(Weight3 a, Weight3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(Weight3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Unhandled a, Weight3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(Weight3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    public Weight3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Weight3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    public Weight3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Weight3 operator %(Weight3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Weight3 operator *(Weight3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Weight3 operator *(double a, Weight3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Weight3 operator /(Weight3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public Weight3 Remainder(Scalar divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Weight3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    public Weight3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Weight3 operator %(Weight3 a, Scalar b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Weight3 operator *(Weight3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Weight3 operator *(Scalar a, Weight3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Weight3 operator /(Weight3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    public static Unhandled3 operator *(Weight3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    public static Unhandled3 operator *(IScalarQuantity a, Weight3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    public static Unhandled3 operator /(Weight3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    public static implicit operator (double x, double y, double z)(Weight3 a) => (a.X, a.Y, a.Z);

    public Vector3 ToVector3() => new(X, Y, Z);
    public static explicit operator Vector3(Weight3 a) => new(a.X, a.Y, a.Z);

    public static Weight3 FromValueTuple((double x, double y, double z) components) => new(components);
    public static explicit operator Weight3((double x, double y, double z) components) => new(components);

    public static Weight3 FromVector3(Vector3 a) => new(a);
    public static explicit operator Weight3(Vector3 a) => new(a);
}