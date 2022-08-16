namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;

internal abstract record class AUnitDefinition<TLocations> : AAttributeDefinition<TLocations>, IUnitDefinition<TLocations>
    where TLocations : AUnitLocations<TLocations>
{
    public string Name { get; }
    public string Plural { get; }

    protected AUnitDefinition(string name, string plural, TLocations locations) : base(locations)
    {
        Name = name;
        Plural = plural;
    }
}
