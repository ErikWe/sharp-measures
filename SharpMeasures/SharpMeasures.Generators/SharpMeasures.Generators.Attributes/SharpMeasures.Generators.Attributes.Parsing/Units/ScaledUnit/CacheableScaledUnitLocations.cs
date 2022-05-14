﻿namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public readonly record struct CacheableScaledUnitLocations(MinimalLocation Attribute, MinimalLocation AttributeName, MinimalLocation Name,
    MinimalLocation Plural, MinimalLocation From, MinimalLocation Offset)
{
    internal static CacheableScaledUnitLocations Construct(ScaledUnitLocations originalLocations)
    {
        return new(MinimalLocation.FromLocation(originalLocations.Attribute), MinimalLocation.FromLocation(originalLocations.AttributeName),
            MinimalLocation.FromLocation(originalLocations.Name), MinimalLocation.FromLocation(originalLocations.Plural),
            MinimalLocation.FromLocation(originalLocations.From), MinimalLocation.FromLocation(originalLocations.Scale));
    }
}