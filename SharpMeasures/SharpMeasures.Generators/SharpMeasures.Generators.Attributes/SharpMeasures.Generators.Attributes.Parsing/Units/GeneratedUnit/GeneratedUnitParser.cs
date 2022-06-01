namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class GeneratedUnitParser
{
    public static IAttributeParser<RawGeneratedUnitDefinition> Parser { get; } = new AttributeParser();

    private static RawGeneratedUnitDefinition DefaultDefiniton() => RawGeneratedUnitDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawGeneratedUnitDefinition, GeneratedUnitLocations, GeneratedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, GeneratedUnitProperties.AllProperties) { }
    }
}
