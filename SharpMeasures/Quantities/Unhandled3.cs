namespace ErikWe.SharpMeasures.Quantities;

using System;
using System.Numerics;

/// <summary>A measure of a three-dimensional quantity that is not covered by a designated type.</summary>
public readonly record struct Unhandled3 :
    IVector3Quantity,
    IScalableVector3Quantity<Unhandled3>,
    INormalizableVector3Quantity<Unhandled3>,
    ITransformableVector3Quantity<Unhandled3>,
    IAddableVector3Quantity<Unhandled3, Unhandled3>,
    ISubtractableVector3Quantity<Unhandled3, Unhandled3>,
    IMultiplicableVector3Quantity<Unhandled3, Scalar>,
    IDivisibleVector3Quantity<Unhandled3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<Unhandled, Vector3>,
    IDotableVector3Quantity<Unhandled, Unhandled3>,
    ICrossableVector3Quantity<Unhandled3, Vector3>,
    ICrossableVector3Quantity<Unhandled3, Unhandled3>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    /// <summary>A zero-magnitude <see cref="Unhandled3"/>.</summary>
    public static readonly Unhandled3 Zero = new(0, 0, 0);
    /// <summary>A <see cref="Unhandled3"/> with components (1, 1, 1).</summary>
    public static readonly Unhandled3 Ones = new(1, 1, 1);

    /// <summary>The magnitude of the X-component of the <see cref="Unhandled3"/>.</summary>
    public double X { get; init; }
    /// <summary>The magnitude of the Y-component of the <see cref="Unhandled3"/>.</summary>
    public double Y { get; init; }
    /// <summary>The magnitude of the Z-component of the <see cref="Unhandled3"/>.</summary>
    public double Z { get; init; }

    /// <summary>Retrieves the magnitudes of the components of the <see cref="Unhandled3"/>.</summary>
    public Vector3 Components => new(X, Y, Z);

    /// <summary>Constructs a new <see cref="Unhandled3"/> with components <paramref name="components"/>.</summary>
    /// <param name="components">The components of the <see cref="Unhandled3"/>.</param>
    public Unhandled3((Unhandled x, Unhandled y, Unhandled z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="Unhandled3"/> with components (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The X-component of the <see cref="Unhandled3"/>.</param>
    /// <param name="y">The Y-component of the <see cref="Unhandled3"/>.</param>
    /// <param name="z">The Z-component of the <see cref="Unhandled3"/>.</param>
    public Unhandled3(Unhandled x, Unhandled y, Unhandled z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="Unhandled3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Unhandled3"/>.</param>
    public Unhandled3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="Unhandled3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="Unhandled3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="Unhandled3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="Unhandled3"/>.</param>
    public Unhandled3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="Unhandled3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Unhandled3"/>.</param>
    public Unhandled3(Vector3 components) : this(components.X, components.Y, components.Z) { }
    /// <summary>Constructs a new <see cref="Unhandled3"/> with components of magnitudes <paramref name="components"/>.</summary>
    /// <param name="components">The magnitudes of the components of the <see cref="Unhandled3"/>.</param>
    public Unhandled3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="Unhandled3"/> with components of magnitudes (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The magnitude of the X-component of the <see cref="Unhandled3"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the <see cref="Unhandled3"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the <see cref="Unhandled3"/>.</param>
    public Unhandled3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <inheritdoc/>
    Scalar IVector3Quantity.Magnitude() => Maths.Vectors.Dot(this, this).SquareRoot();
    /// <inheritdoc/>
    Scalar IVector3Quantity.SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    /// <summary>Computes the magnitude, or norm, of the <see cref="Unhandled3"/>, as an <see cref="Unhandled"/> scalar quantity.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when applicable.</remarks>
    public Unhandled Magnitude() => SquaredMagnitude().SquareRoot();
    /// <summary>Computes the square of the magnitude, or norm, of the <see cref="Unhandled3"/>, as an <see cref="Unhandled"/> scalar quantity.</summary>
    public Unhandled SquaredMagnitude() => Dot(this);

    /// <summary>Computes the normalized <see cref="Unhandled3"/>.</summary>
    public Unhandled3 Normalize() => this / Magnitude();
    /// <summary>Computes the transformation of the <see cref="Unhandled3"/> by <paramref name="transform"/>.</summary>
    /// <param name="transform">The <see cref="Unhandled3"/> is transformed by this <see cref="Matrix4x4"/>.</param>
    public Unhandled3 Transform(Matrix4x4 transform) => new(Maths.Vectors.Transform(this, transform));

    /// <summary>Performs dot-multiplication of the <see cref="Unhandled3"/> by <paramref name="factor"/>, resulting in an
    /// <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The <see cref="Unhandled3"/> is dot-multiplied by this <see cref="Vector3"/>.</param>
    public Unhandled Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    /// <summary>Performs dot-multiplication of the <see cref="Unhandled3"/> by <paramref name="factor"/>, resulting in an
    /// <see cref="Unhandled"/> scalar quantity.</summary>
    /// <param name="factor">The original <see cref="Unhandled3"/> is dot-multiplied by this <see cref="Unhandled3"/>.</param>
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
    /// <summary>Performs dot-multiplication of the <see cref="Unhandled3"/> by <paramref name="factor"/>, resulting in an
    /// <see cref="Unhandled"/> scalar quantity.</summary>
    /// <typeparam name="TFactorVector3Quantity">The three-dimensional vector quantity by which the original <see cref="Unhandled3"/> may be dot-multiplied.</typeparam>
    /// <param name="factor">The original <see cref="Unhandled3"/> is dot-multiplied by this <typeparamref name="TFactorVector3Quantity"/>.</param>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled Dot<TFactorVector3Quantity>(TFactorVector3Quantity factor)
        where TFactorVector3Quantity : IVector3Quantity
    {
        if (factor == null)
        {
            throw new ArgumentNullException(nameof(factor));
        }
        else
        {
            return new(Maths.Vectors.Dot(this, factor));
        }
    }

    /// <summary>Performs cross-multiplication of the <see cref="Unhandled3"/> by <paramref name="factor"/>, resulting in a
    /// <cref see="Unhandled3"/>.</summary>
    /// <param name="factor">The <see cref="Unhandled3"/> is cross-multiplied by this <see cref="Vector3"/>.</param>
    public Unhandled3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    /// <summary>Performs cross-multiplication of the <see cref="Unhandled3"/> by <paramref name="factor"/>, resulting in an
    /// <see cref="Unhandled3"/>.</summary>
    /// <param name="factor">The original <see cref="Unhandled3"/> is cross-multiplied by this <see cref="Unhandled3"/>.</param>
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
    /// <summary>Performs cross-multiplication of the <see cref="Unhandled3"/> by <paramref name="factor"/>, resulting in an <see cref="Unhandled3"/>.</summary>
    /// <typeparam name="TFactorVector3Quantity">The three-dimensional vector quantity by which the original <see cref="Unhandled3"/> may be dot-multiplied.</typeparam>
    /// <param name="factor">The original <see cref="Unhandled3"/> is dot-multiplied by this <typeparamref name="TFactorVector3Quantity"/>.</param>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled3 Cross<TFactorVector3Quantity>(TFactorVector3Quantity factor)
        where TFactorVector3Quantity : IVector3Quantity
    {
        if (factor == null)
        {
            throw new ArgumentNullException(nameof(factor));
        }
        else
        {
            return new(Maths.Vectors.Cross(this, factor));
        }
    }

    /// <summary>Produces a formatted string from the magnitudes of the components of the <see cref="Unhandled3"/>, followed by the symbol [undef].</summary>
    public override string ToString() => $"({X}, {Y}, {Z}) [undef]";

    /// <summary>Unary plus, resulting in the unmodified <see cref="Unhandled3"/>.</summary>
    public Unhandled3 Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Unhandled3"/> with negated components.</summary>
    public Unhandled3 Negate() => new(-X, -Y, -Z);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="a"/>.</summary>
    /// <param name="a">Unary plus is applied to this <see cref="Unhandled3"/>.</param>
    public static Unhandled3 operator +(Unhandled3 a) => a.Plus();
    /// <summary>Negation, resulting in a <see cref="Unhandled3"/> with negated components from that of <paramref name="a"/>.</summary>
    /// <param name="a">Negation is applied to this <see cref="Unhandled3"/>.</param>
    public static Unhandled3 operator -(Unhandled3 a) => a.Negate();

    /// <summary>Adds <paramref name="term"/> to the <see cref="Unhandled3"/>.</summary>
    /// <param name="term">This value is added to the <see cref="Unhandled3"/>.</param>
    public Unhandled3 Add(Unhandled3 term) => new(X + term.X, Y + term.Y, Z + term.Z);
    /// <summary>Subtracts <paramref name="term"/> from the <see cref="Unhandled3"/>.</summary>
    /// <param name="term">This value is subtracted from the <see cref="Unhandled3"/>.</param>
    public Unhandled3 Subtract(Unhandled3 term) => new(X - term.X, Y - term.Y, Z - term.Z);
    /// <summary>Addition of <paramref name="a"/> and <paramref name="b"/>.</summary>
    /// <param name="a">The first term of the addition.</param>
    /// <param name="b">The second term of the addition.</param>
    public static Unhandled3 operator +(Unhandled3 a, Unhandled3 b) => a.Add(b);
    /// <summary>Subtraction of <paramref name="b"/> from <paramref name="a"/>.</summary>
    /// <param name="a">The original value, from which <paramref name="b"/> is subtracted.</param>
    /// <param name="b">This value is subtracted from <paramref name="a"/>.</param>
    public static Unhandled3 operator -(Unhandled3 a, Unhandled3 b) => a.Subtract(b);

    /// <summary>Multiplicates the <see cref="Unhandled3"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Unhandled3"/> is multiplied.</param>
    public Unhandled3 Multiply(Unhandled factor) => new(X * factor, Y * factor, Z * factor);
    /// <summary>Divides the <see cref="Unhandled3"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Unhandled3"/> is divided.</param>
    public Unhandled3 Divide(Unhandled divisor) => new(X / divisor, Y / divisor, Z / divisor);
    /// <summary>Multiplication of the <see cref="Unhandled3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Unhandled3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="Unhandled3"/> <paramref name="a"/> is multiplied.</param>
    public static Unhandled3 operator *(Unhandled3 a, Unhandled b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="b"/> by the <see cref="Unhandled3"/> <paramref name="a"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Unhandled"/> quantity by which the <see cref="Unhandled3"/> <paramref name="b"/> is multiplied.</param>
    /// <param name="b">The <see cref="Unhandled3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="a"/>.</param>
    public static Unhandled3 operator *(Unhandled a, Unhandled3 b) => b.Multiply(a);
    /// <summary>Division of the <see cref="Unhandled3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Unhandled3"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="Unhandled3"/> <paramref name="a"/> is divded.</param>
    public static Unhandled3 operator /(Unhandled3 a, Unhandled b) => a.Divide(b);

    /// <summary>Produces an <see cref="Unhandled3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Unhandled3 Remainder(double divisor) => new(X % divisor, Y % divisor, Z % divisor);
    /// <summary>Scales the <see cref="Unhandled3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Unhandled3"/> is scaled.</param>
    public Unhandled3 Multiply(double factor) => new(X * factor, Y * factor, Z * factor);
    /// <summary>Scales the <see cref="Unhandled3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Unhandled3"/> is divided.</param>
    public Unhandled3 Divide(double divisor) => new(X / divisor, Y / divisor, Z / divisor);
    /// <summary>Produces a <see cref="Unhandled3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Unhandled3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is produced from division of <see cref="Unhandled3"/> <paramref name="a"/> by this value.</param>
    public static Unhandled3 operator %(Unhandled3 a, double b) => a.Remainder(b);
    /// <summary>Scales the <see cref="Unhandled3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Unhandled3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="Unhandled3"/> <paramref name="a"/>.</param>
    public static Unhandled3 operator *(Unhandled3 a, double b) => a.Multiply(b);
    /// <summary>Scales the <see cref="Unhandled3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="Unhandled3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled3"/>, which is scaled by <paramref name="a"/>.</param>
    public static Unhandled3 operator *(double a, Unhandled3 b) => b.Multiply(a);
    /// <summary>Scales the <see cref="Unhandled3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Unhandled3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="Unhandled3"/> <paramref name="a"/>.</param>
    public static Unhandled3 operator /(Unhandled3 a, double b) => a.Divide(b);

    /// <summary>Produces an <see cref="Unhandled3"/>, with each component equal to the remainder from division of the
    /// magnitude of the original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Unhandled3 Remainder(Scalar divisor) => new(X % divisor, Y % divisor, Z % divisor);
    /// <summary>Scales the <see cref="Unhandled3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Unhandled3"/> is scaled.</param>
    public Unhandled3 Multiply(Scalar factor) => new(X * factor, Y * factor, Z * factor);
    /// <summary>Scales the <see cref="Unhandled3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Unhandled3"/> is divided.</param>
    public Unhandled3 Divide(Scalar divisor) => new(X / divisor, Y / divisor, Z / divisor);
    /// <summary>Produces a <see cref="Unhandled3"/>, with each component equal to the remainder from division of the
    /// magnitude of the component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Unhandled3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is produced from division of <see cref="Unhandled3"/> <paramref name="a"/> by this value.</param>
    public static Unhandled3 operator %(Unhandled3 a, Scalar b) => a.Remainder(b);
    /// <summary>Scales the <see cref="Unhandled3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Unhandled3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="Unhandled3"/> <paramref name="a"/>.</param>
    public static Unhandled3 operator *(Unhandled3 a, Scalar b) => a.Multiply(b);
    /// <summary>Scales the <see cref="Unhandled3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="Unhandled3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled3"/>, which is scaled by <paramref name="a"/>.</param>
    public static Unhandled3 operator *(Scalar a, Unhandled3 b) => b.Multiply(a);
    /// <summary>Scales the <see cref="Unhandled3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Unhandled3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="Unhandled3"/> <paramref name="a"/>.</param>
    public static Unhandled3 operator /(Unhandled3 a, Scalar b) => a.Divide(b);

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
    /// <summary>Multiplies the <see cref="Unhandled3"/> by <paramref name="factor"/> of type <typeparamref name="TFactorScalarQuantity"/>.</summary>
    /// <typeparam name="TFactorScalarQuantity">The type of the quantity by which this <see cref="Unhandled3"/> is multiplied.</typeparam>
    /// <param name="factor">The <see cref="Unhandled3"/> is multiplied by this quantity.</param>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled3 Multiply<TFactorScalarQuantity>(TFactorScalarQuantity factor)
        where TFactorScalarQuantity : IScalarQuantity
    {
        if (factor == null)
        {
            throw new ArgumentNullException(nameof(factor));
        }
        else
        {
            return new(X * factor.Magnitude, Y * factor.Magnitude, Z * factor.Magnitude);
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
    /// <summary>Divides the <see cref="Unhandled3"/> by <paramref name="divisor"/> of type <typeparamref name="TDivisorQuantity"/>.</summary>
    /// <typeparam name="TDivisorQuantity">The type of the quantity by which this <see cref="Unhandled3"/> quantity is divided.</typeparam>
    /// <param name="divisor">The <see cref="Unhandled3"/> is divided by this quantity.</param>
    /// <exception cref="ArgumentNullException"/>
    public Unhandled3 Divide<TDivisorQuantity>(TDivisorQuantity divisor)
        where TDivisorQuantity : IScalarQuantity
    {
        if (divisor == null)
        {
            throw new ArgumentNullException(nameof(divisor));
        }
        else
        {
            return new(X / divisor.Magnitude, Y / divisor.Magnitude, Z / divisor.Magnitude);
        }
    }

    /// <summary>Multiplication of the <see cref="Unhandled3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Unhandled3"/>, which is multiplied by <paramref name="b"/>.</param>
    /// <param name="b">This quantity is multiplied by the <see cref="Unhandled3"/> <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{ValueTuple{double, double, double}, TProductVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(Unhandled3 a, IScalarQuantity b) => a.Multiply(b, (x) => new Unhandled3(x));
    /// <summary>Multiplication of the quantity <paramref name="a"/> by the <see cref="Unhandled3"/> <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">This quantity is multiplied by the <see cref="Unhandled3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled3"/>, which is multiplied by <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{ValueTuple{double, double, double}, TProductVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(IScalarQuantity a, Unhandled3 b) => b.Multiply(a, (x) => new Unhandled3(x));
    /// <summary>Division of the <see cref="Unhandled3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Unhandled3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled3"/> <paramref name="a"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientVector3Quantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{ValueTuple{double, double, double}, TQuotientVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator /(Unhandled3 a, IScalarQuantity b) => a.Divide(b, (x) => new Unhandled3(x));

    /// <summary>Converts the <see cref="Unhandled3"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with
    /// values (<see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>).</summary>
    public (double x, double y, double z) ToValueTuple() => (X, Y, Z);
    /// <summary>Converts <paramref name="a"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with
    /// values (<see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>).</summary>
    public static explicit operator (double x, double y, double z)(Unhandled3 a) => (a.X, a.Y, a.Z);

    /// <summary>Converts the <see cref="Unhandled3"/> to the <see cref="Vector3"/> with components of equal magnitude.</summary>
    public Vector3 ToVector3() => new(X, Y, Z);
    /// <summary>Converts <paramref name="a"/> to the <see cref="Vector3"/> with components of equal magnitude.</summary>
    public static explicit operator Vector3(Unhandled3 a) => new(a.X, a.Y, a.Z);

    /// <summary>Constructs the <see cref="Unhandled3"/> with components equal to the values of <paramref name="components"/>.</summary>
    public static Unhandled3 FromValueTuple((double x, double y, double z) components) => new(components);
    /// <summary>Constructs the <see cref="Unhandled3"/> with components equal to the values of <paramref name="components"/>.</summary>
    public static explicit operator Unhandled3((double x, double y, double z) components) => new(components);

    /// <summary>Constructs the <see cref="Unhandled3"/> with components <paramref name="a"/>.</summary>
    public static Unhandled3 FromVector3(Vector3 a) => new(a);
    /// <summary>Constructs the <see cref="Unhandled3"/> with components <paramref name="a"/>.</summary>
    public static explicit operator Unhandled3(Vector3 a) => new(a);
}