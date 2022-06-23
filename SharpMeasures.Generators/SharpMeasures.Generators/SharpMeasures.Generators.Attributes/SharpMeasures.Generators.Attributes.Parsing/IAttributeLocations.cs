namespace SharpMeasures.Generators.Attributes.Parsing;

public interface IAttributeLocations
{
    public abstract MinimalLocation Attribute { get; }
    public abstract MinimalLocation AttributeName { get; }
}
