namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public static class UnitAliasParser
{
    public static IAttributeParser<RawUnitAlias> Parser { get; } = new AttributeParser();

    private static RawUnitAlias DefaultDefinition() => RawUnitAlias.Empty;

    private class AttributeParser : AUnitParser<RawUnitAlias, UnitAliasParsingData, UnitAliasLocations, UnitAliasAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, UnitAliasProperties.AllProperties) { }
    }
}
