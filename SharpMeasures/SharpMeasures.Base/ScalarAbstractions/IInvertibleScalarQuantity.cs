namespace SharpMeasures.ScalarAbstractions;

/// <summary>Describes a scalar quantity that may be inverted, to produce a quantity of type
/// <typeparamref name="TInverseScalarQuantity"/>.</summary>
/// <typeparam name="TInverseScalarQuantity">The scalar quantity that is the inverse of the original quantity.</typeparam>
public interface IInvertibleScalarQuantity<out TInverseScalarQuantity> :
    IScalarQuantity
    where TInverseScalarQuantity : IScalarQuantity
{
    /// <summary>Inverts the quantity, resulting in a <typeparamref name="TInverseScalarQuantity"/>.</summary>
    public abstract TInverseScalarQuantity Invert();
}