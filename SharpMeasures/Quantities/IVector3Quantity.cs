namespace ErikWe.SharpMeasures.Quantities;

using System;
using System.Numerics;

/// <summary>Describes a three-dimensional vector quantity.</summary>
public interface IVector3Quantity
{
    /// <summary>The magnitude of the X-component of the vector quantity.</summary>
    public abstract double MagnitudeX { get; }
    /// <summary>The magnitude of the Y-component of the vector quantity.</summary>
    public abstract double MagnitudeY { get; }
    /// <summary>The magnitude of the Z-component of the vector quantity.</summary>
    public abstract double MagnitudeZ { get; }

    /// <summary>Computes the magnitude, or norm, of the vector quantity.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when applicable.</remarks>
    public abstract Scalar Magnitude();
    /// <summary>Computes the square of the magnitude, or norm, of the vector quantity.</summary>
    public abstract Scalar SquaredMagnitude();
}

/// <summary>Describes a three-dimensional vector quantity that may be normalized.</summary>
/// <typeparam name="TNormalizedVector3Quantity">The three-dimensional vector quantity that is the normalized version of the original quantity,
/// generally <see langword="this"/>.</typeparam>
public interface INormalizableVector3Quantity<out TNormalizedVector3Quantity> :
    IVector3Quantity
    where TNormalizedVector3Quantity : IVector3Quantity
{
    /// <summary>Normalizes the quantity.</summary>
    public abstract TNormalizedVector3Quantity Normalize();
}

/// <summary>Describes a three-dimensional vector quantity that may be transformed according to a transformation matrix.</summary>
/// <typeparam name="TTransformedVector3Quantity">The three-dimensional vector quantity that is the result of transformation of the original quantity,
/// generally <see langword="this"/>.</typeparam>
public interface ITransformableVector3Quantity<out TTransformedVector3Quantity> :
    IVector3Quantity
    where TTransformedVector3Quantity : IVector3Quantity
{
    /// <summary>Transforms the quantity according to the transformation matrix <paramref name="transform"/>.</summary>
    /// <param name="transform">The quantity is transformed based on this transformation matrix.</param>
    public abstract TTransformedVector3Quantity Transform(Matrix4x4 transform);
}

/// <summary>Describes a three-dimensional vector quantity that may be scaled by a <see cref="Scalar"/> or <see cref="double"/>.</summary>
/// <typeparam name="TScaledVector3Quantity">The three-dimensional vector quantity that is the scaled version of the original quantity,
/// generally <see langword="this"/>.</typeparam>
public interface IScalableVector3Quantity<out TScaledVector3Quantity> :
    IVector3Quantity
    where TScaledVector3Quantity : IVector3Quantity
{
    /// <summary>Scales the vector quantity by <paramref name="factor"/>.</summary>
    /// <param name="factor">The vector quantity is scaled by this value.</param>
    public abstract TScaledVector3Quantity Multiply(double factor);
    /// <summary>Scales the vector quantity by <paramref name="factor"/>.</summary>
    /// <param name="factor">The vector quantity is scaled by this value.</param>
    public abstract TScaledVector3Quantity Multiply(Scalar factor);

    /// <summary>Scales the vector quantity through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The vector quantity is scaled through division by this value.</param>
    public abstract TScaledVector3Quantity Divide(double divisor);

    /// <summary>Scales the vector quantity through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The vector quantity is scaled through division by this value.</param>
    public abstract TScaledVector3Quantity Divide(Scalar divisor);
}

/// <summary>Describes a three-dimensional vector quantity to which a quantity of type <typeparamref name="TTermVector3Quantity"/> may be added to produce a quantity
/// of type <typeparamref name="TSumVector3Quantity"/>.</summary>
/// <typeparam name="TSumVector3Quantity">The three-dimensional vector quantity that is the sum of the two quantities.</typeparam>
/// <typeparam name="TTermVector3Quantity">The three-dimensional vector quantity that may be added to the original quantity.</typeparam>
public interface IAddableVector3Quantity<out TSumVector3Quantity, in TTermVector3Quantity> :
    IVector3Quantity
    where TSumVector3Quantity : IVector3Quantity
    where TTermVector3Quantity : IVector3Quantity
{
    /// <summary>Adds the quantity <paramref name="term"/> to the original quantity, resulting in a <typeparamref name="TSumVector3Quantity"/>.</summary>
    /// <param name="term">The quantity that is added to the original quantity.</param>
    public abstract TSumVector3Quantity Add(TTermVector3Quantity term);
}

