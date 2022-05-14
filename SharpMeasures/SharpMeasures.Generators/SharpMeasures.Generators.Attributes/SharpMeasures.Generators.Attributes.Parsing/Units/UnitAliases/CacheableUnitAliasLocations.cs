namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public readonly record struct CacheableUnitAliasLocations(MinimalLocation Attribute, MinimalLocation AttributeName, MinimalLocation Name,
    MinimalLocation Plural, MinimalLocation AliasOf)
{
    internal static CacheableUnitAliasLocations Construct(UnitAliasLocations originalLocations)
    {
        return new(MinimalLocation.FromLocation(originalLocations.Attribute), MinimalLocation.FromLocation(originalLocations.AttributeName),
            MinimalLocation.FromLocation(originalLocations.Name), MinimalLocation.FromLocation(originalLocations.Plural),
            MinimalLocation.FromLocation(originalLocations.AliasOf));
    }
}