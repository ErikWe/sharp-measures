namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class DerivableUnitParser
{
    public static IAttributeParser<UnprocessedDerivableUnitDefinition> Parser { get; } = new AttributeParser();

    private static UnprocessedDerivableUnitDefinition DefaultDefinition() => UnprocessedDerivableUnitDefinition.Empty;

    private class AttributeParser : AAttributeParser<UnprocessedDerivableUnitDefinition, DerivableUnitLocations, DerivableUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DerivableUnitProperties.AllProperties) { }
    }
}
