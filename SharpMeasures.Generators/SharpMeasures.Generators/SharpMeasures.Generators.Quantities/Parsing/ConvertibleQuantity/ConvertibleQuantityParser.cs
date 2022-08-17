namespace SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using SharpMeasures.Generators.Attributes.Parsing;

public static class ConvertibleQuantityParser
{
    public static IAttributeParser<UnprocessedConvertibleQuantityDefinition> Parser { get; } = new AttributeParser();

    private static UnprocessedConvertibleQuantityDefinition DefaultDefinition() => UnprocessedConvertibleQuantityDefinition.Empty;

    private class AttributeParser : AAttributeParser<UnprocessedConvertibleQuantityDefinition, ConvertibleQuantityLocations, ConvertibleQuantityAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ConvertibleQuantityProperties.AllProperties) { }
    }
}
