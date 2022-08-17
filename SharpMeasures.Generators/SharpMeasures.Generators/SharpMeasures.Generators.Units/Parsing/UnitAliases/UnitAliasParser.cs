namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class UnitAliasParser
{
    public static IAttributeParser<UnprocessedUnitAliasDefinition> Parser { get; } = new AttributeParser();

    private static UnprocessedUnitAliasDefinition DefaultDefinition() => UnprocessedUnitAliasDefinition.Empty;

    private class AttributeParser : AAttributeParser<UnprocessedUnitAliasDefinition, UnitAliasLocations, UnitAliasAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, UnitAliasProperties.AllProperties) { }
    }
}
