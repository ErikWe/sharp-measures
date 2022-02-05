namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(OrbitalAngularVelocity3, 3, OrbitalAngularSpeed, UnitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond], [RadiansPerSecond, DegreesPerSecond, TurnsPerSecond])#
public readonly partial record struct OrbitalAngularVelocity3 :
    IVector3Quantity,
    IScalableVector3Quantity<OrbitalAngularVelocity3>,
    INormalizableVector3Quantity<OrbitalAngularVelocity3>,
    ITransformableVector3Quantity<OrbitalAngularVelocity3>,
    IMultiplicableVector3Quantity<OrbitalAngularVelocity3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<OrbitalAngularVelocity3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<OrbitalAngularVelocity3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(OrbitalAngularVelocity3, 3)#
    public static OrbitalAngularVelocity3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(OrbitalAngularVelocity3, 3)#
    public double X { get; init; }
    #Document:ComponentY(OrbitalAngularVelocity3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(OrbitalAngularVelocity3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(OrbitalAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public OrbitalAngularVelocity3((OrbitalAngularSpeed x, OrbitalAngularSpeed y, OrbitalAngularSpeed z) components, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(components.x, components.y, components.z, unitOfAngularVelocity) { }
    #Document:ConstructorComponentsUnit(OrbitalAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public OrbitalAngularVelocity3(OrbitalAngularSpeed x, OrbitalAngularSpeed y, OrbitalAngularSpeed z, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAngularVelocity) { }
    #Document:ConstructorScalarTupleUnit(OrbitalAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public OrbitalAngularVelocity3((Scalar x, Scalar y, Scalar z) components, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(components.x, components.y, components.z, unitOfAngularVelocity) { }
    #Document:ConstructorScalarsUnit(OrbitalAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public OrbitalAngularVelocity3(Scalar x, Scalar y, Scalar z, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAngularVelocity) { }
    #Document:ConstructorVectorUnit(OrbitalAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public OrbitalAngularVelocity3(Vector3 components, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(components.X, components.Y, components.Z, unitOfAngularVelocity) { }
    #Document:ConstructorDoubleTupleUnit(OrbitalAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public OrbitalAngularVelocity3((double x, double y, double z) components, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(components.x, components.y, components.z, unitOfAngularVelocity) { }
    #Document:ConstructorDoublesTupleUnit(OrbitalAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public OrbitalAngularVelocity3(double x, double y, double z, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(x * unitOfAngularVelocity.Factor, y * unitOfAngularVelocity.Factor, z * unitOfAngularVelocity.Factor) { }

    #Document:ConstructorComponentTuple(OrbitalAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public OrbitalAngularVelocity3((OrbitalAngularSpeed x, OrbitalAngularSpeed y, OrbitalAngularSpeed z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(OrbitalAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public OrbitalAngularVelocity3(OrbitalAngularSpeed x, OrbitalAngularSpeed y, OrbitalAngularSpeed z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(OrbitalAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public OrbitalAngularVelocity3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(OrbitalAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public OrbitalAngularVelocity3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(OrbitalAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public OrbitalAngularVelocity3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(OrbitalAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public OrbitalAngularVelocity3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(OrbitalAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public OrbitalAngularVelocity3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

));

    #Document:InUnit(quantity = OrbitalAngularVelocity3, dimensionality = 3, unit = UnitOfAngularVelocity, unitName = RadianPerSecond)#
    public Vector3 RadiansPerSecond => InUnit(UnitOfAngularVelocity.RadianPerSecond);
    #Document:InUnit(quantity = OrbitalAngularVelocity3, dimensionality = 3, unit = UnitOfAngularVelocity, unitName = DegreePerSecond)#
    public Vector3 DegreesPerSecond => InUnit(UnitOfAngularVelocity.DegreePerSecond);
    #Document:InUnit(quantity = OrbitalAngularVelocity3, dimensionality = 3, unit = UnitOfAngularVelocity, unitName = TurnPerSecond)#
    public Vector3 TurnsPerSecond => InUnit(UnitOfAngularVelocity.TurnPerSecond);

    #Document:ScalarMagnitude(OrbitalAngularVelocity3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(OrbitalAngularVelocity3, 3, OrbitalAngularSpeed)#
    public OrbitalAngularSpeed Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(OrbitalAngularVelocity3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(OrbitalAngularVelocity3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(OrbitalAngularVelocity3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(OrbitalAngularVelocity3, 3)#
    public OrbitalAngularVelocity3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(OrbitalAngularVelocity3, 3)#
    public OrbitalAngularVelocity3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(OrbitalAngularVelocity3, 3)#
    public OrbitalAngularVelocity3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(OrbitalAngularVelocity3, 3)#
    public OrbitalAngularSpeed Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(OrbitalAngularVelocity3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(OrbitalAngularVelocity3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(OrbitalAngularVelocity3, 3)#
    public OrbitalAngularVelocity3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(OrbitalAngularVelocity3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(OrbitalAngularVelocity3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(OrbitalAngularVelocity3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [rad / s]";

    #Document:InUnitInstance(OrbitalAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity)#
    public Vector3 InUnit(UnitOfAngularVelocity unitOfAngularVelocity) => InUnit(this, unitOfAngularVelocity);
    #Document:InUnitStatic(OrbitalAngularVelocity3, orbitalAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity)#
    private static Vector3 InUnit(OrbitalAngularVelocity3 orbitalAngularVelocity3, UnitOfAngularVelocity unitOfAngularVelocity) => 
    	orbitalAngularVelocity3.ToVector3() / unitOfAngularVelocity.Factor;
    
    #Document:PlusMethod(OrbitalAngularVelocity3, 3)#
    public OrbitalAngularVelocity3 Plus() => this;
    #Document:NegateMethod(OrbitalAngularVelocity3, 3)#
    public OrbitalAngularVelocity3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(OrbitalAngularVelocity3, 3)#
    public static OrbitalAngularVelocity3 operator +(OrbitalAngularVelocity3 a) => a;
    #Document:NegateOperator(OrbitalAngularVelocity3, 3)#
    public static OrbitalAngularVelocity3 operator -(OrbitalAngularVelocity3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(OrbitalAngularVelocity3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(OrbitalAngularVelocity3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(OrbitalAngularVelocity3, 3)#
    public static Unhandled3 operator *(OrbitalAngularVelocity3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(OrbitalAngularVelocity3, 3)#
    public static Unhandled3 operator *(Unhandled a, OrbitalAngularVelocity3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(OrbitalAngularVelocity3, 3)#
    public static Unhandled3 operator /(OrbitalAngularVelocity3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(OrbitalAngularVelocity3, 3)#
    public OrbitalAngularVelocity3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(OrbitalAngularVelocity3, 3)#
    public OrbitalAngularVelocity3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(OrbitalAngularVelocity3, 3)#
    public OrbitalAngularVelocity3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(OrbitalAngularVelocity3, 3)#
    public static OrbitalAngularVelocity3 operator %(OrbitalAngularVelocity3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(OrbitalAngularVelocity3, 3)#
    public static OrbitalAngularVelocity3 operator *(OrbitalAngularVelocity3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(OrbitalAngularVelocity3, 3)#
    public static OrbitalAngularVelocity3 operator *(double a, OrbitalAngularVelocity3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(OrbitalAngularVelocity3, 3)#
    public static OrbitalAngularVelocity3 operator /(OrbitalAngularVelocity3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(OrbitalAngularVelocity3, 3)#
    public OrbitalAngularVelocity3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(OrbitalAngularVelocity3, 3)#
    public OrbitalAngularVelocity3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(OrbitalAngularVelocity3, 3)#
    public OrbitalAngularVelocity3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(OrbitalAngularVelocity3, 3)#
    public static OrbitalAngularVelocity3 operator %(OrbitalAngularVelocity3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(OrbitalAngularVelocity3, 3)#
    public static OrbitalAngularVelocity3 operator *(OrbitalAngularVelocity3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(OrbitalAngularVelocity3, 3)#
    public static OrbitalAngularVelocity3 operator *(Scalar a, OrbitalAngularVelocity3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(OrbitalAngularVelocity3, 3)#
    public static OrbitalAngularVelocity3 operator /(OrbitalAngularVelocity3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(OrbitalAngularVelocity3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(OrbitalAngularVelocity3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(OrbitalAngularVelocity3, 3)#
    public static Unhandled3 operator *(OrbitalAngularVelocity3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(OrbitalAngularVelocity3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, OrbitalAngularVelocity3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(OrbitalAngularVelocity3, 3)#
    public static Unhandled3 operator /(OrbitalAngularVelocity3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(OrbitalAngularVelocity3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(OrbitalAngularVelocity3, 3)#
    public static implicit operator (double x, double y, double z)(OrbitalAngularVelocity3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(OrbitalAngularVelocity3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(OrbitalAngularVelocity3, 3)#
    public static explicit operator Vector3(OrbitalAngularVelocity3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(OrbitalAngularVelocity3, 3)#
    public static OrbitalAngularVelocity3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(OrbitalAngularVelocity3, 3)#
    public static explicit operator OrbitalAngularVelocity3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(OrbitalAngularVelocity3, 3)#
    public static OrbitalAngularVelocity3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(OrbitalAngularVelocity3, 3)#
    public static explicit operator OrbitalAngularVelocity3(Vector3 a) => new(a);
}
