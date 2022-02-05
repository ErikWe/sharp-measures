namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(Velocity3, 3, Speed, UnitOfVelocity, [MetrePerSecond, KilometrePerHour, MilePerHour], [MetresPerSecond, KilometresPerHour, MilesPerHour])#
public readonly partial record struct Velocity3 :
    IVector3Quantity,
    IScalableVector3Quantity<Velocity3>,
    INormalizableVector3Quantity<Velocity3>,
    ITransformableVector3Quantity<Velocity3>,
    IMultiplicableVector3Quantity<Velocity3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Velocity3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<Velocity3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(Velocity3, 3)#
    public static Velocity3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(Velocity3, 3)#
    public double X { get; init; }
    #Document:ComponentY(Velocity3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(Velocity3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(Velocity3, 3, UnitOfVelocity, unitOfVelocity, [MetrePerSecond, KilometrePerHour, MilePerHour])#
    public Velocity3((Speed x, Speed y, Speed z) components, UnitOfVelocity unitOfVelocity) : this(components.x, components.y, components.z, unitOfVelocity) { }
    #Document:ConstructorComponentsUnit(Velocity3, 3, UnitOfVelocity, unitOfVelocity, [MetrePerSecond, KilometrePerHour, MilePerHour])#
    public Velocity3(Speed x, Speed y, Speed z, UnitOfVelocity unitOfVelocity) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfVelocity) { }
    #Document:ConstructorScalarTupleUnit(Velocity3, 3, UnitOfVelocity, unitOfVelocity, [MetrePerSecond, KilometrePerHour, MilePerHour])#
    public Velocity3((Scalar x, Scalar y, Scalar z) components, UnitOfVelocity unitOfVelocity) : this(components.x, components.y, components.z, unitOfVelocity) { }
    #Document:ConstructorScalarsUnit(Velocity3, 3, UnitOfVelocity, unitOfVelocity, [MetrePerSecond, KilometrePerHour, MilePerHour])#
    public Velocity3(Scalar x, Scalar y, Scalar z, UnitOfVelocity unitOfVelocity) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfVelocity) { }
    #Document:ConstructorVectorUnit(Velocity3, 3, UnitOfVelocity, unitOfVelocity, [MetrePerSecond, KilometrePerHour, MilePerHour])#
    public Velocity3(Vector3 components, UnitOfVelocity unitOfVelocity) : this(components.X, components.Y, components.Z, unitOfVelocity) { }
    #Document:ConstructorDoubleTupleUnit(Velocity3, 3, UnitOfVelocity, unitOfVelocity, [MetrePerSecond, KilometrePerHour, MilePerHour])#
    public Velocity3((double x, double y, double z) components, UnitOfVelocity unitOfVelocity) : this(components.x, components.y, components.z, unitOfVelocity) { }
    #Document:ConstructorDoublesTupleUnit(Velocity3, 3, UnitOfVelocity, unitOfVelocity, [MetrePerSecond, KilometrePerHour, MilePerHour])#
    public Velocity3(double x, double y, double z, UnitOfVelocity unitOfVelocity) : this(x * unitOfVelocity.Factor, y * unitOfVelocity.Factor, z * unitOfVelocity.Factor) { }

    #Document:ConstructorComponentTuple(Velocity3, 3, UnitOfVelocity, unitOfVelocity, [MetrePerSecond, KilometrePerHour, MilePerHour])#
    public Velocity3((Speed x, Speed y, Speed z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(Velocity3, 3, UnitOfVelocity, unitOfVelocity, [MetrePerSecond, KilometrePerHour, MilePerHour])#
    public Velocity3(Speed x, Speed y, Speed z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(Velocity3, 3, UnitOfVelocity, unitOfVelocity, [MetrePerSecond, KilometrePerHour, MilePerHour])#
    public Velocity3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(Velocity3, 3, UnitOfVelocity, unitOfVelocity, [MetrePerSecond, KilometrePerHour, MilePerHour])#
    public Velocity3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(Velocity3, 3, UnitOfVelocity, unitOfVelocity, [MetrePerSecond, KilometrePerHour, MilePerHour])#
    public Velocity3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(Velocity3, 3, UnitOfVelocity, unitOfVelocity, [MetrePerSecond, KilometrePerHour, MilePerHour])#
    public Velocity3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(Velocity3, 3, UnitOfVelocity, unitOfVelocity, [MetrePerSecond, KilometrePerHour, MilePerHour])#
    public Velocity3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    #Document:InUnit(quantity = Velocity3, dimensionality = 3, unit = UnitOfVelocity, unitName = MetrePerSecond)#
    public Vector3 MetresPerSecond => InUnit(UnitOfVelocity.MetrePerSecond);
    #Document:InUnit(quantity = Velocity3, dimensionality = 3, unit = UnitOfVelocity, unitName = KilometrePerHour)#
    public Vector3 KilometresPerHour => InUnit(UnitOfVelocity.KilometrePerHour);

    #Document:InUnit(quantity = Velocity3, dimensionality = 3, unit = UnitOfVelocity, unitName = MilePerHour)#
    public Vector3 MilesPerHour => InUnit(UnitOfVelocity.MilePerHour);

    #Document:ScalarMagnitude(Velocity3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(Velocity3, 3, Speed)#
    public Speed Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(Velocity3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(Velocity3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(Velocity3, 3, SpeedSquared)#
    public SpeedSquared SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(Velocity3, 3)#
    public Velocity3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(Velocity3, 3)#
    public Velocity3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(Velocity3, 3)#
    public Velocity3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(Velocity3, 3)#
    public Speed Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(Velocity3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(Velocity3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(Velocity3, 3)#
    public Velocity3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(Velocity3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(Velocity3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(Velocity3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [m / s]";

    #Document:InUnitInstance(Velocity3, 3, UnitOfVelocity, unitOfVelocity)#
    public Vector3 InUnit(UnitOfVelocity unitOfVelocity) => InUnit(this, unitOfVelocity);
    #Document:InUnitStatic(Velocity3, velocity3, 3, UnitOfVelocity, unitOfVelocity)#
    private static Vector3 InUnit(Velocity3 velocity3, UnitOfVelocity unitOfVelocity) => velocity3.ToVector3() / unitOfVelocity.Factor;
    
    #Document:PlusMethod(Velocity3, 3)#
    public Velocity3 Plus() => this;
    #Document:NegateMethod(Velocity3, 3)#
    public Velocity3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(Velocity3, 3)#
    public static Velocity3 operator +(Velocity3 a) => a;
    #Document:NegateOperator(Velocity3, 3)#
    public static Velocity3 operator -(Velocity3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(Velocity3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(Velocity3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(Velocity3, 3)#
    public static Unhandled3 operator *(Velocity3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(Velocity3, 3)#
    public static Unhandled3 operator *(Unhandled a, Velocity3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(Velocity3, 3)#
    public static Unhandled3 operator /(Velocity3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(Velocity3, 3)#
    public Velocity3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(Velocity3, 3)#
    public Velocity3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(Velocity3, 3)#
    public Velocity3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(Velocity3, 3)#
    public static Velocity3 operator %(Velocity3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(Velocity3, 3)#
    public static Velocity3 operator *(Velocity3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(Velocity3, 3)#
    public static Velocity3 operator *(double a, Velocity3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(Velocity3, 3)#
    public static Velocity3 operator /(Velocity3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(Velocity3, 3)#
    public Velocity3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(Velocity3, 3)#
    public Velocity3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(Velocity3, 3)#
    public Velocity3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(Velocity3, 3)#
    public static Velocity3 operator %(Velocity3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(Velocity3, 3)#
    public static Velocity3 operator *(Velocity3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(Velocity3, 3)#
    public static Velocity3 operator *(Scalar a, Velocity3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(Velocity3, 3)#
    public static Velocity3 operator /(Velocity3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(Velocity3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(Velocity3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(Velocity3, 3)#
    public static Unhandled3 operator *(Velocity3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(Velocity3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, Velocity3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(Velocity3, 3)#
    public static Unhandled3 operator /(Velocity3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(Velocity3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(Velocity3, 3)#
    public static implicit operator (double x, double y, double z)(Velocity3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(Velocity3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(Velocity3, 3)#
    public static explicit operator Vector3(Velocity3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(Velocity3, 3)#
    public static Velocity3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(Velocity3, 3)#
    public static explicit operator Velocity3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(Velocity3, 3)#
    public static Velocity3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(Velocity3, 3)#
    public static explicit operator Velocity3(Vector3 a) => new(a);
}
