namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Generators.Attributes.Parsing;

public static class DerivedQuantityParser
{
    public static IAttributeParser<UnprocessedDerivedQuantityDefinition> Parser { get; } = new AttributeParser();

    private static UnprocessedDerivedQuantityDefinition DefaultDefinition() => UnprocessedDerivedQuantityDefinition.Empty;

    private class AttributeParser : AAttributeParser<UnprocessedDerivedQuantityDefinition, DerivedQuantityLocations, DerivedQuantityAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DerivedQuantityProperties.AllProperties) { }
    }
}
