namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(Force3, 3, Force, UnitOfForce, [Newton, PoundForce], [Newtons, PoundsForce])#
public readonly partial record struct Force3 :
    IVector3Quantity,
    IScalableVector3Quantity<Force3>,
    INormalizableVector3Quantity<Force3>,
    ITransformableVector3Quantity<Force3>,
    IMultiplicableVector3Quantity<Force3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Force3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<Force3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(Force3, 3)#
    public static Force3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(Force3, 3)#
    public double X { get; init; }
    #Document:ComponentY(Force3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(Force3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(Force3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Force3((Force x, Force y, Force z) components, UnitOfForce unitOfForce) : this(components.x, components.y, components.z, unitOfForce) { }
    #Document:ConstructorComponentsUnit(Force3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Force3(Force x, Force y, Force z, UnitOfForce unitOfForce) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfForce) { }
    #Document:ConstructorScalarTupleUnit(Force3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Force3((Scalar x, Scalar y, Scalar z) components, UnitOfForce unitOfForce) : this(components.x, components.y, components.z, unitOfForce) { }
    #Document:ConstructorScalarsUnit(Force3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Force3(Scalar x, Scalar y, Scalar z, UnitOfForce unitOfForce) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfForce) { }
    #Document:ConstructorVectorUnit(Force3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Force3(Vector3 components, UnitOfForce unitOfForce) : this(components.X, components.Y, components.Z, unitOfForce) { }
    #Document:ConstructorDoubleTupleUnit(Force3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Force3((double x, double y, double z) components, UnitOfForce unitOfForce) : this(components.x, components.y, components.z, unitOfForce) { }
    #Document:ConstructorDoublesTupleUnit(Force3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Force3(double x, double y, double z, UnitOfForce unitOfForce) : this(x * unitOfForce.Factor, y * unitOfForce.Factor, z * unitOfForce.Factor) { }

    #Document:ConstructorComponentTuple(Force3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Force3((Force x, Force y, Force z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(Force3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Force3(Force x, Force y, Force z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(Force3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Force3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(Force3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Force3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(Force3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Force3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(Force3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Force3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(Force3, 3, UnitOfForce, unitOfForce, [Newton, PoundForce])#
    public Force3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

);

    #Document:InUnit(quantity = Force3, dimensionality = 3, unit = UnitOfForce, unitName = Newton)#
    public Vector3 Newtons => InUnit(UnitOfForce.Newton);
    #Document:InUnit(quantity = Force3, dimensionality = 3, unit = UnitOfForce, unitName = PoundForce)#
    public Vector3 PoundsForce => InUnit(UnitOfForce.PoundForce);

    #Document:ScalarMagnitude(Force3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(Force3, 3, Force)#
    public Force Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(Force3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(Force3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(Force3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(Force3, 3)#
    public Force3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(Force3, 3)#
    public Force3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(Force3, 3)#
    public Force3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(Force3, 3)#
    public Force Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(Force3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(Force3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(Force3, 3)#
    public Force3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(Force3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(Force3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(Force3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [N]";

    #Document:InUnitInstance(Force3, 3, UnitOfForce, unitOfForce)#
    public Vector3 InUnit(UnitOfForce unitOfForce) => InUnit(this, unitOfForce);
    #Document:InUnitStatic(Force3, force3, 3, UnitOfForce, unitOfForce)#
    private static Vector3 InUnit(Force3 force3, UnitOfForce unitOfForce) => force3.ToVector3() / unitOfForce.Factor;
    
    #Document:PlusMethod(Force3, 3)#
    public Force3 Plus() => this;
    #Document:NegateMethod(Force3, 3)#
    public Force3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(Force3, 3)#
    public static Force3 operator +(Force3 a) => a;
    #Document:NegateOperator(Force3, 3)#
    public static Force3 operator -(Force3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(Force3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(Force3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(Force3, 3)#
    public static Unhandled3 operator *(Force3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(Force3, 3)#
    public static Unhandled3 operator *(Unhandled a, Force3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(Force3, 3)#
    public static Unhandled3 operator /(Force3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(Force3, 3)#
    public Force3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(Force3, 3)#
    public Force3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(Force3, 3)#
    public Force3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(Force3, 3)#
    public static Force3 operator %(Force3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(Force3, 3)#
    public static Force3 operator *(Force3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(Force3, 3)#
    public static Force3 operator *(double a, Force3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(Force3, 3)#
    public static Force3 operator /(Force3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(Force3, 3)#
    public Force3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(Force3, 3)#
    public Force3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(Force3, 3)#
    public Force3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(Force3, 3)#
    public static Force3 operator %(Force3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(Force3, 3)#
    public static Force3 operator *(Force3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(Force3, 3)#
    public static Force3 operator *(Scalar a, Force3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(Force3, 3)#
    public static Force3 operator /(Force3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(Force3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(Force3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(Force3, 3)#
    public static Unhandled3 operator *(Force3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(Force3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, Force3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(Force3, 3)#
    public static Unhandled3 operator /(Force3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(Force3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(Force3, 3)#
    public static implicit operator (double x, double y, double z)(Force3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(Force3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(Force3, 3)#
    public static explicit operator Vector3(Force3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(Force3, 3)#
    public static Force3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(Force3, 3)#
    public static explicit operator Force3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(Force3, 3)#
    public static Force3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(Force3, 3)#
    public static explicit operator Force3(Vector3 a) => new(a);
}
