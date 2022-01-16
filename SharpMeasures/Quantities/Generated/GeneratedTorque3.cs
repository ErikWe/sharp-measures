namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

public readonly partial record struct Torque3(double X, double Y, double Z) :
    IVector3<Torque3>,
	IAddableVector3<Torque3, Torque3>,
	ISubtractableVector3<Torque3, Torque3>
{
    public static Torque3 Zero { get; } = new(0, 0, 0);
    public static Torque3 Ones { get; } = new(Vector3.Ones);

    public Vector3 AsVector3() => new(X, Y, Z);

    public Torque3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    public Torque3((double x, double y, double z) components, UnitOfTorque unitOfTorque) :
        this(components.x, components.y, components.z, unitOfTorque) { }
    public Torque3(double x, double y, double z, UnitOfTorque unitOfTorque) :
        this(x * unitOfTorque.Factor, y * unitOfTorque.Factor, z * unitOfTorque.Factor) { }

	public Vector3 NewtonMetres => InUnit(UnitOfTorque.NewtonMetre);

    Scalar IVector3.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    public Torque Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    public Torque3 Normalize() => this / Magnitude().Magnitude;
    public Torque3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));

    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    public Unhandled Dot<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    public Unhandled3 Cross<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Cross(this, factor));

    public override string ToString() => $"({X}, {Y}, {Z}) [[component]";

    public Vector3 InUnit(UnitOfTorque unitOfTorque) => InUnit(AsVector3(), unitOfTorque);
    private static Vector3 InUnit(Vector3 vector, UnitOfTorque unitOfTorque) => vector / unitOfTorque.Factor;

    public Torque3 Plus() => this;
    public Torque3 Negate() => new(-X, -Y, -Z);
    public static Torque3 operator +(Torque3 a) => a;
    public static Torque3 operator -(Torque3 a) => new(-a.X, -a.Y, -a.Z);

	public Torque3 Add(Torque3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
	public Torque3 Subtract(Torque3 term) => new(X - term.X, Y - term.Y, Z - term.Z);
	public static Torque3 operator +(Torque3 a, Torque3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
	public static Torque3 operator -(Torque3 a, Torque3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(Torque3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Unhandled a, Torque3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(Torque3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    public Torque3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Torque3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    public Torque3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Torque3 operator %(Torque3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Torque3 operator *(Torque3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Torque3 operator *(double a, Torque3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Torque3 operator /(Torque3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public Torque3 Remainder(Scalar divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Torque3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    public Torque3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Torque3 operator %(Torque3 a, Scalar b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Torque3 operator *(Torque3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Torque3 operator *(Scalar a, Torque3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Torque3 operator /(Torque3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    public static Unhandled3 operator *(Torque3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    public static Unhandled3 operator *(IScalarQuantity a, Torque3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    public static Unhandled3 operator /(Torque3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    public static implicit operator (double x, double y, double z)(Torque3 a) => (a.X, a.Y, a.Z);

    public Vector3 ToVector3() => new(X, Y, Z);
    public static explicit operator Vector3(Torque3 a) => new(a.X, a.Y, a.Z);

    public static Torque3 FromValueTuple((double x, double y, double z) components) => new(components);
    public static explicit operator Torque3((double x, double y, double z) components) => new(components);

    public static Torque3 FromVector3(Vector3 a) => new(a);
    public static explicit operator Torque3(Vector3 a) => new(a);
}