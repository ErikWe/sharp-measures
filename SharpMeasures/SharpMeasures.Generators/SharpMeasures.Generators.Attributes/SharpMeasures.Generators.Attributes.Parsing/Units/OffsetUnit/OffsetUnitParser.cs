namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class OffsetUnitParser
{
    public static IAttributeParser<RawOffsetUnitDefinition> Parser { get; } = new AttributeParser();

    private static RawOffsetUnitDefinition DefaultDefinition() => RawOffsetUnitDefinition.Empty;

    private class AttributeParser : AUnitParser<RawOffsetUnitDefinition, OffsetUnitParsingData, OffsetUnitLocations, OffsetUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, OffsetUnitProperties.AllProperties) { }
    }
}
