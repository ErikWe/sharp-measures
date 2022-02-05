namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(Acceleration3, 3, Acceleration, UnitOfAcceleration, [MetrePerSecondSquared], [MetresPerSecondSquared])#
public readonly partial record struct Acceleration3 :
    IVector3Quantity,
    IScalableVector3Quantity<Acceleration3>,
    INormalizableVector3Quantity<Acceleration3>,
    ITransformableVector3Quantity<Acceleration3>,
    IMultiplicableVector3Quantity<Acceleration3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Acceleration3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<Acceleration3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(Acceleration3, 3)#
    public static Acceleration3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(Acceleration3, 3)#
    public double X { get; init; }
    #Document:ComponentY(Acceleration3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(Acceleration3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(Acceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [MetrePerSecondSquared])#
    public Acceleration3((Acceleration x, Acceleration y, Acceleration z) components, UnitOfAcceleration unitOfAcceleration) : 
    	this(components.x, components.y, components.z, unitOfAcceleration) { }
    #Document:ConstructorComponentsUnit(Acceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [MetrePerSecondSquared])#
    public Acceleration3(Acceleration x, Acceleration y, Acceleration z, UnitOfAcceleration unitOfAcceleration) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAcceleration) { }
    #Document:ConstructorScalarTupleUnit(Acceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [MetrePerSecondSquared])#
    public Acceleration3((Scalar x, Scalar y, Scalar z) components, UnitOfAcceleration unitOfAcceleration) : 
    	this(components.x, components.y, components.z, unitOfAcceleration) { }
    #Document:ConstructorScalarsUnit(Acceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [MetrePerSecondSquared])#
    public Acceleration3(Scalar x, Scalar y, Scalar z, UnitOfAcceleration unitOfAcceleration) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAcceleration) { }
    #Document:ConstructorVectorUnit(Acceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [MetrePerSecondSquared])#
    public Acceleration3(Vector3 components, UnitOfAcceleration unitOfAcceleration) : this(components.X, components.Y, components.Z, unitOfAcceleration) { }
    #Document:ConstructorDoubleTupleUnit(Acceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [MetrePerSecondSquared])#
    public Acceleration3((double x, double y, double z) components, UnitOfAcceleration unitOfAcceleration) : 
    	this(components.x, components.y, components.z, unitOfAcceleration) { }
    #Document:ConstructorDoublesTupleUnit(Acceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [MetrePerSecondSquared])#
    public Acceleration3(double x, double y, double z, UnitOfAcceleration unitOfAcceleration) : 
    	this(x * unitOfAcceleration.Factor, y * unitOfAcceleration.Factor, z * unitOfAcceleration.Factor) { }

    #Document:ConstructorComponentTuple(Acceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [MetrePerSecondSquared])#
    public Acceleration3((Acceleration x, Acceleration y, Acceleration z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(Acceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [MetrePerSecondSquared])#
    public Acceleration3(Acceleration x, Acceleration y, Acceleration z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(Acceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [MetrePerSecondSquared])#
    public Acceleration3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(Acceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [MetrePerSecondSquared])#
    public Acceleration3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(Acceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [MetrePerSecondSquared])#
    public Acceleration3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(Acceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [MetrePerSecondSquared])#
    public Acceleration3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(Acceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [MetrePerSecondSquared])#
    public Acceleration3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    #Document:InUnit(quantity = Acceleration3, dimensionality = 3, unit = UnitOfAcceleration, unitName = MetrePerSecondSquared)#
    public Vector3 MetresPerSecondSquared => InUnit(UnitOfAcceleration.MetrePerSecondSquared);

    #Document:ScalarMagnitude(Acceleration3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(Acceleration3, 3, Acceleration)#
    public Acceleration Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(Acceleration3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(Acceleration3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(Acceleration3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(Acceleration3, 3)#
    public Acceleration3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(Acceleration3, 3)#
    public Acceleration3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(Acceleration3, 3)#
    public Acceleration3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(Acceleration3, 3)#
    public Acceleration Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(Acceleration3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(Acceleration3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(Acceleration3, 3)#
    public Acceleration3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(Acceleration3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(Acceleration3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(Acceleration3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [m / s^2]";

    #Document:InUnitInstance(Acceleration3, 3, UnitOfAcceleration, unitOfAcceleration)#
    public Vector3 InUnit(UnitOfAcceleration unitOfAcceleration) => InUnit(this, unitOfAcceleration);
    #Document:InUnitStatic(Acceleration3, acceleration3, 3, UnitOfAcceleration, unitOfAcceleration)#
    private static Vector3 InUnit(Acceleration3 acceleration3, UnitOfAcceleration unitOfAcceleration) => acceleration3.ToVector3() / unitOfAcceleration.Factor;
    
    #Document:PlusMethod(Acceleration3, 3)#
    public Acceleration3 Plus() => this;
    #Document:NegateMethod(Acceleration3, 3)#
    public Acceleration3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(Acceleration3, 3)#
    public static Acceleration3 operator +(Acceleration3 a) => a;
    #Document:NegateOperator(Acceleration3, 3)#
    public static Acceleration3 operator -(Acceleration3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(Acceleration3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(Acceleration3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(Acceleration3, 3)#
    public static Unhandled3 operator *(Acceleration3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(Acceleration3, 3)#
    public static Unhandled3 operator *(Unhandled a, Acceleration3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(Acceleration3, 3)#
    public static Unhandled3 operator /(Acceleration3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(Acceleration3, 3)#
    public Acceleration3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(Acceleration3, 3)#
    public Acceleration3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(Acceleration3, 3)#
    public Acceleration3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(Acceleration3, 3)#
    public static Acceleration3 operator %(Acceleration3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(Acceleration3, 3)#
    public static Acceleration3 operator *(Acceleration3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(Acceleration3, 3)#
    public static Acceleration3 operator *(double a, Acceleration3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(Acceleration3, 3)#
    public static Acceleration3 operator /(Acceleration3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(Acceleration3, 3)#
    public Acceleration3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(Acceleration3, 3)#
    public Acceleration3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(Acceleration3, 3)#
    public Acceleration3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(Acceleration3, 3)#
    public static Acceleration3 operator %(Acceleration3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(Acceleration3, 3)#
    public static Acceleration3 operator *(Acceleration3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(Acceleration3, 3)#
    public static Acceleration3 operator *(Scalar a, Acceleration3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(Acceleration3, 3)#
    public static Acceleration3 operator /(Acceleration3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(Acceleration3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(Acceleration3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(Acceleration3, 3)#
    public static Unhandled3 operator *(Acceleration3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(Acceleration3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, Acceleration3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(Acceleration3, 3)#
    public static Unhandled3 operator /(Acceleration3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(Acceleration3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(Acceleration3, 3)#
    public static implicit operator (double x, double y, double z)(Acceleration3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(Acceleration3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(Acceleration3, 3)#
    public static explicit operator Vector3(Acceleration3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(Acceleration3, 3)#
    public static Acceleration3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(Acceleration3, 3)#
    public static explicit operator Acceleration3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(Acceleration3, 3)#
    public static Acceleration3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(Acceleration3, 3)#
    public static explicit operator Acceleration3(Vector3 a) => new(a);
}
