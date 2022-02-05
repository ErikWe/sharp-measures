namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(SpinAngularAcceleration3, 3, SpinAngularAcceleration, UnitOfSpinAngularAcceleration, [RadianPerSecondSquared], [RadiansPerSecondSquared])#
public readonly partial record struct SpinAngularAcceleration3 :
    IVector3Quantity,
    IScalableVector3Quantity<SpinAngularAcceleration3>,
    INormalizableVector3Quantity<SpinAngularAcceleration3>,
    ITransformableVector3Quantity<SpinAngularAcceleration3>,
    IMultiplicableVector3Quantity<SpinAngularAcceleration3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<SpinAngularAcceleration3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<SpinAngularAcceleration3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(SpinAngularAcceleration3, 3)#
    public static SpinAngularAcceleration3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(SpinAngularAcceleration3, 3)#
    public double X { get; init; }
    #Document:ComponentY(SpinAngularAcceleration3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(SpinAngularAcceleration3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(SpinAngularAcceleration3, 3, UnitOfSpinAngularAcceleration, unitOfSpinAngularAcceleration, [RadianPerSecondSquared])#
    public SpinAngularAcceleration3((SpinAngularAcceleration x, SpinAngularAcceleration y, SpinAngularAcceleration z) components, UnitOfSpinAngularAcceleration unitOfSpinAngularAcceleration) : 
    	this(components.x, components.y, components.z, unitOfSpinAngularAcceleration) { }
    #Document:ConstructorComponentsUnit(SpinAngularAcceleration3, 3, UnitOfSpinAngularAcceleration, unitOfSpinAngularAcceleration, [RadianPerSecondSquared])#
    public SpinAngularAcceleration3(SpinAngularAcceleration x, SpinAngularAcceleration y, SpinAngularAcceleration z, UnitOfSpinAngularAcceleration unitOfSpinAngularAcceleration) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfSpinAngularAcceleration) { }
    #Document:ConstructorScalarTupleUnit(SpinAngularAcceleration3, 3, UnitOfSpinAngularAcceleration, unitOfSpinAngularAcceleration, [RadianPerSecondSquared])#
    public SpinAngularAcceleration3((Scalar x, Scalar y, Scalar z) components, UnitOfSpinAngularAcceleration unitOfSpinAngularAcceleration) : 
    	this(components.x, components.y, components.z, unitOfSpinAngularAcceleration) { }
    #Document:ConstructorScalarsUnit(SpinAngularAcceleration3, 3, UnitOfSpinAngularAcceleration, unitOfSpinAngularAcceleration, [RadianPerSecondSquared])#
    public SpinAngularAcceleration3(Scalar x, Scalar y, Scalar z, UnitOfSpinAngularAcceleration unitOfSpinAngularAcceleration) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfSpinAngularAcceleration) { }
    #Document:ConstructorVectorUnit(SpinAngularAcceleration3, 3, UnitOfSpinAngularAcceleration, unitOfSpinAngularAcceleration, [RadianPerSecondSquared])#
    public SpinAngularAcceleration3(Vector3 components, UnitOfSpinAngularAcceleration unitOfSpinAngularAcceleration) : 
    	this(components.X, components.Y, components.Z, unitOfSpinAngularAcceleration) { }
    #Document:ConstructorDoubleTupleUnit(SpinAngularAcceleration3, 3, UnitOfSpinAngularAcceleration, unitOfSpinAngularAcceleration, [RadianPerSecondSquared])#
    public SpinAngularAcceleration3((double x, double y, double z) components, UnitOfSpinAngularAcceleration unitOfSpinAngularAcceleration) : 
    	this(components.x, components.y, components.z, unitOfSpinAngularAcceleration) { }
    #Document:ConstructorDoublesTupleUnit(SpinAngularAcceleration3, 3, UnitOfSpinAngularAcceleration, unitOfSpinAngularAcceleration, [RadianPerSecondSquared])#
    public SpinAngularAcceleration3(double x, double y, double z, UnitOfSpinAngularAcceleration unitOfSpinAngularAcceleration) : 
    	this(x * unitOfSpinAngularAcceleration.Factor, y * unitOfSpinAngularAcceleration.Factor, z * unitOfSpinAngularAcceleration.Factor) { }

    #Document:ConstructorComponentTuple(SpinAngularAcceleration3, 3, UnitOfSpinAngularAcceleration, unitOfSpinAngularAcceleration, [RadianPerSecondSquared])#
    public SpinAngularAcceleration3((SpinAngularAcceleration x, SpinAngularAcceleration y, SpinAngularAcceleration z) components) : 
    	this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(SpinAngularAcceleration3, 3, UnitOfSpinAngularAcceleration, unitOfSpinAngularAcceleration, [RadianPerSecondSquared])#
    public SpinAngularAcceleration3(SpinAngularAcceleration x, SpinAngularAcceleration y, SpinAngularAcceleration z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(SpinAngularAcceleration3, 3, UnitOfSpinAngularAcceleration, unitOfSpinAngularAcceleration, [RadianPerSecondSquared])#
    public SpinAngularAcceleration3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(SpinAngularAcceleration3, 3, UnitOfSpinAngularAcceleration, unitOfSpinAngularAcceleration, [RadianPerSecondSquared])#
    public SpinAngularAcceleration3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(SpinAngularAcceleration3, 3, UnitOfSpinAngularAcceleration, unitOfSpinAngularAcceleration, [RadianPerSecondSquared])#
    public SpinAngularAcceleration3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(SpinAngularAcceleration3, 3, UnitOfSpinAngularAcceleration, unitOfSpinAngularAcceleration, [RadianPerSecondSquared])#
    public SpinAngularAcceleration3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(SpinAngularAcceleration3, 3, UnitOfSpinAngularAcceleration, unitOfSpinAngularAcceleration, [RadianPerSecondSquared])#
    public SpinAngularAcceleration3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

));

    #Document:InUnit(quantity = SpinAngularAcceleration3, dimensionality = 3, unit = UnitOfSpinAngularAcceleration, unitName = RadianPerSecondSquared)#
    public Vector3 RadiansPerSecondSquared => InUnit(UnitOfSpinAngularAcceleration.RadianPerSecondSquared);

    #Document:ScalarMagnitude(SpinAngularAcceleration3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(SpinAngularAcceleration3, 3, SpinAngularAcceleration)#
    public SpinAngularAcceleration Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(SpinAngularAcceleration3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(SpinAngularAcceleration3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(SpinAngularAcceleration3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(SpinAngularAcceleration3, 3)#
    public SpinAngularAcceleration3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(SpinAngularAcceleration3, 3)#
    public SpinAngularAcceleration3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(SpinAngularAcceleration3, 3)#
    public SpinAngularAcceleration3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(SpinAngularAcceleration3, 3)#
    public SpinAngularAcceleration Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(SpinAngularAcceleration3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(SpinAngularAcceleration3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(SpinAngularAcceleration3, 3)#
    public SpinAngularAcceleration3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(SpinAngularAcceleration3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(SpinAngularAcceleration3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(SpinAngularAcceleration3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [rad / s^2]";

    #Document:InUnitInstance(SpinAngularAcceleration3, 3, UnitOfSpinAngularAcceleration, unitOfSpinAngularAcceleration)#
    public Vector3 InUnit(UnitOfSpinAngularAcceleration unitOfSpinAngularAcceleration) => InUnit(this, unitOfSpinAngularAcceleration);
    #Document:InUnitStatic(SpinAngularAcceleration3, spinAngularAcceleration3, 3, UnitOfSpinAngularAcceleration, unitOfSpinAngularAcceleration)#
    private static Vector3 InUnit(SpinAngularAcceleration3 spinAngularAcceleration3, UnitOfSpinAngularAcceleration unitOfSpinAngularAcceleration) => 
    	spinAngularAcceleration3.ToVector3() / unitOfSpinAngularAcceleration.Factor;
    
    #Document:PlusMethod(SpinAngularAcceleration3, 3)#
    public SpinAngularAcceleration3 Plus() => this;
    #Document:NegateMethod(SpinAngularAcceleration3, 3)#
    public SpinAngularAcceleration3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(SpinAngularAcceleration3, 3)#
    public static SpinAngularAcceleration3 operator +(SpinAngularAcceleration3 a) => a;
    #Document:NegateOperator(SpinAngularAcceleration3, 3)#
    public static SpinAngularAcceleration3 operator -(SpinAngularAcceleration3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(SpinAngularAcceleration3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(SpinAngularAcceleration3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(SpinAngularAcceleration3, 3)#
    public static Unhandled3 operator *(SpinAngularAcceleration3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(SpinAngularAcceleration3, 3)#
    public static Unhandled3 operator *(Unhandled a, SpinAngularAcceleration3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(SpinAngularAcceleration3, 3)#
    public static Unhandled3 operator /(SpinAngularAcceleration3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(SpinAngularAcceleration3, 3)#
    public SpinAngularAcceleration3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(SpinAngularAcceleration3, 3)#
    public SpinAngularAcceleration3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(SpinAngularAcceleration3, 3)#
    public SpinAngularAcceleration3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(SpinAngularAcceleration3, 3)#
    public static SpinAngularAcceleration3 operator %(SpinAngularAcceleration3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(SpinAngularAcceleration3, 3)#
    public static SpinAngularAcceleration3 operator *(SpinAngularAcceleration3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(SpinAngularAcceleration3, 3)#
    public static SpinAngularAcceleration3 operator *(double a, SpinAngularAcceleration3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(SpinAngularAcceleration3, 3)#
    public static SpinAngularAcceleration3 operator /(SpinAngularAcceleration3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(SpinAngularAcceleration3, 3)#
    public SpinAngularAcceleration3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(SpinAngularAcceleration3, 3)#
    public SpinAngularAcceleration3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(SpinAngularAcceleration3, 3)#
    public SpinAngularAcceleration3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(SpinAngularAcceleration3, 3)#
    public static SpinAngularAcceleration3 operator %(SpinAngularAcceleration3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(SpinAngularAcceleration3, 3)#
    public static SpinAngularAcceleration3 operator *(SpinAngularAcceleration3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(SpinAngularAcceleration3, 3)#
    public static SpinAngularAcceleration3 operator *(Scalar a, SpinAngularAcceleration3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(SpinAngularAcceleration3, 3)#
    public static SpinAngularAcceleration3 operator /(SpinAngularAcceleration3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(SpinAngularAcceleration3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(SpinAngularAcceleration3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(SpinAngularAcceleration3, 3)#
    public static Unhandled3 operator *(SpinAngularAcceleration3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(SpinAngularAcceleration3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, SpinAngularAcceleration3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(SpinAngularAcceleration3, 3)#
    public static Unhandled3 operator /(SpinAngularAcceleration3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(SpinAngularAcceleration3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(SpinAngularAcceleration3, 3)#
    public static implicit operator (double x, double y, double z)(SpinAngularAcceleration3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(SpinAngularAcceleration3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(SpinAngularAcceleration3, 3)#
    public static explicit operator Vector3(SpinAngularAcceleration3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(SpinAngularAcceleration3, 3)#
    public static SpinAngularAcceleration3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(SpinAngularAcceleration3, 3)#
    public static explicit operator SpinAngularAcceleration3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(SpinAngularAcceleration3, 3)#
    public static SpinAngularAcceleration3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(SpinAngularAcceleration3, 3)#
    public static explicit operator SpinAngularAcceleration3(Vector3 a) => new(a);
}
