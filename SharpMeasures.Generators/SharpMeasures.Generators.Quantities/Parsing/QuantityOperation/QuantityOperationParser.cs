namespace SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;

using SharpMeasures.Generators.Attributes.Parsing;

public static class QuantityOperationParser
{
    public static IAttributeParser<SymbolicQuantityOperationDefinition> Parser { get; } = new AttributeParser();

    private static SymbolicQuantityOperationDefinition DefaultDefinition() => SymbolicQuantityOperationDefinition.Empty;

    private sealed class AttributeParser : AAttributeParser<SymbolicQuantityOperationDefinition, QuantityOperationLocations, QuantityOperationAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, QuantityOperationProperties.AllProperties) { }
    }
}
