namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class UnitAliasParser
{
    public static IAttributeParser<RawUnitAliasDefinition> Parser { get; } = new AttributeParser();

    private static RawUnitAliasDefinition DefaultDefinition() => RawUnitAliasDefinition.Empty;

    private class AttributeParser : AUnitParser<RawUnitAliasDefinition, UnitAliasParsingData, UnitAliasLocations, UnitAliasAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, UnitAliasProperties.AllProperties) { }
    }
}
