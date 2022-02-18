namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

/// <summary>A measure of the vector quantity <see cref="GravitationalAcceleration3"/>, of dimensionality three,
/// describing <see cref="Acceleration3"/> caused by gravity. The quantity is expressed in
/// <see cref="UnitOfAcceleration"/>, with the SI unit being [m∙s⁻²].
/// <para>
/// New instances of <see cref="GravitationalAcceleration3"/> can be constructed by multiplying a <see cref="GravitationalAcceleration"/> with a <see cref="Vector3"/> or (double, double, double).
/// Instances can also be produced by combining other quantities, either through mathematical operators or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="GravitationalAcceleration3"/> a = (3, 5, 7) * <see cref="GravitationalAcceleration.OneMetrePerSecondSquared"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="GravitationalAcceleration3"/> d = <see cref="GravitationalAcceleration3.From(Weight3, Mass)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the components can be retrieved in the desired <see cref="UnitOfAcceleration"/> using pre-defined properties, such as <see cref="MetresPerSecondSquared"/>.
/// </para>
/// </summary>
/// <remarks>
/// <see cref="GravitationalAcceleration3"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="Acceleration3"/></term>
/// <description>A more general form of <see cref="GravitationalAcceleration3"/>, describing any form of acceleration.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct GravitationalAcceleration3 :
    IVector3Quantity,
    IScalableVector3Quantity<GravitationalAcceleration3>,
    INormalizableVector3Quantity<GravitationalAcceleration3>,
    ITransformableVector3Quantity<GravitationalAcceleration3>,
    IMultiplicableVector3Quantity<GravitationalAcceleration3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<GravitationalAcceleration3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<GravitationalAcceleration, Vector3>,
    IDotableVector3Quantity<Unhandled, Unhandled3>,
    ICrossableVector3Quantity<GravitationalAcceleration3, Vector3>,
    ICrossableVector3Quantity<Unhandled3, Unhandled3>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    /// <summary>The zero-magnitude <see cref="GravitationalAcceleration3"/>.</summary>
    public static GravitationalAcceleration3 Zero { get; } = new(0, 0, 0);

    /// <summary>The magnitude of the X-component of the <see cref="GravitationalAcceleration3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAcceleration)"/> or a pre-defined property
    /// - such as <see cref="MetresPerSecondSquared"/>.</remarks>
    public double X { get; init; }
    /// <summary>The magnitude of the Y-component of the <see cref="GravitationalAcceleration3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAcceleration)"/> or a pre-defined property
    /// - such as <see cref="MetresPerSecondSquared"/>.</remarks>
    public double Y { get; init; }
    /// <summary>The magnitude of the Z-component of the <see cref="GravitationalAcceleration3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAcceleration)"/> or a pre-defined property
    /// - such as <see cref="MetresPerSecondSquared"/>.</remarks>
    public double Z { get; init; }

    /// <summary>Retrieves the magnitudes of the components of the <see cref="GravitationalAcceleration3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfAcceleration)"/> or a pre-defined property
    /// - such as <see cref="MetresPerSecondSquared"/>.</remarks>
    public Vector3 Components => new(X, Y, Z);

    /// <summary>Constructs a new <see cref="GravitationalAcceleration3"/> with components <paramref name="components"/>.</summary>
    /// <param name="components">The components of the <see cref="GravitationalAcceleration3"/>.</param>
    public GravitationalAcceleration3((GravitationalAcceleration x, GravitationalAcceleration y, GravitationalAcceleration z) components) : 
    	this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="GravitationalAcceleration3"/> with components (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The X-component of the <see cref="GravitationalAcceleration3"/>.</param>
    /// <param name="y">The Y-component of the <see cref="GravitationalAcceleration3"/>.</param>
    /// <param name="z">The Z-component of the <see cref="GravitationalAcceleration3"/>.</param>
    public GravitationalAcceleration3(GravitationalAcceleration x, GravitationalAcceleration y, GravitationalAcceleration z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="GravitationalAcceleration3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfAcceleration"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="GravitationalAcceleration3"/>, expressed in <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="unitOfAcceleration">The <see cref="UnitOfAcceleration"/> in which the magnitudes of the components, <paramref name="components"/>, are expressed.</param>
    public GravitationalAcceleration3((Scalar x, Scalar y, Scalar z) components, UnitOfAcceleration unitOfAcceleration) : 
    	this(components.x, components.y, components.z, unitOfAcceleration) { }
    /// <summary>Constructs a new <see cref="GravitationalAcceleration3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>),
    /// expressed in <paramref name="unitOfAcceleration"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="GravitationalAcceleration3"/>, expressed in <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="GravitationalAcceleration3"/>, expressed in <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="GravitationalAcceleration3"/>, expressed in <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="unitOfAcceleration">The <see cref="UnitOfAcceleration"/> in which the magnitudes of the components,
    /// (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>), are expressed.</param>
    public GravitationalAcceleration3(Scalar x, Scalar y, Scalar z, UnitOfAcceleration unitOfAcceleration) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfAcceleration) { }
    /// <summary>Constructs a new <see cref="GravitationalAcceleration3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfAcceleration"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="GravitationalAcceleration3"/>, expressed in <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="unitOfAcceleration">The <see cref="UnitOfAcceleration"/> in which the magnitudes of the components,
    /// <paramref name="components"/>, are expressed.</param>
    public GravitationalAcceleration3(Vector3 components, UnitOfAcceleration unitOfAcceleration) : this(components.X, components.Y, components.Z, unitOfAcceleration) { }
    /// <summary>Constructs a new <see cref="GravitationalAcceleration3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfAcceleration"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="GravitationalAcceleration3"/>, expressed in <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="unitOfAcceleration">The <see cref="UnitOfAcceleration"/> in which the magnitudes of the components, <paramref name="components"/>, are expressed.</param>
    public GravitationalAcceleration3((double x, double y, double z) components, UnitOfAcceleration unitOfAcceleration) : 
    	this(components.x, components.y, components.z, unitOfAcceleration) { }
    /// <summary>Constructs a new <see cref="GravitationalAcceleration3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>),
    /// expressed in <paramref name="unitOfAcceleration"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="GravitationalAcceleration3"/>, expressed in <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="GravitationalAcceleration3"/>, expressed in <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="GravitationalAcceleration3"/>, expressed in <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="unitOfAcceleration">The <see cref="UnitOfAcceleration"/> in which the magnitudes of the components,
    /// (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>), are expressed.</param>
    public GravitationalAcceleration3(double x, double y, double z, UnitOfAcceleration unitOfAcceleration) : 
    	this(x * unitOfAcceleration.Acceleration.Magnitude, y * unitOfAcceleration.Acceleration.Magnitude, z * unitOfAcceleration.Acceleration.Magnitude) { }

    /// <summary>Constructs a new <see cref="GravitationalAcceleration3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="GravitationalAcceleration3"/>.</param>
    /// <remarks>Consider preferring <see cref="GravitationalAcceleration3(ValueTuple{Scalar, Scalar, Scalar}, UnitOfAcceleration)"/>.</remarks>
    public GravitationalAcceleration3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="GravitationalAcceleration3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="GravitationalAcceleration3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="GravitationalAcceleration3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="GravitationalAcceleration3"/>.</param>
    /// <remarks>Consider preferring <see cref="GravitationalAcceleration3(Scalar, Scalar, Scalar, UnitOfAcceleration)"/>.</remarks>
    public GravitationalAcceleration3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="GravitationalAcceleration3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="GravitationalAcceleration3"/>.</param>
    /// <remarks>Consider preferring <see cref="GravitationalAcceleration3(Vector3, UnitOfAcceleration)"/>.</remarks>
    public GravitationalAcceleration3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    /// <summary>Constructs a new <see cref="GravitationalAcceleration3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="GravitationalAcceleration3"/>.</param>
    /// <remarks>Consider preferring <see cref="GravitationalAcceleration3(ValueTuple{double, double, double}, UnitOfAcceleration)"/>.</remarks>
    public GravitationalAcceleration3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="GravitationalAcceleration3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="GravitationalAcceleration3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="GravitationalAcceleration3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="GravitationalAcceleration3"/>.</param>
    /// <remarks>Consider preferring <see cref="GravitationalAcceleration3(double, double, double, UnitOfAcceleration)"/>.</remarks>
    public GravitationalAcceleration3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>Converts the <see cref="GravitationalAcceleration3"/> to an instance of the associated quantity <see cref="Acceleration3"/>, with components of
    /// equal magnitudes.</summary>
    public Acceleration3 AsAcceleration3() => new(X, Y, Z);

    /// <summary>Retrieves the magnitudes of the components of the <see cref="GravitationalAcceleration3"/>, expressed in <see cref="UnitOfAcceleration.MetrePerSecondSquared"/>.</summary>
    public Vector3 MetresPerSecondSquared => InUnit(UnitOfAcceleration.MetrePerSecondSquared);
    /// <summary>Retrieves the magnitudes of the components of the <see cref="GravitationalAcceleration3"/>, expressed in <see cref="UnitOfAcceleration.FootPerSecondSquared"/>.</summary>
    public Vector3 FootsPerSecondSquared => InUnit(UnitOfAcceleration.FootPerSecondSquared);
    /// <summary>Retrieves the magnitudes of the components of the <see cref="GravitationalAcceleration3"/>, expressed in <see cref="UnitOfAcceleration.StandardGravity"/>.</summary>
    public Vector3 StandardGravity => InUnit(UnitOfAcceleration.StandardGravity);

    /// <inheritdoc/>
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    /// <summary>Computes the magnitude, or norm, of the vector quantity <see cref="GravitationalAcceleration3"/>, as a <see cref="GravitationalAcceleration"/>.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when applicable.</remarks>
    public GravitationalAcceleration Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    /// <summary>Computes the square of the magnitude, or norm, of the vector quantity <see cref="GravitationalAcceleration3"/>.</summary>
    /// <remarks>For clarity, consider first extracting the magnitudes of the components in the desired <see cref="UnitOfAcceleration"/>.</remarks>
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    /// <summary>Computes the normalized <see cref="GravitationalAcceleration3"/> - if expressed in SI units.</summary>
    /// <remarks>Note that the resulting <see cref="GravitationalAcceleration3"/> will only be normalized if expressed in SI units.</remarks>
    public GravitationalAcceleration3 Normalize() => this / Magnitude().Magnitude;
    /// <summary>Computes the transformation of the <see cref="GravitationalAcceleration3"/> by <paramref name="transform"/>.</summary>
    /// <param name="transform">The <see cref="GravitationalAcceleration3"/> is transformed by this <see cref="Matrix4x4"/>.</param>
    public GravitationalAcceleration3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    /// <summary>Performs dot-multiplication of the <see cref="GravitationalAcceleration3"/> by <paramref name="factor"/>, resulting in a
    /// <see cref="GravitationalAcceleration"/>.</summary>
    /// <param name="factor">The <see cref="GravitationalAcceleration3"/> is dot-multiplied by this <see cref="Vector3"/>.</param>
    public GravitationalAcceleration Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    /// <summary>Performs dot-multiplication of the <see cref="GravitationalAcceleration3"/> by <paramref name="factor"/>, resulting in a
    /// <see cref="Unhandled"/>.</summary>
    /// <param name="factor">The <see cref="GravitationalAcceleration3"/> is dot-multiplied by this <see cref="Unhandled3"/>.</param>
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TProductScalarQuantity Dot<TProductScalarQuantity, TFactorVector3Quantity>(TFactorVector3Quantity factor, Func<Scalar, TProductScalarQuantity> factory)
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

    /// <summary>Performs cross-multiplication of the <see cref="GravitationalAcceleration3"/> by <paramref name="factor"/>, resulting in a
    /// <cref see="GravitationalAcceleration3"/>.</summary>
    /// <param name="factor">The <see cref="GravitationalAcceleration3"/> is cross-multiplied by this <see cref="Vector3"/>.</param>
    public GravitationalAcceleration3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    /// <summary>Performs cross-multiplication of the <see cref="GravitationalAcceleration3"/> by <paramref name="factor"/>, resulting in a
    /// <see cref="Unhandled3"/>.</summary>
    /// <param name="factor">The <see cref="GravitationalAcceleration3"/> is cross-multiplied by this <see cref="Unhandled3"/>.</param>
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TProductVector3Quantity Cross<TProductVector3Quantity, TFactorVector3Quantity>(TFactorVector3Quantity factor, Func<Vector3, TProductVector3Quantity> factory)
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

    /// <summary>Produces a formatted string from the magnitudes of the components of the <see cref="GravitationalAcceleration3"/> in the default unit
    /// <see cref="UnitOfAcceleration.StandardGravity"/>, followed by the symbol [g].</summary>
    public override string ToString() => $"{StandardGravity} [g]";

    /// <summary>Produces a <see cref="Vector3"/> with components equal to that of the <see cref="GravitationalAcceleration3"/>,
    /// expressed in <paramref name="unitOfAcceleration"/>.</summary>
    /// <param name="unitOfAcceleration">The <see cref="UnitOfAcceleration"/> in which the magnitude is expressed.</param>
    public Vector3 InUnit(UnitOfAcceleration unitOfAcceleration) => InUnit(this, unitOfAcceleration);
    /// <summary>Produces a <see cref="Vector3"/> with components equal to that of the <see cref="GravitationalAcceleration3"/>,
    /// expressed in <paramref name="unitOfAcceleration"/>.</summary>
    /// <param name="gravitationalAcceleration3">The <see cref="GravitationalAcceleration3"/> to be expressed in <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="unitOfAcceleration">The <see cref="UnitOfAcceleration"/> in which the magnitude is expressed.</param>
    private static Vector3 InUnit(GravitationalAcceleration3 gravitationalAcceleration3, UnitOfAcceleration unitOfAcceleration) 
    	=> gravitationalAcceleration3.ToVector3() / unitOfAcceleration.Acceleration.Magnitude;
    
    /// <summary>Unary plus, resulting in the unmodified <see cref="GravitationalAcceleration3"/>.</summary>
    public GravitationalAcceleration3 Plus() => this;
    /// <summary>Negation, resulting in a <see cref="GravitationalAcceleration3"/> with negated components.</summary>
    public GravitationalAcceleration3 Negate() => new(-X, -Y, -Z);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="a"/>.</summary>
    /// <param name="a">Unary plus is applied to this <see cref="GravitationalAcceleration3"/>.</param>
    public static GravitationalAcceleration3 operator +(GravitationalAcceleration3 a) => a;
    /// <summary>Negation, resulting in a <see cref="GravitationalAcceleration3"/> with negated components from that of <paramref name="a"/>.</summary>
    /// <param name="a">Negation is applied to this <see cref="GravitationalAcceleration3"/>.</param>
    public static GravitationalAcceleration3 operator -(GravitationalAcceleration3 a) => new(-a.X, -a.Y, -a.Z);

    /// <summary>Multiplicates the <see cref="GravitationalAcceleration3"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="GravitationalAcceleration3"/> is multiplied.</param>
    public Unhandled3 Multiply(Unhandled factor) => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    /// <summary>Divides the <see cref="GravitationalAcceleration3"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="GravitationalAcceleration3"/> is divided.</param>
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="GravitationalAcceleration3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="GravitationalAcceleration3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="GravitationalAcceleration3"/> <paramref name="a"/> is multiplied.</param>
    public static Unhandled3 operator *(GravitationalAcceleration3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="b"/> by the <see cref="GravitationalAcceleration3"/> <paramref name="a"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Unhandled"/> quantity by which the <see cref="GravitationalAcceleration3"/> <paramref name="b"/> is multiplied.</param>
    /// <param name="b">The <see cref="GravitationalAcceleration3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="a"/>.</param>
    public static Unhandled3 operator *(Unhandled a, GravitationalAcceleration3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Division of the <see cref="GravitationalAcceleration3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="GravitationalAcceleration3"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="GravitationalAcceleration3"/> <paramref name="a"/> is divded.</param>
    public static Unhandled3 operator /(GravitationalAcceleration3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    /// <summary>Produces a <see cref="GravitationalAcceleration3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public GravitationalAcceleration3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    /// <summary>Scales the <see cref="GravitationalAcceleration3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="GravitationalAcceleration3"/> is scaled.</param>
    public GravitationalAcceleration3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    /// <summary>Scales the <see cref="GravitationalAcceleration3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="GravitationalAcceleration3"/> is divided.</param>
    public GravitationalAcceleration3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    /// <summary>Produces a <see cref="GravitationalAcceleration3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="GravitationalAcceleration3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is produced from division of <see cref="GravitationalAcceleration3"/> <paramref name="a"/> by this value.</param>
    public static GravitationalAcceleration3 operator %(GravitationalAcceleration3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    /// <summary>Scales the <see cref="GravitationalAcceleration3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="GravitationalAcceleration3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="GravitationalAcceleration3"/> <paramref name="a"/>.</param>
    public static GravitationalAcceleration3 operator *(GravitationalAcceleration3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    /// <summary>Scales the <see cref="GravitationalAcceleration3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="GravitationalAcceleration3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="GravitationalAcceleration3"/>, which is scaled by <paramref name="a"/>.</param>
    public static GravitationalAcceleration3 operator *(double a, GravitationalAcceleration3 b) => new(a * b.X, a * b.Y, a * b.Z);
    /// <summary>Scales the <see cref="GravitationalAcceleration3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="GravitationalAcceleration3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="GravitationalAcceleration3"/> <paramref name="a"/>.</param>
    public static GravitationalAcceleration3 operator /(GravitationalAcceleration3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    /// <summary>Produces a <see cref="GravitationalAcceleration3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public GravitationalAcceleration3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    /// <summary>Scales the <see cref="GravitationalAcceleration3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="GravitationalAcceleration3"/> is scaled.</param>
    public GravitationalAcceleration3 Multiply(Scalar factor) => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    /// <summary>Scales the <see cref="GravitationalAcceleration3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="GravitationalAcceleration3"/> is divided.</param>
    public GravitationalAcceleration3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    /// <summary>Produces a <see cref="GravitationalAcceleration3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="GravitationalAcceleration3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is produced from division of <see cref="GravitationalAcceleration3"/> <paramref name="a"/> by this value.</param>
    public static GravitationalAcceleration3 operator %(GravitationalAcceleration3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    /// <summary>Scales the <see cref="GravitationalAcceleration3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="GravitationalAcceleration3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="GravitationalAcceleration3"/> <paramref name="a"/>.</param>
    public static GravitationalAcceleration3 operator *(GravitationalAcceleration3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Scales the <see cref="GravitationalAcceleration3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="GravitationalAcceleration3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="GravitationalAcceleration3"/>, which is scaled by <paramref name="a"/>.</param>
    public static GravitationalAcceleration3 operator *(Scalar a, GravitationalAcceleration3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Scales the <see cref="GravitationalAcceleration3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="GravitationalAcceleration3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="GravitationalAcceleration3"/> <paramref name="a"/>.</param>
    public static GravitationalAcceleration3 operator /(GravitationalAcceleration3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TProductVector3Quantity Multiply<TProductVector3Quantity, TFactorScalarQuantity>(TFactorScalarQuantity factor, Func<double, double, double, TProductVector3Quantity> factory)
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
            return factory(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
        }
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public TQuotientVector3Quantity Divide<TQuotientVector3Quantity, TDivisorScalarQuantity>(TDivisorScalarQuantity divisor, Func<double, double, double, TQuotientVector3Quantity> factory)
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
            return factory(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
        }
    }

    /// <summary>Multiplication of the <see cref="GravitationalAcceleration3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="GravitationalAcceleration3"/>, which is multiplied by <paramref name="b"/>.</param>
    /// <param name="b">This quantity is multiplied by the <see cref="GravitationalAcceleration3"/> <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, double, double, TProductVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(GravitationalAcceleration3 a, IScalarQuantity b) => a.Multiply(b, (x, y, z) => new Unhandled3(x, y, z));
    /// <summary>Multiplication of the quantity <paramref name="a"/> by the <see cref="GravitationalAcceleration3"/> <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">This quantity is multiplied by the <see cref="GravitationalAcceleration3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="GravitationalAcceleration3"/>, which is multiplied by <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, double, double, TProductVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(IScalarQuantity a, GravitationalAcceleration3 b) => b.Multiply(a, (x, y, z) => new Unhandled3(x, y, z));
    /// <summary>Division of the <see cref="GravitationalAcceleration3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="GravitationalAcceleration3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="GravitationalAcceleration3"/> <paramref name="a"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientVector3Quantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, double, double, TQuotientVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator /(GravitationalAcceleration3 a, IScalarQuantity b) => a.Divide(b, (x, y, z) => new Unhandled3(x, y, z));

    /// <summary>Converts the <see cref="GravitationalAcceleration3"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with 
    /// values (<see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>), when expressed in SI units.</summary>
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    /// <summary>Converts <paramref name="a"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with 
    /// values (<see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>), when expressed in SI units.</summary>
    public static explicit operator (double x, double y, double z)(GravitationalAcceleration3 a) => (a.X, a.Y, a.Z);

    /// <summary>Converts the <see cref="GravitationalAcceleration3"/> to the <see cref="Vector3"/> with components of equal magnitude, when expressed in SI units.</summary>
    public Vector3 ToVector3() => new(X, Y, Z);
    /// <summary>Converts <paramref name="a"/> to the <see cref="Vector3"/> with components of equal magnitude, when expressed in SI units.</summary>
    public static explicit operator Vector3(GravitationalAcceleration3 a) => new(a.X, a.Y, a.Z);

    /// <summary>Constructs the <see cref="GravitationalAcceleration3"/> with components equal to the values of <paramref name="components"/>, 
    /// when expressed in SI units.</summary>
    public static GravitationalAcceleration3 FromValueTuple((double x, double y, double z) components) => new(components);
    /// <summary>Constructs the <see cref="GravitationalAcceleration3"/> with components equal to the values of <paramref name="components"/>, 
    /// when expressed in SI units.</summary>
    public static explicit operator GravitationalAcceleration3((double x, double y, double z) components) => new(components);

    /// <summary>Constructs the <see cref="GravitationalAcceleration3"/> with components <paramref name="a"/>, when expressed in SI units.</summary>
    public static GravitationalAcceleration3 FromVector3(Vector3 a) => new(a);
    /// <summary>Constructs the <see cref="GravitationalAcceleration3"/> with components <paramref name="a"/>, when expressed in SI units.</summary>
    public static explicit operator GravitationalAcceleration3(Vector3 a) => new(a);
}
