namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(GravitationalAcceleration3, 3, GravitationalAcceleration, UnitOfAcceleration, [StandardGravity, MetrePerSecondSquared], [StandardGravity, MetresPerSecondSquared])#
public readonly partial record struct GravitationalAcceleration3 :
    IVector3Quantity,
    IScalableVector3Quantity<GravitationalAcceleration3>,
    INormalizableVector3Quantity<GravitationalAcceleration3>,
    ITransformableVector3Quantity<GravitationalAcceleration3>,
    IMultiplicableVector3Quantity<GravitationalAcceleration3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<GravitationalAcceleration3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<GravitationalAcceleration3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(GravitationalAcceleration3, 3)#
    public static GravitationalAcceleration3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(GravitationalAcceleration3, 3)#
    public double X { get; init; }
    #Document:ComponentY(GravitationalAcceleration3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(GravitationalAcceleration3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(GravitationalAcceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [StandardGravity, MetrePerSecondSquared])#
    public GravitationalAcceleration3((GravitationalAcceleration x, GravitationalAcceleration y, GravitationalAcceleration z) components, UnitOfAcceleration unitOfAcceleration) : 
    	this(components.x, components.y, components.z, unitOfAcceleration) { }
    #Document:ConstructorComponentsUnit(GravitationalAcceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [StandardGravity, MetrePerSecondSquared])#
    public GravitationalAcceleration3(GravitationalAcceleration x, GravitationalAcceleration y, GravitationalAcceleration z, UnitOfAcceleration unitOfAcceleration) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAcceleration) { }
    #Document:ConstructorScalarTupleUnit(GravitationalAcceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [StandardGravity, MetrePerSecondSquared])#
    public GravitationalAcceleration3((Scalar x, Scalar y, Scalar z) components, UnitOfAcceleration unitOfAcceleration) : 
    	this(components.x, components.y, components.z, unitOfAcceleration) { }
    #Document:ConstructorScalarsUnit(GravitationalAcceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [StandardGravity, MetrePerSecondSquared])#
    public GravitationalAcceleration3(Scalar x, Scalar y, Scalar z, UnitOfAcceleration unitOfAcceleration) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAcceleration) { }
    #Document:ConstructorVectorUnit(GravitationalAcceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [StandardGravity, MetrePerSecondSquared])#
    public GravitationalAcceleration3(Vector3 components, UnitOfAcceleration unitOfAcceleration) : this(components.X, components.Y, components.Z, unitOfAcceleration) { }
    #Document:ConstructorDoubleTupleUnit(GravitationalAcceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [StandardGravity, MetrePerSecondSquared])#
    public GravitationalAcceleration3((double x, double y, double z) components, UnitOfAcceleration unitOfAcceleration) : 
    	this(components.x, components.y, components.z, unitOfAcceleration) { }
    #Document:ConstructorDoublesTupleUnit(GravitationalAcceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [StandardGravity, MetrePerSecondSquared])#
    public GravitationalAcceleration3(double x, double y, double z, UnitOfAcceleration unitOfAcceleration) : 
    	this(x * unitOfAcceleration.Factor, y * unitOfAcceleration.Factor, z * unitOfAcceleration.Factor) { }

    #Document:ConstructorComponentTuple(GravitationalAcceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [StandardGravity, MetrePerSecondSquared])#
    public GravitationalAcceleration3((GravitationalAcceleration x, GravitationalAcceleration y, GravitationalAcceleration z) components) : 
    	this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(GravitationalAcceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [StandardGravity, MetrePerSecondSquared])#
    public GravitationalAcceleration3(GravitationalAcceleration x, GravitationalAcceleration y, GravitationalAcceleration z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(GravitationalAcceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [StandardGravity, MetrePerSecondSquared])#
    public GravitationalAcceleration3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(GravitationalAcceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [StandardGravity, MetrePerSecondSquared])#
    public GravitationalAcceleration3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(GravitationalAcceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [StandardGravity, MetrePerSecondSquared])#
    public GravitationalAcceleration3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(GravitationalAcceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [StandardGravity, MetrePerSecondSquared])#
    public GravitationalAcceleration3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(GravitationalAcceleration3, 3, UnitOfAcceleration, unitOfAcceleration, [StandardGravity, MetrePerSecondSquared])#
    public GravitationalAcceleration3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

);

    #Document:InUnit(quantity = GravitationalAcceleration3, dimensionality = 3, unit = UnitOfAcceleration, unitName = StandardGravity)#
    public Vector3 StandardGravity => InUnit(UnitOfAcceleration.StandardGravity);
    #Document:InUnit(quantity = GravitationalAcceleration3, dimensionality = 3, unit = UnitOfAcceleration, unitName = MetrePerSecondSquared)#
    public Vector3 MetresPerSecondSquared => InUnit(UnitOfAcceleration.MetrePerSecondSquared);

    #Document:ScalarMagnitude(GravitationalAcceleration3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(GravitationalAcceleration3, 3, GravitationalAcceleration)#
    public GravitationalAcceleration Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(GravitationalAcceleration3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(GravitationalAcceleration3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(GravitationalAcceleration3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(GravitationalAcceleration3, 3)#
    public GravitationalAcceleration3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(GravitationalAcceleration3, 3)#
    public GravitationalAcceleration3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(GravitationalAcceleration3, 3)#
    public GravitationalAcceleration3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(GravitationalAcceleration3, 3)#
    public GravitationalAcceleration Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(GravitationalAcceleration3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(GravitationalAcceleration3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(GravitationalAcceleration3, 3)#
    public GravitationalAcceleration3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(GravitationalAcceleration3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(GravitationalAcceleration3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(GravitationalAcceleration3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [m / s^2]";

    #Document:InUnitInstance(GravitationalAcceleration3, 3, UnitOfAcceleration, unitOfAcceleration)#
    public Vector3 InUnit(UnitOfAcceleration unitOfAcceleration) => InUnit(this, unitOfAcceleration);
    #Document:InUnitStatic(GravitationalAcceleration3, gravitationalAcceleration3, 3, UnitOfAcceleration, unitOfAcceleration)#
    private static Vector3 InUnit(GravitationalAcceleration3 gravitationalAcceleration3, UnitOfAcceleration unitOfAcceleration) => 
    	gravitationalAcceleration3.ToVector3() / unitOfAcceleration.Factor;
    
    #Document:PlusMethod(GravitationalAcceleration3, 3)#
    public GravitationalAcceleration3 Plus() => this;
    #Document:NegateMethod(GravitationalAcceleration3, 3)#
    public GravitationalAcceleration3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(GravitationalAcceleration3, 3)#
    public static GravitationalAcceleration3 operator +(GravitationalAcceleration3 a) => a;
    #Document:NegateOperator(GravitationalAcceleration3, 3)#
    public static GravitationalAcceleration3 operator -(GravitationalAcceleration3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(GravitationalAcceleration3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(GravitationalAcceleration3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(GravitationalAcceleration3, 3)#
    public static Unhandled3 operator *(GravitationalAcceleration3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(GravitationalAcceleration3, 3)#
    public static Unhandled3 operator *(Unhandled a, GravitationalAcceleration3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(GravitationalAcceleration3, 3)#
    public static Unhandled3 operator /(GravitationalAcceleration3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(GravitationalAcceleration3, 3)#
    public GravitationalAcceleration3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(GravitationalAcceleration3, 3)#
    public GravitationalAcceleration3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(GravitationalAcceleration3, 3)#
    public GravitationalAcceleration3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(GravitationalAcceleration3, 3)#
    public static GravitationalAcceleration3 operator %(GravitationalAcceleration3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(GravitationalAcceleration3, 3)#
    public static GravitationalAcceleration3 operator *(GravitationalAcceleration3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(GravitationalAcceleration3, 3)#
    public static GravitationalAcceleration3 operator *(double a, GravitationalAcceleration3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(GravitationalAcceleration3, 3)#
    public static GravitationalAcceleration3 operator /(GravitationalAcceleration3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(GravitationalAcceleration3, 3)#
    public GravitationalAcceleration3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(GravitationalAcceleration3, 3)#
    public GravitationalAcceleration3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(GravitationalAcceleration3, 3)#
    public GravitationalAcceleration3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(GravitationalAcceleration3, 3)#
    public static GravitationalAcceleration3 operator %(GravitationalAcceleration3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(GravitationalAcceleration3, 3)#
    public static GravitationalAcceleration3 operator *(GravitationalAcceleration3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(GravitationalAcceleration3, 3)#
    public static GravitationalAcceleration3 operator *(Scalar a, GravitationalAcceleration3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(GravitationalAcceleration3, 3)#
    public static GravitationalAcceleration3 operator /(GravitationalAcceleration3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(GravitationalAcceleration3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(GravitationalAcceleration3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(GravitationalAcceleration3, 3)#
    public static Unhandled3 operator *(GravitationalAcceleration3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(GravitationalAcceleration3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, GravitationalAcceleration3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(GravitationalAcceleration3, 3)#
    public static Unhandled3 operator /(GravitationalAcceleration3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(GravitationalAcceleration3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(GravitationalAcceleration3, 3)#
    public static implicit operator (double x, double y, double z)(GravitationalAcceleration3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(GravitationalAcceleration3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(GravitationalAcceleration3, 3)#
    public static explicit operator Vector3(GravitationalAcceleration3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(GravitationalAcceleration3, 3)#
    public static GravitationalAcceleration3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(GravitationalAcceleration3, 3)#
    public static explicit operator GravitationalAcceleration3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(GravitationalAcceleration3, 3)#
    public static GravitationalAcceleration3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(GravitationalAcceleration3, 3)#
    public static explicit operator GravitationalAcceleration3(Vector3 a) => new(a);
}
