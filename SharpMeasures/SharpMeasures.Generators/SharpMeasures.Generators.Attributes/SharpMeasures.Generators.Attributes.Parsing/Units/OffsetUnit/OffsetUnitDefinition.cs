namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class OffsetUnitDefinition(string Name, string Plural, string From, double Offset, OffsetUnitLocations Locations)
    : IUnitDefinition, IDependantUnitDefinition
{
    public string DependantOn => From;

    IUnitLocations IUnitDefinition.Locations => Locations;
    IDependantUnitLocations IDependantUnitDefinition.Locations => Locations;

    public CacheableOffsetUnitDefinition ToCacheable() => CacheableOffsetUnitDefinition.Construct(this);
}