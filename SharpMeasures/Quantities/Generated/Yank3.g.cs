#nullable enable

namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

/// <summary>A measure of the vector quantity <see cref="Yank3"/>, of dimensionality three,
/// describing change in <see cref="Force3"/> over <see cref="Time"/>. The quantity is expressed in <see cref="UnitOfYank"/>,
/// with the SI unit being [N∙s⁻¹].
/// <para>
/// New instances of <see cref="Yank3"/> can be constructed by multiplying a <see cref="Yank"/> with a <see cref="Vector3"/> or (double, double, double).
/// Instances can also be produced by combining other quantities, either through mathematical operators or using overloads of the static method 'From'. This is demonstrated below:
/// <list type="bullet">
/// <item>
/// <code>
/// <see cref="Yank3"/> a = (3, 5, 7) * <see cref="Yank.OneNewtonPerSecond"/>;
/// </code>
/// </item>
/// <item>
/// <code>
/// <see cref="Yank3"/> d = <see cref="Yank3.From(Force3, Time)"/>;
/// </code>
/// </item>
/// </list>
/// The magnitude of the components can be retrieved in the desired <see cref="UnitOfYank"/> using pre-defined properties, such as <see cref="NewtonsPerSecond"/>.
/// </para>
/// </summary>
public readonly partial record struct Yank3 :
    IVector3Quantity,
    IScalableVector3Quantity<Yank3>,
    INormalizableVector3Quantity<Yank3>,
    ITransformableVector3Quantity<Yank3>,
    IMultiplicableVector3Quantity<Yank3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Yank3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<Yank, Vector3>,
    IDotableVector3Quantity<Unhandled, Unhandled3>,
    ICrossableVector3Quantity<Yank3, Vector3>,
    ICrossableVector3Quantity<Unhandled3, Unhandled3>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    /// <summary>The zero-magnitude <see cref="Yank3"/>.</summary>
    public static Yank3 Zero { get; } = new(0, 0, 0);

    /// <summary>The magnitude of the X-component of the <see cref="Yank3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfYank)"/> or a pre-defined property
    /// - such as <see cref="NewtonsPerSecond"/>.</remarks>
    public double X { get; init; }
    /// <summary>The magnitude of the Y-component of the <see cref="Yank3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfYank)"/> or a pre-defined property
    /// - such as <see cref="NewtonsPerSecond"/>.</remarks>
    public double Y { get; init; }
    /// <summary>The magnitude of the Z-component of the <see cref="Yank3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfYank)"/> or a pre-defined property
    /// - such as <see cref="NewtonsPerSecond"/>.</remarks>
    public double Z { get; init; }

    /// <summary>Retrieves the magnitudes of the components of the <see cref="Yank3"/>, in SI units.</summary>
    /// <remarks>For clarity, consider preferring <see cref="InUnit(UnitOfYank)"/> or a pre-defined property
    /// - such as <see cref="NewtonsPerSecond"/>.</remarks>
    public Vector3 Components => new(X, Y, Z);

    /// <summary>Constructs a new <see cref="Yank3"/> with components <paramref name="components"/>.</summary>
    /// <param name="components">The components of the <see cref="Yank3"/>.</param>
    public Yank3((Yank x, Yank y, Yank z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="Yank3"/> with components (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The X-component of the <see cref="Yank3"/>.</param>
    /// <param name="y">The Y-component of the <see cref="Yank3"/>.</param>
    /// <param name="z">The Z-component of the <see cref="Yank3"/>.</param>
    public Yank3(Yank x, Yank y, Yank z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="Yank3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfYank"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Yank3"/>, expressed in <paramref name="unitOfYank"/>.</param>
    /// <param name="unitOfYank">The <see cref="UnitOfYank"/> in which the magnitudes of the components, <paramref name="components"/>, are expressed.</param>
    public Yank3((Scalar x, Scalar y, Scalar z) components, UnitOfYank unitOfYank) : this(components.x, components.y, components.z, unitOfYank) { }
    /// <summary>Constructs a new <see cref="Yank3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>),
    /// expressed in <paramref name="unitOfYank"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="Yank3"/>, expressed in <paramref name="unitOfYank"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="Yank3"/>, expressed in <paramref name="unitOfYank"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="Yank3"/>, expressed in <paramref name="unitOfYank"/>.</param>
    /// <param name="unitOfYank">The <see cref="UnitOfYank"/> in which the magnitudes of the components,
    /// (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>), are expressed.</param>
    public Yank3(Scalar x, Scalar y, Scalar z, UnitOfYank unitOfYank) : this(x.Magnitude, y.Magnitude, z.Magnitude, unitOfYank) { }
    /// <summary>Constructs a new <see cref="Yank3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfYank"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Yank3"/>, expressed in <paramref name="unitOfYank"/>.</param>
    /// <param name="unitOfYank">The <see cref="UnitOfYank"/> in which the magnitudes of the components,
    /// <paramref name="components"/>, are expressed.</param>
    public Yank3(Vector3 components, UnitOfYank unitOfYank) : this(components.X, components.Y, components.Z, unitOfYank) { }
    /// <summary>Constructs a new <see cref="Yank3"/> with components of magnitudes <paramref name="components"/>,
    /// expressed in <paramref name="unitOfYank"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Yank3"/>, expressed in <paramref name="unitOfYank"/>.</param>
    /// <param name="unitOfYank">The <see cref="UnitOfYank"/> in which the magnitudes of the components, <paramref name="components"/>, are expressed.</param>
    public Yank3((double x, double y, double z) components, UnitOfYank unitOfYank) : this(components.x, components.y, components.z, unitOfYank) { }
    /// <summary>Constructs a new <see cref="Yank3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>),
    /// expressed in <paramref name="unitOfYank"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="Yank3"/>, expressed in <paramref name="unitOfYank"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="Yank3"/>, expressed in <paramref name="unitOfYank"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="Yank3"/>, expressed in <paramref name="unitOfYank"/>.</param>
    /// <param name="unitOfYank">The <see cref="UnitOfYank"/> in which the magnitudes of the components,
    /// (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>), are expressed.</param>
    public Yank3(double x, double y, double z, UnitOfYank unitOfYank) : this(x * unitOfYank.Yank, y * unitOfYank.Yank, z * unitOfYank.Yank) { }

    /// <summary>Constructs a new <see cref="Yank3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Yank3"/>.</param>
    /// <remarks>Consider preferring <see cref="Yank3(ValueTuple{Scalar, Scalar, Scalar}, UnitOfYank)"/>.</remarks>
    public Yank3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="Yank3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="Yank3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="Yank3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="Yank3"/>.</param>
    /// <remarks>Consider preferring <see cref="Yank3(Scalar, Scalar, Scalar, UnitOfYank)"/>.</remarks>
    public Yank3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="Yank3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Yank3"/>.</param>
    /// <remarks>Consider preferring <see cref="Yank3(Vector3, UnitOfYank)"/>.</remarks>
    public Yank3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    /// <summary>Constructs a new <see cref="Yank3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Yank3"/>.</param>
    /// <remarks>Consider preferring <see cref="Yank3(ValueTuple{double, double, double}, UnitOfYank)"/>.</remarks>
    public Yank3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="Yank3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="Yank3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="Yank3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="Yank3"/>.</param>
    /// <remarks>Consider preferring <see cref="Yank3(double, double, double, UnitOfYank)"/>.</remarks>
    public Yank3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>Retrieves the magnitudes of the components of the <see cref="Yank3"/>, expressed in <see cref="UnitOfYank.NewtonPerSecond"/>.</summary>
    public Vector3 NewtonsPerSecond => InUnit(UnitOfYank.NewtonPerSecond);

    /// <inheritdoc/>
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    /// <summary>Computes the magnitude, or norm, of the vector quantity <see cref="Yank3"/>, as a <see cref="Yank"/>.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when applicable.</remarks>
    public Yank Magnitude() => new(Maths.Vectors.Dot(this, this).SquareRoot());
    /// <summary>Computes the square of the magnitude, or norm, of the vector quantity <see cref="Yank3"/>.</summary>
    /// <remarks>For clarity, consider first extracting the magnitudes of the components in the desired <see cref="UnitOfYank"/>.</remarks>
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    /// <summary>Computes the normalized <see cref="Yank3"/> - if expressed in SI units.</summary>
    /// <remarks>Note that the resulting <see cref="Yank3"/> will only be normalized if expressed in SI units.</remarks>
    public Yank3 Normalize() => this / Magnitude().Magnitude;
    /// <summary>Computes the transformation of the <see cref="Yank3"/> by <paramref name="transform"/>.</summary>
    /// <param name="transform">The <see cref="Yank3"/> is transformed by this <see cref="Matrix4x4"/>.</param>
    public Yank3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));
    
    /// <summary>Performs dot-multiplication of the <see cref="Yank3"/> by <paramref name="factor"/>, resulting in a
    /// <see cref="Yank"/>.</summary>
    /// <param name="factor">The <see cref="Yank3"/> is dot-multiplied by this <see cref="Vector3"/>.</param>
    public Yank Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    /// <summary>Performs dot-multiplication of the <see cref="Yank3"/> by <paramref name="factor"/>, resulting in a
    /// <see cref="Unhandled"/>.</summary>
    /// <param name="factor">The <see cref="Yank3"/> is dot-multiplied by this <see cref="Unhandled3"/>.</param>
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

    /// <summary>Performs cross-multiplication of the <see cref="Yank3"/> by <paramref name="factor"/>, resulting in a
    /// <see cref="Yank3"/>.</summary>
    /// <param name="factor">The <see cref="Yank3"/> is cross-multiplied by this <see cref="Vector3"/>.</param>
    public Yank3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    /// <summary>Performs cross-multiplication of the <see cref="Yank3"/> by <paramref name="factor"/>, resulting in a
    /// <see cref="Unhandled3"/>.</summary>
    /// <param name="factor">The <see cref="Yank3"/> is cross-multiplied by this <see cref="Unhandled3"/>.</param>
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

    /// <summary>Produces a formatted string from the magnitudes of the components of the <see cref="Yank3"/> in the default unit
    /// <see cref="UnitOfYank.NewtonPerSecond"/>, followed by the symbol [N∙s⁻¹].</summary>
    public override string ToString() => $"{NewtonsPerSecond} [N∙s⁻¹]";

    /// <summary>Produces a <see cref="Vector3"/> with components equal to that of the <see cref="Yank3"/>,
    /// expressed in <paramref name="unitOfYank"/>.</summary>
    /// <param name="unitOfYank">The <see cref="UnitOfYank"/> in which the magnitude is expressed.</param>
    public Vector3 InUnit(UnitOfYank unitOfYank) => InUnit(this, unitOfYank);
    /// <summary>Produces a <see cref="Vector3"/> with components equal to that of the <see cref="Yank3"/>,
    /// expressed in <paramref name="unitOfYank"/>.</summary>
    /// <param name="yank3">The <see cref="Yank3"/> to be expressed in <paramref name="unitOfYank"/>.</param>
    /// <param name="unitOfYank">The <see cref="UnitOfYank"/> in which the magnitude is expressed.</param>
    private static Vector3 InUnit(Yank3 yank3, UnitOfYank unitOfYank) => yank3.ToVector3() / unitOfYank.Yank.Magnitude;
    
    /// <summary>Unary plus, resulting in the unmodified <see cref="Yank3"/>.</summary>
    public Yank3 Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Yank3"/> with negated components.</summary>
    public Yank3 Negate() => new(-X, -Y, -Z);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="a"/>.</summary>
    /// <param name="a">Unary plus is applied to this <see cref="Yank3"/>.</param>
    public static Yank3 operator +(Yank3 a) => a;
    /// <summary>Negation, resulting in a <see cref="Yank3"/> with negated components from that of <paramref name="a"/>.</summary>
    /// <param name="a">Negation is applied to this <see cref="Yank3"/>.</param>
    public static Yank3 operator -(Yank3 a) => new(-a.X, -a.Y, -a.Z);

    /// <summary>Multiplicates the <see cref="Yank3"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Yank3"/> is multiplied.</param>
    public Unhandled3 Multiply(Unhandled factor) => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    /// <summary>Divides the <see cref="Yank3"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Yank3"/> is divided.</param>
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    /// <summary>Multiplication of the <see cref="Yank3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Yank3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="Yank3"/> <paramref name="a"/> is multiplied.</param>
    public static Unhandled3 operator *(Yank3 a, Unhandled b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="b"/> by the <see cref="Yank3"/> <paramref name="a"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Unhandled"/> quantity by which the <see cref="Yank3"/> <paramref name="b"/> is multiplied.</param>
    /// <param name="b">The <see cref="Yank3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="a"/>.</param>
    public static Unhandled3 operator *(Unhandled a, Yank3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Division of the <see cref="Yank3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Yank3"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="Yank3"/> <paramref name="a"/> is divded.</param>
    public static Unhandled3 operator /(Yank3 a, Unhandled b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

    /// <summary>Produces a <see cref="Yank3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Yank3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    /// <summary>Scales the <see cref="Yank3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Yank3"/> is scaled.</param>
    public Yank3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    /// <summary>Scales the <see cref="Yank3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Yank3"/> is divided.</param>
    public Yank3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    /// <summary>Produces a <see cref="Yank3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Yank3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is produced from division of <see cref="Yank3"/> <paramref name="a"/> by this value.</param>
    public static Yank3 operator %(Yank3 a, double b) => new(a.X % b, a.Y % b, a.Z % b);
    /// <summary>Scales the <see cref="Yank3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Yank3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="Yank3"/> <paramref name="a"/>.</param>
    public static Yank3 operator *(Yank3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    /// <summary>Scales the <see cref="Yank3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="Yank3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Yank3"/>, which is scaled by <paramref name="a"/>.</param>
    public static Yank3 operator *(double a, Yank3 b) => new(a * b.X, a * b.Y, a * b.Z);
    /// <summary>Scales the <see cref="Yank3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Yank3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="Yank3"/> <paramref name="a"/>.</param>
    public static Yank3 operator /(Yank3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    /// <summary>Produces a <see cref="Yank3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Yank3 Remainder(Scalar divisor) => new(X % divisor.Magnitude, Y % divisor.Magnitude, Z % divisor.Magnitude);
    /// <summary>Scales the <see cref="Yank3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Yank3"/> is scaled.</param>
    public Yank3 Multiply(Scalar factor) => new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
    /// <summary>Scales the <see cref="Yank3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Yank3"/> is divided.</param>
    public Yank3 Divide(Scalar divisor) => new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
    /// <summary>Produces a <see cref="Yank3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Yank3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is produced from division of <see cref="Yank3"/> <paramref name="a"/> by this value.</param>
    public static Yank3 operator %(Yank3 a, Scalar b) => new(a.X % b.Magnitude, a.Y % b.Magnitude, a.Z % b.Magnitude);
    /// <summary>Scales the <see cref="Yank3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Yank3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="Yank3"/> <paramref name="a"/>.</param>
    public static Yank3 operator *(Yank3 a, Scalar b) => new(a.X * b.Magnitude, a.Y * b.Magnitude, a.Z * b.Magnitude);
    /// <summary>Scales the <see cref="Yank3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="Yank3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Yank3"/>, which is scaled by <paramref name="a"/>.</param>
    public static Yank3 operator *(Scalar a, Yank3 b) => new(a.Magnitude * b.X, a.Magnitude * b.Y, a.Magnitude * b.Z);
    /// <summary>Scales the <see cref="Yank3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Yank3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="Yank3"/> <paramref name="a"/>.</param>
    public static Yank3 operator /(Yank3 a, Scalar b) => new(a.X / b.Magnitude, a.Y / b.Magnitude, a.Z / b.Magnitude);

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

    /// <summary>Multiplication of the <see cref="Yank3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Yank3"/>, which is multiplied by <paramref name="b"/>.</param>
    /// <param name="b">This quantity is multiplied by the <see cref="Yank3"/> <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{ValueTuple{double, double, double}, TProductVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(Yank3 a, IScalarQuantity b) => a.Multiply(b, (x) => new Unhandled3(x));
    /// <summary>Multiplication of the quantity <paramref name="a"/> by the <see cref="Yank3"/> <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">This quantity is multiplied by the <see cref="Yank3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Yank3"/>, which is multiplied by <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{ValueTuple{double, double, double}, TProductVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(IScalarQuantity a, Yank3 b) => b.Multiply(a, (x) => new Unhandled3(x));
    /// <summary>Division of the <see cref="Yank3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Yank3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Yank3"/> <paramref name="a"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientVector3Quantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{ValueTuple{double, double, double}, TQuotientVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator /(Yank3 a, IScalarQuantity b) => a.Divide(b, (x) => new Unhandled3(x));

    /// <summary>Converts the <see cref="Yank3"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with 
    /// values (<see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>), when expressed in SI units.</summary>
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    /// <summary>Converts <paramref name="a"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with 
    /// values (<see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>), when expressed in SI units.</summary>
    public static explicit operator (double x, double y, double z)(Yank3 a) => (a.X, a.Y, a.Z);

    /// <summary>Converts the <see cref="Yank3"/> to the <see cref="Vector3"/> with components of equal magnitude, when expressed in SI units.</summary>
    public Vector3 ToVector3() => new(X, Y, Z);
    /// <summary>Converts <paramref name="a"/> to the <see cref="Vector3"/> with components of equal magnitude, when expressed in SI units.</summary>
    public static explicit operator Vector3(Yank3 a) => new(a.X, a.Y, a.Z);

    /// <summary>Constructs the <see cref="Yank3"/> with components equal to the values of <paramref name="components"/>, when expressed in SI units.</summary>
    public static Yank3 FromValueTuple((double x, double y, double z) components) => new(components);
    /// <summary>Constructs the <see cref="Yank3"/> with components equal to the values of <paramref name="components"/>, when expressed in SI units.</summary>
    public static explicit operator Yank3((double x, double y, double z) components) => new(components);

    /// <summary>Constructs the <see cref="Yank3"/> with components <paramref name="a"/>, when expressed in SI units.</summary>
    public static Yank3 FromVector3(Vector3 a) => new(a);
    /// <summary>Constructs the <see cref="Yank3"/> with components <paramref name="a"/>, when expressed in SI units.</summary>
    public static explicit operator Yank3(Vector3 a) => new(a);
}
