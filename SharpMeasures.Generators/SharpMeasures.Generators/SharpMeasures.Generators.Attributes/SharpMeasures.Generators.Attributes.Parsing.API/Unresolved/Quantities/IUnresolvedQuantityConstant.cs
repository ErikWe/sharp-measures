namespace SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedQuantityConstant
{
    public abstract string Name { get; }
    public abstract string Unit { get; }

    public abstract bool GenerateMultiplesProperty { get; }
    public abstract string? Multiples { get; }
}
