namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Quantities;

public static class DimensionalEquivalenceParser
{
    public static IAttributeParser<RawDimensionalEquivalenceDefinition> Parser { get; } = new AttributeParser();

    private static RawDimensionalEquivalenceDefinition DefaultDefinition() => RawDimensionalEquivalenceDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawDimensionalEquivalenceDefinition, DimensionalEquivalenceLocations, DimensionalEquivalenceAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DimensionalEquivalenceProperties.AllProperties) { }
    }
}
