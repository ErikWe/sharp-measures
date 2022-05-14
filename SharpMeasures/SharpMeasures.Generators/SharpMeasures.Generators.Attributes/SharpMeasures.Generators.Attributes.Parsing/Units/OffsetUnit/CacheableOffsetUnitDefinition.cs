namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public readonly record struct CacheableOffsetUnitDefinition(string Name, string Plural, string From, double Offset, CacheableOffsetUnitLocations Locations)
{
    internal static CacheableOffsetUnitDefinition Construct(OffsetUnitDefinition originalDefinition)
    {
        return new(originalDefinition.Name, originalDefinition.Plural, originalDefinition.From, originalDefinition.Offset, originalDefinition.Locations.ToCacheable());
    }
}