/// <summary>Describes a three-dimensional vector quantity from which a quantity of type <typeparamref name="TTermScalarQuantity"/> may be subtracted to produce
/// a quantity of type <typeparamref name="TDifferenceScalarQuantity"/>.</summary>
/// <typeparam name="TDifferenceScalarQuantity">The three-dimensional vector quantity that is the difference of the two quantities.</typeparam>
/// <typeparam name="TTermScalarQuantity">The three-dimensional vector quantity that may be subtracted from the original quantity.</typeparam>
public interface ISubtractableVector3Quantity<out TDifferenceScalarQuantity, in TTermScalarQuantity> :
    IVector3Quantity
    where TDifferenceScalarQuantity : IVector3Quantity
    where TTermScalarQuantity : IVector3Quantity
{
    /// <summary>Subtracts the quantity <paramref name="term"/> from the original quantity, resulting in a <typeparamref name="TDifferenceScalarQuantity"/>.</summary>
    /// <param name="term">The quantity that is subtracted from the original quantity.</param>
    public abstract TDifferenceScalarQuantity Subtract(TTermScalarQuantity term);
}

/// <summary>Describes a three-dimensional vector quantity that may be multiplied by a scalar quantity of type <typeparamref name="TFactorScalarQuantity"/>
/// to produce a quantity of type <typeparamref name="TProductVector3Quantity"/>.</summary>
/// <typeparam name="TProductVector3Quantity">The three-dimensional vector quantity that is the product of the two quantities.</typeparam>
/// <typeparam name="TFactorScalarQuantity">The scalar quantity by which the original quantity may be multiplied.</typeparam>
public interface IMultiplicableVector3Quantity<out TProductVector3Quantity, in TFactorScalarQuantity> :
    IVector3Quantity
    where TProductVector3Quantity : IVector3Quantity
    where TFactorScalarQuantity : IScalarQuantity
{
    /// <summary>Multiplies the original quantity by the quantity <paramref name="factor"/>, resulting in a <typeparamref name="TProductVector3Quantity"/>.</summary>
    /// <param name="factor">The quantity by which the original quantity is multiplied.</param>
    public abstract TProductVector3Quantity Multiply(TFactorScalarQuantity factor);
}

/// <summary>Describes a three-dimensional vector quantity that may be multiplied by a scalar quantity of type <typeparamref name="TDivisorScalarQuantity"/>
/// to produce a quantity of type <typeparamref name="TQuotientVector3Quantity"/>.</summary>
/// <typeparam name="TQuotientVector3Quantity">The three-dimensional vector quantity that is the quotient of the two quantities.</typeparam>
/// <typeparam name="TDivisorScalarQuantity">The scalar quantity by which the original quantity may be divided.</typeparam>
public interface IDivisibleVector3Quantity<out TQuotientVector3Quantity, in TDivisorScalarQuantity> :
    IVector3Quantity
    where TQuotientVector3Quantity : IVector3Quantity
    where TDivisorScalarQuantity : IScalarQuantity
{
    /// <summary>Multiplies the original quantity by the quantity <paramref name="factor"/>, resulting in a <typeparamref name="TQuotientVector3Quantity"/>.</summary>
    /// <param name="factor">The quantity by which the original quantity is divided.</param>
    public abstract TQuotientVector3Quantity Multiply(TDivisorScalarQuantity factor);
}

