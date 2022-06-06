namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class FixedUnitParser
{
    public static IAttributeParser<RawFixedUnit> Parser { get; } = new AttributeParser();

    private static RawFixedUnit DefaultDefinition() => RawFixedUnit.Empty;

    private class AttributeParser : AUnitParser<RawFixedUnit, FixedUnitParsingData, FixedUnitLocations, FixedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, FixedUnitProperties.AllProperties) { }
    }
}
