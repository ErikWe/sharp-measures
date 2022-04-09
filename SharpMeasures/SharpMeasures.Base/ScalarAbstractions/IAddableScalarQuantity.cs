namespace SharpMeasures.ScalarAbstractions;

/// <summary>Describes a scalar quantity to which a quantity of type <typeparamref name="TTermVector3Quantity"/> may be added to produce a quantity
/// of type <typeparamref name="TSumVector3Quantity"/>.</summary>
/// <typeparam name="TSumVector3Quantity">The scalar quantity that is the sum of the two quantities.</typeparam>
/// <typeparam name="TTermVector3Quantity">The tscalar quantity that may be added to the original quantity.</typeparam>
public interface IAddableScalarQuantity<out TSumVector3Quantity, in TTermVector3Quantity> :
    IScalarQuantity
    where TSumVector3Quantity : IScalarQuantity
    where TTermVector3Quantity : IScalarQuantity
{
    /// <summary>Adds the quantity <paramref name="term"/> to the original quantity, resulting in a <typeparamref name="TSumVector3Quantity"/>.</summary>
    /// <param name="term">The quantity that is added to the original quantity.</param>
    public abstract TSumVector3Quantity Add(TTermVector3Quantity term);
}