namespace SharpMeasures.Generators.Units.UnitInstances;

using SharpMeasures.Generators.Units.Utility;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

public interface IPrefixedUnit : IDependantUnitInstance
{
    public abstract IUnresolvedUnitInstance From { get; }

    public abstract MetricPrefixName? MetricPrefix { get; }
    public abstract BinaryPrefixName? BinaryPrefix { get; }

    public abstract PrefixType SpecifiedPrefixType { get; }
}
