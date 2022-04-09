namespace SharpMeasures.Vector3Abstractions;

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