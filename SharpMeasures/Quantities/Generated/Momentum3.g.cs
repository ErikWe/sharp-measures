namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(Momentum3, 3, Momentum, UnitOfMomentum, [KilogramMetrePerSecond], [KilogramMetresPerSecond])#
public readonly partial record struct Momentum3 :
    IVector3Quantity,
    IScalableVector3Quantity<Momentum3>,
    INormalizableVector3Quantity<Momentum3>,
    ITransformableVector3Quantity<Momentum3>,
    IMultiplicableVector3Quantity<Momentum3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Momentum3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<Momentum3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(Momentum3, 3)#
    public static Momentum3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(Momentum3, 3)#
    public double X { get; init; }
    #Document:ComponentY(Momentum3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(Momentum3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(Momentum3, 3, UnitOfMomentum, unitOfMomentum, [KilogramMetrePerSecond])#
    public Momentum3((Momentum x, Momentum y, Momentum z) components, UnitOfMomentum unitOfMomentum) : this(components.x, components.y, components.z, unitOfMomentum) { }
    #Document:ConstructorComponentsUnit(Momentum3, 3, UnitOfMomentum, unitOfMomentum, [KilogramMetrePerSecond])#
    public Momentum3(Momentum x, Momentum y, Momentum z, UnitOfMomentum unitOfMomentum) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfMomentum) { }
    #Document:ConstructorScalarTupleUnit(Momentum3, 3, UnitOfMomentum, unitOfMomentum, [KilogramMetrePerSecond])#
    public Momentum3((Scalar x, Scalar y, Scalar z) components, UnitOfMomentum unitOfMomentum) : this(components.x, components.y, components.z, unitOfMomentum) { }
    #Document:ConstructorScalarsUnit(Momentum3, 3, UnitOfMomentum, unitOfMomentum, [KilogramMetrePerSecond])#
    public Momentum3(Scalar x, Scalar y, Scalar z, UnitOfMomentum unitOfMomentum) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfMomentum) { }
    #Document:ConstructorVectorUnit(Momentum3, 3, UnitOfMomentum, unitOfMomentum, [KilogramMetrePerSecond])#
    public Momentum3(Vector3 components, UnitOfMomentum unitOfMomentum) : this(components.X, components.Y, components.Z, unitOfMomentum) { }
    #Document:ConstructorDoubleTupleUnit(Momentum3, 3, UnitOfMomentum, unitOfMomentum, [KilogramMetrePerSecond])#
    public Momentum3((double x, double y, double z) components, UnitOfMomentum unitOfMomentum) : this(components.x, components.y, components.z, unitOfMomentum) { }
    #Document:ConstructorDoublesTupleUnit(Momentum3, 3, UnitOfMomentum, unitOfMomentum, [KilogramMetrePerSecond])#
    public Momentum3(double x, double y, double z, UnitOfMomentum unitOfMomentum) : this(x * unitOfMomentum.Factor, y * unitOfMomentum.Factor, z * unitOfMomentum.Factor) { }

    #Document:ConstructorComponentTuple(Momentum3, 3, UnitOfMomentum, unitOfMomentum, [KilogramMetrePerSecond])#
    public Momentum3((Momentum x, Momentum y, Momentum z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(Momentum3, 3, UnitOfMomentum, unitOfMomentum, [KilogramMetrePerSecond])#
    public Momentum3(Momentum x, Momentum y, Momentum z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(Momentum3, 3, UnitOfMomentum, unitOfMomentum, [KilogramMetrePerSecond])#
    public Momentum3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(Momentum3, 3, UnitOfMomentum, unitOfMomentum, [KilogramMetrePerSecond])#
    public Momentum3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(Momentum3, 3, UnitOfMomentum, unitOfMomentum, [KilogramMetrePerSecond])#
    public Momentum3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(Momentum3, 3, UnitOfMomentum, unitOfMomentum, [KilogramMetrePerSecond])#
    public Momentum3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(Momentum3, 3, UnitOfMomentum, unitOfMomentum, [KilogramMetrePerSecond])#
    public Momentum3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

);

    #Document:InUnit(quantity = Momentum3, dimensionality = 3, unit = UnitOfMomentum, unitName = KilogramMetrePerSecond)#
    public Vector3 KilogramMetresPerSecond => InUnit(UnitOfMomentum.KilogramMetrePerSecond);

    #Document:ScalarMagnitude(Momentum3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(Momentum3, 3, Momentum)#
    public Momentum Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(Momentum3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(Momentum3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(Momentum3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(Momentum3, 3)#
    public Momentum3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(Momentum3, 3)#
    public Momentum3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(Momentum3, 3)#
    public Momentum3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(Momentum3, 3)#
    public Momentum Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(Momentum3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(Momentum3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(Momentum3, 3)#
    public Momentum3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(Momentum3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(Momentum3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(Momentum3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [kg * m / s]";

    #Document:InUnitInstance(Momentum3, 3, UnitOfMomentum, unitOfMomentum)#
    public Vector3 InUnit(UnitOfMomentum unitOfMomentum) => InUnit(this, unitOfMomentum);
    #Document:InUnitStatic(Momentum3, momentum3, 3, UnitOfMomentum, unitOfMomentum)#
    private static Vector3 InUnit(Momentum3 momentum3, UnitOfMomentum unitOfMomentum) => momentum3.ToVector3() / unitOfMomentum.Factor;
    
    #Document:PlusMethod(Momentum3, 3)#
    public Momentum3 Plus() => this;
    #Document:NegateMethod(Momentum3, 3)#
    public Momentum3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(Momentum3, 3)#
    public static Momentum3 operator +(Momentum3 a) => a;
    #Document:NegateOperator(Momentum3, 3)#
    public static Momentum3 operator -(Momentum3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(Momentum3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(Momentum3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(Momentum3, 3)#
    public static Unhandled3 operator *(Momentum3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(Momentum3, 3)#
    public static Unhandled3 operator *(Unhandled a, Momentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(Momentum3, 3)#
    public static Unhandled3 operator /(Momentum3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(Momentum3, 3)#
    public Momentum3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(Momentum3, 3)#
    public Momentum3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(Momentum3, 3)#
    public Momentum3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(Momentum3, 3)#
    public static Momentum3 operator %(Momentum3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(Momentum3, 3)#
    public static Momentum3 operator *(Momentum3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(Momentum3, 3)#
    public static Momentum3 operator *(double a, Momentum3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(Momentum3, 3)#
    public static Momentum3 operator /(Momentum3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(Momentum3, 3)#
    public Momentum3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(Momentum3, 3)#
    public Momentum3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(Momentum3, 3)#
    public Momentum3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(Momentum3, 3)#
    public static Momentum3 operator %(Momentum3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(Momentum3, 3)#
    public static Momentum3 operator *(Momentum3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(Momentum3, 3)#
    public static Momentum3 operator *(Scalar a, Momentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(Momentum3, 3)#
    public static Momentum3 operator /(Momentum3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(Momentum3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(Momentum3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(Momentum3, 3)#
    public static Unhandled3 operator *(Momentum3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(Momentum3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, Momentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(Momentum3, 3)#
    public static Unhandled3 operator /(Momentum3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(Momentum3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(Momentum3, 3)#
    public static implicit operator (double x, double y, double z)(Momentum3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(Momentum3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(Momentum3, 3)#
    public static explicit operator Vector3(Momentum3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(Momentum3, 3)#
    public static Momentum3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(Momentum3, 3)#
    public static explicit operator Momentum3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(Momentum3, 3)#
    public static Momentum3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(Momentum3, 3)#
    public static explicit operator Momentum3(Vector3 a) => new(a);
}
