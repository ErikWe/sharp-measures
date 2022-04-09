namespace SharpMeasures.ScalarAbstractions;

using System;

/// <summary>Describes a scalar quantity that may be divided by a quantity of a generically provided type, to produce a iquantity of another generically
/// provided type.</summary>
public interface IGenericallyDivisibleScalarQuantity :
    IScalarQuantity
{
    /// <summary>Divides the original quantity by the quantity <paramref name="divisor"/> of type <typeparamref name="TDivisorScalarQuantity"/>, resulting in a
    /// <typeparamref name="TQuotientScalarQuantity"/>.</summary>
    /// <typeparam name="TQuotientScalarQuantity">The scalar quantity that is the quotient of the two quantities.</typeparam>
    /// <typeparam name="TDivisorScalarQuantity">The type of the scalar quantity by which the original quantity may be divided.</typeparam>
    /// <param name="divisor">The quantity by which the original quantity is divided.</param>
    /// <param name="factory">Delegate for constructing a <typeparamref name="TQuotientScalarQuantity"/> from a <see langword="double"/>.</param>
    public abstract TQuotientScalarQuantity Divide<TQuotientScalarQuantity, TDivisorScalarQuantity>(TDivisorScalarQuantity divisor,
        Func<double, TQuotientScalarQuantity> factory)
        where TQuotientScalarQuantity : IScalarQuantity
        where TDivisorScalarQuantity : IScalarQuantity;
}