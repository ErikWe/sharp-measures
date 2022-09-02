namespace SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class FixedUnitInstanceParser
{
    public static IAttributeParser<RawFixedUnitInstanceDefinition> Parser { get; } = new AttributeParser();

    private static RawFixedUnitInstanceDefinition DefaultDefinition() => RawFixedUnitInstanceDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawFixedUnitInstanceDefinition, FixedUnitInstanceLocations, FixedUnitInstanceAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, FixedUnitInstanceProperties.AllProperties) { }
    }
}
