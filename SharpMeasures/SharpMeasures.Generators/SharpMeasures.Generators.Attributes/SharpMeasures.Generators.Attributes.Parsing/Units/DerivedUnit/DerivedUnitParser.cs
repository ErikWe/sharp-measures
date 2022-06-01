namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class DerivedUnitParser
{
    public static IAttributeParser<RawDerivedUnitDefinition> Parser { get; } = new AttributeParser();

    private static RawDerivedUnitDefinition DefaultDefinition() => RawDerivedUnitDefinition.Empty;

    private class AttributeParser : AUnitParser<RawDerivedUnitDefinition, DerivedUnitParsingData, DerivedUnitLocations, DerivedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DerivedUnitProperties.AllProperties) { }
    }
}
