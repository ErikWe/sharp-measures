namespace SharpMeasures.ScalarAbstractions;

/// <summary>Describes a scalar quantity to which the cube root may be applied, to produce a quantity of type
/// <typeparamref name="TCubeRootScalarQuantity"/>.</summary>
/// <typeparam name="TCubeRootScalarQuantity">The scalar quantity that is the cube root of the original quantity.</typeparam>
public interface ICubeRootableScalarQuantity<out TCubeRootScalarQuantity> :
    IScalarQuantity
    where TCubeRootScalarQuantity : IScalarQuantity
{
    /// <summary>Takes the cube root of the quantity, resulting in a <typeparamref name="TCubeRootScalarQuantity"/>.</summary>
    public abstract TCubeRootScalarQuantity CubeRoot();
}