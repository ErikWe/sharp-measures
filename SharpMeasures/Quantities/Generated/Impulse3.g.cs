namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(Impulse3, 3, Impulse, UnitOfImpulse, [NewtonSecond], [NewtonSeconds])#
public readonly partial record struct Impulse3 :
    IVector3Quantity,
    IScalableVector3Quantity<Impulse3>,
    INormalizableVector3Quantity<Impulse3>,
    ITransformableVector3Quantity<Impulse3>,
    IMultiplicableVector3Quantity<Impulse3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Impulse3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<Impulse3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(Impulse3, 3)#
    public static Impulse3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(Impulse3, 3)#
    public double X { get; init; }
    #Document:ComponentY(Impulse3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(Impulse3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(Impulse3, 3, UnitOfImpulse, unitOfImpulse, [NewtonSecond])#
    public Impulse3((Impulse x, Impulse y, Impulse z) components, UnitOfImpulse unitOfImpulse) : this(components.x, components.y, components.z, unitOfImpulse) { }
    #Document:ConstructorComponentsUnit(Impulse3, 3, UnitOfImpulse, unitOfImpulse, [NewtonSecond])#
    public Impulse3(Impulse x, Impulse y, Impulse z, UnitOfImpulse unitOfImpulse) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfImpulse) { }
    #Document:ConstructorScalarTupleUnit(Impulse3, 3, UnitOfImpulse, unitOfImpulse, [NewtonSecond])#
    public Impulse3((Scalar x, Scalar y, Scalar z) components, UnitOfImpulse unitOfImpulse) : this(components.x, components.y, components.z, unitOfImpulse) { }
    #Document:ConstructorScalarsUnit(Impulse3, 3, UnitOfImpulse, unitOfImpulse, [NewtonSecond])#
    public Impulse3(Scalar x, Scalar y, Scalar z, UnitOfImpulse unitOfImpulse) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfImpulse) { }
    #Document:ConstructorVectorUnit(Impulse3, 3, UnitOfImpulse, unitOfImpulse, [NewtonSecond])#
    public Impulse3(Vector3 components, UnitOfImpulse unitOfImpulse) : this(components.X, components.Y, components.Z, unitOfImpulse) { }
    #Document:ConstructorDoubleTupleUnit(Impulse3, 3, UnitOfImpulse, unitOfImpulse, [NewtonSecond])#
    public Impulse3((double x, double y, double z) components, UnitOfImpulse unitOfImpulse) : this(components.x, components.y, components.z, unitOfImpulse) { }
    #Document:ConstructorDoublesTupleUnit(Impulse3, 3, UnitOfImpulse, unitOfImpulse, [NewtonSecond])#
    public Impulse3(double x, double y, double z, UnitOfImpulse unitOfImpulse) : this(x * unitOfImpulse.Factor, y * unitOfImpulse.Factor, z * unitOfImpulse.Factor) { }

    #Document:ConstructorComponentTuple(Impulse3, 3, UnitOfImpulse, unitOfImpulse, [NewtonSecond])#
    public Impulse3((Impulse x, Impulse y, Impulse z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(Impulse3, 3, UnitOfImpulse, unitOfImpulse, [NewtonSecond])#
    public Impulse3(Impulse x, Impulse y, Impulse z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(Impulse3, 3, UnitOfImpulse, unitOfImpulse, [NewtonSecond])#
    public Impulse3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(Impulse3, 3, UnitOfImpulse, unitOfImpulse, [NewtonSecond])#
    public Impulse3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(Impulse3, 3, UnitOfImpulse, unitOfImpulse, [NewtonSecond])#
    public Impulse3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(Impulse3, 3, UnitOfImpulse, unitOfImpulse, [NewtonSecond])#
    public Impulse3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(Impulse3, 3, UnitOfImpulse, unitOfImpulse, [NewtonSecond])#
    public Impulse3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

);

    #Document:InUnit(quantity = Impulse3, dimensionality = 3, unit = UnitOfImpulse, unitName = NewtonSecond)#
    public Vector3 NewtonSeconds => InUnit(UnitOfImpulse.NewtonSecond);

    #Document:ScalarMagnitude(Impulse3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(Impulse3, 3, Impulse)#
    public Impulse Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(Impulse3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(Impulse3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(Impulse3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(Impulse3, 3)#
    public Impulse3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(Impulse3, 3)#
    public Impulse3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(Impulse3, 3)#
    public Impulse3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(Impulse3, 3)#
    public Impulse Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(Impulse3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(Impulse3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(Impulse3, 3)#
    public Impulse3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(Impulse3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(Impulse3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(Impulse3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [kg * m / s]";

    #Document:InUnitInstance(Impulse3, 3, UnitOfImpulse, unitOfImpulse)#
    public Vector3 InUnit(UnitOfImpulse unitOfImpulse) => InUnit(this, unitOfImpulse);
    #Document:InUnitStatic(Impulse3, impulse3, 3, UnitOfImpulse, unitOfImpulse)#
    private static Vector3 InUnit(Impulse3 impulse3, UnitOfImpulse unitOfImpulse) => impulse3.ToVector3() / unitOfImpulse.Factor;
    
    #Document:PlusMethod(Impulse3, 3)#
    public Impulse3 Plus() => this;
    #Document:NegateMethod(Impulse3, 3)#
    public Impulse3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(Impulse3, 3)#
    public static Impulse3 operator +(Impulse3 a) => a;
    #Document:NegateOperator(Impulse3, 3)#
    public static Impulse3 operator -(Impulse3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(Impulse3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(Impulse3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(Impulse3, 3)#
    public static Unhandled3 operator *(Impulse3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(Impulse3, 3)#
    public static Unhandled3 operator *(Unhandled a, Impulse3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(Impulse3, 3)#
    public static Unhandled3 operator /(Impulse3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(Impulse3, 3)#
    public Impulse3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(Impulse3, 3)#
    public Impulse3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(Impulse3, 3)#
    public Impulse3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(Impulse3, 3)#
    public static Impulse3 operator %(Impulse3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(Impulse3, 3)#
    public static Impulse3 operator *(Impulse3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(Impulse3, 3)#
    public static Impulse3 operator *(double a, Impulse3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(Impulse3, 3)#
    public static Impulse3 operator /(Impulse3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(Impulse3, 3)#
    public Impulse3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(Impulse3, 3)#
    public Impulse3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(Impulse3, 3)#
    public Impulse3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(Impulse3, 3)#
    public static Impulse3 operator %(Impulse3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(Impulse3, 3)#
    public static Impulse3 operator *(Impulse3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(Impulse3, 3)#
    public static Impulse3 operator *(Scalar a, Impulse3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(Impulse3, 3)#
    public static Impulse3 operator /(Impulse3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(Impulse3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(Impulse3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(Impulse3, 3)#
    public static Unhandled3 operator *(Impulse3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(Impulse3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, Impulse3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(Impulse3, 3)#
    public static Unhandled3 operator /(Impulse3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(Impulse3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(Impulse3, 3)#
    public static implicit operator (double x, double y, double z)(Impulse3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(Impulse3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(Impulse3, 3)#
    public static explicit operator Vector3(Impulse3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(Impulse3, 3)#
    public static Impulse3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(Impulse3, 3)#
    public static explicit operator Impulse3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(Impulse3, 3)#
    public static Impulse3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(Impulse3, 3)#
    public static explicit operator Impulse3(Vector3 a) => new(a);
}
