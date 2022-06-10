namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;

internal abstract record class AUnitDefinition<TLocations> : AAttributeDefinition<TLocations>, IUnitDefinition
    where TLocations : AUnitLocations
{
    public string Name { get; }
    public string Plural { get; }

    IUnitLocations IUnitDefinition.Locations => Locations;

    protected AUnitDefinition(string name, string plural, TLocations locations) : base(locations)
    {
        Name = name;
        Plural = plural;
    }
}
