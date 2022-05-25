namespace SharpMeasures.Generators.Attributes.Parsing;

public interface IAttributeDefinition
{
    public abstract IAttributeParsingData ParsingData { get; }
}