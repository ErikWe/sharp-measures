namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(OrbitalAngularAcceleration3, 3, OrbitalAngularAcceleration, UnitOfAngularAcceleration, [RadianPerSecondSquared], [RadiansPerSecondSquared])#
public readonly partial record struct OrbitalAngularAcceleration3 :
    IVector3Quantity,
    IScalableVector3Quantity<OrbitalAngularAcceleration3>,
    INormalizableVector3Quantity<OrbitalAngularAcceleration3>,
    ITransformableVector3Quantity<OrbitalAngularAcceleration3>,
    IMultiplicableVector3Quantity<OrbitalAngularAcceleration3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<OrbitalAngularAcceleration3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<OrbitalAngularAcceleration3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(OrbitalAngularAcceleration3, 3)#
    public static OrbitalAngularAcceleration3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(OrbitalAngularAcceleration3, 3)#
    public double X { get; init; }
    #Document:ComponentY(OrbitalAngularAcceleration3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(OrbitalAngularAcceleration3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(OrbitalAngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public OrbitalAngularAcceleration3((OrbitalAngularAcceleration x, OrbitalAngularAcceleration y, OrbitalAngularAcceleration z) components, UnitOfAngularAcceleration unitOfAngularAcceleration) : 
    	this(components.x, components.y, components.z, unitOfAngularAcceleration) { }
    #Document:ConstructorComponentsUnit(OrbitalAngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public OrbitalAngularAcceleration3(OrbitalAngularAcceleration x, OrbitalAngularAcceleration y, OrbitalAngularAcceleration z, UnitOfAngularAcceleration unitOfAngularAcceleration) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAngularAcceleration) { }
    #Document:ConstructorScalarTupleUnit(OrbitalAngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public OrbitalAngularAcceleration3((Scalar x, Scalar y, Scalar z) components, UnitOfAngularAcceleration unitOfAngularAcceleration) : 
    	this(components.x, components.y, components.z, unitOfAngularAcceleration) { }
    #Document:ConstructorScalarsUnit(OrbitalAngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public OrbitalAngularAcceleration3(Scalar x, Scalar y, Scalar z, UnitOfAngularAcceleration unitOfAngularAcceleration) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAngularAcceleration) { }
    #Document:ConstructorVectorUnit(OrbitalAngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public OrbitalAngularAcceleration3(Vector3 components, UnitOfAngularAcceleration unitOfAngularAcceleration) : 
    	this(components.X, components.Y, components.Z, unitOfAngularAcceleration) { }
    #Document:ConstructorDoubleTupleUnit(OrbitalAngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public OrbitalAngularAcceleration3((double x, double y, double z) components, UnitOfAngularAcceleration unitOfAngularAcceleration) : 
    	this(components.x, components.y, components.z, unitOfAngularAcceleration) { }
    #Document:ConstructorDoublesTupleUnit(OrbitalAngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public OrbitalAngularAcceleration3(double x, double y, double z, UnitOfAngularAcceleration unitOfAngularAcceleration) : 
    	this(x * unitOfAngularAcceleration.Factor, y * unitOfAngularAcceleration.Factor, z * unitOfAngularAcceleration.Factor) { }

    #Document:ConstructorComponentTuple(OrbitalAngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public OrbitalAngularAcceleration3((OrbitalAngularAcceleration x, OrbitalAngularAcceleration y, OrbitalAngularAcceleration z) components) : 
    	this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(OrbitalAngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public OrbitalAngularAcceleration3(OrbitalAngularAcceleration x, OrbitalAngularAcceleration y, OrbitalAngularAcceleration z) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(OrbitalAngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public OrbitalAngularAcceleration3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(OrbitalAngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public OrbitalAngularAcceleration3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(OrbitalAngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public OrbitalAngularAcceleration3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(OrbitalAngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public OrbitalAngularAcceleration3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(OrbitalAngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public OrbitalAngularAcceleration3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

));

    #Document:InUnit(quantity = OrbitalAngularAcceleration3, dimensionality = 3, unit = UnitOfAngularAcceleration, unitName = RadianPerSecondSquared)#
    public Vector3 RadiansPerSecondSquared => InUnit(UnitOfAngularAcceleration.RadianPerSecondSquared);

    #Document:ScalarMagnitude(OrbitalAngularAcceleration3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(OrbitalAngularAcceleration3, 3, OrbitalAngularAcceleration)#
    public OrbitalAngularAcceleration Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(OrbitalAngularAcceleration3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(OrbitalAngularAcceleration3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(OrbitalAngularAcceleration3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(OrbitalAngularAcceleration3, 3)#
    public OrbitalAngularAcceleration3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(OrbitalAngularAcceleration3, 3)#
    public OrbitalAngularAcceleration3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(OrbitalAngularAcceleration3, 3)#
    public OrbitalAngularAcceleration3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(OrbitalAngularAcceleration3, 3)#
    public OrbitalAngularAcceleration Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(OrbitalAngularAcceleration3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(OrbitalAngularAcceleration3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(OrbitalAngularAcceleration3, 3)#
    public OrbitalAngularAcceleration3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(OrbitalAngularAcceleration3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(OrbitalAngularAcceleration3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(OrbitalAngularAcceleration3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [rad / s^2]";

    #Document:InUnitInstance(OrbitalAngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration)#
    public Vector3 InUnit(UnitOfAngularAcceleration unitOfAngularAcceleration) => InUnit(this, unitOfAngularAcceleration);
    #Document:InUnitStatic(OrbitalAngularAcceleration3, orbitalAngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration)#
    private static Vector3 InUnit(OrbitalAngularAcceleration3 orbitalAngularAcceleration3, UnitOfAngularAcceleration unitOfAngularAcceleration) => 
    	orbitalAngularAcceleration3.ToVector3() / unitOfAngularAcceleration.Factor;
    
    #Document:PlusMethod(OrbitalAngularAcceleration3, 3)#
    public OrbitalAngularAcceleration3 Plus() => this;
    #Document:NegateMethod(OrbitalAngularAcceleration3, 3)#
    public OrbitalAngularAcceleration3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(OrbitalAngularAcceleration3, 3)#
    public static OrbitalAngularAcceleration3 operator +(OrbitalAngularAcceleration3 a) => a;
    #Document:NegateOperator(OrbitalAngularAcceleration3, 3)#
    public static OrbitalAngularAcceleration3 operator -(OrbitalAngularAcceleration3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(OrbitalAngularAcceleration3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(OrbitalAngularAcceleration3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(OrbitalAngularAcceleration3, 3)#
    public static Unhandled3 operator *(OrbitalAngularAcceleration3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(OrbitalAngularAcceleration3, 3)#
    public static Unhandled3 operator *(Unhandled a, OrbitalAngularAcceleration3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(OrbitalAngularAcceleration3, 3)#
    public static Unhandled3 operator /(OrbitalAngularAcceleration3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(OrbitalAngularAcceleration3, 3)#
    public OrbitalAngularAcceleration3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(OrbitalAngularAcceleration3, 3)#
    public OrbitalAngularAcceleration3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(OrbitalAngularAcceleration3, 3)#
    public OrbitalAngularAcceleration3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(OrbitalAngularAcceleration3, 3)#
    public static OrbitalAngularAcceleration3 operator %(OrbitalAngularAcceleration3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(OrbitalAngularAcceleration3, 3)#
    public static OrbitalAngularAcceleration3 operator *(OrbitalAngularAcceleration3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(OrbitalAngularAcceleration3, 3)#
    public static OrbitalAngularAcceleration3 operator *(double a, OrbitalAngularAcceleration3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(OrbitalAngularAcceleration3, 3)#
    public static OrbitalAngularAcceleration3 operator /(OrbitalAngularAcceleration3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(OrbitalAngularAcceleration3, 3)#
    public OrbitalAngularAcceleration3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(OrbitalAngularAcceleration3, 3)#
    public OrbitalAngularAcceleration3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(OrbitalAngularAcceleration3, 3)#
    public OrbitalAngularAcceleration3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(OrbitalAngularAcceleration3, 3)#
    public static OrbitalAngularAcceleration3 operator %(OrbitalAngularAcceleration3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(OrbitalAngularAcceleration3, 3)#
    public static OrbitalAngularAcceleration3 operator *(OrbitalAngularAcceleration3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(OrbitalAngularAcceleration3, 3)#
    public static OrbitalAngularAcceleration3 operator *(Scalar a, OrbitalAngularAcceleration3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(OrbitalAngularAcceleration3, 3)#
    public static OrbitalAngularAcceleration3 operator /(OrbitalAngularAcceleration3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(OrbitalAngularAcceleration3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(OrbitalAngularAcceleration3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(OrbitalAngularAcceleration3, 3)#
    public static Unhandled3 operator *(OrbitalAngularAcceleration3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(OrbitalAngularAcceleration3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, OrbitalAngularAcceleration3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(OrbitalAngularAcceleration3, 3)#
    public static Unhandled3 operator /(OrbitalAngularAcceleration3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(OrbitalAngularAcceleration3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(OrbitalAngularAcceleration3, 3)#
    public static implicit operator (double x, double y, double z)(OrbitalAngularAcceleration3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(OrbitalAngularAcceleration3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(OrbitalAngularAcceleration3, 3)#
    public static explicit operator Vector3(OrbitalAngularAcceleration3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(OrbitalAngularAcceleration3, 3)#
    public static OrbitalAngularAcceleration3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(OrbitalAngularAcceleration3, 3)#
    public static explicit operator OrbitalAngularAcceleration3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(OrbitalAngularAcceleration3, 3)#
    public static OrbitalAngularAcceleration3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(OrbitalAngularAcceleration3, 3)#
    public static explicit operator OrbitalAngularAcceleration3(Vector3 a) => new(a);
}
