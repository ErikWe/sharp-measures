namespace SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using SharpMeasures.Generators.Units.Utility;
using SharpMeasures.Generators.Units.UnitInstances;

public interface IUnresolvedPrefixedUnit : IUnresolvedDependantUnitInstance
{
    public abstract string From { get; }

    public abstract MetricPrefixName? MetricPrefix { get; }
    public abstract BinaryPrefixName? BinaryPrefix { get; }

    public abstract PrefixType SpecifiedPrefixType { get; }
}
