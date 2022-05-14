namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public readonly record struct CacheablePrefixedUnitLocations(MinimalLocation Attribute, MinimalLocation AttributeName, MinimalLocation Name,
    MinimalLocation Plural, MinimalLocation From, MinimalLocation MetricPrefixName, MinimalLocation BinaryPrefixName)
{
    internal static CacheablePrefixedUnitLocations Construct(PrefixedUnitLocations originalLocations)
    {
        return new(MinimalLocation.FromLocation(originalLocations.Attribute), MinimalLocation.FromLocation(originalLocations.AttributeName),
            MinimalLocation.FromLocation(originalLocations.Name), MinimalLocation.FromLocation(originalLocations.Plural),
            MinimalLocation.FromLocation(originalLocations.From), MinimalLocation.FromLocation(originalLocations.MetricPrefixName),
            MinimalLocation.FromLocation(originalLocations.BinaryPrefixName));
    }
}