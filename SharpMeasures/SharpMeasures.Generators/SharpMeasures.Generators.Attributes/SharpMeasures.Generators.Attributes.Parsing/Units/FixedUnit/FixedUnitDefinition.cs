namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class FixedUnitDefinition(string Name, string Plural, double Value, double Bias, FixedUnitLocations Locations)
    : IUnitDefinition
{
    IUnitLocations IUnitDefinition.Locations => Locations;

    public CacheableFixedUnitDefinition ToCacheable() => CacheableFixedUnitDefinition.Construct(this);
}