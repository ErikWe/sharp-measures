namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class PrefixedUnitParser
{
    public static IAttributeParser<RawPrefixedUnitDefinition> Parser { get; } = new AttributeParser();

    private static RawPrefixedUnitDefinition DefaultDefinition() => RawPrefixedUnitDefinition.Empty;

    private class AttributeParser : AUnitParser<RawPrefixedUnitDefinition, PrefixedUnitParsingData, PrefixedUnitLocations, PrefixedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, PrefixedUnitProperties.AllProperties) { }
    }
}
