namespace SharpMeasures.Vector3Abstractions;

using System;

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
    public abstract TProductVector3Quantity Multiply<TProductVector3Quantity, TFactorScalarQuantity>(TFactorScalarQuantity factor,
        Func<Vector3, TProductVector3Quantity> factory)
        where TProductVector3Quantity : IVector3Quantity
        where TFactorScalarQuantity : IScalarQuantity;
}
