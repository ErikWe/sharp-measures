namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class DerivableUnitParser
{
    public static IAttributeParser<RawDerivableUnit> Parser { get; } = new AttributeParser();

    private static RawDerivableUnit DefaultDefinition() => RawDerivableUnit.Empty;

    private class AttributeParser : AAttributeParser<RawDerivableUnit, DerivableUnitLocations, DerivableUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DerivableUnitProperties.AllProperties) { }
    }
}
