namespace SharpMeasures.Generators.Attributes.Parsing;

public abstract record class AAttributeDefinition<TLocations> : IAttributeDefinition<TLocations> where TLocations : IAttributeLocations
{
    public TLocations Locations { get; private init; }

    protected AAttributeDefinition(TLocations locations)
    {
        Locations = locations;
    }
}
