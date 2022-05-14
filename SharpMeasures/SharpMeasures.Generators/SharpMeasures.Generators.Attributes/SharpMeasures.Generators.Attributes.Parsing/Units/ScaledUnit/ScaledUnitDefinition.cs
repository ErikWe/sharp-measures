namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class ScaledUnitDefinition(string Name, string Plural, string From, double Scale, ScaledUnitLocations Locations)
    : IUnitDefinition, IDependantUnitDefinition
{
    public string DependantOn => From;

    IUnitLocations IUnitDefinition.Locations => Locations;
    IDependantUnitLocations IDependantUnitDefinition.Locations => Locations;

    public CacheableScaledUnitDefinition ToCacheable() => CacheableScaledUnitDefinition.Construct(this);
}