namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class DerivableUnitParser
{
    public static IAttributeParser<SymbolicDerivableUnitDefinition> Parser { get; } = new AttributeParser();

    private static SymbolicDerivableUnitDefinition DefaultDefinition() => SymbolicDerivableUnitDefinition.Empty;

    private sealed class AttributeParser : AAttributeParser<SymbolicDerivableUnitDefinition, DerivableUnitLocations, DerivableUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DerivableUnitProperties.AllProperties) { }
    }
}
