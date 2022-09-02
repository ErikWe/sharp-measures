namespace SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class BiasedUnitInstanceParser
{
    public static IAttributeParser<RawBiasedUnitInstanceDefinition> Parser { get; } = new AttributeParser();

    private static RawBiasedUnitInstanceDefinition DefaultDefinition() => RawBiasedUnitInstanceDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawBiasedUnitInstanceDefinition, BiasedUnitInstanceLocations, BiasedUnitInstanceAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, BiasedUnitInstanceProperties.AllProperties) { }
    }
}
