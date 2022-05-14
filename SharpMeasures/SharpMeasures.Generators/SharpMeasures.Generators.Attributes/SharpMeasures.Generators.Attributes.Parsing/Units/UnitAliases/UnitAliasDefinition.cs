namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class UnitAliasDefinition(string Name, string Plural, string AliasOf, UnitAliasLocations Locations)
    : IUnitDefinition, IDependantUnitDefinition
{
    public string DependantOn => AliasOf;

    IUnitLocations IUnitDefinition.Locations => Locations;
    IDependantUnitLocations IDependantUnitDefinition.Locations => Locations;

    public CacheableUnitAliasDefinition ToCacheable() => CacheableUnitAliasDefinition.Construct(this);
}