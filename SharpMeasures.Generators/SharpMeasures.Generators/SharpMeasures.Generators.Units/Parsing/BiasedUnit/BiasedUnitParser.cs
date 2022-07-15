namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class BiasedUnitParser
{
    public static IAttributeParser<RawBiasedUnitDefinition> Parser { get; } = new AttributeParser();

    private static RawBiasedUnitDefinition DefaultDefinition() => RawBiasedUnitDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawBiasedUnitDefinition, BiasedUnitLocations, BiasedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, BiasedUnitProperties.AllProperties) { }
    }
}
