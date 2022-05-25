namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class OffsetUnitParser
{
    public static IAttributeParser<OffsetUnitDefinition> Parser { get; } = new AttributeParser();

    private static OffsetUnitDefinition DefaultDefinition() => OffsetUnitDefinition.Empty;

    private class AttributeParser : AUnitParser<OffsetUnitDefinition, OffsetUnitParsingData, OffsetUnitLocations, OffsetUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, OffsetUnitProperties.AllProperties) { }
    }
}
