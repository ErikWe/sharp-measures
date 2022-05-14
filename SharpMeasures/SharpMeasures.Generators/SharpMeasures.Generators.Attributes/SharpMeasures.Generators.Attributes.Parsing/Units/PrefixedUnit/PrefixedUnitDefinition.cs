namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public record class PrefixedUnitDefinition(string Name, string Plural, string From, MetricPrefixName MetricPrefixName,
    BinaryPrefixName BinaryPrefixName, PrefixedUnitLocations Locations, PrefixedUnitParsingData ParsingData)
    : IUnitDefinition, IDependantUnitDefinition
{
    public string DependantOn => From;

    IUnitLocations IUnitDefinition.Locations => Locations;
    IDependantUnitLocations IDependantUnitDefinition.Locations => Locations;

    public CacheablePrefixedUnitDefinition ToCacheable() => CacheablePrefixedUnitDefinition.Construct(this);

    internal PrefixedUnitDefinition ParseMetricPrefix(MetricPrefixName metricPrefixName)
    {
        return this with
        {
            MetricPrefixName = metricPrefixName,
            ParsingData = ParsingData with { SpecifiedPrefixType = PrefixedUnitParsingData.PrefixType.Metric }
        };
    }

    internal PrefixedUnitDefinition ParseBinaryPrefix(BinaryPrefixName binaryPrefixName)
    {
        return this with
        {
            BinaryPrefixName = binaryPrefixName,
            ParsingData = ParsingData with { SpecifiedPrefixType = PrefixedUnitParsingData.PrefixType.Binary }
        };
    }
}