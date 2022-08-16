namespace SharpMeasures.Generators.Attributes.Parsing;

public abstract record class AUnprocessedAttributeDefinition<TDefinition, TLocations> : IOpenAttributeDefinition<TDefinition, TLocations>
    where TDefinition : AUnprocessedAttributeDefinition<TDefinition, TLocations>
    where TLocations : IAttributeLocations
{
    protected abstract TDefinition Definition { get; }

    public TLocations Locations { get; private init; }

    protected AUnprocessedAttributeDefinition(TLocations locations)
    {
        Locations = locations;
    }

    protected TDefinition WithLocations(TLocations locations) => Definition with { Locations = locations };

    TDefinition IOpenAttributeDefinition<TDefinition, TLocations>.WithLocations(TLocations locations) => WithLocations(locations);
}
