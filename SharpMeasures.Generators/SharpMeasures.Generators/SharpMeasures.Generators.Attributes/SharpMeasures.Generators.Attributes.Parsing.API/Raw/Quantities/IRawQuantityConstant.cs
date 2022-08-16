namespace SharpMeasures.Generators.Raw.Quantities;

public interface IRawQuantityConstant
{
    public abstract string Name { get; }
    public abstract string Unit { get; }

    public abstract bool GenerateMultiplesProperty { get; }
    public abstract string? Multiples { get; }
}
