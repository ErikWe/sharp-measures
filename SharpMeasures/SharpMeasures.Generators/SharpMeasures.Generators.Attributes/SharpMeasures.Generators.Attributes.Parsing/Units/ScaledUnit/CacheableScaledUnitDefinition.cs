namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public readonly record struct CacheableScaledUnitDefinition(string Name, string Plural, string From, double Scale, CacheableScaledUnitLocations Locations)
{
    internal static CacheableScaledUnitDefinition Construct(ScaledUnitDefinition originalDefinition)
    {
        return new(originalDefinition.Name, originalDefinition.Plural, originalDefinition.From, originalDefinition.Scale, originalDefinition.Locations.ToCacheable());
    }
}