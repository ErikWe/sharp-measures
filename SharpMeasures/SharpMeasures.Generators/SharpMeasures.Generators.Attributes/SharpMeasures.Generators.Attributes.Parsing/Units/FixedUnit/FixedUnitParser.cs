namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class FixedUnitParser
{
    public static IAttributeParser<FixedUnitDefinition> Parser { get; } = new AttributeParser();

    private static FixedUnitDefinition DefaultDefinition() => FixedUnitDefinition.Empty;

    private class AttributeParser : AUnitParser<FixedUnitDefinition, FixedUnitParsingData, FixedUnitLocations, FixedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, FixedUnitProperties.AllProperties) { }
    }
}
