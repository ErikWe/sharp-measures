namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(OrbitalAngularMomentum3, 3, OrbitalAngularMomentum, UnitOfOrbitalAngularMomentum, [KilogramMetreSquaredPerSecond], [KilogramMetresSquaredPerSecond])#
public readonly partial record struct OrbitalAngularMomentum3 :
    IVector3Quantity,
    IScalableVector3Quantity<OrbitalAngularMomentum3>,
    INormalizableVector3Quantity<OrbitalAngularMomentum3>,
    ITransformableVector3Quantity<OrbitalAngularMomentum3>,
    IMultiplicableVector3Quantity<OrbitalAngularMomentum3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<OrbitalAngularMomentum3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<OrbitalAngularMomentum3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(OrbitalAngularMomentum3, 3)#
    public static OrbitalAngularMomentum3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(OrbitalAngularMomentum3, 3)#
    public double X { get; init; }
    #Document:ComponentY(OrbitalAngularMomentum3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(OrbitalAngularMomentum3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(OrbitalAngularMomentum3, 3, UnitOfOrbitalAngularMomentum, unitOfOrbitalAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public OrbitalAngularMomentum3((OrbitalAngularMomentum x, OrbitalAngularMomentum y, OrbitalAngularMomentum z) components, UnitOfOrbitalAngularMomentum unitOfOrbitalAngularMomentum) : 
    	this(components.x, components.y, components.z, unitOfOrbitalAngularMomentum) { }
    #Document:ConstructorComponentsUnit(OrbitalAngularMomentum3, 3, UnitOfOrbitalAngularMomentum, unitOfOrbitalAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public OrbitalAngularMomentum3(OrbitalAngularMomentum x, OrbitalAngularMomentum y, OrbitalAngularMomentum z, UnitOfOrbitalAngularMomentum unitOfOrbitalAngularMomentum) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfOrbitalAngularMomentum) { }
    #Document:ConstructorScalarTupleUnit(OrbitalAngularMomentum3, 3, UnitOfOrbitalAngularMomentum, unitOfOrbitalAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public OrbitalAngularMomentum3((Scalar x, Scalar y, Scalar z) components, UnitOfOrbitalAngularMomentum unitOfOrbitalAngularMomentum) : 
    	this(components.x, components.y, components.z, unitOfOrbitalAngularMomentum) { }
    #Document:ConstructorScalarsUnit(OrbitalAngularMomentum3, 3, UnitOfOrbitalAngularMomentum, unitOfOrbitalAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public OrbitalAngularMomentum3(Scalar x, Scalar y, Scalar z, UnitOfOrbitalAngularMomentum unitOfOrbitalAngularMomentum) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfOrbitalAngularMomentum) { }
    #Document:ConstructorVectorUnit(OrbitalAngularMomentum3, 3, UnitOfOrbitalAngularMomentum, unitOfOrbitalAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public OrbitalAngularMomentum3(Vector3 components, UnitOfOrbitalAngularMomentum unitOfOrbitalAngularMomentum) : 
    	this(components.X, components.Y, components.Z, unitOfOrbitalAngularMomentum) { }
    #Document:ConstructorDoubleTupleUnit(OrbitalAngularMomentum3, 3, UnitOfOrbitalAngularMomentum, unitOfOrbitalAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public OrbitalAngularMomentum3((double x, double y, double z) components, UnitOfOrbitalAngularMomentum unitOfOrbitalAngularMomentum) : 
    	this(components.x, components.y, components.z, unitOfOrbitalAngularMomentum) { }
    #Document:ConstructorDoublesTupleUnit(OrbitalAngularMomentum3, 3, UnitOfOrbitalAngularMomentum, unitOfOrbitalAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public OrbitalAngularMomentum3(double x, double y, double z, UnitOfOrbitalAngularMomentum unitOfOrbitalAngularMomentum) : 
    	this(x * unitOfOrbitalAngularMomentum.Factor, y * unitOfOrbitalAngularMomentum.Factor, z * unitOfOrbitalAngularMomentum.Factor) { }

    #Document:ConstructorComponentTuple(OrbitalAngularMomentum3, 3, UnitOfOrbitalAngularMomentum, unitOfOrbitalAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public OrbitalAngularMomentum3((OrbitalAngularMomentum x, OrbitalAngularMomentum y, OrbitalAngularMomentum z) components) : 
    	this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(OrbitalAngularMomentum3, 3, UnitOfOrbitalAngularMomentum, unitOfOrbitalAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public OrbitalAngularMomentum3(OrbitalAngularMomentum x, OrbitalAngularMomentum y, OrbitalAngularMomentum z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(OrbitalAngularMomentum3, 3, UnitOfOrbitalAngularMomentum, unitOfOrbitalAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public OrbitalAngularMomentum3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(OrbitalAngularMomentum3, 3, UnitOfOrbitalAngularMomentum, unitOfOrbitalAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public OrbitalAngularMomentum3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(OrbitalAngularMomentum3, 3, UnitOfOrbitalAngularMomentum, unitOfOrbitalAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public OrbitalAngularMomentum3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(OrbitalAngularMomentum3, 3, UnitOfOrbitalAngularMomentum, unitOfOrbitalAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public OrbitalAngularMomentum3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(OrbitalAngularMomentum3, 3, UnitOfOrbitalAngularMomentum, unitOfOrbitalAngularMomentum, [KilogramMetreSquaredPerSecond])#
    public OrbitalAngularMomentum3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

));

    #Document:InUnit(quantity = OrbitalAngularMomentum3, dimensionality = 3, unit = UnitOfOrbitalAngularMomentum, unitName = KilogramMetreSquaredPerSecond)#
    public Vector3 KilogramMetresSquaredPerSecond => InUnit(UnitOfOrbitalAngularMomentum.KilogramMetreSquaredPerSecond);

    #Document:ScalarMagnitude(OrbitalAngularMomentum3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(OrbitalAngularMomentum3, 3, OrbitalAngularMomentum)#
    public OrbitalAngularMomentum Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(OrbitalAngularMomentum3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(OrbitalAngularMomentum3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(OrbitalAngularMomentum3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(OrbitalAngularMomentum3, 3)#
    public OrbitalAngularMomentum3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(OrbitalAngularMomentum3, 3)#
    public OrbitalAngularMomentum3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(OrbitalAngularMomentum3, 3)#
    public OrbitalAngularMomentum3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(OrbitalAngularMomentum3, 3)#
    public OrbitalAngularMomentum Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(OrbitalAngularMomentum3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(OrbitalAngularMomentum3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(OrbitalAngularMomentum3, 3)#
    public OrbitalAngularMomentum3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(OrbitalAngularMomentum3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(OrbitalAngularMomentum3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(OrbitalAngularMomentum3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [kg * m^2 / s]";

    #Document:InUnitInstance(OrbitalAngularMomentum3, 3, UnitOfOrbitalAngularMomentum, unitOfOrbitalAngularMomentum)#
    public Vector3 InUnit(UnitOfOrbitalAngularMomentum unitOfOrbitalAngularMomentum) => InUnit(this, unitOfOrbitalAngularMomentum);
    #Document:InUnitStatic(OrbitalAngularMomentum3, orbitalAngularMomentum3, 3, UnitOfOrbitalAngularMomentum, unitOfOrbitalAngularMomentum)#
    private static Vector3 InUnit(OrbitalAngularMomentum3 orbitalAngularMomentum3, UnitOfOrbitalAngularMomentum unitOfOrbitalAngularMomentum) => 
    	orbitalAngularMomentum3.ToVector3() / unitOfOrbitalAngularMomentum.Factor;
    
    #Document:PlusMethod(OrbitalAngularMomentum3, 3)#
    public OrbitalAngularMomentum3 Plus() => this;
    #Document:NegateMethod(OrbitalAngularMomentum3, 3)#
    public OrbitalAngularMomentum3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(OrbitalAngularMomentum3, 3)#
    public static OrbitalAngularMomentum3 operator +(OrbitalAngularMomentum3 a) => a;
    #Document:NegateOperator(OrbitalAngularMomentum3, 3)#
    public static OrbitalAngularMomentum3 operator -(OrbitalAngularMomentum3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(OrbitalAngularMomentum3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(OrbitalAngularMomentum3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(OrbitalAngularMomentum3, 3)#
    public static Unhandled3 operator *(OrbitalAngularMomentum3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(OrbitalAngularMomentum3, 3)#
    public static Unhandled3 operator *(Unhandled a, OrbitalAngularMomentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(OrbitalAngularMomentum3, 3)#
    public static Unhandled3 operator /(OrbitalAngularMomentum3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(OrbitalAngularMomentum3, 3)#
    public OrbitalAngularMomentum3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(OrbitalAngularMomentum3, 3)#
    public OrbitalAngularMomentum3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(OrbitalAngularMomentum3, 3)#
    public OrbitalAngularMomentum3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(OrbitalAngularMomentum3, 3)#
    public static OrbitalAngularMomentum3 operator %(OrbitalAngularMomentum3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(OrbitalAngularMomentum3, 3)#
    public static OrbitalAngularMomentum3 operator *(OrbitalAngularMomentum3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(OrbitalAngularMomentum3, 3)#
    public static OrbitalAngularMomentum3 operator *(double a, OrbitalAngularMomentum3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(OrbitalAngularMomentum3, 3)#
    public static OrbitalAngularMomentum3 operator /(OrbitalAngularMomentum3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(OrbitalAngularMomentum3, 3)#
    public OrbitalAngularMomentum3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(OrbitalAngularMomentum3, 3)#
    public OrbitalAngularMomentum3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(OrbitalAngularMomentum3, 3)#
    public OrbitalAngularMomentum3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(OrbitalAngularMomentum3, 3)#
    public static OrbitalAngularMomentum3 operator %(OrbitalAngularMomentum3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(OrbitalAngularMomentum3, 3)#
    public static OrbitalAngularMomentum3 operator *(OrbitalAngularMomentum3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(OrbitalAngularMomentum3, 3)#
    public static OrbitalAngularMomentum3 operator *(Scalar a, OrbitalAngularMomentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(OrbitalAngularMomentum3, 3)#
    public static OrbitalAngularMomentum3 operator /(OrbitalAngularMomentum3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(OrbitalAngularMomentum3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(OrbitalAngularMomentum3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(OrbitalAngularMomentum3, 3)#
    public static Unhandled3 operator *(OrbitalAngularMomentum3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(OrbitalAngularMomentum3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, OrbitalAngularMomentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(OrbitalAngularMomentum3, 3)#
    public static Unhandled3 operator /(OrbitalAngularMomentum3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(OrbitalAngularMomentum3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(OrbitalAngularMomentum3, 3)#
    public static implicit operator (double x, double y, double z)(OrbitalAngularMomentum3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(OrbitalAngularMomentum3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(OrbitalAngularMomentum3, 3)#
    public static explicit operator Vector3(OrbitalAngularMomentum3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(OrbitalAngularMomentum3, 3)#
    public static OrbitalAngularMomentum3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(OrbitalAngularMomentum3, 3)#
    public static explicit operator OrbitalAngularMomentum3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(OrbitalAngularMomentum3, 3)#
    public static OrbitalAngularMomentum3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(OrbitalAngularMomentum3, 3)#
    public static explicit operator OrbitalAngularMomentum3(Vector3 a) => new(a);
}
