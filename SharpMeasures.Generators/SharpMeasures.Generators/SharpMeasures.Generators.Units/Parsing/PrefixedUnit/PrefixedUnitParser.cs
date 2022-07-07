namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class PrefixedUnitParser
{
    public static IAttributeParser<RawPrefixedUnitDefinition> Instance { get; } = new AttributeParser();

    private static RawPrefixedUnitDefinition DefaultDefinition() => RawPrefixedUnitDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawPrefixedUnitDefinition, PrefixedUnitLocations, PrefixedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, PrefixedUnitProperties.AllProperties) { }
    }
}