/// <summary>Describes a three-dimensional vector quantity that may be dot-multiplied by a quantity of type <typeparamref name="TFactorVector3Quantity"/>
/// to produce a quantity of type <typeparamref name="TProductScalarQuantity"/>.</summary>
/// <typeparam name="TProductScalarQuantity">The scalar quantity that is the dot-product of the two quantities.</typeparam>
/// <typeparam name="TFactorVector3Quantity">The three-dimensional vector quantity by which the original quantity may be dot-multiplied.</typeparam>
public interface IDotableVector3Quantity<out TProductScalarQuantity, in TFactorVector3Quantity> :
    IVector3Quantity
    where TProductScalarQuantity : IScalarQuantity
    where TFactorVector3Quantity : IVector3Quantity
{
    /// <summary>Performs dot-multiplication of the original quantity by the quantity <paramref name="factor"/> of type
    /// <typeparamref name="TFactorVector3Quantity"/>, resulting in a <typeparamref name="TProductScalarQuantity"/>.</summary>
    /// <param name="factor">The quantity by which the original quantity is dot-multiplied.</param>
    public abstract TProductScalarQuantity Dot(TFactorVector3Quantity factor);
}

/// <summary>Describes a three-dimensional vector quantity that may be cross-multiplied by a quantity of type <typeparamref name="TFactorVector3Quantity"/>
/// to produce a quantity of type <typeparamref name="TProductVector3Quantity"/>.</summary>
/// <typeparam name="TProductVector3Quantity">The three-dimensional vector quantity that is the cross-product of the two quantities.</typeparam>
/// <typeparam name="TFactorVector3Quantity">The three-dimensional vector quantity by which the original quantity may be cross-multiplied.</typeparam>
public interface ICrossableVector3Quantity<out TProductVector3Quantity, in TFactorVector3Quantity> :
    IVector3Quantity
    where TProductVector3Quantity : IVector3Quantity
    where TFactorVector3Quantity : IVector3Quantity
{
    /// <summary>Performs cross-multiplication of the original quantity by the quantity <paramref name="factor"/> of type
    /// <typeparamref name="TFactorVector3Quantity"/>, resulting in a <typeparamref name="TProductVector3Quantity"/>.</summary>
    /// <param name="factor">The quantity by which the original quantity is cross-multiplied.</param>
    public abstract TProductVector3Quantity Cross(TFactorVector3Quantity factor);
}

/// <summary>Describes a three-dimensional vector quantity that may be multiplied by a scalar quantity of a generically provided type, to produce
/// a vector quantity of another generically provided type.</summary>
public interface IGenericallyMultiplicableVector3Quantity :
    IVector3Quantity
{
    /// <summary>Multiplies the original quantity by the quantity <paramref name="factor"/> of type <typeparamref name="TFactorScalarQuantity"/>, resulting in a
    /// <typeparamref name="TProductVector3Quantity"/>.</summary>
    /// <typeparam name="TProductVector3Quantity">The three-dimensional vector quantity that is the product of the two quantities.</typeparam>
    /// <typeparam name="TFactorScalarQuantity">The scalar quantity by which the original quantity may be multiplied.</typeparam>
    /// <param name="factor">The quantity by which the original quantity is multiplied.</param>
    /// <param name="factory">Delegate for constructing a <typeparamref name="TProductVector3Quantity"/>.</param>
    public abstract TProductVector3Quantity Multiply<TProductVector3Quantity, TFactorScalarQuantity>(TFactorScalarQuantity factor, Func<(double, double, double), TProductVector3Quantity> factory)
        where TProductVector3Quantity : IVector3Quantity
        where TFactorScalarQuantity : IScalarQuantity;
}

/// <summary>Describes a three-dimensional vector quantity that may be divided by a quantity of a generically provided type, to produce a
/// vector quantity of another generically provided type.</summary>
public interface IGenericallyDivisibleVector3Quantity :
    IVector3Quantity
{
    /// <summary>Divides the original quantity by the quantity <paramref name="divisor"/> of type <typeparamref name="TDivisorScalarQuantity"/>, resulting in a
    /// <typeparamref name="TQuotientVector3Quantity"/>.</summary>
    /// <typeparam name="TQuotientVector3Quantity">The three-dimensional vector quantity that is the quotient of the two quantities.</typeparam>
    /// <typeparam name="TDivisorScalarQuantity">The type of the scalar quantity by which the original quantity may be divided.</typeparam>
    /// <param name="divisor">The quantity by which the original quantity is divided.</param>
    /// <param name="factory">Delegate for constructing a <typeparamref name="TQuotientVector3Quantity"/>.</param>
    public abstract TQuotientVector3Quantity Divide<TQuotientVector3Quantity, TDivisorScalarQuantity>(TDivisorScalarQuantity divisor, Func<(double, double, double), TQuotientVector3Quantity> factory)
        where TQuotientVector3Quantity : IVector3Quantity
        where TDivisorScalarQuantity : IScalarQuantity;
}

