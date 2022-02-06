namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

/// <summary>A measure of the vector quantity <see cref="VelocitySquared3"/>, of dimensionality three,
/// describing the square of <see cref="Velocity3"/>. The quantity is expressed in <see cref="UnitOfVelocitySquared"/>, with the SI unit being [m / s].
/// <para>
/// New instances of <see cref="VelocitySquared3"/> can be constructed by multiplying a <see cref="SpeedSquared"/> with a <see cref="Vector3"/> or (double, double, double).
/// Instances can also be produced by combining other quantities, either through mathematical operators or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="VelocitySquared3"/> a = (3, 5, 7) * <see cref="SpeedSquared.OneSquareMetrePerSecondSquared"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="VelocitySquared3"/> d = <see cref="VelocitySquared3.From(Velocity3, Velocity3)"/>;
/// </code>
/// </item>
/// </list>
/// The components of the measure can be retrieved as a <see cref="Vector3"/> using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfVelocitySquared"/>.
/// </para>
/// </summary>
public readonly partial record struct VelocitySquared3 :
    IVector3Quantity,
    IScalableVector3Quantity<VelocitySquared3>,
    INormalizableVector3Quantity<VelocitySquared3>,
    ITransformableVector3Quantity<VelocitySquared3>,
    IMultiplicableVector3Quantity<VelocitySquared3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<VelocitySquared3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<SpeedSquared, Vector3>,
    IDotableVector3Quantity<Unhandled, Unhandled3>,
    ICrossableVector3Quantity<VelocitySquared3, Vector3>,
    ICrossableVector3Quantity<Unhandled3, Unhandled3>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    /// <summary>The zero-magnitude <see cref="VelocitySquared3"/>.</summary>
    public static VelocitySquared3 Zero { get; } = new(0, 0, 0);

    /// <summary>The magnitude of the X-component of the <see cref="VelocitySquared3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfVelocitySquared)"/> or a pre-defined property
    /// - such as <see cref="SquareMetresPerSecondSquared"/>.</remarks>
    public double X { get; init; }
    /// <summary>The magnitude of the Y-component of the <see cref="VelocitySquared3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfVelocitySquared)"/> or a pre-defined property
    /// - such as <see cref="SquareMetresPerSecondSquared"/>.</remarks>
    public double Y { get; init; }
    /// <summary>The magnitude of the Z-component of the <see cref="VelocitySquared3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfVelocitySquared)"/> or a pre-defined property
    /// - such as <see cref="SquareMetresPerSecondSquared"/>.</remarks>
    public double Z { get; init; }

    /// <summary>Retrieves the magnitudes of the components of the <see cref="VelocitySquared3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfVelocitySquared)"/> or a pre-defined property
    /// - such as <see cref="SquareMetresPerSecondSquared"/>.</remarks>
    public Vector3 Components => new(X, Y, Z);

    /// <summary>Constructs a new <see cref="VelocitySquared3"/> with components <paramref name="components"/>.</summary>
    /// <param name="components">The components of the <see cref="VelocitySquared3"/>.</param>
    public VelocitySquared3((SpeedSquared x, SpeedSquared y, SpeedSquared z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="VelocitySquared3"/> with components (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The X-component of the <see cref="VelocitySquared3"/>.</param>
    /// <param name="y">The Y-component of the <see cref="VelocitySquared3"/>.</param>
    /// <param name="z">The Z-component of the <see cref="VelocitySquared3"/>.</param>
    public VelocitySquared3(SpeedSquared x, SpeedSquared y, SpeedSquared z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="VelocitySquared3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfVelocitySquared"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="VelocitySquared3"/>, expressed in <paramref name="unitOfVelocitySquared"/>.</param>
    /// <param name="unitOfVelocitySquared">The <see cref="UnitOfVelocitySquared"/> in which the magnitudes of the components, <paramref name="components"/>, are expressed.</param>
    public VelocitySquared3((Scalar x, Scalar y, Scalar z) components, UnitOfVelocitySquared unitOfVelocitySquared) : 
    	this(components.x, components.y, components.z, unitOfVelocitySquared) { }
    /// <summary>Constructs a new <see cref="VelocitySquared3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>),
    /// expressed in <paramref name="unitOfVelocitySquared"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="VelocitySquared3"/>, expressed in <paramref name="unitOfVelocitySquared"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="VelocitySquared3"/>, expressed in <paramref name="unitOfVelocitySquared"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="VelocitySquared3"/>, expressed in <paramref name="unitOfVelocitySquared"/>.</param>
    /// <param name="unitOfVelocitySquared">The <see cref="UnitOfVelocitySquared"/> in which the magnitudes of the components,
    /// (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>), are expressed.</param>
    public VelocitySquared3(Scalar x, Scalar y, Scalar z, UnitOfVelocitySquared unitOfVelocitySquared) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfVelocitySquared) { }
    /// <summary>Constructs a new <see cref="VelocitySquared3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfVelocitySquared"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="VelocitySquared3"/>, expressed in <paramref name="unitOfVelocitySquared"/>.</param>
    /// <param name="unitOfVelocitySquared">The <see cref="UnitOfVelocitySquared"/> in which the magnitudes of the components,
    /// <paramref name="components"/>, are expressed.</param>
    public VelocitySquared3(Vector3 components, UnitOfVelocitySquared unitOfVelocitySquared) : this(components.X, components.Y, components.Z, unitOfVelocitySquared) { }
    /// <summary>Constructs a new <see cref="VelocitySquared3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfVelocitySquared"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="VelocitySquared3"/>, expressed in <paramref name="unitOfVelocitySquared"/>.</param>
    /// <param name="unitOfVelocitySquared">The <see cref="UnitOfVelocitySquared"/> in which the magnitudes of the components, <paramref name="components"/>, are expressed.</param>
    public VelocitySquared3((double x, double y, double z) components, UnitOfVelocitySquared unitOfVelocitySquared) : 
    	this(components.x, components.y, components.z, unitOfVelocitySquared) { }
    /// <summary>Constructs a new <see cref="VelocitySquared3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>),
    /// expressed in <paramref name="unitOfVelocitySquared"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="VelocitySquared3"/>, expressed in <paramref name="unitOfVelocitySquared"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="VelocitySquared3"/>, expressed in <paramref name="unitOfVelocitySquared"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="VelocitySquared3"/>, expressed in <paramref name="unitOfVelocitySquared"/>.</param>
    /// <param name="unitOfVelocitySquared">The <see cref="UnitOfVelocitySquared"/> in which the magnitudes of the components,
    /// (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>), are expressed.</param>
    public VelocitySquared3(double x, double y, double z, UnitOfVelocitySquared unitOfVelocitySquared) : 
    	this(x * unitOfVelocitySquared.Factor, y * unitOfVelocitySquared.Factor, z * unitOfVelocitySquared.Factor) { }

    /// <summary>Constructs a new <see cref="VelocitySquared3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="VelocitySquared3"/>.</param>
    /// <remarks>Consider preferring <see cref="VelocitySquared3(ValueTuple{Scalar, Scalar, Scalar}, UnitOfVelocitySquared)"/>.</remarks>
    public VelocitySquared3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="VelocitySquared3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="VelocitySquared3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="VelocitySquared3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="VelocitySquared3"/>.</param>
    /// <remarks>Consider preferring <see cref="VelocitySquared3(Scalar, Scalar, Scalar, UnitOfVelocitySquared)"/>.</remarks>
    public VelocitySquared3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="VelocitySquared3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="VelocitySquared3"/>.</param>
    /// <remarks>Consider preferring <see cref="VelocitySquared3(Vector3, UnitOfVelocitySquared)"/>.</remarks>
    public VelocitySquared3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    /// <summary>Constructs a new <see cref="VelocitySquared3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="VelocitySquared3"/>.</param>
    /// <remarks>Consider preferring <see cref="VelocitySquared3(ValueTuple{double, double, double}, UnitOfVelocitySquared)"/>.</remarks>
    public VelocitySquared3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="VelocitySquared3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="VelocitySquared3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="VelocitySquared3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="VelocitySquared3"/>.</param>
    /// <remarks>Consider preferring <see cref="VelocitySquared3(double, double, double, UnitOfVelocitySquared)"/>.</remarks>
    public VelocitySquared3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>Retrieves the magnitudes of the components of the <see cref="VelocitySquared3"/>, expressed in <see cref="UnitOfVelocitySquared.SquareMetrePerSecondSquared"/>.</summary>
    public Vector3 SquareMetresPerSecondSquared => InUnit(UnitOfVelocitySquared.SquareMetrePerSecondSquared);

    /// <inheritdoc/>
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    /// <summary>Computes the magnitude, or norm, of the vector quantity <see cref="VelocitySquared3"/>, as a <see cref="SpeedSquared"/>.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when possible.</remarks>
    public SpeedSquared Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    /// <summary>Computes the square of the magnitude, or norm, of the vector quantity <see cref="VelocitySquared3"/>.</summary>
    /// <remarks>For clarity, consider first extracting the magnitudes of the components in the desired <see cref="UnitOfVelocitySquared"/>.</remarks>
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    /// <summary>Normalizes the <see cref="VelocitySquared3"/> - if expressed in SI units.</summary>
    /// <remarks>Note that the resulting <see cref="VelocitySquared3"/> will only be normalized if expressed in SI units.</remarks>
    public VelocitySquared3 Normalize() => this / Magnitude().Magnitude;
    /// <summary>Computes the transformation of the existing <see cref="VelocitySquared3"/> by <paramref name="transform"/>, resulting in
    /// a new <see cref="VelocitySquared3"/>.</summary>
    /// <param name="transform">The <see cref="VelocitySquared3"/> is transformed by this <see cref="Matrix4x4"/>.</param>
    public VelocitySquared3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    /// <summary>Performs dot-multiplication of the <see cref="VelocitySquared3"/> by <paramref name="factor"/>, resulting in a
    /// <cref name="SpeedSquared"/>.</summary>
    /// <param name="factor">The <see cref="VelocitySquared3"/> is dot-multiplied by this <see cref="Vector3"/>.</param>
    public SpeedSquared Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    /// <summary>Performs dot-multiplication of the <see cref="VelocitySquared3"/> by <paramref name="factor"/>, resulting in a
    /// <cref name="Unhandled"/>.</summary>
    /// <param name="factor">The <see cref="VelocitySquared3"/> is dot-multiplied by this <see cref="Unhandled3"/>.</param>
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    /// <inheritdoc/>
    public TProductScalarQuantity Dot<TProductScalarQuantity, TFactorVector3Quantity>(TFactorVector3Quantity factor, Func<Scalar, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorVector3Quantity : IVector3Quantity
        => factory(Maths.Vectors.Dot(this, factor));
    /// <summary>Performs cross-multiplication of the <see cref="VelocitySquared3"/> by <paramref name="factor"/>, resulting in a
    /// <cref see="VelocitySquared3"/>.</summary>
    /// <param name="factor">The <see cref="VelocitySquared3"/> is cross-multiplied by this <see cref="Vector3"/>.</param>
    public VelocitySquared3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    /// <summary>Performs cross-multiplication of the <see cref="VelocitySquared3"/> by <paramref name="factor"/>, resulting in a
    /// <cref name="Unhandled3"/>.</summary>
    /// <param name="factor">The <see cref="VelocitySquared3"/> is cross-multiplied by this <see cref="Unhandled3"/>.</param>
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    /// <inheritdoc/>
    public TProductVector3Quantity Cross<TProductVector3Quantity, TFactorVector3Quantity>(TFactorVector3Quantity factor, Func<Vector3, TProductVector3Quantity> factory)
        where TProductVector3Quantity : IVector3Quantity
        where TFactorVector3Quantity : IVector3Quantity
        => factory(Maths.Vectors.Cross(this, factor));

    /// <summary>Produces a formatted string from the magnitudes of the components of the <see cref="VelocitySquared3"/> (in SI units),
    /// and the SI base unit of the quantity.</summary>
    public override string ToString() => $"({X}, {Y}, {Z}) [m^2 / s^2]";

    /// <summary>Produces a <see cref="Vector3"/> with components equal to that of the <see cref="VelocitySquared3"/>,
    /// expressed in <paramref name="unitOfVelocitySquared"/>.</summary>
    /// <param name="unitOfVelocitySquared">The <see cref="UnitOfVelocitySquared"/> in which the magnitude is expressed.</param>
    public Vector3 InUnit(UnitOfVelocitySquared unitOfVelocitySquared) => InUnit(this, unitOfVelocitySquared);
    /// <summary>Produces a <see cref="Vector3"/> with components equal to that of the <see cref="VelocitySquared3"/>,
    /// expressed in <paramref name="unitOfVelocitySquared"/>.</summary>
    /// <param name="velocitySquared3">The <see cref="VelocitySquared3"/> to be expressed in <paramref name="unitOfVelocitySquared"/>.</param>
    /// <param name="unitOfVelocitySquared">The <see cref="UnitOfVelocitySquared"/> in which the magnitude is expressed.</param>
    private static Vector3 InUnit(VelocitySquared3 velocitySquared3, UnitOfVelocitySquared unitOfVelocitySquared) => 
    	velocitySquared3.ToVector3() / unitOfVelocitySquared.Factor;
    
    /// <summary>Unary plus, resulting in the unmodified <see cref="VelocitySquared3"/>.</summary>
    public VelocitySquared3 Plus() => this;
    /// <summary>Negation, resulting in a <see cref="VelocitySquared3"/> with negated components.</summary>
    public VelocitySquared3 Negate() => new(-X, -Y, -Z);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="a"/>.</summary>
    /// <param name="a">Unary plus is applied to this instance of <see cref="VelocitySquared3"/>.</param>
    public static VelocitySquared3 operator +(VelocitySquared3 a) => a;
    /// <summary>Negation, resulting in a <see cref="VelocitySquared3"/> with components negated from that of <paramref name="a"/>.</summary>
    /// <param name="a">Negation is applied to this instance of <see cref="VelocitySquared3"/>.</param>
    public static VelocitySquared3 operator -(VelocitySquared3 a) => new(-a.X, -a.Y, -a.Z);

    /// <summary>Multiplies the <see cref="VelocitySquared3"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="VelocitySquared3"/> is multiplied.</param>
    public Unhandled3 Multiply(Unhandled factor) => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    /// <summary>Divides the <see cref="VelocitySquared3"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="VelocitySquared3"/> is divided.</param>
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="VelocitySquared3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="VelocitySquared3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="VelocitySquared3"/> <paramref name="a"/> is multiplied.</param>
    public static Unhandled3 operator *(VelocitySquared3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="b"/> by the <see cref="VelocitySquared3"/> <paramref name="a"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Unhandled"/> quantity by which the <see cref="VelocitySquared3"/> <paramref name="b"/> is multiplied.</param>
    /// <param name="b">The <see cref="VelocitySquared3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="a"/>.</param>
    public static Unhandled3 operator *(Unhandled a, VelocitySquared3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Divides the <see cref="VelocitySquared3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="VelocitySquared3"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="VelocitySquared3"/> <paramref name="a"/> is divded.</param>
    public static Unhandled3 operator /(VelocitySquared3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    /// <summary>Produces a <see cref="VelocitySquared3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public VelocitySquared3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    /// <summary>Scales the <see cref="VelocitySquared3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="VelocitySquared3"/> is scaled.</param>
    public VelocitySquared3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    /// <summary>Scales the <see cref="VelocitySquared3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="VelocitySquared3"/> is divided.</param>
    public VelocitySquared3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    /// <summary>Produces a <see cref="VelocitySquared3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="VelocitySquared3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is retrieved from division of <see cref="VelocitySquared3"/> <paramref name="a"/> by this value.</param>
    public static VelocitySquared3 operator %(VelocitySquared3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    /// <summary>Scales the <see cref="VelocitySquared3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="VelocitySquared3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="VelocitySquared3"/> <paramref name="a"/>.</param>
    public static VelocitySquared3 operator *(VelocitySquared3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    /// <summary>Scales the <see cref="VelocitySquared3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="VelocitySquared3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="VelocitySquared3"/>, which is scaled by <paramref name="a"/>.</param>
    public static VelocitySquared3 operator *(double a, VelocitySquared3 b) => new(a * b.X, a * b.Y, a * b.Z);
    /// <summary>Scales the <see cref="VelocitySquared3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="VelocitySquared3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="VelocitySquared3"/> <paramref name="a"/>.</param>
    public static VelocitySquared3 operator /(VelocitySquared3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    /// <summary>Produces a <see cref="VelocitySquared3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public VelocitySquared3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    /// <summary>Scales the <see cref="VelocitySquared3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="VelocitySquared3"/> is scaled.</param>
    public VelocitySquared3 Multiply(Scalar factor) => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    /// <summary>Scales the <see cref="VelocitySquared3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="VelocitySquared3"/> is divided.</param>
    public VelocitySquared3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    /// <summary>Produces a <see cref="VelocitySquared3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="VelocitySquared3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is retrieved from division of <see cref="VelocitySquared3"/> <paramref name="a"/> by this value.</param>
    public static VelocitySquared3 operator %(VelocitySquared3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    /// <summary>Scales the <see cref="VelocitySquared3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="VelocitySquared3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="VelocitySquared3"/> <paramref name="a"/>.</param>
    public static VelocitySquared3 operator *(VelocitySquared3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Scales the <see cref="VelocitySquared3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="VelocitySquared3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="VelocitySquared3"/>, which is scaled by <paramref name="a"/>.</param>
    public static VelocitySquared3 operator *(Scalar a, VelocitySquared3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Scales the <see cref="VelocitySquared3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="VelocitySquared3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="VelocitySquared3"/> <paramref name="a"/>.</param>
    public static VelocitySquared3 operator /(VelocitySquared3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

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
    /// <summary>Multiples the <see cref="VelocitySquared3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="VelocitySquared3"/>, which is multiplied by <paramref name="b"/>.</param>
    /// <param name="b">This quantity is multiplied by the <see cref="VelocitySquared3"/> <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity, Func{double, double, double, TProductVector3Quantity})"/>.</remarks>
    public static Unhandled3 operator *(VelocitySquared3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Multiples the quantity <paramref name="a"/> by the <see cref="VelocitySquared3"/> <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">This quantity is multiplied by the <see cref="VelocitySquared3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="VelocitySquared3"/>, which is multiplied by <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity, Func{double, double, double, TProductVector3Quantity})"/>.</remarks>
    public static Unhandled3 operator *(IScalarQuantity a, VelocitySquared3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Divides the <see cref="VelocitySquared3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="VelocitySquared3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">The<see cref="VelocitySquared3"/> <paramref name="a"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientVector3Quantity, TDivisorScalarQuantity}(TDivisorScalarQuantity, Func{double, double, double, TQuotientVector3Quantity})"/>.</remarks>
    public static Unhandled3 operator /(VelocitySquared3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    /// <summary>Converts the <see cref="VelocitySquared3"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with values
    /// (<see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>), when expressed in SI units.</summary>
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    /// <summary>Converts <paramref name="a"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with values
    /// (<see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>), when expressed in SI units.</summary>
    public static implicit operator (double x, double y, double z)(VelocitySquared3 a) => (a.X, a.Y, a.Z);

    /// <summary>Converts the <see cref="VelocitySquared3"/> to the <see cref="Vector3"/> with components of
    /// equal magnitude, when expressed in SI units.</summary>
    public Vector3 ToVector3() => new(X, Y, Z);
    /// <summary>Converts <paramref name="a"/> to the <see cref="Vector3"/> with components of
    /// equal magnitude, when expressed in SI units.</summary>
    public static explicit operator Vector3(VelocitySquared3 a) => new(a.X, a.Y, a.Z);

    /// <summary>Constructs the <see cref="VelocitySquared3"/> with components equal to the values of <paramref name="components"/>,
    /// when expressed in SI units.</summary>
    public static VelocitySquared3 FromValueTuple((double x, double y, double z) components) => new(components);
    /// <summary>Constructs the <see cref="VelocitySquared3"/> with components equal to the values of <paramref name="components"/>,
    /// when expressed in SI units.</summary>
    public static explicit operator VelocitySquared3((double x, double y, double z) components) => new(components);

    /// <summary>Converts <paramref name="a"/> to the <see cref="VelocitySquared3"/> with components of equal magnitude,
    /// when expressed in SI units.</summary>
    public static VelocitySquared3 FromVector3(Vector3 a) => new(a);
    /// <summary>Converts <paramref name="a"/> to the <see cref="VelocitySquared3"/> with components of equal magnitude,
    /// when expressed in SI units.</summary>
    public static explicit operator VelocitySquared3(Vector3 a) => new(a);
}
