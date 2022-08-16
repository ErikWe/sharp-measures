namespace SharpMeasures.Generators.Units.UnitInstances;

using SharpMeasures.Generators.Units.Utility;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

public interface IPrefixedUnit : IDependantUnitInstance
{
    public abstract IRawUnitInstance From { get; }

    public abstract MetricPrefixName? MetricPrefix { get; }
    public abstract BinaryPrefixName? BinaryPrefix { get; }

    public abstract PrefixType SpecifiedPrefixType { get; }
}
