namespace SharpMeasures.Generators.Attributes.Parsing;

public abstract record class AAttributeLocations<TLocations> : IOpenAttributeLocations<TLocations>
    where TLocations : AAttributeLocations<TLocations>
{
    protected abstract TLocations Locations { get; }

    public MinimalLocation Attribute { get; private init; }
    public MinimalLocation AttributeName { get; private init; }

    protected TLocations WithAttribute(MinimalLocation attribute, MinimalLocation attributeName) => Locations with
    {
        Attribute = attribute,
        AttributeName = attributeName
    };

    TLocations IOpenAttributeLocations<TLocations>.WithAttribute(MinimalLocation attribute, MinimalLocation attributeName) => WithAttribute(attribute, attributeName);
}
