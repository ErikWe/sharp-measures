namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(Rotation3, 3, Angle, UnitOfAngle, [Radian, Degree, ArcMinute, ArcSecond, Turn, Gradian], [Radians, Degrees, ArcMinutes, ArcSeconds, Turns, Gradians])#
public readonly partial record struct Rotation3 :
    IVector3Quantity,
    IScalableVector3Quantity<Rotation3>,
    INormalizableVector3Quantity<Rotation3>,
    ITransformableVector3Quantity<Rotation3>,
    IMultiplicableVector3Quantity<Rotation3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Rotation3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<Rotation3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(Rotation3, 3)#
    public static Rotation3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(Rotation3, 3)#
    public double X { get; init; }
    #Document:ComponentY(Rotation3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(Rotation3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(Rotation3, 3, UnitOfAngle, unitOfAngle, [Radian, Degree, ArcMinute, ArcSecond, Turn, Gradian])#
    public Rotation3((Angle x, Angle y, Angle z) components, UnitOfAngle unitOfAngle) : this(components.x, components.y, components.z, unitOfAngle) { }
    #Document:ConstructorComponentsUnit(Rotation3, 3, UnitOfAngle, unitOfAngle, [Radian, Degree, ArcMinute, ArcSecond, Turn, Gradian])#
    public Rotation3(Angle x, Angle y, Angle z, UnitOfAngle unitOfAngle) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAngle) { }
    #Document:ConstructorScalarTupleUnit(Rotation3, 3, UnitOfAngle, unitOfAngle, [Radian, Degree, ArcMinute, ArcSecond, Turn, Gradian])#
    public Rotation3((Scalar x, Scalar y, Scalar z) components, UnitOfAngle unitOfAngle) : this(components.x, components.y, components.z, unitOfAngle) { }
    #Document:ConstructorScalarsUnit(Rotation3, 3, UnitOfAngle, unitOfAngle, [Radian, Degree, ArcMinute, ArcSecond, Turn, Gradian])#
    public Rotation3(Scalar x, Scalar y, Scalar z, UnitOfAngle unitOfAngle) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAngle) { }
    #Document:ConstructorVectorUnit(Rotation3, 3, UnitOfAngle, unitOfAngle, [Radian, Degree, ArcMinute, ArcSecond, Turn, Gradian])#
    public Rotation3(Vector3 components, UnitOfAngle unitOfAngle) : this(components.X, components.Y, components.Z, unitOfAngle) { }
    #Document:ConstructorDoubleTupleUnit(Rotation3, 3, UnitOfAngle, unitOfAngle, [Radian, Degree, ArcMinute, ArcSecond, Turn, Gradian])#
    public Rotation3((double x, double y, double z) components, UnitOfAngle unitOfAngle) : this(components.x, components.y, components.z, unitOfAngle) { }
    #Document:ConstructorDoublesTupleUnit(Rotation3, 3, UnitOfAngle, unitOfAngle, [Radian, Degree, ArcMinute, ArcSecond, Turn, Gradian])#
    public Rotation3(double x, double y, double z, UnitOfAngle unitOfAngle) : this(x * unitOfAngle.Factor, y * unitOfAngle.Factor, z * unitOfAngle.Factor) { }

    #Document:ConstructorComponentTuple(Rotation3, 3, UnitOfAngle, unitOfAngle, [Radian, Degree, ArcMinute, ArcSecond, Turn, Gradian])#
    public Rotation3((Angle x, Angle y, Angle z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(Rotation3, 3, UnitOfAngle, unitOfAngle, [Radian, Degree, ArcMinute, ArcSecond, Turn, Gradian])#
    public Rotation3(Angle x, Angle y, Angle z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(Rotation3, 3, UnitOfAngle, unitOfAngle, [Radian, Degree, ArcMinute, ArcSecond, Turn, Gradian])#
    public Rotation3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(Rotation3, 3, UnitOfAngle, unitOfAngle, [Radian, Degree, ArcMinute, ArcSecond, Turn, Gradian])#
    public Rotation3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(Rotation3, 3, UnitOfAngle, unitOfAngle, [Radian, Degree, ArcMinute, ArcSecond, Turn, Gradian])#
    public Rotation3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(Rotation3, 3, UnitOfAngle, unitOfAngle, [Radian, Degree, ArcMinute, ArcSecond, Turn, Gradian])#
    public Rotation3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(Rotation3, 3, UnitOfAngle, unitOfAngle, [Radian, Degree, ArcMinute, ArcSecond, Turn, Gradian])#
    public Rotation3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    #Document:InUnit(quantity = Rotation3, dimensionality = 3, unit = UnitOfAngle, unitName = Radian)#
    public Vector3 Radians => InUnit(UnitOfAngle.Radian);
    #Document:InUnit(quantity = Rotation3, dimensionality = 3, unit = UnitOfAngle, unitName = Degree)#
    public Vector3 Degrees => InUnit(UnitOfAngle.Degree);
    #Document:InUnit(quantity = Rotation3, dimensionality = 3, unit = UnitOfAngle, unitName = ArcMinute)#
    public Vector3 ArcMinutes => InUnit(UnitOfAngle.ArcMinute);
    #Document:InUnit(quantity = Rotation3, dimensionality = 3, unit = UnitOfAngle, unitName = ArcSecond)#
    public Vector3 ArcSeconds => InUnit(UnitOfAngle.ArcSecond);
    #Document:InUnit(quantity = Rotation3, dimensionality = 3, unit = UnitOfAngle, unitName = Turn)#
    public Vector3 Turns => InUnit(UnitOfAngle.Turn);
    #Document:InUnit(quantity = Rotation3, dimensionality = 3, unit = UnitOfAngle, unitName = Gradian)#
    public Vector3 Gradians => InUnit(UnitOfAngle.Gradian);

    #Document:ScalarMagnitude(Rotation3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(Rotation3, 3, Angle)#
    public Angle Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(Rotation3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(Rotation3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(Rotation3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(Rotation3, 3)#
    public Rotation3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(Rotation3, 3)#
    public Rotation3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(Rotation3, 3)#
    public Rotation3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(Rotation3, 3)#
    public Angle Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(Rotation3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(Rotation3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(Rotation3, 3)#
    public Rotation3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(Rotation3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(Rotation3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(Rotation3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [rad]";

    #Document:InUnitInstance(Rotation3, 3, UnitOfAngle, unitOfAngle)#
    public Vector3 InUnit(UnitOfAngle unitOfAngle) => InUnit(this, unitOfAngle);
    #Document:InUnitStatic(Rotation3, rotation3, 3, UnitOfAngle, unitOfAngle)#
    private static Vector3 InUnit(Rotation3 rotation3, UnitOfAngle unitOfAngle) => rotation3.ToVector3() / unitOfAngle.Factor;
    
    #Document:PlusMethod(Rotation3, 3)#
    public Rotation3 Plus() => this;
    #Document:NegateMethod(Rotation3, 3)#
    public Rotation3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(Rotation3, 3)#
    public static Rotation3 operator +(Rotation3 a) => a;
    #Document:NegateOperator(Rotation3, 3)#
    public static Rotation3 operator -(Rotation3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(Rotation3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(Rotation3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(Rotation3, 3)#
    public static Unhandled3 operator *(Rotation3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(Rotation3, 3)#
    public static Unhandled3 operator *(Unhandled a, Rotation3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(Rotation3, 3)#
    public static Unhandled3 operator /(Rotation3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(Rotation3, 3)#
    public Rotation3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(Rotation3, 3)#
    public Rotation3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(Rotation3, 3)#
    public Rotation3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(Rotation3, 3)#
    public static Rotation3 operator %(Rotation3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(Rotation3, 3)#
    public static Rotation3 operator *(Rotation3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(Rotation3, 3)#
    public static Rotation3 operator *(double a, Rotation3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(Rotation3, 3)#
    public static Rotation3 operator /(Rotation3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(Rotation3, 3)#
    public Rotation3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(Rotation3, 3)#
    public Rotation3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(Rotation3, 3)#
    public Rotation3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(Rotation3, 3)#
    public static Rotation3 operator %(Rotation3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(Rotation3, 3)#
    public static Rotation3 operator *(Rotation3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(Rotation3, 3)#
    public static Rotation3 operator *(Scalar a, Rotation3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(Rotation3, 3)#
    public static Rotation3 operator /(Rotation3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(Rotation3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(Rotation3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(Rotation3, 3)#
    public static Unhandled3 operator *(Rotation3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(Rotation3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, Rotation3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(Rotation3, 3)#
    public static Unhandled3 operator /(Rotation3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(Rotation3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(Rotation3, 3)#
    public static implicit operator (double x, double y, double z)(Rotation3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(Rotation3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(Rotation3, 3)#
    public static explicit operator Vector3(Rotation3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(Rotation3, 3)#
    public static Rotation3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(Rotation3, 3)#
    public static explicit operator Rotation3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(Rotation3, 3)#
    public static Rotation3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(Rotation3, 3)#
    public static explicit operator Rotation3(Vector3 a) => new(a);
}
