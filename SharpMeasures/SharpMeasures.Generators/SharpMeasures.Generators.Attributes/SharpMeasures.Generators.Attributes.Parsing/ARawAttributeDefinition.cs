namespace SharpMeasures.Generators.Attributes.Parsing;

public abstract record class ARawAttributeDefinition<TLocations> : IAttributeDefinition
    where TLocations : IAttributeLocations
{
    public TLocations Locations { get; init; }
    IAttributeLocations IAttributeDefinition.Locations => Locations;

    protected ARawAttributeDefinition(TLocations locations)
    {
        Locations = locations;
    }
}
