#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

/// <summary>A measure of the vector quantity <see cref="Momentum3"/>, of dimensionality three,
/// a property of an object with <see cref="Mass"/> in motion. The quantity is expressed in
/// <see cref="UnitOfMomentum"/>, with the SI unit being [kg∙m∙s⁻¹].
/// <para>
/// New instances of <see cref="Momentum3"/> can be constructed by multiplying a <see cref="Momentum"/> with a Vector3 or (double, double, double).
/// Instances can also be produced by combining other quantities, either through mathematical operators or using overloads of the static method 'From'.
/// Lastly, instances can be constructed from quantities sharing the same unit, using instance-methods of the associated quantity - typically prefixed with 'As'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Momentum3"/> a = (3, 5, 7) * <see cref="Momentum.OneKilogramMetrePerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Momentum3"/> d = <see cref="Momentum3.From(Mass, Velocity3)"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Momentum3"/> e = <see cref="Impulse3.AsMomentum3"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the components can be retrieved in the desired <see cref="UnitOfMomentum"/> using pre-defined properties, such as <see cref="KilogramMetresPerSecond"/>./// </para>
/// </summary>
/// <remarks>
/// <see cref="Momentum3"/> is closely related to the following quantities:
/// <list type="bullet">
/// <item>
/// <term><see cref="Impulse3"/></term>
/// <description>Describes a change in <see cref="Momentum3"/>.</description>
/// </item>
/// </list>
/// </remarks>
public readonly partial record struct Momentum3 :
    IVector3Quantity,
    IScalableVector3Quantity<Momentum3>,
    INormalizableVector3Quantity<Momentum3>,
    ITransformableVector3Quantity<Momentum3>,
    IMultiplicableVector3Quantity<Momentum3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Momentum3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<Momentum, Vector3>,
    IDotableVector3Quantity<Unhandled, Unhandled3>,
    ICrossableVector3Quantity<Momentum3, Vector3>,
    ICrossableVector3Quantity<Unhandled3, Unhandled3>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    /// <summary>The zero-magnitude <see cref="Momentum3"/>.</summary>
    public static Momentum3 Zero { get; } = new(0, 0, 0);

    /// <summary>The magnitude of the X-component of the <see cref="Momentum3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfMomentum)"/> or a pre-defined property
    /// - such as <see cref="KilogramMetresPerSecond"/>.</remarks>
    public double X { get; init; }
    /// <summary>The magnitude of the Y-component of the <see cref="Momentum3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfMomentum)"/> or a pre-defined property
    /// - such as <see cref="KilogramMetresPerSecond"/>.</remarks>
    public double Y { get; init; }
    /// <summary>The magnitude of the Z-component of the <see cref="Momentum3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfMomentum)"/> or a pre-defined property
    /// - such as <see cref="KilogramMetresPerSecond"/>.</remarks>
    public double Z { get; init; }

    /// <summary>Retrieves the magnitudes of the components of the <see cref="Momentum3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfMomentum)"/> or a pre-defined property
    /// - such as <see cref="KilogramMetresPerSecond"/>.</remarks>
    public Vector3 Components => new(X, Y, Z);

    /// <summary>Constructs a new <see cref="Momentum3"/> with components <paramref name="components"/>.</summary>
    /// <param name="components">The components of the <see cref="Momentum3"/>.</param>
    public Momentum3((Momentum x, Momentum y, Momentum z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="Momentum3"/> with components (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The X-component of the <see cref="Momentum3"/>.</param>
    /// <param name="y">The Y-component of the <see cref="Momentum3"/>.</param>
    /// <param name="z">The Z-component of the <see cref="Momentum3"/>.</param>
    public Momentum3(Momentum x, Momentum y, Momentum z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="Momentum3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfMomentum"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Momentum3"/>, expressed in <paramref name="unitOfMomentum"/>.</param>
    /// <param name="unitOfMomentum">The <see cref="UnitOfMomentum"/> in which the magnitudes of the components, <paramref name="components"/>, are expressed.</param>
    public Momentum3((Scalar x, Scalar y, Scalar z) components, UnitOfMomentum unitOfMomentum) : this(components.x, components.y, components.z, unitOfMomentum) { }
    /// <summary>Constructs a new <see cref="Momentum3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>),
    /// expressed in <paramref name="unitOfMomentum"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="Momentum3"/>, expressed in <paramref name="unitOfMomentum"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="Momentum3"/>, expressed in <paramref name="unitOfMomentum"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="Momentum3"/>, expressed in <paramref name="unitOfMomentum"/>.</param>
    /// <param name="unitOfMomentum">The <see cref="UnitOfMomentum"/> in which the magnitudes of the components,
    /// (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>), are expressed.</param>
    public Momentum3(Scalar x, Scalar y, Scalar z, UnitOfMomentum unitOfMomentum) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfMomentum) { }
    /// <summary>Constructs a new <see cref="Momentum3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfMomentum"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Momentum3"/>, expressed in <paramref name="unitOfMomentum"/>.</param>
    /// <param name="unitOfMomentum">The <see cref="UnitOfMomentum"/> in which the magnitudes of the components,
    /// <paramref name="components"/>, are expressed.</param>
    public Momentum3(Vector3 components, UnitOfMomentum unitOfMomentum) : this(components.X, components.Y, components.Z, unitOfMomentum) { }
    /// <summary>Constructs a new <see cref="Momentum3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfMomentum"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Momentum3"/>, expressed in <paramref name="unitOfMomentum"/>.</param>
    /// <param name="unitOfMomentum">The <see cref="UnitOfMomentum"/> in which the magnitudes of the components, <paramref name="components"/>, are expressed.</param>
    public Momentum3((double x, double y, double z) components, UnitOfMomentum unitOfMomentum) : this(components.x, components.y, components.z, unitOfMomentum) { }
    /// <summary>Constructs a new <see cref="Momentum3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>),
    /// expressed in <paramref name="unitOfMomentum"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="Momentum3"/>, expressed in <paramref name="unitOfMomentum"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="Momentum3"/>, expressed in <paramref name="unitOfMomentum"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="Momentum3"/>, expressed in <paramref name="unitOfMomentum"/>.</param>
    /// <param name="unitOfMomentum">The <see cref="UnitOfMomentum"/> in which the magnitudes of the components,
    /// (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>), are expressed.</param>
    public Momentum3(double x, double y, double z, UnitOfMomentum unitOfMomentum) : 
    	this(x * unitOfMomentum.Momentum, y * unitOfMomentum.Momentum, z * unitOfMomentum.Momentum) { }

    /// <summary>Constructs a new <see cref="Momentum3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Momentum3"/>.</param>
    /// <remarks>Consider preferring <see cref="Momentum3(ValueTuple{Scalar, Scalar, Scalar}, UnitOfMomentum)"/>.</remarks>
    public Momentum3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="Momentum3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="Momentum3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="Momentum3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="Momentum3"/>.</param>
    /// <remarks>Consider preferring <see cref="Momentum3(Scalar, Scalar, Scalar, UnitOfMomentum)"/>.</remarks>
    public Momentum3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="Momentum3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Momentum3"/>.</param>
    /// <remarks>Consider preferring <see cref="Momentum3(Vector3, UnitOfMomentum)"/>.</remarks>
    public Momentum3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    /// <summary>Constructs a new <see cref="Momentum3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Momentum3"/>.</param>
    /// <remarks>Consider preferring <see cref="Momentum3(ValueTuple{double, double, double}, UnitOfMomentum)"/>.</remarks>
    public Momentum3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="Momentum3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="Momentum3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="Momentum3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="Momentum3"/>.</param>
    /// <remarks>Consider preferring <see cref="Momentum3(double, double, double, UnitOfMomentum)"/>.</remarks>
    public Momentum3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>Converts the <see cref="Momentum3"/> to an instance of the associated quantity <see cref="Impulse3"/>, with components of
    /// equal magnitudes.</summary>
    public Impulse3 AsImpulse3() => new(X, Y, Z);

    /// <summary>Retrieves the magnitudes of the components of the <see cref="Momentum3"/>, expressed in <see cref="UnitOfMomentum.KilogramMetrePerSecond"/>.</summary>
    public Vector3 KilogramMetresPerSecond => InUnit(UnitOfMomentum.KilogramMetrePerSecond);

    /// <inheritdoc/>
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    /// <summary>Computes the magnitude, or norm, of the vector quantity <see cref="Momentum3"/>, as a <see cref="Momentum"/>.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when applicable.</remarks>
    public Momentum Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    /// <summary>Computes the square of the magnitude, or norm, of the vector quantity <see cref="Momentum3"/>.</summary>
    /// <remarks>For clarity, consider first extracting the magnitudes of the components in the desired <see cref="UnitOfMomentum"/>.</remarks>
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    /// <summary>Computes the normalized <see cref="Momentum3"/> - if expressed in SI units.</summary>
    /// <remarks>Note that the resulting <see cref="Momentum3"/> will only be normalized if expressed in SI units.</remarks>
    public Momentum3 Normalize() => this / Magnitude().Magnitude;
    /// <summary>Computes the transformation of the <see cref="Momentum3"/> by <paramref name="transform"/>.</summary>
    /// <param name="transform">The <see cref="Momentum3"/> is transformed by this <see cref="Matrix4x4"/>.</param>
    public Momentum3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    /// <summary>Performs dot-multiplication of the <see cref="Momentum3"/> by <paramref name="factor"/>, resulting in a
    /// <see cref="Momentum"/>.</summary>
    /// <param name="factor">The <see cref="Momentum3"/> is dot-multiplied by this <see cref="Vector3"/>.</param>
    public Momentum Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    /// <summary>Performs dot-multiplication of the <see cref="Momentum3"/> by <paramref name="factor"/>, resulting in a
    /// <see cref="Unhandled"/>.</summary>
    /// <param name="factor">The <see cref="Momentum3"/> is dot-multiplied by this <see cref="Unhandled3"/>.</param>
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

    /// <summary>Performs cross-multiplication of the <see cref="Momentum3"/> by <paramref name="factor"/>, resulting in a
    /// <cref see="Momentum3"/>.</summary>
    /// <param name="factor">The <see cref="Momentum3"/> is cross-multiplied by this <see cref="Vector3"/>.</param>
    public Momentum3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    /// <summary>Performs cross-multiplication of the <see cref="Momentum3"/> by <paramref name="factor"/>, resulting in a
    /// <see cref="Unhandled3"/>.</summary>
    /// <param name="factor">The <see cref="Momentum3"/> is cross-multiplied by this <see cref="Unhandled3"/>.</param>
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

    /// <summary>Produces a formatted string from the magnitudes of the components of the <see cref="Momentum3"/> in the default unit
    /// <see cref="UnitOfMomentum.KilogramMetrePerSecond"/>, followed by the symbol [kg∙m∙s⁻¹].</summary>
    public override string ToString() => $"{KilogramMetresPerSecond} [kg∙m∙s⁻¹]";

    /// <summary>Produces a <see cref="Vector3"/> with components equal to that of the <see cref="Momentum3"/>,
    /// expressed in <paramref name="unitOfMomentum"/>.</summary>
    /// <param name="unitOfMomentum">The <see cref="UnitOfMomentum"/> in which the magnitude is expressed.</param>
    public Vector3 InUnit(UnitOfMomentum unitOfMomentum) => InUnit(this, unitOfMomentum);
    /// <summary>Produces a <see cref="Vector3"/> with components equal to that of the <see cref="Momentum3"/>,
    /// expressed in <paramref name="unitOfMomentum"/>.</summary>
    /// <param name="momentum3">The <see cref="Momentum3"/> to be expressed in <paramref name="unitOfMomentum"/>.</param>
    /// <param name="unitOfMomentum">The <see cref="UnitOfMomentum"/> in which the magnitude is expressed.</param>
    private static Vector3 InUnit(Momentum3 momentum3, UnitOfMomentum unitOfMomentum) => momentum3.ToVector3() / unitOfMomentum.Momentum.Magnitude;
    
    /// <summary>Unary plus, resulting in the unmodified <see cref="Momentum3"/>.</summary>
    public Momentum3 Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Momentum3"/> with negated components.</summary>
    public Momentum3 Negate() => new(-X, -Y, -Z);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="a"/>.</summary>
    /// <param name="a">Unary plus is applied to this <see cref="Momentum3"/>.</param>
    public static Momentum3 operator +(Momentum3 a) => a;
    /// <summary>Negation, resulting in a <see cref="Momentum3"/> with negated components from that of <paramref name="a"/>.</summary>
    /// <param name="a">Negation is applied to this <see cref="Momentum3"/>.</param>
    public static Momentum3 operator -(Momentum3 a) => new(-a.X, -a.Y, -a.Z);

    /// <summary>Multiplicates the <see cref="Momentum3"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Momentum3"/> is multiplied.</param>
    public Unhandled3 Multiply(Unhandled factor) => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    /// <summary>Divides the <see cref="Momentum3"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Momentum3"/> is divided.</param>
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Momentum3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Momentum3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="Momentum3"/> <paramref name="a"/> is multiplied.</param>
    public static Unhandled3 operator *(Momentum3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="b"/> by the <see cref="Momentum3"/> <paramref name="a"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Unhandled"/> quantity by which the <see cref="Momentum3"/> <paramref name="b"/> is multiplied.</param>
    /// <param name="b">The <see cref="Momentum3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="a"/>.</param>
    public static Unhandled3 operator *(Unhandled a, Momentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Division of the <see cref="Momentum3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Momentum3"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="Momentum3"/> <paramref name="a"/> is divded.</param>
    public static Unhandled3 operator /(Momentum3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    /// <summary>Produces a <see cref="Momentum3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Momentum3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    /// <summary>Scales the <see cref="Momentum3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Momentum3"/> is scaled.</param>
    public Momentum3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    /// <summary>Scales the <see cref="Momentum3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Momentum3"/> is divided.</param>
    public Momentum3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    /// <summary>Produces a <see cref="Momentum3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Momentum3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is produced from division of <see cref="Momentum3"/> <paramref name="a"/> by this value.</param>
    public static Momentum3 operator %(Momentum3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    /// <summary>Scales the <see cref="Momentum3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Momentum3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="Momentum3"/> <paramref name="a"/>.</param>
    public static Momentum3 operator *(Momentum3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    /// <summary>Scales the <see cref="Momentum3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="Momentum3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Momentum3"/>, which is scaled by <paramref name="a"/>.</param>
    public static Momentum3 operator *(double a, Momentum3 b) => new(a * b.X, a * b.Y, a * b.Z);
    /// <summary>Scales the <see cref="Momentum3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Momentum3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="Momentum3"/> <paramref name="a"/>.</param>
    public static Momentum3 operator /(Momentum3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    /// <summary>Produces a <see cref="Momentum3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Momentum3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    /// <summary>Scales the <see cref="Momentum3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Momentum3"/> is scaled.</param>
    public Momentum3 Multiply(Scalar factor) => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    /// <summary>Scales the <see cref="Momentum3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Momentum3"/> is divided.</param>
    public Momentum3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    /// <summary>Produces a <see cref="Momentum3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Momentum3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is produced from division of <see cref="Momentum3"/> <paramref name="a"/> by this value.</param>
    public static Momentum3 operator %(Momentum3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    /// <summary>Scales the <see cref="Momentum3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Momentum3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="Momentum3"/> <paramref name="a"/>.</param>
    public static Momentum3 operator *(Momentum3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Scales the <see cref="Momentum3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="Momentum3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Momentum3"/>, which is scaled by <paramref name="a"/>.</param>
    public static Momentum3 operator *(Scalar a, Momentum3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Scales the <see cref="Momentum3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Momentum3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="Momentum3"/> <paramref name="a"/>.</param>
    public static Momentum3 operator /(Momentum3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

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

    /// <summary>Multiplication of the <see cref="Momentum3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Momentum3"/>, which is multiplied by <paramref name="b"/>.</param>
    /// <param name="b">This quantity is multiplied by the <see cref="Momentum3"/> <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, double, double, TProductVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(Momentum3 a, IScalarQuantity b) => a.Multiply(b, (x, y, z) => new Unhandled3(x, y, z));
    /// <summary>Multiplication of the quantity <paramref name="a"/> by the <see cref="Momentum3"/> <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">This quantity is multiplied by the <see cref="Momentum3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Momentum3"/>, which is multiplied by <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{double, double, double, TProductVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(IScalarQuantity a, Momentum3 b) => b.Multiply(a, (x, y, z) => new Unhandled3(x, y, z));
    /// <summary>Division of the <see cref="Momentum3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Momentum3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Momentum3"/> <paramref name="a"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientVector3Quantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{double, double, double, TQuotientVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator /(Momentum3 a, IScalarQuantity b) => a.Divide(b, (x, y, z) => new Unhandled3(x, y, z));

    /// <summary>Converts the <see cref="Momentum3"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with 
    /// values (<see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>), when expressed in SI units.</summary>
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    /// <summary>Converts <paramref name="a"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with 
    /// values (<see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>), when expressed in SI units.</summary>
    public static explicit operator (double x, double y, double z)(Momentum3 a) => (a.X, a.Y, a.Z);

    /// <summary>Converts the <see cref="Momentum3"/> to the <see cref="Vector3"/> with components of equal magnitude, when expressed in SI units.</summary>
    public Vector3 ToVector3() => new(X, Y, Z);
    /// <summary>Converts <paramref name="a"/> to the <see cref="Vector3"/> with components of equal magnitude, when expressed in SI units.</summary>
    public static explicit operator Vector3(Momentum3 a) => new(a.X, a.Y, a.Z);

    /// <summary>Constructs the <see cref="Momentum3"/> with components equal to the values of <paramref name="components"/>, when expressed in SI units.</summary>
    public static Momentum3 FromValueTuple((double x, double y, double z) components) => new(components);
    /// <summary>Constructs the <see cref="Momentum3"/> with components equal to the values of <paramref name="components"/>, when expressed in SI units.</summary>
    public static explicit operator Momentum3((double x, double y, double z) components) => new(components);

    /// <summary>Constructs the <see cref="Momentum3"/> with components <paramref name="a"/>, when expressed in SI units.</summary>
    public static Momentum3 FromVector3(Vector3 a) => new(a);
    /// <summary>Constructs the <see cref="Momentum3"/> with components <paramref name="a"/>, when expressed in SI units.</summary>
    public static explicit operator Momentum3(Vector3 a) => new(a);
}
