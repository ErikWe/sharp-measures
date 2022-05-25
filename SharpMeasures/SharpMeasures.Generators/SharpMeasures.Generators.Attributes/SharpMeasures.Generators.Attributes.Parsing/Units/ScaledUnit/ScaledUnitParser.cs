namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class ScaledUnitParser
{
    public static IAttributeParser<ScaledUnitDefinition> Parser { get; } = new AttributeParser();

    private static ScaledUnitDefinition DefaultDefinition() => ScaledUnitDefinition.Empty;

    private class AttributeParser : AUnitParser<ScaledUnitDefinition, ScaledUnitParsingData, ScaledUnitLocations, ScaledUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ScaledUnitProperties.AllProperties) { }
    }
}
