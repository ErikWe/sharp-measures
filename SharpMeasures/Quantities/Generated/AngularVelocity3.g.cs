namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(AngularVelocity3, 3, AngularSpeed, UnitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond], [RadiansPerSecond, DegreesPerSecond, TurnsPerSecond])#
public readonly partial record struct AngularVelocity3 :
    IVector3Quantity,
    IScalableVector3Quantity<AngularVelocity3>,
    INormalizableVector3Quantity<AngularVelocity3>,
    ITransformableVector3Quantity<AngularVelocity3>,
    IMultiplicableVector3Quantity<AngularVelocity3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<AngularVelocity3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<AngularVelocity3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(AngularVelocity3, 3)#
    public static AngularVelocity3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(AngularVelocity3, 3)#
    public double X { get; init; }
    #Document:ComponentY(AngularVelocity3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(AngularVelocity3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(AngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public AngularVelocity3((AngularSpeed x, AngularSpeed y, AngularSpeed z) components, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(components.x, components.y, components.z, unitOfAngularVelocity) { }
    #Document:ConstructorComponentsUnit(AngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public AngularVelocity3(AngularSpeed x, AngularSpeed y, AngularSpeed z, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAngularVelocity) { }
    #Document:ConstructorScalarTupleUnit(AngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public AngularVelocity3((Scalar x, Scalar y, Scalar z) components, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(components.x, components.y, components.z, unitOfAngularVelocity) { }
    #Document:ConstructorScalarsUnit(AngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public AngularVelocity3(Scalar x, Scalar y, Scalar z, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAngularVelocity) { }
    #Document:ConstructorVectorUnit(AngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public AngularVelocity3(Vector3 components, UnitOfAngularVelocity unitOfAngularVelocity) : this(components.X, components.Y, components.Z, unitOfAngularVelocity) { }
    #Document:ConstructorDoubleTupleUnit(AngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public AngularVelocity3((double x, double y, double z) components, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(components.x, components.y, components.z, unitOfAngularVelocity) { }
    #Document:ConstructorDoublesTupleUnit(AngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public AngularVelocity3(double x, double y, double z, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(x * unitOfAngularVelocity.Factor, y * unitOfAngularVelocity.Factor, z * unitOfAngularVelocity.Factor) { }

    #Document:ConstructorComponentTuple(AngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public AngularVelocity3((AngularSpeed x, AngularSpeed y, AngularSpeed z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(AngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public AngularVelocity3(AngularSpeed x, AngularSpeed y, AngularSpeed z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(AngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public AngularVelocity3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(AngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public AngularVelocity3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(AngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public AngularVelocity3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(AngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public AngularVelocity3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(AngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public AngularVelocity3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

));

    #Document:InUnit(quantity = AngularVelocity3, dimensionality = 3, unit = UnitOfAngularVelocity, unitName = RadianPerSecond)#
    public Vector3 RadiansPerSecond => InUnit(UnitOfAngularVelocity.RadianPerSecond);
    #Document:InUnit(quantity = AngularVelocity3, dimensionality = 3, unit = UnitOfAngularVelocity, unitName = DegreePerSecond)#
    public Vector3 DegreesPerSecond => InUnit(UnitOfAngularVelocity.DegreePerSecond);
    #Document:InUnit(quantity = AngularVelocity3, dimensionality = 3, unit = UnitOfAngularVelocity, unitName = TurnPerSecond)#
    public Vector3 TurnsPerSecond => InUnit(UnitOfAngularVelocity.TurnPerSecond);

    #Document:ScalarMagnitude(AngularVelocity3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(AngularVelocity3, 3, AngularSpeed)#
    public AngularSpeed Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(AngularVelocity3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(AngularVelocity3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(AngularVelocity3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(AngularVelocity3, 3)#
    public AngularVelocity3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(AngularVelocity3, 3)#
    public AngularVelocity3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(AngularVelocity3, 3)#
    public AngularVelocity3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(AngularVelocity3, 3)#
    public AngularSpeed Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(AngularVelocity3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(AngularVelocity3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(AngularVelocity3, 3)#
    public AngularVelocity3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(AngularVelocity3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(AngularVelocity3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(AngularVelocity3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [rad / s]";

    #Document:InUnitInstance(AngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity)#
    public Vector3 InUnit(UnitOfAngularVelocity unitOfAngularVelocity) => InUnit(this, unitOfAngularVelocity);
    #Document:InUnitStatic(AngularVelocity3, angularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity)#
    private static Vector3 InUnit(AngularVelocity3 angularVelocity3, UnitOfAngularVelocity unitOfAngularVelocity) => 
    	angularVelocity3.ToVector3() / unitOfAngularVelocity.Factor;
    
    #Document:PlusMethod(AngularVelocity3, 3)#
    public AngularVelocity3 Plus() => this;
    #Document:NegateMethod(AngularVelocity3, 3)#
    public AngularVelocity3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(AngularVelocity3, 3)#
    public static AngularVelocity3 operator +(AngularVelocity3 a) => a;
    #Document:NegateOperator(AngularVelocity3, 3)#
    public static AngularVelocity3 operator -(AngularVelocity3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(AngularVelocity3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(AngularVelocity3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(AngularVelocity3, 3)#
    public static Unhandled3 operator *(AngularVelocity3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(AngularVelocity3, 3)#
    public static Unhandled3 operator *(Unhandled a, AngularVelocity3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(AngularVelocity3, 3)#
    public static Unhandled3 operator /(AngularVelocity3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(AngularVelocity3, 3)#
    public AngularVelocity3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(AngularVelocity3, 3)#
    public AngularVelocity3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(AngularVelocity3, 3)#
    public AngularVelocity3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(AngularVelocity3, 3)#
    public static AngularVelocity3 operator %(AngularVelocity3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(AngularVelocity3, 3)#
    public static AngularVelocity3 operator *(AngularVelocity3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(AngularVelocity3, 3)#
    public static AngularVelocity3 operator *(double a, AngularVelocity3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(AngularVelocity3, 3)#
    public static AngularVelocity3 operator /(AngularVelocity3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(AngularVelocity3, 3)#
    public AngularVelocity3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(AngularVelocity3, 3)#
    public AngularVelocity3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(AngularVelocity3, 3)#
    public AngularVelocity3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(AngularVelocity3, 3)#
    public static AngularVelocity3 operator %(AngularVelocity3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(AngularVelocity3, 3)#
    public static AngularVelocity3 operator *(AngularVelocity3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(AngularVelocity3, 3)#
    public static AngularVelocity3 operator *(Scalar a, AngularVelocity3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(AngularVelocity3, 3)#
    public static AngularVelocity3 operator /(AngularVelocity3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(AngularVelocity3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(AngularVelocity3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(AngularVelocity3, 3)#
    public static Unhandled3 operator *(AngularVelocity3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(AngularVelocity3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, AngularVelocity3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(AngularVelocity3, 3)#
    public static Unhandled3 operator /(AngularVelocity3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(AngularVelocity3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(AngularVelocity3, 3)#
    public static implicit operator (double x, double y, double z)(AngularVelocity3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(AngularVelocity3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(AngularVelocity3, 3)#
    public static explicit operator Vector3(AngularVelocity3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(AngularVelocity3, 3)#
    public static AngularVelocity3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(AngularVelocity3, 3)#
    public static explicit operator AngularVelocity3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(AngularVelocity3, 3)#
    public static AngularVelocity3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(AngularVelocity3, 3)#
    public static explicit operator AngularVelocity3(Vector3 a) => new(a);
}
