namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public record class PrefixedUnitDefinition : ADependantUnitDefinition<PrefixedUnitLocations>
{
    public enum PrefixType { Metric, Binary }

    public string From => DependantOn;

    public MetricPrefixName MetricPrefixName { get; }
    public BinaryPrefixName BinaryPrefixName { get; }

    public PrefixType SpecifiedPrefixType { get; }

    public PrefixedUnitDefinition(string name, string plural, string from, MetricPrefixName metricPrefixName, PrefixedUnitLocations locations)
        : base(name, plural, from, locations)
    {
        MetricPrefixName = metricPrefixName;
        SpecifiedPrefixType = PrefixType.Metric;
    }

    public PrefixedUnitDefinition(string name, string plural, string from, BinaryPrefixName binaryPrefixName, PrefixedUnitLocations locations)
        : base(name, plural, from, locations)
    {
        BinaryPrefixName = binaryPrefixName;
        SpecifiedPrefixType = PrefixType.Binary;
    }
}