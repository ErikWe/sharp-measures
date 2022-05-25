namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class DerivableUnitParser
{
    public static IAttributeParser<DerivableUnitDefinition> Parser { get; } = new AttributeParser();

    private static DerivableUnitDefinition DefaultDefinition() => DerivableUnitDefinition.Empty;

    private class AttributeParser : AAttributeParser<DerivableUnitDefinition, DerivableUnitParsingData, DerivableUnitLocations, DerivableUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DerivableUnitProperties.AllProperties) { }
    }
}
