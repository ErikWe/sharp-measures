namespace SharpMeasures.Vector3Abstractions;

/// <summary>Describes a three-dimensional vector quantity that may be cross-multiplied by a quantity of type <typeparamref name="TFactorVector3Quantity"/>
/// to produce a quantity of type <typeparamref name="TProductVector3Quantity"/>.</summary>
/// <typeparam name="TProductVector3Quantity">The three-dimensional vector quantity that is the cross-product of the two quantities.</typeparam>
/// <typeparam name="TFactorVector3Quantity">The three-dimensional vector quantity by which the original quantity may be cross-multiplied.</typeparam>
public interface ICrossableVector3Quantity<out TProductVector3Quantity, in TFactorVector3Quantity> :
    IVector3Quantity
    where TProductVector3Quantity : IVector3Quantity
    where TFactorVector3Quantity : IVector3Quantity
{
    /// <summary>Performs cross-multiplication of the original quantity by the quantity <paramref name="factor"/> of type
    /// <typeparamref name="TFactorVector3Quantity"/>, resulting in a <typeparamref name="TProductVector3Quantity"/>.</summary>
    /// <param name="factor">The quantity by which the original quantity is cross-multiplied.</param>
    public abstract TProductVector3Quantity Cross(TFactorVector3Quantity factor);
}
