namespace SharpMeasures.Generators.Quantities;

using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

public interface IQuantityConstant
{
    public abstract string Name { get; }
    public abstract IUnresolvedUnitInstance Unit { get; }

    public abstract bool GenerateMultiplesProperty { get; }
    public abstract string? Multiples { get; }
}
