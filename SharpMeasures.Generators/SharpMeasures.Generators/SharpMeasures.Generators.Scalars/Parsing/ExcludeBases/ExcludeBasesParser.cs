namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;

internal static class ExcludeBasesParser
{
    public static IAttributeParser<RawUnitListDefinition> Parser { get; } = new AttributeParser();

    private static RawUnitListDefinition DefaultDefinition() => RawUnitListDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawUnitListDefinition, UnitListLocations, ExcludeBasesAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ExcludeBasesProperties.AllProperties) { }
    }
}
