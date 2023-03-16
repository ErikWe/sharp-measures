namespace SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;

using SharpMeasures.Generators.Attributes.Parsing;

public static class QuantityProcessParser
{
    public static IAttributeParser<RawQuantityProcessDefinition> Parser { get; } = new AttributeParser();

    private static RawQuantityProcessDefinition DefaultDefinition() => RawQuantityProcessDefinition.Empty;

    private sealed class AttributeParser : AAttributeParser<RawQuantityProcessDefinition, QuantityProcessLocations, QuantityProcessAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, QuantityProcessProperties.AllProperties) { }
    }
}
