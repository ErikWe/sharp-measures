namespace SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedQuantityBase : IUnresolvedQuantity
{
    public abstract NamedType Unit { get; }

    new public abstract NamedType Difference { get; }
}
