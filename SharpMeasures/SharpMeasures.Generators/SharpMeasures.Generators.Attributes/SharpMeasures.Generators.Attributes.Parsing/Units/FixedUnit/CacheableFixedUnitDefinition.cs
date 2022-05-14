namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public readonly record struct CacheableFixedUnitDefinition(string Name, string Plural, double Value, double Bias, CacheableFixedUnitLocations Locations)
{
    internal static CacheableFixedUnitDefinition Construct(FixedUnitDefinition originalDefinition)
    {
        return new(originalDefinition.Name, originalDefinition.Plural, originalDefinition.Value, originalDefinition.Bias, originalDefinition.Locations.ToCacheable());
    }
}