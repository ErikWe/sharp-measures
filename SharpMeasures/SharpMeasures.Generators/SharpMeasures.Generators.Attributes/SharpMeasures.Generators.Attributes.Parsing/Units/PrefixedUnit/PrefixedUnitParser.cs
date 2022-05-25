namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class PrefixedUnitParser
{
    public static IAttributeParser<PrefixedUnitDefinition> Parser { get; } = new AttributeParser();

    private static PrefixedUnitDefinition DefaultDefinition() => PrefixedUnitDefinition.Empty;

    private class AttributeParser : AUnitParser<PrefixedUnitDefinition, PrefixedUnitParsingData, PrefixedUnitLocations, PrefixedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, PrefixedUnitProperties.AllProperties) { }
    }
}
