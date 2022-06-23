namespace SharpMeasures.Generators.Attributes.Parsing;

public abstract record class AAttributeLocations : IAttributeLocations
{
    public MinimalLocation Attribute { get; init; }
    public MinimalLocation AttributeName { get; init; }
}
