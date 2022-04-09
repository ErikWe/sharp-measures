namespace SharpMeasures.ScalarAbstractions;

/// <summary>Describes a scalar quantity from which a quantity of type <typeparamref name="TTermScalarQuantity"/> may be subtracted to produce
/// a quantity of type <typeparamref name="TDifferenceScalarQuantity"/>.</summary>
/// <typeparam name="TDifferenceScalarQuantity">The scalar quantity that is the difference of the two quantities.</typeparam>
/// <typeparam name="TTermScalarQuantity">The scalar quantity that may be subtracted from the original quantity.</typeparam>
public interface ISubtractableScalarQuantity<out TDifferenceScalarQuantity, in TTermScalarQuantity> :
    IScalarQuantity
    where TDifferenceScalarQuantity : IScalarQuantity
    where TTermScalarQuantity : IScalarQuantity
{
    /// <summary>Subtracts the quantity <paramref name="term"/> from the original quantity, resulting in a <typeparamref name="TDifferenceScalarQuantity"/>.</summary>
    /// <param name="term">The quantity that is subtracted from the original quantity.</param>
    public abstract TDifferenceScalarQuantity Subtract(TTermScalarQuantity term);
}