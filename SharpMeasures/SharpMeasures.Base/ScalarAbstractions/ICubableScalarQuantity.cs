namespace SharpMeasures.ScalarAbstractions;

/// <summary>Describes a scalar quantity that may be cubed, to produce a quantity of type
/// <typeparamref name="TCubeScalarQuantity"/>.</summary>
/// <typeparam name="TCubeScalarQuantity">The scalar quantity that is the cube of the original quantity.</typeparam>
public interface ICubableScalarQuantity<out TCubeScalarQuantity> :
    IScalarQuantity
    where TCubeScalarQuantity : IScalarQuantity
{
    /// <summary>Cubes the quantity, resulting in a <typeparamref name="TCubeScalarQuantity"/>.</summary>
    public abstract TCubeScalarQuantity Cube();
}