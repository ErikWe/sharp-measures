namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpMeasures.Generators.Vectors;

public static class GeneratedUnitParser
{
    public static IAttributeParser<RawGeneratedVectorDefinition> Parser { get; } = new AttributeParser();

    private static RawGeneratedVectorDefinition DefaultDefiniton() => RawGeneratedVectorDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawGeneratedVectorDefinition, GeneratedVectorLocations, GeneratedVectorAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, GeneratedVectorProperties.AllProperties) { }
    }
}
