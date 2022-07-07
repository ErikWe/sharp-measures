namespace SharpMeasures.Generators.Unresolved.Scalars;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units.UnitInstances;

public interface IUnresolvedScalarConstant
{
    public abstract IScalarConstant UnresolvedTarget { get; }

    public abstract string Name { get; }
    public abstract string Unit { get; }
    public abstract double Value { get; }

    public abstract bool GenerateMultiplesProperty { get; }
    public abstract string? Multiples { get; }
}
