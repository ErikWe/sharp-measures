namespace SharpMeasures.Vector3Abstractions;

/// <summary>Describes a three-dimensional vector quantity that may be dot-multiplied by a quantity of type <typeparamref name="TFactorVector3Quantity"/>
/// to produce a quantity of type <typeparamref name="TProductScalarQuantity"/>.</summary>
/// <typeparam name="TProductScalarQuantity">The scalar quantity that is the dot-product of the two quantities.</typeparam>
/// <typeparam name="TFactorVector3Quantity">The three-dimensional vector quantity by which the original quantity may be dot-multiplied.</typeparam>
public interface IDotableVector3Quantity<out TProductScalarQuantity, in TFactorVector3Quantity> :
    IVector3Quantity
    where TProductScalarQuantity : IScalarQuantity
    where TFactorVector3Quantity : IVector3Quantity
{
    /// <summary>Performs dot-multiplication of the original quantity by the quantity <paramref name="factor"/> of type
    /// <typeparamref name="TFactorVector3Quantity"/>, resulting in a <typeparamref name="TProductScalarQuantity"/>.</summary>
    /// <param name="factor">The quantity by which the original quantity is dot-multiplied.</param>
    public abstract TProductScalarQuantity Dot(TFactorVector3Quantity factor);
}
