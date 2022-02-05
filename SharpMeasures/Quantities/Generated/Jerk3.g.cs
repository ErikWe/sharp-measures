namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(Jerk3, 3, Jerk, UnitOfJerk, [MetrePerSecondCubed], [MetresPerSecondCubed])#
public readonly partial record struct Jerk3 :
    IVector3Quantity,
    IScalableVector3Quantity<Jerk3>,
    INormalizableVector3Quantity<Jerk3>,
    ITransformableVector3Quantity<Jerk3>,
    IMultiplicableVector3Quantity<Jerk3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Jerk3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<Jerk3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(Jerk3, 3)#
    public static Jerk3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(Jerk3, 3)#
    public double X { get; init; }
    #Document:ComponentY(Jerk3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(Jerk3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(Jerk3, 3, UnitOfJerk, unitOfJerk, [MetrePerSecondCubed])#
    public Jerk3((Jerk x, Jerk y, Jerk z) components, UnitOfJerk unitOfJerk) : this(components.x, components.y, components.z, unitOfJerk) { }
    #Document:ConstructorComponentsUnit(Jerk3, 3, UnitOfJerk, unitOfJerk, [MetrePerSecondCubed])#
    public Jerk3(Jerk x, Jerk y, Jerk z, UnitOfJerk unitOfJerk) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfJerk) { }
    #Document:ConstructorScalarTupleUnit(Jerk3, 3, UnitOfJerk, unitOfJerk, [MetrePerSecondCubed])#
    public Jerk3((Scalar x, Scalar y, Scalar z) components, UnitOfJerk unitOfJerk) : this(components.x, components.y, components.z, unitOfJerk) { }
    #Document:ConstructorScalarsUnit(Jerk3, 3, UnitOfJerk, unitOfJerk, [MetrePerSecondCubed])#
    public Jerk3(Scalar x, Scalar y, Scalar z, UnitOfJerk unitOfJerk) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfJerk) { }
    #Document:ConstructorVectorUnit(Jerk3, 3, UnitOfJerk, unitOfJerk, [MetrePerSecondCubed])#
    public Jerk3(Vector3 components, UnitOfJerk unitOfJerk) : this(components.X, components.Y, components.Z, unitOfJerk) { }
    #Document:ConstructorDoubleTupleUnit(Jerk3, 3, UnitOfJerk, unitOfJerk, [MetrePerSecondCubed])#
    public Jerk3((double x, double y, double z) components, UnitOfJerk unitOfJerk) : this(components.x, components.y, components.z, unitOfJerk) { }
    #Document:ConstructorDoublesTupleUnit(Jerk3, 3, UnitOfJerk, unitOfJerk, [MetrePerSecondCubed])#
    public Jerk3(double x, double y, double z, UnitOfJerk unitOfJerk) : this(x * unitOfJerk.Factor, y * unitOfJerk.Factor, z * unitOfJerk.Factor) { }

    #Document:ConstructorComponentTuple(Jerk3, 3, UnitOfJerk, unitOfJerk, [MetrePerSecondCubed])#
    public Jerk3((Jerk x, Jerk y, Jerk z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(Jerk3, 3, UnitOfJerk, unitOfJerk, [MetrePerSecondCubed])#
    public Jerk3(Jerk x, Jerk y, Jerk z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(Jerk3, 3, UnitOfJerk, unitOfJerk, [MetrePerSecondCubed])#
    public Jerk3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(Jerk3, 3, UnitOfJerk, unitOfJerk, [MetrePerSecondCubed])#
    public Jerk3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(Jerk3, 3, UnitOfJerk, unitOfJerk, [MetrePerSecondCubed])#
    public Jerk3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(Jerk3, 3, UnitOfJerk, unitOfJerk, [MetrePerSecondCubed])#
    public Jerk3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(Jerk3, 3, UnitOfJerk, unitOfJerk, [MetrePerSecondCubed])#
    public Jerk3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    #Document:InUnit(quantity = Jerk3, dimensionality = 3, unit = UnitOfJerk, unitName = MetrePerSecondCubed)#
    public Vector3 MetresPerSecondCubed => InUnit(UnitOfJerk.MetrePerSecondCubed);

    #Document:ScalarMagnitude(Jerk3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(Jerk3, 3, Jerk)#
    public Jerk Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(Jerk3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(Jerk3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(Jerk3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(Jerk3, 3)#
    public Jerk3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(Jerk3, 3)#
    public Jerk3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(Jerk3, 3)#
    public Jerk3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(Jerk3, 3)#
    public Jerk Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(Jerk3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(Jerk3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(Jerk3, 3)#
    public Jerk3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(Jerk3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(Jerk3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(Jerk3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [m / s^3]";

    #Document:InUnitInstance(Jerk3, 3, UnitOfJerk, unitOfJerk)#
    public Vector3 InUnit(UnitOfJerk unitOfJerk) => InUnit(this, unitOfJerk);
    #Document:InUnitStatic(Jerk3, jerk3, 3, UnitOfJerk, unitOfJerk)#
    private static Vector3 InUnit(Jerk3 jerk3, UnitOfJerk unitOfJerk) => jerk3.ToVector3() / unitOfJerk.Factor;
    
    #Document:PlusMethod(Jerk3, 3)#
    public Jerk3 Plus() => this;
    #Document:NegateMethod(Jerk3, 3)#
    public Jerk3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(Jerk3, 3)#
    public static Jerk3 operator +(Jerk3 a) => a;
    #Document:NegateOperator(Jerk3, 3)#
    public static Jerk3 operator -(Jerk3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(Jerk3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(Jerk3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(Jerk3, 3)#
    public static Unhandled3 operator *(Jerk3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(Jerk3, 3)#
    public static Unhandled3 operator *(Unhandled a, Jerk3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(Jerk3, 3)#
    public static Unhandled3 operator /(Jerk3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(Jerk3, 3)#
    public Jerk3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(Jerk3, 3)#
    public Jerk3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(Jerk3, 3)#
    public Jerk3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(Jerk3, 3)#
    public static Jerk3 operator %(Jerk3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(Jerk3, 3)#
    public static Jerk3 operator *(Jerk3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(Jerk3, 3)#
    public static Jerk3 operator *(double a, Jerk3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(Jerk3, 3)#
    public static Jerk3 operator /(Jerk3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(Jerk3, 3)#
    public Jerk3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(Jerk3, 3)#
    public Jerk3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(Jerk3, 3)#
    public Jerk3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(Jerk3, 3)#
    public static Jerk3 operator %(Jerk3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(Jerk3, 3)#
    public static Jerk3 operator *(Jerk3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(Jerk3, 3)#
    public static Jerk3 operator *(Scalar a, Jerk3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(Jerk3, 3)#
    public static Jerk3 operator /(Jerk3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(Jerk3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(Jerk3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(Jerk3, 3)#
    public static Unhandled3 operator *(Jerk3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(Jerk3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, Jerk3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(Jerk3, 3)#
    public static Unhandled3 operator /(Jerk3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(Jerk3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(Jerk3, 3)#
    public static implicit operator (double x, double y, double z)(Jerk3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(Jerk3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(Jerk3, 3)#
    public static explicit operator Vector3(Jerk3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(Jerk3, 3)#
    public static Jerk3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(Jerk3, 3)#
    public static explicit operator Jerk3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(Jerk3, 3)#
    public static Jerk3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(Jerk3, 3)#
    public static explicit operator Jerk3(Vector3 a) => new(a);
}
