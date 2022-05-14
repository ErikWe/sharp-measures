namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public readonly record struct CacheablePrefixedUnitDefinition(string Name, string Plural, string From, MetricPrefixName MetricPrefixName,
    BinaryPrefixName BinaryPrefixName, CacheablePrefixedUnitLocations Locations, PrefixedUnitParsingData ParsingData)
{
    internal static CacheablePrefixedUnitDefinition Construct(PrefixedUnitDefinition originalDefinition)
    {
        return new(originalDefinition.Name, originalDefinition.Plural, originalDefinition.From, originalDefinition.MetricPrefixName,
            originalDefinition.BinaryPrefixName, originalDefinition.Locations.ToCacheable(), originalDefinition.ParsingData);
    }
}