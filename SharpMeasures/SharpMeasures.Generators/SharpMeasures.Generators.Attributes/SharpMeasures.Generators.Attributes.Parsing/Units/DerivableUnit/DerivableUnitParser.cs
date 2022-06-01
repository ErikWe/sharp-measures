namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class DerivableUnitParser
{
    public static IAttributeParser<RawDerivableUnitDefinition> Parser { get; } = new AttributeParser();

    private static RawDerivableUnitDefinition DefaultDefinition() => RawDerivableUnitDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawDerivableUnitDefinition, DerivableUnitLocations, DerivableUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DerivableUnitProperties.AllProperties) { }
    }
}
