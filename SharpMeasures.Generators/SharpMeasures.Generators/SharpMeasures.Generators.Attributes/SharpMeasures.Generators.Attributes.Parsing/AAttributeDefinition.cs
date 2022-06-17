namespace SharpMeasures.Generators.Attributes.Parsing;

public abstract record class AAttributeDefinition<TLocations> : IAttributeDefinition
    where TLocations : IAttributeLocations
{
    public TLocations Locations { get; }
    IAttributeLocations IAttributeDefinition.Locations => Locations;

    protected AAttributeDefinition(TLocations locations)
    {
        Locations = locations;
    }
}
