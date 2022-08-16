namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class BiasedUnitParser
{
    public static IAttributeParser<UnprocessedBiasedUnitDefinition> Parser { get; } = new AttributeParser();

    private static UnprocessedBiasedUnitDefinition DefaultDefinition() => UnprocessedBiasedUnitDefinition.Empty;

    private class AttributeParser : AAttributeParser<UnprocessedBiasedUnitDefinition, BiasedUnitLocations, BiasedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, BiasedUnitProperties.AllProperties) { }
    }
}
