namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(Size3, 3, Length, UnitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile], [Femtometres, Picometres, Nanometres, Micrometres, Millimetres, Centimetres, Decimetres, Metres, Kilometres, AstronomicalUnits, Lightyears, Parsecs, Inches, Feet, Yards, Miles])#
public readonly partial record struct Size3 :
    IVector3Quantity,
    IScalableVector3Quantity<Size3>,
    INormalizableVector3Quantity<Size3>,
    ITransformableVector3Quantity<Size3>,
    IMultiplicableVector3Quantity<Size3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Size3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<Size3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(Size3, 3)#
    public static Size3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(Size3, 3)#
    public double X { get; init; }
    #Document:ComponentY(Size3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(Size3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(Size3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Size3((Length x, Length y, Length z) components, UnitOfLength unitOfLength) : this(components.x, components.y, components.z, unitOfLength) { }
    #Document:ConstructorComponentsUnit(Size3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Size3(Length x, Length y, Length z, UnitOfLength unitOfLength) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfLength) { }
    #Document:ConstructorScalarTupleUnit(Size3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Size3((Scalar x, Scalar y, Scalar z) components, UnitOfLength unitOfLength) : this(components.x, components.y, components.z, unitOfLength) { }
    #Document:ConstructorScalarsUnit(Size3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Size3(Scalar x, Scalar y, Scalar z, UnitOfLength unitOfLength) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfLength) { }
    #Document:ConstructorVectorUnit(Size3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Size3(Vector3 components, UnitOfLength unitOfLength) : this(components.X, components.Y, components.Z, unitOfLength) { }
    #Document:ConstructorDoubleTupleUnit(Size3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Size3((double x, double y, double z) components, UnitOfLength unitOfLength) : this(components.x, components.y, components.z, unitOfLength) { }
    #Document:ConstructorDoublesTupleUnit(Size3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Size3(double x, double y, double z, UnitOfLength unitOfLength) : this(x * unitOfLength.Factor, y * unitOfLength.Factor, z * unitOfLength.Factor) { }

    #Document:ConstructorComponentTuple(Size3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Size3((Length x, Length y, Length z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(Size3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Size3(Length x, Length y, Length z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(Size3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Size3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(Size3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Size3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(Size3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Size3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(Size3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Size3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(Size3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Size3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

));

    #Document:InUnit(quantity = Size3, dimensionality = 3, unit = UnitOfLength, unitName = Femtometre)#
    public Vector3 Femtometres => InUnit(UnitOfLength.Femtometre);
    #Document:InUnit(quantity = Size3, dimensionality = 3, unit = UnitOfLength, unitName = Picometre)#
    public Vector3 Picometres => InUnit(UnitOfLength.Picometre);
    #Document:InUnit(quantity = Size3, dimensionality = 3, unit = UnitOfLength, unitName = Nanometre)#
    public Vector3 Nanometres => InUnit(UnitOfLength.Nanometre);
    #Document:InUnit(quantity = Size3, dimensionality = 3, unit = UnitOfLength, unitName = Micrometre)#
    public Vector3 Micrometres => InUnit(UnitOfLength.Micrometre);
    #Document:InUnit(quantity = Size3, dimensionality = 3, unit = UnitOfLength, unitName = Millimetre)#
    public Vector3 Millimetres => InUnit(UnitOfLength.Millimetre);
    #Document:InUnit(quantity = Size3, dimensionality = 3, unit = UnitOfLength, unitName = Centimetre)#
    public Vector3 Centimetres => InUnit(UnitOfLength.Centimetre);
    #Document:InUnit(quantity = Size3, dimensionality = 3, unit = UnitOfLength, unitName = Decimetre)#
    public Vector3 Decimetres => InUnit(UnitOfLength.Decimetre);
    #Document:InUnit(quantity = Size3, dimensionality = 3, unit = UnitOfLength, unitName = Metre)#
    public Vector3 Metres => InUnit(UnitOfLength.Metre);
    #Document:InUnit(quantity = Size3, dimensionality = 3, unit = UnitOfLength, unitName = Kilometre)#
    public Vector3 Kilometres => InUnit(UnitOfLength.Kilometre);

    #Document:InUnit(quantity = Size3, dimensionality = 3, unit = UnitOfLength, unitName = AstronomicalUnit)#
    public Vector3 AstronomicalUnits => InUnit(UnitOfLength.AstronomicalUnit);
    #Document:InUnit(quantity = Size3, dimensionality = 3, unit = UnitOfLength, unitName = Lightyear)#
    public Vector3 Lightyears => InUnit(UnitOfLength.Lightyear);
    #Document:InUnit(quantity = Size3, dimensionality = 3, unit = UnitOfLength, unitName = Parsec)#
    public Vector3 Parsecs => InUnit(UnitOfLength.Parsec);

    #Document:InUnit(quantity = Size3, dimensionality = 3, unit = UnitOfLength, unitName = Inch)#
    public Vector3 Inches => InUnit(UnitOfLength.Inch);
    #Document:InUnit(quantity = Size3, dimensionality = 3, unit = UnitOfLength, unitName = Foot)#
    public Vector3 Feet => InUnit(UnitOfLength.Foot);
    #Document:InUnit(quantity = Size3, dimensionality = 3, unit = UnitOfLength, unitName = Yard)#
    public Vector3 Yards => InUnit(UnitOfLength.Yard);
    #Document:InUnit(quantity = Size3, dimensionality = 3, unit = UnitOfLength, unitName = Mile)#
    public Vector3 Miles => InUnit(UnitOfLength.Mile);

    #Document:ScalarMagnitude(Size3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(Size3, 3, Length)#
    public Length Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(Size3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(Size3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(Size3, 3, Area)#
    public Area SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(Size3, 3)#
    public Size3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(Size3, 3)#
    public Size3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(Size3, 3)#
    public Size3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(Size3, 3)#
    public Length Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(Size3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(Size3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(Size3, 3)#
    public Size3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(Size3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(Size3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(Size3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [m]";

    #Document:InUnitInstance(Size3, 3, UnitOfLength, unitOfLength)#
    public Vector3 InUnit(UnitOfLength unitOfLength) => InUnit(this, unitOfLength);
    #Document:InUnitStatic(Size3, size3, 3, UnitOfLength, unitOfLength)#
    private static Vector3 InUnit(Size3 size3, UnitOfLength unitOfLength) => size3.ToVector3() / unitOfLength.Factor;
    
    #Document:PlusMethod(Size3, 3)#
    public Size3 Plus() => this;
    #Document:NegateMethod(Size3, 3)#
    public Size3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(Size3, 3)#
    public static Size3 operator +(Size3 a) => a;
    #Document:NegateOperator(Size3, 3)#
    public static Size3 operator -(Size3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(Size3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(Size3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(Size3, 3)#
    public static Unhandled3 operator *(Size3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(Size3, 3)#
    public static Unhandled3 operator *(Unhandled a, Size3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(Size3, 3)#
    public static Unhandled3 operator /(Size3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(Size3, 3)#
    public Size3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(Size3, 3)#
    public Size3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(Size3, 3)#
    public Size3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(Size3, 3)#
    public static Size3 operator %(Size3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(Size3, 3)#
    public static Size3 operator *(Size3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(Size3, 3)#
    public static Size3 operator *(double a, Size3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(Size3, 3)#
    public static Size3 operator /(Size3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(Size3, 3)#
    public Size3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(Size3, 3)#
    public Size3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(Size3, 3)#
    public Size3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(Size3, 3)#
    public static Size3 operator %(Size3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(Size3, 3)#
    public static Size3 operator *(Size3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(Size3, 3)#
    public static Size3 operator *(Scalar a, Size3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(Size3, 3)#
    public static Size3 operator /(Size3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(Size3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(Size3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(Size3, 3)#
    public static Unhandled3 operator *(Size3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(Size3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, Size3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(Size3, 3)#
    public static Unhandled3 operator /(Size3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(Size3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(Size3, 3)#
    public static implicit operator (double x, double y, double z)(Size3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(Size3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(Size3, 3)#
    public static explicit operator Vector3(Size3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(Size3, 3)#
    public static Size3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(Size3, 3)#
    public static explicit operator Size3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(Size3, 3)#
    public static Size3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(Size3, 3)#
    public static explicit operator Size3(Vector3 a) => new(a);
}
