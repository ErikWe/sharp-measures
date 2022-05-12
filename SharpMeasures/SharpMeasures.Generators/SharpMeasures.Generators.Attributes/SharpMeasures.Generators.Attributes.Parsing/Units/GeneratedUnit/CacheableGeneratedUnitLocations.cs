namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public readonly record struct CacheableGeneratedUnitLocations(MinimalLocation Attribute, MinimalLocation AttributeName, MinimalLocation Quantity,
    MinimalLocation AllowBias, MinimalLocation GenerateDocumentation)
{
    internal static CacheableGeneratedUnitLocations Construct(GeneratedUnitLocations originalLocations)
    {
        return new(MinimalLocation.FromLocation(originalLocations.Attribute), MinimalLocation.FromLocation(originalLocations.AttributeName),
            MinimalLocation.FromLocation(originalLocations.Quantity), MinimalLocation.FromLocation(originalLocations.AllowBias),
            MinimalLocation.FromLocation(originalLocations.GenerateDocumentation));
    }
}