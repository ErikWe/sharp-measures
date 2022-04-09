namespace SharpMeasures.ScalarAbstractions;

using System;

/// <summary>Describes a scalar quantity that may be multiplied by a quantity of a generically provided type, to produce a quantity of another generically
/// provided type.</summary>
public interface IGenericallyMultiplicableScalarQuantity :
    IScalarQuantity
{
    /// <summary>Multiplies the original quantity by the quantity <paramref name="factor"/> of type <typeparamref name="TFactorScalarQuantity"/>, resulting in a
    /// <typeparamref name="TProductScalarQuantity"/>.</summary>
    /// <typeparam name="TProductScalarQuantity">The scalar quantity that is the product of the two quantities.</typeparam>
    /// <typeparam name="TFactorScalarQuantity">The scalar quantity by which the original quantity may be multiplied.</typeparam>
    /// <param name="factor">The quantity by which the original quantity is multiplied.</param>
    /// <param name="factory">Delegate for constructing a <typeparamref name="TProductScalarQuantity"/> from a <see langword="double"/>.</param>
    public abstract TProductScalarQuantity Multiply<TProductScalarQuantity, TFactorScalarQuantity>(TFactorScalarQuantity factor,
        Func<double, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorScalarQuantity : IScalarQuantity;
}