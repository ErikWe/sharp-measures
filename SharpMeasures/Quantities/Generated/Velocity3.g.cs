namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

/// <summary>A measure of the vector quantity <see cref="Velocity3"/>, of dimensionality three,
/// describing change in <see cref="Position3"/>, or <see cref="Displacement3"/>, over <see cref="Time"/>. The quantity is expressed
/// in <see cref="UnitOfVelocity"/>, with the SI unit being [m / s].
/// <para>
/// New instances of <see cref="Velocity3"/> can be constructed by multiplying a <see cref="Speed"/> with a <see cref="Vector3"/> or (double, double, double).
/// Instances can also be produced by combining other quantities, either through mathematical operators or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Velocity3"/> a = (3, 5, 7) * <see cref="Speed.OneMetrePerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Velocity3"/> d = <see cref="Velocity3.From(Displacement3, Time)"/>;
/// </code>
/// </item>
/// </list>
/// The components of the measure can be retrieved as a <see cref="Vector3"/> using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfVelocity"/>.
/// </para>
/// </summary>
public readonly partial record struct Velocity3 :
    IVector3Quantity,
    IScalableVector3Quantity<Velocity3>,
    INormalizableVector3Quantity<Velocity3>,
    ITransformableVector3Quantity<Velocity3>,
    IMultiplicableVector3Quantity<Velocity3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Velocity3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<Speed, Vector3>,
    IDotableVector3Quantity<Unhandled, Unhandled3>,
    ICrossableVector3Quantity<Velocity3, Vector3>,
    ICrossableVector3Quantity<Unhandled3, Unhandled3>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    /// <summary>The zero-magnitude <see cref="Velocity3"/>.</summary>
    public static Velocity3 Zero { get; } = new(0, 0, 0);

    /// <summary>The magnitude of the X-component of the <see cref="Velocity3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfVelocity)"/> or a pre-defined property
    /// - such as <see cref="MetresPerSecond"/>.</remarks>
    public double X { get; init; }
    /// <summary>The magnitude of the Y-component of the <see cref="Velocity3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfVelocity)"/> or a pre-defined property
    /// - such as <see cref="MetresPerSecond"/>.</remarks>
    public double Y { get; init; }
    /// <summary>The magnitude of the Z-component of the <see cref="Velocity3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfVelocity)"/> or a pre-defined property
    /// - such as <see cref="MetresPerSecond"/>.</remarks>
    public double Z { get; init; }

    /// <summary>Retrieves the magnitudes of the components of the <see cref="Velocity3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfVelocity)"/> or a pre-defined property
    /// - such as <see cref="MetresPerSecond"/>.</remarks>
    public Vector3 Components => new(X, Y, Z);

    /// <summary>Constructs a new <see cref="Velocity3"/> with components <paramref name="components"/>.</summary>
    /// <param name="components">The components of the <see cref="Velocity3"/>.</param>
    public Velocity3((Speed x, Speed y, Speed z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="Velocity3"/> with components (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The X-component of the <see cref="Velocity3"/>.</param>
    /// <param name="y">The Y-component of the <see cref="Velocity3"/>.</param>
    /// <param name="z">The Z-component of the <see cref="Velocity3"/>.</param>
    public Velocity3(Speed x, Speed y, Speed z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="Velocity3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfVelocity"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Velocity3"/>, expressed in <paramref name="unitOfVelocity"/>.</param>
    /// <param name="unitOfVelocity">The <see cref="UnitOfVelocity"/> in which the magnitudes of the components, <paramref name="components"/>, are expressed.</param>
    public Velocity3((Scalar x, Scalar y, Scalar z) components, UnitOfVelocity unitOfVelocity) : this(components.x, components.y, components.z, unitOfVelocity) { }
    /// <summary>Constructs a new <see cref="Velocity3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>),
    /// expressed in <paramref name="unitOfVelocity"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="Velocity3"/>, expressed in <paramref name="unitOfVelocity"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="Velocity3"/>, expressed in <paramref name="unitOfVelocity"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="Velocity3"/>, expressed in <paramref name="unitOfVelocity"/>.</param>
    /// <param name="unitOfVelocity">The <see cref="UnitOfVelocity"/> in which the magnitudes of the components,
    /// (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>), are expressed.</param>
    public Velocity3(Scalar x, Scalar y, Scalar z, UnitOfVelocity unitOfVelocity) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfVelocity) { }
    /// <summary>Constructs a new <see cref="Velocity3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfVelocity"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Velocity3"/>, expressed in <paramref name="unitOfVelocity"/>.</param>
    /// <param name="unitOfVelocity">The <see cref="UnitOfVelocity"/> in which the magnitudes of the components,
    /// <paramref name="components"/>, are expressed.</param>
    public Velocity3(Vector3 components, UnitOfVelocity unitOfVelocity) : this(components.X, components.Y, components.Z, unitOfVelocity) { }
    /// <summary>Constructs a new <see cref="Velocity3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfVelocity"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Velocity3"/>, expressed in <paramref name="unitOfVelocity"/>.</param>
    /// <param name="unitOfVelocity">The <see cref="UnitOfVelocity"/> in which the magnitudes of the components, <paramref name="components"/>, are expressed.</param>
    public Velocity3((double x, double y, double z) components, UnitOfVelocity unitOfVelocity) : this(components.x, components.y, components.z, unitOfVelocity) { }
    /// <summary>Constructs a new <see cref="Velocity3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>),
    /// expressed in <paramref name="unitOfVelocity"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="Velocity3"/>, expressed in <paramref name="unitOfVelocity"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="Velocity3"/>, expressed in <paramref name="unitOfVelocity"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="Velocity3"/>, expressed in <paramref name="unitOfVelocity"/>.</param>
    /// <param name="unitOfVelocity">The <see cref="UnitOfVelocity"/> in which the magnitudes of the components,
    /// (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>), are expressed.</param>
    public Velocity3(double x, double y, double z, UnitOfVelocity unitOfVelocity) : this(x * unitOfVelocity.Factor, y * unitOfVelocity.Factor, z * unitOfVelocity.Factor) { }

    /// <summary>Constructs a new <see cref="Velocity3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Velocity3"/>.</param>
    /// <remarks>Consider preferring <see cref="Velocity3(ValueTuple{Scalar, Scalar, Scalar}, UnitOfVelocity)"/>.</remarks>
    public Velocity3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="Velocity3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="Velocity3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="Velocity3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="Velocity3"/>.</param>
    /// <remarks>Consider preferring <see cref="Velocity3(Scalar, Scalar, Scalar, UnitOfVelocity)"/>.</remarks>
    public Velocity3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="Velocity3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Velocity3"/>.</param>
    /// <remarks>Consider preferring <see cref="Velocity3(Vector3, UnitOfVelocity)"/>.</remarks>
    public Velocity3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    /// <summary>Constructs a new <see cref="Velocity3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Velocity3"/>.</param>
    /// <remarks>Consider preferring <see cref="Velocity3(ValueTuple{double, double, double}, UnitOfVelocity)"/>.</remarks>
    public Velocity3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="Velocity3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="Velocity3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="Velocity3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="Velocity3"/>.</param>
    /// <remarks>Consider preferring <see cref="Velocity3(double, double, double, UnitOfVelocity)"/>.</remarks>
    public Velocity3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>Retrieves the magnitudes of the components of the <see cref="Velocity3"/>, expressed in <see cref="UnitOfVelocity.MetrePerSecond"/>.</summary>
    public Vector3 MetresPerSecond => InUnit(UnitOfVelocity.MetrePerSecond);
    /// <summary>Retrieves the magnitudes of the components of the <see cref="Velocity3"/>, expressed in <see cref="UnitOfVelocity.KilometrePerHour"/>.</summary>
    public Vector3 KilometresPerHour => InUnit(UnitOfVelocity.KilometrePerHour);

    /// <summary>Retrieves the magnitudes of the components of the <see cref="Velocity3"/>, expressed in <see cref="UnitOfVelocity.MilePerHour"/>.</summary>
    public Vector3 MilesPerHour => InUnit(UnitOfVelocity.MilePerHour);

    /// <inheritdoc/>
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    /// <summary>Computes the magnitude, or norm, of the vector quantity <see cref="Velocity3"/>, as a <see cref="Speed"/>.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when possible.</remarks>
    public Speed Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    /// <inheritdoc/>
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);
    /// <summary>Computes the square of the magnitude, or norm, of the vector quantity <see cref="Velocity3"/>, as a <see cref="SpeedSquared"/>.</summary>
    public SpeedSquared SquaredMagnitude() => new(Maths.Vectors.Dot(this, this));

    /// <summary>Normalizes the <see cref="Velocity3"/> - if expressed in SI units.</summary>
    /// <remarks>Note that the resulting <see cref="Velocity3"/> will only be normalized if expressed in SI units.</remarks>
    public Velocity3 Normalize() => this / Magnitude().Magnitude;
    /// <summary>Computes the transformation of the existing <see cref="Velocity3"/> by <paramref name="transform"/>, resulting in
    /// a new <see cref="Velocity3"/>.</summary>
    /// <param name="transform">The <see cref="Velocity3"/> is transformed by this <see cref="Matrix4x4"/>.</param>
    public Velocity3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    /// <summary>Performs dot-multiplication of the <see cref="Velocity3"/> by <paramref name="factor"/>, resulting in a
    /// <cref name="Speed"/>.</summary>
    /// <param name="factor">The <see cref="Velocity3"/> is dot-multiplied by this <see cref="Vector3"/>.</param>
    public Speed Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    /// <summary>Performs dot-multiplication of the <see cref="Velocity3"/> by <paramref name="factor"/>, resulting in a
    /// <cref name="Unhandled"/>.</summary>
    /// <param name="factor">The <see cref="Velocity3"/> is dot-multiplied by this <see cref="Unhandled3"/>.</param>
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    /// <inheritdoc/>
    public TProductScalarQuantity Dot<TProductScalarQuantity, TFactorVector3Quantity>(TFactorVector3Quantity factor, Func<Scalar, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorVector3Quantity : IVector3Quantity
        => factory(Maths.Vectors.Dot(this, factor));
    /// <summary>Performs cross-multiplication of the <see cref="Velocity3"/> by <paramref name="factor"/>, resulting in a
    /// <cref see="Velocity3"/>.</summary>
    /// <param name="factor">The <see cref="Velocity3"/> is cross-multiplied by this <see cref="Vector3"/>.</param>
    public Velocity3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    /// <summary>Performs cross-multiplication of the <see cref="Velocity3"/> by <paramref name="factor"/>, resulting in a
    /// <cref name="Unhandled3"/>.</summary>
    /// <param name="factor">The <see cref="Velocity3"/> is cross-multiplied by this <see cref="Unhandled3"/>.</param>
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    /// <inheritdoc/>
    public TProductVector3Quantity Cross<TProductVector3Quantity, TFactorVector3Quantity>(TFactorVector3Quantity factor, Func<Vector3, TProductVector3Quantity> factory)
        where TProductVector3Quantity : IVector3Quantity
        where TFactorVector3Quantity : IVector3Quantity
        => factory(Maths.Vectors.Cross(this, factor));

    /// <summary>Produces a formatted string from the magnitudes of the components of the <see cref="Velocity3"/> (in SI units),
    /// and the SI base unit of the quantity.</summary>
    public override string ToString() => $"({X}, {Y}, {Z}) [m / s]";

    /// <summary>Produces a <see cref="Vector3"/> with components equal to that of the <see cref="Velocity3"/>,
    /// expressed in <paramref name="unitOfVelocity"/>.</summary>
    /// <param name="unitOfVelocity">The <see cref="UnitOfVelocity"/> in which the magnitude is expressed.</param>
    public Vector3 InUnit(UnitOfVelocity unitOfVelocity) => InUnit(this, unitOfVelocity);
    /// <summary>Produces a <see cref="Vector3"/> with components equal to that of the <see cref="Velocity3"/>,
    /// expressed in <paramref name="unitOfVelocity"/>.</summary>
    /// <param name="velocity3">The <see cref="Velocity3"/> to be expressed in <paramref name="unitOfVelocity"/>.</param>
    /// <param name="unitOfVelocity">The <see cref="UnitOfVelocity"/> in which the magnitude is expressed.</param>
    private static Vector3 InUnit(Velocity3 velocity3, UnitOfVelocity unitOfVelocity) => velocity3.ToVector3() / unitOfVelocity.Factor;
    
    /// <summary>Unary plus, resulting in the unmodified <see cref="Velocity3"/>.</summary>
    public Velocity3 Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Velocity3"/> with negated components.</summary>
    public Velocity3 Negate() => new(-X, -Y, -Z);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="a"/>.</summary>
    /// <param name="a">Unary plus is applied to this instance of <see cref="Velocity3"/>.</param>
    public static Velocity3 operator +(Velocity3 a) => a;
    /// <summary>Negation, resulting in a <see cref="Velocity3"/> with components negated from that of <paramref name="a"/>.</summary>
    /// <param name="a">Negation is applied to this instance of <see cref="Velocity3"/>.</param>
    public static Velocity3 operator -(Velocity3 a) => new(-a.X, -a.Y, -a.Z);

    /// <summary>Multiplies the <see cref="Velocity3"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Velocity3"/> is multiplied.</param>
    public Unhandled3 Multiply(Unhandled factor) => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    /// <summary>Divides the <see cref="Velocity3"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Velocity3"/> is divided.</param>
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="Velocity3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Velocity3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="Velocity3"/> <paramref name="a"/> is multiplied.</param>
    public static Unhandled3 operator *(Velocity3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="b"/> by the <see cref="Velocity3"/> <paramref name="a"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Unhandled"/> quantity by which the <see cref="Velocity3"/> <paramref name="b"/> is multiplied.</param>
    /// <param name="b">The <see cref="Velocity3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="a"/>.</param>
    public static Unhandled3 operator *(Unhandled a, Velocity3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Divides the <see cref="Velocity3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Velocity3"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="Velocity3"/> <paramref name="a"/> is divded.</param>
    public static Unhandled3 operator /(Velocity3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    /// <summary>Produces a <see cref="Velocity3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Velocity3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    /// <summary>Scales the <see cref="Velocity3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Velocity3"/> is scaled.</param>
    public Velocity3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    /// <summary>Scales the <see cref="Velocity3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Velocity3"/> is divided.</param>
    public Velocity3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    /// <summary>Produces a <see cref="Velocity3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Velocity3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is retrieved from division of <see cref="Velocity3"/> <paramref name="a"/> by this value.</param>
    public static Velocity3 operator %(Velocity3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    /// <summary>Scales the <see cref="Velocity3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Velocity3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="Velocity3"/> <paramref name="a"/>.</param>
    public static Velocity3 operator *(Velocity3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    /// <summary>Scales the <see cref="Velocity3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="Velocity3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Velocity3"/>, which is scaled by <paramref name="a"/>.</param>
    public static Velocity3 operator *(double a, Velocity3 b) => new(a * b.X, a * b.Y, a * b.Z);
    /// <summary>Scales the <see cref="Velocity3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Velocity3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="Velocity3"/> <paramref name="a"/>.</param>
    public static Velocity3 operator /(Velocity3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    /// <summary>Produces a <see cref="Velocity3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public Velocity3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    /// <summary>Scales the <see cref="Velocity3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Velocity3"/> is scaled.</param>
    public Velocity3 Multiply(Scalar factor) => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    /// <summary>Scales the <see cref="Velocity3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Velocity3"/> is divided.</param>
    public Velocity3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    /// <summary>Produces a <see cref="Velocity3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Velocity3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is retrieved from division of <see cref="Velocity3"/> <paramref name="a"/> by this value.</param>
    public static Velocity3 operator %(Velocity3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    /// <summary>Scales the <see cref="Velocity3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Velocity3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="Velocity3"/> <paramref name="a"/>.</param>
    public static Velocity3 operator *(Velocity3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Scales the <see cref="Velocity3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="Velocity3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Velocity3"/>, which is scaled by <paramref name="a"/>.</param>
    public static Velocity3 operator *(Scalar a, Velocity3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Scales the <see cref="Velocity3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Velocity3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="Velocity3"/> <paramref name="a"/>.</param>
    public static Velocity3 operator /(Velocity3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    /// <inheritdoc/>
    public TProductVector3Quantity Multiply<TProductVector3Quantity, TFactorScalarQuantity>(TFactorScalarQuantity factor, Func<double, double, double, TProductVector3Quantity> factory)
        where TProductVector3Quantity : IVector3Quantity
        where TFactorScalarQuantity : IScalarQuantity
        => factory(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    /// <inheritdoc/>
    public TQuotientVector3Quantity Divide<TQuotientVector3Quantity, TDivisorScalarQuantity>(TDivisorScalarQuantity divisor, Func<double, double, double, TQuotientVector3Quantity> factory)
        where TQuotientVector3Quantity : IVector3Quantity
        where TDivisorScalarQuantity : IScalarQuantity
        => factory(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    /// <summary>Multiples the <see cref="Velocity3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Velocity3"/>, which is multiplied by <paramref name="b"/>.</param>
    /// <param name="b">This quantity is multiplied by the <see cref="Velocity3"/> <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity, Func{double, double, double, TProductVector3Quantity})"/>.</remarks>
    public static Unhandled3 operator *(Velocity3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Multiples the quantity <paramref name="a"/> by the <see cref="Velocity3"/> <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">This quantity is multiplied by the <see cref="Velocity3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Velocity3"/>, which is multiplied by <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity, Func{double, double, double, TProductVector3Quantity})"/>.</remarks>
    public static Unhandled3 operator *(IScalarQuantity a, Velocity3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Divides the <see cref="Velocity3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Velocity3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">The<see cref="Velocity3"/> <paramref name="a"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientVector3Quantity, TDivisorScalarQuantity}(TDivisorScalarQuantity, Func{double, double, double, TQuotientVector3Quantity})"/>.</remarks>
    public static Unhandled3 operator /(Velocity3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    /// <summary>Converts the <see cref="Velocity3"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with values
    /// (<see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>), when expressed in SI units.</summary>
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    /// <summary>Converts <paramref name="a"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with values
    /// (<see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>), when expressed in SI units.</summary>
    public static implicit operator (double x, double y, double z)(Velocity3 a) => (a.X, a.Y, a.Z);

    /// <summary>Converts the <see cref="Velocity3"/> to the <see cref="Vector3"/> with components of
    /// equal magnitude, when expressed in SI units.</summary>
    public Vector3 ToVector3() => new(X, Y, Z);
    /// <summary>Converts <paramref name="a"/> to the <see cref="Vector3"/> with components of
    /// equal magnitude, when expressed in SI units.</summary>
    public static explicit operator Vector3(Velocity3 a) => new(a.X, a.Y, a.Z);

    /// <summary>Constructs the <see cref="Velocity3"/> with components equal to the values of <paramref name="components"/>,
    /// when expressed in SI units.</summary>
    public static Velocity3 FromValueTuple((double x, double y, double z) components) => new(components);
    /// <summary>Constructs the <see cref="Velocity3"/> with components equal to the values of <paramref name="components"/>,
    /// when expressed in SI units.</summary>
    public static explicit operator Velocity3((double x, double y, double z) components) => new(components);

    /// <summary>Converts <paramref name="a"/> to the <see cref="Velocity3"/> with components of equal magnitude,
    /// when expressed in SI units.</summary>
    public static Velocity3 FromVector3(Vector3 a) => new(a);
    /// <summary>Converts <paramref name="a"/> to the <see cref="Velocity3"/> with components of equal magnitude,
    /// when expressed in SI units.</summary>
    public static explicit operator Velocity3(Vector3 a) => new(a);
}
