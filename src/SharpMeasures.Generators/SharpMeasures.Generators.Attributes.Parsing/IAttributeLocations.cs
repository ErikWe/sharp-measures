namespace SharpMeasures.Generators.Attributes.Parsing;

public interface IOpenAttributeLocations<out TLocations> : IAttributeLocations where TLocations : IOpenAttributeLocations<TLocations>
{
    public abstract TLocations WithAttribute(MinimalLocation attribute, MinimalLocation attributeName);
}
