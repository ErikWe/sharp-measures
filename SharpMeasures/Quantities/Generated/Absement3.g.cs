namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System.Numerics;

/// <summary>A measure of the vector quantity <see cref="Absement3"/>, of dimensionality three, describing sustained displacement
/// - <see cref="Displacement3"/> for some <see cref="Time"/>. The quantity is expressed in <see cref="UnitOfAbsement"/>, with the SI unit being [m * s].
/// <para>
/// New instances of <see cref="Absement3"/> can be constructed by multiplying a <see cref="Absement"/> with a Vector3 or <see cref="System.ValueTuple"/>.
/// Instances can also be produced by combining other quantities, either through mathematical operators or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Absement3"/> a = (3, 5, 7) * <see cref="Absement.OneMetreSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Absement3"/> d = <see cref="Absement3.From(Displacement3, Time)"/>;
/// </code>
/// </item>
/// </list>
/// The components of the measure can be retrieved as a <see cref="Vector3"/> using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfAbsement"/>.
/// </para>
/// </summary>
public readonly partial record struct Absement3 :
    IVector3Quantity,
    IScalableVector3Quantity<Absement3>,
    INormalizableVector3Quantity<Absement3>,
    ITransformableVector3Quantity<Absement3>,
    IMultiplicableVector3Quantity<Absement3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Absement3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<Absement3, Scalar>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    /// <summary>The zero-magnitude <see cref="Absement3"/>.</summary>
    public static Absement3 Zero { get; } = new(0, 0, 0);

    /// <summary>X-component of the <see cref="Absement3"/>.</summary>
    public double X { get; init; }
    /// <summary>Y-component of the <see cref="Absement3"/></summary>
    public double Y { get; init; }
    /// <summary>Z-component of the <see cref="Absement3"/></summary>
    public double Z { get; init; }

    UnresolvedTagError
    public Absement3((Absement x, Absement y, Absement z) components, UnitOfAbsement unitOfAbsement) : this(components.x, components.y, components.z, unitOfAbsement) { }
    UnresolvedTagError
    public Absement3(Absement x, Absement y, Absement z, UnitOfAbsement unitOfAbsement) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAbsement) { }
    UnresolvedTagError
    public Absement3((Scalar x, Scalar y, Scalar z) components, UnitOfAbsement unitOfAbsement) : this(components.x, components.y, components.z, unitOfAbsement) { }
    UnresolvedTagError
    public Absement3(Scalar x, Scalar y, Scalar z, UnitOfAbsement unitOfAbsement) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAbsement) { }
    UnresolvedTagError
    public Absement3(Vector3 components, UnitOfAbsement unitOfAbsement) : this(components.X, components.Y, components.Z, unitOfAbsement) { }
    UnresolvedTagError
    public Absement3((double x, double y, double z) components, UnitOfAbsement unitOfAbsement) : this(components.x, components.y, components.z, unitOfAbsement) { }
    UnresolvedTagError
    public Absement3(double x, double y, double z, UnitOfAbsement unitOfAbsement) : this(x * unitOfAbsement.Factor, y * unitOfAbsement.Factor, z * unitOfAbsement.Factor) { }

    UnresolvedTagError
    public Absement3((Absement x, Absement y, Absement z) components) : this(components.x, components.y, components.z) { }
    UnresolvedTagError
    public Absement3(Absement x, Absement y, Absement z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    UnresolvedTagError
    public Absement3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    UnresolvedTagError
    public Absement3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    UnresolvedTagError
    public Absement3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    UnresolvedTagError
    public Absement3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    UnresolvedTagError
    public Absement3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    UnresolvedTagError
    public Vector3 MetreSeconds => InUnit(UnitOfAbsement.MetreSecond);

    UnresolvedTagError
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    UnresolvedTagError
    public Absement Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
#NonScalarQuantityComponent#
    UnresolvedTagError
    public Scalar Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
#/NonScalarQuantityComponent#
    UnresolvedTagError
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    UnresolvedTagError
    public undefined SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    UnresolvedTagError
    public Absement3 Normalize() => this / Magnitude().Magnitude;
#NonScalarQuantityComponent#
    UnresolvedTagError
    public Absement3 Normalize() => this / Magnitude();
#/NonScalarQuantityComponent#
    UnresolvedTagError
    public Absement3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    UnresolvedTagError
    public Absement Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    UnresolvedTagError
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    UnresolvedTagError
    public Unhandled Dot<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Dot(this, factor));
    UnresolvedTagError
    public Absement3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    UnresolvedTagError
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    UnresolvedTagError
    public Unhandled3 Cross<TVector3Quantity>(TVector3Quantity factor) where TVector3Quantity : IVector3Quantity => new(Maths.Vectors.Cross(this, factor));

    UnresolvedTagError
    public override string ToString() => $"({X}, {Y}, {Z}) [m * s]";

    UnresolvedTagError
    public Vector3 InUnit(UnitOfAbsement unitOfAbsement) => InUnit(this, unitOfAbsement);
    UnresolvedTagError
    private static Vector3 InUnit(Absement3 absement3, UnitOfAbsement unitOfAbsement) => absement3.ToVector3() / unitOfAbsement.Factor;
    
    UnresolvedTagError
    public Absement3 Plus() => this;
    UnresolvedTagError
    public Absement3 Negate() => new(-X, -Y, -Z);
    UnresolvedTagError
    public static Absement3 operator +(Absement3 a) => a;
    UnresolvedTagError
    public static Absement3 operator -(Absement3 a) => new(-a.X, -a.Y, -a.Z);

    UnresolvedTagError
    public Unhandled3 Multiply(Unhandled factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    UnresolvedTagError
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    UnresolvedTagError
    public static Unhandled3 operator *(Absement3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    UnresolvedTagError
    public static Unhandled3 operator *(Unhandled a, Absement3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    UnresolvedTagError
    public static Unhandled3 operator /(Absement3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    UnresolvedTagError
    public Absement3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    UnresolvedTagError
    public Absement3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    UnresolvedTagError
    public Absement3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    UnresolvedTagError
    public static Absement3 operator %(Absement3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    UnresolvedTagError
    public static Absement3 operator *(Absement3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    UnresolvedTagError
    public static Absement3 operator *(double a, Absement3 b) => new(a * b.X, a * b.Y, a * b.Z);
    UnresolvedTagError
    public static Absement3 operator /(Absement3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    UnresolvedTagError
    public Absement3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    UnresolvedTagError
    public Absement3 Multiply(Scalar factor) => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    UnresolvedTagError
    public Absement3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    UnresolvedTagError
    public static Absement3 operator %(Absement3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    UnresolvedTagError
    public static Absement3 operator *(Absement3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    UnresolvedTagError
    public static Absement3 operator *(Scalar a, Absement3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    UnresolvedTagError
    public static Absement3 operator /(Absement3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    UnresolvedTagError
    public Unhandled3 Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(XX * factor.Magnitude, YY * factor.Magnitude, ZZ * factor.Magnitude);
    UnresolvedTagError
    public Unhandled3 Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    UnresolvedTagError
    public static Unhandled3 operator *(Absement3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    UnresolvedTagError
    public static Unhandled3 operator *(IScalarQuantity a, Absement3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    UnresolvedTagError
    public static Unhandled3 operator /(Absement3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    UnresolvedTagError
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    UnresolvedTagError
    public static implicit operator (double x, double y, double z)(Absement3 a) => (a.X, a.Y, a.Z);

    UnresolvedTagError
    public Vector3 ToVector3() => new(X, Y, Z);
    UnresolvedTagError
    public static explicit operator Vector3(Absement3 a) => new(a.X, a.Y, a.Z);

    UnresolvedTagError
    public static Absement3 FromValueTuple((double x, double y, double z) components) => new(components);
    UnresolvedTagError
    public static explicit operator Absement3((double x, double y, double z) components) => new(components);

    UnresolvedTagError
    public static Absement3 FromVector3(Vector3 a) => new(a);
    UnresolvedTagError
    public static explicit operator Absement3(Vector3 a) => new(a);
}
