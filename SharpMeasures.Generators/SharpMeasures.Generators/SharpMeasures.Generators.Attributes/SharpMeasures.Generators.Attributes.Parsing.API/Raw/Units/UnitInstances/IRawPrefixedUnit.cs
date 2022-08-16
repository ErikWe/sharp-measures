namespace SharpMeasures.Generators.Raw.Units.UnitInstances;

using SharpMeasures.Generators.Units.Utility;
using SharpMeasures.Generators.Units.UnitInstances;

public interface IRawPrefixedUnit : IRawDependantUnitInstance
{
    public abstract string From { get; }

    public abstract MetricPrefixName? MetricPrefix { get; }
    public abstract BinaryPrefixName? BinaryPrefix { get; }

    public abstract PrefixType SpecifiedPrefixType { get; }
}
