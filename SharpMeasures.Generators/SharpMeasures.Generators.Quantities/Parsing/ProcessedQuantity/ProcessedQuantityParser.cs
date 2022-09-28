namespace SharpMeasures.Generators.Quantities.Parsing.ProcessedQuantity;

using SharpMeasures.Generators.Attributes.Parsing;

public static class ProcessedQuantityParser
{
    public static IAttributeParser<RawProcessedQuantityDefinition> Parser { get; } = new AttributeParser();

    private static RawProcessedQuantityDefinition DefaultDefinition() => RawProcessedQuantityDefinition.Empty;

    private sealed class AttributeParser : AAttributeParser<RawProcessedQuantityDefinition, ProcessedQuantityLocations, ProcessedQuantityAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ProcessedQuantityProperties.AllProperties) { }
    }
}
