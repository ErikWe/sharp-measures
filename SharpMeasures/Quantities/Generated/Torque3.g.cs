namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(Torque3, 3, Torque, UnitOfTorque, [NewtonMetre], [NewtonMetres])#
public readonly partial record struct Torque3 :
    IVector3Quantity,
    IScalableVector3Quantity<Torque3>,
    INormalizableVector3Quantity<Torque3>,
    ITransformableVector3Quantity<Torque3>,
    IMultiplicableVector3Quantity<Torque3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Torque3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<Torque3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(Torque3, 3)#
    public static Torque3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(Torque3, 3)#
    public double X { get; init; }
    #Document:ComponentY(Torque3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(Torque3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(Torque3, 3, UnitOfTorque, unitOfTorque, [NewtonMetre])#
    public Torque3((Torque x, Torque y, Torque z) components, UnitOfTorque unitOfTorque) : this(components.x, components.y, components.z, unitOfTorque) { }
    #Document:ConstructorComponentsUnit(Torque3, 3, UnitOfTorque, unitOfTorque, [NewtonMetre])#
    public Torque3(Torque x, Torque y, Torque z, UnitOfTorque unitOfTorque) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfTorque) { }
    #Document:ConstructorScalarTupleUnit(Torque3, 3, UnitOfTorque, unitOfTorque, [NewtonMetre])#
    public Torque3((Scalar x, Scalar y, Scalar z) components, UnitOfTorque unitOfTorque) : this(components.x, components.y, components.z, unitOfTorque) { }
    #Document:ConstructorScalarsUnit(Torque3, 3, UnitOfTorque, unitOfTorque, [NewtonMetre])#
    public Torque3(Scalar x, Scalar y, Scalar z, UnitOfTorque unitOfTorque) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfTorque) { }
    #Document:ConstructorVectorUnit(Torque3, 3, UnitOfTorque, unitOfTorque, [NewtonMetre])#
    public Torque3(Vector3 components, UnitOfTorque unitOfTorque) : this(components.X, components.Y, components.Z, unitOfTorque) { }
    #Document:ConstructorDoubleTupleUnit(Torque3, 3, UnitOfTorque, unitOfTorque, [NewtonMetre])#
    public Torque3((double x, double y, double z) components, UnitOfTorque unitOfTorque) : this(components.x, components.y, components.z, unitOfTorque) { }
    #Document:ConstructorDoublesTupleUnit(Torque3, 3, UnitOfTorque, unitOfTorque, [NewtonMetre])#
    public Torque3(double x, double y, double z, UnitOfTorque unitOfTorque) : this(x * unitOfTorque.Factor, y * unitOfTorque.Factor, z * unitOfTorque.Factor) { }

    #Document:ConstructorComponentTuple(Torque3, 3, UnitOfTorque, unitOfTorque, [NewtonMetre])#
    public Torque3((Torque x, Torque y, Torque z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(Torque3, 3, UnitOfTorque, unitOfTorque, [NewtonMetre])#
    public Torque3(Torque x, Torque y, Torque z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(Torque3, 3, UnitOfTorque, unitOfTorque, [NewtonMetre])#
    public Torque3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(Torque3, 3, UnitOfTorque, unitOfTorque, [NewtonMetre])#
    public Torque3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(Torque3, 3, UnitOfTorque, unitOfTorque, [NewtonMetre])#
    public Torque3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(Torque3, 3, UnitOfTorque, unitOfTorque, [NewtonMetre])#
    public Torque3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(Torque3, 3, UnitOfTorque, unitOfTorque, [NewtonMetre])#
    public Torque3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    #Document:InUnit(quantity = Torque3, dimensionality = 3, unit = UnitOfTorque, unitName = NewtonMetre)#
    public Vector3 NewtonMetres => InUnit(UnitOfTorque.NewtonMetre);

    #Document:ScalarMagnitude(Torque3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(Torque3, 3, Torque)#
    public Torque Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(Torque3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(Torque3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(Torque3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(Torque3, 3)#
    public Torque3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(Torque3, 3)#
    public Torque3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(Torque3, 3)#
    public Torque3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(Torque3, 3)#
    public Torque Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(Torque3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(Torque3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(Torque3, 3)#
    public Torque3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(Torque3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(Torque3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(Torque3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [N * m]";

    #Document:InUnitInstance(Torque3, 3, UnitOfTorque, unitOfTorque)#
    public Vector3 InUnit(UnitOfTorque unitOfTorque) => InUnit(this, unitOfTorque);
    #Document:InUnitStatic(Torque3, torque3, 3, UnitOfTorque, unitOfTorque)#
    private static Vector3 InUnit(Torque3 torque3, UnitOfTorque unitOfTorque) => torque3.ToVector3() / unitOfTorque.Factor;
    
    #Document:PlusMethod(Torque3, 3)#
    public Torque3 Plus() => this;
    #Document:NegateMethod(Torque3, 3)#
    public Torque3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(Torque3, 3)#
    public static Torque3 operator +(Torque3 a) => a;
    #Document:NegateOperator(Torque3, 3)#
    public static Torque3 operator -(Torque3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(Torque3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(Torque3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(Torque3, 3)#
    public static Unhandled3 operator *(Torque3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(Torque3, 3)#
    public static Unhandled3 operator *(Unhandled a, Torque3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(Torque3, 3)#
    public static Unhandled3 operator /(Torque3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(Torque3, 3)#
    public Torque3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(Torque3, 3)#
    public Torque3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(Torque3, 3)#
    public Torque3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(Torque3, 3)#
    public static Torque3 operator %(Torque3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(Torque3, 3)#
    public static Torque3 operator *(Torque3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(Torque3, 3)#
    public static Torque3 operator *(double a, Torque3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(Torque3, 3)#
    public static Torque3 operator /(Torque3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(Torque3, 3)#
    public Torque3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(Torque3, 3)#
    public Torque3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(Torque3, 3)#
    public Torque3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(Torque3, 3)#
    public static Torque3 operator %(Torque3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(Torque3, 3)#
    public static Torque3 operator *(Torque3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(Torque3, 3)#
    public static Torque3 operator *(Scalar a, Torque3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(Torque3, 3)#
    public static Torque3 operator /(Torque3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(Torque3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(Torque3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(Torque3, 3)#
    public static Unhandled3 operator *(Torque3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(Torque3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, Torque3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(Torque3, 3)#
    public static Unhandled3 operator /(Torque3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(Torque3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(Torque3, 3)#
    public static implicit operator (double x, double y, double z)(Torque3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(Torque3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(Torque3, 3)#
    public static explicit operator Vector3(Torque3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(Torque3, 3)#
    public static Torque3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(Torque3, 3)#
    public static explicit operator Torque3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(Torque3, 3)#
    public static Torque3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(Torque3, 3)#
    public static explicit operator Torque3(Vector3 a) => new(a);
}
