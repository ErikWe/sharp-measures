namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal static class PrefixedUnitParser
{
    public static IAttributeParser<RawPrefixedUnitDefinition> Instance { get; } = new AttributeParser();

    private static RawPrefixedUnitDefinition DefaultDefinition() => RawPrefixedUnitDefinition.Empty;

    private class AttributeParser : AUnitParser<RawPrefixedUnitDefinition, PrefixedUnitParsingData, PrefixedUnitLocations, PrefixedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, PrefixedUnitProperties.AllProperties) { }
    }
}
