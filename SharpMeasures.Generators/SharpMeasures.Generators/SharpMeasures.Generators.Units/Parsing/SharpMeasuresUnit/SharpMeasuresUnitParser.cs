namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SharpMeasuresUnitParser
{
    public static IAttributeParser<UnprocessedSharpMeasuresUnitDefinition> Parser { get; } = new AttributeParser();

    private static UnprocessedSharpMeasuresUnitDefinition DefaultDefiniton() => UnprocessedSharpMeasuresUnitDefinition.Empty;

    private class AttributeParser : AAttributeParser<UnprocessedSharpMeasuresUnitDefinition, SharpMeasuresUnitLocations, SharpMeasuresUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SharpMeasuresUnitProperties.AllProperties) { }
    }
}
