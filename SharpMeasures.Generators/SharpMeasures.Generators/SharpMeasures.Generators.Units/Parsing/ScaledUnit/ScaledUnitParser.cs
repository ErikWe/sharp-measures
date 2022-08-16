namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class ScaledUnitParser
{
    public static IAttributeParser<UnprocessedScaledUnitDefinition> Parser { get; } = new AttributeParser();

    private static UnprocessedScaledUnitDefinition DefaultDefinition() => UnprocessedScaledUnitDefinition.Empty;

    private class AttributeParser : AAttributeParser<UnprocessedScaledUnitDefinition, ScaledUnitLocations, ScaledUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ScaledUnitProperties.AllProperties) { }
    }
}
