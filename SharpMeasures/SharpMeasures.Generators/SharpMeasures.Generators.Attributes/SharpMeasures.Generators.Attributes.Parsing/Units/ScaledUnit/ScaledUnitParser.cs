namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class ScaledUnitParser
{
    public static IAttributeParser<RawScaledUnitDefinition> Parser { get; } = new AttributeParser();

    private static RawScaledUnitDefinition DefaultDefinition() => RawScaledUnitDefinition.Empty;

    private class AttributeParser : AUnitParser<RawScaledUnitDefinition, ScaledUnitParsingData, ScaledUnitLocations, ScaledUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ScaledUnitProperties.AllProperties) { }
    }
}
