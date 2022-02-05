namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(TemperatureGradient3, 3, TemperatureGradient, UnitOfTemperatureGradient, [KelvinPerMetre, CelsiusPerMetre, RankinePerMetre, FahrenheitPerMetre], [KelvinPerMetre, CelsiusPerMetre, RankinePerMetre, FahrenheitPerMetre])#
public readonly partial record struct TemperatureGradient3 :
    IVector3Quantity,
    IScalableVector3Quantity<TemperatureGradient3>,
    INormalizableVector3Quantity<TemperatureGradient3>,
    ITransformableVector3Quantity<TemperatureGradient3>,
    IMultiplicableVector3Quantity<TemperatureGradient3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<TemperatureGradient3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<TemperatureGradient3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(TemperatureGradient3, 3)#
    public static TemperatureGradient3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(TemperatureGradient3, 3)#
    public double X { get; init; }
    #Document:ComponentY(TemperatureGradient3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(TemperatureGradient3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(TemperatureGradient3, 3, UnitOfTemperatureGradient, unitOfTemperatureGradient, [KelvinPerMetre, CelsiusPerMetre, RankinePerMetre, FahrenheitPerMetre])#
    public TemperatureGradient3((TemperatureGradient x, TemperatureGradient y, TemperatureGradient z) components, UnitOfTemperatureGradient unitOfTemperatureGradient) : 
    	this(components.x, components.y, components.z, unitOfTemperatureGradient) { }
    #Document:ConstructorComponentsUnit(TemperatureGradient3, 3, UnitOfTemperatureGradient, unitOfTemperatureGradient, [KelvinPerMetre, CelsiusPerMetre, RankinePerMetre, FahrenheitPerMetre])#
    public TemperatureGradient3(TemperatureGradient x, TemperatureGradient y, TemperatureGradient z, UnitOfTemperatureGradient unitOfTemperatureGradient) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfTemperatureGradient) { }
    #Document:ConstructorScalarTupleUnit(TemperatureGradient3, 3, UnitOfTemperatureGradient, unitOfTemperatureGradient, [KelvinPerMetre, CelsiusPerMetre, RankinePerMetre, FahrenheitPerMetre])#
    public TemperatureGradient3((Scalar x, Scalar y, Scalar z) components, UnitOfTemperatureGradient unitOfTemperatureGradient) : 
    	this(components.x, components.y, components.z, unitOfTemperatureGradient) { }
    #Document:ConstructorScalarsUnit(TemperatureGradient3, 3, UnitOfTemperatureGradient, unitOfTemperatureGradient, [KelvinPerMetre, CelsiusPerMetre, RankinePerMetre, FahrenheitPerMetre])#
    public TemperatureGradient3(Scalar x, Scalar y, Scalar z, UnitOfTemperatureGradient unitOfTemperatureGradient) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfTemperatureGradient) { }
    #Document:ConstructorVectorUnit(TemperatureGradient3, 3, UnitOfTemperatureGradient, unitOfTemperatureGradient, [KelvinPerMetre, CelsiusPerMetre, RankinePerMetre, FahrenheitPerMetre])#
    public TemperatureGradient3(Vector3 components, UnitOfTemperatureGradient unitOfTemperatureGradient) : 
    	this(components.X, components.Y, components.Z, unitOfTemperatureGradient) { }
    #Document:ConstructorDoubleTupleUnit(TemperatureGradient3, 3, UnitOfTemperatureGradient, unitOfTemperatureGradient, [KelvinPerMetre, CelsiusPerMetre, RankinePerMetre, FahrenheitPerMetre])#
    public TemperatureGradient3((double x, double y, double z) components, UnitOfTemperatureGradient unitOfTemperatureGradient) : 
    	this(components.x, components.y, components.z, unitOfTemperatureGradient) { }
    #Document:ConstructorDoublesTupleUnit(TemperatureGradient3, 3, UnitOfTemperatureGradient, unitOfTemperatureGradient, [KelvinPerMetre, CelsiusPerMetre, RankinePerMetre, FahrenheitPerMetre])#
    public TemperatureGradient3(double x, double y, double z, UnitOfTemperatureGradient unitOfTemperatureGradient) : 
    	this(x * unitOfTemperatureGradient.Factor, y * unitOfTemperatureGradient.Factor, z * unitOfTemperatureGradient.Factor) { }

    #Document:ConstructorComponentTuple(TemperatureGradient3, 3, UnitOfTemperatureGradient, unitOfTemperatureGradient, [KelvinPerMetre, CelsiusPerMetre, RankinePerMetre, FahrenheitPerMetre])#
    public TemperatureGradient3((TemperatureGradient x, TemperatureGradient y, TemperatureGradient z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(TemperatureGradient3, 3, UnitOfTemperatureGradient, unitOfTemperatureGradient, [KelvinPerMetre, CelsiusPerMetre, RankinePerMetre, FahrenheitPerMetre])#
    public TemperatureGradient3(TemperatureGradient x, TemperatureGradient y, TemperatureGradient z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(TemperatureGradient3, 3, UnitOfTemperatureGradient, unitOfTemperatureGradient, [KelvinPerMetre, CelsiusPerMetre, RankinePerMetre, FahrenheitPerMetre])#
    public TemperatureGradient3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(TemperatureGradient3, 3, UnitOfTemperatureGradient, unitOfTemperatureGradient, [KelvinPerMetre, CelsiusPerMetre, RankinePerMetre, FahrenheitPerMetre])#
    public TemperatureGradient3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(TemperatureGradient3, 3, UnitOfTemperatureGradient, unitOfTemperatureGradient, [KelvinPerMetre, CelsiusPerMetre, RankinePerMetre, FahrenheitPerMetre])#
    public TemperatureGradient3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(TemperatureGradient3, 3, UnitOfTemperatureGradient, unitOfTemperatureGradient, [KelvinPerMetre, CelsiusPerMetre, RankinePerMetre, FahrenheitPerMetre])#
    public TemperatureGradient3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(TemperatureGradient3, 3, UnitOfTemperatureGradient, unitOfTemperatureGradient, [KelvinPerMetre, CelsiusPerMetre, RankinePerMetre, FahrenheitPerMetre])#
    public TemperatureGradient3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    #Document:InUnit(quantity = TemperatureGradient3, dimensionality = 3, unit = UnitOfTemperatureGradient, unitName = KelvinPerMetre)#
    public Vector3 KelvinPerMetre => InUnit(UnitOfTemperatureGradient.KelvinPerMetre);
    #Document:InUnit(quantity = TemperatureGradient3, dimensionality = 3, unit = UnitOfTemperatureGradient, unitName = CelsiusPerMetre)#
    public Vector3 CelsiusPerMetre => InUnit(UnitOfTemperatureGradient.CelsiusPerMetre);
    #Document:InUnit(quantity = TemperatureGradient3, dimensionality = 3, unit = UnitOfTemperatureGradient, unitName = RankinePerMetre)#
    public Vector3 RankinePerMetre => InUnit(UnitOfTemperatureGradient.RankinePerMetre);
    #Document:InUnit(quantity = TemperatureGradient3, dimensionality = 3, unit = UnitOfTemperatureGradient, unitName = FahrenheitPerMetre)#
    public Vector3 FahrenheitPerMetre => InUnit(UnitOfTemperatureGradient.FahrenheitPerMetre);

    #Document:ScalarMagnitude(TemperatureGradient3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(TemperatureGradient3, 3, TemperatureGradient)#
    public TemperatureGradient Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(TemperatureGradient3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(TemperatureGradient3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(TemperatureGradient3, 3, undefined)#
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(TemperatureGradient3, 3)#
    public TemperatureGradient3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(TemperatureGradient3, 3)#
    public TemperatureGradient3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(TemperatureGradient3, 3)#
    public TemperatureGradient3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(TemperatureGradient3, 3)#
    public TemperatureGradient Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(TemperatureGradient3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(TemperatureGradient3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(TemperatureGradient3, 3)#
    public TemperatureGradient3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(TemperatureGradient3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(TemperatureGradient3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(TemperatureGradient3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [K / m]";

    #Document:InUnitInstance(TemperatureGradient3, 3, UnitOfTemperatureGradient, unitOfTemperatureGradient)#
    public Vector3 InUnit(UnitOfTemperatureGradient unitOfTemperatureGradient) => InUnit(this, unitOfTemperatureGradient);
    #Document:InUnitStatic(TemperatureGradient3, temperatureGradient3, 3, UnitOfTemperatureGradient, unitOfTemperatureGradient)#
    private static Vector3 InUnit(TemperatureGradient3 temperatureGradient3, UnitOfTemperatureGradient unitOfTemperatureGradient) => 
    	temperatureGradient3.ToVector3() / unitOfTemperatureGradient.Factor;
    
    #Document:PlusMethod(TemperatureGradient3, 3)#
    public TemperatureGradient3 Plus() => this;
    #Document:NegateMethod(TemperatureGradient3, 3)#
    public TemperatureGradient3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(TemperatureGradient3, 3)#
    public static TemperatureGradient3 operator +(TemperatureGradient3 a) => a;
    #Document:NegateOperator(TemperatureGradient3, 3)#
    public static TemperatureGradient3 operator -(TemperatureGradient3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(TemperatureGradient3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(TemperatureGradient3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(TemperatureGradient3, 3)#
    public static Unhandled3 operator *(TemperatureGradient3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(TemperatureGradient3, 3)#
    public static Unhandled3 operator *(Unhandled a, TemperatureGradient3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(TemperatureGradient3, 3)#
    public static Unhandled3 operator /(TemperatureGradient3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(TemperatureGradient3, 3)#
    public TemperatureGradient3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(TemperatureGradient3, 3)#
    public TemperatureGradient3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(TemperatureGradient3, 3)#
    public TemperatureGradient3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(TemperatureGradient3, 3)#
    public static TemperatureGradient3 operator %(TemperatureGradient3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(TemperatureGradient3, 3)#
    public static TemperatureGradient3 operator *(TemperatureGradient3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(TemperatureGradient3, 3)#
    public static TemperatureGradient3 operator *(double a, TemperatureGradient3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(TemperatureGradient3, 3)#
    public static TemperatureGradient3 operator /(TemperatureGradient3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(TemperatureGradient3, 3)#
    public TemperatureGradient3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(TemperatureGradient3, 3)#
    public TemperatureGradient3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(TemperatureGradient3, 3)#
    public TemperatureGradient3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(TemperatureGradient3, 3)#
    public static TemperatureGradient3 operator %(TemperatureGradient3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(TemperatureGradient3, 3)#
    public static TemperatureGradient3 operator *(TemperatureGradient3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(TemperatureGradient3, 3)#
    public static TemperatureGradient3 operator *(Scalar a, TemperatureGradient3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(TemperatureGradient3, 3)#
    public static TemperatureGradient3 operator /(TemperatureGradient3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(TemperatureGradient3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(TemperatureGradient3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(TemperatureGradient3, 3)#
    public static Unhandled3 operator *(TemperatureGradient3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(TemperatureGradient3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, TemperatureGradient3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(TemperatureGradient3, 3)#
    public static Unhandled3 operator /(TemperatureGradient3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(TemperatureGradient3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(TemperatureGradient3, 3)#
    public static implicit operator (double x, double y, double z)(TemperatureGradient3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(TemperatureGradient3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(TemperatureGradient3, 3)#
    public static explicit operator Vector3(TemperatureGradient3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(TemperatureGradient3, 3)#
    public static TemperatureGradient3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(TemperatureGradient3, 3)#
    public static explicit operator TemperatureGradient3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(TemperatureGradient3, 3)#
    public static TemperatureGradient3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(TemperatureGradient3, 3)#
    public static explicit operator TemperatureGradient3(Vector3 a) => new(a);
}
