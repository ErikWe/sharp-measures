namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

/// <summary>A measure of the vector quantity <see cref="OrbitalAngularMomentum3"/>, of dimensionality three,
/// a property of an object with <see cref="Mass"/> rotating about an external point. The quantity is expressed in
/// <see cref="UnitOfOrbitalAngularMomentum"/>, with the SI unit being [kg * m² / s].
/// <para>
/// New instances of <see cref="OrbitalAngularMomentum3"/> can be constructed by multiplying a <see cref="OrbitalAngularMomentum"/> with a Vector3 or (double, double, double).
/// Instances can also be produced by combining other quantities, either through mathematical operators or using overloads of the static method 'From'.
/// Lastly, instances can be constructed from quantities sharing the same unit, using instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="OrbitalAngularMomentum3"/> a = (3, 5, 7) * <see cref="OrbitalAngularMomentum.OneKilogramMetreSquaredPerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="OrbitalAngularMomentum3"/> d = <see cref="OrbitalAngularMomentum3.From(MomentOfInertia, OrbitalAngularVelocity3)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="OrbitalAngularMomentum3"/> e = <see cref="AngularMomentum3.AsOrbitalAngularMomentum3"/>;
/// </code>
/// </item>
/// </list>
/// The components of the measure can be retrieved as a <see cref="Vector3"/> using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfOrbitalAngularMomentum"/>.
/// </para>
/// </summary>
/// <remarks>
/// <see cref="OrbitalAngularMomentum3"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="AngularMomentum3"/></term>
/// <description>Describes any type of angular momentum.</description>
/// </item>
/// <item>
/// <term><see cref="SpinAngularMomentum3"/></term>
/// <description>Describes the <see cref="AngularMomentum3"/> of an object in rotation about the center of mass of the object.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct OrbitalAngularMomentum3 :
    IVector3Quantity,
    IScalableVector3Quantity<OrbitalAngularMomentum3>,
    INormalizableVector3Quantity<OrbitalAngularMomentum3>,
    ITransformableVector3Quantity<OrbitalAngularMomentum3>,
    IMultiplicableVector3Quantity<OrbitalAngularMomentum3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<OrbitalAngularMomentum3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<OrbitalAngularMomentum, Vector3>,
    IDotableVector3Quantity<Unhandled, Unhandled3>,
    ICrossableVector3Quantity<OrbitalAngularMomentum3, Vector3>,
    ICrossableVector3Quantity<Unhandled3, Unhandled3>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    /// <summary>The zero-magnitude <see cref="OrbitalAngularMomentum3"/>.</summary>
    public static OrbitalAngularMomentum3 Zero { get; } = new(0, 0, 0);

    /// <summary>The magnitude of the X-component of the <see cref="OrbitalAngularMomentum3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfOrbitalAngularMomentum)"/> or a pre-defined property
    /// - such as <see cref="KilogramMetresSquaredPerSecond"/>.</remarks>
    public double X { get; init; }
    /// <summary>The magnitude of the Y-component of the <see cref="OrbitalAngularMomentum3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfOrbitalAngularMomentum)"/> or a pre-defined property
    /// - such as <see cref="KilogramMetresSquaredPerSecond"/>.</remarks>
    public double Y { get; init; }
    /// <summary>The magnitude of the Z-component of the <see cref="OrbitalAngularMomentum3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfOrbitalAngularMomentum)"/> or a pre-defined property
    /// - such as <see cref="KilogramMetresSquaredPerSecond"/>.</remarks>
    public double Z { get; init; }

    /// <summary>Retrieves the magnitudes of the components of the <see cref="OrbitalAngularMomentum3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfOrbitalAngularMomentum)"/> or a pre-defined property
    /// - such as <see cref="KilogramMetresSquaredPerSecond"/>.</remarks>
    public Vector3 Components => new(X, Y, Z);

    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components <paramref name="components"/>.</summary>
    /// <param name="components">The components of the <see cref="OrbitalAngularMomentum3"/>.</param>
    public OrbitalAngularMomentum3((OrbitalAngularMomentum x, OrbitalAngularMomentum y, OrbitalAngularMomentum z) components) : 
    	this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The X-component of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <param name="y">The Y-component of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <param name="z">The Z-component of the <see cref="OrbitalAngularMomentum3"/>.</param>
    public OrbitalAngularMomentum3(OrbitalAngularMomentum x, OrbitalAngularMomentum y, OrbitalAngularMomentum z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfOrbitalAngularMomentum"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="OrbitalAngularMomentum3"/>, expressed in <paramref name="unitOfOrbitalAngularMomentum"/>.</param>
    /// <param name="unitOfOrbitalAngularMomentum">The <see cref="UnitOfOrbitalAngularMomentum"/> in which the magnitudes of the components, <paramref name="components"/>, are expressed.</param>
    public OrbitalAngularMomentum3((Scalar x, Scalar y, Scalar z) components, UnitOfOrbitalAngularMomentum unitOfOrbitalAngularMomentum) : 
    	this(components.x, components.y, components.z, unitOfOrbitalAngularMomentum) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>),
    /// expressed in <paramref name="unitOfOrbitalAngularMomentum"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="OrbitalAngularMomentum3"/>, expressed in <paramref name="unitOfOrbitalAngularMomentum"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="OrbitalAngularMomentum3"/>, expressed in <paramref name="unitOfOrbitalAngularMomentum"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="OrbitalAngularMomentum3"/>, expressed in <paramref name="unitOfOrbitalAngularMomentum"/>.</param>
    /// <param name="unitOfOrbitalAngularMomentum">The <see cref="UnitOfOrbitalAngularMomentum"/> in which the magnitudes of the components,
    /// (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>), are expressed.</param>
    public OrbitalAngularMomentum3(Scalar x, Scalar y, Scalar z, UnitOfOrbitalAngularMomentum unitOfOrbitalAngularMomentum) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfOrbitalAngularMomentum) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfOrbitalAngularMomentum"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="OrbitalAngularMomentum3"/>, expressed in <paramref name="unitOfOrbitalAngularMomentum"/>.</param>
    /// <param name="unitOfOrbitalAngularMomentum">The <see cref="UnitOfOrbitalAngularMomentum"/> in which the magnitudes of the components,
    /// <paramref name="components"/>, are expressed.</param>
    public OrbitalAngularMomentum3(Vector3 components, UnitOfOrbitalAngularMomentum unitOfOrbitalAngularMomentum) : 
    	this(components.X, components.Y, components.Z, unitOfOrbitalAngularMomentum) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfOrbitalAngularMomentum"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="OrbitalAngularMomentum3"/>, expressed in <paramref name="unitOfOrbitalAngularMomentum"/>.</param>
    /// <param name="unitOfOrbitalAngularMomentum">The <see cref="UnitOfOrbitalAngularMomentum"/> in which the magnitudes of the components, <paramref name="components"/>, are expressed.</param>
    public OrbitalAngularMomentum3((double x, double y, double z) components, UnitOfOrbitalAngularMomentum unitOfOrbitalAngularMomentum) : 
    	this(components.x, components.y, components.z, unitOfOrbitalAngularMomentum) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>),
    /// expressed in <paramref name="unitOfOrbitalAngularMomentum"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="OrbitalAngularMomentum3"/>, expressed in <paramref name="unitOfOrbitalAngularMomentum"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="OrbitalAngularMomentum3"/>, expressed in <paramref name="unitOfOrbitalAngularMomentum"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="OrbitalAngularMomentum3"/>, expressed in <paramref name="unitOfOrbitalAngularMomentum"/>.</param>
    /// <param name="unitOfOrbitalAngularMomentum">The <see cref="UnitOfOrbitalAngularMomentum"/> in which the magnitudes of the components,
    /// (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>), are expressed.</param>
    public OrbitalAngularMomentum3(double x, double y, double z, UnitOfOrbitalAngularMomentum unitOfOrbitalAngularMomentum) : 
    	this(x * unitOfOrbitalAngularMomentum.Factor, y * unitOfOrbitalAngularMomentum.Factor, z * unitOfOrbitalAngularMomentum.Factor) { }

    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularMomentum3(ValueTuple{Scalar, Scalar, Scalar}, UnitOfOrbitalAngularMomentum)"/>.</remarks>
    public OrbitalAngularMomentum3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularMomentum3(Scalar, Scalar, Scalar, UnitOfOrbitalAngularMomentum)"/>.</remarks>
    public OrbitalAngularMomentum3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularMomentum3(Vector3, UnitOfOrbitalAngularMomentum)"/>.</remarks>
    public OrbitalAngularMomentum3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularMomentum3(ValueTuple{double, double, double}, UnitOfOrbitalAngularMomentum)"/>.</remarks>
    public OrbitalAngularMomentum3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularMomentum3(double, double, double, UnitOfOrbitalAngularMomentum)"/>.</remarks>
    public OrbitalAngularMomentum3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>Converts the <see cref="OrbitalAngularMomentum3"/> to an instance of the associated quantity <see cref="AngularMomentum"/>, with components of
    /// equal magnitudes.</summary>
    public AngularMomentum3 AsAngularMomentum3() => new(X, Y, Z);
    /// <summary>Converts the <see cref="OrbitalAngularMomentum3"/> to an instance of the associated quantity <see cref="SpinAngularMomentum"/>, with components of
    /// equal magnitudes.</summary>
    public SpinAngularMomentum3 AsSpinAngularMomentum3() => new(X, Y, Z);

    /// <summary>Retrieves the magnitudes of the components of the <see cref="OrbitalAngularMomentum3"/>, expressed in <see cref="UnitOfOrbitalAngularMomentum.KilogramMetreSquaredPerSecond"/>.</summary>
    public Vector3 KilogramMetresSquaredPerSecond => InUnit(UnitOfOrbitalAngularMomentum.KilogramMetreSquaredPerSecond);

    /// <inheritdoc/>
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    /// <summary>Computes the magnitude, or norm, of the vector quantity <see cref="OrbitalAngularMomentum3"/>, as a <see cref="OrbitalAngularMomentum"/>.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when possible.</remarks>
    public OrbitalAngularMomentum Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    /// <summary>Computes the square of the magnitude, or norm, of the vector quantity <see cref="OrbitalAngularMomentum3"/>.</summary>
    /// <remarks>For clarity, consider first extracting the magnitudes of the components in the desired <see cref="UnitOfOrbitalAngularMomentum"/>.</remarks>
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    /// <summary>Normalizes the <see cref="OrbitalAngularMomentum3"/> - if expressed in SI units.</summary>
    /// <remarks>Note that the resulting <see cref="OrbitalAngularMomentum3"/> will only be normalized if expressed in SI units.</remarks>
    public OrbitalAngularMomentum3 Normalize() => this / Magnitude().Magnitude;
    /// <summary>Computes the transformation of the existing <see cref="OrbitalAngularMomentum3"/> by <paramref name="transform"/>, resulting in
    /// a new <see cref="OrbitalAngularMomentum3"/>.</summary>
    /// <param name="transform">The <see cref="OrbitalAngularMomentum3"/> is transformed by this <see cref="Matrix4x4"/>.</param>
    public OrbitalAngularMomentum3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    /// <summary>Performs dot-multiplication of the <see cref="OrbitalAngularMomentum3"/> by <paramref name="factor"/>, resulting in a
    /// <cref name="OrbitalAngularMomentum"/>.</summary>
    /// <param name="factor">The <see cref="OrbitalAngularMomentum3"/> is dot-multiplied by this <see cref="Vector3"/>.</param>
    public OrbitalAngularMomentum Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    /// <summary>Performs dot-multiplication of the <see cref="OrbitalAngularMomentum3"/> by <paramref name="factor"/>, resulting in a
    /// <cref name="Unhandled"/>.</summary>
    /// <param name="factor">The <see cref="OrbitalAngularMomentum3"/> is dot-multiplied by this <see cref="Unhandled3"/>.</param>
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    /// <inheritdoc/>
    public TProductScalarQuantity Dot<TProductScalarQuantity, TFactorVector3Quantity>(TFactorVector3Quantity factor, Func<Scalar, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorVector3Quantity : IVector3Quantity
        => factory(Maths.Vectors.Dot(this, factor));
    /// <summary>Performs cross-multiplication of the <see cref="OrbitalAngularMomentum3"/> by <paramref name="factor"/>, resulting in a
    /// <cref see="OrbitalAngularMomentum3"/>.</summary>
    /// <param name="factor">The <see cref="OrbitalAngularMomentum3"/> is cross-multiplied by this <see cref="Vector3"/>.</param>
    public OrbitalAngularMomentum3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    /// <summary>Performs cross-multiplication of the <see cref="OrbitalAngularMomentum3"/> by <paramref name="factor"/>, resulting in a
    /// <cref name="Unhandled3"/>.</summary>
    /// <param name="factor">The <see cref="OrbitalAngularMomentum3"/> is cross-multiplied by this <see cref="Unhandled3"/>.</param>
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    /// <inheritdoc/>
    public TProductVector3Quantity Cross<TProductVector3Quantity, TFactorVector3Quantity>(TFactorVector3Quantity factor, Func<Vector3, TProductVector3Quantity> factory)
        where TProductVector3Quantity : IVector3Quantity
        where TFactorVector3Quantity : IVector3Quantity
        => factory(Maths.Vectors.Cross(this, factor));

    /// <summary>Produces a formatted string from the magnitudes of the components of the <see cref="OrbitalAngularMomentum3"/> (in SI units),
    /// and the SI base unit of the quantity.</summary>
    public override string ToString() => $"({X}, {Y}, {Z}) [kg * m^2 / s]";

    /// <summary>Produces a <see cref="Vector3"/> with components equal to that of the <see cref="OrbitalAngularMomentum3"/>,
    /// expressed in <paramref name="unitOfOrbitalAngularMomentum"/>.</summary>
    /// <param name="unitOfOrbitalAngularMomentum">The <see cref="UnitOfOrbitalAngularMomentum"/> in which the magnitude is expressed.</param>
    public Vector3 InUnit(UnitOfOrbitalAngularMomentum unitOfOrbitalAngularMomentum) => InUnit(this, unitOfOrbitalAngularMomentum);
    /// <summary>Produces a <see cref="Vector3"/> with components equal to that of the <see cref="OrbitalAngularMomentum3"/>,
    /// expressed in <paramref name="unitOfOrbitalAngularMomentum"/>.</summary>
    /// <param name="orbitalAngularMomentum3">The <see cref="OrbitalAngularMomentum3"/> to be expressed in <paramref name="unitOfOrbitalAngularMomentum"/>.</param>
    /// <param name="unitOfOrbitalAngularMomentum">The <see cref="UnitOfOrbitalAngularMomentum"/> in which the magnitude is expressed.</param>
    private static Vector3 InUnit(OrbitalAngularMomentum3 orbitalAngularMomentum3, UnitOfOrbitalAngularMomentum unitOfOrbitalAngularMomentum) => 
    	orbitalAngularMomentum3.ToVector3() / unitOfOrbitalAngularMomentum.Factor;
    
    /// <summary>Unary plus, resulting in the unmodified <see cref="OrbitalAngularMomentum3"/>.</summary>
    public OrbitalAngularMomentum3 Plus() => this;
    /// <summary>Negation, resulting in a <see cref="OrbitalAngularMomentum3"/> with negated components.</summary>
    public OrbitalAngularMomentum3 Negate() => new(-X, -Y, -Z);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="a"/>.</summary>
    /// <param name="a">Unary plus is applied to this instance of <see cref="OrbitalAngularMomentum3"/>.</param>
    public static OrbitalAngularMomentum3 operator +(OrbitalAngularMomentum3 a) => a;
    /// <summary>Negation, resulting in a <see cref="OrbitalAngularMomentum3"/> with components negated from that of <paramref name="a"/>.</summary>
    /// <param name="a">Negation is applied to this instance of <see cref="OrbitalAngularMomentum3"/>.</param>
    public static OrbitalAngularMomentum3 operator -(OrbitalAngularMomentum3 a) => new(-a.X, -a.Y, -a.Z);

    /// <summary>Multiplies the <see cref="OrbitalAngularMomentum3"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="OrbitalAngularMomentum3"/> is multiplied.</param>
    public Unhandled3 Multiply(Unhandled factor) => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    /// <summary>Divides the <see cref="OrbitalAngularMomentum3"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="OrbitalAngularMomentum3"/> is divided.</param>
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> is multiplied.</param>
    public static Unhandled3 operator *(OrbitalAngularMomentum3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="b"/> by the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Unhandled"/> quantity by which the <see cref="OrbitalAngularMomentum3"/> <paramref name="b"/> is multiplied.</param>
    /// <param name="b">The <see cref="OrbitalAngularMomentum3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="a"/>.</param>
    public static Unhandled3 operator *(Unhandled a, OrbitalAngularMomentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Divides the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> is divded.</param>
    public static Unhandled3 operator /(OrbitalAngularMomentum3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    /// <summary>Produces a <see cref="OrbitalAngularMomentum3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public OrbitalAngularMomentum3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="OrbitalAngularMomentum3"/> is scaled.</param>
    public OrbitalAngularMomentum3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="OrbitalAngularMomentum3"/> is divided.</param>
    public OrbitalAngularMomentum3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    /// <summary>Produces a <see cref="OrbitalAngularMomentum3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is retrieved from division of <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by this value.</param>
    public static OrbitalAngularMomentum3 operator %(OrbitalAngularMomentum3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/>.</param>
    public static OrbitalAngularMomentum3 operator *(OrbitalAngularMomentum3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="OrbitalAngularMomentum3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="OrbitalAngularMomentum3"/>, which is scaled by <paramref name="a"/>.</param>
    public static OrbitalAngularMomentum3 operator *(double a, OrbitalAngularMomentum3 b) => new(a * b.X, a * b.Y, a * b.Z);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/>.</param>
    public static OrbitalAngularMomentum3 operator /(OrbitalAngularMomentum3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    /// <summary>Produces a <see cref="OrbitalAngularMomentum3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public OrbitalAngularMomentum3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="OrbitalAngularMomentum3"/> is scaled.</param>
    public OrbitalAngularMomentum3 Multiply(Scalar factor) => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="OrbitalAngularMomentum3"/> is divided.</param>
    public OrbitalAngularMomentum3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    /// <summary>Produces a <see cref="OrbitalAngularMomentum3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is retrieved from division of <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by this value.</param>
    public static OrbitalAngularMomentum3 operator %(OrbitalAngularMomentum3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/>.</param>
    public static OrbitalAngularMomentum3 operator *(OrbitalAngularMomentum3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="OrbitalAngularMomentum3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="OrbitalAngularMomentum3"/>, which is scaled by <paramref name="a"/>.</param>
    public static OrbitalAngularMomentum3 operator *(Scalar a, OrbitalAngularMomentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/>.</param>
    public static OrbitalAngularMomentum3 operator /(OrbitalAngularMomentum3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

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
    /// <summary>Multiples the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, which is multiplied by <paramref name="b"/>.</param>
    /// <param name="b">This quantity is multiplied by the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity, Func{double, double, double, TProductVector3Quantity})"/>.</remarks>
    public static Unhandled3 operator *(OrbitalAngularMomentum3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Multiples the quantity <paramref name="a"/> by the <see cref="OrbitalAngularMomentum3"/> <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">This quantity is multiplied by the <see cref="OrbitalAngularMomentum3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="OrbitalAngularMomentum3"/>, which is multiplied by <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity, Func{double, double, double, TProductVector3Quantity})"/>.</remarks>
    public static Unhandled3 operator *(IScalarQuantity a, OrbitalAngularMomentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Divides the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">The<see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientVector3Quantity, TDivisorScalarQuantity}(TDivisorScalarQuantity, Func{double, double, double, TQuotientVector3Quantity})"/>.</remarks>
    public static Unhandled3 operator /(OrbitalAngularMomentum3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    /// <summary>Converts the <see cref="OrbitalAngularMomentum3"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with values
    /// (<see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>), when expressed in SI units.</summary>
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    /// <summary>Converts <paramref name="a"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with values
    /// (<see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>), when expressed in SI units.</summary>
    public static implicit operator (double x, double y, double z)(OrbitalAngularMomentum3 a) => (a.X, a.Y, a.Z);

    /// <summary>Converts the <see cref="OrbitalAngularMomentum3"/> to the <see cref="Vector3"/> with components of
    /// equal magnitude, when expressed in SI units.</summary>
    public Vector3 ToVector3() => new(X, Y, Z);
    /// <summary>Converts <paramref name="a"/> to the <see cref="Vector3"/> with components of
    /// equal magnitude, when expressed in SI units.</summary>
    public static explicit operator Vector3(OrbitalAngularMomentum3 a) => new(a.X, a.Y, a.Z);

    /// <summary>Constructs the <see cref="OrbitalAngularMomentum3"/> with components equal to the values of <paramref name="components"/>,
    /// when expressed in SI units.</summary>
    public static OrbitalAngularMomentum3 FromValueTuple((double x, double y, double z) components) => new(components);
    /// <summary>Constructs the <see cref="OrbitalAngularMomentum3"/> with components equal to the values of <paramref name="components"/>,
    /// when expressed in SI units.</summary>
    public static explicit operator OrbitalAngularMomentum3((double x, double y, double z) components) => new(components);

    /// <summary>Converts <paramref name="a"/> to the <see cref="OrbitalAngularMomentum3"/> with components of equal magnitude,
    /// when expressed in SI units.</summary>
    public static OrbitalAngularMomentum3 FromVector3(Vector3 a) => new(a);
    /// <summary>Converts <paramref name="a"/> to the <see cref="OrbitalAngularMomentum3"/> with components of equal magnitude,
    /// when expressed in SI units.</summary>
    public static explicit operator OrbitalAngularMomentum3(Vector3 a) => new(a);
}
