namespace SharpMeasures.ScalarAbstractions;

/// <summary>Describes a scalar quantity that may be multiplied by a quantity of type <typeparamref name="TFactorScalarQuantity"/>
/// to produce a quantity of type <typeparamref name="TProductScalarQuantity"/>.</summary>
/// <typeparam name="TProductScalarQuantity">The scalar quantity that is the product of the two quantities.</typeparam>
/// <typeparam name="TFactorScalarQuantity">The scalar quantity by which the original quantity may be multiplied.</typeparam>
public interface IMultiplicableScalarQuantity<out TProductScalarQuantity, in TFactorScalarQuantity> :
    IScalarQuantity
    where TProductScalarQuantity : IScalarQuantity
    where TFactorScalarQuantity : IScalarQuantity
{
    /// <summary>Multiplies the original quantity by the quantity <paramref name="factor"/>, resulting in a <typeparamref name="TProductScalarQuantity"/>.</summary>
    /// <param name="factor">The quantity by which the original quantity is multiplied.</param>
    public abstract TProductScalarQuantity Multiply(TFactorScalarQuantity factor);
}