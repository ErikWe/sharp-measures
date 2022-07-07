namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class UnitAliasParser
{
    public static IAttributeParser<RawUnitAliasDefinition> Instance { get; } = new AttributeParser();

    private static RawUnitAliasDefinition DefaultDefinition() => RawUnitAliasDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawUnitAliasDefinition, UnitAliasLocations, UnitAliasAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, UnitAliasProperties.AllProperties) { }
    }
}
