namespace SharpMeasures.Vector3Abstractions;

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
