#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

/// <summary>A measure of the vector quantity <see cref="OrbitalAngularVelocity3"/>, of dimensionality three,
/// describing the <see cref="AngularVelocity3"/> of an object about an external point. The quantity is expressed in
/// <see cref="UnitOfAngularVelocity"/>, with the SI unit being [rad∙s⁻¹].
/// <para>
/// New instances of <see cref="OrbitalAngularVelocity3"/> can be constructed by multiplying a <see cref="OrbitalAngularSpeed"/> with a Vector3 or (double, double, double).
/// Instances can also be produced by combining other quantities, either through mathematical operators or using overloads of the static method 'From'.
/// Lastly, instances can be constructed from quantities sharing the same unit, using instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="OrbitalAngularVelocity3"/> a = (3, 5, 7) * <see cref="OrbitalAngularSpeed.OneRadianPerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="OrbitalAngularVelocity3"/> d = <see cref="OrbitalAngularVelocity3.From(Rotation3, Time)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="OrbitalAngularVelocity3"/> e = <see cref="AngularVelocity3.AsOrbitalAngularVelocity"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the components can be retrieved in the desired <see cref="UnitOfAngularVelocity"/> using pre-defined properties, such as <see cref="RadiansPerSecond"/>./// </para>
/// </summary>
/// <remarks>
/// <see cref="OrbitalAngularVelocity3"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="AngularVelocity3"/></term>
/// <description>Describes any type of angular velocity.</description>
/// </item>
/// <item>
/// <term><see cref="SpinAngularVelocity3"/></term>
/// <description>Describes the <see cref="AngularVelocity3"/> of an object about the internal center of rotation.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct OrbitalAngularVelocity3 :
    IVector3Quantity,
    IScalableVector3Quantity<OrbitalAngularVelocity3>,
    INormalizableVector3Quantity<OrbitalAngularVelocity3>,
    ITransformableVector3Quantity<OrbitalAngularVelocity3>,
    IMultiplicableVector3Quantity<OrbitalAngularVelocity3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<OrbitalAngularVelocity3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<OrbitalAngularSpeed, Vector3>,
    IDotableVector3Quantity<Unhandled, Unhandled3>,
    ICrossableVector3Quantity<OrbitalAngularVelocity3, Vector3>,
    ICrossableVector3Quantity<Unhandled3, Unhandled3>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    /// <summary>The zero-magnitude <see cref="OrbitalAngularVelocity3"/>.</summary>
    public static OrbitalAngularVelocity3 Zero { get; } = new(0, 0, 0);

    /// <summary>The magnitude of the X-component of the <see cref="OrbitalAngularVelocity3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAngularVelocity)"/> or a pre-defined property
    /// - such as <see cref="RadiansPerSecond"/>.</remarks>
    public double X { get; init; }
    /// <summary>The magnitude of the Y-component of the <see cref="OrbitalAngularVelocity3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAngularVelocity)"/> or a pre-defined property
    /// - such as <see cref="RadiansPerSecond"/>.</remarks>
    public double Y { get; init; }
    /// <summary>The magnitude of the Z-component of the <see cref="OrbitalAngularVelocity3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAngularVelocity)"/> or a pre-defined property
    /// - such as <see cref="RadiansPerSecond"/>.</remarks>
    public double Z { get; init; }

    /// <summary>Retrieves the magnitudes of the components of the <see cref="OrbitalAngularVelocity3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAngularVelocity)"/> or a pre-defined property
    /// - such as <see cref="RadiansPerSecond"/>.</remarks>
    public Vector3 Components => new(X, Y, Z);

    /// <summary>Constructs a new <see cref="OrbitalAngularVelocity3"/> with components <paramref name="components"/>.</summary>
    /// <param name="components">The components of the <see cref="OrbitalAngularVelocity3"/>.</param>
    public OrbitalAngularVelocity3((OrbitalAngularSpeed x, OrbitalAngularSpeed y, OrbitalAngularSpeed z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularVelocity3"/> with components (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The X-component of the <see cref="OrbitalAngularVelocity3"/>.</param>
    /// <param name="y">The Y-component of the <see cref="OrbitalAngularVelocity3"/>.</param>
    /// <param name="z">The Z-component of the <see cref="OrbitalAngularVelocity3"/>.</param>
    public OrbitalAngularVelocity3(OrbitalAngularSpeed x, OrbitalAngularSpeed y, OrbitalAngularSpeed z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularVelocity3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="OrbitalAngularVelocity3"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="unitOfAngularVelocity">The <see cref="UnitOfAngularVelocity"/> in which the magnitudes of the components, <paramref name="components"/>, are expressed.</param>
    public OrbitalAngularVelocity3((Scalar x, Scalar y, Scalar z) components, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(components.x, components.y, components.z, unitOfAngularVelocity) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularVelocity3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>),
    /// expressed in <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="OrbitalAngularVelocity3"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="OrbitalAngularVelocity3"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="OrbitalAngularVelocity3"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="unitOfAngularVelocity">The <see cref="UnitOfAngularVelocity"/> in which the magnitudes of the components,
    /// (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>), are expressed.</param>
    public OrbitalAngularVelocity3(Scalar x, Scalar y, Scalar z, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAngularVelocity) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularVelocity3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="OrbitalAngularVelocity3"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="unitOfAngularVelocity">The <see cref="UnitOfAngularVelocity"/> in which the magnitudes of the components,
    /// <paramref name="components"/>, are expressed.</param>
    public OrbitalAngularVelocity3(Vector3 components, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(components.X, components.Y, components.Z, unitOfAngularVelocity) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularVelocity3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="OrbitalAngularVelocity3"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="unitOfAngularVelocity">The <see cref="UnitOfAngularVelocity"/> in which the magnitudes of the components, <paramref name="components"/>, are expressed.</param>
    public OrbitalAngularVelocity3((double x, double y, double z) components, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(components.x, components.y, components.z, unitOfAngularVelocity) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularVelocity3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>),
    /// expressed in <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="OrbitalAngularVelocity3"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="OrbitalAngularVelocity3"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="OrbitalAngularVelocity3"/>, expressed in <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="unitOfAngularVelocity">The <see cref="UnitOfAngularVelocity"/> in which the magnitudes of the components,
    /// (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>), are expressed.</param>
    public OrbitalAngularVelocity3(double x, double y, double z, UnitOfAngularVelocity unitOfAngularVelocity) : 
    	this(x * unitOfAngularVelocity.AngularSpeed.Magnitude, y * unitOfAngularVelocity.AngularSpeed.Magnitude, z * unitOfAngularVelocity.AngularSpeed.Magnitude) { }

    /// <summary>Constructs a new <see cref="OrbitalAngularVelocity3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="OrbitalAngularVelocity3"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularVelocity3(ValueTuple{Scalar, Scalar, Scalar}, UnitOfAngularVelocity)"/>.</remarks>
    public OrbitalAngularVelocity3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularVelocity3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="OrbitalAngularVelocity3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="OrbitalAngularVelocity3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="OrbitalAngularVelocity3"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularVelocity3(Scalar, Scalar, Scalar, UnitOfAngularVelocity)"/>.</remarks>
    public OrbitalAngularVelocity3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularVelocity3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="OrbitalAngularVelocity3"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularVelocity3(Vector3, UnitOfAngularVelocity)"/>.</remarks>
    public OrbitalAngularVelocity3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularVelocity3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="OrbitalAngularVelocity3"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularVelocity3(ValueTuple{double, double, double}, UnitOfAngularVelocity)"/>.</remarks>
    public OrbitalAngularVelocity3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularVelocity3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="OrbitalAngularVelocity3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="OrbitalAngularVelocity3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="OrbitalAngularVelocity3"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularVelocity3(double, double, double, UnitOfAngularVelocity)"/>.</remarks>
    public OrbitalAngularVelocity3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>Converts the <see cref="OrbitalAngularVelocity3"/> to an instance of the associated quantity <see cref="AngularVelocity3"/>, with components of
    /// equal magnitudes.</summary>
    public AngularVelocity3 AsAngularVelocity => new(X, Y, Z);
    /// <summary>Converts the <see cref="OrbitalAngularVelocity3"/> to an instance of the associated quantity <see cref="SpinAngularVelocity3"/>, with components of
    /// equal magnitudes.</summary>
    public SpinAngularVelocity3 AsSpinAngularVelocity => new(X, Y, Z);

    /// <summary>Retrieves the magnitudes of the components of the <see cref="OrbitalAngularVelocity3"/>, expressed in <see cref="UnitOfAngularVelocity.RadianPerSecond"/>.</summary>
    public Vector3 RadiansPerSecond => InUnit(UnitOfAngularVelocity.RadianPerSecond);
    /// <summary>Retrieves the magnitudes of the components of the <see cref="OrbitalAngularVelocity3"/>, expressed in <see cref="UnitOfAngularVelocity.DegreePerSecond"/>.</summary>
    public Vector3 DegreesPerSecond => InUnit(UnitOfAngularVelocity.DegreePerSecond);
    /// <summary>Retrieves the magnitudes of the components of the <see cref="OrbitalAngularVelocity3"/>, expressed in <see cref="UnitOfAngularVelocity.RevolutionPerSecond"/>.</summary>
    public Vector3 RevolutionsPerSecond => InUnit(UnitOfAngularVelocity.RevolutionPerSecond);
    /// <summary>Retrieves the magnitudes of the components of the <see cref="OrbitalAngularVelocity3"/>, expressed in <see cref="UnitOfAngularVelocity.RevolutionPerMinute"/>.</summary>
    public Vector3 RevolutionsPerMinute => InUnit(UnitOfAngularVelocity.RevolutionPerMinute);

    /// <inheritdoc/>
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    /// <summary>Computes the magnitude, or norm, of the vector quantity <see cref="OrbitalAngularVelocity3"/>, as a <see cref="OrbitalAngularSpeed"/>.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when applicable.</remarks>
    public OrbitalAngularSpeed Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    /// <summary>Computes the square of the magnitude, or norm, of the vector quantity <see cref="OrbitalAngularVelocity3"/>.</summary>
    /// <remarks>For clarity, consider first extracting the magnitudes of the components in the desired <see cref="UnitOfAngularVelocity"/>.</remarks>
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    /// <summary>Computes the normalized <see cref="OrbitalAngularVelocity3"/> - if expressed in SI units.</summary>
    /// <remarks>Note that the resulting <see cref="OrbitalAngularVelocity3"/> will only be normalized if expressed in SI units.</remarks>
    public OrbitalAngularVelocity3 Normalize() => this / Magnitude().Magnitude;
    /// <summary>Computes the transformation of the <see cref="OrbitalAngularVelocity3"/> by <paramref name="transform"/>.</summary>
    /// <param name="transform">The <see cref="OrbitalAngularVelocity3"/> is transformed by this <see cref="Matrix4x4"/>.</param>
    public OrbitalAngularVelocity3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    /// <summary>Performs dot-multiplication of the <see cref="OrbitalAngularVelocity3"/> by <paramref name="factor"/>, resulting in a
    /// <see cref="OrbitalAngularSpeed"/>.</summary>
    /// <param name="factor">The <see cref="OrbitalAngularVelocity3"/> is dot-multiplied by this <see cref="Vector3"/>.</param>
    public OrbitalAngularSpeed Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    /// <summary>Performs dot-multiplication of the <see cref="OrbitalAngularVelocity3"/> by <paramref name="factor"/>, resulting in a
    /// <see cref="Unhandled"/>.</summary>
    /// <param name="factor">The <see cref="OrbitalAngularVelocity3"/> is dot-multiplied by this <see cref="Unhandled3"/>.</param>
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TProductScalarQuantity Dot<TProductScalarQuantity, TFactorVector3Quantity>(TFactorVector3Quantity factor, Func<double, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorVector3Quantity : IVector3Quantity
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }
        else if (factor == null)
        {
            throw new ArgumentNullException(nameof(factor));
        }
        else
        {
            return factory(Maths.Vectors.Dot(this, factor));
        }
    }

    /// <summary>Performs cross-multiplication of the <see cref="OrbitalAngularVelocity3"/> by <paramref name="factor"/>, resulting in a
    /// <see cref="OrbitalAngularVelocity3"/>.</summary>
    /// <param name="factor">The <see cref="OrbitalAngularVelocity3"/> is cross-multiplied by this <see cref="Vector3"/>.</param>
    public OrbitalAngularVelocity3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    /// <summary>Performs cross-multiplication of the <see cref="OrbitalAngularVelocity3"/> by <paramref name="factor"/>, resulting in a
    /// <see cref="Unhandled3"/>.</summary>
    /// <param name="factor">The <see cref="OrbitalAngularVelocity3"/> is cross-multiplied by this <see cref="Unhandled3"/>.</param>
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TProductVector3Quantity Cross<TProductVector3Quantity, TFactorVector3Quantity>(TFactorVector3Quantity factor, Func<(double, double, double), TProductVector3Quantity> factory)
        where TProductVector3Quantity : IVector3Quantity
        where TFactorVector3Quantity : IVector3Quantity
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }
        else if (factor == null)
        {
            throw new ArgumentNullException(nameof(factor));
        }
        else
        {
            return factory(Maths.Vectors.Cross(this, factor));
        }
    }

    /// <summary>Produces a formatted string from the magnitudes of the components of the <see cref="OrbitalAngularVelocity3"/> in the default unit
    /// <see cref="UnitOfAngularVelocity.RadianPerSecond"/>, followed by the symbol [rad∙s⁻¹].</summary>
    public override string ToString() => $"{RadiansPerSecond} [rad∙s⁻¹]";

    /// <summary>Produces a <see cref="Vector3"/> with components equal to that of the <see cref="OrbitalAngularVelocity3"/>,
    /// expressed in <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="unitOfAngularVelocity">The <see cref="UnitOfAngularVelocity"/> in which the magnitude is expressed.</param>
    public Vector3 InUnit(UnitOfAngularVelocity unitOfAngularVelocity) => InUnit(this, unitOfAngularVelocity);
    /// <summary>Produces a <see cref="Vector3"/> with components equal to that of the <see cref="OrbitalAngularVelocity3"/>,
    /// expressed in <paramref name="unitOfAngularVelocity"/>.</summary>
    /// <param name="orbitalAngularVelocity3">The <see cref="OrbitalAngularVelocity3"/> to be expressed in <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="unitOfAngularVelocity">The <see cref="UnitOfAngularVelocity"/> in which the magnitude is expressed.</param>
    private static Vector3 InUnit(OrbitalAngularVelocity3 orbitalAngularVelocity3, UnitOfAngularVelocity unitOfAngularVelocity) 
    	=> orbitalAngularVelocity3.ToVector3() / unitOfAngularVelocity.AngularSpeed.Magnitude;
    
    /// <summary>Unary plus, resulting in the unmodified <see cref="OrbitalAngularVelocity3"/>.</summary>
    public OrbitalAngularVelocity3 Plus() => this;
    /// <summary>Negation, resulting in a <see cref="OrbitalAngularVelocity3"/> with negated components.</summary>
    public OrbitalAngularVelocity3 Negate() => new(-X, -Y, -Z);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="a"/>.</summary>
    /// <param name="a">Unary plus is applied to this <see cref="OrbitalAngularVelocity3"/>.</param>
    public static OrbitalAngularVelocity3 operator +(OrbitalAngularVelocity3 a) => a;
    /// <summary>Negation, resulting in a <see cref="OrbitalAngularVelocity3"/> with negated components from that of <paramref name="a"/>.</summary>
    /// <param name="a">Negation is applied to this <see cref="OrbitalAngularVelocity3"/>.</param>
    public static OrbitalAngularVelocity3 operator -(OrbitalAngularVelocity3 a) => new(-a.X, -a.Y, -a.Z);

    /// <summary>Multiplicates the <see cref="OrbitalAngularVelocity3"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="OrbitalAngularVelocity3"/> is multiplied.</param>
    public Unhandled3 Multiply(Unhandled factor) => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    /// <summary>Divides the <see cref="OrbitalAngularVelocity3"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="OrbitalAngularVelocity3"/> is divided.</param>
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="OrbitalAngularVelocity3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/> is multiplied.</param>
    public static Unhandled3 operator *(OrbitalAngularVelocity3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="b"/> by the <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Unhandled"/> quantity by which the <see cref="OrbitalAngularVelocity3"/> <paramref name="b"/> is multiplied.</param>
    /// <param name="b">The <see cref="OrbitalAngularVelocity3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="a"/>.</param>
    public static Unhandled3 operator *(Unhandled a, OrbitalAngularVelocity3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Division of the <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="OrbitalAngularVelocity3"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/> is divded.</param>
    public static Unhandled3 operator /(OrbitalAngularVelocity3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    /// <summary>Produces a <see cref="OrbitalAngularVelocity3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public OrbitalAngularVelocity3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    /// <summary>Scales the <see cref="OrbitalAngularVelocity3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="OrbitalAngularVelocity3"/> is scaled.</param>
    public OrbitalAngularVelocity3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    /// <summary>Scales the <see cref="OrbitalAngularVelocity3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="OrbitalAngularVelocity3"/> is divided.</param>
    public OrbitalAngularVelocity3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    /// <summary>Produces a <see cref="OrbitalAngularVelocity3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="OrbitalAngularVelocity3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is produced from division of <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/> by this value.</param>
    public static OrbitalAngularVelocity3 operator %(OrbitalAngularVelocity3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    /// <summary>Scales the <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="OrbitalAngularVelocity3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/>.</param>
    public static OrbitalAngularVelocity3 operator *(OrbitalAngularVelocity3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    /// <summary>Scales the <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="OrbitalAngularVelocity3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="OrbitalAngularVelocity3"/>, which is scaled by <paramref name="a"/>.</param>
    public static OrbitalAngularVelocity3 operator *(double a, OrbitalAngularVelocity3 b) => new(a * b.X, a * b.Y, a * b.Z);
    /// <summary>Scales the <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="OrbitalAngularVelocity3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/>.</param>
    public static OrbitalAngularVelocity3 operator /(OrbitalAngularVelocity3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    /// <summary>Produces a <see cref="OrbitalAngularVelocity3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public OrbitalAngularVelocity3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    /// <summary>Scales the <see cref="OrbitalAngularVelocity3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="OrbitalAngularVelocity3"/> is scaled.</param>
    public OrbitalAngularVelocity3 Multiply(Scalar factor) => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    /// <summary>Scales the <see cref="OrbitalAngularVelocity3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="OrbitalAngularVelocity3"/> is divided.</param>
    public OrbitalAngularVelocity3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    /// <summary>Produces a <see cref="OrbitalAngularVelocity3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="OrbitalAngularVelocity3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is produced from division of <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/> by this value.</param>
    public static OrbitalAngularVelocity3 operator %(OrbitalAngularVelocity3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    /// <summary>Scales the <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="OrbitalAngularVelocity3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/>.</param>
    public static OrbitalAngularVelocity3 operator *(OrbitalAngularVelocity3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Scales the <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="OrbitalAngularVelocity3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="OrbitalAngularVelocity3"/>, which is scaled by <paramref name="a"/>.</param>
    public static OrbitalAngularVelocity3 operator *(Scalar a, OrbitalAngularVelocity3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Scales the <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="OrbitalAngularVelocity3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/>.</param>
    public static OrbitalAngularVelocity3 operator /(OrbitalAngularVelocity3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TProductVector3Quantity Multiply<TProductVector3Quantity, TFactorScalarQuantity>(TFactorScalarQuantity factor, Func<(double, double, double), TProductVector3Quantity> factory)
        where TProductVector3Quantity : IVector3Quantity
        where TFactorScalarQuantity : IScalarQuantity
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }
        else if (factor == null)
        {
            throw new ArgumentNullException(nameof(factor));
        }
        else
        {
            return factory((X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude));
        }
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TQuotientVector3Quantity Divide<TQuotientVector3Quantity, TDivisorScalarQuantity>(TDivisorScalarQuantity divisor, Func<(double, double, double), TQuotientVector3Quantity> factory)
        where TQuotientVector3Quantity : IVector3Quantity
        where TDivisorScalarQuantity : IScalarQuantity
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }
        else if (divisor == null)
        {
            throw new ArgumentNullException(nameof(divisor));
        }
        else
        {
            return factory((X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude));
        }
    }

    /// <summary>Multiplication of the <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="OrbitalAngularVelocity3"/>, which is multiplied by <paramref name="b"/>.</param>
    /// <param name="b">This quantity is multiplied by the <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{ValueTuple{double, double, double}, TProductVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(OrbitalAngularVelocity3 a, IScalarQuantity b) => a.Multiply(b, (x) => new Unhandled3(x));
    /// <summary>Multiplication of the quantity <paramref name="a"/> by the <see cref="OrbitalAngularVelocity3"/> <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">This quantity is multiplied by the <see cref="OrbitalAngularVelocity3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="OrbitalAngularVelocity3"/>, which is multiplied by <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{ValueTuple{double, double, double}, TProductVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(IScalarQuantity a, OrbitalAngularVelocity3 b) => b.Multiply(a, (x) => new Unhandled3(x));
    /// <summary>Division of the <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="OrbitalAngularVelocity3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="OrbitalAngularVelocity3"/> <paramref name="a"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientVector3Quantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{ValueTuple{double, double, double}, TQuotientVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator /(OrbitalAngularVelocity3 a, IScalarQuantity b) => a.Divide(b, (x) => new Unhandled3(x));

    /// <summary>Converts the <see cref="OrbitalAngularVelocity3"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with 
    /// values (<see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>), when expressed in SI units.</summary>
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    /// <summary>Converts <paramref name="a"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with 
    /// values (<see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>), when expressed in SI units.</summary>
    public static explicit operator (double x, double y, double z)(OrbitalAngularVelocity3 a) => (a.X, a.Y, a.Z);

    /// <summary>Converts the <see cref="OrbitalAngularVelocity3"/> to the <see cref="Vector3"/> with components of equal magnitude, when expressed in SI units.</summary>
    public Vector3 ToVector3() => new(X, Y, Z);
    /// <summary>Converts <paramref name="a"/> to the <see cref="Vector3"/> with components of equal magnitude, when expressed in SI units.</summary>
    public static explicit operator Vector3(OrbitalAngularVelocity3 a) => new(a.X, a.Y, a.Z);

    /// <summary>Constructs the <see cref="OrbitalAngularVelocity3"/> with components equal to the values of <paramref name="components"/>, 
    /// when expressed in SI units.</summary>
    public static OrbitalAngularVelocity3 FromValueTuple((double x, double y, double z) components) => new(components);
    /// <summary>Constructs the <see cref="OrbitalAngularVelocity3"/> with components equal to the values of <paramref name="components"/>, 
    /// when expressed in SI units.</summary>
    public static explicit operator OrbitalAngularVelocity3((double x, double y, double z) components) => new(components);

    /// <summary>Constructs the <see cref="OrbitalAngularVelocity3"/> with components <paramref name="a"/>, when expressed in SI units.</summary>
    public static OrbitalAngularVelocity3 FromVector3(Vector3 a) => new(a);
    /// <summary>Constructs the <see cref="OrbitalAngularVelocity3"/> with components <paramref name="a"/>, when expressed in SI units.</summary>
    public static explicit operator OrbitalAngularVelocity3(Vector3 a) => new(a);
}
