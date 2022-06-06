namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class ScaledUnitParser
{
    public static IAttributeParser<RawScaledUnit> Parser { get; } = new AttributeParser();

    private static RawScaledUnit DefaultDefinition() => RawScaledUnit.Empty;

    private class AttributeParser : AUnitParser<RawScaledUnit, ScaledUnitParsingData, ScaledUnitLocations, ScaledUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ScaledUnitProperties.AllProperties) { }
    }
}
