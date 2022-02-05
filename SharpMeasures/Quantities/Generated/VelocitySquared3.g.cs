namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(VelocitySquared3, 3, SpeedSquared, UnitOfVelocitySquared, [SquareMetrePerSecondSquared], [SquareMetresPerSecondSquared])#
public readonly partial record struct VelocitySquared3 :
    IVector3Quantity,
    IScalableVector3Quantity<VelocitySquared3>,
    INormalizableVector3Quantity<VelocitySquared3>,
    ITransformableVector3Quantity<VelocitySquared3>,
    IMultiplicableVector3Quantity<VelocitySquared3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<VelocitySquared3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<VelocitySquared3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(VelocitySquared3, 3)#
    public static VelocitySquared3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(VelocitySquared3, 3)#
    public double X { get; init; }
    #Document:ComponentY(VelocitySquared3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(VelocitySquared3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(VelocitySquared3, 3, UnitOfVelocitySquared, unitOfVelocitySquared, [SquareMetrePerSecondSquared])#
    public VelocitySquared3((SpeedSquared x, SpeedSquared y, SpeedSquared z) components, UnitOfVelocitySquared unitOfVelocitySquared) : 
    	this(components.x, components.y, components.z, unitOfVelocitySquared) { }
    #Document:ConstructorComponentsUnit(VelocitySquared3, 3, UnitOfVelocitySquared, unitOfVelocitySquared, [SquareMetrePerSecondSquared])#
    public VelocitySquared3(SpeedSquared x, SpeedSquared y, SpeedSquared z, UnitOfVelocitySquared unitOfVelocitySquared) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfVelocitySquared) { }
    #Document:ConstructorScalarTupleUnit(VelocitySquared3, 3, UnitOfVelocitySquared, unitOfVelocitySquared, [SquareMetrePerSecondSquared])#
    public VelocitySquared3((Scalar x, Scalar y, Scalar z) components, UnitOfVelocitySquared unitOfVelocitySquared) : 
    	this(components.x, components.y, components.z, unitOfVelocitySquared) { }
    #Document:ConstructorScalarsUnit(VelocitySquared3, 3, UnitOfVelocitySquared, unitOfVelocitySquared, [SquareMetrePerSecondSquared])#
    public VelocitySquared3(Scalar x, Scalar y, Scalar z, UnitOfVelocitySquared unitOfVelocitySquared) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfVelocitySquared) { }
    #Document:ConstructorVectorUnit(VelocitySquared3, 3, UnitOfVelocitySquared, unitOfVelocitySquared, [SquareMetrePerSecondSquared])#
    public VelocitySquared3(Vector3 components, UnitOfVelocitySquared unitOfVelocitySquared) : this(components.X, components.Y, components.Z, unitOfVelocitySquared) { }
    #Document:ConstructorDoubleTupleUnit(VelocitySquared3, 3, UnitOfVelocitySquared, unitOfVelocitySquared, [SquareMetrePerSecondSquared])#
    public VelocitySquared3((double x, double y, double z) components, UnitOfVelocitySquared unitOfVelocitySquared) : 
    	this(components.x, components.y, components.z, unitOfVelocitySquared) { }
    #Document:ConstructorDoublesTupleUnit(VelocitySquared3, 3, UnitOfVelocitySquared, unitOfVelocitySquared, [SquareMetrePerSecondSquared])#
    public VelocitySquared3(double x, double y, double z, UnitOfVelocitySquared unitOfVelocitySquared) : 
    	this(x * unitOfVelocitySquared.Factor, y * unitOfVelocitySquared.Factor, z * unitOfVelocitySquared.Factor) { }

    #Document:ConstructorComponentTuple(VelocitySquared3, 3, UnitOfVelocitySquared, unitOfVelocitySquared, [SquareMetrePerSecondSquared])#
    public VelocitySquared3((SpeedSquared x, SpeedSquared y, SpeedSquared z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(VelocitySquared3, 3, UnitOfVelocitySquared, unitOfVelocitySquared, [SquareMetrePerSecondSquared])#
    public VelocitySquared3(SpeedSquared x, SpeedSquared y, SpeedSquared z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(VelocitySquared3, 3, UnitOfVelocitySquared, unitOfVelocitySquared, [SquareMetrePerSecondSquared])#
    public VelocitySquared3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(VelocitySquared3, 3, UnitOfVelocitySquared, unitOfVelocitySquared, [SquareMetrePerSecondSquared])#
    public VelocitySquared3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(VelocitySquared3, 3, UnitOfVelocitySquared, unitOfVelocitySquared, [SquareMetrePerSecondSquared])#
    public VelocitySquared3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(VelocitySquared3, 3, UnitOfVelocitySquared, unitOfVelocitySquared, [SquareMetrePerSecondSquared])#
    public VelocitySquared3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(VelocitySquared3, 3, UnitOfVelocitySquared, unitOfVelocitySquared, [SquareMetrePerSecondSquared])#
    public VelocitySquared3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    #Document:InUnit(quantity = VelocitySquared3, dimensionality = 3, unit = UnitOfVelocitySquared, unitName = SquareMetrePerSecondSquared)#
    public Vector3 SquareMetresPerSecondSquared => InUnit(UnitOfVelocitySquared.SquareMetrePerSecondSquared);

    #Document:ScalarMagnitude(VelocitySquared3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(VelocitySquared3, 3, SpeedSquared)#
    public SpeedSquared Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(VelocitySquared3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(VelocitySquared3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(VelocitySquared3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(VelocitySquared3, 3)#
    public VelocitySquared3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(VelocitySquared3, 3)#
    public VelocitySquared3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(VelocitySquared3, 3)#
    public VelocitySquared3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(VelocitySquared3, 3)#
    public SpeedSquared Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(VelocitySquared3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(VelocitySquared3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(VelocitySquared3, 3)#
    public VelocitySquared3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(VelocitySquared3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(VelocitySquared3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(VelocitySquared3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [m^2 / s^2]";

    #Document:InUnitInstance(VelocitySquared3, 3, UnitOfVelocitySquared, unitOfVelocitySquared)#
    public Vector3 InUnit(UnitOfVelocitySquared unitOfVelocitySquared) => InUnit(this, unitOfVelocitySquared);
    #Document:InUnitStatic(VelocitySquared3, velocitySquared3, 3, UnitOfVelocitySquared, unitOfVelocitySquared)#
    private static Vector3 InUnit(VelocitySquared3 velocitySquared3, UnitOfVelocitySquared unitOfVelocitySquared) => 
    	velocitySquared3.ToVector3() / unitOfVelocitySquared.Factor;
    
    #Document:PlusMethod(VelocitySquared3, 3)#
    public VelocitySquared3 Plus() => this;
    #Document:NegateMethod(VelocitySquared3, 3)#
    public VelocitySquared3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(VelocitySquared3, 3)#
    public static VelocitySquared3 operator +(VelocitySquared3 a) => a;
    #Document:NegateOperator(VelocitySquared3, 3)#
    public static VelocitySquared3 operator -(VelocitySquared3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(VelocitySquared3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(VelocitySquared3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(VelocitySquared3, 3)#
    public static Unhandled3 operator *(VelocitySquared3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(VelocitySquared3, 3)#
    public static Unhandled3 operator *(Unhandled a, VelocitySquared3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(VelocitySquared3, 3)#
    public static Unhandled3 operator /(VelocitySquared3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(VelocitySquared3, 3)#
    public VelocitySquared3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(VelocitySquared3, 3)#
    public VelocitySquared3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(VelocitySquared3, 3)#
    public VelocitySquared3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(VelocitySquared3, 3)#
    public static VelocitySquared3 operator %(VelocitySquared3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(VelocitySquared3, 3)#
    public static VelocitySquared3 operator *(VelocitySquared3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(VelocitySquared3, 3)#
    public static VelocitySquared3 operator *(double a, VelocitySquared3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(VelocitySquared3, 3)#
    public static VelocitySquared3 operator /(VelocitySquared3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(VelocitySquared3, 3)#
    public VelocitySquared3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(VelocitySquared3, 3)#
    public VelocitySquared3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(VelocitySquared3, 3)#
    public VelocitySquared3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(VelocitySquared3, 3)#
    public static VelocitySquared3 operator %(VelocitySquared3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(VelocitySquared3, 3)#
    public static VelocitySquared3 operator *(VelocitySquared3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(VelocitySquared3, 3)#
    public static VelocitySquared3 operator *(Scalar a, VelocitySquared3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(VelocitySquared3, 3)#
    public static VelocitySquared3 operator /(VelocitySquared3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(VelocitySquared3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(VelocitySquared3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(VelocitySquared3, 3)#
    public static Unhandled3 operator *(VelocitySquared3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(VelocitySquared3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, VelocitySquared3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(VelocitySquared3, 3)#
    public static Unhandled3 operator /(VelocitySquared3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(VelocitySquared3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(VelocitySquared3, 3)#
    public static implicit operator (double x, double y, double z)(VelocitySquared3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(VelocitySquared3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(VelocitySquared3, 3)#
    public static explicit operator Vector3(VelocitySquared3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(VelocitySquared3, 3)#
    public static VelocitySquared3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(VelocitySquared3, 3)#
    public static explicit operator VelocitySquared3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(VelocitySquared3, 3)#
    public static VelocitySquared3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(VelocitySquared3, 3)#
    public static explicit operator VelocitySquared3(Vector3 a) => new(a);
}
