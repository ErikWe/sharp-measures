namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.UnitInstances;
using SharpMeasures.Generators.Units.Utility;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

internal record class PrefixedUnitDefinition : ADependantUnitDefinition<PrefixedUnitLocations>, IPrefixedUnit
{
    public IRawUnitInstance From => DependantOn;

    public MetricPrefixName? MetricPrefix { get; }
    public BinaryPrefixName? BinaryPrefix { get; }

    public PrefixType SpecifiedPrefixType => MetricPrefix is null ? PrefixType.Binary : PrefixType.Metric;

    public PrefixedUnitDefinition(string name, string plural, IRawUnitInstance from, MetricPrefixName metricPrefix, PrefixedUnitLocations locations)
        : base(name, plural, from, locations)
    {
        MetricPrefix = metricPrefix;
    }

    public PrefixedUnitDefinition(string name, string plural, IRawUnitInstance from, BinaryPrefixName binaryPrefix, PrefixedUnitLocations locations)
        : base(name, plural, from, locations)
    {
        BinaryPrefix = binaryPrefix;
    }
}
