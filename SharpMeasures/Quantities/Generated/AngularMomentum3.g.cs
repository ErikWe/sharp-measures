namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(AngularMomentum3, 3, AngularMomentum, UnitOfAngularMomentum, [KilogramMetreSquaredPerSecond], [KilogramMetresSquaredPerSecond])#
public readonly partial record struct AngularMomentum3 :
    IVector3Quantity,
    IScalableVector3Quantity<AngularMomentum3>,
    INormalizableVector3Quantity<AngularMomentum3>,
    ITransformableVector3Quantity<AngularMomentum3>,
    IMultiplicableVector3Quantity<AngularMomentum3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<AngularMomentum3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<AngularMomentum3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(AngularMomentum3, 3)#
    public static AngularMomentum3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(AngularMomentum3, 3)#
    public double X { get; init; }
    #Document:ComponentY(AngularMomentum3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(AngularMomentum3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(AngularMomentum3, 3, UnitOfAngularMomentum, unitOfAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public AngularMomentum3((AngularMomentum x, AngularMomentum y, AngularMomentum z) components, UnitOfAngularMomentum unitOfAngularMomentum) : 
    	this(components.x, components.y, components.z, unitOfAngularMomentum) { }
    #Document:ConstructorComponentsUnit(AngularMomentum3, 3, UnitOfAngularMomentum, unitOfAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public AngularMomentum3(AngularMomentum x, AngularMomentum y, AngularMomentum z, UnitOfAngularMomentum unitOfAngularMomentum) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAngularMomentum) { }
    #Document:ConstructorScalarTupleUnit(AngularMomentum3, 3, UnitOfAngularMomentum, unitOfAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public AngularMomentum3((Scalar x, Scalar y, Scalar z) components, UnitOfAngularMomentum unitOfAngularMomentum) : 
    	this(components.x, components.y, components.z, unitOfAngularMomentum) { }
    #Document:ConstructorScalarsUnit(AngularMomentum3, 3, UnitOfAngularMomentum, unitOfAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public AngularMomentum3(Scalar x, Scalar y, Scalar z, UnitOfAngularMomentum unitOfAngularMomentum) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAngularMomentum) { }
    #Document:ConstructorVectorUnit(AngularMomentum3, 3, UnitOfAngularMomentum, unitOfAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public AngularMomentum3(Vector3 components, UnitOfAngularMomentum unitOfAngularMomentum) : this(components.X, components.Y, components.Z, unitOfAngularMomentum) { }
    #Document:ConstructorDoubleTupleUnit(AngularMomentum3, 3, UnitOfAngularMomentum, unitOfAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public AngularMomentum3((double x, double y, double z) components, UnitOfAngularMomentum unitOfAngularMomentum) : 
    	this(components.x, components.y, components.z, unitOfAngularMomentum) { }
    #Document:ConstructorDoublesTupleUnit(AngularMomentum3, 3, UnitOfAngularMomentum, unitOfAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public AngularMomentum3(double x, double y, double z, UnitOfAngularMomentum unitOfAngularMomentum) : 
    	this(x * unitOfAngularMomentum.Factor, y * unitOfAngularMomentum.Factor, z * unitOfAngularMomentum.Factor) { }

    #Document:ConstructorComponentTuple(AngularMomentum3, 3, UnitOfAngularMomentum, unitOfAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public AngularMomentum3((AngularMomentum x, AngularMomentum y, AngularMomentum z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(AngularMomentum3, 3, UnitOfAngularMomentum, unitOfAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public AngularMomentum3(AngularMomentum x, AngularMomentum y, AngularMomentum z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(AngularMomentum3, 3, UnitOfAngularMomentum, unitOfAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public AngularMomentum3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(AngularMomentum3, 3, UnitOfAngularMomentum, unitOfAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public AngularMomentum3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(AngularMomentum3, 3, UnitOfAngularMomentum, unitOfAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public AngularMomentum3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(AngularMomentum3, 3, UnitOfAngularMomentum, unitOfAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public AngularMomentum3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(AngularMomentum3, 3, UnitOfAngularMomentum, unitOfAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public AngularMomentum3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

));

    #Document:InUnit(quantity = AngularMomentum3, dimensionality = 3, unit = UnitOfAngularMomentum, unitName = KilogramMetreSquaredPerSecond)#
    public Vector3 KilogramMetresSquaredPerSecond => InUnit(UnitOfAngularMomentum.KilogramMetreSquaredPerSecond);

    #Document:ScalarMagnitude(AngularMomentum3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(AngularMomentum3, 3, AngularMomentum)#
    public AngularMomentum Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(AngularMomentum3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(AngularMomentum3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(AngularMomentum3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(AngularMomentum3, 3)#
    public AngularMomentum3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(AngularMomentum3, 3)#
    public AngularMomentum3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(AngularMomentum3, 3)#
    public AngularMomentum3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(AngularMomentum3, 3)#
    public AngularMomentum Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(AngularMomentum3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(AngularMomentum3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(AngularMomentum3, 3)#
    public AngularMomentum3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(AngularMomentum3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(AngularMomentum3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(AngularMomentum3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [kg * m^2 / s]";

    #Document:InUnitInstance(AngularMomentum3, 3, UnitOfAngularMomentum, unitOfAngularMomentum)#
    public Vector3 InUnit(UnitOfAngularMomentum unitOfAngularMomentum) => InUnit(this, unitOfAngularMomentum);
    #Document:InUnitStatic(AngularMomentum3, angularMomentum3, 3, UnitOfAngularMomentum, unitOfAngularMomentum)#
    private static Vector3 InUnit(AngularMomentum3 angularMomentum3, UnitOfAngularMomentum unitOfAngularMomentum) => 
    	angularMomentum3.ToVector3() / unitOfAngularMomentum.Factor;
    
    #Document:PlusMethod(AngularMomentum3, 3)#
    public AngularMomentum3 Plus() => this;
    #Document:NegateMethod(AngularMomentum3, 3)#
    public AngularMomentum3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(AngularMomentum3, 3)#
    public static AngularMomentum3 operator +(AngularMomentum3 a) => a;
    #Document:NegateOperator(AngularMomentum3, 3)#
    public static AngularMomentum3 operator -(AngularMomentum3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(AngularMomentum3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(AngularMomentum3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(AngularMomentum3, 3)#
    public static Unhandled3 operator *(AngularMomentum3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(AngularMomentum3, 3)#
    public static Unhandled3 operator *(Unhandled a, AngularMomentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(AngularMomentum3, 3)#
    public static Unhandled3 operator /(AngularMomentum3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(AngularMomentum3, 3)#
    public AngularMomentum3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(AngularMomentum3, 3)#
    public AngularMomentum3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(AngularMomentum3, 3)#
    public AngularMomentum3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(AngularMomentum3, 3)#
    public static AngularMomentum3 operator %(AngularMomentum3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(AngularMomentum3, 3)#
    public static AngularMomentum3 operator *(AngularMomentum3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(AngularMomentum3, 3)#
    public static AngularMomentum3 operator *(double a, AngularMomentum3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(AngularMomentum3, 3)#
    public static AngularMomentum3 operator /(AngularMomentum3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(AngularMomentum3, 3)#
    public AngularMomentum3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(AngularMomentum3, 3)#
    public AngularMomentum3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(AngularMomentum3, 3)#
    public AngularMomentum3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(AngularMomentum3, 3)#
    public static AngularMomentum3 operator %(AngularMomentum3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(AngularMomentum3, 3)#
    public static AngularMomentum3 operator *(AngularMomentum3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(AngularMomentum3, 3)#
    public static AngularMomentum3 operator *(Scalar a, AngularMomentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(AngularMomentum3, 3)#
    public static AngularMomentum3 operator /(AngularMomentum3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(AngularMomentum3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(AngularMomentum3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(AngularMomentum3, 3)#
    public static Unhandled3 operator *(AngularMomentum3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(AngularMomentum3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, AngularMomentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(AngularMomentum3, 3)#
    public static Unhandled3 operator /(AngularMomentum3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(AngularMomentum3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(AngularMomentum3, 3)#
    public static implicit operator (double x, double y, double z)(AngularMomentum3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(AngularMomentum3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(AngularMomentum3, 3)#
    public static explicit operator Vector3(AngularMomentum3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(AngularMomentum3, 3)#
    public static AngularMomentum3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(AngularMomentum3, 3)#
    public static explicit operator AngularMomentum3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(AngularMomentum3, 3)#
    public static AngularMomentum3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(AngularMomentum3, 3)#
    public static explicit operator AngularMomentum3(Vector3 a) => new(a);
}
