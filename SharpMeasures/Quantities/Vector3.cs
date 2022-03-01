namespace ErikWe.SharpMeasures.Quantities;

using System;
using System.Numerics;

/// <summary>A three-dimensional vector, with a X, Y, and Z component.</summary>
public readonly record struct Vector3 :
    IVector3Quantity,
    IScalableVector3Quantity<Vector3>,
    INormalizableVector3Quantity<Vector3>,
    ITransformableVector3Quantity<Vector3>,
    IAddableVector3Quantity<Vector3, Vector3>,
    ISubtractableVector3Quantity<Vector3, Vector3>,
    IMultiplicableVector3Quantity<Vector3, Scalar>,
    IDivisibleVector3Quantity<Vector3, Scalar>,
    IMultiplicableVector3Quantity<Unhandled3, Unhandled>,
    IDivisibleVector3Quantity<Unhandled3, Unhandled>,
    IDotableVector3Quantity<Scalar, Vector3>,
    IDotableVector3Quantity<Unhandled, Unhandled3>,
    ICrossableVector3Quantity<Vector3, Vector3>,
    ICrossableVector3Quantity<Unhandled3, Unhandled3>,
    IGenericallyMultiplicableVector3Quantity,
    IGenericallyDivisibleVector3Quantity,
    IGenericallyDotableVector3Quantity,
    IGenericallyCrossableVector3Quantity
{
    /// <summary>The zero-magnitude <see cref="Vector3"/>.</summary>
    public static Vector3 Zero { get; } = new(0, 0, 0);
    /// <summary>The <see cref="Vector3"/> with components (1, 1, 1).</summary>
    public static Vector3 Ones { get; } = new(1, 1, 1);

    /// <summary>The magnitude of the X-component of the <see cref="Vector3"/>.</summary>
    public double MagnitudeX { get; }
    /// <summary>The magnitude of the Y-component of the <see cref="Vector3"/>.</summary>
    public double MagnitudeY { get; }
    /// <summary>The magnitude of the Z-component of the <see cref="Vector3"/>.</summary>
    public double MagnitudeZ { get; }

    /// <summary>The X-component of the <see cref="Vector3"/>.</summary>
    public Scalar X => new(MagnitudeX);
    /// <summary>The Y-component of the <see cref="Vector3"/>.</summary>
    public Scalar Y => new(MagnitudeY);
    /// <summary>The Z-component of the <see cref="Vector3"/>.</summary>
    public Scalar Z => new(MagnitudeZ);

    /// <summary>Constructs a new <see cref="Vector3"/> with <paramref name="components"/>.</summary>
    /// <param name="components">The components of the <see cref="Vector3"/>.</param>
    public Vector3((Scalar x, Scalar y, Scalar z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="Vector3"/> with components (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The X-component of the <see cref="Vector3"/>.</param>
    /// <param name="y">The Y-component of the <see cref="Vector3"/>.</param>
    /// <param name="z">The Z-component of the <see cref="Vector3"/>.</param>
    public Vector3(Scalar x, Scalar y, Scalar z) : this(x.Magnitude, y.Magnitude, z.Magnitude) { }
    /// <summary>Constructs a new <see cref="Vector3"/> with <paramref name="components"/>.</summary>
    /// <param name="components">The components of the <see cref="Vector3"/>.</param>
    public Vector3((double x, double y, double z) components) : this(components.x, components.y, components.z) { }
    /// <summary>Constructs a new <see cref="Vector3"/> with components (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).</summary>
    /// <param name="x">The X-component of the <see cref="Vector3"/>.</param>
    /// <param name="y">The Y-component of the <see cref="Vector3"/>.</param>
    /// <param name="z">The Z-component of the <see cref="Vector3"/>.</param>
    public Vector3(double x, double y, double z)
    {
        MagnitudeX = x;
        MagnitudeY = y;
        MagnitudeZ = z;
    }

    /// <summary>Computes the magnitude, or norm, of the vector quantity.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when applicable.</remarks>
    public Scalar Magnitude() => SquaredMagnitude().SquareRoot();
    /// <summary>Computes the square of the magnitude, or norm, of the vector quantity.</summary>
    public Scalar SquaredMagnitude() => Maths.Vectors.Dot(this, this);

    /// <summary>Computes the normalized <see cref="Vector3"/>.</summary>
    public Vector3 Normalize() => this / Magnitude();
    /// <summary>Computes the transformation of the <see cref="Vector3"/> by <paramref name="transform"/>.</summary>
    /// <param name="transform">The <see cref="Vector3"/> is transformed by this <see cref="Matrix4x4"/>.</param>
    public Vector3 Transform(Matrix4x4 transform) => Maths.Vectors.Transform(this, transform);

    /// <summary>Performs dot-multiplication of the <see cref="Vector3"/> by <paramref name="factor"/>, resulting in a
    /// <see cref="Scalar"/> quantity.</summary>
    /// <param name="factor">The original <see cref="Vector3"/> is dot-multiplied by this <see cref="Vector3"/>.</param>
    public Scalar Dot(Vector3 factor) => new(Maths.Vectors.Dot(this, factor));
    /// <summary>Performs dot-multiplication of the <see cref="Vector3"/> by <paramref name="factor"/>, resulting in an
    /// <see cref="Unhandled"/> scalar quantity.</summary>
    /// <param name="factor">The <see cref="Vector3"/> is dot-multiplied by this <see cref="Vector3"/>.</param>
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

    /// <summary>Performs cross-multiplication of the <see cref="Vector3"/> by <paramref name="factor"/>, resulting in a
    /// <cref see="Vector3"/>.</summary>
    /// <param name="factor">The original <see cref="Vector3"/> is cross-multiplied by this <see cref="Vector3"/>.</param>
    public Vector3 Cross(Vector3 factor) => new(Maths.Vectors.Cross(this, factor));
    /// <summary>Performs cross-multiplication of the <see cref="Vector3"/> by <paramref name="factor"/>, resulting in an
    /// <see cref="Unhandled3"/>.</summary>
    /// <param name="factor">The <see cref="Vector3"/> is cross-multiplied by this <see cref="Unhandled3"/>.</param>
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

    /// <summary>Produces a formatted string from the components of the <see cref="Vector3"/>.</summary>
    public override string ToString() => $"({MagnitudeX}, {MagnitudeY}, {MagnitudeZ})";

    /// <summary>Unary plus, resulting in the unmodified <see cref="Vector3"/>.</summary>
    public Vector3 Plus() => this;
    /// <summary>Negation, resulting in a <see cref="Vector3"/> with negated components.</summary>
    public Vector3 Negate() => new(-MagnitudeX, -MagnitudeY, -MagnitudeZ);
    /// <summary>Unary plus, resulting in the unmodified <paramref name="a"/>.</summary>
    /// <param name="a">Unary plus is applied to this <see cref="Vector3"/>.</param>
    public static Vector3 operator +(Vector3 a) => a.Plus();
    /// <summary>Negation, resulting in a <see cref="Vector3"/> with negated components from that of <paramref name="a"/>.</summary>
    /// <param name="a">Negation is applied to this <see cref="Vector3"/>.</param>
    public static Vector3 operator -(Vector3 a) => a.Negate();

    /// <summary>Adds <paramref name="term"/> to the <see cref="Vector3"/>.</summary>
    /// <param name="term">This value is added to the <see cref="Vector3"/>.</param>
    public Vector3 Add(Vector3 term) => new(MagnitudeX + term.MagnitudeX, MagnitudeY + term.MagnitudeY, MagnitudeZ + term.MagnitudeZ);
    /// <summary>Subtracts <paramref name="term"/> from the <see cref="Vector3"/>.</summary>
    /// <param name="term">This value is subtracted from the <see cref="Vector3"/> quantity.</param>
    public Vector3 Subtract(Vector3 term) => new(MagnitudeX - term.MagnitudeX, MagnitudeY - term.MagnitudeY, MagnitudeZ - term.MagnitudeZ);
    /// <summary>Addition of <paramref name="a"/> and <paramref name="b"/>.</summary>
    /// <param name="a">The first term of the addition.</param>
    /// <param name="b">The second term of the addition.</param>
    public static Vector3 operator +(Vector3 a, Vector3 b) => a.Add(b);
    /// <summary>Subtraction of <paramref name="b"/> from <paramref name="a"/>.</summary>
    /// <param name="a">The original value, from which <paramref name="b"/> is subtracted.</param>
    /// <param name="b">This value is subtracted from <paramref name="a"/>.</param>
    public static Vector3 operator -(Vector3 a, Vector3 b) => a.Subtract(b);

    /// <summary>Multiplicates the <see cref="Vector3"/> by the <see cref="Unhandled"/> quantity <paramref name="factor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="factor">The factor by which the <see cref="Vector3"/> is multiplied.</param>
    public Unhandled3 Multiply(Unhandled factor) => new(MagnitudeX * factor, MagnitudeY * factor, MagnitudeZ * factor);
    /// <summary>Divides the <see cref="Vector3"/> by the <see cref="Unhandled"/> quantity <paramref name="divisor"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the <see cref="Vector3"/> is divided.</param>
    public Unhandled3 Divide(Unhandled divisor) => new(MagnitudeX / divisor, MagnitudeY / divisor, MagnitudeZ / divisor);
    /// <summary>Multiplication of the <see cref="Vector3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Vector3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="Vector3"/> <paramref name="a"/> is multiplied.</param>
    public static Unhandled3 operator *(Vector3 a, Unhandled b) => a.Multiply(b);
    /// <summary>Multiplication of the <see cref="Unhandled"/> quantity <paramref name="b"/> by the <see cref="Vector3"/> <paramref name="a"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Unhandled"/> quantity by which the <see cref="Vector3"/> <paramref name="b"/> is multiplied.</param>
    /// <param name="b">The <see cref="Vector3"/>, which is multiplied by the <see cref="Unhandled"/> quantity <paramref name="a"/>.</param>
    public static Unhandled3 operator *(Unhandled a, Vector3 b) => b.Multiply(a);
    /// <summary>Division of the <see cref="Vector3"/> <paramref name="a"/> by the <see cref="Unhandled"/> quantity <paramref name="b"/> -
    /// resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Vector3"/>, which is divided by the <see cref="Unhandled"/> quantity <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Unhandled"/> quantity by which the <see cref="Vector3"/> <paramref name="a"/> is divded.</param>
    public static Unhandled3 operator /(Vector3 a, Unhandled b) => a.Divide(b);

    /// <summary>Produces an <see cref="Vector3"/>, with each component equal to the remainder from division of the
    /// original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Vector3 Remainder(double divisor) => new(MagnitudeX % divisor, MagnitudeY % divisor, MagnitudeZ % divisor);
    /// <summary>Scales the <see cref="Vector3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Vector3"/> is scaled.</param>
    public Vector3 Multiply(double factor) => new(MagnitudeX * factor, MagnitudeY * factor, MagnitudeZ * factor);
    /// <summary>Scales the <see cref="Vector3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Vector3"/> is divided.</param>
    public Vector3 Divide(double divisor) => new(MagnitudeX / divisor, MagnitudeY / divisor, MagnitudeZ / divisor);
    /// <summary>Produces a <see cref="Vector3"/>, with each component equal to the remainder from division of the
    /// component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Vector3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is produced from division of <see cref="Vector3"/> <paramref name="a"/> by this value.</param>
    public static Vector3 operator %(Vector3 a, double b) => a.Remainder(b);
    /// <summary>Scales the <see cref="Vector3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Vector3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Vector3 operator *(Vector3 a, double b) => a.Multiply(b);
    /// <summary>Scales the <see cref="Vector3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Vector3"/>, which is scaled by <paramref name="a"/>.</param>
    public static Vector3 operator *(double a, Vector3 b) => b.Multiply(a);
    /// <summary>Scales the <see cref="Vector3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Vector3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Vector3 operator /(Vector3 a, double b) => a.Divide(b);

    /// <summary>Produces an <see cref="Vector3"/>, with each component equal to the remainder from division of the
    /// original component by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The remainder is produced from division by this value.</param>
    public Vector3 Remainder(Scalar divisor) => new(MagnitudeX % divisor, MagnitudeY % divisor, MagnitudeZ % divisor);
    /// <summary>Scales the <see cref="Vector3"/> by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the <see cref="Vector3"/> is scaled.</param>
    public Vector3 Multiply(Scalar factor) => new(MagnitudeX * factor, MagnitudeY * factor, MagnitudeZ * factor);
    /// <summary>Scales the <see cref="Vector3"/> through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the <see cref="Vector3"/> is divided.</param>
    public Vector3 Divide(Scalar divisor) => new(MagnitudeX / divisor, MagnitudeY / divisor, MagnitudeZ / divisor);
    /// <summary>Produces a <see cref="Vector3"/>, with each component equal to the remainder from division of the
    /// component of <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Vector3"/>, the components of which are divided by <paramref name="b"/> to produce a remainder.</param>
    /// <param name="b">The remainder is produced from division of <see cref="Vector3"/> <paramref name="a"/> by this value.</param>
    public static Vector3 operator %(Vector3 a, Scalar b) => a.Remainder(b);
    /// <summary>Scales the <see cref="Vector3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Vector3"/>, which is scaled by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to scale the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Vector3 operator *(Vector3 a, Scalar b) => a.Multiply(b);
    /// <summary>Scales the <see cref="Vector3"/> <paramref name="a"/> by <paramref name="b"/>.</summary>
    /// <param name="a">This value is used to scale the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Vector3"/>, which is scaled by <paramref name="a"/>.</param>
    public static Vector3 operator *(Scalar a, Vector3 b) => b.Multiply(a);
    /// <summary>Scales the <see cref="Vector3"/> <paramref name="a"/> through division by <paramref name="b"/>.</summary>
    /// <param name="a">The <see cref="Vector3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">This value is used to divide the <see cref="Vector3"/> <paramref name="a"/>.</param>
    public static Vector3 operator /(Vector3 a, Scalar b) => a.Divide(b);

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

    /// <summary>Multiplication of the <see cref="Vector3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Vector3"/>, which is multiplied by <paramref name="b"/>.</param>
    /// <param name="b">This quantity is multiplied by the <see cref="Vector3"/> <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{ValueTuple{double, double, double}, TProductVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(Vector3 a, IScalarQuantity b) => a.Multiply(b, (x) => new Unhandled3(x));
    /// <summary>Multiplication of the quantity <paramref name="a"/> by the <see cref="Vector3"/> <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">This quantity is multiplied by the <see cref="Vector3"/> <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Vector3"/>, which is multiplied by <paramref name="a"/>.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Multiply{TProductVector3Quantity, TFactorScalarQuantity}(TFactorScalarQuantity,
    /// Func{ValueTuple{double, double, double}, TProductVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator *(IScalarQuantity a, Vector3 b) => b.Multiply(a, (x) => new Unhandled3(x));
    /// <summary>Division of the <see cref="Vector3"/> <paramref name="a"/> by the quantity <paramref name="b"/>
    /// - resulting in an <see cref="Unhandled3"/> quantity.</summary>
    /// <param name="a">The <see cref="Vector3"/>, which is divided by <paramref name="b"/>.</param>
    /// <param name="b">The <see cref="Vector3"/> <paramref name="a"/> is divided by this quantity.</param>
    /// <remarks>To avoid boxing, prefer <see cref="Divide{TQuotientVector3Quantity, TDivisorScalarQuantity}(TDivisorScalarQuantity,
    /// Func{ValueTuple{double, double, double}, TQuotientVector3Quantity})"/>.</remarks>
    /// <exception cref="ArgumentNullException"/>
    public static Unhandled3 operator /(Vector3 a, IScalarQuantity b) => a.Divide(b, (x) => new Unhandled3(x));

    /// <summary>Converts the <see cref="Vector3"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with
    /// values (<see cref="MagnitudeX"/>, <see cref="MagnitudeY"/>, <see cref="MagnitudeZ"/>).</summary>
    public (double x, double y, double z) ToValueTuple() => (MagnitudeX, MagnitudeY, MagnitudeZ);
    /// <summary>Converts <paramref name="a"/> to a (<see langword="double"/>, <see langword="double"/>, <see langword="double"/>) with
    /// values (<see cref="MagnitudeX"/>, <see cref="MagnitudeY"/>, <see cref="MagnitudeZ"/>).</summary>
    public static implicit operator (double x, double y, double z)(Vector3 a) => (a.MagnitudeX, a.MagnitudeY, a.MagnitudeZ);

    /// <summary>Constructs the <see cref="Vector3"/> with components equal to the values of <paramref name="components"/>.</summary>
    public static Vector3 FromValueTuple((double x, double y, double z) components) => new(components.x, components.y, components.z);
    /// <summary>Constructs the <see cref="Vector3"/> with components equal to the values of <paramref name="components"/>.</summary>
    public static explicit operator Vector3((double x, double y, double z) components) => new(components.x, components.y, components.z);
}