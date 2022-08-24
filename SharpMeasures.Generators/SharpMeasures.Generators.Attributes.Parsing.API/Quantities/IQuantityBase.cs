namespace SharpMeasures.Generators.Quantities;

public interface IQuantityBase : IQuantity
{
    public abstract NamedType Unit { get; }

    new public abstract bool ImplementSum { get; }
    new public abstract bool ImplementDifference { get; }
}
