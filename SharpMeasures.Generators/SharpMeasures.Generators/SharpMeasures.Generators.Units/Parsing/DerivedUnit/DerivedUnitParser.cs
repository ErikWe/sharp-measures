namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class DerivedUnitParser
{
    public static IAttributeParser<UnprocessedDerivedUnitDefinition> Parser { get; } = new AttributeParser();

    private static UnprocessedDerivedUnitDefinition DefaultDefinition() => UnprocessedDerivedUnitDefinition.Empty;

    private class AttributeParser : AAttributeParser<UnprocessedDerivedUnitDefinition, DerivedUnitLocations, DerivedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, DerivedUnitProperties.AllProperties) { }
    }
}
