namespace SharpMeasures.ScalarAbstractions;

/// <summary>Describes a scalar quantity that may be squared, to produce a quantity of type
/// <typeparamref name="TSquareScalarQuantity"/>.</summary>
/// <typeparam name="TSquareScalarQuantity">The scalar quantity that is the square of the original quantity.</typeparam>
public interface ISquarableScalarQuantity<out TSquareScalarQuantity> :
    IScalarQuantity
    where TSquareScalarQuantity : IScalarQuantity
{
    /// <summary>Squares the quantity, resulting in a <typeparamref name="TSquareScalarQuantity"/>.</summary>
    public abstract TSquareScalarQuantity Square();
}