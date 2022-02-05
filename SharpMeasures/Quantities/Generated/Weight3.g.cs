namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(Weight3, 3, Weight, UnitOfForce, [Newton, PoundForce], [Newtons, PoundsForce])#
public readonly partial record struct Weight3 :
    IVector3Quantity,
    IScalableVector3Quantity<Weight3>,
    INormalizableVector3Quantity<Weight3>,
    ITransformableVector3Quantity<Weight3>,
    IMultiplicableVector3Quantity<Weight3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Weight3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<Weight3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(Weight3, 3)#
    public static Weight3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(Weight3, 3)#
    public double X { get; init; }
    #Document:ComponentY(Weight3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(Weight3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(Weight3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Weight3((Weight x, Weight y, Weight z) components, UnitOfForce unitOfForce) : this(components.x, components.y, components.z, unitOfForce) { }
    #Document:ConstructorComponentsUnit(Weight3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Weight3(Weight x, Weight y, Weight z, UnitOfForce unitOfForce) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfForce) { }
    #Document:ConstructorScalarTupleUnit(Weight3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Weight3((Scalar x, Scalar y, Scalar z) components, UnitOfForce unitOfForce) : this(components.x, components.y, components.z, unitOfForce) { }
    #Document:ConstructorScalarsUnit(Weight3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Weight3(Scalar x, Scalar y, Scalar z, UnitOfForce unitOfForce) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfForce) { }
    #Document:ConstructorVectorUnit(Weight3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Weight3(Vector3 components, UnitOfForce unitOfForce) : this(components.X, components.Y, components.Z, unitOfForce) { }
    #Document:ConstructorDoubleTupleUnit(Weight3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Weight3((double x, double y, double z) components, UnitOfForce unitOfForce) : this(components.x, components.y, components.z, unitOfForce) { }
    #Document:ConstructorDoublesTupleUnit(Weight3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Weight3(double x, double y, double z, UnitOfForce unitOfForce) : this(x * unitOfForce.Factor, y * unitOfForce.Factor, z * unitOfForce.Factor) { }

    #Document:ConstructorComponentTuple(Weight3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Weight3((Weight x, Weight y, Weight z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(Weight3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Weight3(Weight x, Weight y, Weight z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(Weight3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Weight3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(Weight3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Weight3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(Weight3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Weight3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(Weight3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Weight3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(Weight3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Weight3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

);

    #Document:InUnit(quantity = Weight3, dimensionality = 3, unit = UnitOfForce, unitName = Newton)#
    public Vector3 Newtons => InUnit(UnitOfForce.Newton);
    #Document:InUnit(quantity = Weight3, dimensionality = 3, unit = UnitOfForce, unitName = PoundForce)#
    public Vector3 PoundsForce => InUnit(UnitOfForce.PoundForce);

    #Document:ScalarMagnitude(Weight3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(Weight3, 3, Weight)#
    public Weight Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(Weight3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(Weight3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(Weight3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(Weight3, 3)#
    public Weight3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(Weight3, 3)#
    public Weight3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(Weight3, 3)#
    public Weight3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(Weight3, 3)#
    public Weight Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(Weight3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(Weight3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(Weight3, 3)#
    public Weight3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(Weight3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(Weight3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(Weight3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [N]";

    #Document:InUnitInstance(Weight3, 3, UnitOfForce, unitOfForce)#
    public Vector3 InUnit(UnitOfForce unitOfForce) => InUnit(this, unitOfForce);
    #Document:InUnitStatic(Weight3, weight3, 3, UnitOfForce, unitOfForce)#
    private static Vector3 InUnit(Weight3 weight3, UnitOfForce unitOfForce) => weight3.ToVector3() / unitOfForce.Factor;
    
    #Document:PlusMethod(Weight3, 3)#
    public Weight3 Plus() => this;
    #Document:NegateMethod(Weight3, 3)#
    public Weight3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(Weight3, 3)#
    public static Weight3 operator +(Weight3 a) => a;
    #Document:NegateOperator(Weight3, 3)#
    public static Weight3 operator -(Weight3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(Weight3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(Weight3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(Weight3, 3)#
    public static Unhandled3 operator *(Weight3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(Weight3, 3)#
    public static Unhandled3 operator *(Unhandled a, Weight3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(Weight3, 3)#
    public static Unhandled3 operator /(Weight3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(Weight3, 3)#
    public Weight3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(Weight3, 3)#
    public Weight3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(Weight3, 3)#
    public Weight3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(Weight3, 3)#
    public static Weight3 operator %(Weight3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(Weight3, 3)#
    public static Weight3 operator *(Weight3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(Weight3, 3)#
    public static Weight3 operator *(double a, Weight3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(Weight3, 3)#
    public static Weight3 operator /(Weight3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(Weight3, 3)#
    public Weight3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(Weight3, 3)#
    public Weight3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(Weight3, 3)#
    public Weight3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(Weight3, 3)#
    public static Weight3 operator %(Weight3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(Weight3, 3)#
    public static Weight3 operator *(Weight3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(Weight3, 3)#
    public static Weight3 operator *(Scalar a, Weight3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(Weight3, 3)#
    public static Weight3 operator /(Weight3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(Weight3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(Weight3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(Weight3, 3)#
    public static Unhandled3 operator *(Weight3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(Weight3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, Weight3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(Weight3, 3)#
    public static Unhandled3 operator /(Weight3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(Weight3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(Weight3, 3)#
    public static implicit operator (double x, double y, double z)(Weight3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(Weight3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(Weight3, 3)#
    public static explicit operator Vector3(Weight3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(Weight3, 3)#
    public static Weight3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(Weight3, 3)#
    public static explicit operator Weight3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(Weight3, 3)#
    public static Weight3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(Weight3, 3)#
    public static explicit operator Weight3(Vector3 a) => new(a);
}
