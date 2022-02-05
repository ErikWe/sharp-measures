namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(Displacement3, 3, Length, UnitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile], [Femtometres, Picometres, Nanometres, Micrometres, Millimetres, Centimetres, Decimetres, Metres, Kilometres, AstronomicalUnits, Lightyears, Parsecs, Inches, Feet, Yards, Miles])#
public readonly partial record struct Displacement3 :
    IVector3Quantity,
    IScalableVector3Quantity<Displacement3>,
    INormalizableVector3Quantity<Displacement3>,
    ITransformableVector3Quantity<Displacement3>,
    IMultiplicableVector3Quantity<Displacement3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Displacement3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<Displacement3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(Displacement3, 3)#
    public static Displacement3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(Displacement3, 3)#
    public double X { get; init; }
    #Document:ComponentY(Displacement3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(Displacement3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(Displacement3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Displacement3((Length x, Length y, Length z) components, UnitOfLength unitOfLength) : this(components.x, components.y, components.z, unitOfLength) { }
    #Document:ConstructorComponentsUnit(Displacement3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Displacement3(Length x, Length y, Length z, UnitOfLength unitOfLength) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfLength) { }
    #Document:ConstructorScalarTupleUnit(Displacement3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Displacement3((Scalar x, Scalar y, Scalar z) components, UnitOfLength unitOfLength) : this(components.x, components.y, components.z, unitOfLength) { }
    #Document:ConstructorScalarsUnit(Displacement3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Displacement3(Scalar x, Scalar y, Scalar z, UnitOfLength unitOfLength) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfLength) { }
    #Document:ConstructorVectorUnit(Displacement3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Displacement3(Vector3 components, UnitOfLength unitOfLength) : this(components.X, components.Y, components.Z, unitOfLength) { }
    #Document:ConstructorDoubleTupleUnit(Displacement3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Displacement3((double x, double y, double z) components, UnitOfLength unitOfLength) : this(components.x, components.y, components.z, unitOfLength) { }
    #Document:ConstructorDoublesTupleUnit(Displacement3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Displacement3(double x, double y, double z, UnitOfLength unitOfLength) : this(x * unitOfLength.Factor, y * unitOfLength.Factor, z * unitOfLength.Factor) { }

    #Document:ConstructorComponentTuple(Displacement3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Displacement3((Length x, Length y, Length z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(Displacement3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Displacement3(Length x, Length y, Length z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(Displacement3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Displacement3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(Displacement3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Displacement3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(Displacement3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Displacement3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(Displacement3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Displacement3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(Displacement3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Displacement3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

));

    #Document:InUnit(quantity = Displacement3, dimensionality = 3, unit = UnitOfLength, unitName = Femtometre)#
    public Vector3 Femtometres => InUnit(UnitOfLength.Femtometre);
    #Document:InUnit(quantity = Displacement3, dimensionality = 3, unit = UnitOfLength, unitName = Picometre)#
    public Vector3 Picometres => InUnit(UnitOfLength.Picometre);
    #Document:InUnit(quantity = Displacement3, dimensionality = 3, unit = UnitOfLength, unitName = Nanometre)#
    public Vector3 Nanometres => InUnit(UnitOfLength.Nanometre);
    #Document:InUnit(quantity = Displacement3, dimensionality = 3, unit = UnitOfLength, unitName = Micrometre)#
    public Vector3 Micrometres => InUnit(UnitOfLength.Micrometre);
    #Document:InUnit(quantity = Displacement3, dimensionality = 3, unit = UnitOfLength, unitName = Millimetre)#
    public Vector3 Millimetres => InUnit(UnitOfLength.Millimetre);
    #Document:InUnit(quantity = Displacement3, dimensionality = 3, unit = UnitOfLength, unitName = Centimetre)#
    public Vector3 Centimetres => InUnit(UnitOfLength.Centimetre);
    #Document:InUnit(quantity = Displacement3, dimensionality = 3, unit = UnitOfLength, unitName = Decimetre)#
    public Vector3 Decimetres => InUnit(UnitOfLength.Decimetre);
    #Document:InUnit(quantity = Displacement3, dimensionality = 3, unit = UnitOfLength, unitName = Metre)#
    public Vector3 Metres => InUnit(UnitOfLength.Metre);
    #Document:InUnit(quantity = Displacement3, dimensionality = 3, unit = UnitOfLength, unitName = Kilometre)#
    public Vector3 Kilometres => InUnit(UnitOfLength.Kilometre);

    #Document:InUnit(quantity = Displacement3, dimensionality = 3, unit = UnitOfLength, unitName = AstronomicalUnit)#
    public Vector3 AstronomicalUnits => InUnit(UnitOfLength.AstronomicalUnit);
    #Document:InUnit(quantity = Displacement3, dimensionality = 3, unit = UnitOfLength, unitName = Lightyear)#
    public Vector3 Lightyears => InUnit(UnitOfLength.Lightyear);
    #Document:InUnit(quantity = Displacement3, dimensionality = 3, unit = UnitOfLength, unitName = Parsec)#
    public Vector3 Parsecs => InUnit(UnitOfLength.Parsec);

    #Document:InUnit(quantity = Displacement3, dimensionality = 3, unit = UnitOfLength, unitName = Inch)#
    public Vector3 Inches => InUnit(UnitOfLength.Inch);
    #Document:InUnit(quantity = Displacement3, dimensionality = 3, unit = UnitOfLength, unitName = Foot)#
    public Vector3 Feet => InUnit(UnitOfLength.Foot);
    #Document:InUnit(quantity = Displacement3, dimensionality = 3, unit = UnitOfLength, unitName = Yard)#
    public Vector3 Yards => InUnit(UnitOfLength.Yard);
    #Document:InUnit(quantity = Displacement3, dimensionality = 3, unit = UnitOfLength, unitName = Mile)#
    public Vector3 Miles => InUnit(UnitOfLength.Mile);

    #Document:ScalarMagnitude(Displacement3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(Displacement3, 3, Length)#
    public Length Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(Displacement3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(Displacement3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(Displacement3, 3, Area)#
    public Area SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(Displacement3, 3)#
    public Displacement3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(Displacement3, 3)#
    public Displacement3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(Displacement3, 3)#
    public Displacement3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(Displacement3, 3)#
    public Length Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(Displacement3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(Displacement3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(Displacement3, 3)#
    public Displacement3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(Displacement3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(Displacement3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(Displacement3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [m]";

    #Document:InUnitInstance(Displacement3, 3, UnitOfLength, unitOfLength)#
    public Vector3 InUnit(UnitOfLength unitOfLength) => InUnit(this, unitOfLength);
    #Document:InUnitStatic(Displacement3, displacement3, 3, UnitOfLength, unitOfLength)#
    private static Vector3 InUnit(Displacement3 displacement3, UnitOfLength unitOfLength) => displacement3.ToVector3() / unitOfLength.Factor;
    
    #Document:PlusMethod(Displacement3, 3)#
    public Displacement3 Plus() => this;
    #Document:NegateMethod(Displacement3, 3)#
    public Displacement3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(Displacement3, 3)#
    public static Displacement3 operator +(Displacement3 a) => a;
    #Document:NegateOperator(Displacement3, 3)#
    public static Displacement3 operator -(Displacement3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(Displacement3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(Displacement3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(Displacement3, 3)#
    public static Unhandled3 operator *(Displacement3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(Displacement3, 3)#
    public static Unhandled3 operator *(Unhandled a, Displacement3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(Displacement3, 3)#
    public static Unhandled3 operator /(Displacement3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(Displacement3, 3)#
    public Displacement3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(Displacement3, 3)#
    public Displacement3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(Displacement3, 3)#
    public Displacement3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(Displacement3, 3)#
    public static Displacement3 operator %(Displacement3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(Displacement3, 3)#
    public static Displacement3 operator *(Displacement3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(Displacement3, 3)#
    public static Displacement3 operator *(double a, Displacement3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(Displacement3, 3)#
    public static Displacement3 operator /(Displacement3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(Displacement3, 3)#
    public Displacement3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(Displacement3, 3)#
    public Displacement3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(Displacement3, 3)#
    public Displacement3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(Displacement3, 3)#
    public static Displacement3 operator %(Displacement3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(Displacement3, 3)#
    public static Displacement3 operator *(Displacement3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(Displacement3, 3)#
    public static Displacement3 operator *(Scalar a, Displacement3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(Displacement3, 3)#
    public static Displacement3 operator /(Displacement3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(Displacement3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(Displacement3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(Displacement3, 3)#
    public static Unhandled3 operator *(Displacement3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(Displacement3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, Displacement3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(Displacement3, 3)#
    public static Unhandled3 operator /(Displacement3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(Displacement3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(Displacement3, 3)#
    public static implicit operator (double x, double y, double z)(Displacement3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(Displacement3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(Displacement3, 3)#
    public static explicit operator Vector3(Displacement3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(Displacement3, 3)#
    public static Displacement3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(Displacement3, 3)#
    public static explicit operator Displacement3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(Displacement3, 3)#
    public static Displacement3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(Displacement3, 3)#
    public static explicit operator Displacement3(Vector3 a) => new(a);
}
