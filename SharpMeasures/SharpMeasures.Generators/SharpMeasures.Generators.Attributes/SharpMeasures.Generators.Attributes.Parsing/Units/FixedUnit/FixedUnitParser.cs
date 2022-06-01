namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class FixedUnitParser
{
    public static IAttributeParser<RawFixedUnitDefinition> Parser { get; } = new AttributeParser();

    private static RawFixedUnitDefinition DefaultDefinition() => RawFixedUnitDefinition.Empty;

    private class AttributeParser : AUnitParser<RawFixedUnitDefinition, FixedUnitParsingData, FixedUnitLocations, FixedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, FixedUnitProperties.AllProperties) { }
    }
}
