namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal static class UnitAliasParser
{
    public static IAttributeParser<RawUnitAliasDefinition> Instance { get; } = new AttributeParser();

    private static RawUnitAliasDefinition DefaultDefinition() => RawUnitAliasDefinition.Empty;

    private class AttributeParser : AUnitParser<RawUnitAliasDefinition, UnitAliasParsingData, UnitAliasLocations, UnitAliasAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, UnitAliasProperty.AllProperties) { }
    }
}
