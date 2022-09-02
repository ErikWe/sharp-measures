namespace SharpMeasures.Generators.Attributes.Parsing;

public abstract record class AAttributeDefinition<TLocations> : IAttributeDefinition<TLocations> where TLocations : IAttributeLocations
{
    public TLocations Locations { get; private init; }

    IAttributeLocations IAttributeDefinition.Locations => Locations;

    protected AAttributeDefinition(TLocations locations)
    {
        Locations = locations;
    }
}
