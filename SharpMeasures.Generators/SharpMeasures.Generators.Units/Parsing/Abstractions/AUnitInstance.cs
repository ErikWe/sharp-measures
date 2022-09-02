namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;

internal abstract record class AUnitInstance<TLocations> : AAttributeDefinition<TLocations>, IUnitInstance
    where TLocations : IUnitInstanceLocations
{
    public string Name { get; }
    public string PluralForm { get; }

    IUnitInstanceLocations IUnitInstance.Locations => Locations;

    protected AUnitInstance(string name, string pluralForm, TLocations locations) : base(locations)
    {
        Name = name;
        PluralForm = pluralForm;
    }
}
