namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class PrefixedUnitInstanceParser
{
    public static IAttributeParser<RawPrefixedUnitInstanceDefinition> Parser { get; } = new AttributeParser();

    private static RawPrefixedUnitInstanceDefinition DefaultDefinition() => RawPrefixedUnitInstanceDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawPrefixedUnitInstanceDefinition, PrefixedUnitInstanceLocations, PrefixedUnitInstanceAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, PrefixedUnitInstanceProperties.AllProperties) { }
    }
}
