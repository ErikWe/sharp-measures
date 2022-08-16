namespace SharpMeasures.Generators.Raw.Quantities;

public interface IRawQuantityBase : IRawQuantity
{
    public abstract NamedType Unit { get; }

    new public abstract NamedType Difference { get; }
}
