#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

/// <summary>A measure of the vector quantity <see cref="OrbitalAngularMomentum3"/>, of dimensionality three,
/// a property of an object with <see cref="Mass"/> rotating about an external point. The quantity is expressed in
/// <see cref="UnitOfAngularMomentum"/>, with the SI unit being [kg∙m²∙s⁻¹].
/// <para>
/// New instances of <see cref="OrbitalAngularMomentum3"/> can be constructed by multiplying a <see cref="OrbitalAngularMomentum"/> with a Vector3 or (double, double, double).
/// Instances can also be produced by combining other quantities, either through mathematical operators or using overloads of the static method 'From'.
/// Lastly, instances can be constructed from quantities sharing the same unit, using instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code><see cref="OrbitalAngularMomentum3"/> a = (3, 5, 7) * <see cref="OrbitalAngularMomentum.OneKilogramSquareMetrePerSecond"/>;</code>
/// </item>
/// <item>
/// <code>
/// <see cref="OrbitalAngularMomentum3"/> d = <see cref="OrbitalAngularMomentum3.From(MomentOfInertia, OrbitalAngularVelocity3)"/>;
/// </code>
/// </item>
/// <item>
/// <code><see cref="OrbitalAngularMomentum3"/> e = <see cref="AngularMomentum3.AsOrbitalAngularMomentum"/>;</code>
/// </item>
/// </list>
/// The magnitude of the components can be retrieved in the desired <see cref="UnitOfAngularMomentum"/> using pre-defined properties, such as <see cref="KilogramSquareMetresPerSecond"/>./// </para>
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
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAngularMomentum)"/> or a pre-defined property
    /// - such as <see cref="KilogramSquareMetresPerSecond"/>.</remarks>
    public double MagnitudeX { get; init; }
    /// <summary>The magnitude of the Y-component of the <see cref="OrbitalAngularMomentum3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAngularMomentum)"/> or a pre-defined property
    /// - such as <see cref="KilogramSquareMetresPerSecond"/>.</remarks>
    public double MagnitudeY { get; init; }
    /// <summary>The magnitude of the Z-component of the <see cref="OrbitalAngularMomentum3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAngularMomentum)"/> or a pre-defined property
    /// - such as <see cref="KilogramSquareMetresPerSecond"/>.</remarks>
    public double MagnitudeZ { get; init; }

    /// <summary>Retrieves the magnitudes of the components of the <see cref="OrbitalAngularMomentum3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAngularMomentum)"/> or a pre-defined property
    /// - such as <see cref="KilogramSquareMetresPerSecond"/>.</remarks>
    public Vector3 Components => new(MagnitudeX, MagnitudeY, MagnitudeZ);

    /// <summary>The X-component of the <see cref="OrbitalAngularMomentum3"/>.</summary>
    public OrbitalAngularMomentum X => new(MagnitudeX);
    /// <summary>The Y-component of the <see cref="OrbitalAngularMomentum3"/>.</summary>
    public OrbitalAngularMomentum Y => new(MagnitudeY);
    /// <summary>The Z-component of the <see cref="OrbitalAngularMomentum3"/>.</summary>
    public OrbitalAngularMomentum Z => new(MagnitudeZ);

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
    /// expressed in <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="OrbitalAngularMomentum3"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitudes of the components, <paramref name="components"/>, are expressed.</param>
    public OrbitalAngularMomentum3((Scalar x, Scalar y, Scalar z) components, UnitOfAngularMomentum unitOfAngularMomentum) : 
    	this(components.x, components.y, components.z, unitOfAngularMomentum) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>),
    /// expressed in <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="OrbitalAngularMomentum3"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="OrbitalAngularMomentum3"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="OrbitalAngularMomentum3"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitudes of the components,
    /// (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>), are expressed.</param>
    public OrbitalAngularMomentum3(Scalar x, Scalar y, Scalar z, UnitOfAngularMomentum unitOfAngularMomentum) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAngularMomentum) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="OrbitalAngularMomentum3"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitudes of the components,
    /// <paramref name="components"/>, are expressed.</param>
    public OrbitalAngularMomentum3(Vector3 components, UnitOfAngularMomentum unitOfAngularMomentum) : 
    	this(components.MagnitudeX, components.MagnitudeY, components.MagnitudeZ, unitOfAngularMomentum) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="OrbitalAngularMomentum3"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitudes of the components, <paramref name="components"/>, are expressed.</param>
    public OrbitalAngularMomentum3((double x, double y, double z) components, UnitOfAngularMomentum unitOfAngularMomentum) : 
    	this(components.x, components.y, components.z, unitOfAngularMomentum) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>),
    /// expressed in <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="OrbitalAngularMomentum3"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="OrbitalAngularMomentum3"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="OrbitalAngularMomentum3"/>, expressed in <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitudes of the components,
    /// (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>), are expressed.</param>
    public OrbitalAngularMomentum3(double x, double y, double z, UnitOfAngularMomentum unitOfAngularMomentum) : 
    	this(x * unitOfAngularMomentum.AngularMomentum.Magnitude, y * unitOfAngularMomentum.AngularMomentum.Magnitude, z * unitOfAngularMomentum.AngularMomentum.Magnitude) { }

    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularMomentum3(ValueTuple{Scalar, Scalar, Scalar}, UnitOfAngularMomentum)"/>.</remarks>
    public OrbitalAngularMomentum3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularMomentum3(Scalar, Scalar, Scalar, UnitOfAngularMomentum)"/>.</remarks>
    public OrbitalAngularMomentum3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularMomentum3(Vector3, UnitOfAngularMomentum)"/>.</remarks>
    public OrbitalAngularMomentum3(Vector3 components) : this(components.MagnitudeX, components.MagnitudeY, components.MagnitudeZ) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularMomentum3(ValueTuple{double, double, double}, UnitOfAngularMomentum)"/>.</remarks>
    public OrbitalAngularMomentum3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="OrbitalAngularMomentum3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="OrbitalAngularMomentum3"/>.</param>
    /// <remarks>Consider preferring <see cref="OrbitalAngularMomentum3(double, double, double, UnitOfAngularMomentum)"/>.</remarks>
    public OrbitalAngularMomentum3(double x, double y, double z)
    {
        MagnitudeX = x;
        MagnitudeY = y;
        MagnitudeZ = z;
    }

    /// <summary>Converts the <see cref="OrbitalAngularMomentum3"/> to an instance of the associated quantity <see cref="AngularMomentum3"/>, with components of
    /// equal magnitudes.</summary>
    public AngularMomentum3 AsAngularMomentum => new(MagnitudeX, MagnitudeY, MagnitudeZ);
    /// <summary>Converts the <see cref="OrbitalAngularMomentum3"/> to an instance of the associated quantity <see cref="SpinAngularMomentum3"/>, with components of
    /// equal magnitudes.</summary>
    public SpinAngularMomentum3 AsSpinAngularMomentum => new(MagnitudeX, MagnitudeY, MagnitudeZ);

    /// <summary>Retrieves the magnitudes of the components of the <see cref="OrbitalAngularMomentum3"/>, expressed in <see cref="UnitOfAngularMomentum.KilogramSquareMetrePerSecond"/>.</summary>
    public Vector3 KilogramSquareMetresPerSecond => InUnit(UnitOfAngularMomentum.KilogramSquareMetrePerSecond);

    /// <inheritdoc/>
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    /// <summary>Computes the magnitude, or norm, of the vector quantity <see cref="OrbitalAngularMomentum3"/>, as a <see cref="OrbitalAngularMomentum"/>.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when applicable.</remarks>
    public OrbitalAngularMomentum Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    /// <summary>Computes the square of the magnitude, or norm, of the vector quantity <see cref="OrbitalAngularMomentum3"/>.</summary>
    /// <remarks>For clarity, consider first extracting the magnitudes of the components in the desired <see cref="UnitOfAngularMomentum"/>.</remarks>
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    /// <summary>Computes the normalized <see cref="OrbitalAngularMomentum3"/> - if expressed in SI units.</summary>
    /// <remarks>Note that the resulting <see cref="OrbitalAngularMomentum3"/> will only be normalized if expressed in SI units.</remarks>
    public OrbitalAngularMomentum3 Normalize() => this / Magnitude().Magnitude;
    /// <summary>Computes the transformation of the <see cref="OrbitalAngularMomentum3"/> by <paramref name="transform"/>.</summary>
    /// <param name="transform">The <see cref="OrbitalAngularMomentum3"/> is transformed by this <see cref="Matrix4x4"/>.</param>
    public OrbitalAngularMomentum3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    /// <summary>Performs dot-multiplication of the <see cref="OrbitalAngularMomentum3"/> by <paramref name="factor"/>, resulting in a
    /// <see cref="OrbitalAngularMomentum"/>.</summary>
    /// <param name="factor">The <see cref="OrbitalAngularMomentum3"/> is dot-multiplied by this <see cref="Vector3"/>.</param>
    public OrbitalAngularMomentum Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    /// <summary>Performs dot-multiplication of the <see cref="OrbitalAngularMomentum3"/> by <paramref name="factor"/>, resulting in a
    /// <see cref="Unhandled"/>.</summary>
    /// <param name="factor">The <see cref="OrbitalAngularMomentum3"/> is dot-multiplied by this <see cref="Unhandled3"/>.</param>
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TProductScalarQuantity Dot<TProductScalarQuantity, TFactorVector3Quantity>(TFactorVector3Quantity factor, Func<double, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorVector3Quantity : IVector3Quantity
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        ArgumentNullException.ThrowIfNull(factor, nameof(factor));

        return factory(Maths.Vectors.Dot(this, factor));
    }

    /// <summary>Performs cross-multiplication of the <see cref="OrbitalAngularMomentum3"/> by <paramref name="factor"/>, resulting in a
    /// <see cref="OrbitalAngularMomentum3"/>.</summary>
    /// <param name="factor">The <see cref="OrbitalAngularMomentum3"/> is cross-multiplied by this <see cref="Vector3"/>.</param>
    public OrbitalAngularMomentum3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    /// <summary>Performs cross-multiplication of the <see cref="OrbitalAngularMomentum3"/> by <paramref name="factor"/>, resulting in a
    /// <see cref="Unhandled3"/>.</summary>
    /// <param name="factor">The <see cref="OrbitalAngularMomentum3"/> is cross-multiplied by this <see cref="Unhandled3"/>.</param>
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TProductVector3Quantity Cross<TProductVector3Quantity, TFactorVector3Quantity>(TFactorVector3Quantity factor, Func<(double, double, double), TProductVector3Quantity> factory)
        where TProductVector3Quantity : IVector3Quantity
        where TFactorVector3Quantity : IVector3Quantity
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        ArgumentNullException.ThrowIfNull(factor, nameof(factor));

        return factory(Maths.Vectors.Cross(this, factor));
    }

    /// <summary>Produces a formatted string from the magnitudes of the components of the <see cref="OrbitalAngularMomentum3"/> in the default unit
    /// <see cref="UnitOfAngularMomentum.KilogramSquareMetrePerSecond"/>, followed by the symbol [kg∙m²∙s⁻¹].</summary>
    public override string ToString() => $"{KilogramSquareMetresPerSecond} [kg∙m²∙s⁻¹]";

    /// <summary>Produces a <see cref="Vector3"/> with components equal to that of the <see cref="OrbitalAngularMomentum3"/>,
    /// expressed in <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitude is expressed.</param>
    public Vector3 InUnit(UnitOfAngularMomentum unitOfAngularMomentum) => InUnit(this, unitOfAngularMomentum);
    /// <summary>Produces a <see cref="Vector3"/> with components equal to that of the <see cref="OrbitalAngularMomentum3"/>,
    /// expressed in <paramref name="unitOfAngularMomentum"/>.</summary>
    /// <param name="orbitalAngularMomentum3">The <see cref="OrbitalAngularMomentum3"/> to be expressed in <paramref name="unitOfAngularMomentum"/>.</param>
    /// <param name="unitOfAngularMomentum">The <see cref="UnitOfAngularMomentum"/> in which the magnitude is expressed.</param>
    private static Vector3 InUnit(OrbitalAngularMomentum3 orbitalAngularMomentum3, UnitOfAngularMomentum unitOfAngularMomentum) 
    	=> orbitalAngularMomentum3.ToVector3() / unitOfAngularMomentum.AngularMomentum.Magnitude;
    
    /// <summary>Unary plus, resulting in the unmodified <see cref="OrbitalAngularMomentum3"/>.</summary>
    public OrbitalAngularMomentum3 Plus() => this;
    /// <summary>Negation, resulting in a <see cref="OrbitalAngularMomentum3"/> with negated components.</summary>
    public OrbitalAngularMomentum3 Negate() => new(-MagnitudeX, -MagnitudeY, -MagnitudeZ);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="a"/>.</summary>
    /// <param name="a">Unary plus is applied to this <see cref="OrbitalAngularMomentum3"/>.</param>
    public static OrbitalAngularMomentum3 operator +(OrbitalAngularMomentum3 a) => a;
    /// <summary>Negation, resulting in a <see cref="OrbitalAngularMomentum3"/> with negated components from that of <paramref name="a"/>.</summary>
    /// <param name="a">Negation is applied to this <see cref="OrbitalAngularMomentum3"/>.</param>
    public static OrbitalAngularMomentum3 operator -(OrbitalAngularMomentum3 a) => new(-a.MagnitudeX, -a.MagnitudeY, -a.MagnitudeZ);

    /// <summary>Multiplicates the <see cref="OrbitalAngularMomentum3"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="OrbitalAngularMomentum3"/> is multiplied.</param>
    public Unhandled3 Multiply(Unhandled factor) => new(MagnitudeX * factor.Magnitude, MagnitudeY * factor.Magnitude, MagnitudeZ * factor.Magnitude);
    /// <summary>Divides the <see cref="OrbitalAngularMomentum3"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="OrbitalAngularMomentum3"/> is divided.</param>
    public Unhandled3 Divide(Unhandled divisor) => new(MagnitudeX / divisor.Magnitude, MagnitudeY / divisor.Magnitude, MagnitudeZ / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> is multiplied.</param>
    public static Unhandled3 operator *(OrbitalAngularMomentum3 a, Unhandled b) => new(a.MagnitudeX * b.Magnitude, a.MagnitudeY * b.Magnitude, a.MagnitudeZ * b.Magnitude);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="b"/> by the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Unhandled"/> quantity by which the <see cref="OrbitalAngularMomentum3"/> <paramref name="b"/> is multiplied.</param>
    /// <param name="b">The <see cref="OrbitalAngularMomentum3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="a"/>.</param>
    public static Unhandled3 operator *(Unhandled a, OrbitalAngularMomentum3 b) => new(a.Magnitude * b.MagnitudeX, a.Magnitude * b.MagnitudeY, a.Magnitude * b.MagnitudeZ);
    /// <summary>Division of the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> is divded.</param>
    public static Unhandled3 operator /(OrbitalAngularMomentum3 a, Unhandled b) => new(a.MagnitudeX / b.Magnitude, a.MagnitudeY / b.Magnitude, a.MagnitudeZ / b.Magnitude);

    /// <summary>Produces a <see cref="OrbitalAngularMomentum3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public OrbitalAngularMomentum3 Remainder(double divisor) => new(MagnitudeX % divisor, MagnitudeY % divisor, MagnitudeZ % divisor);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="OrbitalAngularMomentum3"/> is scaled.</param>
    public OrbitalAngularMomentum3 Multiply(double factor) => new(MagnitudeX * factor, MagnitudeY * factor, MagnitudeZ * factor);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="OrbitalAngularMomentum3"/> is divided.</param>
    public OrbitalAngularMomentum3 Divide(double divisor) => new(MagnitudeX / divisor, MagnitudeY / divisor, MagnitudeZ / divisor);
    /// <summary>Produces a <see cref="OrbitalAngularMomentum3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is produced from division of <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by this value.</param>
    public static OrbitalAngularMomentum3 operator %(OrbitalAngularMomentum3 a, double b) => new(a.MagnitudeX % b, a.MagnitudeY % b, a.MagnitudeZ % b);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/>.</param>
    public static OrbitalAngularMomentum3 operator *(OrbitalAngularMomentum3 a, double b) => new(a.MagnitudeX * b, a.MagnitudeY * b, a.MagnitudeZ * b);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="OrbitalAngularMomentum3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="OrbitalAngularMomentum3"/>, which is scaled by <paramref name="a"/>.</param>
    public static OrbitalAngularMomentum3 operator *(double a, OrbitalAngularMomentum3 b) => new(a * b.MagnitudeX, a * b.MagnitudeY, a * b.MagnitudeZ);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/>.</param>
    public static OrbitalAngularMomentum3 operator /(OrbitalAngularMomentum3 a, double b) => new(a.MagnitudeX / b, a.MagnitudeY / b, a.MagnitudeZ / b);

    /// <summary>Produces a <see cref="OrbitalAngularMomentum3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public OrbitalAngularMomentum3 Remainder(Scalar divisor) => new(MagnitudeX % divisor.Magnitude, MagnitudeY % divisor.Magnitude, MagnitudeZ % divisor.Magnitude);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="OrbitalAngularMomentum3"/> is scaled.</param>
    public OrbitalAngularMomentum3 Multiply(Scalar factor) => new(MagnitudeX * factor.Magnitude, MagnitudeY * factor.Magnitude, MagnitudeZ * factor.Magnitude);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="OrbitalAngularMomentum3"/> is divided.</param>
    public OrbitalAngularMomentum3 Divide(Scalar divisor) => new(MagnitudeX / divisor.Magnitude, MagnitudeY / divisor.Magnitude, MagnitudeZ / divisor.Magnitude);
    /// <summary>Produces a <see cref="OrbitalAngularMomentum3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is produced from division of <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by this value.</param>
    public static OrbitalAngularMomentum3 operator %(OrbitalAngularMomentum3 a, Scalar b) => new(a.MagnitudeX % b.Magnitude, a.MagnitudeY % b.Magnitude, a.MagnitudeZ % b.Magnitude);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/>.</param>
    public static OrbitalAngularMomentum3 operator *(OrbitalAngularMomentum3 a, Scalar b) => new(a.MagnitudeX * b.Magnitude, a.MagnitudeY * b.Magnitude, a.MagnitudeZ * b.Magnitude);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="OrbitalAngularMomentum3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="OrbitalAngularMomentum3"/>, which is scaled by <paramref name="a"/>.</param>
    public static OrbitalAngularMomentum3 operator *(Scalar a, OrbitalAngularMomentum3 b) => new(a.Magnitude * b.MagnitudeX, a.Magnitude * b.MagnitudeY, a.Magnitude * b.MagnitudeZ);
    /// <summary>Scales the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/>.</param>
    public static OrbitalAngularMomentum3 operator /(OrbitalAngularMomentum3 a, Scalar b) => new(a.MagnitudeX / b.Magnitude, a.MagnitudeY / b.Magnitude, a.MagnitudeZ / b.Magnitude);

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TProductVector3Quantity Multiply<TProductVector3Quantity, TFactorScalarQuantity>(TFactorScalarQuantity factor, Func<(double, double, double), TProductVector3Quantity> factory)
        where TProductVector3Quantity : IVector3Quantity
        where TFactorScalarQuantity : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        ArgumentNullException.ThrowIfNull(factor, nameof(factor));

        return factory((MagnitudeX * factor.Magnitude, MagnitudeY * factor.Magnitude, MagnitudeZ * factor.Magnitude));
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TQuotientVector3Quantity Divide<TQuotientVector3Quantity, TDivisorScalarQuantity>(TDivisorScalarQuantity divisor, Func<(double, double, double), TQuotientVector3Quantity> factory)
        where TQuotientVector3Quantity : IVector3Quantity
        where TDivisorScalarQuantity : IScalarQuantity
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        ArgumentNullException.ThrowIfNull(divisor, nameof(divisor));

        return factory((MagnitudeX / divisor.Magnitude, MagnitudeY / divisor.Magnitude, MagnitudeZ / divisor.Magnitude));
    }

    /// <summary>Multiplication of the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, which is multiplied by <paramref name="b"/>.</param>
    /// <param name="b">This quantity is multiplied by the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{ValueTuple{double, double, double}, TProductVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(OrbitalAngularMomentum3 a, IScalarQuantity b) => a.Multiply(b, (x) => new Unhandled3(x));
    /// <summary>Multiplication of the quantity <paramref name="a"/> by the <see cref="OrbitalAngularMomentum3"/> <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">This quantity is multiplied by the <see cref="OrbitalAngularMomentum3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="OrbitalAngularMomentum3"/>, which is multiplied by <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{ValueTuple{double, double, double}, TProductVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(IScalarQuantity a, OrbitalAngularMomentum3 b) => b.Multiply(a, (x) => new Unhandled3(x));
    /// <summary>Division of the <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="OrbitalAngularMomentum3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="OrbitalAngularMomentum3"/> <paramref name="a"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientVector3Quantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{ValueTuple{double, double, double}, TQuotientVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator /(OrbitalAngularMomentum3 a, IScalarQuantity b) => a.Divide(b, (x) => new Unhandled3(x));

    /// <summary>Converts the <see cref="OrbitalAngularMomentum3"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with 
    /// values (<see cref="MagnitudeX"/>, <see cref="MagnitudeY"/>, <see cref="MagnitudeZ"/>), when expressed in SI units.</summary>
    public (double x, double y, double z) ToValueTuple() => (MagnitudeX, MagnitudeY, MagnitudeZ);
    /// <summary>Converts <paramref name="a"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with 
    /// values (<see cref="MagnitudeX"/>, <see cref="MagnitudeY"/>, <see cref="MagnitudeZ"/>), when expressed in SI units.</summary>
    public static explicit operator (double x, double y, double z)(OrbitalAngularMomentum3 a) => (a.MagnitudeX, a.MagnitudeY, a.MagnitudeZ);

    /// <summary>Converts the <see cref="OrbitalAngularMomentum3"/> to the <see cref="Vector3"/> with components of equal magnitude, when expressed in SI units.</summary>
    public Vector3 ToVector3() => new(MagnitudeX, MagnitudeY, MagnitudeZ);
    /// <summary>Converts <paramref name="a"/> to the <see cref="Vector3"/> with components of equal magnitude, when expressed in SI units.</summary>
    public static explicit operator Vector3(OrbitalAngularMomentum3 a) => new(a.MagnitudeX, a.MagnitudeY, a.MagnitudeZ);

    /// <summary>Constructs the <see cref="OrbitalAngularMomentum3"/> with components equal to the values of <paramref name="components"/>, 
    /// when expressed in SI units.</summary>
    public static OrbitalAngularMomentum3 FromValueTuple((double x, double y, double z) components) => new(components);
    /// <summary>Constructs the <see cref="OrbitalAngularMomentum3"/> with components equal to the values of <paramref name="components"/>, 
    /// when expressed in SI units.</summary>
    public static explicit operator OrbitalAngularMomentum3((double x, double y, double z) components) => new(components);

    /// <summary>Constructs the <see cref="OrbitalAngularMomentum3"/> with components <paramref name="a"/>, when expressed in SI units.</summary>
    public static OrbitalAngularMomentum3 FromVector3(Vector3 a) => new(a);
    /// <summary>Constructs the <see cref="OrbitalAngularMomentum3"/> with components <paramref name="a"/>, when expressed in SI units.</summary>
    public static explicit operator OrbitalAngularMomentum3(Vector3 a) => new(a);
}
