namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class DerivedUnitParser
{
    public static IAttributeParser<DerivedUnitDefinition> Parser { get; } = new AttributeParser();

    private static DerivedUnitDefinition DefaultDefinition() => DerivedUnitDefinition.Empty;

    private class AttributeParser : AUnitParser<DerivedUnitDefinition, DerivedUnitParsingData, DerivedUnitLocations, DerivedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DerivedUnitProperties.AllProperties) { }
    }
}
