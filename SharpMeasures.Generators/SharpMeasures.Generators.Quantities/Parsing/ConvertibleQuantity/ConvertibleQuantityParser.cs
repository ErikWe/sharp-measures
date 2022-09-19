namespace SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using SharpMeasures.Generators.Attributes.Parsing;

public static class ConvertibleQuantityParser
{
    public static IAttributeParser<SymbolicConvertibleQuantityDefinition> Parser { get; } = new AttributeParser();

    private static SymbolicConvertibleQuantityDefinition DefaultDefinition() => SymbolicConvertibleQuantityDefinition.Empty;

    private sealed class AttributeParser : AAttributeParser<SymbolicConvertibleQuantityDefinition, ConvertibleQuantityLocations, ConvertibleQuantityAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ConvertibleQuantityProperties.AllProperties) { }
    }
}
