namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class DerivedUnitParser
{
    public static IAttributeParser<RawDerivedUnit> Parser { get; } = new AttributeParser();

    private static RawDerivedUnit DefaultDefinition() => RawDerivedUnit.Empty;

    private class AttributeParser : AUnitParser<RawDerivedUnit, DerivedUnitParsingData, DerivedUnitLocations, DerivedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DerivedUnitProperties.AllProperties) { }
    }
}
