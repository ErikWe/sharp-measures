namespace SharpMeasures.Vector3Abstractions;

using System;

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
    public abstract TProductScalarQuantity Dot<TProductScalarQuantity, TFactorVector3Quantity>(TFactorVector3Quantity factor,
        Func<Scalar, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorVector3Quantity : IVector3Quantity;
}
