namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public readonly record struct CacheableFixedUnitLocations(MinimalLocation Attribute, MinimalLocation AttributeName, MinimalLocation Name,
    MinimalLocation Plural, MinimalLocation Value, MinimalLocation Bias)
{
    internal static CacheableFixedUnitLocations Construct(FixedUnitLocations originalLocations)
    {
        return new(MinimalLocation.FromLocation(originalLocations.Attribute), MinimalLocation.FromLocation(originalLocations.AttributeName),
            MinimalLocation.FromLocation(originalLocations.Name), MinimalLocation.FromLocation(originalLocations.Plural),
            MinimalLocation.FromLocation(originalLocations.Value), MinimalLocation.FromLocation(originalLocations.Bias));
    }
}