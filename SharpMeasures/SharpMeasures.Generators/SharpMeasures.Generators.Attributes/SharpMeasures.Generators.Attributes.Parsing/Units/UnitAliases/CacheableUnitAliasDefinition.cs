namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public readonly record struct CacheableUnitAliasDefinition(string Name, string Plural, string AliasOf, CacheableUnitAliasLocations Locations)
{
    internal static CacheableUnitAliasDefinition Construct(UnitAliasDefinition originalDefinition)
    {
        return new(originalDefinition.Name, originalDefinition.Plural, originalDefinition.AliasOf, originalDefinition.Locations.ToCacheable());
    }
}