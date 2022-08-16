namespace SharpMeasures.Generators.Quantities;

using SharpMeasures.Generators.Raw.Units.UnitInstances;

public interface IQuantityConstant
{
    public abstract string Name { get; }
    public abstract IRawUnitInstance Unit { get; }

    public abstract bool GenerateMultiplesProperty { get; }
    public abstract string? Multiples { get; }
}
