namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class UnitAliasParser
{
    public static IAttributeParser<UnitAliasDefinition> Parser { get; } = new AttributeParser();

    private static UnitAliasDefinition DefaultDefinition() => UnitAliasDefinition.Empty;

    private class AttributeParser : AUnitParser<UnitAliasDefinition, UnitAliasParsingData, UnitAliasLocations, UnitAliasAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, UnitAliasProperties.AllProperties) { }
    }
}
