namespace SharpMeasures.ScalarAbstractions;

/// <summary>Describes a scalar quantity that may be divided by a quantity of type <typeparamref name="TDivisorScalarQuantity"/>
/// to produce a quantity of type <typeparamref name="TQuotientScalarQuantity"/>.</summary>
/// <typeparam name="TQuotientScalarQuantity">The scalar quantity that is the quotient of the two quantities.</typeparam>
/// <typeparam name="TDivisorScalarQuantity">The scalar quantity by which the original quantity may be divided.</typeparam>
public interface IDivisibleScalarQuantity<out TQuotientScalarQuantity, in TDivisorScalarQuantity> :
    IScalarQuantity
    where TQuotientScalarQuantity : IScalarQuantity
    where TDivisorScalarQuantity : IScalarQuantity
{
    /// <summary>Divides the original quantity by the quantity <paramref name="divisor"/>, resulting in a <typeparamref name="TQuotientScalarQuantity"/>.</summary>
    /// <param name="divisor">The quantity by which the original quantity is divided.</param>
    public abstract TQuotientScalarQuantity Divide(TDivisorScalarQuantity divisor);
}