/// <summary>Describes a three-dimensional vector quantity that may be dot-multiplied by a vector quantity of a generically provided type, to produce
/// a scalar quantity of another generically provided type.</summary>
public interface IGenericallyDotableVector3Quantity :
    IVector3Quantity
{
    /// <summary>Performs dot-multiplication of the original quantity by the quantity <paramref name="factor"/> of type <typeparamref name="TFactorVector3Quantity"/>, resulting in a
    /// <typeparamref name="TProductScalarQuantity"/>.</summary>
    /// <typeparam name="TProductScalarQuantity">The scalar quantity that is the dot-product of the two quantities.</typeparam>
    /// <typeparam name="TFactorVector3Quantity">The three-dimensional vector quantity by which the original quantity may be dot-multiplied.</typeparam>
    /// <param name="factor">The quantity by which the original quantity is dot-multiplied.</param>
    /// <param name="factory">Delegate for constructing a <typeparamref name="TProductScalarQuantity"/> from a <see cref="Scalar"/>.</param>
    public abstract TProductScalarQuantity Dot<TProductScalarQuantity, TFactorVector3Quantity>(TFactorVector3Quantity factor, Func<double, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorVector3Quantity : IVector3Quantity;
}

/// <summary>Describes a three-dimensional vector quantity that may be cross-multiplied by a quantity of a generically provided type, to produce a vector
/// quantity of another generically provided type.</summary>
public interface IGenericallyCrossableVector3Quantity :
    IVector3Quantity
{
    /// <summary>Performs cross-multiplication of the original quantity by the quantity <paramref name="factor"/> of type <typeparamref name="TFactorVector3Quantity"/>, resulting in a
    /// <typeparamref name="TProductVector3Quantity"/>.</summary>
    /// <typeparam name="TProductVector3Quantity">The three-dimensional vector quantity that is the cross-product of the two quantities.</typeparam>
    /// <typeparam name="TFactorVector3Quantity">The three-dimensional vector quantity by which the original quantity may be cross-multiplied.</typeparam>
    /// <param name="factor">The quantity by which the original quantity is cross-multiplied.</param>
    /// <param name="factory">Delegate for constructing a <typeparamref name="TProductVector3Quantity"/>.</param>
    public abstract TProductVector3Quantity Cross<TProductVector3Quantity, TFactorVector3Quantity>(TFactorVector3Quantity factor, Func<(double, double, double), TProductVector3Quantity> factory)
        where TProductVector3Quantity : IVector3Quantity
        where TFactorVector3Quantity : IVector3Quantity;
}

/// <summary>Describes a scalar quantity that may be multiplied by a three-dimensional vector quantity of type <typeparamref name="TFactorVector3Quantity"/>
/// to produce a quantity of type <typeparamref name="TProductVector3Quantity"/>.</summary>
/// <typeparam name="TProductVector3Quantity">The three-dimensional vector quantity that is the product of the two quantities.</typeparam>
/// <typeparam name="TFactorVector3Quantity">The three-dimensional vector quantity by which the original quantity may be multiplied.</typeparam>
public interface IVector3MultiplicableScalarQuantity<out TProductVector3Quantity, in TFactorVector3Quantity> :
    IScalarQuantity
    where TProductVector3Quantity : IVector3Quantity
    where TFactorVector3Quantity : IVector3Quantity
{
    /// <summary>Multiplies the original quantity by the quantity <paramref name="factor"/>, resulting in a <typeparamref name="TProductVector3Quantity"/>.</summary>
    /// <param name="factor">The quantity by which the original quantity is multiplied.</param>
    public abstract TProductVector3Quantity Multiply(TFactorVector3Quantity factor);
}