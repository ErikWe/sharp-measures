namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class PrefixedUnitParser
{
    public static IAttributeParser<UnprocessedPrefixedUnitDefinition> Parser { get; } = new AttributeParser();

    private static UnprocessedPrefixedUnitDefinition DefaultDefinition() => UnprocessedPrefixedUnitDefinition.Empty;

    private class AttributeParser : AAttributeParser<UnprocessedPrefixedUnitDefinition, PrefixedUnitLocations, PrefixedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, PrefixedUnitProperties.AllProperties) { }
    }
}
