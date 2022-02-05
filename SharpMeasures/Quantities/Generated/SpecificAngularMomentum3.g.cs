namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(SpecificAngularMomentum3, 3, SpecificAngularMomentum, UnitOfSpecificAngularMomentum, [SquareMetrePerSecond], [SquareMetresPerSecond])#
public readonly partial record struct SpecificAngularMomentum3 :
    IVector3Quantity,
    IScalableVector3Quantity<SpecificAngularMomentum3>,
    INormalizableVector3Quantity<SpecificAngularMomentum3>,
    ITransformableVector3Quantity<SpecificAngularMomentum3>,
    IMultiplicableVector3Quantity<SpecificAngularMomentum3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<SpecificAngularMomentum3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<SpecificAngularMomentum3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(SpecificAngularMomentum3, 3)#
    public static SpecificAngularMomentum3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(SpecificAngularMomentum3, 3)#
    public double X { get; init; }
    #Document:ComponentY(SpecificAngularMomentum3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(SpecificAngularMomentum3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(SpecificAngularMomentum3, 3, UnitOfSpecificAngularMomentum, unitOfSpecificAngularMomentum, [SquareMetrePerSecond])#
    public SpecificAngularMomentum3((SpecificAngularMomentum x, SpecificAngularMomentum y, SpecificAngularMomentum z) components, UnitOfSpecificAngularMomentum unitOfSpecificAngularMomentum) : 
    	this(components.x, components.y, components.z, unitOfSpecificAngularMomentum) { }
    #Document:ConstructorComponentsUnit(SpecificAngularMomentum3, 3, UnitOfSpecificAngularMomentum, unitOfSpecificAngularMomentum, [SquareMetrePerSecond])#
    public SpecificAngularMomentum3(SpecificAngularMomentum x, SpecificAngularMomentum y, SpecificAngularMomentum z, UnitOfSpecificAngularMomentum unitOfSpecificAngularMomentum) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfSpecificAngularMomentum) { }
    #Document:ConstructorScalarTupleUnit(SpecificAngularMomentum3, 3, UnitOfSpecificAngularMomentum, unitOfSpecificAngularMomentum, [SquareMetrePerSecond])#
    public SpecificAngularMomentum3((Scalar x, Scalar y, Scalar z) components, UnitOfSpecificAngularMomentum unitOfSpecificAngularMomentum) : 
    	this(components.x, components.y, components.z, unitOfSpecificAngularMomentum) { }
    #Document:ConstructorScalarsUnit(SpecificAngularMomentum3, 3, UnitOfSpecificAngularMomentum, unitOfSpecificAngularMomentum, [SquareMetrePerSecond])#
    public SpecificAngularMomentum3(Scalar x, Scalar y, Scalar z, UnitOfSpecificAngularMomentum unitOfSpecificAngularMomentum) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfSpecificAngularMomentum) { }
    #Document:ConstructorVectorUnit(SpecificAngularMomentum3, 3, UnitOfSpecificAngularMomentum, unitOfSpecificAngularMomentum, [SquareMetrePerSecond])#
    public SpecificAngularMomentum3(Vector3 components, UnitOfSpecificAngularMomentum unitOfSpecificAngularMomentum) : 
    	this(components.X, components.Y, components.Z, unitOfSpecificAngularMomentum) { }
    #Document:ConstructorDoubleTupleUnit(SpecificAngularMomentum3, 3, UnitOfSpecificAngularMomentum, unitOfSpecificAngularMomentum, [SquareMetrePerSecond])#
    public SpecificAngularMomentum3((double x, double y, double z) components, UnitOfSpecificAngularMomentum unitOfSpecificAngularMomentum) : 
    	this(components.x, components.y, components.z, unitOfSpecificAngularMomentum) { }
    #Document:ConstructorDoublesTupleUnit(SpecificAngularMomentum3, 3, UnitOfSpecificAngularMomentum, unitOfSpecificAngularMomentum, [SquareMetrePerSecond])#
    public SpecificAngularMomentum3(double x, double y, double z, UnitOfSpecificAngularMomentum unitOfSpecificAngularMomentum) : 
    	this(x * unitOfSpecificAngularMomentum.Factor, y * unitOfSpecificAngularMomentum.Factor, z * unitOfSpecificAngularMomentum.Factor) { }

    #Document:ConstructorComponentTuple(SpecificAngularMomentum3, 3, UnitOfSpecificAngularMomentum, unitOfSpecificAngularMomentum, [SquareMetrePerSecond])#
    public SpecificAngularMomentum3((SpecificAngularMomentum x, SpecificAngularMomentum y, SpecificAngularMomentum z) components) : 
    	this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(SpecificAngularMomentum3, 3, UnitOfSpecificAngularMomentum, unitOfSpecificAngularMomentum, [SquareMetrePerSecond])#
    public SpecificAngularMomentum3(SpecificAngularMomentum x, SpecificAngularMomentum y, SpecificAngularMomentum z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(SpecificAngularMomentum3, 3, UnitOfSpecificAngularMomentum, unitOfSpecificAngularMomentum, [SquareMetrePerSecond])#
    public SpecificAngularMomentum3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(SpecificAngularMomentum3, 3, UnitOfSpecificAngularMomentum, unitOfSpecificAngularMomentum, [SquareMetrePerSecond])#
    public SpecificAngularMomentum3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(SpecificAngularMomentum3, 3, UnitOfSpecificAngularMomentum, unitOfSpecificAngularMomentum, [SquareMetrePerSecond])#
    public SpecificAngularMomentum3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(SpecificAngularMomentum3, 3, UnitOfSpecificAngularMomentum, unitOfSpecificAngularMomentum, [SquareMetrePerSecond])#
    public SpecificAngularMomentum3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(SpecificAngularMomentum3, 3, UnitOfSpecificAngularMomentum, unitOfSpecificAngularMomentum, [SquareMetrePerSecond])#
    public SpecificAngularMomentum3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    #Document:InUnit(quantity = SpecificAngularMomentum3, dimensionality = 3, unit = UnitOfSpecificAngularMomentum, unitName = SquareMetrePerSecond)#
    public Vector3 SquareMetresPerSecond => InUnit(UnitOfSpecificAngularMomentum.SquareMetrePerSecond);

    #Document:ScalarMagnitude(SpecificAngularMomentum3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(SpecificAngularMomentum3, 3, SpecificAngularMomentum)#
    public SpecificAngularMomentum Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(SpecificAngularMomentum3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(SpecificAngularMomentum3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(SpecificAngularMomentum3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(SpecificAngularMomentum3, 3)#
    public SpecificAngularMomentum3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(SpecificAngularMomentum3, 3)#
    public SpecificAngularMomentum3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(SpecificAngularMomentum3, 3)#
    public SpecificAngularMomentum3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(SpecificAngularMomentum3, 3)#
    public SpecificAngularMomentum Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(SpecificAngularMomentum3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(SpecificAngularMomentum3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(SpecificAngularMomentum3, 3)#
    public SpecificAngularMomentum3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(SpecificAngularMomentum3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(SpecificAngularMomentum3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(SpecificAngularMomentum3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [m^2 / s]";

    #Document:InUnitInstance(SpecificAngularMomentum3, 3, UnitOfSpecificAngularMomentum, unitOfSpecificAngularMomentum)#
    public Vector3 InUnit(UnitOfSpecificAngularMomentum unitOfSpecificAngularMomentum) => InUnit(this, unitOfSpecificAngularMomentum);
    #Document:InUnitStatic(SpecificAngularMomentum3, specificAngularMomentum3, 3, UnitOfSpecificAngularMomentum, unitOfSpecificAngularMomentum)#
    private static Vector3 InUnit(SpecificAngularMomentum3 specificAngularMomentum3, UnitOfSpecificAngularMomentum unitOfSpecificAngularMomentum) => 
    	specificAngularMomentum3.ToVector3() / unitOfSpecificAngularMomentum.Factor;
    
    #Document:PlusMethod(SpecificAngularMomentum3, 3)#
    public SpecificAngularMomentum3 Plus() => this;
    #Document:NegateMethod(SpecificAngularMomentum3, 3)#
    public SpecificAngularMomentum3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(SpecificAngularMomentum3, 3)#
    public static SpecificAngularMomentum3 operator +(SpecificAngularMomentum3 a) => a;
    #Document:NegateOperator(SpecificAngularMomentum3, 3)#
    public static SpecificAngularMomentum3 operator -(SpecificAngularMomentum3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(SpecificAngularMomentum3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(SpecificAngularMomentum3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(SpecificAngularMomentum3, 3)#
    public static Unhandled3 operator *(SpecificAngularMomentum3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(SpecificAngularMomentum3, 3)#
    public static Unhandled3 operator *(Unhandled a, SpecificAngularMomentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(SpecificAngularMomentum3, 3)#
    public static Unhandled3 operator /(SpecificAngularMomentum3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(SpecificAngularMomentum3, 3)#
    public SpecificAngularMomentum3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(SpecificAngularMomentum3, 3)#
    public SpecificAngularMomentum3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(SpecificAngularMomentum3, 3)#
    public SpecificAngularMomentum3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(SpecificAngularMomentum3, 3)#
    public static SpecificAngularMomentum3 operator %(SpecificAngularMomentum3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(SpecificAngularMomentum3, 3)#
    public static SpecificAngularMomentum3 operator *(SpecificAngularMomentum3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(SpecificAngularMomentum3, 3)#
    public static SpecificAngularMomentum3 operator *(double a, SpecificAngularMomentum3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(SpecificAngularMomentum3, 3)#
    public static SpecificAngularMomentum3 operator /(SpecificAngularMomentum3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(SpecificAngularMomentum3, 3)#
    public SpecificAngularMomentum3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(SpecificAngularMomentum3, 3)#
    public SpecificAngularMomentum3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(SpecificAngularMomentum3, 3)#
    public SpecificAngularMomentum3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(SpecificAngularMomentum3, 3)#
    public static SpecificAngularMomentum3 operator %(SpecificAngularMomentum3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(SpecificAngularMomentum3, 3)#
    public static SpecificAngularMomentum3 operator *(SpecificAngularMomentum3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(SpecificAngularMomentum3, 3)#
    public static SpecificAngularMomentum3 operator *(Scalar a, SpecificAngularMomentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(SpecificAngularMomentum3, 3)#
    public static SpecificAngularMomentum3 operator /(SpecificAngularMomentum3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(SpecificAngularMomentum3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(SpecificAngularMomentum3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(SpecificAngularMomentum3, 3)#
    public static Unhandled3 operator *(SpecificAngularMomentum3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(SpecificAngularMomentum3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, SpecificAngularMomentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(SpecificAngularMomentum3, 3)#
    public static Unhandled3 operator /(SpecificAngularMomentum3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(SpecificAngularMomentum3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(SpecificAngularMomentum3, 3)#
    public static implicit operator (double x, double y, double z)(SpecificAngularMomentum3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(SpecificAngularMomentum3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(SpecificAngularMomentum3, 3)#
    public static explicit operator Vector3(SpecificAngularMomentum3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(SpecificAngularMomentum3, 3)#
    public static SpecificAngularMomentum3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(SpecificAngularMomentum3, 3)#
    public static explicit operator SpecificAngularMomentum3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(SpecificAngularMomentum3, 3)#
    public static SpecificAngularMomentum3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(SpecificAngularMomentum3, 3)#
    public static explicit operator SpecificAngularMomentum3(Vector3 a) => new(a);
}
