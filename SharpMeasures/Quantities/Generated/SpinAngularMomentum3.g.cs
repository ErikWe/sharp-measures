namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(SpinAngularMomentum3, 3, SpinAngularMomentum, UnitOfSpinAngularMomentum, [KilogramMetreSquaredPerSecond], [KilogramMetresSquaredPerSecond])#
public readonly partial record struct SpinAngularMomentum3 :
    IVector3Quantity,
    IScalableVector3Quantity<SpinAngularMomentum3>,
    INormalizableVector3Quantity<SpinAngularMomentum3>,
    ITransformableVector3Quantity<SpinAngularMomentum3>,
    IMultiplicableVector3Quantity<SpinAngularMomentum3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<SpinAngularMomentum3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<SpinAngularMomentum3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(SpinAngularMomentum3, 3)#
    public static SpinAngularMomentum3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(SpinAngularMomentum3, 3)#
    public double X { get; init; }
    #Document:ComponentY(SpinAngularMomentum3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(SpinAngularMomentum3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(SpinAngularMomentum3, 3, UnitOfSpinAngularMomentum, unitOfSpinAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public SpinAngularMomentum3((SpinAngularMomentum x, SpinAngularMomentum y, SpinAngularMomentum z) components, UnitOfSpinAngularMomentum unitOfSpinAngularMomentum) : 
    	this(components.x, components.y, components.z, unitOfSpinAngularMomentum) { }
    #Document:ConstructorComponentsUnit(SpinAngularMomentum3, 3, UnitOfSpinAngularMomentum, unitOfSpinAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public SpinAngularMomentum3(SpinAngularMomentum x, SpinAngularMomentum y, SpinAngularMomentum z, UnitOfSpinAngularMomentum unitOfSpinAngularMomentum) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfSpinAngularMomentum) { }
    #Document:ConstructorScalarTupleUnit(SpinAngularMomentum3, 3, UnitOfSpinAngularMomentum, unitOfSpinAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public SpinAngularMomentum3((Scalar x, Scalar y, Scalar z) components, UnitOfSpinAngularMomentum unitOfSpinAngularMomentum) : 
    	this(components.x, components.y, components.z, unitOfSpinAngularMomentum) { }
    #Document:ConstructorScalarsUnit(SpinAngularMomentum3, 3, UnitOfSpinAngularMomentum, unitOfSpinAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public SpinAngularMomentum3(Scalar x, Scalar y, Scalar z, UnitOfSpinAngularMomentum unitOfSpinAngularMomentum) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfSpinAngularMomentum) { }
    #Document:ConstructorVectorUnit(SpinAngularMomentum3, 3, UnitOfSpinAngularMomentum, unitOfSpinAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public SpinAngularMomentum3(Vector3 components, UnitOfSpinAngularMomentum unitOfSpinAngularMomentum) : 
    	this(components.X, components.Y, components.Z, unitOfSpinAngularMomentum) { }
    #Document:ConstructorDoubleTupleUnit(SpinAngularMomentum3, 3, UnitOfSpinAngularMomentum, unitOfSpinAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public SpinAngularMomentum3((double x, double y, double z) components, UnitOfSpinAngularMomentum unitOfSpinAngularMomentum) : 
    	this(components.x, components.y, components.z, unitOfSpinAngularMomentum) { }
    #Document:ConstructorDoublesTupleUnit(SpinAngularMomentum3, 3, UnitOfSpinAngularMomentum, unitOfSpinAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public SpinAngularMomentum3(double x, double y, double z, UnitOfSpinAngularMomentum unitOfSpinAngularMomentum) : 
    	this(x * unitOfSpinAngularMomentum.Factor, y * unitOfSpinAngularMomentum.Factor, z * unitOfSpinAngularMomentum.Factor) { }

    #Document:ConstructorComponentTuple(SpinAngularMomentum3, 3, UnitOfSpinAngularMomentum, unitOfSpinAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public SpinAngularMomentum3((SpinAngularMomentum x, SpinAngularMomentum y, SpinAngularMomentum z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(SpinAngularMomentum3, 3, UnitOfSpinAngularMomentum, unitOfSpinAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public SpinAngularMomentum3(SpinAngularMomentum x, SpinAngularMomentum y, SpinAngularMomentum z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(SpinAngularMomentum3, 3, UnitOfSpinAngularMomentum, unitOfSpinAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public SpinAngularMomentum3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(SpinAngularMomentum3, 3, UnitOfSpinAngularMomentum, unitOfSpinAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public SpinAngularMomentum3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(SpinAngularMomentum3, 3, UnitOfSpinAngularMomentum, unitOfSpinAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public SpinAngularMomentum3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(SpinAngularMomentum3, 3, UnitOfSpinAngularMomentum, unitOfSpinAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public SpinAngularMomentum3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(SpinAngularMomentum3, 3, UnitOfSpinAngularMomentum, unitOfSpinAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public SpinAngularMomentum3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

));

    #Document:InUnit(quantity = SpinAngularMomentum3, dimensionality = 3, unit = UnitOfSpinAngularMomentum, unitName = KilogramMetreSquaredPerSecond)#
    public Vector3 KilogramMetresSquaredPerSecond => InUnit(UnitOfSpinAngularMomentum.KilogramMetreSquaredPerSecond);

    #Document:ScalarMagnitude(SpinAngularMomentum3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(SpinAngularMomentum3, 3, SpinAngularMomentum)#
    public SpinAngularMomentum Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(SpinAngularMomentum3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(SpinAngularMomentum3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(SpinAngularMomentum3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(SpinAngularMomentum3, 3)#
    public SpinAngularMomentum3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(SpinAngularMomentum3, 3)#
    public SpinAngularMomentum3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(SpinAngularMomentum3, 3)#
    public SpinAngularMomentum3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(SpinAngularMomentum3, 3)#
    public SpinAngularMomentum Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(SpinAngularMomentum3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(SpinAngularMomentum3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(SpinAngularMomentum3, 3)#
    public SpinAngularMomentum3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(SpinAngularMomentum3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(SpinAngularMomentum3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(SpinAngularMomentum3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [kg * m^2 / s]";

    #Document:InUnitInstance(SpinAngularMomentum3, 3, UnitOfSpinAngularMomentum, unitOfSpinAngularMomentum)#
    public Vector3 InUnit(UnitOfSpinAngularMomentum unitOfSpinAngularMomentum) => InUnit(this, unitOfSpinAngularMomentum);
    #Document:InUnitStatic(SpinAngularMomentum3, spinAngularMomentum3, 3, UnitOfSpinAngularMomentum, unitOfSpinAngularMomentum)#
    private static Vector3 InUnit(SpinAngularMomentum3 spinAngularMomentum3, UnitOfSpinAngularMomentum unitOfSpinAngularMomentum) => 
    	spinAngularMomentum3.ToVector3() / unitOfSpinAngularMomentum.Factor;
    
    #Document:PlusMethod(SpinAngularMomentum3, 3)#
    public SpinAngularMomentum3 Plus() => this;
    #Document:NegateMethod(SpinAngularMomentum3, 3)#
    public SpinAngularMomentum3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(SpinAngularMomentum3, 3)#
    public static SpinAngularMomentum3 operator +(SpinAngularMomentum3 a) => a;
    #Document:NegateOperator(SpinAngularMomentum3, 3)#
    public static SpinAngularMomentum3 operator -(SpinAngularMomentum3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(SpinAngularMomentum3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(SpinAngularMomentum3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(SpinAngularMomentum3, 3)#
    public static Unhandled3 operator *(SpinAngularMomentum3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(SpinAngularMomentum3, 3)#
    public static Unhandled3 operator *(Unhandled a, SpinAngularMomentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(SpinAngularMomentum3, 3)#
    public static Unhandled3 operator /(SpinAngularMomentum3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(SpinAngularMomentum3, 3)#
    public SpinAngularMomentum3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(SpinAngularMomentum3, 3)#
    public SpinAngularMomentum3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(SpinAngularMomentum3, 3)#
    public SpinAngularMomentum3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(SpinAngularMomentum3, 3)#
    public static SpinAngularMomentum3 operator %(SpinAngularMomentum3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(SpinAngularMomentum3, 3)#
    public static SpinAngularMomentum3 operator *(SpinAngularMomentum3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(SpinAngularMomentum3, 3)#
    public static SpinAngularMomentum3 operator *(double a, SpinAngularMomentum3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(SpinAngularMomentum3, 3)#
    public static SpinAngularMomentum3 operator /(SpinAngularMomentum3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(SpinAngularMomentum3, 3)#
    public SpinAngularMomentum3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(SpinAngularMomentum3, 3)#
    public SpinAngularMomentum3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(SpinAngularMomentum3, 3)#
    public SpinAngularMomentum3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(SpinAngularMomentum3, 3)#
    public static SpinAngularMomentum3 operator %(SpinAngularMomentum3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(SpinAngularMomentum3, 3)#
    public static SpinAngularMomentum3 operator *(SpinAngularMomentum3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(SpinAngularMomentum3, 3)#
    public static SpinAngularMomentum3 operator *(Scalar a, SpinAngularMomentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(SpinAngularMomentum3, 3)#
    public static SpinAngularMomentum3 operator /(SpinAngularMomentum3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(SpinAngularMomentum3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(SpinAngularMomentum3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(SpinAngularMomentum3, 3)#
    public static Unhandled3 operator *(SpinAngularMomentum3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(SpinAngularMomentum3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, SpinAngularMomentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(SpinAngularMomentum3, 3)#
    public static Unhandled3 operator /(SpinAngularMomentum3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(SpinAngularMomentum3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(SpinAngularMomentum3, 3)#
    public static implicit operator (double x, double y, double z)(SpinAngularMomentum3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(SpinAngularMomentum3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(SpinAngularMomentum3, 3)#
    public static explicit operator Vector3(SpinAngularMomentum3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(SpinAngularMomentum3, 3)#
    public static SpinAngularMomentum3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(SpinAngularMomentum3, 3)#
    public static explicit operator SpinAngularMomentum3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(SpinAngularMomentum3, 3)#
    public static SpinAngularMomentum3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(SpinAngularMomentum3, 3)#
    public static explicit operator SpinAngularMomentum3(Vector3 a) => new(a);
}
