namespace SharpMeasures.Generators.Quantities;

public interface ISpecializedQuantityType : IQuantityType
{
    new public abstract ISpecializedQuantity Definition { get; }
}
