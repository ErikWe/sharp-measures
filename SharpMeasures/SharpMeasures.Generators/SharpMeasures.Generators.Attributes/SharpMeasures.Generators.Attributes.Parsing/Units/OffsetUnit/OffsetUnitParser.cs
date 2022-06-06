namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class OffsetUnitParser
{
    public static IAttributeParser<RawOffsetUnit> Parser { get; } = new AttributeParser();

    private static RawOffsetUnit DefaultDefinition() => RawOffsetUnit.Empty;

    private class AttributeParser : AUnitParser<RawOffsetUnit, OffsetUnitParsingData, OffsetUnitLocations, OffsetUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, OffsetUnitProperties.AllProperties) { }
    }
}
