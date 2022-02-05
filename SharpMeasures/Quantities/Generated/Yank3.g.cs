namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(Yank3, 3, Yank, UnitOfYank, [NewtonPerSecond], [NewtonsPerSecond])#
public readonly partial record struct Yank3 :
    IVector3Quantity,
    IScalableVector3Quantity<Yank3>,
    INormalizableVector3Quantity<Yank3>,
    ITransformableVector3Quantity<Yank3>,
    IMultiplicableVector3Quantity<Yank3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Yank3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<Yank3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(Yank3, 3)#
    public static Yank3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(Yank3, 3)#
    public double X { get; init; }
    #Document:ComponentY(Yank3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(Yank3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(Yank3, 3, UnitOfYank, unitOfYank, [NewtonPerSecond])#
    public Yank3((Yank x, Yank y, Yank z) components, UnitOfYank unitOfYank) : this(components.x, components.y, components.z, unitOfYank) { }
    #Document:ConstructorComponentsUnit(Yank3, 3, UnitOfYank, unitOfYank, [NewtonPerSecond])#
    public Yank3(Yank x, Yank y, Yank z, UnitOfYank unitOfYank) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfYank) { }
    #Document:ConstructorScalarTupleUnit(Yank3, 3, UnitOfYank, unitOfYank, [NewtonPerSecond])#
    public Yank3((Scalar x, Scalar y, Scalar z) components, UnitOfYank unitOfYank) : this(components.x, components.y, components.z, unitOfYank) { }
    #Document:ConstructorScalarsUnit(Yank3, 3, UnitOfYank, unitOfYank, [NewtonPerSecond])#
    public Yank3(Scalar x, Scalar y, Scalar z, UnitOfYank unitOfYank) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfYank) { }
    #Document:ConstructorVectorUnit(Yank3, 3, UnitOfYank, unitOfYank, [NewtonPerSecond])#
    public Yank3(Vector3 components, UnitOfYank unitOfYank) : this(components.X, components.Y, components.Z, unitOfYank) { }
    #Document:ConstructorDoubleTupleUnit(Yank3, 3, UnitOfYank, unitOfYank, [NewtonPerSecond])#
    public Yank3((double x, double y, double z) components, UnitOfYank unitOfYank) : this(components.x, components.y, components.z, unitOfYank) { }
    #Document:ConstructorDoublesTupleUnit(Yank3, 3, UnitOfYank, unitOfYank, [NewtonPerSecond])#
    public Yank3(double x, double y, double z, UnitOfYank unitOfYank) : this(x * unitOfYank.Factor, y * unitOfYank.Factor, z * unitOfYank.Factor) { }

    #Document:ConstructorComponentTuple(Yank3, 3, UnitOfYank, unitOfYank, [NewtonPerSecond])#
    public Yank3((Yank x, Yank y, Yank z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(Yank3, 3, UnitOfYank, unitOfYank, [NewtonPerSecond])#
    public Yank3(Yank x, Yank y, Yank z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(Yank3, 3, UnitOfYank, unitOfYank, [NewtonPerSecond])#
    public Yank3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(Yank3, 3, UnitOfYank, unitOfYank, [NewtonPerSecond])#
    public Yank3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(Yank3, 3, UnitOfYank, unitOfYank, [NewtonPerSecond])#
    public Yank3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(Yank3, 3, UnitOfYank, unitOfYank, [NewtonPerSecond])#
    public Yank3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(Yank3, 3, UnitOfYank, unitOfYank, [NewtonPerSecond])#
    public Yank3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    #Document:InUnit(quantity = Yank3, dimensionality = 3, unit = UnitOfYank, unitName = NewtonPerSecond)#
    public Vector3 NewtonsPerSecond => InUnit(UnitOfYank.NewtonPerSecond);

    #Document:ScalarMagnitude(Yank3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(Yank3, 3, Yank)#
    public Yank Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(Yank3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(Yank3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(Yank3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(Yank3, 3)#
    public Yank3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(Yank3, 3)#
    public Yank3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(Yank3, 3)#
    public Yank3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(Yank3, 3)#
    public Yank Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(Yank3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(Yank3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(Yank3, 3)#
    public Yank3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(Yank3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(Yank3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(Yank3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [N / s]";

    #Document:InUnitInstance(Yank3, 3, UnitOfYank, unitOfYank)#
    public Vector3 InUnit(UnitOfYank unitOfYank) => InUnit(this, unitOfYank);
    #Document:InUnitStatic(Yank3, yank3, 3, UnitOfYank, unitOfYank)#
    private static Vector3 InUnit(Yank3 yank3, UnitOfYank unitOfYank) => yank3.ToVector3() / unitOfYank.Factor;
    
    #Document:PlusMethod(Yank3, 3)#
    public Yank3 Plus() => this;
    #Document:NegateMethod(Yank3, 3)#
    public Yank3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(Yank3, 3)#
    public static Yank3 operator +(Yank3 a) => a;
    #Document:NegateOperator(Yank3, 3)#
    public static Yank3 operator -(Yank3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(Yank3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(Yank3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(Yank3, 3)#
    public static Unhandled3 operator *(Yank3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(Yank3, 3)#
    public static Unhandled3 operator *(Unhandled a, Yank3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(Yank3, 3)#
    public static Unhandled3 operator /(Yank3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(Yank3, 3)#
    public Yank3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(Yank3, 3)#
    public Yank3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(Yank3, 3)#
    public Yank3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(Yank3, 3)#
    public static Yank3 operator %(Yank3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(Yank3, 3)#
    public static Yank3 operator *(Yank3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(Yank3, 3)#
    public static Yank3 operator *(double a, Yank3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(Yank3, 3)#
    public static Yank3 operator /(Yank3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(Yank3, 3)#
    public Yank3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(Yank3, 3)#
    public Yank3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(Yank3, 3)#
    public Yank3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(Yank3, 3)#
    public static Yank3 operator %(Yank3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(Yank3, 3)#
    public static Yank3 operator *(Yank3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(Yank3, 3)#
    public static Yank3 operator *(Scalar a, Yank3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(Yank3, 3)#
    public static Yank3 operator /(Yank3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(Yank3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(Yank3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(Yank3, 3)#
    public static Unhandled3 operator *(Yank3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(Yank3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, Yank3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(Yank3, 3)#
    public static Unhandled3 operator /(Yank3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(Yank3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(Yank3, 3)#
    public static implicit operator (double x, double y, double z)(Yank3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(Yank3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(Yank3, 3)#
    public static explicit operator Vector3(Yank3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(Yank3, 3)#
    public static Yank3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(Yank3, 3)#
    public static explicit operator Yank3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(Yank3, 3)#
    public static Yank3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(Yank3, 3)#
    public static explicit operator Yank3(Vector3 a) => new(a);
}
