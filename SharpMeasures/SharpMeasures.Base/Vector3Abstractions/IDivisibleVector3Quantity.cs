namespace SharpMeasures.Vector3Abstractions;

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
