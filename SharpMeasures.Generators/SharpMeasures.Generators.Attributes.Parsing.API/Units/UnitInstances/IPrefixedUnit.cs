namespace SharpMeasures.Generators.Units.UnitInstances;

using SharpMeasures.Generators.Units.Utility;

public interface IPrefixedUnit : IDependantUnitInstance
{
    public abstract string From { get; }

    public abstract MetricPrefixName? MetricPrefix { get; }
    public abstract BinaryPrefixName? BinaryPrefix { get; }

    public abstract PrefixType SpecifiedPrefixType { get; }
}
