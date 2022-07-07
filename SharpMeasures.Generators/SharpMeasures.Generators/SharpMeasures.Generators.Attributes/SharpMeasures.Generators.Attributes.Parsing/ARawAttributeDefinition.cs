namespace SharpMeasures.Generators.Attributes.Parsing;

public abstract record class ARawAttributeDefinition<TDefinition, TLocations> : IOpenAttributeDefinition<TDefinition, TLocations>
    where TDefinition : ARawAttributeDefinition<TDefinition, TLocations>
    where TLocations : IAttributeLocations
{
    protected abstract TDefinition Definition { get; }

    public TLocations Locations { get; private init; }

    protected ARawAttributeDefinition(TLocations locations)
    {
        Locations = locations;
    }

    protected TDefinition WithLocations(TLocations locations) => Definition with { Locations = locations };

    TDefinition IOpenAttributeDefinition<TDefinition, TLocations>.WithLocations(TLocations locations) => WithLocations(locations);
}
