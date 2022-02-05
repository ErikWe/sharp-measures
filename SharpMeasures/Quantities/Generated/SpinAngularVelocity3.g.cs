namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(SpinAngularVelocity3, 3, SpinAngularSpeed, UnitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond], [RadiansPerSecond, DegreesPerSecond, TurnsPerSecond])#
public readonly partial record struct SpinAngularVelocity3 :
    IVector3Quantity,
    IScalableVector3Quantity<SpinAngularVelocity3>,
    INormalizableVector3Quantity<SpinAngularVelocity3>,
    ITransformableVector3Quantity<SpinAngularVelocity3>,
    IMultiplicableVector3Quantity<SpinAngularVelocity3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<SpinAngularVelocity3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<SpinAngularVelocity3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(SpinAngularVelocity3, 3)#
    public static SpinAngularVelocity3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(SpinAngularVelocity3, 3)#
    public double X { get; init; }
    #Document:ComponentY(SpinAngularVelocity3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(SpinAngularVelocity3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(SpinAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public SpinAngularVelocity3((SpinAngularSpeed x, SpinAngularSpeed y, SpinAngularSpeed z) components, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(components.x, components.y, components.z, unitOfAngularVelocity) { }
    #Document:ConstructorComponentsUnit(SpinAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public SpinAngularVelocity3(SpinAngularSpeed x, SpinAngularSpeed y, SpinAngularSpeed z, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAngularVelocity) { }
    #Document:ConstructorScalarTupleUnit(SpinAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public SpinAngularVelocity3((Scalar x, Scalar y, Scalar z) components, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(components.x, components.y, components.z, unitOfAngularVelocity) { }
    #Document:ConstructorScalarsUnit(SpinAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public SpinAngularVelocity3(Scalar x, Scalar y, Scalar z, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAngularVelocity) { }
    #Document:ConstructorVectorUnit(SpinAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public SpinAngularVelocity3(Vector3 components, UnitOfAngularVelocity unitOfAngularVelocity) : this(components.X, components.Y, components.Z, unitOfAngularVelocity) { }
    #Document:ConstructorDoubleTupleUnit(SpinAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public SpinAngularVelocity3((double x, double y, double z) components, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(components.x, components.y, components.z, unitOfAngularVelocity) { }
    #Document:ConstructorDoublesTupleUnit(SpinAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public SpinAngularVelocity3(double x, double y, double z, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(x * unitOfAngularVelocity.Factor, y * unitOfAngularVelocity.Factor, z * unitOfAngularVelocity.Factor) { }

    #Document:ConstructorComponentTuple(SpinAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public SpinAngularVelocity3((SpinAngularSpeed x, SpinAngularSpeed y, SpinAngularSpeed z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(SpinAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public SpinAngularVelocity3(SpinAngularSpeed x, SpinAngularSpeed y, SpinAngularSpeed z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(SpinAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public SpinAngularVelocity3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(SpinAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public SpinAngularVelocity3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(SpinAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public SpinAngularVelocity3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(SpinAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public SpinAngularVelocity3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(SpinAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity, [RadianPerSecond, DegreePerSecond, TurnPerSecond])#
    public SpinAngularVelocity3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

));

    #Document:InUnit(quantity = SpinAngularVelocity3, dimensionality = 3, unit = UnitOfAngularVelocity, unitName = RadianPerSecond)#
    public Vector3 RadiansPerSecond => InUnit(UnitOfAngularVelocity.RadianPerSecond);
    #Document:InUnit(quantity = SpinAngularVelocity3, dimensionality = 3, unit = UnitOfAngularVelocity, unitName = DegreePerSecond)#
    public Vector3 DegreesPerSecond => InUnit(UnitOfAngularVelocity.DegreePerSecond);
    #Document:InUnit(quantity = SpinAngularVelocity3, dimensionality = 3, unit = UnitOfAngularVelocity, unitName = TurnPerSecond)#
    public Vector3 TurnsPerSecond => InUnit(UnitOfAngularVelocity.TurnPerSecond);

    #Document:ScalarMagnitude(SpinAngularVelocity3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(SpinAngularVelocity3, 3, SpinAngularSpeed)#
    public SpinAngularSpeed Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(SpinAngularVelocity3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(SpinAngularVelocity3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(SpinAngularVelocity3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(SpinAngularVelocity3, 3)#
    public SpinAngularVelocity3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(SpinAngularVelocity3, 3)#
    public SpinAngularVelocity3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(SpinAngularVelocity3, 3)#
    public SpinAngularVelocity3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(SpinAngularVelocity3, 3)#
    public SpinAngularSpeed Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(SpinAngularVelocity3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(SpinAngularVelocity3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(SpinAngularVelocity3, 3)#
    public SpinAngularVelocity3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(SpinAngularVelocity3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(SpinAngularVelocity3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(SpinAngularVelocity3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [rad / s]";

    #Document:InUnitInstance(SpinAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity)#
    public Vector3 InUnit(UnitOfAngularVelocity unitOfAngularVelocity) => InUnit(this, unitOfAngularVelocity);
    #Document:InUnitStatic(SpinAngularVelocity3, spinAngularVelocity3, 3, UnitOfAngularVelocity, unitOfAngularVelocity)#
    private static Vector3 InUnit(SpinAngularVelocity3 spinAngularVelocity3, UnitOfAngularVelocity unitOfAngularVelocity) => 
    	spinAngularVelocity3.ToVector3() / unitOfAngularVelocity.Factor;
    
    #Document:PlusMethod(SpinAngularVelocity3, 3)#
    public SpinAngularVelocity3 Plus() => this;
    #Document:NegateMethod(SpinAngularVelocity3, 3)#
    public SpinAngularVelocity3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(SpinAngularVelocity3, 3)#
    public static SpinAngularVelocity3 operator +(SpinAngularVelocity3 a) => a;
    #Document:NegateOperator(SpinAngularVelocity3, 3)#
    public static SpinAngularVelocity3 operator -(SpinAngularVelocity3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(SpinAngularVelocity3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(SpinAngularVelocity3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(SpinAngularVelocity3, 3)#
    public static Unhandled3 operator *(SpinAngularVelocity3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(SpinAngularVelocity3, 3)#
    public static Unhandled3 operator *(Unhandled a, SpinAngularVelocity3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(SpinAngularVelocity3, 3)#
    public static Unhandled3 operator /(SpinAngularVelocity3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(SpinAngularVelocity3, 3)#
    public SpinAngularVelocity3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(SpinAngularVelocity3, 3)#
    public SpinAngularVelocity3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(SpinAngularVelocity3, 3)#
    public SpinAngularVelocity3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(SpinAngularVelocity3, 3)#
    public static SpinAngularVelocity3 operator %(SpinAngularVelocity3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(SpinAngularVelocity3, 3)#
    public static SpinAngularVelocity3 operator *(SpinAngularVelocity3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(SpinAngularVelocity3, 3)#
    public static SpinAngularVelocity3 operator *(double a, SpinAngularVelocity3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(SpinAngularVelocity3, 3)#
    public static SpinAngularVelocity3 operator /(SpinAngularVelocity3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(SpinAngularVelocity3, 3)#
    public SpinAngularVelocity3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(SpinAngularVelocity3, 3)#
    public SpinAngularVelocity3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(SpinAngularVelocity3, 3)#
    public SpinAngularVelocity3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(SpinAngularVelocity3, 3)#
    public static SpinAngularVelocity3 operator %(SpinAngularVelocity3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(SpinAngularVelocity3, 3)#
    public static SpinAngularVelocity3 operator *(SpinAngularVelocity3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(SpinAngularVelocity3, 3)#
    public static SpinAngularVelocity3 operator *(Scalar a, SpinAngularVelocity3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(SpinAngularVelocity3, 3)#
    public static SpinAngularVelocity3 operator /(SpinAngularVelocity3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(SpinAngularVelocity3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(SpinAngularVelocity3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(SpinAngularVelocity3, 3)#
    public static Unhandled3 operator *(SpinAngularVelocity3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(SpinAngularVelocity3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, SpinAngularVelocity3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(SpinAngularVelocity3, 3)#
    public static Unhandled3 operator /(SpinAngularVelocity3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(SpinAngularVelocity3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(SpinAngularVelocity3, 3)#
    public static implicit operator (double x, double y, double z)(SpinAngularVelocity3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(SpinAngularVelocity3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(SpinAngularVelocity3, 3)#
    public static explicit operator Vector3(SpinAngularVelocity3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(SpinAngularVelocity3, 3)#
    public static SpinAngularVelocity3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(SpinAngularVelocity3, 3)#
    public static explicit operator SpinAngularVelocity3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(SpinAngularVelocity3, 3)#
    public static SpinAngularVelocity3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(SpinAngularVelocity3, 3)#
    public static explicit operator SpinAngularVelocity3(Vector3 a) => new(a);
}
