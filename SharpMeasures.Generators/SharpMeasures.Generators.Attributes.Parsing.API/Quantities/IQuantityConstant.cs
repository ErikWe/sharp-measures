namespace SharpMeasures.Generators.Quantities;

public interface IQuantityConstant
{
    public abstract string Name { get; }
    public abstract string Unit { get; }

    public abstract bool GenerateMultiplesProperty { get; }
    public abstract string? Multiples { get; }
}
