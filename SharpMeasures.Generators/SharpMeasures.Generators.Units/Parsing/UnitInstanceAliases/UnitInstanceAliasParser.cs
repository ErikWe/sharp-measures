namespace SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class UnitInstanceAliasParser
{
    public static IAttributeParser<RawUnitInstanceAliasDefinition> Parser { get; } = new AttributeParser();

    private static RawUnitInstanceAliasDefinition DefaultDefinition() => RawUnitInstanceAliasDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawUnitInstanceAliasDefinition, UnitInstanceAliasLocations, UnitInstanceAliasAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, UnitInstanceAliasProperties.AllProperties) { }
    }
}
