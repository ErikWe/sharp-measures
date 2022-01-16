namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

public readonly partial record struct Position3(double X, double Y, double Z) :
    IVector3<Position3>,
	IAddableVector3<Position3, Position3>,
	ISubtractableVector3<Position3, Position3>
{
    public static Position3 Zero { get; } = new(0, 0, 0);
    public static Position3 Ones { get; } = new(Vector3.Ones);

    public Vector3 AsVector3() => new(X, Y, Z);
	public Displacement3 AsDisplacement3() => new(X, Y, Z);

    public Position3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    public Position3((double x, double y, double z) components, UnitOfLength unitOfLength) :
        this(components.x, components.y, components.z, unitOfLength) { }
    public Position3(double x, double y, double z, UnitOfLength unitOfLength) :
        this(x * unitOfLength.Factor, y * unitOfLength.Factor, z * unitOfLength.Factor) { }

	public Vector3 Femtometres => InUnit(UnitOfLength.Femtometre);
	public Vector3 Picometres => InUnit(UnitOfLength.Picometre);
	public Vector3 Nanometres => InUnit(UnitOfLength.Nanometre);
	public Vector3 Micrometres => InUnit(UnitOfLength.Micrometre);
	public Vector3 Millimetres => InUnit(UnitOfLength.Millimetre);
	public Vector3 Centimetres => InUnit(UnitOfLength.Centimetre);
	public Vector3 Decimetres => InUnit(UnitOfLength.Decimetre);
	public Vector3 Metres => InUnit(UnitOfLength.Metre);
	public Vector3 Kilometres => InUnit(UnitOfLength.Kilometre);

	public Vector3 AstronomicalUnits => InUnit(UnitOfLength.AstronomicalUnit);
	public Vector3 Lightyears => InUnit(UnitOfLength.Lightyear);
	public Vector3 Parsecs => InUnit(UnitOfLength.Parsec);

	public Vector3 Inches => InUnit(UnitOfLength.Inch);
	public Vector3 Feet => InUnit(UnitOfLength.Foot);
	public Vector3 Yards => InUnit(UnitOfLength.Yard);
	public Vector3 Miles => InUnit(UnitOfLength.Mile);

    Scalar IVector3.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    public Length Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    Scalar IVector3.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    public Area SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    public Position3 Normalize() => this / Magnitude().Magnitude;
    public Position3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));

    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    public Unhandled Dot<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Dot(this, factor));
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    public Unhandled3 Cross<TQuantity>(TQuantity factor) where TQuantity : IVector3 => new(Maths.Vectors.Cross(this, factor));

    public override string ToString() => $"({X}, {Y}, {Z}) [m]";

    public Vector3 InUnit(UnitOfLength unitOfLength) => InUnit(AsVector3(), unitOfLength);
    private static Vector3 InUnit(Vector3 vector, UnitOfLength unitOfLength) => vector / unitOfLength.Factor;

    public Position3 Plus() => this;
    public Position3 Negate() => new(-X, -Y, -Z);
    public static Position3 operator +(Position3 a) => a;
    public static Position3 operator -(Position3 a) => new(-a.X, -a.Y, -a.Z);

	public Position3 Add(Position3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
	public Position3 Subtract(Position3 term) => new(X - term.X, Y - term.Y, Z - term.Z);
	public static Position3 operator +(Position3 a, Position3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
	public static Position3 operator -(Position3 a, Position3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Unhandled3 operator *(Position3 a, Unhandled b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Unhandled3 operator *(Unhandled a, Position3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Unhandled3 operator /(Position3 a, Unhandled b) => new(a.X / b, a.Y / b, a.Z / b);

    public Position3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Position3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    public Position3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Position3 operator %(Position3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Position3 operator *(Position3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Position3 operator *(double a, Position3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Position3 operator /(Position3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public Position3 Remainder(Scalar divisor) => new(X % divisor, Y % divisor, Z % divisor);
    public Position3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    public Position3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    public static Position3 operator %(Position3 a, Scalar b) => new(a.X % b, a.Y % b, a.Z % b);
    public static Position3 operator *(Position3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Position3 operator *(Scalar a, Position3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Position3 operator /(Position3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    public static Unhandled3 operator *(Position3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    public static Unhandled3 operator *(IScalarQuantity a, Position3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    public static Unhandled3 operator /(Position3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    public static implicit operator (double x, double y, double z)(Position3 a) => (a.X, a.Y, a.Z);

    public Vector3 ToVector3() => new(X, Y, Z);
    public static explicit operator Vector3(Position3 a) => new(a.X, a.Y, a.Z);

    public static Position3 FromValueTuple((double x, double y, double z) components) => new(components);
    public static explicit operator Position3((double x, double y, double z) components) => new(components);

    public static Position3 FromVector3(Vector3 a) => new(a);
    public static explicit operator Position3(Vector3 a) => new(a);
}