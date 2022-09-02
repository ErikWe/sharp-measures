namespace SharpMeasures.Generators;

public interface IAttributeDefinition
{
    public abstract IAttributeLocations Locations { get; }
}

public interface IAttributeLocations
{
    public abstract MinimalLocation Attribute { get; }
    public abstract MinimalLocation AttributeName { get; }
}
