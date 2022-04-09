namespace SharpMeasures.ScalarAbstractions;

/// <summary>Describes a scalar quantity to which the square root may be applied, to produce a quantity of type
/// <typeparamref name="TSquareRootScalarQuantity"/>.</summary>
/// <typeparam name="TSquareRootScalarQuantity">The scalar quantity that is the square root of the original quantity.</typeparam>
public interface ISquareRootableScalarQuantity<out TSquareRootScalarQuantity> :
    IScalarQuantity
    where TSquareRootScalarQuantity : IScalarQuantity
{
    /// <summary>Takes the square root of the quantity, resulting in a <typeparamref name="TSquareRootScalarQuantity"/>.</summary>
    public abstract TSquareRootScalarQuantity SquareRoot();
}