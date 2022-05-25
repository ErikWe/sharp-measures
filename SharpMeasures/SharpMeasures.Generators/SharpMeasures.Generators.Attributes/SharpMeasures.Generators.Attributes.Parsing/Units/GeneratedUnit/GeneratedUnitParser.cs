namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class GeneratedUnitParser
{
    public static IAttributeParser<GeneratedUnitDefinition> Parser { get; } = new AttributeParser();

    private static GeneratedUnitDefinition DefaultDefiniton() => GeneratedUnitDefinition.Empty;

    private class AttributeParser : AAttributeParser<GeneratedUnitDefinition, GeneratedUnitParsingData, GeneratedUnitLocations, GeneratedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, GeneratedUnitProperties.AllProperties) { }
    }
}
