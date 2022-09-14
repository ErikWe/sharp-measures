namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SharpMeasuresUnitParser
{
    public static IAttributeParser<SymbolicSharpMeasuresUnitDefinition> Parser { get; } = new AttributeParser();

    private static SymbolicSharpMeasuresUnitDefinition DefaultDefiniton() => SymbolicSharpMeasuresUnitDefinition.Empty;

    private class AttributeParser : AAttributeParser<SymbolicSharpMeasuresUnitDefinition, SharpMeasuresUnitLocations, SharpMeasuresUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SharpMeasuresUnitProperties.AllProperties) { }
    }
}
