namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Units.UnitInstances;

public interface IScalarConstant
{
    public abstract string Name { get; }
    public abstract IUnitInstance Unit { get; }
    public abstract double Value { get; }

    public abstract bool GenerateMultiplesProperty { get; }
    public abstract string? Multiples { get; }
}
