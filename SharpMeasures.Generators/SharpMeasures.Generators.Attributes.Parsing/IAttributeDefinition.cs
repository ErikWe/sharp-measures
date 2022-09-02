namespace SharpMeasures.Generators.Attributes.Parsing;

public interface IAttributeDefinition<out TLocations> : IAttributeDefinition
    where TLocations : IAttributeLocations
{
    new public abstract TLocations Locations { get; }
}

public interface IOpenAttributeDefinition<out TDefinition, TLocations> : IAttributeDefinition<TLocations>
    where TDefinition : IOpenAttributeDefinition<TDefinition, TLocations>
    where TLocations : IAttributeLocations
{
    public abstract TDefinition WithLocations(TLocations locations);
}
