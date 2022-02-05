namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

#Document:Header(Position3, 3, Length, UnitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile], [Femtometres, Picometres, Nanometres, Micrometres, Millimetres, Centimetres, Decimetres, Metres, Kilometres, AstronomicalUnits, Lightyears, Parsecs, Inches, Feet, Yards, Miles])#
public readonly partial record struct Position3 :
    IVector3Quantity,
    IScalableVector3Quantity<Position3>,
    INormalizableVector3Quantity<Position3>,
    ITransformableVector3Quantity<Position3>,
    IMultiplicableVector3Quantity<Position3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Position3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<Position3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    #Document:Zero(Position3, 3)#
    public static Position3 Zero { get; } = new(0, 0, 0);

    #Document:ComponentX(Position3, 3)#
    public double X { get; init; }
    #Document:ComponentY(Position3, 3)#
    public double Y { get; init; }
    #Document:ComponentZ(Position3, 3)#
    public double Z { get; init; }

    #Document:ConstructorComponentTupleUnit(Position3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Position3((Length x, Length y, Length z) components, UnitOfLength unitOfLength) : this(components.x, components.y, components.z, unitOfLength) { }
    #Document:ConstructorComponentsUnit(Position3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Position3(Length x, Length y, Length z, UnitOfLength unitOfLength) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfLength) { }
    #Document:ConstructorScalarTupleUnit(Position3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Position3((Scalar x, Scalar y, Scalar z) components, UnitOfLength unitOfLength) : this(components.x, components.y, components.z, unitOfLength) { }
    #Document:ConstructorScalarsUnit(Position3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Position3(Scalar x, Scalar y, Scalar z, UnitOfLength unitOfLength) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfLength) { }
    #Document:ConstructorVectorUnit(Position3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Position3(Vector3 components, UnitOfLength unitOfLength) : this(components.X, components.Y, components.Z, unitOfLength) { }
    #Document:ConstructorDoubleTupleUnit(Position3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Position3((double x, double y, double z) components, UnitOfLength unitOfLength) : this(components.x, components.y, components.z, unitOfLength) { }
    #Document:ConstructorDoublesTupleUnit(Position3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Position3(double x, double y, double z, UnitOfLength unitOfLength) : this(x * unitOfLength.Factor, y * unitOfLength.Factor, z * unitOfLength.Factor) { }

    #Document:ConstructorComponentTuple(Position3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Position3((Length x, Length y, Length z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorComponents(Position3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Position3(Length x, Length y, Length z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorScalarTuple(Position3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Position3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorScalars(Position3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Position3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    #Document:ConstructorVector(Position3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Position3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    #Document:ConstructorDoubleTuple(Position3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Position3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    #Document:ConstructorDoubles(Position3, 3, UnitOfLength, unitOfLength, [Femtometre, Picometre, Nanometre, Micrometre, Millimetre, Centimetre, Decimetre, Metre, Kilometre, AstronomicalUnit, Lightyear, Parsec, Inch, Foot, Yard, Mile])#
    public Position3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

));

    #Document:InUnit(quantity = Position3, dimensionality = 3, unit = UnitOfLength, unitName = Femtometre)#
    public Vector3 Femtometres => InUnit(UnitOfLength.Femtometre);
    #Document:InUnit(quantity = Position3, dimensionality = 3, unit = UnitOfLength, unitName = Picometre)#
    public Vector3 Picometres => InUnit(UnitOfLength.Picometre);
    #Document:InUnit(quantity = Position3, dimensionality = 3, unit = UnitOfLength, unitName = Nanometre)#
    public Vector3 Nanometres => InUnit(UnitOfLength.Nanometre);
    #Document:InUnit(quantity = Position3, dimensionality = 3, unit = UnitOfLength, unitName = Micrometre)#
    public Vector3 Micrometres => InUnit(UnitOfLength.Micrometre);
    #Document:InUnit(quantity = Position3, dimensionality = 3, unit = UnitOfLength, unitName = Millimetre)#
    public Vector3 Millimetres => InUnit(UnitOfLength.Millimetre);
    #Document:InUnit(quantity = Position3, dimensionality = 3, unit = UnitOfLength, unitName = Centimetre)#
    public Vector3 Centimetres => InUnit(UnitOfLength.Centimetre);
    #Document:InUnit(quantity = Position3, dimensionality = 3, unit = UnitOfLength, unitName = Decimetre)#
    public Vector3 Decimetres => InUnit(UnitOfLength.Decimetre);
    #Document:InUnit(quantity = Position3, dimensionality = 3, unit = UnitOfLength, unitName = Metre)#
    public Vector3 Metres => InUnit(UnitOfLength.Metre);
    #Document:InUnit(quantity = Position3, dimensionality = 3, unit = UnitOfLength, unitName = Kilometre)#
    public Vector3 Kilometres => InUnit(UnitOfLength.Kilometre);

    #Document:InUnit(quantity = Position3, dimensionality = 3, unit = UnitOfLength, unitName = AstronomicalUnit)#
    public Vector3 AstronomicalUnits => InUnit(UnitOfLength.AstronomicalUnit);
    #Document:InUnit(quantity = Position3, dimensionality = 3, unit = UnitOfLength, unitName = Lightyear)#
    public Vector3 Lightyears => InUnit(UnitOfLength.Lightyear);
    #Document:InUnit(quantity = Position3, dimensionality = 3, unit = UnitOfLength, unitName = Parsec)#
    public Vector3 Parsecs => InUnit(UnitOfLength.Parsec);

    #Document:InUnit(quantity = Position3, dimensionality = 3, unit = UnitOfLength, unitName = Inch)#
    public Vector3 Inches => InUnit(UnitOfLength.Inch);
    #Document:InUnit(quantity = Position3, dimensionality = 3, unit = UnitOfLength, unitName = Foot)#
    public Vector3 Feet => InUnit(UnitOfLength.Foot);
    #Document:InUnit(quantity = Position3, dimensionality = 3, unit = UnitOfLength, unitName = Yard)#
    public Vector3 Yards => InUnit(UnitOfLength.Yard);
    #Document:InUnit(quantity = Position3, dimensionality = 3, unit = UnitOfLength, unitName = Mile)#
    public Vector3 Miles => InUnit(UnitOfLength.Mile);

    #Document:ScalarMagnitude(Position3, 3)#
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    #Document:ScalarQuantityMagnitude(Position3, 3, Length)#
    public Length Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    #Document:ScalarMagnitude(Position3, 3)#
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    #Document:ScalarSquaredMagnitude(Position3, 3)#
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    #Document:ScalarQuantitySquaredMagnitude(Position3, 3, Area)#
    public Area SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    #Document:Normalize(Position3, 3)#
    public Position3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    #Document:Normalize(Position3, 3)#
    public Position3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    #Document:Transform(Position3, 3)#
    public Position3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    #Document:DotVector(Position3, 3)#
    public Length Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotUnhandled(Position3, 3)#
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    #Document:DotGeneric(Position3, 3)#
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    #Document:CrossVector(Position3, 3)#
    public Position3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossUnhandled(Position3, 3)#
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    #Document:CrossGeneric(Position3, 3)#
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    #Document:ToString(Position3, 3)#
    public override string ToString() => $"({X}, {Y}, {Z}) [m]";

    #Document:InUnitInstance(Position3, 3, UnitOfLength, unitOfLength)#
    public Vector3 InUnit(UnitOfLength unitOfLength) => InUnit(this, unitOfLength);
    #Document:InUnitStatic(Position3, position3, 3, UnitOfLength, unitOfLength)#
    private static Vector3 InUnit(Position3 position3, UnitOfLength unitOfLength) => position3.ToVector3() / unitOfLength.Factor;
    
    #Document:PlusMethod(Position3, 3)#
    public Position3 Plus() => this;
    #Document:NegateMethod(Position3, 3)#
    public Position3 Negate() => new(-X, -Y, -Z);
    #Document:PlusOperator(Position3, 3)#
    public static Position3 operator +(Position3 a) => a;
    #Document:NegateOperator(Position3, 3)#
    public static Position3 operator -(Position3 a) => new(-a.X, -a.Y, -a.Z);

    #Document:MultiplyUnhandledMethod(Position3, 3)#
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideUnhandledMethod(Position3, 3)#
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyUnhandledOperatorLHS(Position3, 3)#
    public static Unhandled3 operator *(Position3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyUnhandledOperatorRHS(Position3, 3)#
    public static Unhandled3 operator *(Unhandled a, Position3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideUnhandledOperator(Position3, 3)#
    public static Unhandled3 operator /(Position3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:RemainderDoubleMethod(Position3, 3)#
    public Position3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    #Document:MultiplyDoubleMethod(Position3, 3)#
    public Position3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    #Document:DivideDoubleMethod(Position3, 3)#
    public Position3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    #Document:RemainderDoubleOperator(Position3, 3)#
    public static Position3 operator %(Position3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    #Document:MultiplyDoubleOperatorLHS(Position3, 3)#
    public static Position3 operator *(Position3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    #Document:MultiplyDoubleOperatorRHS(Position3, 3)#
    public static Position3 operator *(double a, Position3 b) => new(a * b.X, a * b.Y, a * b.Z);
    #Document:DivideDoubleOperator(Position3, 3)#
    public static Position3 operator /(Position3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    #Document:RemainderScalarMethod(Position3, 3)#
    public Position3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    #Document:MultiplyScalarMethod(Position3, 3)#
    public Position3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideScalarMethod(Position3, 3)#
    public Position3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:RemainderScalarOperator(Position3, 3)#
    public static Position3 operator %(Position3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    #Document:MultiplyScalarOperatorLHS(Position3, 3)#
    public static Position3 operator *(Position3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyScalarOperatorRHS(Position3, 3)#
    public static Position3 operator *(Scalar a, Position3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideScalarOperator(Position3, 3)#
    public static Position3 operator /(Position3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:MultiplyTScalar(Position3, 3)#
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    #Document:DivideTScalar(Position3, 3)#
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    #Document:MultiplyIScalarLHS(Position3, 3)#
    public static Unhandled3 operator *(Position3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    #Document:MultiplyIScalarRHS(Position3, 3)#
    public static Unhandled3 operator *(IScalarQuantity a, Position3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    #Document:DivideIScalar(Position3, 3)#
    public static Unhandled3 operator /(Position3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    #Document:ToValueTupleMethod(Position3, 3)#
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    #Document:ToValueTupleOperator(Position3, 3)#
    public static implicit operator (double x, double y, double z)(Position3 a) => (a.X, a.Y, a.Z);

    #Document:ToVectorMethod(Position3, 3)#
    public Vector3 ToVector3() => new(X, Y, Z);
    #Document:ToVectorOperator(Position3, 3)#
    public static explicit operator Vector3(Position3 a) => new(a.X, a.Y, a.Z);

    #Document:FromValueTupleMethod(Position3, 3)#
    public static Position3 FromValueTuple((double x, double y, double z) components) => new(components);
    #Document:FromValueTupleOperator(Position3, 3)#
    public static explicit operator Position3((double x, double y, double z) components) => new(components);

    #Document:FromVectorMethod(Position3, 3)#
    public static Position3 FromVector3(Vector3 a) => new(a);
    #Document:FromVectorOperator(Position3, 3)#
    public static explicit operator Position3(Vector3 a) => new(a);
}
