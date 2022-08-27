namespace SharpMeasures.Generators.Attributes.Parsing;

public interface IAttributeLocations
{
    public abstract MinimalLocation Attribute { get; }
    public abstract MinimalLocation AttributeName { get; }
}

public interface IOpenAttributeLocations<out TLocations> : IAttributeLocations
    where TLocations : IOpenAttributeLocations<TLocations>
{
    public abstract TLocations WithAttribute(MinimalLocation attribute, MinimalLocation attributeName);
}
