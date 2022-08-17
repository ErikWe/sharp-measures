namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;

internal static class ExcludeBasesParser
{
    public static IAttributeParser<UnprocessedUnitListDefinition> Parser { get; } = new AttributeParser();

    private static UnprocessedUnitListDefinition DefaultDefinition() => UnprocessedUnitListDefinition.Empty;

    private class AttributeParser : AAttributeParser<UnprocessedUnitListDefinition, UnitListLocations, ExcludeBasesAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ExcludeBasesProperties.AllProperties) { }
    }
}
