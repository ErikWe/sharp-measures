namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.UnitInstances;
using SharpMeasures.Generators.Units.Utility;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

internal record class PrefixedUnitDefinition : ADependantUnitDefinition<PrefixedUnitLocations>, IPrefixedUnit
{
    public IUnresolvedUnitInstance From => DependantOn;

    public MetricPrefixName? MetricPrefix { get; }
    public BinaryPrefixName? BinaryPrefix { get; }

    public PrefixType SpecifiedPrefixType => MetricPrefix is null ? PrefixType.Binary : PrefixType.Metric;

    public PrefixedUnitDefinition(string name, string plural, IUnresolvedUnitInstance from, MetricPrefixName metricPrefix, PrefixedUnitLocations locations)
        : base(name, plural, from, locations)
    {
        MetricPrefix = metricPrefix;
    }

    public PrefixedUnitDefinition(string name, string plural, IUnresolvedUnitInstance from, BinaryPrefixName binaryPrefix, PrefixedUnitLocations locations)
        : base(name, plural, from, locations)
    {
        BinaryPrefix = binaryPrefix;
    }
}
