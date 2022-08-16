namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class FixedUnitParser
{
    public static IAttributeParser<UnprocessedFixedUnitDefinition> Parser { get; } = new AttributeParser();

    private static UnprocessedFixedUnitDefinition DefaultDefinition() => UnprocessedFixedUnitDefinition.Empty;

    private class AttributeParser : AAttributeParser<UnprocessedFixedUnitDefinition, FixedUnitLocations, FixedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, FixedUnitProperties.AllProperties) { }
    }
}
