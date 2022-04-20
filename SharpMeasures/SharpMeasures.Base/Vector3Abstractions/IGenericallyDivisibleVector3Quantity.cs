namespace SharpMeasures.Vector3Abstractions;

using System;

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
    public abstract TQuotientVector3Quantity Divide<TQuotientVector3Quantity, TDivisorScalarQuantity>(TDivisorScalarQuantity divisor,
        Func<Vector3, TQuotientVector3Quantity> factory)
        where TQuotientVector3Quantity : IVector3Quantity
        where TDivisorScalarQuantity : IScalarQuantity;
}
