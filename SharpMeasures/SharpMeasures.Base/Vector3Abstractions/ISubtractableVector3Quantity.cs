namespace SharpMeasures.Vector3Abstractions;

/// <summary>Describes a three-dimensional vector quantity from which a quantity of type <typeparamref name="TTermScalarQuantity"/> may be subtracted to produce
/// a quantity of type <typeparamref name="TDifferenceScalarQuantity"/>.</summary>
/// <typeparam name="TDifferenceScalarQuantity">The three-dimensional vector quantity that is the difference of the two quantities.</typeparam>
/// <typeparam name="TTermScalarQuantity">The three-dimensional vector quantity that may be subtracted from the original quantity.</typeparam>
public interface ISubtractableVector3Quantity<out TDifferenceScalarQuantity, in TTermScalarQuantity> :
    IVector3Quantity
    where TDifferenceScalarQuantity : IVector3Quantity
    where TTermScalarQuantity : IVector3Quantity
{
    /// <summary>Subtracts the quantity <paramref name="term"/> from the original quantity, resulting in a <typeparamref name="TDifferenceScalarQuantity"/>.</summary>
    /// <param name="term">The quantity that is subtracted from the original quantity.</param>
    public abstract TDifferenceScalarQuantity Subtract(TTermScalarQuantity term);
}
