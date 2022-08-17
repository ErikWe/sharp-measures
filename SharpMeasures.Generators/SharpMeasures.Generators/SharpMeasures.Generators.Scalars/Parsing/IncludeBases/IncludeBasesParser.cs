namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;

internal static class IncludeBasesParser
{
    public static IAttributeParser<UnprocessedUnitListDefinition> Parser { get; } = new AttributeParser();

    private static UnprocessedUnitListDefinition DefaultDefinition() => UnprocessedUnitListDefinition.Empty;

    private class AttributeParser : AAttributeParser<UnprocessedUnitListDefinition, UnitListLocations, IncludeBasesAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, IncludeBasesProperties.AllProperties) { }
    }
}
