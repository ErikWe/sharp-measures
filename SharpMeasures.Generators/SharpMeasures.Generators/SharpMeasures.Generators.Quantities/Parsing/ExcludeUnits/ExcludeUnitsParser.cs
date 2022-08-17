namespace SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;

public static class ExcludeUnitsParser
{
    public static IAttributeParser<UnprocessedUnitListDefinition> Parser { get; } = new AttributeParser();

    private static UnprocessedUnitListDefinition DefaultDefinition() => UnprocessedUnitListDefinition.Empty;

    private class AttributeParser : AAttributeParser<UnprocessedUnitListDefinition, UnitListLocations, ExcludeUnitsAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ExcludeUnitsProperties.AllProperties) { }
    }
}
