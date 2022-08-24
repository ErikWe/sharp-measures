namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class FixedUnitParser
{
    public static IAttributeParser<RawFixedUnitDefinition> Parser { get; } = new AttributeParser();

    private static RawFixedUnitDefinition DefaultDefinition() => RawFixedUnitDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawFixedUnitDefinition, FixedUnitLocations, FixedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, FixedUnitProperties.AllProperties) { }
    }
}
