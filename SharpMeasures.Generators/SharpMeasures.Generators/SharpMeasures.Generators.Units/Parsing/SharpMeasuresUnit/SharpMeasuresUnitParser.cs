namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SharpMeasuresUnitParser
{
    public static IAttributeParser<RawSharpMeasuresUnitDefinition> Instance { get; } = new AttributeParser();

    private static RawSharpMeasuresUnitDefinition DefaultDefiniton() => RawSharpMeasuresUnitDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawSharpMeasuresUnitDefinition, SharpMeasuresUnitLocations, SharpMeasuresUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SharpMeasuresUnitProperties.AllProperties) { }
    }
}
