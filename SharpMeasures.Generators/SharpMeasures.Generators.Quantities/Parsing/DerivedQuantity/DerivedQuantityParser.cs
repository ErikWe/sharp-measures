namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Generators.Attributes.Parsing;

public static class DerivedQuantityParser
{
    public static IAttributeParser<SymbolicDerivedQuantityDefinition> Parser { get; } = new AttributeParser();

    private static SymbolicDerivedQuantityDefinition DefaultDefinition() => SymbolicDerivedQuantityDefinition.Empty;

    private class AttributeParser : AAttributeParser<SymbolicDerivedQuantityDefinition, DerivedQuantityLocations, DerivedQuantityAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DerivedQuantityProperties.AllProperties) { }
    }
}
