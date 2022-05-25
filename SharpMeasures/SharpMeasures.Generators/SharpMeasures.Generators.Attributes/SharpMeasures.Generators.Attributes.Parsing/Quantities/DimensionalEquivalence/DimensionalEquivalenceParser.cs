namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Quantities;

public static class DimensionalEquivalenceParser
{
    public static IAttributeParser<DimensionalEquivalenceDefinition> Parser { get; } = new AttributeParser();

    private static DimensionalEquivalenceDefinition DefaultDefinition() => DimensionalEquivalenceDefinition.Empty;

    private class AttributeParser
        : AAttributeParser<DimensionalEquivalenceDefinition, DimensionalEquivalenceParsingData, DimensionalEquivalenceLocations, DimensionalEquivalenceAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DimensionalEquivalenceProperties.AllProperties) { }
    }
}
