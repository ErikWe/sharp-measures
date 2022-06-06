namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Quantities;

public static class DimensionalEquivalenceParser
{
    public static IAttributeParser<RawDimensionalEquivalence> Parser { get; } = new AttributeParser();

    private static RawDimensionalEquivalence DefaultDefinition() => RawDimensionalEquivalence.Empty;

    private class AttributeParser : AAttributeParser<RawDimensionalEquivalence, DimensionalEquivalenceLocations, DimensionalEquivalenceAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DimensionalEquivalenceProperties.AllProperties) { }
    }
}
