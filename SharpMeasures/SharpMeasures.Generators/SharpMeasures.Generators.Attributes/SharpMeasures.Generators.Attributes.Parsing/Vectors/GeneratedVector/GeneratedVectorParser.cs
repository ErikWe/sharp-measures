namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpMeasures.Generators.Vectors;

public static class GeneratedUnitParser
{
    public static IAttributeParser<GeneratedVectorDefinition> Parser { get; } = new AttributeParser();

    private static GeneratedVectorDefinition DefaultDefiniton() => GeneratedVectorDefinition.Empty;

    private class AttributeParser : AAttributeParser<GeneratedVectorDefinition, GeneratedVectorParsingData, GeneratedVectorLocations, GeneratedVectorAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, GeneratedVectorProperties.AllProperties) { }
    }
}
