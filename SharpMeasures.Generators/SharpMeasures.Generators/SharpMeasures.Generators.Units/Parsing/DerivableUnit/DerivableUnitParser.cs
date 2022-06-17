namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class DerivableUnitParser
{
    public static IAttributeParser<RawDerivableUnitDefinition> Instance { get; } = new AttributeParser();

    private static RawDerivableUnitDefinition DefaultDefinition() => RawDerivableUnitDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawDerivableUnitDefinition, DerivableUnitLocations, DerivableUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DerivableUnitProperties.AllProperties) { }
    }
}
