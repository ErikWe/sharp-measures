namespace SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using SharpMeasures.Generators.Attributes.Parsing;

public static class ConvertibleQuantityParser
{
    public static IAttributeParser<RawConvertibleQuantityDefinition> Parser { get; } = new AttributeParser();

    private static RawConvertibleQuantityDefinition DefaultDefinition() => RawConvertibleQuantityDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawConvertibleQuantityDefinition, ConvertibleQuantityLocations, ConvertibleQuantityAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ConvertibleQuantityProperties.AllProperties) { }
    }
}
