namespace SharpMeasures.Vector3Abstractions;

using System;

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
    public abstract TProductVector3Quantity Cross<TProductVector3Quantity, TFactorVector3Quantity>(TFactorVector3Quantity factor,
        Func<Vector3, TProductVector3Quantity> factory)
        where TProductVector3Quantity : IVector3Quantity
        where TFactorVector3Quantity : IVector3Quantity;
}
