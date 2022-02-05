namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(AngularAcceleration3, 3, AngularAcceleration, UnitOfAngularAcceleration, [RadianPerSecondSquared], [RadiansPerSecondSquared])#
public readonly partial record struct AngularAcceleration3 :
    IVector3Quantity,
    IScalableVector3Quantity<AngularAcceleration3>,
    INormalizableVector3Quantity<AngularAcceleration3>,
    ITransformableVector3Quantity<AngularAcceleration3>,
    IMultiplicableVector3Quantity<AngularAcceleration3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<AngularAcceleration3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<AngularAcceleration3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(AngularAcceleration3, 3)#
    public static AngularAcceleration3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(AngularAcceleration3, 3)#
    public double X { get; init; }
    #Document:ComponentY(AngularAcceleration3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(AngularAcceleration3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(AngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public AngularAcceleration3((AngularAcceleration x, AngularAcceleration y, AngularAcceleration z) components, UnitOfAngularAcceleration unitOfAngularAcceleration) : 
    	this(components.x, components.y, components.z, unitOfAngularAcceleration) { }
    #Document:ConstructorComponentsUnit(AngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public AngularAcceleration3(AngularAcceleration x, AngularAcceleration y, AngularAcceleration z, UnitOfAngularAcceleration unitOfAngularAcceleration) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAngularAcceleration) { }
    #Document:ConstructorScalarTupleUnit(AngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public AngularAcceleration3((Scalar x, Scalar y, Scalar z) components, UnitOfAngularAcceleration unitOfAngularAcceleration) : 
    	this(components.x, components.y, components.z, unitOfAngularAcceleration) { }
    #Document:ConstructorScalarsUnit(AngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public AngularAcceleration3(Scalar x, Scalar y, Scalar z, UnitOfAngularAcceleration unitOfAngularAcceleration) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAngularAcceleration) { }
    #Document:ConstructorVectorUnit(AngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public AngularAcceleration3(Vector3 components, UnitOfAngularAcceleration unitOfAngularAcceleration) : 
    	this(components.X, components.Y, components.Z, unitOfAngularAcceleration) { }
    #Document:ConstructorDoubleTupleUnit(AngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public AngularAcceleration3((double x, double y, double z) components, UnitOfAngularAcceleration unitOfAngularAcceleration) : 
    	this(components.x, components.y, components.z, unitOfAngularAcceleration) { }
    #Document:ConstructorDoublesTupleUnit(AngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public AngularAcceleration3(double x, double y, double z, UnitOfAngularAcceleration unitOfAngularAcceleration) : 
    	this(x * unitOfAngularAcceleration.Factor, y * unitOfAngularAcceleration.Factor, z * unitOfAngularAcceleration.Factor) { }

    #Document:ConstructorComponentTuple(AngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public AngularAcceleration3((AngularAcceleration x, AngularAcceleration y, AngularAcceleration z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(AngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public AngularAcceleration3(AngularAcceleration x, AngularAcceleration y, AngularAcceleration z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(AngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public AngularAcceleration3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(AngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public AngularAcceleration3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(AngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public AngularAcceleration3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(AngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public AngularAcceleration3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(AngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration, [RadianPerSecondSquared])#
    public AngularAcceleration3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

));

    #Document:InUnit(quantity = AngularAcceleration3, dimensionality = 3, unit = UnitOfAngularAcceleration, unitName = RadianPerSecondSquared)#
    public Vector3 RadiansPerSecondSquared => InUnit(UnitOfAngularAcceleration.RadianPerSecondSquared);

    #Document:ScalarMagnitude(AngularAcceleration3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(AngularAcceleration3, 3, AngularAcceleration)#
    public AngularAcceleration Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(AngularAcceleration3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(AngularAcceleration3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(AngularAcceleration3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(AngularAcceleration3, 3)#
    public AngularAcceleration3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(AngularAcceleration3, 3)#
    public AngularAcceleration3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(AngularAcceleration3, 3)#
    public AngularAcceleration3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(AngularAcceleration3, 3)#
    public AngularAcceleration Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(AngularAcceleration3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(AngularAcceleration3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(AngularAcceleration3, 3)#
    public AngularAcceleration3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(AngularAcceleration3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(AngularAcceleration3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(AngularAcceleration3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [rad / s^2]";

    #Document:InUnitInstance(AngularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration)#
    public Vector3 InUnit(UnitOfAngularAcceleration unitOfAngularAcceleration) => InUnit(this, unitOfAngularAcceleration);
    #Document:InUnitStatic(AngularAcceleration3, angularAcceleration3, 3, UnitOfAngularAcceleration, unitOfAngularAcceleration)#
    private static Vector3 InUnit(AngularAcceleration3 angularAcceleration3, UnitOfAngularAcceleration unitOfAngularAcceleration) => 
    	angularAcceleration3.ToVector3() / unitOfAngularAcceleration.Factor;
    
    #Document:PlusMethod(AngularAcceleration3, 3)#
    public AngularAcceleration3 Plus() => this;
    #Document:NegateMethod(AngularAcceleration3, 3)#
    public AngularAcceleration3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(AngularAcceleration3, 3)#
    public static AngularAcceleration3 operator +(AngularAcceleration3 a) => a;
    #Document:NegateOperator(AngularAcceleration3, 3)#
    public static AngularAcceleration3 operator -(AngularAcceleration3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(AngularAcceleration3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(AngularAcceleration3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(AngularAcceleration3, 3)#
    public static Unhandled3 operator *(AngularAcceleration3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(AngularAcceleration3, 3)#
    public static Unhandled3 operator *(Unhandled a, AngularAcceleration3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(AngularAcceleration3, 3)#
    public static Unhandled3 operator /(AngularAcceleration3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(AngularAcceleration3, 3)#
    public AngularAcceleration3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(AngularAcceleration3, 3)#
    public AngularAcceleration3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(AngularAcceleration3, 3)#
    public AngularAcceleration3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(AngularAcceleration3, 3)#
    public static AngularAcceleration3 operator %(AngularAcceleration3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(AngularAcceleration3, 3)#
    public static AngularAcceleration3 operator *(AngularAcceleration3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(AngularAcceleration3, 3)#
    public static AngularAcceleration3 operator *(double a, AngularAcceleration3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(AngularAcceleration3, 3)#
    public static AngularAcceleration3 operator /(AngularAcceleration3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(AngularAcceleration3, 3)#
    public AngularAcceleration3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(AngularAcceleration3, 3)#
    public AngularAcceleration3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(AngularAcceleration3, 3)#
    public AngularAcceleration3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(AngularAcceleration3, 3)#
    public static AngularAcceleration3 operator %(AngularAcceleration3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(AngularAcceleration3, 3)#
    public static AngularAcceleration3 operator *(AngularAcceleration3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(AngularAcceleration3, 3)#
    public static AngularAcceleration3 operator *(Scalar a, AngularAcceleration3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(AngularAcceleration3, 3)#
    public static AngularAcceleration3 operator /(AngularAcceleration3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(AngularAcceleration3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(AngularAcceleration3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(AngularAcceleration3, 3)#
    public static Unhandled3 operator *(AngularAcceleration3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(AngularAcceleration3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, AngularAcceleration3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(AngularAcceleration3, 3)#
    public static Unhandled3 operator /(AngularAcceleration3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(AngularAcceleration3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(AngularAcceleration3, 3)#
    public static implicit operator (double x, double y, double z)(AngularAcceleration3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(AngularAcceleration3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(AngularAcceleration3, 3)#
    public static explicit operator Vector3(AngularAcceleration3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(AngularAcceleration3, 3)#
    public static AngularAcceleration3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(AngularAcceleration3, 3)#
    public static explicit operator AngularAcceleration3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(AngularAcceleration3, 3)#
    public static AngularAcceleration3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(AngularAcceleration3, 3)#
    public static explicit operator AngularAcceleration3(Vector3 a) => new(a);
}
