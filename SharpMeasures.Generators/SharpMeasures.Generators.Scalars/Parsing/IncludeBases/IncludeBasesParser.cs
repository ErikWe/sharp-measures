namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;

internal static class IncludeBasesParser
{
    public static IAttributeParser<RawUnitListDefinition> Parser { get; } = new AttributeParser();

    private static RawUnitListDefinition DefaultDefinition() => RawUnitListDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawUnitListDefinition, UnitListLocations, IncludeBasesAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, IncludeBasesProperties.AllProperties) { }
    }
}
