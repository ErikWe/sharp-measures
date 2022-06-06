namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class PrefixedUnitParser
{
    public static IAttributeParser<RawPrefixedUnit> Parser { get; } = new AttributeParser();

    private static RawPrefixedUnit DefaultDefinition() => RawPrefixedUnit.Empty;

    private class AttributeParser : AUnitParser<RawPrefixedUnit, PrefixedUnitParsingData, PrefixedUnitLocations, PrefixedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, PrefixedUnitProperties.AllProperties) { }
    }
}
