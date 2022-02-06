namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

/// <summary>A measure of the vector quantity <see cref="TemperatureGradient3"/>, of dimensionality three,
/// describing a <see cref="TemperatureDifference"/> over some <see cref="Displacement3"/>. The quantity is expressed
/// in <see cref="UnitOfTemperatureGradient"/>, with the SI unit being [K / m].
/// <para>
/// New instances of <see cref="TemperatureGradient3"/> can be constructed by multiplying a <see cref="TemperatureGradient"/> with a <see cref="Vector3"/> or (double, double, double).
/// Instances can also be produced by combining other quantities, either through mathematical operators or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="TemperatureGradient3"/> a = (3, 5, 7) * <see cref="TemperatureGradient.OneKelvinPerMetre"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="TemperatureGradient3"/> d = <see cref="TemperatureGradient3.From(TemperatureDifference, Displacement3)"/>;
/// </code>
/// </item>
/// </list>
/// The components of the measure can be retrieved as a <see cref="Vector3"/> using pre-defined properties, prefixed with 'In', followed by the desired <see cref="UnitOfTemperatureGradient"/>.
/// </para>
/// </summary>
public readonly partial record struct TemperatureGradient3 :
    IVector3Quantity,
    IScalableVector3Quantity<TemperatureGradient3>,
    INormalizableVector3Quantity<TemperatureGradient3>,
    ITransformableVector3Quantity<TemperatureGradient3>,
    IMultiplicableVector3Quantity<TemperatureGradient3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<TemperatureGradient3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<TemperatureGradient, Vector3>,
    IDotableVector3Quantity<Unhandled, Unhandled3>,
    ICrossableVector3Quantity<TemperatureGradient3, Vector3>,
    ICrossableVector3Quantity<Unhandled3, Unhandled3>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    /// <summary>The zero-magnitude <see cref="TemperatureGradient3"/>.</summary>
    public static TemperatureGradient3 Zero { get; } = new(0, 0, 0);

    /// <summary>The magnitude of the X-component of the <see cref="TemperatureGradient3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfTemperatureGradient)"/> or a pre-defined property
    /// - such as <see cref="KelvinPerMetre"/>.</remarks>
    public double X { get; init; }
    /// <summary>The magnitude of the Y-component of the <see cref="TemperatureGradient3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfTemperatureGradient)"/> or a pre-defined property
    /// - such as <see cref="KelvinPerMetre"/>.</remarks>
    public double Y { get; init; }
    /// <summary>The magnitude of the Z-component of the <see cref="TemperatureGradient3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfTemperatureGradient)"/> or a pre-defined property
    /// - such as <see cref="KelvinPerMetre"/>.</remarks>
    public double Z { get; init; }

    /// <summary>Constructs a new <see cref="TemperatureGradient3"/> with components <paramref name="components"/>.</summary>
    /// <param name="components">The components of the <see cref="TemperatureGradient3"/>.</param>
    public TemperatureGradient3((TemperatureGradient x, TemperatureGradient y, TemperatureGradient z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="TemperatureGradient3"/> with components (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The X-component of the <see cref="TemperatureGradient3"/>.</param>
    /// <param name="y">The Y-component of the <see cref="TemperatureGradient3"/>.</param>
    /// <param name="z">The Z-component of the <see cref="TemperatureGradient3"/>.</param>
    public TemperatureGradient3(TemperatureGradient x, TemperatureGradient y, TemperatureGradient z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="TemperatureGradient3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfTemperatureGradient"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="TemperatureGradient3"/>, expressed in <paramref name="unitOfTemperatureGradient"/>.</param>
    /// <param name="unitOfTemperatureGradient">The <see cref="UnitOfTemperatureGradient"/> in which the magnitudes of the components, <paramref name="components"/>, are expressed.</param>
    public TemperatureGradient3((Scalar x, Scalar y, Scalar z) components, UnitOfTemperatureGradient unitOfTemperatureGradient) : 
    	this(components.x, components.y, components.z, unitOfTemperatureGradient) { }
    /// <summary>Constructs a new <see cref="TemperatureGradient3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>),
    /// expressed in <paramref name="unitOfTemperatureGradient"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="TemperatureGradient3"/>, expressed in <paramref name="unitOfTemperatureGradient"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="TemperatureGradient3"/>, expressed in <paramref name="unitOfTemperatureGradient"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="TemperatureGradient3"/>, expressed in <paramref name="unitOfTemperatureGradient"/>.</param>
    /// <param name="unitOfTemperatureGradient">The <see cref="UnitOfTemperatureGradient"/> in which the magnitudes of the components,
    /// (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>), are expressed.</param>
    public TemperatureGradient3(Scalar x, Scalar y, Scalar z, UnitOfTemperatureGradient unitOfTemperatureGradient) : 
    	this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfTemperatureGradient) { }
    /// <summary>Constructs a new <see cref="TemperatureGradient3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfTemperatureGradient"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="TemperatureGradient3"/>, expressed in <paramref name="unitOfTemperatureGradient"/>.</param>
    /// <param name="unitOfTemperatureGradient">The <see cref="UnitOfTemperatureGradient"/> in which the magnitudes of the components,
    /// <paramref name="components"/>, are expressed.</param>
    public TemperatureGradient3(Vector3 components, UnitOfTemperatureGradient unitOfTemperatureGradient) : 
    	this(components.X, components.Y, components.Z, unitOfTemperatureGradient) { }
    /// <summary>Constructs a new <see cref="TemperatureGradient3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfTemperatureGradient"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="TemperatureGradient3"/>, expressed in <paramref name="unitOfTemperatureGradient"/>.</param>
    /// <param name="unitOfTemperatureGradient">The <see cref="UnitOfTemperatureGradient"/> in which the magnitudes of the components, <paramref name="components"/>, are expressed.</param>
    public TemperatureGradient3((double x, double y, double z) components, UnitOfTemperatureGradient unitOfTemperatureGradient) : 
    	this(components.x, components.y, components.z, unitOfTemperatureGradient) { }
    /// <summary>Constructs a new <see cref="TemperatureGradient3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>),
    /// expressed in <paramref name="unitOfTemperatureGradient"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="TemperatureGradient3"/>, expressed in <paramref name="unitOfTemperatureGradient"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="TemperatureGradient3"/>, expressed in <paramref name="unitOfTemperatureGradient"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="TemperatureGradient3"/>, expressed in <paramref name="unitOfTemperatureGradient"/>.</param>
    /// <param name="unitOfTemperatureGradient">The <see cref="UnitOfTemperatureGradient"/> in which the magnitudes of the components,
    /// (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>), are expressed.</param>
    public TemperatureGradient3(double x, double y, double z, UnitOfTemperatureGradient unitOfTemperatureGradient) : 
    	this(x * unitOfTemperatureGradient.Factor, y * unitOfTemperatureGradient.Factor, z * unitOfTemperatureGradient.Factor) { }

    /// <summary>Constructs a new <see cref="TemperatureGradient3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="TemperatureGradient3"/>.</param>
    /// <remarks>Consider preferring <see cref="TemperatureGradient3(ValueTuple{Scalar, Scalar, Scalar}, UnitOfTemperatureGradient)"/>.</remarks>
    public TemperatureGradient3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="TemperatureGradient3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="TemperatureGradient3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="TemperatureGradient3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="TemperatureGradient3"/>.</param>
    /// <remarks>Consider preferring <see cref="TemperatureGradient3(Scalar, Scalar, Scalar, UnitOfTemperatureGradient)"/>.</remarks>
    public TemperatureGradient3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="TemperatureGradient3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="TemperatureGradient3"/>.</param>
    /// <remarks>Consider preferring <see cref="TemperatureGradient3(Vector3, UnitOfTemperatureGradient)"/>.</remarks>
    public TemperatureGradient3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    /// <summary>Constructs a new <see cref="TemperatureGradient3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="TemperatureGradient3"/>.</param>
    /// <remarks>Consider preferring <see cref="TemperatureGradient3(ValueTuple{double, double, double}, UnitOfTemperatureGradient)"/>.</remarks>
    public TemperatureGradient3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="TemperatureGradient3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="TemperatureGradient3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="TemperatureGradient3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="TemperatureGradient3"/>.</param>
    /// <remarks>Consider preferring <see cref="TemperatureGradient3(double, double, double, UnitOfTemperatureGradient)"/>.</remarks>
    public TemperatureGradient3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>Retrieves the magnitudes of the components of the <see cref="TemperatureGradient3"/>, expressed in <see cref="UnitOfTemperatureGradient.KelvinPerMetre"/>.</summary>
    public Vector3 KelvinPerMetre => InUnit(UnitOfTemperatureGradient.KelvinPerMetre);
    /// <summary>Retrieves the magnitudes of the components of the <see cref="TemperatureGradient3"/>, expressed in <see cref="UnitOfTemperatureGradient.CelsiusPerMetre"/>.</summary>
    public Vector3 CelsiusPerMetre => InUnit(UnitOfTemperatureGradient.CelsiusPerMetre);
    /// <summary>Retrieves the magnitudes of the components of the <see cref="TemperatureGradient3"/>, expressed in <see cref="UnitOfTemperatureGradient.RankinePerMetre"/>.</summary>
    public Vector3 RankinePerMetre => InUnit(UnitOfTemperatureGradient.RankinePerMetre);
    /// <summary>Retrieves the magnitudes of the components of the <see cref="TemperatureGradient3"/>, expressed in <see cref="UnitOfTemperatureGradient.FahrenheitPerMetre"/>.</summary>
    public Vector3 FahrenheitPerMetre => InUnit(UnitOfTemperatureGradient.FahrenheitPerMetre);

    /// <inheritdoc/>
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    /// <summary>Computes the magnitude, or norm, of the vector quantity <see cref="TemperatureGradient3"/>, as a <see cref="TemperatureGradient"/>.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when possible.</remarks>
    public TemperatureGradient Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    /// <summary>Computes the square of the magnitude, or norm, of the vector quantity <see cref="TemperatureGradient3"/>.</summary>
    /// <remarks>For clarity, consider first extracting the magnitudes of the components in the desired <see cref="UnitOfTemperatureGradient"/>.</remarks>
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    /// <summary>Normalizes the <see cref="TemperatureGradient3"/> - if expressed in SI units.</summary>
    /// <remarks>Note that the resulting <see cref="TemperatureGradient3"/> will only be normalized if expressed in SI units.</remarks>
    public TemperatureGradient3 Normalize() => this / Magnitude().Magnitude;
    /// <summary>Computes the transformation of the existing <see cref="TemperatureGradient3"/> by <paramref name="transform"/>, resulting in
    /// a new <see cref="TemperatureGradient3"/>.</summary>
    /// <param name="transform">The <see cref="TemperatureGradient3"/> is transformed by this <see cref="Matrix4x4"/>.</param>
    public TemperatureGradient3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    /// <summary>Performs dot-multiplication of the <see cref="TemperatureGradient3"/> by <paramref name="factor"/>, resulting in a
    /// <cref name="TemperatureGradient"/>.</summary>
    /// <param name="factor">The <see cref="TemperatureGradient3"/> is dot-multiplied by this <see cref="Vector3"/>.</param>
    public TemperatureGradient Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    /// <summary>Performs dot-multiplication of the <see cref="TemperatureGradient3"/> by <paramref name="factor"/>, resulting in a
    /// <cref name="Unhandled"/>.</summary>
    /// <param name="factor">The <see cref="TemperatureGradient3"/> is dot-multiplied by this <see cref="Unhandled3"/>.</param>
    public Unhandled Dot(Unhandled3 factor) => new(Maths.Vectors.Dot(this, factor));
    /// <inheritdoc/>
    public TProductScalarQuantity Dot<TProductScalarQuantity, TFactorVector3Quantity>(TFactorVector3Quantity factor, Func<Scalar, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorVector3Quantity : IVector3Quantity
        => factory(Maths.Vectors.Dot(this, factor));
    /// <summary>Performs cross-multiplication of the <see cref="TemperatureGradient3"/> by <paramref name="factor"/>, resulting in a
    /// <cref see="TemperatureGradient3"/>.</summary>
    /// <param name="factor">The <see cref="TemperatureGradient3"/> is cross-multiplied by this <see cref="Vector3"/>.</param>
    public TemperatureGradient3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    /// <summary>Performs cross-multiplication of the <see cref="TemperatureGradient3"/> by <paramref name="factor"/>, resulting in a
    /// <cref name="Unhandled3"/>.</summary>
    /// <param name="factor">The <see cref="TemperatureGradient3"/> is cross-multiplied by this <see cref="Unhandled3"/>.</param>
    public Unhandled3 Cross(Unhandled3 factor) => new(Maths.Vectors.Cross(this, factor));
    /// <inheritdoc/>
    public TProductVector3Quantity Cross<TProductVector3Quantity, TFactorVector3Quantity>(TFactorVector3Quantity factor, Func<Vector3, TProductVector3Quantity> factory)
        where TProductVector3Quantity : IVector3Quantity
        where TFactorVector3Quantity : IVector3Quantity
        => factory(Maths.Vectors.Cross(this, factor));

    /// <summary>Produces a formatted string from the magnitudes of the components of the <see cref="TemperatureGradient3"/> (in SI units),
    /// and the SI base unit of the quantity.</summary>
    public override string ToString() => $"({X}, {Y}, {Z}) [K / m]";

    /// <summary>Produces a <see cref="Vector3"/> with components equal to that of the <see cref="TemperatureGradient3"/>,
    /// expressed in <paramref name="unitOfTemperatureGradient"/>.</summary>
    /// <param name="unitOfTemperatureGradient">The <see cref="UnitOfTemperatureGradient"/> in which the magnitude is expressed.</param>
    public Vector3 InUnit(UnitOfTemperatureGradient unitOfTemperatureGradient) => InUnit(this, unitOfTemperatureGradient);
    /// <summary>Produces a <see cref="Vector3"/> with components equal to that of the <see cref="TemperatureGradient3"/>,
    /// expressed in <paramref name="unitOfTemperatureGradient"/>.</summary>
    /// <param name="temperatureGradient3">The <see cref="TemperatureGradient3"/> to be expressed in <paramref name="unitOfTemperatureGradient"/>.</param>
    /// <param name="unitOfTemperatureGradient">The <see cref="UnitOfTemperatureGradient"/> in which the magnitude is expressed.</param>
    private static Vector3 InUnit(TemperatureGradient3 temperatureGradient3, UnitOfTemperatureGradient unitOfTemperatureGradient) => 
    	temperatureGradient3.ToVector3() / unitOfTemperatureGradient.Factor;
    
    /// <summary>Unary plus, resulting in the unmodified <see cref="TemperatureGradient3"/>.</summary>
    public TemperatureGradient3 Plus() => this;
    /// <summary>Negation, resulting in a <see cref="TemperatureGradient3"/> with negated components.</summary>
    public TemperatureGradient3 Negate() => new(-X, -Y, -Z);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="a"/>.</summary>
    /// <param name="a">Unary plus is applied to this instance of <see cref="TemperatureGradient3"/>.</param>
    public static TemperatureGradient3 operator +(TemperatureGradient3 a) => a;
    /// <summary>Negation, resulting in a <see cref="TemperatureGradient3"/> with components negated from that of <paramref name="a"/>.</summary>
    /// <param name="a">Negation is applied to this instance of <see cref="TemperatureGradient3"/>.</param>
    public static TemperatureGradient3 operator -(TemperatureGradient3 a) => new(-a.X, -a.Y, -a.Z);

    /// <summary>Multiplies the <see cref="TemperatureGradient3"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="TemperatureGradient3"/> is multiplied.</param>
    public Unhandled3 Multiply(Unhandled factor) => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    /// <summary>Divides the <see cref="TemperatureGradient3"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="TemperatureGradient3"/> is divided.</param>
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    /// <summary>Multiplies the <see cref="TemperatureGradient3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="TemperatureGradient3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="TemperatureGradient3"/> <paramref name="a"/> is multiplied.</param>
    public static Unhandled3 operator *(TemperatureGradient3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Multiplies the <see cref="Unhandled"/> quantity <paramref name="b"/> by the <see cref="TemperatureGradient3"/> <paramref name="a"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Unhandled"/> quantity by which the <see cref="TemperatureGradient3"/> <paramref name="b"/> is multiplied.</param>
    /// <param name="b">The <see cref="TemperatureGradient3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="a"/>.</param>
    public static Unhandled3 operator *(Unhandled a, TemperatureGradient3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Divides the <see cref="TemperatureGradient3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="TemperatureGradient3"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="TemperatureGradient3"/> <paramref name="a"/> is divded.</param>
    public static Unhandled3 operator /(TemperatureGradient3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    /// <summary>Produces a <see cref="TemperatureGradient3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public TemperatureGradient3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    /// <summary>Scales the <see cref="TemperatureGradient3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="TemperatureGradient3"/> is scaled.</param>
    public TemperatureGradient3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    /// <summary>Scales the <see cref="TemperatureGradient3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="TemperatureGradient3"/> is divided.</param>
    public TemperatureGradient3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    /// <summary>Produces a <see cref="TemperatureGradient3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="TemperatureGradient3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is retrieved from division of <see cref="TemperatureGradient3"/> <paramref name="a"/> by this value.</param>
    public static TemperatureGradient3 operator %(TemperatureGradient3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    /// <summary>Scales the <see cref="TemperatureGradient3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="TemperatureGradient3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="TemperatureGradient3"/> <paramref name="a"/>.</param>
    public static TemperatureGradient3 operator *(TemperatureGradient3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    /// <summary>Scales the <see cref="TemperatureGradient3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="TemperatureGradient3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="TemperatureGradient3"/>, which is scaled by <paramref name="a"/>.</param>
    public static TemperatureGradient3 operator *(double a, TemperatureGradient3 b) => new(a * b.X, a * b.Y, a * b.Z);
    /// <summary>Scales the <see cref="TemperatureGradient3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="TemperatureGradient3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="TemperatureGradient3"/> <paramref name="a"/>.</param>
    public static TemperatureGradient3 operator /(TemperatureGradient3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    /// <summary>Produces a <see cref="TemperatureGradient3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public TemperatureGradient3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    /// <summary>Scales the <see cref="TemperatureGradient3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="TemperatureGradient3"/> is scaled.</param>
    public TemperatureGradient3 Multiply(Scalar factor) => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    /// <summary>Scales the <see cref="TemperatureGradient3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="TemperatureGradient3"/> is divided.</param>
    public TemperatureGradient3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    /// <summary>Produces a <see cref="TemperatureGradient3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="TemperatureGradient3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is retrieved from division of <see cref="TemperatureGradient3"/> <paramref name="a"/> by this value.</param>
    public static TemperatureGradient3 operator %(TemperatureGradient3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    /// <summary>Scales the <see cref="TemperatureGradient3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="TemperatureGradient3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="TemperatureGradient3"/> <paramref name="a"/>.</param>
    public static TemperatureGradient3 operator *(TemperatureGradient3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Scales the <see cref="TemperatureGradient3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="TemperatureGradient3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="TemperatureGradient3"/>, which is scaled by <paramref name="a"/>.</param>
    public static TemperatureGradient3 operator *(Scalar a, TemperatureGradient3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Scales the <see cref="TemperatureGradient3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="TemperatureGradient3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="TemperatureGradient3"/> <paramref name="a"/>.</param>
    public static TemperatureGradient3 operator /(TemperatureGradient3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

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
    /// <summary>Multiples the <see cref="TemperatureGradient3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="TemperatureGradient3"/>, which is multiplied by <paramref name="b"/>.</param>
    /// <param name="b">This quantity is multiplied by the <see cref="TemperatureGradient3"/> <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity, Func{double, double, double, TProductVector3Quantity})"/>.</remarks>
    public static Unhandled3 operator *(TemperatureGradient3 a, IScalarQuantity b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Multiples the quantity <paramref name="a"/> by the <see cref="TemperatureGradient3"/> <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">This quantity is multiplied by the <see cref="TemperatureGradient3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="TemperatureGradient3"/>, which is multiplied by <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity, Func{double, double, double, TProductVector3Quantity})"/>.</remarks>
    public static Unhandled3 operator *(IScalarQuantity a, TemperatureGradient3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Divides the <see cref="TemperatureGradient3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="TemperatureGradient3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">The<see cref="TemperatureGradient3"/> <paramref name="a"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientVector3Quantity, TDivisorScalarQuantity}(TDivisorScalarQuantity, Func{double, double, double, TQuotientVector3Quantity})"/>.</remarks>
    public static Unhandled3 operator /(TemperatureGradient3 a, IScalarQuantity b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    /// <summary>Converts the <see cref="TemperatureGradient3"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with values
    /// (<see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>), when expressed in SI units.</summary>
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    /// <summary>Converts <paramref name="a"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with values
    /// (<see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>), when expressed in SI units.</summary>
    public static implicit operator (double x, double y, double z)(TemperatureGradient3 a) => (a.X, a.Y, a.Z);

    /// <summary>Converts the <see cref="TemperatureGradient3"/> to the <see cref="Vector3"/> with components of
    /// equal magnitude, when expressed in SI units.</summary>
    public Vector3 ToVector3() => new(X, Y, Z);
    /// <summary>Converts <paramref name="a"/> to the <see cref="Vector3"/> with components of
    /// equal magnitude, when expressed in SI units.</summary>
    public static explicit operator Vector3(TemperatureGradient3 a) => new(a.X, a.Y, a.Z);

    /// <summary>Constructs the <see cref="TemperatureGradient3"/> with components equal to the values of <paramref name="components"/>,
    /// when expressed in SI units.</summary>
    public static TemperatureGradient3 FromValueTuple((double x, double y, double z) components) => new(components);
    /// <summary>Constructs the <see cref="TemperatureGradient3"/> with components equal to the values of <paramref name="components"/>,
    /// when expressed in SI units.</summary>
    public static explicit operator TemperatureGradient3((double x, double y, double z) components) => new(components);

    /// <summary>Converts <paramref name="a"/> to the <see cref="TemperatureGradient3"/> with components of equal magnitude,
    /// when expressed in SI units.</summary>
    public static TemperatureGradient3 FromVector3(Vector3 a) => new(a);
    /// <summary>Converts <paramref name="a"/> to the <see cref="TemperatureGradient3"/> with components of equal magnitude,
    /// when expressed in SI units.</summary>
    public static explicit operator TemperatureGradient3(Vector3 a) => new(a);
}
