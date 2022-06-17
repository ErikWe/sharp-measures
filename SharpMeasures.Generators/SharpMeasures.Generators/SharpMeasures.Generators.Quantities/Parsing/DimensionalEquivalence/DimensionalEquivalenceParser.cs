namespace SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;

using SharpMeasures.Generators.Attributes.Parsing;

public static class DimensionalEquivalenceParser
{
    public static IAttributeParser<RawDimensionalEquivalenceDefinition> Parser { get; } = new AttributeParser();

    private static RawDimensionalEquivalenceDefinition DefaultDefinition() => RawDimensionalEquivalenceDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawDimensionalEquivalenceDefinition, DimensionalEquivalenceLocations, DimensionalEquivalenceAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DimensionalEquivalenceProperties.AllProperties) { }
    }
}
