namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Generators.Attributes.Parsing;

public static class DerivedQuantityParser
{
    public static IAttributeParser<RawDerivedQuantityDefinition> Parser { get; } = new AttributeParser();

    private static RawDerivedQuantityDefinition DefaultDefinition() => RawDerivedQuantityDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawDerivedQuantityDefinition, DerivedQuantityLocations, DerivedQuantityAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DerivedQuantityProperties.AllProperties) { }
    }
}
